using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BesiegeDlc;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

[AddComponentMenu("Water/Controllers/Water Fog Controller")]
public class WaterFogController : MonoBehaviour
{
	[Serializable]
	public class ParticleFog
	{
		public Color Color;

		public float Min;

		public float Max = 200f;
	}

	public Vignetting vignette;

	public Camera mainCamera;

	public Camera hudCamera;

	public MonoBehaviour volumetricFog;

	public ColorfulFog orthographicUnderwaterFog;

	public MonoBehaviour AbovewaterFogComponent;

	public MonoBehaviour[] underWaterComponents;

	public MonoBehaviour[] aboveWaterComponents;

	public GameObject godRays;

	public GameObject AboveWaterSound;

	public GameObject UnderWaterSound;

	public Material[] waterMaterails;

	public GameObject[] UnderWaterGameObjectsToternOff;

	public MonoBehaviour[] DestroyIfNotWaterLevel;

	private float height;

	public float offset;

	public static bool overWater;

	private bool firstCheck = true;

	public AudioMixer masterMixer;

	public AudioSource waveSFX;

	public float waveListenRange = 350f;

	public static Action<bool> UnderwaterToggled;

	private float oldFarPlane;

	private float oldVignetteBlur;

	private float oldVignetteIntensity;

	private float oldVignetteBlurSpread;

	private bool init;

	private bool boundsSim = true;

	private static HashSet<Material> BlockEffectParticlesMaterials = new HashSet<Material>();

	private static int Count = 0;

	private bool fogEnabled;

	public ParticleFog particleFogAbove = new ParticleFog
	{
		Color = new Color(0.3f, 0.52f, 0.57f, 1f),
		Min = 200f,
		Max = 400f
	};

	public ParticleFog particleFogBelow = new ParticleFog
	{
		Color = new Color(0.3f, 0.45f, 0.45f, 1f),
		Min = 0f,
		Max = 200f
	};

	public void LevelCleaning()
	{
		UnityEngine.Object.DestroyImmediate(volumetricFog);
		UnityEngine.Object.DestroyImmediate(orthographicUnderwaterFog);
		UnityEngine.Object.DestroyImmediate(AbovewaterFogComponent);
		for (int i = 0; i < underWaterComponents.Length; i++)
		{
			UnityEngine.Object.DestroyImmediate(underWaterComponents[i]);
		}
		for (int j = 0; j < aboveWaterComponents.Length; j++)
		{
			UnityEngine.Object.DestroyImmediate(aboveWaterComponents[j]);
		}
		UnityEngine.Object.DestroyImmediate(godRays);
		UnityEngine.Object.DestroyImmediate(AboveWaterSound);
		UnityEngine.Object.DestroyImmediate(UnderWaterSound);
		for (int k = 0; k < DestroyIfNotWaterLevel.Length; k++)
		{
			UnityEngine.Object.DestroyImmediate(DestroyIfNotWaterLevel[k]);
		}
		if (vignette != null)
		{
			vignette.blur = 0f;
			vignette.intensity = 3.5f;
		}
	}

	private void Init()
	{
		if (init)
		{
			return;
		}
		WaterLod waterLod = UnityEngine.Object.FindObjectOfType<WaterLod>();
		if ((bool)waterLod)
		{
			Material sharedMaterial = waterLod.waterLODs[0].LODMeshrenderer[0].sharedMaterial;
			if (waterMaterails.Length > 0 && !waterMaterails.Contains(sharedMaterial))
			{
				waterMaterails[0] = sharedMaterial;
			}
		}
		PlanarReflections component = mainCamera.GetComponent<PlanarReflections>();
		if ((bool)component && !component.enabled)
		{
			component.Awake();
		}
		vignette = mainCamera.GetComponent<Vignetting>();
		hudCamera = mainCamera.transform.GetChild(0).GetComponent<Camera>();
		if (waveSFX == null)
		{
			WinCondition winCondition = WinCondition.Instance ?? UnityEngine.Object.FindObjectOfType<WinCondition>();
			waveSFX = winCondition.GetComponent<AudioSource>();
		}
		oldFarPlane = mainCamera.farClipPlane;
		oldVignetteBlur = vignette.blur;
		oldVignetteIntensity = vignette.intensity;
		oldVignetteBlurSpread = vignette.blurSpread;
		Shader.SetGlobalColor("_AboveFogColor", particleFogAbove.Color);
		Shader.SetGlobalFloat("_AboveFogMin", particleFogAbove.Min);
		Shader.SetGlobalFloat("_AboveFogMax", particleFogAbove.Max);
		Shader.SetGlobalColor("_BelowFogColor", particleFogBelow.Color);
		Shader.SetGlobalFloat("_BelowFogMin", particleFogBelow.Min);
		Shader.SetGlobalFloat("_BelowFogMax", particleFogBelow.Max);
		init = true;
		Count++;
	}

	public void SetMaterial(Material mat)
	{
		if (waterMaterails.Length > 0)
		{
			waterMaterails[0] = mat;
			if (fogEnabled)
			{
				Set(overWater);
			}
		}
	}

	protected void OnEnable()
	{
		MouseOrbit.CameraMoved = (Action<Vector3>)Delegate.Combine(MouseOrbit.CameraMoved, new Action<Vector3>(UpdateAfterCamera));
		UpdateAfterCamera(Vector3.zero);
		StartCoroutine(OnEnableRoutune());
		if (StatMaster.isMP && !Shader.IsKeywordEnabled("_Simulating"))
		{
			Shader.EnableKeyword("_Simulating");
		}
	}

	protected IEnumerator OnEnableRoutune()
	{
		if (DlcManager.Instance.GetDlcStatus(DlcManager.DlcType.Water) != DlcManager.DlcStatusType.Allowed)
		{
			SceneManager.LoadScene("TITLE SCREEN", LoadSceneMode.Single);
			yield break;
		}
		yield return new WaitForEndOfFrame();
		Init();
		Toggle(true);
	}

	protected void OnDisable()
	{
		MouseOrbit.CameraMoved = (Action<Vector3>)Delegate.Remove(MouseOrbit.CameraMoved, new Action<Vector3>(UpdateAfterCamera));
		firstCheck = true;
		Toggle(false);
	}

	protected void UpdateAfterCamera(Vector3 camPos)
	{
		UpdateWaterHeight();
		bool flag = StatMaster.isMP || StatMaster.levelSimulating || !StatMaster.Bounding.Enabled;
		if (flag != boundsSim)
		{
			if (flag)
			{
				boundsSim = true;
				Shader.EnableKeyword("_Simulating");
			}
			else
			{
				boundsSim = false;
				Shader.DisableKeyword("_Simulating");
			}
		}
	}

	public void Toggle(bool toggle)
	{
		if (toggle)
		{
			Camera camera = mainCamera;
			float farClipPlane = 2600f;
			hudCamera.farClipPlane = farClipPlane;
			camera.farClipPlane = farClipPlane;
			UpdateWaterHeight();
			return;
		}
		fogEnabled = false;
		ClearWaterEffects();
		if (volumetricFog != null)
		{
			volumetricFog.enabled = false;
		}
		if (AbovewaterFogComponent != null)
		{
			AbovewaterFogComponent.enabled = false;
		}
		for (int i = 0; i < aboveWaterComponents.Length; i++)
		{
			if (aboveWaterComponents[i] != null)
			{
				aboveWaterComponents[i].enabled = false;
			}
		}
		for (int j = 0; j < underWaterComponents.Length; j++)
		{
			if (underWaterComponents[j] != null)
			{
				underWaterComponents[j].enabled = false;
			}
		}
		if (vignette != null)
		{
			vignette.blurSpread = oldVignetteBlurSpread;
			vignette.intensity = oldVignetteIntensity;
			vignette.blur = oldVignetteBlur;
		}
		if (mainCamera != null)
		{
			Camera camera2 = mainCamera;
			float farClipPlane = oldFarPlane;
			hudCamera.farClipPlane = farClipPlane;
			camera2.farClipPlane = farClipPlane;
		}
	}

	private void UpdateWaterHeight()
	{
		Vector3 vector = mainCamera.transform.position + mainCamera.transform.forward * mainCamera.nearClipPlane;
		height = WaterController.CheckHeightMap(vector.x, vector.z, true) + offset;
		if (SingleInstanceFindOnly<MouseOrbit>.Instance.IsOrthographic || (vector.y > height && (!overWater || firstCheck)))
		{
			Set(true);
		}
		else if (!SingleInstanceFindOnly<MouseOrbit>.Instance.IsOrthographic && vector.y < height && (overWater || firstCheck))
		{
			Set(false);
		}
		if (SingleInstanceFindOnly<MouseOrbit>.Instance.IsOrthographic)
		{
			orthographicUnderwaterFog.height = height;
			orthographicUnderwaterFog.enabled = true;
		}
		else if (orthographicUnderwaterFog.enabled)
		{
			orthographicUnderwaterFog.enabled = false;
		}
		float num = 1f - Mathf.Clamp01(Mathf.Abs(vector.y - height) / waveListenRange);
		waveSFX.volume = num * 0.8f + 0.2f;
		firstCheck = false;
	}

	private void Set(bool over)
	{
		overWater = over;
		if (volumetricFog != null)
		{
			volumetricFog.enabled = !over;
		}
		if (over)
		{
			vignette.blur = 0f;
			vignette.intensity = 3.5f;
		}
		else
		{
			vignette.blur = 0.5f;
			vignette.intensity = 3.2f;
		}
		if (AboveWaterSound != null)
		{
			AboveWaterSound.SetActive(over);
		}
		if (UnderWaterSound != null)
		{
			UnderWaterSound.SetActive(!over);
		}
		if (AbovewaterFogComponent != null)
		{
			AbovewaterFogComponent.enabled = over;
		}
		for (int i = 0; i < aboveWaterComponents.Length; i++)
		{
			aboveWaterComponents[i].enabled = over;
		}
		for (int j = 0; j < underWaterComponents.Length; j++)
		{
			underWaterComponents[j].enabled = !over;
		}
		if (masterMixer != null)
		{
			masterMixer.SetFloat("ExplosionGain", (!over) ? 9f : 0f);
			masterMixer.SetFloat("SfxDamper", (!over) ? 950 : 22000);
			masterMixer.SetFloat("AmbientDamper", (!over) ? 950 : 22000);
			masterMixer.SetFloat("MusicDamper", (!over) ? 2500 : 22000);
		}
		if (over)
		{
			Shader.DisableKeyword("_IsUnderWater");
		}
		else
		{
			Shader.EnableKeyword("_IsUnderWater");
		}
		for (int k = 0; k < waterMaterails.Length; k++)
		{
			waterMaterails[k].SetFloat("_Cull", (!over) ? 1 : 2);
		}
		for (int l = 0; l < UnderWaterGameObjectsToternOff.Length; l++)
		{
			UnderWaterGameObjectsToternOff[l].SetActive(!over);
		}
		SetPlaneReflection(over);
		foreach (Material blockEffectParticlesMaterial in BlockEffectParticlesMaterials)
		{
			blockEffectParticlesMaterial.renderQueue = ((!over) ? 2999 : 3001);
		}
		if (UnderwaterToggled != null)
		{
			UnderwaterToggled(!over);
		}
		fogEnabled = true;
	}

	public void SetPlaneReflection(bool toggle)
	{
		int reflectionQuality = OptionsMaster.BesiegeConfig.ReflectionQuality;
		toggle = toggle && reflectionQuality != 0;
		for (int i = 0; i < waterMaterails.Length; i++)
		{
			if (toggle)
			{
				waterMaterails[i].EnableKeyword("Reflection_Cam");
				continue;
			}
			waterMaterails[i].DisableKeyword("Reflection_Cam");
			Shader.SetGlobalTexture(Shader.PropertyToID("_PlanarReflectionTexture"), null);
		}
	}

	public void SetReflectionBlur(bool toggle)
	{
		for (int i = 0; i < waterMaterails.Length; i++)
		{
			if (toggle)
			{
				waterMaterails[i].EnableKeyword("Blur_Reflection");
			}
			else
			{
				waterMaterails[i].DisableKeyword("Blur_Reflection");
			}
		}
	}

	public static void AddEffectMat(Material mat)
	{
		if (!BlockEffectParticlesMaterials.Contains(mat) && BlockEffectParticlesMaterials.Count < 100)
		{
			mat.renderQueue = ((!overWater) ? 2999 : 3001);
			BlockEffectParticlesMaterials.Add(mat);
		}
	}

	public void OnDestroy()
	{
		if (Count < 2)
		{
			ClearWaterEffects();
			BlockEffectParticlesMaterials.Clear();
		}
		Count--;
	}

	protected void ClearWaterEffects()
	{
		overWater = true;
		if (Shader.IsKeywordEnabled("_IsUnderwater"))
		{
			Shader.DisableKeyword("_IsUnderWater");
		}
		for (int i = 0; i < waterMaterails.Length; i++)
		{
			waterMaterails[i].SetFloat("_Cull", 0f);
		}
		if (masterMixer != null)
		{
			masterMixer.SetFloat("ExplosionGain", 0f);
			masterMixer.SetFloat("SfxDamper", 22000f);
			masterMixer.SetFloat("AmbientDamper", 22000f);
			masterMixer.SetFloat("MusicDamper", 22000f);
		}
		foreach (Material blockEffectParticlesMaterial in BlockEffectParticlesMaterials)
		{
			blockEffectParticlesMaterial.renderQueue = 3000;
		}
	}
}
