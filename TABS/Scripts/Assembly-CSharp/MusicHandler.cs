using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : ServicePrefab
{
	private const string MAP_CREATOR_SCENE_NAME = "LevelScene";

	private const string BATTLE_SUFFIX = "/Battle";

	private const string PLACEMENT_SUFFIX = "/Placement";

	private bool loadingEditorScene;

	private bool loadingCustomMap;

	private AudioSource[] sources;

	public int mainAudioSourceID;

	public int otherAudioSourceID = 1;

	public SoundBank bank;

	public Dictionary<string, SongInstance> m_songs = new Dictionary<string, SongInstance>();

	private SongInstance m_currentSong;

	public string OverrideSongCategory { get; set; }

	public override void OnAwake()
	{
		loadingEditorScene = false;
		Init();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		loadingCustomMap = false;
		if (scene.name.Equals("LevelScene"))
		{
			loadingEditorScene = true;
		}
		else if (loadingEditorScene)
		{
			loadingEditorScene = false;
			loadingCustomMap = true;
		}
		else
		{
			OverrideSongCategory = null;
		}
	}

	private void Init()
	{
		sources = GetComponentsInChildren<AudioSource>();
		for (int i = 0; i < bank.Categories.Length; i++)
		{
			for (int j = 0; j < bank.Categories[i].soundEffects.Length; j++)
			{
				SongInstance songInstance = new SongInstance();
				if (bank.Categories[i].soundEffects[j].clipTypes.Length != 0 && bank.Categories[i].soundEffects[j].clipTypes[0].clips.Length != 0)
				{
					songInstance.clip = bank.Categories[i].soundEffects[j].clipTypes[0].clips[0];
					songInstance.soundEffectInstance = bank.Categories[i].soundEffects[j];
					songInstance.songRef = bank.Categories[i].categoryName + "/" + bank.Categories[i].soundEffects[j].soundRef;
					songInstance.positionInSong = 0;
					m_songs.Add(songInstance.songRef, songInstance);
				}
			}
		}
	}

	public void PlayMenuMusic()
	{
		PlaySong("Misc/Menumusic");
	}

	public void PlayUnitCreatorMusic()
	{
		PlaySong("UnitCreator/Jazz");
	}

	public void PlayCreditsMusic()
	{
		PlaySong("Credits/CreditsSong");
	}

	public void PlaySongPlacement(MapAsset mapAsset)
	{
		if (!(mapAsset == null))
		{
			string songCategory = GetSongCategory(mapAsset);
			if (!string.IsNullOrEmpty(songCategory))
			{
				songCategory += "/Placement";
				PlaySong(songCategory);
			}
		}
	}

	public void PlayPlacementSongFromCategory(string songCategory)
	{
		string text = songCategory;
		text += "/Placement";
		PlaySong(text);
	}

	public void PlaySongBattle(MapAsset mapAsset)
	{
		if (!(mapAsset == null))
		{
			string songCategory = GetSongCategory(mapAsset);
			songCategory += "/Battle";
			PlaySong(songCategory);
		}
	}

	public AudioClip[] GetAudioClipsForMapAsset(MapAsset mapAsset)
	{
		if (mapAsset == null)
		{
			return new AudioClip[0];
		}
		string songCategory = GetSongCategory(mapAsset);
		string key = songCategory + "/Placement";
		string key2 = songCategory + "/Battle";
		List<AudioClip> list = new List<AudioClip>();
		if (m_songs.ContainsKey(key))
		{
			list.Add(m_songs[key].clip);
		}
		if (m_songs.ContainsKey(key2))
		{
			list.Add(m_songs[key2].clip);
		}
		return list.ToArray();
	}

	public void PlaySong(string songRef)
	{
		if (!m_songs.ContainsKey(songRef))
		{
			Debug.LogError("You tried to play a song that does not exist. Please only play songs that exist: " + songRef);
			return;
		}
		SongInstance songInstance = m_songs[songRef];
		if (!(songInstance.clip == null))
		{
			if (m_currentSong != null)
			{
				m_songs[m_currentSong.songRef].positionInSong = sources[mainAudioSourceID].timeSamples;
			}
			if (songInstance == m_currentSong)
			{
				Debug.Log("Playing same song, ignoring...");
				return;
			}
			mainAudioSourceID = ((mainAudioSourceID == 0) ? 1 : 0);
			otherAudioSourceID = ((mainAudioSourceID == 0) ? 1 : 0);
			m_currentSong = songInstance;
			sources[mainAudioSourceID].Stop();
			sources[mainAudioSourceID].clip = songInstance.clip;
			StopAllCoroutines();
			StartCoroutine(SongTransition(songInstance.positionInSong));
		}
	}

	private IEnumerator SongTransition(int positionToPlaySong)
	{
		while (sources[otherAudioSourceID].volume > 0f)
		{
			sources[otherAudioSourceID].volume -= Time.deltaTime * 1.5f;
			yield return null;
		}
		sources[otherAudioSourceID].Stop();
		int num = positionToPlaySong;
		for (int i = 0; i < m_currentSong.soundEffectInstance.transitionMeasures.Length; i++)
		{
			int num2 = m_currentSong.clip.samples / m_currentSong.soundEffectInstance.lengthInMeasures * m_currentSong.soundEffectInstance.transitionMeasures[i];
			if (num2 >= num)
			{
				num = num2;
				break;
			}
		}
		if (num == positionToPlaySong)
		{
			num = 0;
		}
		sources[mainAudioSourceID].timeSamples = Mathf.Clamp(num, 0, sources[mainAudioSourceID].clip.samples - 1);
		sources[mainAudioSourceID].Play();
		while (sources[mainAudioSourceID].volume < m_currentSong.soundEffectInstance.volume.x)
		{
			sources[mainAudioSourceID].volume += Time.deltaTime * 1.5f;
			yield return null;
		}
	}

	public void MuteMusic()
	{
		StartCoroutine(MuteMusicCorutine());
	}

	private IEnumerator MuteMusicCorutine()
	{
		float startVolume = sources[mainAudioSourceID].volume;
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * 5f;
			yield return null;
			sources[mainAudioSourceID].volume = Mathf.Lerp(startVolume, 0f, t);
		}
		sources[mainAudioSourceID].volume = 0f;
	}

	private string GetSongCategory(MapAsset mapAsset)
	{
		if (loadingCustomMap && string.IsNullOrEmpty(OverrideSongCategory))
		{
			return null;
		}
		if (!string.IsNullOrEmpty(OverrideSongCategory))
		{
			return OverrideSongCategory;
		}
		if (!(mapAsset != null))
		{
			return string.Empty;
		}
		return mapAsset.SongCategoryName;
	}
}
