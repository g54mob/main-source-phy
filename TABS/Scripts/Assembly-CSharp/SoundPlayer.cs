using System.Collections.Generic;
using TFBGames;
using UnityEngine;

public class SoundPlayer : ServicePrefab
{
	public List<AudioPlayer> audioPlayers = new List<AudioPlayer>();

	public GameObject defaultSource;

	public SoundBank soundBank;

	public Dictionary<SoundEffectInstance, AudioPlayerListWithFloat> soundEffectDictionary = new Dictionary<SoundEffectInstance, AudioPlayerListWithFloat>();

	private const float MAX_CLIP_AGE = 300f;

	private Dictionary<AudioClip, float> m_audioClipAge = new Dictionary<AudioClip, float>();

	public AudioPlayer PlaySoundEffect(string soundRef, float volumeMultiplier, Vector3 position, SoundEffectVariations.MaterialType materialType = SoundEffectVariations.MaterialType.Default, Transform transformToFollow = null, float pitchMultiplier = 1f)
	{
		if (!soundBank)
		{
			return null;
		}
		SoundEffectInstance soundEffect = soundBank.GetSoundEffect(soundRef);
		if (soundEffect == null)
		{
			return null;
		}
		if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Menu && soundEffect.spatialBlend > 0.1f)
		{
			return null;
		}
		AudioPlayer availableSource;
		Play(soundEffect, availableSource = GetAvailableSource(), volumeMultiplier, position, materialType, transformToFollow, pitchMultiplier);
		return availableSource;
	}

	public AudioPlayer PlaySoundEffectNonAlloc(AudioPathData audioPathData, float volumeMultiplier, Vector3 position, SoundEffectVariations.MaterialType materialType = SoundEffectVariations.MaterialType.Default, Transform transformToFollow = null, float pitchMultiplier = 1f)
	{
		if (!soundBank)
		{
			return null;
		}
		if (audioPathData == null)
		{
			return null;
		}
		SoundEffectInstance soundEffectNonAlloc = soundBank.GetSoundEffectNonAlloc(audioPathData.Category, audioPathData.Effect, audioPathData.EffectVolume, audioPathData.EffectPitch);
		if (soundEffectNonAlloc == null)
		{
			return null;
		}
		if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Menu && soundEffectNonAlloc.spatialBlend > 0.1f)
		{
			return null;
		}
		AudioPlayer availableSource = GetAvailableSource();
		Play(soundEffectNonAlloc, availableSource, volumeMultiplier, position, materialType, transformToFollow, pitchMultiplier);
		return availableSource;
	}

	private void Play(SoundEffectInstance soundEffect, AudioPlayer player, float volumeMultiplier, Vector3 position, SoundEffectVariations.MaterialType materialType, Transform transformToFollow, float pitchMultiplier)
	{
		bool flag = false;
		if (soundEffect.clipTypes == null || soundEffect.clipTypes.Length == 0 || soundEffect.clipTypes[0].clips == null || soundEffect.clipTypes[0].clips.Length == 0)
		{
			return;
		}
		if (soundEffectDictionary.ContainsKey(soundEffect))
		{
			if (soundEffectDictionary[soundEffect].lastPlayed + soundEffect.cooldown > Time.unscaledTime)
			{
				return;
			}
			if (soundEffectDictionary[soundEffect].audioPlayerList.Count >= soundEffect.maxInstances && soundEffect.maxInstanceBehaviour == SoundEffectInstance.MaxInstanceBehaviour.Steal)
			{
				player = soundEffectDictionary[soundEffect].GetLastAssigned();
				flag = true;
			}
		}
		AudioClip audioClip;
		if (materialType == SoundEffectVariations.MaterialType.Default)
		{
			audioClip = soundEffect.clipTypes[0].clips[Random.Range(0, soundEffect.clipTypes[0].clips.Length)];
		}
		else
		{
			int iDFromMeterialType = soundEffect.GetIDFromMeterialType(materialType);
			audioClip = ((soundEffect.clipTypes.Length <= 1 || soundEffect.clipTypes[iDFromMeterialType].clips.Length == 0) ? soundEffect.clipTypes[0].clips[Random.Range(0, soundEffect.clipTypes[0].clips.Length)] : soundEffect.clipTypes[iDFromMeterialType].clips[Random.Range(0, soundEffect.clipTypes[iDFromMeterialType].clips.Length)]);
		}
		UpdateClipAge(audioClip);
		if ((bool)audioClip)
		{
			player.source.clip = audioClip;
			player.isUsed = true;
			player.source.loop = false;
			player.source.time = 0f;
			player.source.Stop();
			player.source.priority = soundEffect.priority;
			float num = Random.Range(soundEffect.pitch.x, soundEffect.pitch.y) * soundEffect.category.pitchMultiplier * pitchMultiplier;
			if (num == 0f)
			{
				num = 1f;
			}
			player.source.pitch = num;
			float volume = Random.Range(soundEffect.volume.x, soundEffect.volume.y) * volumeMultiplier * soundEffect.category.volumeMultiplier;
			player.source.volume = volume;
			player.source.minDistance = soundEffect.minDistance * soundEffect.category.minDistanceMultiplier;
			player.source.maxDistance = soundEffect.maxDistance * soundEffect.category.maxDistanceMultiplier;
			player.source.transform.position = position;
			player.source.spatialBlend = soundEffect.spatialBlend;
			if (soundEffect.overrideMixerGroup != null)
			{
				player.source.outputAudioMixerGroup = soundEffect.overrideMixerGroup;
			}
			else
			{
				player.source.outputAudioMixerGroup = soundEffect.categoryMixerGroup;
			}
			player.source.Play();
			player.clipLength = player.source.clip.length;
			player.currentTimePlayed = 0f;
			player.defaultPitch = num;
			if (player.source.spatialBlend > 0.1f)
			{
				player.source.pitch = num * Time.timeScale;
			}
			player.source.dopplerLevel *= Time.timeScale;
			if (!soundEffectDictionary.ContainsKey(soundEffect))
			{
				soundEffectDictionary.Add(soundEffect, new AudioPlayerListWithFloat());
			}
			if (!flag)
			{
				soundEffectDictionary[soundEffect].audioPlayerList.Add(player);
			}
			soundEffectDictionary[soundEffect].lastPlayed = Time.unscaledTime;
		}
	}

	public void KillAllSoundInstances()
	{
		foreach (KeyValuePair<SoundEffectInstance, AudioPlayerListWithFloat> item in soundEffectDictionary)
		{
			foreach (AudioPlayer audioPlayer in item.Value.audioPlayerList)
			{
				StartCoroutine(audioPlayer.FadeOut());
			}
			item.Value.audioPlayerList.Clear();
		}
	}

	private AudioPlayer GetAvailableSource()
	{
		AudioPlayer audioPlayer = null;
		for (int i = 0; i < audioPlayers.Count; i++)
		{
			if (!audioPlayers[i].isUsed)
			{
				audioPlayer = audioPlayers[i];
				break;
			}
		}
		if (audioPlayer == null)
		{
			audioPlayer = AddNewSource();
			audioPlayers.Add(audioPlayer);
		}
		return audioPlayer;
	}

	private AudioPlayer AddNewSource()
	{
		GameObject obj = Object.Instantiate(defaultSource, base.transform.position, base.transform.rotation);
		obj.transform.SetParent(base.transform, worldPositionStays: true);
		AudioSource component = obj.GetComponent<AudioSource>();
		return new AudioPlayer
		{
			source = component
		};
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
		if (Time.timeSinceLevelLoad % 301f == 0f)
		{
			UnloadOldClips();
		}
		int i = 0;
		for (int count = audioPlayers.Count; i < count; i++)
		{
			AudioPlayer audioPlayer = audioPlayers[i];
			if (audioPlayer.isUsed)
			{
				UpdateAudioPlayerPositionAndPitch(audioPlayer);
			}
		}
	}

	private void UpdateAudioPlayerPositionAndPitch(AudioPlayer audioPlayer)
	{
		if (!audioPlayer.isUsed)
		{
			return;
		}
		if (audioPlayer.currentTimePlayed < audioPlayer.clipLength)
		{
			if (audioPlayer.source.spatialBlend > 0.1f)
			{
				audioPlayer.currentTimePlayed += Time.deltaTime * audioPlayer.source.pitch;
				if (audioPlayer.transformToFollow != null)
				{
					audioPlayer.source.transform.position = audioPlayer.transformToFollow.position;
				}
				audioPlayer.source.pitch = Mathf.Lerp(audioPlayer.source.pitch, audioPlayer.defaultPitch * Time.timeScale, Time.unscaledDeltaTime * 5f);
			}
			else
			{
				audioPlayer.currentTimePlayed += Time.unscaledDeltaTime * audioPlayer.source.pitch;
			}
			return;
		}
		foreach (SoundEffectInstance key in soundEffectDictionary.Keys)
		{
			AudioPlayerListWithFloat audioPlayerListWithFloat = soundEffectDictionary[key];
			if (audioPlayerListWithFloat.audioPlayerList.Contains(audioPlayer))
			{
				audioPlayerListWithFloat.audioPlayerList.Remove(audioPlayer);
			}
		}
		audioPlayer.isUsed = false;
	}

	private void UpdateClipAge(AudioClip clip)
	{
		if (clip != null && !clip.preloadAudioData && clip.loadType != AudioClipLoadType.Streaming)
		{
			if (m_audioClipAge.TryGetValue(clip, out var _))
			{
				m_audioClipAge[clip] = Time.realtimeSinceStartup;
			}
			else
			{
				m_audioClipAge.Add(clip, Time.realtimeSinceStartup);
			}
		}
	}

	private void UnloadOldClips()
	{
		List<AudioClip> list = new List<AudioClip>();
		foreach (KeyValuePair<AudioClip, float> item in m_audioClipAge)
		{
			if (Time.realtimeSinceStartup - item.Value >= 300f)
			{
				item.Key.UnloadAudioData();
				list.Add(item.Key);
			}
		}
		foreach (AudioClip item2 in list)
		{
			m_audioClipAge.Remove(item2);
		}
	}
}
