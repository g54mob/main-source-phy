using System;
using UnityEngine;

[DisallowMultipleComponent]
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

	[Header("Sampling")]
	[SerializeField]
	private float hitchMs;

	[SerializeField]
	private float rendererSampleInterval;

	[SerializeField]
	private bool sampleRenderers;

	[Header("Optional")]
	[SerializeField]
	private Renderer[] trackedRenderers;

	[SerializeField]
	private CullLayerCache cullingCache;

	private int frames;

	private int hitchCount;

	private float timeSum;

	private float fpsSum;

	private float fpsMin;

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
	}

	private void Update()
	{
	}

	public Snapshot Capture(bool resetAfterCapture = false, bool includeDeviceInfo = false)
	{
		return null;
	}

	public string CaptureJson(bool resetAfterCapture = false, bool includeDeviceInfo = false)
	{
		return null;
	}

	public void SetTrackedRenderers(Renderer[] renderers)
	{
	}

	public void ResetCounters()
	{
	}

	private void SampleRenderers()
	{
	}
}
