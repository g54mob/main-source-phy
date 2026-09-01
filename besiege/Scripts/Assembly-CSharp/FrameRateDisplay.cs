using System.Collections.Generic;
using UnityEngine;

public class FrameRateDisplay : MonoBehaviour
{
	public DynamicText text;

	public DynamicText lowFPS;

	public LineRenderer graph;

	public float height = 1f;

	public float width = 1f;

	public float duration = 3f;

	private PerformanceAnalyser perfAnalyser;

	private Queue<float> frameQueue = new Queue<float>();

	private float queueTime;

	private int currentLock = -1;

	private float wait = 0.1f;

	private float rate = 0.5f;

	private float t;

	private float graphLength = 0.9f;

	private float heightTo60 = 21.78f;

	private float min = 60f;

	private float max = 60f;

	private float FPS = 60f;

	private bool destroying;

	protected void Start()
	{
		perfAnalyser = SingleInstance<PerformanceAnalyser>.Instance;
	}

	protected void OnEnable()
	{
		t = 0f - wait;
		frameQueue.Clear();
		queueTime = 0f;
	}

	protected void LateUpdate()
	{
		if (StatMaster.isHeadless || destroying)
		{
			return;
		}
		if (t < 0f)
		{
			t += Time.unscaledDeltaTime;
			return;
		}
		FPS = RollingFPS();
		if (FPS == 0f)
		{
			FPS = perfAnalyser.UncappedFPS;
		}
		if (t > rate)
		{
			t = 0f;
			ReferenceMaster.SetDynamicText(text, FPS.ToString("f2"));
			if ((bool)lowFPS)
			{
				ReferenceMaster.SetDynamicText(lowFPS, (min * 1000f).ToString("f0") + "⇢" + (max * 1000f).ToString("f0") + "ms");
			}
		}
		t += Time.unscaledDeltaTime;
		if ((bool)graph && graph.gameObject.activeInHierarchy)
		{
			DrawGraph();
		}
	}

	protected float RollingFPS()
	{
		int fPSLock = OptionsMaster.GetFPSLock();
		if (fPSLock != currentLock)
		{
			currentLock = fPSLock;
			frameQueue.Clear();
			queueTime = 0f;
		}
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		frameQueue.Enqueue(unscaledDeltaTime);
		queueTime += unscaledDeltaTime;
		if (queueTime > duration)
		{
			queueTime -= frameQueue.Dequeue();
		}
		float num = queueTime / (1f * (float)frameQueue.Count);
		return 1f / num;
	}

	private float GetLockDelta()
	{
		if ((float)currentLock <= 0f)
		{
			return 0.001f;
		}
		return 1f / (1f * (float)currentLock);
	}

	private void OnApplicationQuit()
	{
		destroying = true;
	}

	private void DrawGraph()
	{
		int num = 0;
		int count = frameQueue.Count;
		Vector3[] array = new Vector3[count];
		Vector3 vector = Vector3.right * (width * graphLength) / (1f * (float)count);
		min = float.MaxValue;
		max = 0f;
		foreach (float item in frameQueue)
		{
			float num2 = item;
			if (num2 > max)
			{
				max = num2;
			}
			if (num2 < min)
			{
				min = num2;
			}
			float num3 = num2 * heightTo60 * height;
			if (num3 > 0.95f)
			{
				num3 = 0.95f;
			}
			array[num] = Vector3.up * num3 + num * vector;
			num++;
		}
		graph.SetVertexCount(count);
		graph.SetPositions(array);
		Vector3 localPosition = lowFPS.transform.localPosition;
		if (array[array.Length - 1].y > 0.6f)
		{
			localPosition.y = -0.1212112f;
		}
		else if (array[array.Length - 1].y < 0.4f)
		{
			localPosition.y = 0.2412128f;
		}
		lowFPS.transform.localPosition = localPosition;
	}

	private Vector3[] InterpolateArray(Vector3[] pos)
	{
		int num = 4;
		int count = frameQueue.Count;
		count *= num;
		Vector3[] array = new Vector3[count];
		for (int i = 0; i < pos.Length; i++)
		{
			if (i == pos.Length - 1)
			{
				array[i * num] = pos[i];
				break;
			}
			Vector2 vector = pos[i];
			Vector2 vector2 = pos[i + 1];
			for (int j = 0; j < num; j++)
			{
				float num2 = (float)j / (1f * (float)num);
				Vector3 vector3 = pos[i];
				vector3.x = Mathf.Lerp(vector.x, vector2.x, num2);
				vector3.y = Mathf.Lerp(vector.y, vector2.y, num2);
				array[i * num + j] = vector3;
			}
		}
		return array;
	}
}
