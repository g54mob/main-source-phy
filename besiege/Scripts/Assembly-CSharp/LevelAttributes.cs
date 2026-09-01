using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("Levels/Level Attributes")]
public class LevelAttributes : MonoBehaviour
{
	public int islandId;

	public int levelNameLocalisationIndex;

	[FormerlySerializedAs("endOfIslandLevel")]
	public bool islandFinalLevel;

	public bool campaignFinalLevel;

	public bool sandBoxLevel;

	[Header("Properties")]
	public float floorHeight;

	public static float FloorHeight;

	[Header("Weather Effects")]
	public bool lensRain;

	public bool overlayRain;

	public GameObject rainDropParticles;

	public GameObject rainOverlay;

	[HideInInspector]
	public TextMesh nextZoneText;

	public static LevelAttributes instance;

	public static void FindInstance()
	{
		if (instance == null)
		{
			instance = UnityEngine.Object.FindObjectOfType<LevelAttributes>();
		}
	}

	private void Awake()
	{
		FloorHeight = floorHeight;
		if (sandBoxLevel)
		{
			StatMaster.currentIslandID = ((!WaterController.Exist) ? (-1) : (-2));
		}
		else
		{
			StatMaster.currentIslandID = islandId;
		}
		StampFanfareController.endOfIslandLevel = islandFinalLevel;
		instance = this;
		Island currentIsland = StatMaster.GetCurrentIsland();
		if (currentIsland == Island.Water || currentIsland == Island.WaterSandbox)
		{
			if (Shader.IsKeywordEnabled("_UseNormalFog"))
			{
				Shader.DisableKeyword("_UseNormalFog");
			}
			return;
		}
		if (!Shader.IsKeywordEnabled("_UseNormalFog"))
		{
			Shader.EnableKeyword("_UseNormalFog");
		}
		Shader.DisableKeyword("_IsUnderWater");
	}

	private IEnumerator Start()
	{
		if (rainDropParticles == null)
		{
			rainDropParticles = GameObject.Find("RainDropLensParticles");
		}
		rainDropParticles.SetActive(lensRain);
		if (lensRain || overlayRain)
		{
			WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Combine(WaterFogController.UnderwaterToggled, new Action<bool>(ToggleRain));
		}
		if (rainOverlay == null)
		{
			rainOverlay = GameObject.Find("RainParticleOverlay");
		}
		rainOverlay.SetActive(overlayRain);
		SingleInstanceFindOnly<MouseOrbit>.Instance.yPosClamp = floorHeight + 0.2f;
		SingleInstanceFindOnly<AddPiece>.Instance.floorHeight = floorHeight;
		yield return null;
	}

	private void ToggleRain(bool under)
	{
		if (lensRain && rainDropParticles != null)
		{
			rainDropParticles.SetActive(!under);
		}
		if (overlayRain && rainOverlay != null)
		{
			rainOverlay.SetActive(!under);
		}
	}

	protected void OnDestroy()
	{
		instance = null;
		WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Remove(WaterFogController.UnderwaterToggled, new Action<bool>(ToggleRain));
	}
}
