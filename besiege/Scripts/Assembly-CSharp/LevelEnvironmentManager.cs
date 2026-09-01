using System;
using System.Collections.Generic;
using BesiegeDlc;
using UnityEngine;

public class LevelEnvironmentManager : MonoBehaviour
{
	[Serializable]
	public struct WaterSettings
	{
		public Material material;

		public Material fogMat;

		public Cubemap cubeMap;
	}

	public LevelSettings.LevelEnvironment currentEnv;

	public Renderer floorRenderer;

	public Camera mainCam;

	public ColorfulFog colorfulFog;

	public Light dirLight;

	public BloomAndLensFlares bloom;

	public ColorCorrectionLut colorCorrectionLut;

	public Texture2D defaultColorCorrectionLut;

	public LevelEnvironment[] environments;

	private bool somethingToDisable = true;

	private WaterFogController waterFog;

	public WaterSettings[] waterTypes = new WaterSettings[0];

	internal void Start()
	{
		List<LevelEnvironment> list = new List<LevelEnvironment>();
		for (int i = 0; i < environments.Length; i++)
		{
			LevelEnvironment levelEnvironment = environments[i];
			if (DlcManager.Instance.AddEnv(levelEnvironment))
			{
				list.Add(levelEnvironment);
				continue;
			}
			for (int j = 0; j < levelEnvironment.envSetup.Length; j++)
			{
				GameObject gameObject = levelEnvironment.envSetup[j];
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
			for (int j = 0; j < levelEnvironment.activateComponent.Length; j++)
			{
				MonoBehaviour monoBehaviour = levelEnvironment.activateComponent[j];
				if (monoBehaviour != null)
				{
					UnityEngine.Object.Destroy(monoBehaviour);
				}
			}
		}
		environments = list.ToArray();
	}

	public LevelEnvironment GetEnv(LevelSettings.LevelEnvironment env)
	{
		for (int i = 0; i < environments.Length; i++)
		{
			LevelEnvironment levelEnvironment = environments[i];
			if (levelEnvironment.env == env)
			{
				return levelEnvironment;
			}
		}
		return null;
	}

	public void SetEnvironment(LevelSettings.LevelEnvironment env)
	{
		if (env == currentEnv)
		{
			return;
		}
		LevelEnvironment env2 = GetEnv(env);
		if (env2 == null)
		{
			env = LevelSettings.LevelEnvironment.Barren;
			env2 = GetEnv(env);
		}
		StatMaster.SetLevelEnvironment(env);
		LevelEnvironment env3 = GetEnv(currentEnv);
		if (env3 != null && somethingToDisable)
		{
			for (int i = 0; i < env3.envSetup.Length; i++)
			{
				env3.envSetup[i].SetActive(false);
			}
			for (int i = 0; i < env3.activateComponent.Length; i++)
			{
				env3.activateComponent[i].enabled = false;
			}
			if (env3.reparentEnvRoot)
			{
				env3.envRoot.parent = env3.envParent;
			}
			for (int i = 0; i < env3.physicsGoalChildren.Length; i++)
			{
				env3.physicsGoalChildren[i].SetParent(env3.localGoalObj, true);
			}
		}
		if (env2.env == LevelSettings.LevelEnvironment.Water)
		{
			if (Shader.IsKeywordEnabled("_UseNormalFog"))
			{
				Shader.DisableKeyword("_UseNormalFog");
			}
		}
		else if (!Shader.IsKeywordEnabled("_UseNormalFog"))
		{
			Shader.EnableKeyword("_UseNormalFog");
		}
		if (env3.env == LevelSettings.LevelEnvironment.Water && env2.env != LevelSettings.LevelEnvironment.Water && WaterController.Exist)
		{
			WaterController.Disable();
		}
		currentEnv = env;
		SingleInstanceFindOnly<AddPiece>.Instance.floorHeight = env2.floorHeight + 5f;
		floorRenderer.transform.position = new Vector3(floorRenderer.transform.position.x, env2.floorHeight, floorRenderer.transform.position.z);
		if (env != LevelSettings.LevelEnvironment.LoadingMultiverse)
		{
			NetworkAddPiece instance = NetworkAddPiece.Instance;
			instance.CalculateWorldBoundaries();
			if (instance.boundVisCode != null)
			{
				instance.boundVisCode.SetFloorPos(StatMaster.Bounding.Enabled);
			}
			for (int i = 0; i < Playerlist.Players.Count; i++)
			{
				PlayerBuildZone buildZone = Playerlist.Players[i].buildZone;
				if (buildZone != null)
				{
					buildZone.UpdateTeam(Playerlist.Players[i].team, env);
				}
			}
		}
		somethingToDisable = true;
		floorRenderer.material = env2.floorMaterial;
		if ((bool)colorfulFog)
		{
			colorfulFog.enabled = env2.hasColoredFog;
		}
		if (env2.hasRenderSettingsFog)
		{
			RenderSettings.fog = true;
			RenderSettings.fogColor = env2.renderSettingsFogColor;
		}
		else
		{
			RenderSettings.fog = false;
		}
		if (colorCorrectionLut != null)
		{
			Texture2D texture2D = ((!env2.hasCustomColorCorrectionLut) ? defaultColorCorrectionLut : env2.customColorCorrectionLut);
			colorCorrectionLut.Convert(texture2D, "<set by LevelEnvironmentManager: " + texture2D.name + ">");
		}
		if (bloom != null)
		{
			bloom.lensflareIntensity = env2.lensFlareIntensity;
		}
		dirLight.color = env2.dirLightColor;
		dirLight.transform.rotation = Quaternion.Euler(env2.dirLightEulerRot);
		dirLight.intensity = env2.dirLightIntensity;
		dirLight.cookieSize = env2.dirLightCookieSize;
		for (int i = 0; i < env2.envSetup.Length; i++)
		{
			env2.envSetup[i].SetActive(true);
		}
		for (int i = 0; i < env2.activateComponent.Length; i++)
		{
			env2.activateComponent[i].enabled = true;
		}
		for (int i = 0; i < env2.physicsGoalChildren.Length; i++)
		{
			env2.physicsGoalChildren[i].SetParent(SingleInstanceFindOnly<AddPiece>.Instance.PhysicsGoalObject, true);
			if (StatMaster.levelSimulating || StatMaster.isLocalSim)
			{
				UnityEngine.Object.Instantiate(env2.physicsGoalChildren[i], ReferenceMaster.physicsGoalInstance, true);
			}
		}
		if (env2.hasColoredFog)
		{
			SetFogShaderValues(colorfulFog);
		}
		if (ReferenceMaster.onLevelEditorEnvironmentChanged != null)
		{
			ReferenceMaster.onLevelEditorEnvironmentChanged(currentEnv);
		}
		if (env2.env == LevelSettings.LevelEnvironment.Water && ReferenceMaster.onShadowsChanged != null)
		{
			ReferenceMaster.onShadowsChanged();
		}
	}

	private void SetFogShaderValues(ColorfulFog fog)
	{
		Color color = Color.black;
		float num = 500f;
		float value = 1000f;
		if (fog.distanceFog)
		{
			switch (fog.coloringMode)
			{
			case ColorfulFog.ColoringMode.Solid:
				color = fog.solidColor;
				break;
			case ColorfulFog.ColoringMode.Cube:
				color = Color.gray;
				break;
			}
			num = fog.startDistance;
			value = num + 250f;
		}
		Shader.SetGlobalColor("_FogVolumeColor", color);
		Shader.SetGlobalColor("_FogInscatteringColor", Color.black);
		Shader.SetGlobalFloat("_FogVolumeMin", num);
		Shader.SetGlobalFloat("_FogVolumeMax", value);
		Shader.SetGlobalVector("_FogLightDir", Vector3.forward);
	}

	public void UpdateWaterHeight(float h)
	{
		if (!waterFog)
		{
			waterFog = UnityEngine.Object.FindObjectOfType<WaterFogController>();
		}
		if ((bool)waterFog)
		{
			(waterFog.AbovewaterFogComponent as ColorfulFog).height = 5.75f + h;
			(waterFog.aboveWaterComponents[0] as ColorfulFog).height = 65f + h;
		}
		WaterController.SetHeight(h);
	}

	public void UpdateEnvironmentType(int i)
	{
		WaterSettings waterSettings = waterTypes[i];
		if ((bool)waterFog)
		{
			ColorfulFog colorfulFog = waterFog.AbovewaterFogComponent as ColorfulFog;
			colorfulFog.fogCube = waterSettings.cubeMap;
			WaterController.SetMaterial(waterSettings.material, waterSettings.fogMat);
			waterFog.SetMaterial(waterSettings.material);
		}
	}
}
