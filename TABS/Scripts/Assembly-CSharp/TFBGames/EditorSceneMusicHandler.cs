using LevelCreator;
using UnityEngine;

namespace TFBGames
{
	public class EditorSceneMusicHandler : MonoBehaviour
	{
		private const float MusicDelayTime = 2f;

		[SerializeField]
		private DMEditor dmEditor;

		private bool isWaitingToPlayMusic;

		private float elapsedTimeWaitingForMusic;

		private int? indexOfMusicToPLay;

		private string nameOfMusicToPlay;

		private MusicHandler musicHander;

		private void Awake()
		{
			musicHander = ServiceLocator.GetService<MusicHandler>();
		}

		private void OnEnable()
		{
			dmEditor.SettingMusicToPlay += BeginCountdownToPlayMusic;
		}

		private void OnDisable()
		{
			dmEditor.SettingMusicToPlay -= BeginCountdownToPlayMusic;
		}

		private void Update()
		{
			if (isWaitingToPlayMusic)
			{
				if (elapsedTimeWaitingForMusic >= 2f)
				{
					PlayEditorMusic();
					isWaitingToPlayMusic = false;
				}
				elapsedTimeWaitingForMusic += Time.unscaledDeltaTime;
			}
		}

		private void BeginCountdownToPlayMusic(int musicIndex, string musicName)
		{
			indexOfMusicToPLay = musicIndex;
			nameOfMusicToPlay = musicName;
			elapsedTimeWaitingForMusic = 0f;
			isWaitingToPlayMusic = true;
		}

		private void PlayEditorMusic()
		{
			if (!indexOfMusicToPLay.HasValue || string.IsNullOrEmpty(nameOfMusicToPlay))
			{
				Debug.LogError("cannot play music");
				return;
			}
			dmEditor.LevelSettings.musicIndex = indexOfMusicToPLay.Value;
			dmEditor.LevelSettingsMenu.m_musicSelector.SetIndexWithoutNotify(indexOfMusicToPLay.Value);
			isWaitingToPlayMusic = false;
			musicHander.PlayPlacementSongFromCategory(nameOfMusicToPlay);
			indexOfMusicToPLay = null;
			nameOfMusicToPlay = string.Empty;
		}
	}
}
