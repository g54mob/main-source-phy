using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILeaderboardEntry : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass34_0
	{
		public UILeaderboardEntry _003C_003E4__this;

		public int pending;

		internal void _003CLoadMapImagesRoutine_003Eb__0(Texture2D texture)
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass35_0
	{
		public UILeaderboardEntry _003C_003E4__this;

		public int version;

		public LeaderboardImageThrottle.ZipFramesRequest handle;

		public int pending;

		internal void _003CLoadZipFramesRoutine_003Eb__0(Texture2D preview)
		{
		}

		internal void _003CLoadZipFramesRoutine_003Eb__1(Texture2D[] frames)
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass36_0
	{
		public string replayPath;

		public UILeaderboardEntry _003C_003E4__this;

		public int version;

		public LeaderboardImageThrottle.ZipFramesRequest handle;

		public int pending;

		internal byte[] _003CLoadLocalZipFramesRoutine_003Eb__0()
		{
			return null;
		}

		internal void _003CLoadLocalZipFramesRoutine_003Eb__1(Texture2D preview)
		{
		}

		internal void _003CLoadLocalZipFramesRoutine_003Eb__2(Texture2D[] frames)
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CCycleMapImagesRoutine_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UILeaderboardEntry _003C_003E4__this;

		private int _003Cindex_003E5__2;

		private WaitForSeconds _003Cwait_003E5__3;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CCycleMapImagesRoutine_003Ed__38(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CLoadLocalZipFramesRoutine_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string replayPath;

		public UILeaderboardEntry _003C_003E4__this;

		public int version;

		private _003C_003Ec__DisplayClass36_0 _003C_003E8__1;

		private Task<byte[]> _003CreadTask_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CLoadLocalZipFramesRoutine_003Ed__36(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CLoadMapImagesRoutine_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UILeaderboardEntry _003C_003E4__this;

		public LeaderboardEntryResponse entry;

		public int version;

		private _003C_003Ec__DisplayClass34_0 _003C_003E8__1;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CLoadMapImagesRoutine_003Ed__34(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CLoadZipFramesRoutine_003Ed__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UILeaderboardEntry _003C_003E4__this;

		public int version;

		public string zipUrl;

		private _003C_003Ec__DisplayClass35_0 _003C_003E8__1;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CLoadZipFramesRoutine_003Ed__35(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[Header("References")]
	public TMP_Text Text_Position;

	public TMP_Text Text_Name;

	public TMP_Text Text_Description;

	public RawImage Image_ProfileIcon;

	public RawImage Image_Map;

	[Header("Image Cycling")]
	public float MapImageCycleInterval;

	public bool CycleMapImages;

	[Header("Cache")]
	public float CacheMaxAgeDays;

	public bool CleanCacheOnAwake;

	[Header("Loading")]
	public int RequestTimeoutSeconds;

	[Header("Global Throttle")]
	public int GlobalMaxConcurrentDownloads;

	public int GlobalMaxTextureCreatesPerFrame;

	public int GlobalMaxActiveImageJobs;

	public float GlobalDownloadStartSpacing;

	public float GlobalTextureCreateSpacing;

	[Header("Runtime")]
	[ReadOnly]
	public LeaderboardEntryResponse Entry;

	private readonly List<Texture2D> mapTextures;

	private readonly List<LeaderboardImageThrottle.TextureRequest> activeTextureRequests;

	private readonly List<LeaderboardImageThrottle.ZipFramesRequest> activeZipRequests;

	private Coroutine mapLoadRoutine;

	private Coroutine mapCycleRoutine;

	private Texture2D profileTexture;

	private int mapLoadVersion;

	private static Task cacheCleanupTask;

	private void Awake()
	{
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
	}

	public static void UnloadUnusedCachedTextures(IEnumerable<string> imageUrls, IEnumerable<string> avatarBase64s = null, IEnumerable<string> zipUrls = null)
	{
	}

	public static void UnloadUnusedCachedTexturesForEntries(IEnumerable<LeaderboardEntryResponse> entries)
	{
	}

	public static void ClearMemoryCache()
	{
	}

	public void Init(int index, LeaderboardEntryResponse entry)
	{
	}

	public void InitLocal(int index, LeaderboardEntryResponse entry)
	{
	}

	private void SetupProfileImage(LeaderboardEntryResponse entry)
	{
	}

	private void SetupMapImages(LeaderboardEntryResponse entry)
	{
	}

	[IteratorStateMachine(typeof(_003CLoadMapImagesRoutine_003Ed__34))]
	private IEnumerator LoadMapImagesRoutine(LeaderboardEntryResponse entry, int version)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CLoadZipFramesRoutine_003Ed__35))]
	private IEnumerator LoadZipFramesRoutine(string zipUrl, int version)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CLoadLocalZipFramesRoutine_003Ed__36))]
	private IEnumerator LoadLocalZipFramesRoutine(string replayPath, int version)
	{
		return null;
	}

	private void QueueTextureRequest(string url, bool priority, int version, Action<Texture2D> onComplete)
	{
	}

	[IteratorStateMachine(typeof(_003CCycleMapImagesRoutine_003Ed__38))]
	private IEnumerator CycleMapImagesRoutine()
	{
		return null;
	}

	private void StopMapLoading()
	{
	}

	private void StopMapCycle()
	{
	}

	private void ClearMapTextures()
	{
	}

	private void ClearProfileTexture()
	{
	}

	private bool IsMapLoadValid(int version)
	{
		return false;
	}

	private static Texture2D LoadProfileTextureImmediate(string base64)
	{
		return null;
	}

	private static void CleanupImageCacheOlderThan(TimeSpan maxAge)
	{
	}
}
