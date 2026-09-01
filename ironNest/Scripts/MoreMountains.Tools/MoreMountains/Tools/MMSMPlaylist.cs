using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	[CreateAssetMenu(menuName = "MoreMountains/Audio/MMSM Playlist")]
	public class MMSMPlaylist : ScriptableObject
	{
		public enum PlayModes
		{
			PlayForever = 0,
			PlayOnce = 1,
			PlayXTimes = 2
		}

		public enum PlayOrders
		{
			Normal = 0,
			ReverseOrder = 1,
			Random = 2,
			RandomUnique = 3
		}

		[Header("Play Modes")]
		[Tooltip("the sound manager track on which to play this playlist's songs")]
		public MMSoundManager.MMSoundManagerTracks Track;

		[Tooltip("the order in which to play songs (top to bottom, bottom to top, random, or random while trying to maintain playcount across songs")]
		public PlayOrders PlayOrder;

		[Tooltip("if this is true, random seed will be randomized by the system clock")]
		[MMEnumCondition("PlayOrder", new int[] { 2, 3 })]
		public bool RandomizeOrderSeed;

		[Tooltip("whether to play this playlist forever, only once, or play songs until total playcount reaches MaxAmountOfPlays")]
		public PlayModes PlayMode;

		[Tooltip("when in PlayXTimes mode, the max amount of plays before this playlist ends")]
		[MMEnumCondition("PlayMode", new int[] { 2 })]
		public int MaxAmountOfPlays;

		[Tooltip("a playlist to switch to when reaching the end of this playlist")]
		[MMEnumCondition("PlayMode", new int[] { 1, 2 })]
		public MMSMPlaylist NextPlaylist;

		[Tooltip("the list of songs to play on this playlist")]
		public List<MMSMPlaylistSong> Songs;

		[Header("Debug")]
		[Tooltip("the total number of times songs in this playlist have been played ")]
		[MMReadOnly]
		public int PlayCount;

		protected List<int> _randomUniqueCandidates;

		public virtual void Initialization()
		{
		}

		public virtual int PickNextIndex(int direction, int currentSongIndex, ref int queuedSongIndex, bool bypassLoop)
		{
			return 0;
		}

		public virtual void ResetPlayCount()
		{
		}

		protected virtual void OnValidate()
		{
		}
	}
}
