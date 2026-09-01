using System;
using System.IO;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

namespace AsyncGPUReadbackPluginNs
{
	public class AsyncGPUReadbackPluginRequest
	{
		private bool usePlugin;

		private AsyncGPUReadbackRequest gpuRequest;

		private int eventId;

		private bool bufferCreated;

		public bool done
		{
			get
			{
				if (usePlugin)
				{
					return isRequestDone(eventId);
				}
				return gpuRequest.done;
			}
		}

		public bool hasError
		{
			get
			{
				if (usePlugin)
				{
					return isRequestError(eventId);
				}
				return gpuRequest.hasError;
			}
		}

		public AsyncGPUReadbackPluginRequest(Texture src)
		{
			if (SystemInfo.supportsAsyncGPUReadback)
			{
				usePlugin = false;
				gpuRequest = AsyncGPUReadback.Request(src, 0, TextureFormat.RGB24);
			}
			else if (isCompatible())
			{
				usePlugin = true;
				int texture = (int)src.GetNativeTexturePtr();
				eventId = makeRequest_mainThread(texture, 0);
				GL.IssuePluginEvent(getfunction_makeRequest_renderThread(), eventId);
			}
			else
			{
				Debug.LogError("AsyncGPUReadback is not supported on your system.");
			}
		}

		public unsafe void GetRawData(byte[] output)
		{
			if (usePlugin)
			{
				void* buffer = null;
				int length = 0;
				getData_mainThread(eventId, ref buffer, ref length);
				Marshal.Copy(new IntPtr(buffer), output, 0, output.Length);
				bufferCreated = true;
			}
			else
			{
				byte* unsafeReadOnlyPtr = (byte*)gpuRequest.GetData<byte>().GetUnsafeReadOnlyPtr();
				new UnmanagedMemoryStream(unsafeReadOnlyPtr, output.Length).Read(output, 0, output.Length);
			}
		}

		public void Update(bool force = false)
		{
			if (usePlugin)
			{
				GL.IssuePluginEvent(getfunction_update_renderThread(), eventId);
			}
			else if (force)
			{
				gpuRequest.Update();
			}
		}

		public void Dispose()
		{
			if (usePlugin && bufferCreated)
			{
				dispose(eventId);
			}
		}

		[DllImport("AsyncGPUReadbackPlugin")]
		private static extern bool isCompatible();

		[DllImport("AsyncGPUReadbackPlugin")]
		private static extern int makeRequest_mainThread(int texture, int miplevel);

		[DllImport("AsyncGPUReadbackPlugin")]
		private static extern IntPtr getfunction_makeRequest_renderThread();

		[DllImport("AsyncGPUReadbackPlugin")]
		private static extern void makeRequest_renderThread(int event_id);

		[DllImport("AsyncGPUReadbackPlugin")]
		private static extern IntPtr getfunction_update_renderThread();

		[DllImport("AsyncGPUReadbackPlugin")]
		private unsafe static extern void getData_mainThread(int event_id, ref void* buffer, ref int length);

		[DllImport("AsyncGPUReadbackPlugin")]
		private static extern bool isRequestError(int event_id);

		[DllImport("AsyncGPUReadbackPlugin")]
		private static extern bool isRequestDone(int event_id);

		[DllImport("AsyncGPUReadbackPlugin")]
		private static extern void dispose(int event_id);
	}
}
