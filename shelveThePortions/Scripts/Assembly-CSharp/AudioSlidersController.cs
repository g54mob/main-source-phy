using System;

public class AudioSlidersController : Singleton<AudioSlidersController>
{
	public Action OnSlidersChange;

	public void InvokeOnSlidersChange()
	{
		if (OnSlidersChange != null)
		{
			OnSlidersChange();
		}
	}

	public static float GetVolumePercent(AudioType audioType)
	{
		return audioType switch
		{
			AudioType.SFX => GetSFXVolume(), 
			AudioType.Music => GetMusicVolume(), 
			AudioType.Environment => GetEnvironmentVolume(), 
			_ => 1f, 
		};
	}

	public static void SetMasterSlider(float vol)
	{
		SaveSystem.SetMasterSlider(vol);
		Singleton<AudioSlidersController>.Instance.InvokeOnSlidersChange();
	}

	public static float GetMusicVolume()
	{
		return SaveSystem.GetMusicSlider() * SaveSystem.GetMasterSlider();
	}

	public static void SetMusicSlider(float vol)
	{
		SaveSystem.SetMusicSlider(vol);
		Singleton<AudioSlidersController>.Instance.InvokeOnSlidersChange();
	}

	public static float GetSFXVolume()
	{
		return SaveSystem.GetSFXSlider() * SaveSystem.GetMasterSlider();
	}

	public static void SetSFXSlider(float vol)
	{
		SaveSystem.SetSFXSlider(vol);
		Singleton<AudioSlidersController>.Instance.InvokeOnSlidersChange();
	}

	public static float GetEnvironmentVolume()
	{
		return SaveSystem.GetEnvironmentSlider() * SaveSystem.GetMasterSlider();
	}

	public static void SetEnvironmentSlider(float vol)
	{
		SaveSystem.SetEnvironmentSlider(vol);
		Singleton<AudioSlidersController>.Instance.InvokeOnSlidersChange();
	}
}
