using MoreMountains.Tools;
using TMPro;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class PlaylistDemo : MonoBehaviour
	{
		public MMSMPlaylistManager PlaylistManager;

		public MMProgressBar ProgressBar;

		public TMP_Text SongName;

		public TMP_Text SongDuration;

		protected virtual void Update()
		{
		}

		protected virtual void UpdateSongName()
		{
		}

		protected virtual void OnPlayEvent(int channel)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
