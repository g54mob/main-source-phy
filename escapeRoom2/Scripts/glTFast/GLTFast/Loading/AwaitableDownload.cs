using System;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine.Networking;

namespace GLTFast.Loading
{
	public class AwaitableDownload : IDownload, IDisposable, INativeDownload
	{
		private const string k_MimeTypeGltfBinary = "model/gltf-binary";

		private const string k_MimeTypeGltf = "model/gltf+json";

		protected UnityWebRequest m_Request;

		protected UnityWebRequestAsyncOperation m_AsyncOperation;

		public bool Success
		{
			get
			{
				if (m_Request != null && m_Request.isDone)
				{
					return m_Request.result == UnityWebRequest.Result.Success;
				}
				return false;
			}
		}

		public string Error
		{
			get
			{
				if (m_Request != null)
				{
					return m_Request.error;
				}
				return "Request disposed";
			}
		}

		public byte[] Data => m_Request?.downloadHandler.data;

		public NativeArray<byte>.ReadOnly NativeData => m_Request?.downloadHandler.nativeData ?? default(NativeArray<byte>.ReadOnly);

		public string Text => m_Request?.downloadHandler.text;

		public bool? IsBinary
		{
			get
			{
				if (Success)
				{
					string responseHeader = m_Request.GetResponseHeader("Content-Type");
					if (responseHeader == "model/gltf-binary")
					{
						return true;
					}
					if (responseHeader == "model/gltf+json")
					{
						return false;
					}
				}
				return GltfGlobals.IsGltfBinary(NativeData);
			}
		}

		protected AwaitableDownload()
		{
		}

		public AwaitableDownload(Uri url)
		{
			Init(url);
		}

		private void Init(Uri url)
		{
			m_Request = UnityWebRequest.Get(url);
			m_AsyncOperation = m_Request.SendWebRequest();
		}

		public async Task WaitAsync()
		{
			while (!m_AsyncOperation.isDone)
			{
				await Task.Yield();
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				m_Request.Dispose();
				m_Request = null;
			}
		}
	}
}
