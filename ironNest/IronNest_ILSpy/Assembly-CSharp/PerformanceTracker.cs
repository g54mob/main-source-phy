using System;
using Cpp2ILInjected;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Profiling;

public sealed class PerformanceTracker : MonoBehaviour
{
	[Serializable]
	public sealed class Snapshot
	{
		public float uptime;

		public int frames;

		public float fpsAvg;

		public float fpsMin;

		public float fpsMax;

		public float frameMsAvg;

		public float frameMsMax;

		public int hitchCount;

		public int gc0;

		public int gc1;

		public int gc2;

		public long allocMb;

		public long reservedMb;

		public long unusedReservedMb;

		public long monoUsedMb;

		public long monoHeapMb;

		public int renderersTracked;

		public int renderersVisible;

		public int renderersHidden;

		public int cullingMask;

		public string deviceModel;

		public string gpu;

		public string os;

		public int ramMb;

		public int vramMb;

		public int screenW;

		public int screenH;

		public int quality;

		public string appVersion;
	}

	public static PerformanceTracker Instance;

	private float hitchMs = 120f;

	private float rendererSampleInterval = 2f;

	private bool sampleRenderers = true;

	private Renderer[] trackedRenderers;

	private CullLayerCache cullingCache;

	private int frames;

	private int hitchCount;

	private float timeSum;

	private float fpsSum;

	private float fpsMin = 3.4028235E+38f;

	private float fpsMax;

	private float frameMsMax;

	private float rendererTimer;

	private int visibleRenderers;

	private int hiddenRenderers;

	private int startGc0;

	private int startGc1;

	private int startGc2;

	private void Awake()
	{
		Instance = this;
		int num = GC.CollectionCount(0);
		startGc0 = num;
		int num2 = GC.CollectionCount(1);
		startGc1 = num2;
		int num3 = GC.CollectionCount(2);
		bool flag = trackedRenderers == null;
		startGc2 = num3;
		if (!flag)
		{
			Renderer[] array = trackedRenderers;
			if (array.Length != 0)
			{
				return;
			}
		}
		if (sampleRenderers)
		{
			Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>(includeInactive: true);
			trackedRenderers = componentsInChildren;
		}
	}

	private void Update()
	{
		//IL_0012: Invalid comparison between I4 and F4
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		if (!(0f < unscaledDeltaTime))
		{
			return;
		}
		float num = unscaledDeltaTime + timeSum;
		int num2 = frames + 1;
		frames = num2;
		float num3 = unscaledDeltaTime * 1000f;
		float num4 = 1f / unscaledDeltaTime;
		timeSum = num;
		float num5 = num4 + fpsSum;
		fpsSum = num5;
		if (fpsMin > num4)
		{
			fpsMin = num4;
		}
		if (num4 > fpsMax)
		{
			fpsMax = num4;
		}
		if (num3 > frameMsMax)
		{
			frameMsMax = num3;
		}
		if (!(num3 < hitchMs))
		{
			int num6 = hitchCount + 1;
			hitchCount = num6;
		}
		if (sampleRenderers && trackedRenderers != null)
		{
			float num7 = (rendererTimer = unscaledDeltaTime + rendererTimer);
			if (!(rendererSampleInterval > num7))
			{
				rendererTimer = 0f;
				SampleRenderers();
			}
		}
	}

	public Snapshot Capture(bool resetAfterCapture = false, bool includeDeviceInfo = false)
	{
		//IL_0081: Expected F4, but got I4
		//IL_06d2: Expected F4, but got I4
		//IL_008f: Expected F4, but got I4
		//IL_009f: Invalid comparison between F4 and I4
		//IL_00b1: Expected F4, but got I4
		//IL_01a1: Expected O, but got I8
		//IL_01af: Expected I8, but got O
		//IL_01ee: Expected O, but got I8
		//IL_01fc: Expected I8, but got O
		//IL_0240: Expected O, but got I8
		//IL_024e: Expected I8, but got O
		//IL_028d: Expected O, but got I8
		//IL_029b: Expected I8, but got O
		//IL_02df: Expected O, but got I8
		//IL_02ed: Expected I8, but got O
		Snapshot snapshot = new Snapshot();
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		int cullingMask;
		if (snapshot != null)
		{
			snapshot.uptime = realtimeSinceStartup;
			snapshot.frames = frames;
			float fpsAvg = ((frames <= 0) ? 0f : (fpsSum / (float)frames));
			snapshot.fpsAvg = fpsAvg;
			float num = fpsMin;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018049CAFDh\"");
			if (fpsMin == 3.4028235E+38f)
			{
				num = 0f;
			}
			snapshot.fpsMin = num;
			snapshot.fpsMax = fpsMax;
			bool flag = frames <= 0;
			float frameMsAvg = 0f;
			if (!flag)
			{
				bool flag2 = !(timeSum > 0f);
				frameMsAvg = 0f;
				if (!flag2)
				{
					float num2 = timeSum / (float)frames;
					frameMsAvg = num2 * 1000f;
				}
			}
			snapshot.frameMsAvg = frameMsAvg;
			snapshot.frameMsMax = frameMsMax;
			snapshot.hitchCount = hitchCount;
			int num3 = GC.CollectionCount(0);
			int gc = num3 - startGc0;
			snapshot.gc0 = gc;
			int num4 = GC.CollectionCount(1);
			int gc2 = num4 - startGc1;
			snapshot.gc1 = gc2;
			int num5 = GC.CollectionCount(2);
			int gc3 = num5 - startGc2;
			snapshot.gc2 = gc3;
			long totalAllocatedMemoryLong = Profiler.GetTotalAllocatedMemoryLong();
			long num6 = totalAllocatedMemoryLong >> 63;
			long num7 = num6 & 0xFFFFF;
			object obj = totalAllocatedMemoryLong + num7;
			long allocMb = obj >> 20;
			snapshot.allocMb = allocMb;
			long totalReservedMemoryLong = Profiler.GetTotalReservedMemoryLong();
			long num8 = totalReservedMemoryLong >> 63;
			long num9 = num8 & 0xFFFFF;
			object obj2 = totalReservedMemoryLong + num9;
			long reservedMb = obj2 >> 20;
			snapshot.reservedMb = reservedMb;
			long totalUnusedReservedMemoryLong = Profiler.GetTotalUnusedReservedMemoryLong();
			long num10 = totalUnusedReservedMemoryLong >> 63;
			long num11 = num10 & 0xFFFFF;
			object obj3 = totalUnusedReservedMemoryLong + num11;
			long unusedReservedMb = obj3 >> 20;
			snapshot.unusedReservedMb = unusedReservedMb;
			long monoUsedSizeLong = Profiler.GetMonoUsedSizeLong();
			long num12 = monoUsedSizeLong >> 63;
			long num13 = num12 & 0xFFFFF;
			object obj4 = monoUsedSizeLong + num13;
			long monoUsedMb = obj4 >> 20;
			snapshot.monoUsedMb = monoUsedMb;
			long monoHeapSizeLong = Profiler.GetMonoHeapSizeLong();
			long num14 = monoHeapSizeLong >> 63;
			long num15 = num14 & 0xFFFFF;
			object obj5 = monoHeapSizeLong + num15;
			long monoHeapMb = obj5 >> 20;
			snapshot.monoHeapMb = monoHeapMb;
			int renderersTracked;
			if (trackedRenderers == null)
			{
				renderersTracked = 0;
			}
			else
			{
				Renderer[] array = trackedRenderers;
				renderersTracked = array.Length;
			}
			snapshot.renderersTracked = renderersTracked;
			snapshot.renderersVisible = visibleRenderers;
			snapshot.renderersHidden = hiddenRenderers;
			CullLayerCache instance;
			if (cullingCache == null)
			{
				if (CullLayerCache.Instance == null)
				{
					cullingMask = 0;
					goto IL_03d6;
				}
				instance = CullLayerCache.Instance;
			}
			else
			{
				instance = cullingCache;
			}
			if ((object)instance != null)
			{
				cullingMask = instance.ReadCullMask();
				goto IL_03d6;
			}
		}
		return (Snapshot)(object)new NullReferenceException();
		IL_03d6:
		snapshot.cullingMask = cullingMask;
		string os;
		if (includeDeviceInfo)
		{
			string deviceModel = SystemInfo.deviceModel;
			snapshot.deviceModel = deviceModel;
			string graphicsDeviceName = SystemInfo.graphicsDeviceName;
			snapshot.gpu = graphicsDeviceName;
			os = SystemInfo.operatingSystem;
		}
		else
		{
			snapshot.deviceModel = null;
			snapshot.gpu = null;
			os = null;
		}
		snapshot.os = os;
		int systemMemorySize = SystemInfo.systemMemorySize;
		snapshot.ramMb = systemMemorySize;
		int graphicsMemorySize = SystemInfo.graphicsMemorySize;
		snapshot.vramMb = graphicsMemorySize;
		int width = Screen.width;
		snapshot.screenW = width;
		int height = Screen.height;
		snapshot.screenH = height;
		int qualityLevel = QualitySettings.GetQualityLevel();
		snapshot.quality = qualityLevel;
		string version = Application.version;
		snapshot.appVersion = version;
		if (resetAfterCapture)
		{
			frames = 0;
			timeSum = 0f;
			fpsMin = 3.4028235E+38f;
			frameMsMax = 0f;
			int num16 = GC.CollectionCount(0);
			startGc0 = num16;
			int num17 = GC.CollectionCount(1);
			startGc1 = num17;
			int num18 = GC.CollectionCount(2);
			startGc2 = num18;
		}
		return snapshot;
	}

	public string CaptureJson(bool resetAfterCapture = false, bool includeDeviceInfo = false)
	{
		Snapshot value = Capture(resetAfterCapture, includeDeviceInfo);
		return JsonConvert.SerializeObject(value);
	}

	public void SetTrackedRenderers(Renderer[] renderers)
	{
		trackedRenderers = renderers;
		SampleRenderers();
	}

	public void ResetCounters()
	{
		fpsMin = 3.4028235E+38f;
		frames = 0;
		timeSum = 0f;
		frameMsMax = 0f;
		int num = GC.CollectionCount(0);
		startGc0 = num;
		int num2 = GC.CollectionCount(1);
		startGc1 = num2;
		int num3 = GC.CollectionCount(2);
		startGc2 = num3;
	}

	private void SampleRenderers()
	{
		//IL_0032: Expected O, but got I4
		//IL_005d: Expected O, but got I
		//IL_0080: Expected O, but got I
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_00ae: Expected O, but got I
		//IL_00ee: Expected O, but got I
		Renderer[] array = trackedRenderers;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		object obj = 32;
		while (num4 < array.Length)
		{
			Renderer[] array2 = trackedRenderers;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ r14_v4+v71 @ rbx_v5 (UnityEngine.Renderer[])]");
			if ((UnityEngine.Object)0 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ r14_v4+v71 @ rbx_v5 (UnityEngine.Renderer[])]");
				if (((Renderer)0).enabled)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ r14_v4+v71 @ rbx_v5 (UnityEngine.Renderer[])]");
					GameObject gameObject = ((Component)0).gameObject;
					if (gameObject.activeInHierarchy)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ r14_v4+v71 @ rbx_v5 (UnityEngine.Renderer[])]");
						if (!((Renderer)0).isVisible)
						{
							num3++;
						}
						else
						{
							num2++;
						}
					}
				}
			}
			array = trackedRenderers;
			num++;
			obj += 8;
			num4 = num;
		}
		visibleRenderers = num2;
		hiddenRenderers = num3;
	}
}
