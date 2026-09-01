using DarkTonic.MasterAudio;
using UnityEngine;

public class ThemeAudio
{
	private static float FADE_OUT_SECONDS = 0.5f;

	private static PlaySoundResult m_ThemeAudioInstance;

	private static PlaySoundResult m_WaterInstance;

	public static void Play(ThemeAudioClip clip)
	{
		if (m_ThemeAudioInstance != null)
		{
			Debug.LogWarningFormat("Trying to start multiple theme ambient tracks. Only one should play at a time.");
			Stop();
		}
		m_ThemeAudioInstance = MasterAudio.PlaySound(GetAudioGroupForThemeAmbience(clip));
		if (!SandboxSettings.m_NoWater)
		{
			PlayWaterSounds(GetAudioGroupForThemeWaterAmbience(clip));
		}
	}

	public static void Stop()
	{
		if (m_ThemeAudioInstance != null)
		{
			m_ThemeAudioInstance.ActingVariation.FadeOutNowAndStop(FADE_OUT_SECONDS);
			m_ThemeAudioInstance = null;
		}
		StopWaterSounds();
	}

	public static void UpdateAmbienceVolume(float vol)
	{
		if (m_ThemeAudioInstance != null)
		{
			m_ThemeAudioInstance.ActingVariation.VarAudio.volume = vol * m_ThemeAudioInstance.ActingVariation.OriginalVolume;
		}
	}

	private static Vector3 GetAmbientAudioOriginForWater()
	{
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		if (!leftTerrain || !rightTerrain)
		{
			Debug.LogWarningFormat("Could not find terrains for computing ambient audio origin");
			return Vector3.zero;
		}
		if (!leftTerrain.m_SpawnPoint || !rightTerrain.m_SpawnPoint)
		{
			Debug.LogWarningFormat("Terrains require spawn points to compute ambient audio origin");
		}
		return new Vector3(((leftTerrain.m_SpawnPoint.transform.position + rightTerrain.m_SpawnPoint.transform.position) / 2f).x, WaterBlocks.GetHeight(), 0f);
	}

	private static void PlayWaterSounds(string audioGroup)
	{
		Vector3 ambientAudioOriginForWater = GetAmbientAudioOriginForWater();
		m_WaterInstance = MasterAudio.PlaySound3DAtVector3(audioGroup, ambientAudioOriginForWater);
	}

	private static void StopWaterSounds()
	{
		if (m_WaterInstance != null)
		{
			m_WaterInstance.ActingVariation.FadeOutNowAndStop(FADE_OUT_SECONDS);
			m_WaterInstance = null;
		}
	}

	private static string GetAudioGroupForThemeAmbience(ThemeAudioClip clip)
	{
		return clip switch
		{
			ThemeAudioClip.NEWDESERT => "amb_biome_newdesert_lp", 
			ThemeAudioClip.ROCKS => "amb_biome_rocks_lp", 
			ThemeAudioClip.ALPINE => "amb_biome_alpine_lp", 
			ThemeAudioClip.SKYSCRAPER => "amb_biome_skyscraper_lp", 
			ThemeAudioClip.VIKING => "amb_biome_viking_lp", 
			ThemeAudioClip.TOXIC => "amb_biome_toxic_lp", 
			ThemeAudioClip.TROPICS => "amb_biome_tropics_lp", 
			ThemeAudioClip.THEWALL => "amb_biome_thewall_lp", 
			ThemeAudioClip.TURNPIKE => "amb_biome_turnpike_lp", 
			ThemeAudioClip.GLACIER => "amb_biome_glacier_lp", 
			ThemeAudioClip.SANTORINI => "amb_biome_santorini_lp", 
			ThemeAudioClip.SANDBOX => "amb_biome_sandbox_lp", 
			ThemeAudioClip.CYBER => "amb_biome_alpineMeadow_lp", 
			ThemeAudioClip.PYRAMIDS => "amb_biome_pyramid_lp", 
			ThemeAudioClip.DESK => "amb_biome_alpineMeadow_lp", 
			_ => string.Empty, 
		};
	}

	private static string GetAudioGroupForThemeWaterAmbience(ThemeAudioClip clip)
	{
		if (clip == ThemeAudioClip.TOXIC)
		{
			return "amb_spot_toxic_water_lp";
		}
		return "amb_spot_water_close_lp";
	}
}
