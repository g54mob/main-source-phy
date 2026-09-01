using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Networking;

public static class LeaderboardImageThrottle
{
	public sealed class TextureRequest
	{
		private bool _003CCancelled_003Ek__BackingField;

		private bool _003CCompleted_003Ek__BackingField;

		public TextureJob Job;

		public TextureRequest ChildTextureRequest;

		public BytesRequest ChildBytesRequest;

		public Action<Texture2D> OnComplete;

		public bool Cancelled
		{
			get
			{
				return _003CCancelled_003Ek__BackingField;
			}
			set
			{
				_003CCancelled_003Ek__BackingField = value;
			}
		}

		public bool Completed
		{
			get
			{
				return _003CCompleted_003Ek__BackingField;
			}
			set
			{
				_003CCompleted_003Ek__BackingField = value;
			}
		}

		public void Cancel()
		{
			_003CCancelled_003Ek__BackingField = true;
			OnComplete = null;
			if (ChildTextureRequest != null)
			{
				ChildTextureRequest.Cancel();
			}
			BytesRequest childBytesRequest = ChildBytesRequest;
			if (ChildBytesRequest != null)
			{
				childBytesRequest._003CCancelled_003Ek__BackingField = true;
				childBytesRequest.OnComplete = null;
			}
		}
	}

	public class ZipFramesRequest
	{
		private bool _003CCancelled_003Ek__BackingField;

		private bool _003CCompleted_003Ek__BackingField;

		public BytesRequest BytesRequest;

		public readonly List<TextureRequest> TextureRequests;

		public Action<Texture2D> OnPreview;

		public Action<Texture2D[]> OnComplete;

		public bool Cancelled
		{
			get
			{
				return _003CCancelled_003Ek__BackingField;
			}
			set
			{
				_003CCancelled_003Ek__BackingField = value;
			}
		}

		public bool Completed
		{
			get
			{
				return _003CCompleted_003Ek__BackingField;
			}
			set
			{
				_003CCompleted_003Ek__BackingField = value;
			}
		}

		public unsafe void Cancel()
		{
			//IL_01d7: Expected O, but got Ref
			_003CCancelled_003Ek__BackingField = true;
			OnPreview = null;
			OnComplete = null;
			BytesRequest bytesRequest = BytesRequest;
			if (BytesRequest != null)
			{
				bytesRequest._003CCancelled_003Ek__BackingField = true;
				bytesRequest.OnComplete = null;
			}
			if (TextureRequests != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<TextureRequest>.Enumerator enumerator = default(List<TextureRequest>.Enumerator);
				TextureRequest textureRequest = default(TextureRequest);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					textureRequest?.Cancel();
				}
				enumerator.Dispose();
				List<TextureRequest> textureRequests = TextureRequests;
				bool flag = TextureRequests == null;
				List<TextureRequest>.Enumerator enumerator2 = (List<TextureRequest>.Enumerator)(&enumerator);
				if (!flag)
				{
					int version = textureRequests._version + 1;
					textureRequests._version = version;
					((List<TextureRequest>.Enumerator*)null)->Dispose();
					object obj = default(object);
					if (obj == null)
					{
						textureRequests._size = 0;
						return;
					}
					textureRequests._size = 0;
					if (textureRequests._size > 0)
					{
						Array.Clear(textureRequests._items, 0, textureRequests._size);
					}
					return;
				}
			}
			throw new NullReferenceException();
		}

		public ZipFramesRequest()
		{
			List<TextureRequest> textureRequests = new List<TextureRequest>();
			TextureRequests = textureRequests;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	public class BytesRequest
	{
		private bool _003CCancelled_003Ek__BackingField;

		private bool _003CCompleted_003Ek__BackingField;

		public DownloadJob Job;

		public Action<byte[]> OnComplete;

		public bool Cancelled
		{
			get
			{
				return _003CCancelled_003Ek__BackingField;
			}
			set
			{
				_003CCancelled_003Ek__BackingField = value;
			}
		}

		public bool Completed
		{
			get
			{
				return _003CCompleted_003Ek__BackingField;
			}
			set
			{
				_003CCompleted_003Ek__BackingField = value;
			}
		}

		public void Cancel()
		{
			_003CCancelled_003Ek__BackingField = true;
			OnComplete = null;
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

		public unsafe bool HasLiveRequests()
		{
			//IL_0036: Expected O, but got Ref
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<BytesRequest>.Enumerator enumerator = default(List<BytesRequest>.Enumerator);
			object obj = default(object);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = obj == null;
					List<BytesRequest> list = (List<BytesRequest>)(&enumerator);
					if (flag)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ stack_8_v3+10]");
					if ((nint)0 == 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
						return true;
					}
					continue;
				}
				enumerator.Dispose();
				return false;
			}
			throw new NullReferenceException();
		}

		public DownloadJob()
		{
			List<BytesRequest> requests = new List<BytesRequest>();
			Requests = requests;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
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

		public unsafe bool HasLiveRequests()
		{
			//IL_0036: Expected O, but got Ref
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<TextureRequest>.Enumerator enumerator = default(List<TextureRequest>.Enumerator);
			object obj = default(object);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = obj == null;
					List<TextureRequest> list = (List<TextureRequest>)(&enumerator);
					if (flag)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ stack_8_v3+10]");
					if ((nint)0 == 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
						return true;
					}
					continue;
				}
				enumerator.Dispose();
				return false;
			}
			throw new NullReferenceException();
		}

		public TextureJob()
		{
			List<TextureRequest> requests = new List<TextureRequest>();
			Requests = requests;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	private sealed class Runner : MonoBehaviour
	{
		private void Update()
		{
			object obj = default(object);
			while (true)
			{
				Queue<Action> mainThreadActions = LeaderboardImageThrottle.mainThreadActions;
				if (mainThreadActions._size > 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v22 @ stack_18_v2+18] (should have been resolved before IL gen)");
					}
					continue;
				}
				break;
			}
			PumpDownloadJobs();
			PumpTextureJobs();
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<ZipArchiveEntry, string> _003C_003E9__51_0;

		public static Func<char, bool> _003C_003E9__74_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal string _003CReadFrameZip_003Eb__51_0(ZipArchiveEntry x)
		{
			if (x != null)
			{
				return x._storedEntryName;
			}
			return (string)(object)new NullReferenceException();
		}

		internal bool _003CNormalizeBase64Payload_003Eb__74_0(char x)
		{
			bool flag = char.IsWhiteSpace(x);
			return (byte)((flag ? 1u : 0u) ^ 1u) != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass31_0
	{
		public TextureRequest request;

		public Texture2D memoryTexture;

		public string url;

		public bool priority;

		public Action<Texture2D> _003C_003E9__3;

		internal void _003CRequestUrlTexture_003Eb__0()
		{
			TextureRequest textureRequest = request;
			if (request != null && !textureRequest._003CCancelled_003Ek__BackingField && !textureRequest._003CCompleted_003Ek__BackingField)
			{
				Action<Texture2D> onComplete = textureRequest.OnComplete;
				textureRequest.OnComplete = null;
				textureRequest._003CCompleted_003Ek__BackingField = true;
				if (textureRequest.OnComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v110 @ rdi_v2 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
				}
			}
		}

		internal void _003CRequestUrlTexture_003Eb__1()
		{
			TextureRequest textureRequest = request;
			if (request != null && !textureRequest._003CCancelled_003Ek__BackingField && !textureRequest._003CCompleted_003Ek__BackingField)
			{
				Action<Texture2D> onComplete = textureRequest.OnComplete;
				textureRequest.OnComplete = null;
				textureRequest._003CCompleted_003Ek__BackingField = true;
				if (textureRequest.OnComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v82 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
				}
			}
		}

		internal void _003CRequestUrlTexture_003Eb__2(byte[] bytes)
		{
			//IL_01bb: Expected O, but got I
			//IL_01cb: Expected O, but got I
			//IL_01db: Expected O, but got I
			Action<Texture2D> onComplete = default(Action<Texture2D>);
			while (true)
			{
				TextureRequest textureRequest = request;
				if (textureRequest._003CCancelled_003Ek__BackingField)
				{
					break;
				}
				if (bytes != null && bytes.Length != 0)
				{
					string urlKey = GetUrlKey(url);
					if (_003C_003E9__3 == null)
					{
						Action<Texture2D> action = delegate(Texture2D texture)
						{
							bool flag = texture != null;
							bool flag2 = !flag;
							Texture2D texture2D = texture;
							if (!flag2)
							{
								string text = Hash(url);
								string key = "url:" + text;
								Texture2D texture2D2 = AddOrGetMemoryTexture(key, texture);
								texture2D = texture2D2;
							}
							TextureRequest textureRequest2 = request;
							if (request != null && !textureRequest2._003CCancelled_003Ek__BackingField && !textureRequest2._003CCompleted_003Ek__BackingField)
							{
								Action<Texture2D> onComplete3 = textureRequest2.OnComplete;
								textureRequest2.OnComplete = null;
								textureRequest2._003CCompleted_003Ek__BackingField = true;
								if (textureRequest2.OnComplete != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v165 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
								}
							}
						};
						_003C_003E9__3 = action;
					}
					TextureRequest childTextureRequest = RequestTextureFromBytes(urlKey, bytes, priority, markNonReadable: true, onComplete);
					textureRequest.ChildTextureRequest = childTextureRequest;
					break;
				}
				if (textureRequest != null && !textureRequest._003CCancelled_003Ek__BackingField && !textureRequest._003CCompleted_003Ek__BackingField)
				{
					Action<Texture2D> onComplete2 = textureRequest.OnComplete;
					textureRequest._003CCompleted_003Ek__BackingField = true;
					textureRequest.OnComplete = null;
					if (textureRequest.OnComplete != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D>)+18]");
						object obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D>)+28]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D>)+40]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v229 @ rax_v8 (should have been resolved before IL gen)");
						continue;
					}
					break;
				}
				break;
			}
		}

		internal void _003CRequestUrlTexture_003Eb__3(Texture2D texture)
		{
			bool flag = texture != null;
			bool flag2 = !flag;
			Texture2D texture2D = texture;
			if (!flag2)
			{
				string text = Hash(url);
				string key = "url:" + text;
				Texture2D texture2D2 = AddOrGetMemoryTexture(key, texture);
				texture2D = texture2D2;
			}
			TextureRequest textureRequest = request;
			if (request != null && !textureRequest._003CCancelled_003Ek__BackingField && !textureRequest._003CCompleted_003Ek__BackingField)
			{
				Action<Texture2D> onComplete = textureRequest.OnComplete;
				textureRequest.OnComplete = null;
				textureRequest._003CCompleted_003Ek__BackingField = true;
				if (textureRequest.OnComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v165 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
				}
			}
		}
	}

	private sealed class _003C_003Ec__DisplayClass32_0
	{
		public ZipFramesRequest request;

		public Texture2D[] cachedFrames;

		public string zipUrl;

		internal void _003CRequestZipFrameTextures_003Eb__0()
		{
			ZipFramesRequest zipFramesRequest = request;
			if (request != null && !zipFramesRequest._003CCancelled_003Ek__BackingField && !zipFramesRequest._003CCompleted_003Ek__BackingField)
			{
				Action<Texture2D[]> onComplete = zipFramesRequest.OnComplete;
				zipFramesRequest._003CCompleted_003Ek__BackingField = true;
				zipFramesRequest.OnPreview = null;
				zipFramesRequest.OnComplete = null;
				if (zipFramesRequest.OnComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v110 @ rdi_v2 (System.Action`1<UnityEngine.Texture2D[]>)+18] (should have been resolved before IL gen)");
				}
			}
		}

		internal void _003CRequestZipFrameTextures_003Eb__1()
		{
			//IL_00c5: Expected O, but got I4
			ZipFramesRequest zipFramesRequest = request;
			if (zipFramesRequest._003CCancelled_003Ek__BackingField)
			{
				return;
			}
			if (cachedFrames != null)
			{
				Texture2D[] array = cachedFrames;
				if (array.Length != 0)
				{
					ZipFramesRequest zipFramesRequest2 = request;
					Action<Texture2D> onPreview = zipFramesRequest2.OnPreview;
					if (zipFramesRequest2.OnPreview != null)
					{
						Texture2D[] array2 = cachedFrames;
						object obj = array2.Length - 1;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v128 @ rcx_v10 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
					}
				}
			}
			ZipFramesRequest zipFramesRequest3 = request;
			if (request != null && !zipFramesRequest3._003CCancelled_003Ek__BackingField && !zipFramesRequest3._003CCompleted_003Ek__BackingField)
			{
				Action<Texture2D[]> onComplete = zipFramesRequest3.OnComplete;
				zipFramesRequest3._003CCompleted_003Ek__BackingField = true;
				zipFramesRequest3.OnPreview = null;
				zipFramesRequest3.OnComplete = null;
				if (zipFramesRequest3.OnComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v245 @ rbx_v5 (System.Action`1<UnityEngine.Texture2D[]>)+18] (should have been resolved before IL gen)");
				}
			}
		}

		internal void _003CRequestZipFrameTextures_003Eb__2(byte[] bytes)
		{
			//IL_0155: Expected O, but got I
			//IL_0165: Expected O, but got I
			//IL_0175: Expected O, but got I
			while (true)
			{
				ZipFramesRequest zipFramesRequest = request;
				if (!zipFramesRequest._003CCancelled_003Ek__BackingField)
				{
					if (bytes != null && bytes.Length != 0)
					{
						break;
					}
					if (zipFramesRequest == null || zipFramesRequest._003CCancelled_003Ek__BackingField || zipFramesRequest._003CCompleted_003Ek__BackingField)
					{
						return;
					}
					Action<Texture2D[]> onComplete = zipFramesRequest.OnComplete;
					zipFramesRequest._003CCompleted_003Ek__BackingField = true;
					zipFramesRequest.OnPreview = null;
					zipFramesRequest.OnComplete = null;
					if (zipFramesRequest.OnComplete == null)
					{
						return;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D[]>)+18]");
					object obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D[]>)+28]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D[]>)+40]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v214 @ rax_v9 (should have been resolved before IL gen)");
					continue;
				}
				return;
			}
			IEnumerator routine = ProcessZipFramesRoutine(request, zipUrl, bytes);
			Coroutine coroutine = runner.StartCoroutine(routine);
		}
	}

	private sealed class _003C_003Ec__DisplayClass33_0
	{
		public ZipFramesRequest request;

		public Texture2D[] cachedFrames;

		internal void _003CRequestZipFrameTexturesFromBytes_003Eb__0()
		{
			ZipFramesRequest zipFramesRequest = request;
			if (request != null && !zipFramesRequest._003CCancelled_003Ek__BackingField && !zipFramesRequest._003CCompleted_003Ek__BackingField)
			{
				Action<Texture2D[]> onComplete = zipFramesRequest.OnComplete;
				zipFramesRequest._003CCompleted_003Ek__BackingField = true;
				zipFramesRequest.OnPreview = null;
				zipFramesRequest.OnComplete = null;
				if (zipFramesRequest.OnComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v110 @ rdi_v2 (System.Action`1<UnityEngine.Texture2D[]>)+18] (should have been resolved before IL gen)");
				}
			}
		}

		internal void _003CRequestZipFrameTexturesFromBytes_003Eb__1()
		{
			//IL_00c5: Expected O, but got I4
			ZipFramesRequest zipFramesRequest = request;
			if (zipFramesRequest._003CCancelled_003Ek__BackingField)
			{
				return;
			}
			if (cachedFrames != null)
			{
				Texture2D[] array = cachedFrames;
				if (array.Length != 0)
				{
					ZipFramesRequest zipFramesRequest2 = request;
					Action<Texture2D> onPreview = zipFramesRequest2.OnPreview;
					if (zipFramesRequest2.OnPreview != null)
					{
						Texture2D[] array2 = cachedFrames;
						object obj = array2.Length - 1;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v128 @ rcx_v10 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
					}
				}
			}
			ZipFramesRequest zipFramesRequest3 = request;
			if (request != null && !zipFramesRequest3._003CCancelled_003Ek__BackingField && !zipFramesRequest3._003CCompleted_003Ek__BackingField)
			{
				Action<Texture2D[]> onComplete = zipFramesRequest3.OnComplete;
				zipFramesRequest3._003CCompleted_003Ek__BackingField = true;
				zipFramesRequest3.OnPreview = null;
				zipFramesRequest3.OnComplete = null;
				if (zipFramesRequest3.OnComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v245 @ rbx_v5 (System.Action`1<UnityEngine.Texture2D[]>)+18] (should have been resolved before IL gen)");
				}
			}
		}
	}

	private sealed class _003C_003Ec__DisplayClass34_0
	{
		public BytesRequest request;

		internal void _003CRequestBytes_003Eb__0()
		{
			BytesRequest bytesRequest = request;
			if (request != null && !bytesRequest._003CCancelled_003Ek__BackingField && !bytesRequest._003CCompleted_003Ek__BackingField)
			{
				Action<byte[]> onComplete = bytesRequest.OnComplete;
				bytesRequest.OnComplete = null;
				bytesRequest._003CCompleted_003Ek__BackingField = true;
				if (bytesRequest.OnComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v110 @ rdi_v2 (System.Action`1<System.Byte[]>)+18] (should have been resolved before IL gen)");
				}
			}
		}
	}

	private sealed class _003C_003Ec__DisplayClass41_0
	{
		public TextureRequest request;

		public Texture2D memoryTexture;

		internal void _003CRequestTextureFromBytes_003Eb__0()
		{
			TextureRequest textureRequest = request;
			if (request != null && !textureRequest._003CCancelled_003Ek__BackingField && !textureRequest._003CCompleted_003Ek__BackingField)
			{
				Action<Texture2D> onComplete = textureRequest.OnComplete;
				textureRequest.OnComplete = null;
				textureRequest._003CCompleted_003Ek__BackingField = true;
				if (textureRequest.OnComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v110 @ rdi_v2 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
				}
			}
		}

		internal void _003CRequestTextureFromBytes_003Eb__1()
		{
			TextureRequest textureRequest = request;
			if (request != null && !textureRequest._003CCancelled_003Ek__BackingField && !textureRequest._003CCompleted_003Ek__BackingField)
			{
				Action<Texture2D> onComplete = textureRequest.OnComplete;
				textureRequest.OnComplete = null;
				textureRequest._003CCompleted_003Ek__BackingField = true;
				if (textureRequest.OnComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v82 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
				}
			}
		}
	}

	private sealed class _003C_003Ec__DisplayClass45_0
	{
		public TextureJob job;

		internal bool _003CProcessTextureJobRoutine_003Eb__0()
		{
			//IL_0041: Expected I4, but got O
			if (job != null)
			{
				return job.HasLiveRequests();
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass50_0
	{
		public string zipUrl;

		public List<byte[]> frameBytes;

		public ZipFramesRequest request;

		public Texture2D[] frames;

		public int finalIndex;

		public int pending;

		internal void _003CProcessZipFramesRoutine_003Eg__QueueFrame_007C0(int index, bool priority)
		{
			_003C_003Ec__DisplayClass50_1 CS_0024_003C_003E8__locals10 = new _003C_003Ec__DisplayClass50_1();
			CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals1 = this;
			CS_0024_003C_003E8__locals10.index = index;
			string zipFrameKey = GetZipFrameKey(zipUrl, index);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Action<Texture2D> action = delegate(Texture2D texture)
			{
				_003C_003Ec__DisplayClass50_0 obj = CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals1;
				ZipFramesRequest zipFramesRequest2 = obj.request;
				if (!zipFramesRequest2._003CCancelled_003Ek__BackingField)
				{
					Texture2D[] array = obj.frames;
					int index2 = CS_0024_003C_003E8__locals10.index;
					array[index2] = texture;
					_003C_003Ec__DisplayClass50_0 obj2 = CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals1;
					if (CS_0024_003C_003E8__locals10.index == obj2.finalIndex && texture != null)
					{
						_003C_003Ec__DisplayClass50_0 obj3 = CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals1;
						ZipFramesRequest zipFramesRequest3 = obj3.request;
						Action<Texture2D> onPreview = zipFramesRequest3.OnPreview;
						if (zipFramesRequest3.OnPreview != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v289 @ rcx_v21 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
						}
					}
					_003C_003Ec__DisplayClass50_0 obj4 = CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals1;
					_003C_003Ec__DisplayClass50_0 obj5 = CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals1;
					int num = obj4.pending - 1;
					obj5.pending = num;
					_003C_003Ec__DisplayClass50_0 obj6 = CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals1;
					if (obj6.pending <= 0)
					{
						ZipFramesRequest zipFramesRequest4 = obj6.request;
						if (obj6.request != null && !zipFramesRequest4._003CCancelled_003Ek__BackingField && !zipFramesRequest4._003CCompleted_003Ek__BackingField)
						{
							Action<Texture2D[]> onComplete2 = zipFramesRequest4.OnComplete;
							zipFramesRequest4._003CCompleted_003Ek__BackingField = true;
							zipFramesRequest4.OnPreview = null;
							zipFramesRequest4.OnComplete = null;
							if (zipFramesRequest4.OnComplete != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v370 @ rbx_v5 (System.Action`1<UnityEngine.Texture2D[]>)+18] (should have been resolved before IL gen)");
							}
						}
					}
				}
			};
			byte[] bytes = default(byte[]);
			Action<Texture2D> onComplete = default(Action<Texture2D>);
			TextureRequest item = RequestTextureFromBytes(zipFrameKey, bytes, priority, markNonReadable: true, onComplete);
			ZipFramesRequest zipFramesRequest = request;
			zipFramesRequest.TextureRequests.Add(item);
		}
	}

	private sealed class _003C_003Ec__DisplayClass50_1
	{
		public int index;

		public _003C_003Ec__DisplayClass50_0 CS_0024_003C_003E8__locals1;

		internal void _003CProcessZipFramesRoutine_003Eb__1(Texture2D texture)
		{
			_003C_003Ec__DisplayClass50_0 obj = CS_0024_003C_003E8__locals1;
			ZipFramesRequest request = obj.request;
			if (request._003CCancelled_003Ek__BackingField)
			{
				return;
			}
			Texture2D[] frames = obj.frames;
			int num = index;
			frames[num] = texture;
			_003C_003Ec__DisplayClass50_0 obj2 = CS_0024_003C_003E8__locals1;
			if (index == obj2.finalIndex && texture != null)
			{
				_003C_003Ec__DisplayClass50_0 obj3 = CS_0024_003C_003E8__locals1;
				ZipFramesRequest request2 = obj3.request;
				Action<Texture2D> onPreview = request2.OnPreview;
				if (request2.OnPreview != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v289 @ rcx_v21 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
				}
			}
			_003C_003Ec__DisplayClass50_0 obj4 = CS_0024_003C_003E8__locals1;
			_003C_003Ec__DisplayClass50_0 obj5 = CS_0024_003C_003E8__locals1;
			int pending = obj4.pending - 1;
			obj5.pending = pending;
			_003C_003Ec__DisplayClass50_0 obj6 = CS_0024_003C_003E8__locals1;
			if (obj6.pending > 0)
			{
				return;
			}
			ZipFramesRequest request3 = obj6.request;
			if (obj6.request != null && !request3._003CCancelled_003Ek__BackingField && !request3._003CCompleted_003Ek__BackingField)
			{
				Action<Texture2D[]> onComplete = request3.OnComplete;
				request3._003CCompleted_003Ek__BackingField = true;
				request3.OnPreview = null;
				request3.OnComplete = null;
				if (request3.OnComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v370 @ rbx_v5 (System.Action`1<UnityEngine.Texture2D[]>)+18] (should have been resolved before IL gen)");
				}
			}
		}
	}

	private sealed class _003C_003Ec__DisplayClass52_0
	{
		public byte[] zipBytes;

		internal List<byte[]> _003CReadFrameZipAsync_003Eb__0()
		{
			return ReadFrameZip(zipBytes);
		}
	}

	private sealed class _003C_003Ec__DisplayClass64_0
	{
		public string file;

		public TimeSpan maxAge;

		internal unsafe byte[] _003CReadCachedBytesAsync_003Eb__0()
		{
			//IL_0013: Expected I, but got O
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			byte[] result;
			if (File.Exists(file))
			{
				nint num = (nint)typeof(TimeSpan);
				TimeSpan timeSpan = (TimeSpan)(this + 24);
				double totalSeconds = ((TimeSpan*)timeSpan)->TotalSeconds;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm1\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rcx_v4 (Il2CppClass<System.TimeSpan>)+E4]");
				if ((nint)0 > (nint)0)
				{
					DateTime utcNow = DateTime.UtcNow;
					DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(file);
					TimeSpan timeSpan2 = utcNow - lastWriteTimeUtc;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E11880");
					object obj = default(object);
					if (obj != null)
					{
						result = null;
						goto IL_0168;
					}
				}
				byte[] array = File.ReadAllBytes(file);
				if (array == null)
				{
					return (byte[])(object)new NullReferenceException();
				}
				bool flag = array.Length == 0;
				result = null;
				if (!flag)
				{
					result = array;
				}
			}
			else
			{
				result = null;
			}
			goto IL_0168;
			IL_0168:
			return result;
		}
	}

	private sealed class _003C_003Ec__DisplayClass65_0
	{
		public string file;

		public byte[] bytes;

		internal void _003CWriteCachedBytesAsync_003Eb__0()
		{
			string directoryName = Path.GetDirectoryName(file);
			if (!string.IsNullOrEmpty(directoryName))
			{
				DirectoryInfo directoryInfo = Directory.CreateDirectory(directoryName);
			}
			File.WriteAllBytes(file, bytes);
		}
	}

	private sealed class _003CProcessDownloadJobRoutine_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DownloadJob job;

		private UnityWebRequest _003Crequest_003E5__2;

		private bool _003CcountedDownload_003E5__3;

		private Task<byte[]> _003CcacheTask_003E5__4;

		private UnityWebRequestAsyncOperation _003Coperation_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CProcessDownloadJobRoutine_003Ed__38(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
			//IL_002f: Expected O, but got I4
			if (_003C_003E1__state != -3)
			{
				object obj = _003C_003E1__state - 1;
				if ((nint)obj > 2)
				{
					return;
				}
			}
			_003C_003Em__Finally1();
		}

		private unsafe bool MoveNext()
		{
			//IL_0362: Expected I4, but got I8
			//IL_0b59: Expected I4, but got I8
			//IL_0018: Expected O, but got I4
			//IL_0343: Expected I4, but got I8
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			//IL_00a6: Expected I4, but got I8
			//IL_0bc7: Expected I, but got O
			//IL_007f: Expected I4, but got I8
			//IL_0087: Expected O, but got I
			//IL_0889: Expected O, but got I
			//IL_03d1: Expected O, but got I4
			//IL_0a90: Expected I, but got O
			//IL_0ace: Invalid comparison between F4 and I4
			//IL_0ae0: Expected F4, but got I4
			//IL_0119: Expected O, but got I
			//IL_09a9: Expected I, but got O
			//IL_0143: Expected O, but got I
			//IL_0194: Expected O, but got I
			//IL_01d7: Expected O, but got I4
			//IL_0816: Expected O, but got Ref
			//IL_021f: Expected O, but got I4
			_003CProcessDownloadJobRoutine_003Ed__38 obj = default(_003CProcessDownloadJobRoutine_003Ed__38);
			bool flag = obj._003C_003E1__state == 0;
			UnityWebRequestAsyncOperation unityWebRequestAsyncOperation;
			nint num = default(nint);
			Task<byte[]> task;
			if (!flag)
			{
				object obj2 = obj._003C_003E1__state - 1;
				if (!flag)
				{
					object obj3 = obj2 - 1;
					if (!flag)
					{
						if ((nint)obj3 != 1)
						{
							return false;
						}
						obj._003C_003E1__state = -3;
						string text = (string)num;
						unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)(object)obj;
						goto IL_0235;
					}
					obj._003C_003E1__state = -3;
					goto IL_0bb9;
				}
				obj._003C_003E1__state = -3;
				task = (Task<byte[]>)(object)obj;
			}
			else
			{
				obj._003C_003E1__state = -1;
				int activeDownloadJobs = LeaderboardImageThrottle.activeDownloadJobs + 1;
				LeaderboardImageThrottle.activeDownloadJobs = activeDownloadJobs;
				obj._003Crequest_003E5__2 = null;
				obj._003CcountedDownload_003E5__3 = false;
				obj._003C_003E1__state = -3;
				DownloadJob downloadJob = obj.job;
				bool flag2 = obj.job == null;
				task = null;
				if (flag2)
				{
					throw new NullReferenceException();
				}
				_003C_003Ec__DisplayClass64_0 CS_0024_003C_003E8__locals11 = new _003C_003Ec__DisplayClass64_0();
				bool flag3 = CS_0024_003C_003E8__locals11 == null;
				task = null;
				if (flag3)
				{
					throw new NullReferenceException();
				}
				CS_0024_003C_003E8__locals11.file = downloadJob.CacheFile;
				CS_0024_003C_003E8__locals11.maxAge = downloadJob.CacheMaxAge;
				Func<byte[]> function = delegate
				{
					//IL_0013: Expected I, but got O
					//IL_001e: Unknown result type (might be due to invalid IL or missing references)
					//IL_0023: Expected O, but got Unknown
					byte[] result3;
					if (File.Exists(CS_0024_003C_003E8__locals11.file))
					{
						nint num5 = (nint)typeof(TimeSpan);
						TimeSpan timeSpan = (TimeSpan)(CS_0024_003C_003E8__locals11 + 24);
						double totalSeconds = ((TimeSpan*)timeSpan)->TotalSeconds;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm1\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rcx_v4 (Il2CppClass<System.TimeSpan>)+E4]");
						if ((nint)0 > (nint)0)
						{
							DateTime utcNow = DateTime.UtcNow;
							DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(CS_0024_003C_003E8__locals11.file);
							TimeSpan timeSpan2 = utcNow - lastWriteTimeUtc;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E11880");
							object obj5 = default(object);
							if (obj5 != null)
							{
								result3 = null;
								goto IL_0168;
							}
						}
						byte[] array = File.ReadAllBytes(CS_0024_003C_003E8__locals11.file);
						if (array == null)
						{
							return (byte[])(object)new NullReferenceException();
						}
						bool flag28 = array.Length == 0;
						result3 = null;
						if (!flag28)
						{
							result3 = array;
						}
					}
					else
					{
						result3 = null;
					}
					goto IL_0168;
					IL_0168:
					return result3;
				};
				Task<byte[]> task2 = (obj._003CcacheTask_003E5__4 = Task.Run(function));
				object obj4 = 0;
				num = 0;
				task = task2;
			}
			if (obj._003CcacheTask_003E5__4 != null)
			{
				if (!obj._003CcacheTask_003E5__4.IsCompleted)
				{
					if (obj.job.HasLiveRequests())
					{
						obj._003C_003E2__current = null;
						obj._003C_003E1__state = 1;
						return true;
					}
					goto IL_08e8;
				}
				bool flag4 = obj.job == null;
				task = null;
				if (!flag4)
				{
					if (obj.job.HasLiveRequests())
					{
						bool flag5 = TryGetTaskResult(obj._003CcacheTask_003E5__4, out var result);
						bool flag6 = !flag5;
						num = 0;
						if (!flag6)
						{
							bool flag7 = result == null;
							num = 0;
							if (!flag7)
							{
								CompleteDownloadJob(obj.job, result);
								goto IL_08e8;
							}
						}
					}
					goto IL_0bb9;
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
			IL_0235:
			bool flag8 = obj._003Coperation_003E5__5 == null;
			string text2 = (string)(object)unityWebRequestAsyncOperation;
			if (!flag8)
			{
				if (!obj._003Coperation_003E5__5.isDone)
				{
					bool flag9 = obj.job == null;
					Task<byte[]> task3 = null;
					if (flag9)
					{
						throw new NullReferenceException();
					}
					if (obj.job.HasLiveRequests())
					{
						obj._003C_003E2__current = null;
						obj._003C_003E1__state = 3;
						return true;
					}
					bool flag10 = obj._003Crequest_003E5__2 == null;
					task3 = null;
					if (flag10)
					{
						throw new NullReferenceException();
					}
					obj._003Crequest_003E5__2.Abort();
				}
				else
				{
					bool flag11 = obj.job == null;
					text2 = null;
					if (flag11)
					{
						throw new NullReferenceException();
					}
					if (obj.job.HasLiveRequests())
					{
						bool flag12 = obj._003Crequest_003E5__2 == null;
						text2 = null;
						if (flag12)
						{
							throw new NullReferenceException();
						}
						UnityWebRequest.Result result2 = obj._003Crequest_003E5__2.result;
						if (result2 == UnityWebRequest.Result.Success)
						{
							bool flag13 = obj._003Crequest_003E5__2 == null;
							Task<byte[]> task3 = null;
							if (flag13)
							{
								text2 = (string)(object)task3;
								throw new NullReferenceException();
							}
							DownloadHandler downloadHandler = obj._003Crequest_003E5__2.downloadHandler;
							bool flag14 = downloadHandler == null;
							task3 = null;
							if (flag14)
							{
								throw new NullReferenceException();
							}
							byte[] data = downloadHandler.data;
							if (data != null && data.Length != 0)
							{
								DownloadJob downloadJob2 = obj.job;
								bool flag15 = obj.job == null;
								task3 = null;
								if (flag15)
								{
									throw new NullReferenceException();
								}
								_003C_003Ec__DisplayClass65_0 CS_0024_003C_003E8__locals14 = new _003C_003Ec__DisplayClass65_0();
								bool flag16 = CS_0024_003C_003E8__locals14 == null;
								task3 = null;
								if (!flag16)
								{
									CS_0024_003C_003E8__locals14.file = downloadJob2.CacheFile;
									CS_0024_003C_003E8__locals14.bytes = data;
									Action action = delegate
									{
										string directoryName = Path.GetDirectoryName(CS_0024_003C_003E8__locals14.file);
										if (!string.IsNullOrEmpty(directoryName))
										{
											DirectoryInfo directoryInfo = Directory.CreateDirectory(directoryName);
										}
										File.WriteAllBytes(CS_0024_003C_003E8__locals14.file, CS_0024_003C_003E8__locals14.bytes);
									};
									Task task4 = Task.Run(action);
									CompleteDownloadJob(obj.job, data);
									obj._003CcacheTask_003E5__4 = null;
									obj._003Coperation_003E5__5 = null;
									obj._003C_003Em__Finally1();
									return false;
								}
								throw new NullReferenceException();
							}
						}
						else
						{
							DownloadJob downloadJob3 = obj.job;
							bool flag17 = obj.job == null;
							text2 = null;
							if (flag17)
							{
								throw new NullReferenceException();
							}
							bool flag18 = obj._003Crequest_003E5__2 == null;
							text2 = null;
							if (flag18)
							{
								throw new NullReferenceException();
							}
							long responseCode = obj._003Crequest_003E5__2.responseCode;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							bool flag19 = obj._003Crequest_003E5__2 == null;
							long num2 = default(long);
							text2 = (string)(&num2);
							if (flag19)
							{
								throw new NullReferenceException();
							}
							string error = obj._003Crequest_003E5__2.error;
							object arg = default(object);
							string message = $"Could not load URL: {downloadJob3.Url}\n{arg} {error}";
							Debug.LogError(message);
						}
						CompleteDownloadJob(obj.job, null);
					}
				}
				goto IL_08e8;
			}
			throw new NullReferenceException();
			IL_08e8:
			obj._003C_003Em__Finally1();
			return false;
			IL_0bb9:
			nint num3 = (nint)typeof(LeaderboardImageThrottle);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v538 @ rax_v5 (Il2CppClass<LeaderboardImageThrottle>)+B8]");
			nint num4 = 0;
			if (LeaderboardImageThrottle.activeDownloads < MaxConcurrentDownloads)
			{
				float unscaledTime = Time.unscaledTime;
				float nextDownloadStartTime = LeaderboardImageThrottle.nextDownloadStartTime;
				bool flag20 = LeaderboardImageThrottle.nextDownloadStartTime > unscaledTime;
				num4 = (nint)typeof(LeaderboardImageThrottle);
				if (!flag20)
				{
					int activeDownloads = LeaderboardImageThrottle.activeDownloads + 1;
					LeaderboardImageThrottle.activeDownloads = activeDownloads;
					obj._003CcountedDownload_003E5__3 = true;
					bool flag21 = !(DownloadStartSpacing > 0f);
					nextDownloadStartTime = 0f;
					if (!flag21)
					{
						nextDownloadStartTime = Time.unscaledTime;
						float nextDownloadStartTime2 = nextDownloadStartTime + DownloadStartSpacing;
						LeaderboardImageThrottle.nextDownloadStartTime = nextDownloadStartTime2;
					}
					DownloadJob downloadJob4 = obj.job;
					bool flag22 = obj.job == null;
					text2 = (string)(object)typeof(LeaderboardImageThrottle);
					if (!flag22)
					{
						UnityWebRequest unityWebRequest = UnityWebRequest.Get(downloadJob4.Url);
						obj._003Crequest_003E5__2 = unityWebRequest;
						text2 = (string)(object)obj.job;
						bool flag23 = obj.job == null;
						string text = (string)num;
						if (!flag23)
						{
							bool flag24 = obj._003Crequest_003E5__2 == null;
							text = (string)num;
							if (!flag24)
							{
								UnityWebRequest unityWebRequest2 = obj._003Crequest_003E5__2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v809 @ rdx_v26 (System.String)+30]");
								unityWebRequest2.timeout = 0;
								bool flag25 = obj._003Crequest_003E5__2 == null;
								text = null;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v809 @ rdx_v26 (System.String)+30]");
								text2 = (string)0;
								if (!flag25)
								{
									obj._003Crequest_003E5__2.SetRequestHeader("User-Agent", "IronNest-Unity");
									bool flag26 = obj._003Crequest_003E5__2 == null;
									object obj4 = 0;
									text = "IronNest-Unity";
									text2 = "User-Agent";
									if (flag26)
									{
										throw new NullReferenceException();
									}
									UnityWebRequestAsyncOperation unityWebRequestAsyncOperation2 = (obj._003Coperation_003E5__5 = obj._003Crequest_003E5__2.SendWebRequest());
									obj4 = 0;
									text = "IronNest-Unity";
									unityWebRequestAsyncOperation = unityWebRequestAsyncOperation2;
									goto IL_0235;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						num = (nint)text;
						throw new NullReferenceException();
					}
					task = (Task<byte[]>)(object)text2;
					throw new NullReferenceException();
				}
			}
			bool flag27 = obj.job == null;
			task = (Task<byte[]>)num4;
			if (!flag27)
			{
				if (obj.job.HasLiveRequests())
				{
					obj._003C_003E2__current = null;
					obj._003C_003E1__state = 2;
					return true;
				}
				goto IL_08e8;
			}
			throw new NullReferenceException();
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			//IL_00ba: Expected I4, but got I8
			//IL_00d6: Expected I, but got O
			//IL_0042: Expected I, but got O
			bool flag = _003Crequest_003E5__2 == null;
			_003C_003E1__state = -1;
			if (!flag)
			{
				_003Crequest_003E5__2.Dispose();
			}
			if (_003CcountedDownload_003E5__3)
			{
				nint num = (nint)typeof(LeaderboardImageThrottle);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rdx_v9 (Il2CppClass<LeaderboardImageThrottle>)+E4]");
				bool flag2 = (nint)0 < (nint)0;
				int num2 = LeaderboardImageThrottle.activeDownloads - 1;
				int activeDownloads = 0;
				if (!flag2)
				{
					activeDownloads = num2;
				}
				LeaderboardImageThrottle.activeDownloads = activeDownloads;
			}
			nint num3 = (nint)typeof(LeaderboardImageThrottle);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ rdx_v3 (Il2CppClass<LeaderboardImageThrottle>)+E4]");
			bool flag3 = (nint)0 < (nint)0;
			int num4 = LeaderboardImageThrottle.activeDownloadJobs - 1;
			int activeDownloadJobs = 0;
			if (!flag3)
			{
				activeDownloadJobs = num4;
			}
			LeaderboardImageThrottle.activeDownloadJobs = activeDownloadJobs;
			DownloadJob downloadJob = job;
			bool flag4 = downloadJobs.Remove(downloadJob.Key);
			PumpDownloadJobs();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private sealed class _003CProcessTextureJobRoutine_003Ed__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TextureJob job;

		private _003C_003Ec__DisplayClass45_0 _003C_003E8__1;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CProcessTextureJobRoutine_003Ed__45(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
			if (_003C_003E1__state == -3 || _003C_003E1__state == 1)
			{
				_003C_003Em__Finally1();
			}
		}

		private unsafe bool MoveNext()
		{
			//IL_0017: Expected I4, but got I8
			//IL_0243: Expected I4, but got I8
			//IL_004d: Expected I, but got O
			//IL_04b7: Expected O, but got I
			//IL_008f: Expected I, but got O
			//IL_0582: Expected I4, but got I8
			//IL_02b5: Expected O, but got I
			//IL_02dc: Expected I, but got O
			//IL_02fa: Expected O, but got I
			//IL_0319: Expected I, but got O
			//IL_0349: Expected O, but got I
			//IL_0370: Expected I, but got O
			//IL_03b1: Expected I, but got O
			//IL_04e0: Expected I, but got O
			//IL_05fe: Expected I, but got O
			//IL_03f2: Expected I, but got O
			//IL_0167: Expected I, but got O
			//IL_05b6: Expected I, but got O
			//IL_042d: Expected I, but got O
			_003CProcessTextureJobRoutine_003Ed__45 obj = default(_003CProcessTextureJobRoutine_003Ed__45);
			if (obj._003C_003E1__state == 0)
			{
				obj._003C_003E1__state = -1;
				_003C_003Ec__DisplayClass45_0 obj2 = new _003C_003Ec__DisplayClass45_0();
				obj._003C_003E8__1 = obj2;
				_003C_003Ec__DisplayClass45_0 obj3 = obj._003C_003E8__1;
				nint num = (nint)obj.job;
				if (obj._003C_003E8__1 == null)
				{
					UnityEngine.Object obj4 = (UnityEngine.Object)num;
					throw new NullReferenceException();
				}
				obj3.job = obj.job;
				num = (nint)typeof(LeaderboardImageThrottle);
				int activeTextureJobs = LeaderboardImageThrottle.activeTextureJobs + 1;
				LeaderboardImageThrottle.activeTextureJobs = activeTextureJobs;
				obj._003C_003E1__state = -3;
				_003C_003Ec__DisplayClass45_0 obj5 = obj._003C_003E8__1;
				if (obj._003C_003E8__1 == null)
				{
					throw new NullReferenceException();
				}
				TextureJob textureJob = obj5.job;
				if (obj5.job == null)
				{
					throw new NullReferenceException();
				}
				if (memoryTextures == null)
				{
					throw new NullReferenceException();
				}
				nint num2;
				if (!memoryTextures.TryGetValue(textureJob.Key, out var value) || !(value != null))
				{
					Func<bool> isValid = delegate
					{
						//IL_0041: Expected I4, but got O
						if (obj._003C_003E8__1.job == null)
						{
							NullReferenceException ex = new NullReferenceException();
							return (byte)(int)ex != 0;
						}
						return obj._003C_003E8__1.job.HasLiveRequests();
					};
					_003CWaitForTextureCreateSlot_003Ed__48 obj6 = new _003CWaitForTextureCreateSlot_003Ed__48(0);
					obj6._003C_003E1__state = 0;
					bool flag = obj6 == null;
					num2 = 0;
					num = unchecked((nint)null);
					if (!flag)
					{
						obj6.isValid = isValid;
						obj._003C_003E2__current = obj6;
						obj._003C_003E1__state = 1;
						return true;
					}
					throw new NullReferenceException();
				}
				_003C_003Ec__DisplayClass45_0 obj7 = obj._003C_003E8__1;
				bool flag2 = obj._003C_003E8__1 == null;
				num2 = unchecked((nint)null);
				if (flag2)
				{
					num = unchecked((nint)null);
					throw new NullReferenceException();
				}
				CompleteTextureJob(obj7.job, value);
			}
			else
			{
				if (obj._003C_003E1__state != 1)
				{
					return false;
				}
				obj._003C_003E1__state = -3;
				_003C_003Ec__DisplayClass45_0 obj8 = obj._003C_003E8__1;
				bool flag3 = obj._003C_003E8__1 == null;
				nint num2 = default(nint);
				ref Texture2D reference = ref *(Texture2D*)num2;
				if (flag3)
				{
					throw new NullReferenceException();
				}
				if (obj8.job.HasLiveRequests())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ stack_8_v2 (LeaderboardImageThrottle+<ProcessTextureJobRoutine>d__45)+28]");
					object obj9 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ stack_8_v2 (LeaderboardImageThrottle+<ProcessTextureJobRoutine>d__45)+28]");
					bool flag4 = (nint)0 == 0;
					reference = ref *(Texture2D*)num2;
					nint num3 = (nint)obj;
					if (!flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v405 @ rax_v35+10]");
						object obj10 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v405 @ rax_v35+10]");
						bool flag5 = (nint)0 == 0;
						num3 = (nint)obj;
						if (!flag5)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v524 @ rcx_v20+18]");
							nint num4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v524 @ rcx_v20+20]");
							Texture2D texture2D = CreateTexture((byte[])num4, markNonReadable: false);
							bool flag6 = texture2D != null;
							bool flag7 = !flag6;
							num2 = unchecked((nint)null);
							Texture2D texture2D2 = null;
							_003C_003Ec__DisplayClass45_0 texture = (_003C_003Ec__DisplayClass45_0)(object)texture2D;
							UnityEngine.Object obj4;
							if (!flag7)
							{
								_003C_003Ec__DisplayClass45_0 obj11 = obj._003C_003E8__1;
								bool flag8 = obj._003C_003E8__1 == null;
								num2 = unchecked((nint)null);
								obj4 = null;
								texture = (_003C_003Ec__DisplayClass45_0)(object)texture2D;
								if (flag8)
								{
									num3 = (nint)obj4;
									throw new NullReferenceException();
								}
								TextureJob textureJob2 = obj11.job;
								bool flag9 = obj11.job == null;
								num2 = unchecked((nint)null);
								obj4 = null;
								texture = (_003C_003Ec__DisplayClass45_0)(object)texture2D;
								if (flag9)
								{
									throw new NullReferenceException();
								}
								Texture2D texture2D3 = AddOrGetMemoryTexture(textureJob2.Key, texture2D);
								num2 = unchecked((nint)null);
								texture2D2 = texture2D;
								texture = (_003C_003Ec__DisplayClass45_0)(object)texture2D3;
							}
							_003C_003Ec__DisplayClass45_0 obj12 = obj._003C_003E8__1;
							bool flag10 = obj._003C_003E8__1 == null;
							obj4 = texture2D2;
							if (!flag10)
							{
								CompleteTextureJob(obj12.job, (Texture2D)(object)texture);
								obj._003C_003Em__Finally1();
								return false;
							}
							throw new NullReferenceException();
						}
						reference = ref *(Texture2D*)num2;
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
			}
			obj._003C_003Em__Finally1();
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			//IL_0055: Expected I4, but got I8
			//IL_0063: Expected I, but got O
			_003C_003E1__state = -1;
			nint num = (nint)typeof(LeaderboardImageThrottle);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rdx_v1 (Il2CppClass<LeaderboardImageThrottle>)+E4]");
			bool flag = (nint)0 < (nint)0;
			int num2 = LeaderboardImageThrottle.activeTextureJobs - 1;
			int activeTextureJobs = 0;
			if (!flag)
			{
				activeTextureJobs = num2;
			}
			LeaderboardImageThrottle.activeTextureJobs = activeTextureJobs;
			_003C_003Ec__DisplayClass45_0 obj = _003C_003E8__1;
			TextureJob textureJob = obj.job;
			bool flag2 = textureJobs.Remove(textureJob.Key);
			PumpTextureJobs();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private sealed class _003CProcessZipFramesRoutine_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string zipUrl;

		public ZipFramesRequest request;

		public byte[] zipBytes;

		private _003C_003Ec__DisplayClass50_0 _003C_003E8__1;

		private Task<List<byte[]>> _003CextractTask_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CProcessZipFramesRoutine_003Ed__50(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0126: Expected I4, but got I8
			//IL_0768: Expected I4, but got O
			//IL_0253: Unknown result type (might be due to invalid IL or missing references)
			//IL_0258: Expected Ref, but got Unknown
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003C_003Ec__DisplayClass50_0 obj = new _003C_003Ec__DisplayClass50_0();
				_003C_003E8__1 = obj;
				_003C_003Ec__DisplayClass50_0 obj2 = _003C_003E8__1;
				if (_003C_003E8__1 != null)
				{
					obj2.zipUrl = zipUrl;
					_003C_003Ec__DisplayClass50_0 obj3 = _003C_003E8__1;
					if (_003C_003E8__1 != null)
					{
						obj3.request = request;
						_003C_003Ec__DisplayClass52_0 CS_0024_003C_003E8__locals3 = new _003C_003Ec__DisplayClass52_0();
						if (CS_0024_003C_003E8__locals3 != null)
						{
							CS_0024_003C_003E8__locals3.zipBytes = zipBytes;
							Func<List<byte[]>> function = () => ReadFrameZip(CS_0024_003C_003E8__locals3.zipBytes);
							Task<List<byte[]>> task = Task.Run(function);
							_003CextractTask_003E5__2 = task;
							goto IL_07b1;
						}
					}
				}
				goto IL_075a;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				goto IL_07b1;
			}
			goto IL_07d0;
			IL_07b1:
			if (_003CextractTask_003E5__2 != null)
			{
				bool isCompleted = _003CextractTask_003E5__2.IsCompleted;
				_003C_003Ec__DisplayClass50_0 obj4 = _003C_003E8__1;
				if (_003C_003E8__1 != null)
				{
					ZipFramesRequest zipFramesRequest = obj4.request;
					if (!isCompleted)
					{
						if (obj4.request != null)
						{
							if (!zipFramesRequest._003CCancelled_003Ek__BackingField)
							{
								_003C_003E2__current = null;
								_003C_003E1__state = 1;
								return true;
							}
							goto IL_07d0;
						}
					}
					else if (obj4.request != null)
					{
						if (zipFramesRequest._003CCancelled_003Ek__BackingField)
						{
							goto IL_07d0;
						}
						if (_003C_003E8__1 != null)
						{
							ref List<byte[]> result = ref *(List<byte[]>*)(_003C_003E8__1 + 24);
							if (TryGetTaskResult(_003CextractTask_003E5__2, out result))
							{
								_003C_003Ec__DisplayClass50_0 obj5 = _003C_003E8__1;
								if (_003C_003E8__1 == null)
								{
									goto IL_075a;
								}
								if (obj5.frameBytes != null)
								{
									List<byte[]> frameBytes = obj5.frameBytes;
									if (frameBytes._size != 0)
									{
										Texture2D[] frames = new Texture2D[frameBytes._size];
										obj5.frames = frames;
										_003C_003Ec__DisplayClass50_0 obj6 = _003C_003E8__1;
										if (_003C_003E8__1 != null)
										{
											List<byte[]> frameBytes2 = obj6.frameBytes;
											if (obj6.frameBytes != null)
											{
												_003C_003Ec__DisplayClass50_0 obj7 = _003C_003E8__1;
												obj7.pending = frameBytes2._size;
												_003C_003Ec__DisplayClass50_0 obj8 = _003C_003E8__1;
												if (_003C_003E8__1 != null)
												{
													List<byte[]> frameBytes3 = obj8.frameBytes;
													if (obj8.frameBytes != null)
													{
														_003C_003Ec__DisplayClass50_0 obj9 = _003C_003E8__1;
														int finalIndex = frameBytes3._size - 1;
														obj9.finalIndex = finalIndex;
														_003C_003Ec__DisplayClass50_0 obj10 = _003C_003E8__1;
														if (_003C_003E8__1 != null)
														{
															string zipFramePrefix = GetZipFramePrefix(obj10.zipUrl);
															_003C_003Ec__DisplayClass50_0 obj11 = _003C_003E8__1;
															if (_003C_003E8__1 != null && obj11.frameBytes != null && zipFrameCounts != null)
															{
																object obj12 = default(object);
																zipFrameCounts.set_Item(zipFramePrefix, (int)(&obj12));
																_003C_003Ec__DisplayClass50_0 obj13 = _003C_003E8__1;
																if (_003C_003E8__1 != null)
																{
																	_003C_003E8__1._003CProcessZipFramesRoutine_003Eg__QueueFrame_007C0(obj13.finalIndex, priority: true);
																	_003C_003Ec__DisplayClass50_0 obj14 = _003C_003E8__1;
																	bool flag = _003C_003E8__1 == null;
																	int num = 0;
																	int num2 = 0;
																	if (!flag)
																	{
																		while (true)
																		{
																			List<byte[]> frameBytes4 = obj14.frameBytes;
																			if (obj14.frameBytes == null)
																			{
																				break;
																			}
																			if (num2 < frameBytes4._size)
																			{
																				_003C_003Ec__DisplayClass50_0 obj15 = _003C_003E8__1;
																				if (_003C_003E8__1 == null)
																				{
																					break;
																				}
																				if (num != obj15.finalIndex)
																				{
																					_003C_003E8__1._003CProcessZipFramesRoutine_003Eg__QueueFrame_007C0(num, priority: false);
																				}
																				obj14 = _003C_003E8__1;
																				num++;
																				if (_003C_003E8__1 == null)
																				{
																					break;
																				}
																				num2 = num;
																				continue;
																			}
																			goto IL_07d0;
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
										goto IL_075a;
									}
								}
							}
							_003C_003Ec__DisplayClass50_0 obj16 = _003C_003E8__1;
							if (_003C_003E8__1 != null)
							{
								ZipFramesRequest zipFramesRequest2 = obj16.request;
								if (obj16.request != null && !zipFramesRequest2._003CCancelled_003Ek__BackingField && !zipFramesRequest2._003CCompleted_003Ek__BackingField)
								{
									Action<Texture2D[]> onComplete = zipFramesRequest2.OnComplete;
									zipFramesRequest2._003CCompleted_003Ek__BackingField = true;
									zipFramesRequest2.OnPreview = null;
									zipFramesRequest2.OnComplete = null;
									if (zipFramesRequest2.OnComplete != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v85 @ rsi_v5 (System.Action`1<UnityEngine.Texture2D[]>)+18] (should have been resolved before IL gen)");
									}
								}
								goto IL_07d0;
							}
						}
					}
				}
			}
			goto IL_075a;
			IL_07d0:
			return false;
			IL_075a:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private sealed class _003CWaitForTextureCreateSlot_003Ed__48 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Func<bool> isValid;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CWaitForTextureCreateSlot_003Ed__48(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0036: Expected I4, but got I8
			//IL_0122: Invalid comparison between F4 and I4
			//IL_00e5: Expected I4, but got O
			//IL_01cc: Invalid comparison between F4 and I4
			if (_003C_003E1__state == 0 || _003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				float num = TextureCreateSpacing;
				float nextTextureCreateTime;
				float num2;
				if (TextureCreateSpacing > 0f)
				{
					num = Time.unscaledTime;
					bool flag = LeaderboardImageThrottle.nextTextureCreateTime > num;
					nextTextureCreateTime = LeaderboardImageThrottle.nextTextureCreateTime;
					num2 = num;
					if (flag)
					{
						goto IL_006f;
					}
				}
				int frameCount = Time.frameCount;
				if (lastTextureFrame != frameCount)
				{
					int frameCount2 = Time.frameCount;
					lastTextureFrame = frameCount2;
					LeaderboardImageThrottle.textureCreatesThisFrame = 0;
				}
				bool flag2 = LeaderboardImageThrottle.textureCreatesThisFrame >= MaxTextureCreatesPerFrame;
				nextTextureCreateTime = LeaderboardImageThrottle.nextTextureCreateTime;
				num2 = num;
				if (flag2)
				{
					goto IL_006f;
				}
				int textureCreatesThisFrame = LeaderboardImageThrottle.textureCreatesThisFrame + 1;
				LeaderboardImageThrottle.textureCreatesThisFrame = textureCreatesThisFrame;
				if (TextureCreateSpacing > 0f)
				{
					float unscaledTime = Time.unscaledTime;
					float nextTextureCreateTime2 = unscaledTime + TextureCreateSpacing;
					LeaderboardImageThrottle.nextTextureCreateTime = nextTextureCreateTime2;
				}
			}
			goto IL_0104;
			IL_0104:
			return false;
			IL_006f:
			Func<bool> func = isValid;
			if (isValid != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v200 @ rcx_v8 (System.Func`1<System.Boolean>)+18] (should have been resolved before IL gen)");
				object obj = default(object);
				if (obj != null)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0104;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
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

	private static string CacheDir
	{
		get
		{
			//IL_003d: Expected I, but got O
			do
			{
				if (string.IsNullOrEmpty(cacheDir))
				{
					string persistentDataPath = Application.persistentDataPath;
					string text = Path.Combine(persistentDataPath, "ImageCache");
					cacheDir = text;
					break;
				}
				nint num = (nint)typeof(LeaderboardImageThrottle);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ rax_v9 (Il2CppClass<LeaderboardImageThrottle>)+E4]");
			}
			while ((nint)0 == 0);
			return cacheDir;
		}
	}

	public static void Configure(int maxConcurrentDownloads, int maxTextureCreatesPerFrame, int maxActiveJobs, float downloadStartSpacing, float textureCreateSpacing)
	{
		//IL_00ad: Invalid comparison between I4 and F4
		//IL_00bf: Expected F4, but got I4
		//IL_0122: Invalid comparison between I4 and F4
		//IL_0134: Expected F4, but got I4
		bool flag = maxConcurrentDownloads < 1;
		int maxConcurrentDownloads2 = 1;
		if (!flag)
		{
			maxConcurrentDownloads2 = maxConcurrentDownloads;
		}
		MaxConcurrentDownloads = maxConcurrentDownloads2;
		bool flag2 = maxTextureCreatesPerFrame < 1;
		int maxTextureCreatesPerFrame2 = 1;
		if (!flag2)
		{
			maxTextureCreatesPerFrame2 = maxTextureCreatesPerFrame;
		}
		MaxTextureCreatesPerFrame = maxTextureCreatesPerFrame2;
		bool flag3 = maxActiveJobs < 1;
		int maxActiveJobs2 = 1;
		if (!flag3)
		{
			maxActiveJobs2 = maxActiveJobs;
		}
		MaxActiveJobs = maxActiveJobs2;
		bool flag4 = !(0f < downloadStartSpacing);
		float downloadStartSpacing2 = 0f;
		if (!flag4)
		{
			downloadStartSpacing2 = downloadStartSpacing;
		}
		DownloadStartSpacing = downloadStartSpacing2;
		float num = default(float);
		bool flag5 = !(0f < num);
		float textureCreateSpacing2 = 0f;
		if (!flag5)
		{
			textureCreateSpacing2 = num;
		}
		TextureCreateSpacing = textureCreateSpacing2;
		EnsureRunner();
	}

	private static void EnsureRunner()
	{
		if (LeaderboardImageThrottle.runner == null)
		{
			GameObject gameObject = new GameObject("LeaderboardImageThrottle");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			Runner runner = gameObject.AddComponent<Runner>();
			LeaderboardImageThrottle.runner = runner;
		}
	}

	public unsafe static TextureRequest RequestUrlTexture(string url, bool priority, TimeSpan cacheMaxAge, int timeoutSeconds, Action<Texture2D> onComplete)
	{
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected Ref, but got Unknown
		_003C_003Ec__DisplayClass31_0 CS_0024_003C_003E8__locals24 = new _003C_003Ec__DisplayClass31_0();
		Action action;
		if (CS_0024_003C_003E8__locals24 != null)
		{
			CS_0024_003C_003E8__locals24.url = url;
			CS_0024_003C_003E8__locals24.priority = priority;
			EnsureRunner();
			TextureRequest textureRequest = new TextureRequest();
			if (textureRequest != null)
			{
				Action<Texture2D> onComplete2 = default(Action<Texture2D>);
				textureRequest.OnComplete = onComplete2;
				CS_0024_003C_003E8__locals24.request = textureRequest;
				if (string.IsNullOrWhiteSpace(CS_0024_003C_003E8__locals24.url))
				{
					nint method = default(nint);
					action = new Action(CS_0024_003C_003E8__locals24, method);
					method = 0;
					goto IL_020a;
				}
				CS_0024_003C_003E8__locals24.memoryTexture = null;
				if (!string.IsNullOrWhiteSpace(CS_0024_003C_003E8__locals24.url))
				{
					string text = Hash(CS_0024_003C_003E8__locals24.url);
					string key = "url:" + text;
					if (memoryTextures == null)
					{
						goto IL_0222;
					}
					if (memoryTextures.TryGetValue(key, out *(Texture2D*)(CS_0024_003C_003E8__locals24 + 24)) && CS_0024_003C_003E8__locals24.memoryTexture != null)
					{
						action = null;
						nint method = 0;
						goto IL_020a;
					}
				}
				TextureRequest request = CS_0024_003C_003E8__locals24.request;
				Action<byte[]> action2 = delegate(byte[] bytes)
				{
					//IL_01bb: Expected O, but got I
					//IL_01cb: Expected O, but got I
					//IL_01db: Expected O, but got I
					Action<Texture2D> onComplete4 = default(Action<Texture2D>);
					while (true)
					{
						TextureRequest request2 = CS_0024_003C_003E8__locals24.request;
						if (request2._003CCancelled_003Ek__BackingField)
						{
							break;
						}
						if (bytes != null && bytes.Length != 0)
						{
							string urlKey = GetUrlKey(CS_0024_003C_003E8__locals24.url);
							if (CS_0024_003C_003E8__locals24._003C_003E9__3 == null)
							{
								Action<Texture2D> action3 = delegate(Texture2D texture)
								{
									bool flag = texture != null;
									bool flag2 = !flag;
									Texture2D texture2D = texture;
									if (!flag2)
									{
										string text2 = Hash(CS_0024_003C_003E8__locals24.url);
										string key2 = "url:" + text2;
										Texture2D texture2D2 = AddOrGetMemoryTexture(key2, texture);
										texture2D = texture2D2;
									}
									TextureRequest request3 = CS_0024_003C_003E8__locals24.request;
									if (CS_0024_003C_003E8__locals24.request != null && !request3._003CCancelled_003Ek__BackingField && !request3._003CCompleted_003Ek__BackingField)
									{
										Action<Texture2D> onComplete6 = request3.OnComplete;
										request3.OnComplete = null;
										request3._003CCompleted_003Ek__BackingField = true;
										if (request3.OnComplete != null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v165 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
										}
									}
								};
								CS_0024_003C_003E8__locals24._003C_003E9__3 = action3;
							}
							TextureRequest childTextureRequest = RequestTextureFromBytes(urlKey, bytes, CS_0024_003C_003E8__locals24.priority, markNonReadable: true, onComplete4);
							request2.ChildTextureRequest = childTextureRequest;
							break;
						}
						if (request2 == null || request2._003CCancelled_003Ek__BackingField || request2._003CCompleted_003Ek__BackingField)
						{
							break;
						}
						Action<Texture2D> onComplete5 = request2.OnComplete;
						request2._003CCompleted_003Ek__BackingField = true;
						request2.OnComplete = null;
						if (request2.OnComplete == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D>)+18]");
						object obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D>)+28]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D>)+40]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v229 @ rax_v8 (should have been resolved before IL gen)");
					}
				};
				Action<byte[]> onComplete3 = default(Action<byte[]>);
				BytesRequest childBytesRequest = RequestBytes(CS_0024_003C_003E8__locals24.url, CS_0024_003C_003E8__locals24.priority, cacheMaxAge, timeoutSeconds, onComplete3);
				if (CS_0024_003C_003E8__locals24.request != null)
				{
					request.ChildBytesRequest = childBytesRequest;
					goto IL_0218;
				}
			}
		}
		goto IL_0222;
		IL_0218:
		return CS_0024_003C_003E8__locals24.request;
		IL_0222:
		return (TextureRequest)(object)new NullReferenceException();
		IL_020a:
		EnqueueMainThreadAction(action);
		goto IL_0218;
	}

	public unsafe static ZipFramesRequest RequestZipFrameTextures(string zipUrl, TimeSpan cacheMaxAge, int timeoutSeconds, Action<Texture2D> onPreview, Action<Texture2D[]> onComplete)
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected Ref, but got Unknown
		_003C_003Ec__DisplayClass32_0 CS_0024_003C_003E8__locals14 = new _003C_003Ec__DisplayClass32_0();
		if (CS_0024_003C_003E8__locals14 != null)
		{
			CS_0024_003C_003E8__locals14.zipUrl = zipUrl;
			EnsureRunner();
			ZipFramesRequest zipFramesRequest = new ZipFramesRequest();
			if (zipFramesRequest != null)
			{
				zipFramesRequest.OnPreview = onPreview;
				Action<Texture2D[]> onComplete2 = default(Action<Texture2D[]>);
				zipFramesRequest.OnComplete = onComplete2;
				CS_0024_003C_003E8__locals14.request = zipFramesRequest;
				Action action2;
				if (!string.IsNullOrWhiteSpace(CS_0024_003C_003E8__locals14.zipUrl))
				{
					if (!TryGetAllMemoryZipFrames(CS_0024_003C_003E8__locals14.zipUrl, out *(Texture2D[]*)(CS_0024_003C_003E8__locals14 + 24)))
					{
						ZipFramesRequest request = CS_0024_003C_003E8__locals14.request;
						Action<byte[]> action = delegate(byte[] bytes)
						{
							//IL_0155: Expected O, but got I
							//IL_0165: Expected O, but got I
							//IL_0175: Expected O, but got I
							while (true)
							{
								ZipFramesRequest request2 = CS_0024_003C_003E8__locals14.request;
								if (request2._003CCancelled_003Ek__BackingField)
								{
									return;
								}
								if (bytes != null && bytes.Length != 0)
								{
									break;
								}
								if (request2 == null || request2._003CCancelled_003Ek__BackingField || request2._003CCompleted_003Ek__BackingField)
								{
									return;
								}
								Action<Texture2D[]> onComplete4 = request2.OnComplete;
								request2._003CCompleted_003Ek__BackingField = true;
								request2.OnPreview = null;
								request2.OnComplete = null;
								if (request2.OnComplete == null)
								{
									return;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D[]>)+18]");
								object obj = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D[]>)+28]");
								object obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rdi_v4 (System.Action`1<UnityEngine.Texture2D[]>)+40]");
								object obj3 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v214 @ rax_v9 (should have been resolved before IL gen)");
							}
							IEnumerator routine = ProcessZipFramesRoutine(CS_0024_003C_003E8__locals14.request, CS_0024_003C_003E8__locals14.zipUrl, bytes);
							Coroutine coroutine = runner.StartCoroutine(routine);
						};
						Action<byte[]> onComplete3 = default(Action<byte[]>);
						BytesRequest bytesRequest = RequestBytes(CS_0024_003C_003E8__locals14.zipUrl, priority: true, cacheMaxAge, timeoutSeconds, onComplete3);
						if (CS_0024_003C_003E8__locals14.request != null)
						{
							request.BytesRequest = bytesRequest;
							goto IL_018c;
						}
						goto IL_0196;
					}
					action2 = null;
					nint num = 0;
				}
				else
				{
					nint num = default(nint);
					action2 = new Action(CS_0024_003C_003E8__locals14, num);
					num = 0;
				}
				EnqueueMainThreadAction(action2);
				goto IL_018c;
			}
		}
		goto IL_0196;
		IL_0196:
		return (ZipFramesRequest)(object)new NullReferenceException();
		IL_018c:
		return CS_0024_003C_003E8__locals14.request;
	}

	public unsafe static ZipFramesRequest RequestZipFrameTexturesFromBytes(string key, byte[] zipBytes, Action<Texture2D> onPreview, Action<Texture2D[]> onComplete)
	{
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected Ref, but got Unknown
		_003C_003Ec__DisplayClass33_0 obj = new _003C_003Ec__DisplayClass33_0();
		EnsureRunner();
		ZipFramesRequest zipFramesRequest = new ZipFramesRequest();
		if (zipFramesRequest != null)
		{
			zipFramesRequest.OnPreview = onPreview;
			zipFramesRequest.OnComplete = onComplete;
			if (obj != null)
			{
				obj.request = zipFramesRequest;
				Action action;
				if (!string.IsNullOrWhiteSpace(key) && zipBytes != null && zipBytes.Length != 0)
				{
					if (!TryGetAllMemoryZipFrames(key, out *(Texture2D[]*)(obj + 24)))
					{
						IEnumerator routine = ProcessZipFramesRoutine(obj.request, key, zipBytes);
						if ((object)runner != null)
						{
							Coroutine coroutine = runner.StartCoroutine(routine);
							goto IL_0163;
						}
						goto IL_016d;
					}
					action = null;
					nint num = 0;
				}
				else
				{
					nint num = default(nint);
					action = new Action(obj, num);
					num = 0;
				}
				EnqueueMainThreadAction(action);
				goto IL_0163;
			}
		}
		goto IL_016d;
		IL_0163:
		return obj.request;
		IL_016d:
		return (ZipFramesRequest)(object)new NullReferenceException();
	}

	private static BytesRequest RequestBytes(string url, bool priority, TimeSpan cacheMaxAge, int timeoutSeconds, Action<byte[]> onComplete)
	{
		_003C_003Ec__DisplayClass34_0 CS_0024_003C_003E8__locals8 = new _003C_003Ec__DisplayClass34_0();
		EnsureRunner();
		BytesRequest bytesRequest = new BytesRequest();
		if (bytesRequest != null)
		{
			Action<byte[]> onComplete2 = default(Action<byte[]>);
			bytesRequest.OnComplete = onComplete2;
			if (CS_0024_003C_003E8__locals8 != null)
			{
				CS_0024_003C_003E8__locals8.request = bytesRequest;
				if (string.IsNullOrWhiteSpace(url))
				{
					Action action = delegate
					{
						BytesRequest request2 = CS_0024_003C_003E8__locals8.request;
						if (CS_0024_003C_003E8__locals8.request != null && !request2._003CCancelled_003Ek__BackingField && !request2._003CCompleted_003Ek__BackingField)
						{
							Action<byte[]> onComplete3 = request2.OnComplete;
							request2.OnComplete = null;
							request2._003CCompleted_003Ek__BackingField = true;
							if (request2.OnComplete != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v110 @ rdi_v2 (System.Action`1<System.Byte[]>)+18] (should have been resolved before IL gen)");
							}
						}
					};
					EnqueueMainThreadAction(action);
					goto IL_02db;
				}
				string text = Hash(url);
				string key = "download:" + text;
				if (downloadJobs != null)
				{
					DownloadJob downloadJob = default(DownloadJob);
					if (!downloadJobs.TryGetValue(key, out var _))
					{
						downloadJob = new DownloadJob();
						List<BytesRequest> requests = new List<BytesRequest>();
						downloadJob.Requests = requests;
						downloadJob.Url = url;
						downloadJob.Key = key;
						string cacheFile = GetCacheFile(url);
						downloadJob.CacheFile = cacheFile;
						downloadJob.CacheMaxAge = cacheMaxAge;
						bool flag = timeoutSeconds < 1;
						int timeoutSeconds2 = 1;
						if (!flag)
						{
							timeoutSeconds2 = timeoutSeconds;
						}
						downloadJob.TimeoutSeconds = timeoutSeconds2;
						if (downloadJobs == null)
						{
							goto IL_0311;
						}
						downloadJobs.set_Item(key, downloadJob);
					}
					BytesRequest request = CS_0024_003C_003E8__locals8.request;
					if (CS_0024_003C_003E8__locals8.request != null)
					{
						request.Job = downloadJob;
						if (downloadJob != null && downloadJob.Requests != null)
						{
							downloadJob.Requests.Add(CS_0024_003C_003E8__locals8.request);
							if (downloadJob != null)
							{
								Queue<DownloadJob> queue;
								if (!priority)
								{
									if (downloadJob.QueuedNormal != priority)
									{
										goto IL_02d1;
									}
									downloadJob.QueuedNormal = true;
									queue = normalDownloadJobs;
								}
								else
								{
									if (downloadJob.QueuedPriority)
									{
										goto IL_02d1;
									}
									downloadJob.QueuedPriority = true;
									queue = priorityDownloadJobs;
								}
								if (queue != null)
								{
									queue.Enqueue(downloadJob);
									goto IL_02d1;
								}
							}
						}
					}
				}
			}
		}
		goto IL_0311;
		IL_02d1:
		PumpDownloadJobs();
		goto IL_02db;
		IL_02db:
		return CS_0024_003C_003E8__locals8.request;
		IL_0311:
		return (BytesRequest)(object)new NullReferenceException();
	}

	private static void EnqueueDownloadJob(DownloadJob job, bool priority)
	{
		Queue<DownloadJob> queue;
		if (!priority)
		{
			if (job.QueuedNormal != priority)
			{
				return;
			}
			job.QueuedNormal = true;
			queue = normalDownloadJobs;
		}
		else
		{
			if (job.QueuedPriority)
			{
				return;
			}
			job.QueuedPriority = true;
			queue = priorityDownloadJobs;
		}
		queue.Enqueue(job);
	}

	private static void PumpDownloadJobs()
	{
		EnsureRunner();
		DownloadJob downloadJob = default(DownloadJob);
		DownloadJob downloadJob3 = default(DownloadJob);
		while (activeDownloadJobs < MaxActiveJobs)
		{
			Queue<DownloadJob> queue = priorityDownloadJobs;
			DownloadJob downloadJob2;
			if (queue._size > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
				downloadJob.QueuedPriority = false;
				downloadJob2 = downloadJob;
			}
			else
			{
				Queue<DownloadJob> queue2 = normalDownloadJobs;
				if (queue2._size <= 0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
				downloadJob3.QueuedNormal = false;
				downloadJob2 = downloadJob3;
			}
			if (downloadJob2 == null)
			{
				break;
			}
			if (!downloadJob2.Started)
			{
				if (downloadJob2.HasLiveRequests())
				{
					downloadJob2.Started = true;
					IEnumerator routine = ProcessDownloadJobRoutine(downloadJob2);
					Coroutine coroutine = runner.StartCoroutine(routine);
				}
				else
				{
					bool flag = downloadJobs.Remove(downloadJob2.Key);
				}
			}
		}
	}

	private static DownloadJob DequeueDownloadJob()
	{
		Queue<DownloadJob> queue = priorityDownloadJobs;
		if (priorityDownloadJobs != null)
		{
			DownloadJob downloadJob = default(DownloadJob);
			if (queue._size > 0)
			{
				if (priorityDownloadJobs != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
					if (downloadJob != null)
					{
						downloadJob.QueuedPriority = false;
						return downloadJob;
					}
				}
			}
			else
			{
				Queue<DownloadJob> queue2 = normalDownloadJobs;
				if (normalDownloadJobs != null)
				{
					if (queue2._size <= 0)
					{
						return null;
					}
					if (normalDownloadJobs != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
						if (downloadJob != null)
						{
							downloadJob.QueuedNormal = false;
							return downloadJob;
						}
					}
				}
			}
		}
		return (DownloadJob)(object)new NullReferenceException();
	}

	private static IEnumerator ProcessDownloadJobRoutine(DownloadJob job)
	{
		_003CProcessDownloadJobRoutine_003Ed__38 obj = new _003CProcessDownloadJobRoutine_003Ed__38(0);
		obj._003C_003E1__state = 0;
		obj.job = job;
		return obj;
	}

	private static void CompleteDownloadJob(DownloadJob job, byte[] bytes)
	{
		//IL_00aa: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<BytesRequest>.Enumerator enumerator = default(List<BytesRequest>.Enumerator);
		object obj = default(object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (obj == null)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ stack_8_v2+10]");
			if ((nint)0 != 0)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ stack_8_v2+11]");
			if ((nint)0 == 0)
			{
				_ = 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ stack_8_v2+20]");
				object obj2 = 0;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ stack_8_v2+20]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v150 @ rdi_v3+18] (should have been resolved before IL gen)");
				}
			}
		}
		enumerator.Dispose();
	}

	private static void CompleteBytesRequest(BytesRequest request, byte[] bytes)
	{
		if (request != null && !request._003CCancelled_003Ek__BackingField && !request._003CCompleted_003Ek__BackingField)
		{
			Action<byte[]> onComplete = request.OnComplete;
			request.OnComplete = null;
			request._003CCompleted_003Ek__BackingField = true;
			if (request.OnComplete != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v96 @ rbx_v3 (System.Action`1<System.Byte[]>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	private unsafe static TextureRequest RequestTextureFromBytes(string key, byte[] bytes, bool priority, bool markNonReadable, Action<Texture2D> onComplete)
	{
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected Ref, but got Unknown
		_003C_003Ec__DisplayClass41_0 obj = new _003C_003Ec__DisplayClass41_0();
		EnsureRunner();
		TextureRequest textureRequest = new TextureRequest();
		Action action;
		if (textureRequest != null)
		{
			Action<Texture2D> onComplete2 = default(Action<Texture2D>);
			textureRequest.OnComplete = onComplete2;
			if (obj != null)
			{
				obj.request = textureRequest;
				if (string.IsNullOrWhiteSpace(key) || bytes == null || bytes.Length == 0)
				{
					nint method = default(nint);
					action = new Action(obj, method);
					method = 0;
					goto IL_031d;
				}
				if (memoryTextures != null)
				{
					if (memoryTextures.TryGetValue(key, out *(Texture2D*)(obj + 24)) && obj.memoryTexture != null)
					{
						action = null;
						nint method = 0;
						goto IL_031d;
					}
					if (textureJobs != null)
					{
						TextureJob textureJob = default(TextureJob);
						if (!textureJobs.TryGetValue(key, out var _))
						{
							textureJob = new TextureJob();
							List<TextureRequest> requests = new List<TextureRequest>();
							textureJob.Requests = requests;
							textureJob.Key = key;
							textureJob.Bytes = bytes;
							textureJob.MarkNonReadable = markNonReadable;
							if (textureJobs == null)
							{
								goto IL_0335;
							}
							textureJobs.set_Item(key, textureJob);
						}
						TextureRequest request = obj.request;
						if (obj.request != null)
						{
							request.Job = textureJob;
							if (textureJob != null && textureJob.Requests != null)
							{
								textureJob.Requests.Add(obj.request);
								if (textureJob != null)
								{
									Queue<TextureJob> queue;
									if (!priority)
									{
										if (textureJob.QueuedNormal != priority)
										{
											goto IL_02f7;
										}
										textureJob.QueuedNormal = true;
										queue = normalTextureJobs;
									}
									else
									{
										if (textureJob.QueuedPriority)
										{
											goto IL_02f7;
										}
										textureJob.QueuedPriority = true;
										queue = priorityTextureJobs;
									}
									if (queue != null)
									{
										queue.Enqueue(textureJob);
										goto IL_02f7;
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0335;
		IL_0335:
		return (TextureRequest)(object)new NullReferenceException();
		IL_032b:
		return obj.request;
		IL_031d:
		EnqueueMainThreadAction(action);
		goto IL_032b;
		IL_02f7:
		PumpTextureJobs();
		goto IL_032b;
	}

	private static void EnqueueTextureJob(TextureJob job, bool priority)
	{
		Queue<TextureJob> queue;
		if (!priority)
		{
			if (job.QueuedNormal != priority)
			{
				return;
			}
			job.QueuedNormal = true;
			queue = normalTextureJobs;
		}
		else
		{
			if (job.QueuedPriority)
			{
				return;
			}
			job.QueuedPriority = true;
			queue = priorityTextureJobs;
		}
		queue.Enqueue(job);
	}

	private static void PumpTextureJobs()
	{
		EnsureRunner();
		TextureJob textureJob = default(TextureJob);
		TextureJob textureJob3 = default(TextureJob);
		while (activeTextureJobs < MaxActiveJobs)
		{
			Queue<TextureJob> queue = priorityTextureJobs;
			TextureJob textureJob2;
			if (queue._size > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
				textureJob.QueuedPriority = false;
				textureJob2 = textureJob;
			}
			else
			{
				Queue<TextureJob> queue2 = normalTextureJobs;
				if (queue2._size <= 0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
				textureJob3.QueuedNormal = false;
				textureJob2 = textureJob3;
			}
			if (textureJob2 == null)
			{
				break;
			}
			if (!textureJob2.Started)
			{
				if (textureJob2.HasLiveRequests())
				{
					textureJob2.Started = true;
					IEnumerator routine = ProcessTextureJobRoutine(textureJob2);
					Coroutine coroutine = runner.StartCoroutine(routine);
				}
				else
				{
					bool flag = textureJobs.Remove(textureJob2.Key);
				}
			}
		}
	}

	private static TextureJob DequeueTextureJob()
	{
		Queue<TextureJob> queue = priorityTextureJobs;
		if (priorityTextureJobs != null)
		{
			TextureJob textureJob = default(TextureJob);
			if (queue._size > 0)
			{
				if (priorityTextureJobs != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
					if (textureJob != null)
					{
						textureJob.QueuedPriority = false;
						return textureJob;
					}
				}
			}
			else
			{
				Queue<TextureJob> queue2 = normalTextureJobs;
				if (normalTextureJobs != null)
				{
					if (queue2._size <= 0)
					{
						return null;
					}
					if (normalTextureJobs != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
						if (textureJob != null)
						{
							textureJob.QueuedNormal = false;
							return textureJob;
						}
					}
				}
			}
		}
		return (TextureJob)(object)new NullReferenceException();
	}

	private static IEnumerator ProcessTextureJobRoutine(TextureJob job)
	{
		_003CProcessTextureJobRoutine_003Ed__45 obj = new _003CProcessTextureJobRoutine_003Ed__45(0);
		obj._003C_003E1__state = 0;
		obj.job = job;
		return obj;
	}

	private static void CompleteTextureJob(TextureJob job, Texture2D texture)
	{
		//IL_00aa: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<TextureRequest>.Enumerator enumerator = default(List<TextureRequest>.Enumerator);
		object obj = default(object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (obj == null)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ stack_8_v2+10]");
			if ((nint)0 != 0)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ stack_8_v2+11]");
			if ((nint)0 == 0)
			{
				_ = 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ stack_8_v2+30]");
				object obj2 = 0;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ stack_8_v2+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v150 @ rdi_v3+18] (should have been resolved before IL gen)");
				}
			}
		}
		enumerator.Dispose();
	}

	private static void CompleteTextureRequest(TextureRequest request, Texture2D texture)
	{
		if (request != null && !request._003CCancelled_003Ek__BackingField && !request._003CCompleted_003Ek__BackingField)
		{
			Action<Texture2D> onComplete = request.OnComplete;
			request.OnComplete = null;
			request._003CCompleted_003Ek__BackingField = true;
			if (request.OnComplete != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v96 @ rbx_v3 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	private static IEnumerator WaitForTextureCreateSlot(Func<bool> isValid)
	{
		_003CWaitForTextureCreateSlot_003Ed__48 obj = new _003CWaitForTextureCreateSlot_003Ed__48(0);
		obj._003C_003E1__state = 0;
		obj.isValid = isValid;
		return obj;
	}

	private static bool TryTakeTextureCreateSlot()
	{
		//IL_0105: Invalid comparison between F4 and I4
		//IL_00c6: Invalid comparison between F4 and I4
		if (TextureCreateSpacing > 0f)
		{
			float unscaledTime = Time.unscaledTime;
			if (nextTextureCreateTime > unscaledTime)
			{
				goto IL_0039;
			}
		}
		int frameCount = Time.frameCount;
		if (lastTextureFrame != frameCount)
		{
			int frameCount2 = Time.frameCount;
			lastTextureFrame = frameCount2;
			textureCreatesThisFrame = 0;
		}
		if (textureCreatesThisFrame < MaxTextureCreatesPerFrame)
		{
			int num = textureCreatesThisFrame + 1;
			textureCreatesThisFrame = num;
			if (TextureCreateSpacing > 0f)
			{
				float unscaledTime2 = Time.unscaledTime;
				float num2 = unscaledTime2 + TextureCreateSpacing;
				nextTextureCreateTime = num2;
			}
			return true;
		}
		goto IL_0039;
		IL_0039:
		return false;
	}

	private static IEnumerator ProcessZipFramesRoutine(ZipFramesRequest request, string zipUrl, byte[] zipBytes)
	{
		_003CProcessZipFramesRoutine_003Ed__50 obj = new _003CProcessZipFramesRoutine_003Ed__50(0);
		obj._003C_003E1__state = 0;
		obj.request = request;
		obj.zipUrl = zipUrl;
		obj.zipBytes = zipBytes;
		return obj;
	}

	public unsafe static List<byte[]> ReadFrameZip(byte[] zipBytes)
	{
		//IL_04a7: Expected O, but got I4
		//IL_009e: Expected O, but got Ref
		//IL_04dc: Expected O, but got I4
		//IL_05a9: Expected O, but got Ref
		//IL_05dd: Expected O, but got Ref
		//IL_00f5: Expected I, but got O
		//IL_0180: Expected O, but got I4
		//IL_012d: Expected O, but got I
		//IL_0136: Expected O, but got I4
		//IL_038b: Expected O, but got I
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Expected O, but got Unknown
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_01d4: Expected O, but got I4
		//IL_0229: Expected O, but got I4
		//IL_02e9: Expected I, but got O
		//IL_0267: Expected O, but got I4
		//IL_0270: Expected O, but got I4
		List<byte[]> list = new List<byte[]>();
		MemoryStream memoryStream = new MemoryStream(zipBytes);
		memoryStream._002Ector(zipBytes);
		Stream stream = default(Stream);
		ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Read);
		ZipArchive zipArchive2 = default(ZipArchive);
		if (zipArchive2 != null)
		{
			ReadOnlyCollection<ZipArchiveEntry> entries = zipArchive2.Entries;
			Func<ZipArchiveEntry, string> keySelector = _003C_003Ec._003C_003E9__51_0;
			bool flag = _003C_003Ec._003C_003E9__51_0 != null;
			object obj = 0;
			if (!flag)
			{
				Func<ZipArchiveEntry, string> func = (_003C_003Ec._003C_003E9__51_0 = (ZipArchiveEntry x) => (string)((x != null) ? ((object)x._storedEntryName) : ((object)new NullReferenceException())));
				obj = 0;
				keySelector = func;
			}
			IOrderedEnumerable<ZipArchiveEntry> orderedEnumerable = Enumerable.OrderBy(entries, keySelector);
			if (orderedEnumerable != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				ZipArchive zipArchive3 = default(ZipArchive);
				object obj2 = (object)(&zipArchive3);
				object obj3 = default(object);
				object obj12 = default(object);
				ZipArchiveEntry zipArchiveEntry = default(ZipArchiveEntry);
				Stream stream3 = default(Stream);
				Stream stream4 = default(Stream);
				byte[] item = default(byte[]);
				while (true)
				{
					object obj4;
					object obj11;
					if (zipArchive3 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (obj3 != null)
						{
							bool flag2 = zipArchive3 == null;
							ZipArchive zipArchive4 = null;
							if (!flag2)
							{
								nint num = (nint)zipArchive3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v376 @ r10_v6 (Il2CppClass<System.IO.Compression.ZipArchive>)+12E]");
								if ((nint)0 >= (nint)0)
								{
									goto IL_016d;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v376 @ r10_v6 (Il2CppClass<System.IO.Compression.ZipArchive>)+B0]");
								obj4 = 0;
								object obj5 = 0;
								while (true)
								{
									object obj6 = obj5 + obj5;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v627 @ r8_v20+v546 @ rax_v75*8]");
									if (0 == (nint)typeof(IEnumerator<ZipArchiveEntry>))
									{
										break;
									}
									obj5++;
									object obj7 = obj5;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v376 @ r10_v6 (Il2CppClass<System.IO.Compression.ZipArchive>)+12E]");
									if ((nint)obj7 < 0)
									{
										continue;
									}
									goto IL_016d;
								}
								object obj8 = obj5 + obj5;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v627 @ r8_v20+8+v622 @ rcx_v53*8]");
								object obj9 = (nint)0 << 4;
								object obj10 = obj9 + 312;
								obj11 = obj10 + num;
								goto IL_0570;
							}
							throw new NullReferenceException();
						}
						if (obj2 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						}
						break;
					}
					throw new NullReferenceException();
					IL_016d:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj4 = 0;
					obj11 = obj12;
					goto IL_0570;
					IL_0570:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v628 @ rdx_v21] (should have been resolved before IL gen)");
					if (zipArchiveEntry != null)
					{
						if (zipArchiveEntry._storedEntryName != null)
						{
							bool flag3 = zipArchiveEntry._storedEntryName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase);
							object obj13 = 0;
							if (!flag3)
							{
								if (zipArchiveEntry._storedEntryName == null)
								{
									throw new NullReferenceException();
								}
								bool flag4 = zipArchiveEntry._storedEntryName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase);
								obj13 = 0;
								if (!flag4)
								{
									bool flag5 = zipArchiveEntry._storedEntryName.EndsWith(".png", StringComparison.OrdinalIgnoreCase);
									bool flag6 = !flag5;
									obj13 = 0;
									obj = 0;
									if (flag6)
									{
										continue;
									}
								}
							}
							Stream stream2 = zipArchiveEntry.Open();
							MemoryStream memoryStream2 = new MemoryStream();
							if (stream3 != null)
							{
								stream3.CopyTo(stream4);
								if (stream4 != null)
								{
									nint num2 = (nint)stream4;
									Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v901 @ rdx_v33 (Il2CppClass<System.String>)+3E8] (should have been resolved before IL gen)");
									if (list != null)
									{
										list.Add(item);
										if (stream4 != null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
										}
										if (stream3 != null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
										}
										obj = obj13;
										continue;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				object obj14 = (object)(&zipArchive2);
				if (obj14 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				object obj15 = (object)(&stream);
				if (obj15 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return list;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	private static Task<List<byte[]>> ReadFrameZipAsync(byte[] zipBytes)
	{
		_003C_003Ec__DisplayClass52_0 CS_0024_003C_003E8__locals3 = new _003C_003Ec__DisplayClass52_0();
		if (CS_0024_003C_003E8__locals3 != null)
		{
			CS_0024_003C_003E8__locals3.zipBytes = zipBytes;
			Func<List<byte[]>> function = () => ReadFrameZip(CS_0024_003C_003E8__locals3.zipBytes);
			return Task.Run(function);
		}
		return (Task<List<byte[]>>)(object)new NullReferenceException();
	}

	private static void CompleteZipRequest(ZipFramesRequest request, Texture2D[] frames)
	{
		if (request != null && !request._003CCancelled_003Ek__BackingField && !request._003CCompleted_003Ek__BackingField)
		{
			Action<Texture2D[]> onComplete = request.OnComplete;
			request._003CCompleted_003Ek__BackingField = true;
			request.OnPreview = null;
			request.OnComplete = null;
			if (request.OnComplete != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v103 @ rdi_v3 (System.Action`1<UnityEngine.Texture2D[]>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	private unsafe static bool TryGetAllMemoryZipFrames(string zipUrl, out Texture2D[] frames)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_01b6: Expected I4, but got O
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		ref Texture2D[] reference = ref *(Texture2D[]*)null;
		string zipFramePrefix = GetZipFramePrefix(zipUrl);
		Texture2D[] array;
		if (zipFrameCounts.TryGetValue(zipFramePrefix, out var value) && value > 0)
		{
			array = new Texture2D[value];
			if (value <= 0)
			{
				goto IL_0160;
			}
			object obj = array + 32;
			int num = 0;
			while (true)
			{
				string zipFrameKey = GetZipFrameKey(zipUrl, num);
				if (!memoryTextures.TryGetValue(zipFrameKey, out var value2) || !(value2 != null))
				{
					break;
				}
				if (num < array.Length)
				{
					obj = value2;
					num++;
					obj += 8;
					if (num >= value)
					{
						goto IL_0160;
					}
					continue;
				}
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
			}
		}
		return false;
		IL_0160:
		reference = ref *(Texture2D[]*)array;
		return true;
	}

	public unsafe static bool TryGetMemoryTextureForUrl(string url, out Texture2D texture)
	{
		//IL_00a2: Expected I4, but got O
		ref Texture2D reference = ref *(Texture2D*)null;
		if (!string.IsNullOrWhiteSpace(url))
		{
			string text = Hash(url);
			string key = "url:" + text;
			if (memoryTextures != null)
			{
				bool flag = memoryTextures.TryGetValue(key, out texture);
				if (!flag)
				{
					return flag;
				}
				return texture != null;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	public unsafe static bool TryGetMemoryTextureForAvatar(string base64, out Texture2D texture)
	{
		//IL_0060: Expected I4, but got O
		ref Texture2D reference = ref *(Texture2D*)null;
		if (!string.IsNullOrWhiteSpace(base64))
		{
			string avatarKey = GetAvatarKey(base64);
			if (memoryTextures != null)
			{
				bool flag = memoryTextures.TryGetValue(avatarKey, out texture);
				if (!flag)
				{
					return flag;
				}
				return texture != null;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	private static Texture2D AddOrGetMemoryTexture(string key, Texture2D texture)
	{
		if (!(texture != null) || string.IsNullOrWhiteSpace(key))
		{
			goto IL_00ff;
		}
		if (memoryTextures != null)
		{
			if (memoryTextures.TryGetValue(key, out var value) && value != null)
			{
				if (value != texture)
				{
					UnityEngine.Object.Destroy(texture);
				}
				return value;
			}
			if (memoryTextures != null)
			{
				memoryTextures.set_Item(key, texture);
				goto IL_00ff;
			}
		}
		return (Texture2D)(object)new NullReferenceException();
		IL_00ff:
		return texture;
	}

	public static Texture2D AddOrGetMemoryTextureForAvatar(string base64, Texture2D texture)
	{
		bool flag = texture != null;
		Texture2D result = texture;
		if (flag)
		{
			bool flag2 = string.IsNullOrWhiteSpace(base64);
			result = texture;
			if (!flag2)
			{
				string avatarKey = GetAvatarKey(base64);
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 92 Invalid \"Jump target not found in method: 0x180583540\"");
				Texture2D texture2D = default(Texture2D);
				result = texture2D;
			}
		}
		return result;
	}

	public unsafe static void UnloadUnusedCachedTextures(IEnumerable<string> imageUrls, IEnumerable<string> avatarBase64s = null, IEnumerable<string> zipUrls = null)
	{
		//IL_0042: Expected O, but got Ref
		//IL_01f8: Expected O, but got Ref
		//IL_0486: Expected O, but got Ref
		//IL_020a: Expected I, but got O
		//IL_0498: Expected I, but got O
		//IL_0291: Expected O, but got I4
		//IL_051f: Expected O, but got I4
		//IL_0242: Expected O, but got I
		//IL_074b: Expected O, but got Ref
		//IL_04d0: Expected O, but got I
		//IL_009e: Expected I, but got O
		//IL_046f: Expected I, but got O
		//IL_03d2: Expected O, but got I
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Expected O, but got Unknown
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Expected O, but got Unknown
		//IL_0125: Expected O, but got I4
		//IL_0660: Expected O, but got I
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Expected O, but got Unknown
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Expected O, but got Unknown
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Expected O, but got Unknown
		//IL_0ac3: Expected I, but got O
		//IL_00d6: Expected O, but got I
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Expected O, but got Unknown
		//IL_02c9: Expected I, but got O
		//IL_0557: Expected I, but got O
		//IL_0350: Expected O, but got I4
		//IL_0195: Expected O, but got I
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Expected O, but got Unknown
		//IL_05de: Expected O, but got I4
		//IL_0301: Expected O, but got I
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_091a: Expected O, but got Ref
		//IL_058f: Expected O, but got I
		//IL_0165: Expected I, but got O
		//IL_0415: Expected O, but got I
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Expected O, but got Unknown
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Expected O, but got Unknown
		//IL_06a3: Expected O, but got I
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Expected O, but got Unknown
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Expected O, but got Unknown
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Expected O, but got Unknown
		//IL_07ca: Expected O, but got Ref
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Expected O, but got Unknown
		HashSet<string> hashSet = new HashSet<string>();
		HashSet<string> hashSet2 = new HashSet<string>();
		hashSet2._002Ector();
		bool flag = imageUrls == null;
		string text = null;
		nint num2 = default(nint);
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = (object)(&text);
			string text2 = null;
			object obj2 = default(object);
			object obj9 = default(object);
			string text5 = default(string);
			while (true)
			{
				object obj3;
				object obj8;
				if (false)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj2 == null)
					{
						break;
					}
					bool flag2 = 0 == 0;
					text2 = null;
					if (!flag2)
					{
						nint num = (nint)text;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ r10_v27 (Il2CppClass<System.String>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_0112;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ r10_v27 (Il2CppClass<System.String>)+B0]");
						obj3 = 0;
						string text3 = null;
						while (true)
						{
							object obj4 = text3 + text3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r8_v69+v1057 @ rax_v177*8]");
							if (0 == (nint)typeof(IEnumerator<string>))
							{
								break;
							}
							text3++;
							string text4 = text3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ r10_v27 (Il2CppClass<System.String>)+12E]");
							if ((nint)text4 < 0)
							{
								continue;
							}
							goto IL_0112;
						}
						object obj5 = text3 + text3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r8_v69+8+v1290 @ rcx_v136*8]");
						object obj6 = (nint)0 << 4;
						object obj7 = obj6 + 312;
						obj8 = obj7 + num;
						goto IL_0a9e;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
				IL_0112:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj3 = 0;
				obj8 = obj9;
				goto IL_0a9e;
				IL_0a9e:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1295 @ rdx_v107] (should have been resolved before IL gen)");
				bool flag3 = string.IsNullOrWhiteSpace(text5);
				num2 = (nint)typeof(IEnumerator<string>);
				text2 = text5;
				if (!flag3)
				{
					string urlKey = GetUrlKey(text5);
					hashSet.Add(urlKey);
					num2 = (nint)typeof(IEnumerator<string>);
					text2 = (string)(object)hashSet;
				}
			}
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}
		bool flag4 = avatarBase64s == null;
		nint num3 = num2;
		if (!flag4)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj10 = (object)(&text);
			string text2 = null;
			object obj17 = default(object);
			object obj18 = default(object);
			object obj25 = default(object);
			string text10 = default(string);
			while (true)
			{
				object obj11;
				object obj16;
				if (false)
				{
					nint num4 = (nint)text;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ r10_v13 (Il2CppClass<System.String>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_027e;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ r10_v13 (Il2CppClass<System.String>)+B0]");
					obj11 = 0;
					string text6 = null;
					while (true)
					{
						object obj12 = text6 + text6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v883 @ r8_v42+v769 @ rax_v131*8]");
						if (0 == (nint)typeof(IEnumerator))
						{
							break;
						}
						text6++;
						string text7 = text6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ r10_v13 (Il2CppClass<System.String>)+12E]");
						if ((nint)text7 < 0)
						{
							continue;
						}
						goto IL_027e;
					}
					object obj13 = text6 + text6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v883 @ r8_v42+8+v1088 @ rcx_v107*8]");
					object obj14 = (nint)0 << 4;
					object obj15 = obj14 + 312;
					obj16 = obj15 + num4;
					goto IL_0b70;
				}
				throw new NullReferenceException();
				IL_027e:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj11 = 0;
				obj16 = obj17;
				goto IL_0b70;
				IL_0b70:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1093 @ rdx_v68] (should have been resolved before IL gen)");
				if (obj18 == null)
				{
					break;
				}
				bool flag5 = 0 == 0;
				text2 = null;
				object obj19;
				object obj24;
				if (!flag5)
				{
					nint num5 = (nint)text;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v373 @ r10_v14 (Il2CppClass<System.String>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_033d;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v373 @ r10_v14 (Il2CppClass<System.String>)+B0]");
					obj19 = 0;
					string text8 = null;
					while (true)
					{
						object obj20 = text8 + text8;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ r8_v45+v1389 @ rax_v126*8]");
						if (0 == (nint)typeof(IEnumerator<string>))
						{
							break;
						}
						text8++;
						string text9 = text8;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v373 @ r10_v14 (Il2CppClass<System.String>)+12E]");
						if ((nint)text9 < 0)
						{
							continue;
						}
						goto IL_033d;
					}
					object obj21 = text8 + text8;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ r8_v45+8+v1672 @ rcx_v101*8]");
					object obj22 = (nint)0 << 4;
					object obj23 = obj22 + 312;
					obj24 = obj23 + num5;
					goto IL_0b97;
				}
				throw new NullReferenceException();
				IL_033d:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj19 = 0;
				obj24 = obj25;
				goto IL_0b97;
				IL_0b97:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1677 @ rdx_v73] (should have been resolved before IL gen)");
				bool flag6 = string.IsNullOrWhiteSpace(text10);
				text2 = text10;
				if (!flag6)
				{
					string avatarKey = GetAvatarKey(text10);
					bool flag7 = hashSet == null;
					text2 = text10;
					if (flag7)
					{
						throw new NullReferenceException();
					}
					hashSet.Add(avatarKey);
					text2 = (string)(object)hashSet;
				}
			}
			if (obj10 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			num3 = (nint)typeof(IEnumerator);
		}
		if (zipUrls != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj26 = (object)(&text);
			string text2 = null;
			object obj33 = default(object);
			object obj34 = default(object);
			object obj41 = default(object);
			string text15 = default(string);
			while (true)
			{
				object obj27;
				object obj32;
				if (false)
				{
					nint num6 = (nint)text;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ r10_v10 (Il2CppClass<System.String>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_050c;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ r10_v10 (Il2CppClass<System.String>)+B0]");
					obj27 = 0;
					string text11 = null;
					while (true)
					{
						object obj28 = text11 + text11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1118 @ r8_v29+v952 @ rax_v106*8]");
						if (0 == (nint)typeof(IEnumerator))
						{
							break;
						}
						text11++;
						string text12 = text11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ r10_v10 (Il2CppClass<System.String>)+12E]");
						if ((nint)text12 < 0)
						{
							continue;
						}
						goto IL_050c;
					}
					object obj29 = text11 + text11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1118 @ r8_v29+8+v1251 @ rcx_v81*8]");
					object obj30 = (nint)0 << 4;
					object obj31 = obj30 + 312;
					obj32 = obj31 + num6;
					goto IL_0c78;
				}
				throw new NullReferenceException();
				IL_050c:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj27 = 0;
				obj32 = obj33;
				goto IL_0c78;
				IL_0c78:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1256 @ rdx_v43] (should have been resolved before IL gen)");
				if (obj34 == null)
				{
					break;
				}
				bool flag8 = 0 == 0;
				text2 = null;
				object obj35;
				object obj40;
				if (!flag8)
				{
					nint num7 = (nint)text;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ r10_v11 (Il2CppClass<System.String>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_05cb;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ r10_v11 (Il2CppClass<System.String>)+B0]");
					obj35 = 0;
					string text13 = null;
					while (true)
					{
						object obj36 = text13 + text13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v529 @ r8_v32+v1576 @ rax_v101*8]");
						if (0 == (nint)typeof(IEnumerator<string>))
						{
							break;
						}
						text13++;
						string text14 = text13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ r10_v11 (Il2CppClass<System.String>)+12E]");
						if ((nint)text14 < 0)
						{
							continue;
						}
						goto IL_05cb;
					}
					object obj37 = text13 + text13;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v529 @ r8_v32+8+v1779 @ rcx_v75*8]");
					object obj38 = (nint)0 << 4;
					object obj39 = obj38 + 312;
					obj40 = obj39 + num7;
					goto IL_0c9f;
				}
				throw new NullReferenceException();
				IL_05cb:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj35 = 0;
				obj40 = obj41;
				goto IL_0c9f;
				IL_0c9f:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1784 @ rdx_v48] (should have been resolved before IL gen)");
				bool flag9 = string.IsNullOrWhiteSpace(text15);
				text2 = text15;
				if (!flag9)
				{
					string zipFramePrefix = GetZipFramePrefix(text15);
					bool flag10 = hashSet2 == null;
					text2 = text15;
					if (flag10)
					{
						throw new NullReferenceException();
					}
					hashSet2.Add(zipFramePrefix);
					text2 = (string)(object)hashSet2;
				}
			}
			if (obj26 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}
		Dictionary<string, Texture2D>.KeyCollection keys = memoryTextures.Keys;
		List<string> list = new List<string>(keys);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<string>.Enumerator enumerator = default(List<string>.Enumerator);
		string text16 = default(string);
		HashSet<string>.Enumerator enumerator3 = default(HashSet<string>.Enumerator);
		List<string>.Enumerator enumerator4 = default(List<string>.Enumerator);
		string text17 = default(string);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag11 = hashSet == null;
				HashSet<string>.Enumerator enumerator2 = (HashSet<string>.Enumerator)(&enumerator);
				if (flag11)
				{
					break;
				}
				if (hashSet.Contains(text16))
				{
					continue;
				}
				if (hashSet2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18087B860");
					while (true)
					{
						if (enumerator3.MoveNext())
						{
							string current = enumerator3.Current;
							bool flag12 = text16 == null;
							enumerator2 = (HashSet<string>.Enumerator)(&enumerator3);
							if (!flag12)
							{
								if (text16.StartsWith(current, StringComparison.Ordinal))
								{
									enumerator3.Dispose();
									break;
								}
								continue;
							}
							throw new NullReferenceException();
						}
						enumerator3.Dispose();
						if (memoryTextures != null)
						{
							if (memoryTextures.TryGetValue(text16, out var value) && value != null)
							{
								UnityEngine.Object.Destroy(value);
							}
							if (memoryTextures != null)
							{
								bool flag13 = memoryTextures.Remove(text16);
								break;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			Dictionary<string, int>.KeyCollection keys2 = zipFrameCounts.Keys;
			List<string> list2 = new List<string>(keys2);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			while (true)
			{
				if (enumerator4.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag14 = hashSet2 == null;
					Dictionary<string, int> dictionary = (Dictionary<string, int>)(&enumerator4);
					if (!flag14)
					{
						if (!hashSet2.Contains(text17))
						{
							if (zipFrameCounts == null)
							{
								break;
							}
							bool flag15 = zipFrameCounts.Remove(text17);
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator4.Dispose();
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	public static void ClearMemoryCache()
	{
		//IL_0018: Expected O, but got I4
		Dictionary<string, int> dictionary = (Dictionary<string, int>)(object)memoryTextures;
		if (memoryTextures != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
			KeyValuePair<string, Texture2D> keyValuePair = (KeyValuePair<string, Texture2D>)0;
			Dictionary<string, Texture2D>.Enumerator enumerator = default(Dictionary<string, Texture2D>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
				if (obj != null)
				{
					Texture2D value = keyValuePair.Value;
					UnityEngine.Object.Destroy(value);
				}
			}
			enumerator.Dispose();
			bool flag = memoryTextures == null;
			dictionary = (Dictionary<string, int>)(object)memoryTextures;
			if (!flag)
			{
				memoryTextures.Clear();
				if (zipFrameCounts != null)
				{
					zipFrameCounts.Clear();
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	public static string GetCacheFile(string url)
	{
		string path = CacheDir;
		DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
		string path2 = CacheDir;
		string text = Hash(url);
		string path3 = text + ".img";
		return Path.Combine(path2, path3);
	}

	private unsafe static Task<byte[]> ReadCachedBytesAsync(string file, TimeSpan maxAge)
	{
		_003C_003Ec__DisplayClass64_0 CS_0024_003C_003E8__locals7 = new _003C_003Ec__DisplayClass64_0();
		if (CS_0024_003C_003E8__locals7 != null)
		{
			CS_0024_003C_003E8__locals7.file = file;
			CS_0024_003C_003E8__locals7.maxAge = maxAge;
			Func<byte[]> function = delegate
			{
				//IL_0013: Expected I, but got O
				//IL_001e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0023: Expected O, but got Unknown
				byte[] result;
				if (File.Exists(CS_0024_003C_003E8__locals7.file))
				{
					nint num = (nint)typeof(TimeSpan);
					TimeSpan timeSpan = (TimeSpan)(CS_0024_003C_003E8__locals7 + 24);
					double totalSeconds = ((TimeSpan*)timeSpan)->TotalSeconds;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm1\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rcx_v4 (Il2CppClass<System.TimeSpan>)+E4]");
					if ((nint)0 > (nint)0)
					{
						DateTime utcNow = DateTime.UtcNow;
						DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(CS_0024_003C_003E8__locals7.file);
						TimeSpan timeSpan2 = utcNow - lastWriteTimeUtc;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E11880");
						object obj = default(object);
						if (obj != null)
						{
							result = null;
							goto IL_0168;
						}
					}
					byte[] array = File.ReadAllBytes(CS_0024_003C_003E8__locals7.file);
					if (array == null)
					{
						return (byte[])(object)new NullReferenceException();
					}
					bool flag = array.Length == 0;
					result = null;
					if (!flag)
					{
						result = array;
					}
				}
				else
				{
					result = null;
				}
				goto IL_0168;
				IL_0168:
				return result;
			};
			return Task.Run(function);
		}
		return (Task<byte[]>)(object)new NullReferenceException();
	}

	private static void WriteCachedBytesAsync(string file, byte[] bytes)
	{
		_003C_003Ec__DisplayClass65_0 CS_0024_003C_003E8__locals5 = new _003C_003Ec__DisplayClass65_0();
		CS_0024_003C_003E8__locals5.file = file;
		CS_0024_003C_003E8__locals5.bytes = bytes;
		Action action = delegate
		{
			string directoryName = Path.GetDirectoryName(CS_0024_003C_003E8__locals5.file);
			if (!string.IsNullOrEmpty(directoryName))
			{
				DirectoryInfo directoryInfo = Directory.CreateDirectory(directoryName);
			}
			File.WriteAllBytes(CS_0024_003C_003E8__locals5.file, CS_0024_003C_003E8__locals5.bytes);
		};
		Task task = Task.Run(action);
	}

	private static void EnqueueMainThreadAction(Action action)
	{
		EnsureRunner();
		mainThreadActions.Enqueue(action);
	}

	private unsafe static bool TryGetTaskResult<T>(Task<T> task, out T result)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0038: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r9_v1 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r9_v1 (Il2CppClass<T>)+FC]");
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			if (task == null)
			{
				goto IL_00ba;
			}
		}
		TaskStatus status = task.Status;
		if (status == TaskStatus.RanToCompletion)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180922A20");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			return true;
		}
		goto IL_00ba;
		IL_00ba:
		return false;
	}

	public static Texture2D CreateTexture(byte[] bytes, bool markNonReadable)
	{
		if (bytes != null && bytes.Length != 0)
		{
			bool mipChain = default(bool);
			Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain);
			if (!ImageConversion.LoadImage(texture2D, bytes, markNonReadable))
			{
				UnityEngine.Object.Destroy(texture2D);
				return null;
			}
			return texture2D;
		}
		return null;
	}

	private static string GetDownloadKey(string url)
	{
		string text = Hash(url);
		return "download:" + text;
	}

	public static string GetUrlKey(string url)
	{
		string text = Hash(url);
		return "url:" + text;
	}

	public static string GetZipFramePrefix(string zipUrl)
	{
		string text = Hash(zipUrl);
		return "zip:" + text + ":frame:";
	}

	public static string GetZipFrameKey(string zipUrl, int index)
	{
		string zipFramePrefix = GetZipFramePrefix(zipUrl);
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		return $"{zipFramePrefix}{arg:0000}";
	}

	public static string GetAvatarKey(string base64)
	{
		string value = NormalizeBase64Payload(base64);
		string text = Hash(value);
		return "avatar:" + text;
	}

	public static string NormalizeBase64Payload(string base64)
	{
		//IL_0149: Expected I4, but got I8
		//IL_0177: Expected O, but got I4
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected I4, but got Unknown
		string text4;
		if (!string.IsNullOrEmpty(base64))
		{
			if (base64 != null)
			{
				int num = base64.IndexOf(',');
				bool flag = num < 0;
				string source = base64;
				if (!flag)
				{
					int num2 = num + 1;
					int length = base64._stringLength - num2;
					string text = base64.Substring(num2, length);
					source = text;
				}
				Func<char, bool> predicate = _003C_003Ec._003C_003E9__74_0;
				if (_003C_003Ec._003C_003E9__74_0 == null)
				{
					predicate = (_003C_003Ec._003C_003E9__74_0 = delegate(char x)
					{
						bool flag2 = char.IsWhiteSpace(x);
						return (byte)((flag2 ? 1u : 0u) ^ 1u) != 0;
					});
				}
				IEnumerable<char> source2 = Enumerable.Where(source, predicate);
				char[] val = Enumerable.ToArray(source2);
				string text2 = ((string)null).CreateString(val);
				if (text2 != null)
				{
					string text3 = text2.Replace('-', '+');
					if (text3 != null)
					{
						text4 = text3.Replace('_', '/');
						if (text4 != null)
						{
							int num3 = (int)(text4._stringLength & 0x80000003L);
							if ((nint)text4 < 0)
							{
								object obj = num3 - 1;
								object obj2 = obj | -4;
								num3 = obj2 + 1;
							}
							switch (num3)
							{
							case 3:
								return text4 + "=";
							case 2:
								return text4 + "==";
							}
							goto IL_0292;
						}
					}
				}
			}
			return (string)(object)new NullReferenceException();
		}
		text4 = base64;
		goto IL_0292;
		IL_0292:
		return text4;
	}

	public static string Hash(string value)
	{
		//IL_00a3: Expected I4, but got I8
		//IL_006c: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AD01]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag = value == null;
		ulong num = 14695981039346656037uL;
		if (!flag)
		{
			ulong num2 = 14695981039346656037uL;
			for (int i = 0; i < value._stringLength; i++)
			{
				char c = value.get_Chars(i);
				int num3 = (int)((long)(int)c ^ (long)num2);
				num2 = (ulong)(num3 * 1099511628211L);
			}
			int num4 = (int)((long)value._stringLength ^ (long)num2);
			ulong num5 = (ulong)(num4 * 1099511628211L);
			num = num5;
		}
		return num.ToString("x16");
	}

	static LeaderboardImageThrottle()
	{
		//IL_00e7: Expected I4, but got I8
		MaxConcurrentDownloads = 2;
		MaxTextureCreatesPerFrame = 1;
		MaxActiveJobs = 4;
		DownloadStartSpacing = 0.15f;
		TextureCreateSpacing = 0.05f;
		Queue<DownloadJob> queue = new Queue<DownloadJob>();
		priorityDownloadJobs = queue;
		Queue<DownloadJob> queue2 = new Queue<DownloadJob>();
		normalDownloadJobs = queue2;
		Queue<TextureJob> queue3 = new Queue<TextureJob>();
		priorityTextureJobs = queue3;
		Queue<TextureJob> queue4 = new Queue<TextureJob>();
		normalTextureJobs = queue4;
		Queue<Action> queue5 = new Queue<Action>();
		mainThreadActions = queue5;
		Dictionary<string, DownloadJob> dictionary = new Dictionary<string, DownloadJob>();
		downloadJobs = dictionary;
		Dictionary<string, TextureJob> dictionary2 = new Dictionary<string, TextureJob>();
		textureJobs = dictionary2;
		Dictionary<string, Texture2D> dictionary3 = new Dictionary<string, Texture2D>();
		memoryTextures = dictionary3;
		Dictionary<string, int> dictionary4 = new Dictionary<string, int>();
		zipFrameCounts = dictionary4;
		lastTextureFrame = -1;
	}
}
