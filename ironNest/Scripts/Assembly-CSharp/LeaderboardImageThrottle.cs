using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class LeaderboardImageThrottle
{
	public sealed class TextureRequest
	{
		public TextureJob Job;

		public TextureRequest ChildTextureRequest;

		public BytesRequest ChildBytesRequest;

		public Action<Texture2D> OnComplete;

		public bool Cancelled { get; set; }

		public bool Completed { get; set; }

		public void Cancel()
		{
		}
	}

	public class ZipFramesRequest
	{
		public BytesRequest BytesRequest;

		public readonly List<TextureRequest> TextureRequests;

		public Action<Texture2D> OnPreview;

		public Action<Texture2D[]> OnComplete;

		public bool Cancelled { get; set; }

		public bool Completed { get; set; }

		public void Cancel()
		{
		}
	}

	public class BytesRequest
	{
		public DownloadJob Job;

		public Action<byte[]> OnComplete;

		public bool Cancelled { get; set; }

		public bool Completed { get; set; }

		public void Cancel()
		{
		}
	}

	public class DownloadJob
	{
		public string Url;

		public string Key;

		public string CacheFile;

		public TimeSpan CacheMaxAge;

		public int TimeoutSeconds;

		public bool Started;

		public bool QueuedPriority;

		public bool QueuedNormal;

		public readonly List<BytesRequest> Requests;

		public bool HasLiveRequests()
		{
			return false;
		}
	}

	public class TextureJob
	{
		public string Key;

		public byte[] Bytes;

		public bool MarkNonReadable;

		public bool Started;

		public bool QueuedPriority;

		public bool QueuedNormal;

		public readonly List<TextureRequest> Requests;

		public bool HasLiveRequests()
		{
			return false;
		}
	}

	private sealed class Runner : MonoBehaviour
	{
		private void Update()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass45_0
	{
		public TextureJob job;

		internal bool _003CProcessTextureJobRoutine_003Eb__0()
		{
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass50_0
	{
		public string zipUrl;

		public List<byte[]> frameBytes;

		public ZipFramesRequest request;

		public Texture2D[] frames;

		public int finalIndex;

		public int pending;
	}

	[CompilerGenerated]
	private sealed class _003CProcessDownloadJobRoutine_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DownloadJob job;

		private UnityWebRequest _003Crequest_003E5__2;

		private bool _003CcountedDownload_003E5__3;

		private Task<byte[]> _003CcacheTask_003E5__4;

		private UnityWebRequestAsyncOperation _003Coperation_003E5__5;

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
		public _003CProcessDownloadJobRoutine_003Ed__38(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CProcessTextureJobRoutine_003Ed__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TextureJob job;

		private _003C_003Ec__DisplayClass45_0 _003C_003E8__1;

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
		public _003CProcessTextureJobRoutine_003Ed__45(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CProcessZipFramesRoutine_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string zipUrl;

		public ZipFramesRequest request;

		public byte[] zipBytes;

		private _003C_003Ec__DisplayClass50_0 _003C_003E8__1;

		private Task<List<byte[]>> _003CextractTask_003E5__2;

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
		public _003CProcessZipFramesRoutine_003Ed__50(int _003C_003E1__state)
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
	private sealed class _003CWaitForTextureCreateSlot_003Ed__48 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Func<bool> isValid;

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
		public _003CWaitForTextureCreateSlot_003Ed__48(int _003C_003E1__state)
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

	public static int MaxConcurrentDownloads;

	public static int MaxTextureCreatesPerFrame;

	public static int MaxActiveJobs;

	public static float DownloadStartSpacing;

	public static float TextureCreateSpacing;

	private static readonly Queue<DownloadJob> priorityDownloadJobs;

	private static readonly Queue<DownloadJob> normalDownloadJobs;

	private static readonly Queue<TextureJob> priorityTextureJobs;

	private static readonly Queue<TextureJob> normalTextureJobs;

	private static readonly Queue<Action> mainThreadActions;

	private static readonly Dictionary<string, DownloadJob> downloadJobs;

	private static readonly Dictionary<string, TextureJob> textureJobs;

	private static readonly Dictionary<string, Texture2D> memoryTextures;

	private static readonly Dictionary<string, int> zipFrameCounts;

	private static Runner runner;

	private static string cacheDir;

	private static int activeDownloads;

	private static int activeDownloadJobs;

	private static int activeTextureJobs;

	private static int lastTextureFrame;

	private static int textureCreatesThisFrame;

	private static float nextDownloadStartTime;

	private static float nextTextureCreateTime;

	private static string CacheDir => null;

	public static void Configure(int maxConcurrentDownloads, int maxTextureCreatesPerFrame, int maxActiveJobs, float downloadStartSpacing, float textureCreateSpacing)
	{
	}

	private static void EnsureRunner()
	{
	}

	public static TextureRequest RequestUrlTexture(string url, bool priority, TimeSpan cacheMaxAge, int timeoutSeconds, Action<Texture2D> onComplete)
	{
		return null;
	}

	public static ZipFramesRequest RequestZipFrameTextures(string zipUrl, TimeSpan cacheMaxAge, int timeoutSeconds, Action<Texture2D> onPreview, Action<Texture2D[]> onComplete)
	{
		return null;
	}

	public static ZipFramesRequest RequestZipFrameTexturesFromBytes(string key, byte[] zipBytes, Action<Texture2D> onPreview, Action<Texture2D[]> onComplete)
	{
		return null;
	}

	private static BytesRequest RequestBytes(string url, bool priority, TimeSpan cacheMaxAge, int timeoutSeconds, Action<byte[]> onComplete)
	{
		return null;
	}

	private static void EnqueueDownloadJob(DownloadJob job, bool priority)
	{
	}

	private static void PumpDownloadJobs()
	{
	}

	private static DownloadJob DequeueDownloadJob()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CProcessDownloadJobRoutine_003Ed__38))]
	private static IEnumerator ProcessDownloadJobRoutine(DownloadJob job)
	{
		return null;
	}

	private static void CompleteDownloadJob(DownloadJob job, byte[] bytes)
	{
	}

	private static void CompleteBytesRequest(BytesRequest request, byte[] bytes)
	{
	}

	private static TextureRequest RequestTextureFromBytes(string key, byte[] bytes, bool priority, bool markNonReadable, Action<Texture2D> onComplete)
	{
		return null;
	}

	private static void EnqueueTextureJob(TextureJob job, bool priority)
	{
	}

	private static void PumpTextureJobs()
	{
	}

	private static TextureJob DequeueTextureJob()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CProcessTextureJobRoutine_003Ed__45))]
	private static IEnumerator ProcessTextureJobRoutine(TextureJob job)
	{
		return null;
	}

	private static void CompleteTextureJob(TextureJob job, Texture2D texture)
	{
	}

	private static void CompleteTextureRequest(TextureRequest request, Texture2D texture)
	{
	}

	[IteratorStateMachine(typeof(_003CWaitForTextureCreateSlot_003Ed__48))]
	private static IEnumerator WaitForTextureCreateSlot(Func<bool> isValid)
	{
		return null;
	}

	private static bool TryTakeTextureCreateSlot()
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CProcessZipFramesRoutine_003Ed__50))]
	private static IEnumerator ProcessZipFramesRoutine(ZipFramesRequest request, string zipUrl, byte[] zipBytes)
	{
		return null;
	}

	public static List<byte[]> ReadFrameZip(byte[] zipBytes)
	{
		return null;
	}

	private static Task<List<byte[]>> ReadFrameZipAsync(byte[] zipBytes)
	{
		return null;
	}

	private static void CompleteZipRequest(ZipFramesRequest request, Texture2D[] frames)
	{
	}

	private static bool TryGetAllMemoryZipFrames(string zipUrl, out Texture2D[] frames)
	{
		frames = null;
		return false;
	}

	public static bool TryGetMemoryTextureForUrl(string url, out Texture2D texture)
	{
		texture = null;
		return false;
	}

	public static bool TryGetMemoryTextureForAvatar(string base64, out Texture2D texture)
	{
		texture = null;
		return false;
	}

	private static Texture2D AddOrGetMemoryTexture(string key, Texture2D texture)
	{
		return null;
	}

	public static Texture2D AddOrGetMemoryTextureForAvatar(string base64, Texture2D texture)
	{
		return null;
	}

	public static void UnloadUnusedCachedTextures(IEnumerable<string> imageUrls, IEnumerable<string> avatarBase64s = null, IEnumerable<string> zipUrls = null)
	{
	}

	public static void ClearMemoryCache()
	{
	}

	public static string GetCacheFile(string url)
	{
		return null;
	}

	private static Task<byte[]> ReadCachedBytesAsync(string file, TimeSpan maxAge)
	{
		return null;
	}

	private static void WriteCachedBytesAsync(string file, byte[] bytes)
	{
	}

	private static void EnqueueMainThreadAction(Action action)
	{
	}

	private static bool TryGetTaskResult<T>(Task<T> task, out T result)
	{
		result = default(T);
		return false;
	}

	public static Texture2D CreateTexture(byte[] bytes, bool markNonReadable)
	{
		return null;
	}

	private static string GetDownloadKey(string url)
	{
		return null;
	}

	public static string GetUrlKey(string url)
	{
		return null;
	}

	public static string GetZipFramePrefix(string zipUrl)
	{
		return null;
	}

	public static string GetZipFrameKey(string zipUrl, int index)
	{
		return null;
	}

	public static string GetAvatarKey(string base64)
	{
		return null;
	}

	public static string NormalizeBase64Payload(string base64)
	{
		return null;
	}

	public static string Hash(string value)
	{
		return null;
	}
}
