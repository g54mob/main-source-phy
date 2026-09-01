using System.Collections.Generic;
using UnityEngine;

public class WaterSplash
{
	private static List<ParticleSystem> m_Splashes = new List<ParticleSystem>();

	private static List<Material> m_InstantiatedMaterials = new List<Material>();

	public static void Play(Vector3 pos, WaterSplashSize splashSize, WaterBlock waterBlock)
	{
		ParticleSystem particleSystem = InstantiateParticleSystem(pos, (splashSize == WaterSplashSize.BIG) ? Prefabs.m_Instance.m_SplashBig : Prefabs.m_Instance.m_SplashSmall);
		if (particleSystem != null)
		{
			m_Splashes.Add(particleSystem);
			particleSystem.Play();
		}
		waterBlock.CreateSplash(pos.x + 0.5f, GetSplashForce(splashSize));
		SimAudio.Play(GetSplashAudioGroup(splashSize), particleSystem.transform.position);
	}

	public static void StopAll()
	{
		foreach (ParticleSystem splash in m_Splashes)
		{
			splash.Stop();
		}
	}

	public static void DestroyAll()
	{
		foreach (ParticleSystem splash in m_Splashes)
		{
			splash.Stop();
			Object.Destroy(splash.gameObject);
		}
		m_Splashes.Clear();
		foreach (Material instantiatedMaterial in m_InstantiatedMaterials)
		{
			Object.Destroy(instantiatedMaterial);
		}
	}

	private static ParticleSystem InstantiateParticleSystem(Vector3 pos, GameObject prefab)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, prefab.transform.rotation);
		if (gameObject == null)
		{
			return null;
		}
		Material[] materials = gameObject.GetComponent<Renderer>().materials;
		m_InstantiatedMaterials.AddRange(materials);
		return gameObject.GetComponent<ParticleSystem>();
	}

	private static string GetSplashAudioGroup(WaterSplashSize splashSize)
	{
		switch (splashSize)
		{
		case WaterSplashSize.BIG:
			return "sfx_water_splash_large";
		case WaterSplashSize.MEDIUM:
			return "sfx_water_splash_med";
		case WaterSplashSize.SMALL:
			return "sfx_water_splash_small";
		default:
			Debug.LogWarningFormat("Unexpected splash size in GetSplashAudioGroup: {0}", splashSize.ToString());
			return "sfx_water_splash_med";
		}
	}

	private static float GetSplashForce(WaterSplashSize splashSize)
	{
		switch (splashSize)
		{
		case WaterSplashSize.BIG:
			return 2.5f;
		case WaterSplashSize.MEDIUM:
			return 1.5f;
		case WaterSplashSize.SMALL:
			return 0.6f;
		default:
			Debug.LogWarningFormat("Unexpected splash size in GetSplashForce: {0}", splashSize.ToString());
			return 1f;
		}
	}
}
