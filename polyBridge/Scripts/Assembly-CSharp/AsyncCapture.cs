using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Poly.Base;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class AsyncCapture : MonoBehaviour
{
	private struct WriteToFileTask
	{
		public byte[] buffer;

		public string filename;
	}

	private class ThreadInfo
	{
		public Thread thread;

		public int threadIdx;

		public bool isFinished;

		public bool done;

		public bool abortTasks;
	}

	[Header("Threaded Write to Disk")]
	public bool useThreadToWriteFiles;

	[Tooltip("When this number is exceeded, file writing defaults back to synchronized processing in the main thread.")]
	public int maxNumQueuedTasks = 32;

	public bool debugLogThreading;

	private const int threadJoinTimeout = 1000;

	private const int threadCleanupJoinTimeout = 1;

	[Header("Quality Presets")]
	public AsyncCaptureQualityPreset m_QualityLow;

	public AsyncCaptureQualityPreset m_QualityMedium;

	public AsyncCaptureQualityPreset m_QualityHigh;

	public AsyncCaptureQualityPreset m_QualityUltra;

	[NonSerialized]
	public AsyncCaptureQuality m_AsyncCaptureQuality;

	[NonSerialized]
	public bool m_IsRecording;

	[NonSerialized]
	public int m_StartIndex;

	[NonSerialized]
	public int m_NumFrames;

	[NonSerialized]
	public int m_MaxSeconds;

	[NonSerialized]
	public int m_MaxFrames;

	[NonSerialized]
	public float m_ElapsedTime;

	[NonSerialized]
	public bool m_Initialized;

	private AsyncCaptureQualityPreset m_AsyncCaptureQualityPreset;

	private Queue<AsyncGPUReadbackRequest> m_Requests;

	private RenderTexture m_RenderTexture;

	private byte[] m_WriteBuffer;

	private float m_LastSnapshotTime;

	private int m_CurrentFrame;

	private Camera m_Camera;

	private bool m_Wrapping;

	private readonly string REPLAY_FRAMES = "ReplayFrames";

	private string m_ReplayFramesPath;

	private string m_ReplayFrameInBuffer;

	private static DateTime startTime;

	private static int frameCount;

	private ConcurrentPool<byte[]> bufferPool;

	private ConcurrentQueue<WriteToFileTask> taskQueue = new ConcurrentQueue<WriteToFileTask>();

	private ThreadInfo fileWritingThread;

	private static int numThreadsCreated;

	private static List<ThreadInfo> threadInfos = new List<ThreadInfo>();

	public int width => m_AsyncCaptureQualityPreset.m_Width;

	public int height => m_AsyncCaptureQualityPreset.m_Height;

	public int framerate => m_AsyncCaptureQualityPreset.m_Framerate;

	private AsyncCaptureQualityPreset GetPresetForQuality(AsyncCaptureQuality quality)
	{
		switch (quality)
		{
		case AsyncCaptureQuality.LOW:
			return m_QualityLow;
		case AsyncCaptureQuality.MEDIUM:
			return m_QualityMedium;
		case AsyncCaptureQuality.HIGH:
			return m_QualityHigh;
		case AsyncCaptureQuality.ULTRA:
			return m_QualityUltra;
		default:
			UnityEngine.Debug.LogWarningFormat("Unrecognized Async Capture Quality: {0}", quality.ToString());
			return null;
		}
	}

	private void Awake()
	{
		m_Requests = new Queue<AsyncGPUReadbackRequest>();
		m_Camera = GetComponent<Camera>();
		startTime = DateTime.Now;
	}

	private void OnDestroy()
	{
		Async_CompleteAllWriteToFileJobs();
		bufferPool?.Dispose();
	}

	public void Init(AsyncCaptureQuality quality, int maxSeconds)
	{
		m_AsyncCaptureQuality = quality;
		m_AsyncCaptureQualityPreset = GetPresetForQuality(quality);
		m_MaxSeconds = maxSeconds;
		m_MaxFrames = m_MaxSeconds * framerate;
		if (m_RenderTexture != null)
		{
			m_RenderTexture.Release();
		}
		m_RenderTexture = new RenderTexture(width, height, 8);
		m_RenderTexture.antiAliasing = 4;
		m_Camera.targetTexture = m_RenderTexture;
		m_ElapsedTime = 0f;
		try
		{
			int byteSizeForEachFrame = width * height * 3;
			m_WriteBuffer = new byte[byteSizeForEachFrame];
			Async_CompleteAllWriteToFileJobs();
			bufferPool?.Dispose();
			bufferPool = new ConcurrentPool<byte[]>(() => new byte[byteSizeForEachFrame]);
			m_Initialized = true;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogWarningFormat("Caught exception when allocating frames for replay recording: {0}", ex.Message.ToString());
			m_Initialized = false;
		}
		m_ReplayFramesPath = Path.Combine(Application.persistentDataPath, REPLAY_FRAMES);
		Utils.CreateDirectory(m_ReplayFramesPath);
	}

	private void Update()
	{
		frameCount++;
		while (m_Requests.Count > 0)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = m_Requests.Peek();
			asyncGPUReadbackRequest.Update();
			if (asyncGPUReadbackRequest.hasError)
			{
				m_Requests.Dequeue();
				continue;
			}
			if (!asyncGPUReadbackRequest.done)
			{
				break;
			}
			_ = Time.realtimeSinceStartup;
			NativeArray<Color32> data = asyncGPUReadbackRequest.GetData<Color32>();
			int layerDataSize = asyncGPUReadbackRequest.layerDataSize;
			if (useThreadToWriteFiles)
			{
				if (taskQueue.Count < maxNumQueuedTasks)
				{
					_ = m_WriteBuffer.Length;
					Async_SaveBytestream(data);
					IncreaseIndex();
				}
				else
				{
					UnityEngine.Debug.Log($"Async-Capture: Frame-export queue full, skipping frame; num current tasks {taskQueue.Count}");
				}
			}
			else
			{
				SaveBytestream(data, layerDataSize);
				IncreaseIndex();
			}
			m_Requests.Dequeue();
		}
	}

	public void DoPostRender()
	{
		if (!m_IsRecording || Mathf.Approximately(Time.timeScale, 0f))
		{
			return;
		}
		m_ElapsedTime += Time.unscaledDeltaTime;
		float num = 1f / (float)framerate;
		if (m_ElapsedTime > num)
		{
			if (m_Requests.Count < 8)
			{
				m_Requests.Enqueue(AsyncGPUReadback.Request(m_Camera.targetTexture, 0, TextureFormat.RGB24));
			}
			else
			{
				UnityEngine.Debug.LogWarningFormat("Too many requests.");
			}
			m_LastSnapshotTime = Time.realtimeSinceStartup;
			m_ElapsedTime -= num;
		}
	}

	public void Reset()
	{
		m_CurrentFrame = 0;
		m_StartIndex = 0;
		m_NumFrames = 0;
		m_ElapsedTime = 0f;
		m_Wrapping = false;
		m_Requests.Clear();
	}

	private void SaveBytestream(NativeArray<Color32> buffer, int unused_dataSize)
	{
		buffer.Reinterpret<byte>(4).CopyTo(m_WriteBuffer);
		string replayFrameFullPath = GetReplayFrameFullPath(m_CurrentFrame);
		File.WriteAllBytes(replayFrameFullPath, m_WriteBuffer);
		m_ReplayFrameInBuffer = replayFrameFullPath;
	}

	private void IncreaseIndex()
	{
		m_CurrentFrame++;
		m_NumFrames++;
		int num = framerate * m_MaxSeconds;
		if (m_NumFrames > num)
		{
			m_NumFrames = num;
		}
		if (m_CurrentFrame >= num)
		{
			m_CurrentFrame = 0;
			m_Wrapping = true;
		}
		if (m_Wrapping)
		{
			m_StartIndex++;
			if (m_StartIndex >= num)
			{
				m_StartIndex = 0;
			}
		}
	}

	public byte[] GetFrame(int index)
	{
		string replayFrameFullPath = GetReplayFrameFullPath(index);
		if (replayFrameFullPath == m_ReplayFrameInBuffer)
		{
			return m_WriteBuffer;
		}
		try
		{
			m_ReplayFrameInBuffer = replayFrameFullPath;
			m_WriteBuffer = File.ReadAllBytes(replayFrameFullPath);
			return m_WriteBuffer;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogWarning("Caught exception trying to read replay frame: " + ex.Message);
			return null;
		}
	}

	public byte[] GetFirstFrame()
	{
		string replayFrameFullPath = GetReplayFrameFullPath(m_StartIndex);
		if (replayFrameFullPath == m_ReplayFrameInBuffer)
		{
			return m_WriteBuffer;
		}
		try
		{
			m_ReplayFrameInBuffer = replayFrameFullPath;
			m_WriteBuffer = File.ReadAllBytes(replayFrameFullPath);
			return m_WriteBuffer;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogWarning("Caught exception trying to read replay frame: " + ex.Message);
			return null;
		}
	}

	public string GetReplayFramesFullPath()
	{
		return m_ReplayFramesPath;
	}

	public string GetReplayFrameFullPath(int index)
	{
		return Path.Combine(m_ReplayFramesPath, index.ToString("D5"));
	}

	public void DeleteAllReplayFrames()
	{
		Utils.DeleteAllFilesInDirectory(m_ReplayFramesPath);
	}

	private void Async_SaveBytestream(NativeArray<Color32> buffer)
	{
		WriteToFileTask item = new WriteToFileTask
		{
			buffer = bufferPool.Get()
		};
		buffer.Reinterpret<byte>(4).CopyTo(item.buffer);
		item.filename = GetReplayFrameFullPath(m_CurrentFrame);
		taskQueue.Enqueue(item);
		if (fileWritingThread != null && fileWritingThread.thread.IsAlive)
		{
			return;
		}
		if (fileWritingThread != null)
		{
			_ = fileWritingThread.isFinished;
			if (fileWritingThread.thread.Join(1000))
			{
				threadInfos.Remove(fileWritingThread);
			}
			fileWritingThread = null;
		}
		fileWritingThread = new ThreadInfo();
		fileWritingThread.thread = new Thread(Async_ProcessWriteToFileTasks);
		fileWritingThread.thread.Name = "SAVE Write Capture Frames to Disk";
		fileWritingThread.threadIdx = numThreadsCreated++;
		threadInfos.Add(fileWritingThread);
		fileWritingThread.thread.Start(fileWritingThread);
	}

	public void Async_ProcessWriteToFileTasks(object data)
	{
		ThreadInfo threadInfo = (ThreadInfo)data;
		_ = threadInfo.threadIdx;
		int num = 0;
		int num2 = 0;
		while ((!threadInfo.done || !taskQueue.IsEmpty) && !threadInfo.abortTasks && num < 10)
		{
			if (!threadInfo.done && !threadInfo.abortTasks && taskQueue.IsEmpty)
			{
				try
				{
					Thread.Sleep(30);
					num++;
				}
				catch (ThreadInterruptedException)
				{
				}
			}
			int num3 = 0;
			WriteToFileTask result;
			while (!threadInfo.abortTasks && taskQueue.TryDequeue(out result))
			{
				num = 0;
				num2++;
				try
				{
					File.WriteAllBytes(result.filename, result.buffer);
				}
				catch (Exception)
				{
				}
				bufferPool.Release(result.buffer);
				num3++;
			}
		}
		threadInfo.isFinished = true;
	}

	public bool Aysnc_CaptureStillHasWorkToDo()
	{
		if (taskQueue.Count > 0)
		{
			return true;
		}
		if (threadInfos.Count > 0 && threadInfos[0].thread.IsAlive)
		{
			return true;
		}
		return false;
	}

	private string JoinFinishedThreadsAndGetListOfAliveThreads()
	{
		string text = "";
		int num = threadInfos.Count - 1;
		while (0 <= num)
		{
			if (threadInfos[num].isFinished)
			{
				if (threadInfos[num].thread.Join(1))
				{
					threadInfos.RemoveAt(num);
				}
			}
			else
			{
				text = text + threadInfos[num].threadIdx + " ";
			}
			num--;
		}
		return text;
	}

	public void Async_CompleteAllWriteToFileJobs()
	{
		if (fileWritingThread != null)
		{
			fileWritingThread.done = true;
			if (fileWritingThread.thread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
			{
				fileWritingThread.thread.Interrupt();
			}
			bool flag = fileWritingThread.thread.Join(1000);
			if (!flag)
			{
				fileWritingThread.abortTasks = true;
				UnityEngine.Debug.LogWarning("CMPL Joining thread, emergency (failed to join normally)");
				UnityEngine.Debug.Log("Async-Capture: Joining thread failed, emergency (failed to join normally)");
				flag = fileWritingThread.thread.Join(2000);
				if (!flag)
				{
					UnityEngine.Debug.Log($"Async-Capture: Thread failed to emergency-join. Num tasks left: {taskQueue.Count}");
				}
				WriteToFileTask result;
				while (taskQueue.TryDequeue(out result))
				{
					bufferPool.Release(result.buffer);
				}
			}
			if (flag)
			{
				threadInfos.Remove(fileWritingThread);
			}
			fileWritingThread = null;
			if (!(JoinFinishedThreadsAndGetListOfAliveThreads() != ""))
			{
			}
		}
		else
		{
			JoinFinishedThreadsAndGetListOfAliveThreads();
		}
		_ = taskQueue.IsEmpty;
	}

	[Conditional("NEVER_ACTIVE__XX")]
	private void DLog(string message)
	{
		if (debugLogThreading)
		{
			TimeSpan timeSpan = DateTime.Now - startTime;
			UnityEngine.Debug.Log($"({frameCount,5}){(float)timeSpan.TotalSeconds,8:###0.000} {message}");
		}
	}
}
