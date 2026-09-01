using UnityEngine;

public class NetworkInterpolation
{
	public bool isActive;

	public Vector3 Vector;

	public Quaternion Rotation;

	public bool newData;

	public Vector3 lastVec;

	public Vector3 prevVec;

	public Quaternion lastRot;

	public Quaternion prevRot;

	private static float MIN_ROT_THRESHOLD = 0.99999f;

	private static float MAX_ROT_THRESHOLD = 0.999f;

	private static float ROT_THRESHOLD = MIN_ROT_THRESHOLD;

	private static float MIN_VEC_THRESHOLD = 0.001f;

	private static float MAX_VEC_THRESHOLD = 0.1f;

	private static float VEC_THRESHOLD = MIN_VEC_THRESHOLD;

	private float currentDelta;

	private bool updatedAlpha;

	private float baseInterval;

	private float interval;

	private float MAX_ALPHA = 1.5f;

	private float maxAlpha;

	private float maxDelta;

	private float alpha;

	private bool hasVec;

	private bool hasRot;

	private static float acosOne = Mathf.Acos(1f);

	private bool reverseQuat;

	private float halfTheta;

	private float sinHalfTheta;

	private bool lerpQuat;

	private Quaternion halfQuat = default(Quaternion);

	private Vector3 deltaVec;

	private bool usePrediction;

	private bool lerpVec;

	public Vector3 NormalizedDeltaVector
	{
		get
		{
			return (!isActive) ? Vector3.zero : ((lastVec - prevVec) / interval);
		}
	}

	public Vector3 AngularVelocity
	{
		get
		{
			if (!isActive)
			{
				return Vector3.zero;
			}
			return (prevRot * Quaternion.Inverse(lastRot)).eulerAngles / interval;
		}
	}

	public float AngularVelocityMag
	{
		get
		{
			if (!isActive)
			{
				return 0f;
			}
			float angle;
			Vector3 axis;
			(prevRot * Quaternion.Inverse(lastRot)).ToAngleAxis(out angle, out axis);
			return angle;
		}
	}

	public NetworkInterpolation()
	{
		maxAlpha = MAX_ALPHA;
		halfQuat = default(Quaternion);
		usePrediction = true;
	}

	public static void AdjustThreshold(float vecThreshold, float rotThreshold)
	{
		VEC_THRESHOLD = MIN_VEC_THRESHOLD + (MAX_VEC_THRESHOLD - MIN_VEC_THRESHOLD) * vecThreshold;
		ROT_THRESHOLD = MIN_ROT_THRESHOLD + (MAX_ROT_THRESHOLD - MIN_ROT_THRESHOLD) * rotThreshold;
	}

	public void SetPrediction(bool toggle)
	{
		maxAlpha = ((!toggle) ? 1f : MAX_ALPHA);
		maxDelta = interval * maxAlpha;
		usePrediction = toggle;
	}

	public void Update(float delta)
	{
		updatedAlpha = false;
		if (newData)
		{
			if (currentDelta < maxDelta)
			{
				currentDelta += delta;
				float num = currentDelta / interval;
				alpha = ((!(num < maxAlpha)) ? maxAlpha : num);
				updatedAlpha = true;
			}
			else if (!usePrediction)
			{
				if (hasVec)
				{
					Vector = lastVec;
				}
				if (hasRot)
				{
					Rotation = lastRot;
				}
				isActive = false;
			}
		}
		else if (isActive)
		{
			bool flag = false;
			if (alpha != 1f)
			{
				float num2 = delta / interval;
				if (alpha < 1f)
				{
					alpha += num2;
					flag = alpha >= 1f;
				}
				else
				{
					alpha -= num2;
					flag = alpha <= 1f;
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				alpha = 1f;
				if (hasVec)
				{
					Vector = lastVec;
				}
				if (hasRot)
				{
					Rotation = lastRot;
				}
				isActive = false;
			}
			else
			{
				updatedAlpha = true;
			}
		}
		if (!updatedAlpha)
		{
			return;
		}
		if (hasVec && lerpVec)
		{
			Vector = new Vector3(prevVec.x + deltaVec.x * alpha, prevVec.y + deltaVec.y * alpha, prevVec.z + deltaVec.z * alpha);
		}
		if (hasRot && lerpQuat)
		{
			float num3 = Mathf.Sin((1f - alpha) * halfTheta) / sinHalfTheta;
			float num4 = Mathf.Sin(alpha * halfTheta) / sinHalfTheta;
			Rotation = default(Quaternion);
			if (reverseQuat)
			{
				Rotation.x = prevRot.x * num3 - lastRot.x * num4;
				Rotation.y = prevRot.y * num3 - lastRot.y * num4;
				Rotation.z = prevRot.z * num3 - lastRot.z * num4;
				Rotation.w = prevRot.w * num3 - lastRot.w * num4;
			}
			else
			{
				Rotation.x = prevRot.x * num3 + lastRot.x * num4;
				Rotation.y = prevRot.y * num3 + lastRot.y * num4;
				Rotation.z = prevRot.z * num3 + lastRot.z * num4;
				Rotation.w = prevRot.w * num3 + lastRot.w * num4;
			}
		}
	}

	public void SkipToEnd()
	{
		alpha = 1f;
		isActive = true;
		Stop();
	}

	public void Stop()
	{
		newData = false;
	}

	public void Set(Vector3 vec)
	{
		float num = Vector.x - lastVec.x;
		float num2 = Vector.y - lastVec.y;
		float num3 = Vector.z - lastVec.z;
		float smoothness = NetworkScene.ServerSettings.smoothness;
		prevVec = new Vector3(lastVec.x + num * smoothness, lastVec.y + num2 * smoothness, lastVec.z + num3 * smoothness);
		lastVec = vec;
		CacheVecLerpData();
		float x = vec.x;
		float y = vec.y;
		float z = vec.z;
		float num4 = x - lastVec.x;
		float num5 = y - lastVec.y;
		float num6 = z - lastVec.z;
		float num7 = x - prevVec.x;
		float num8 = y - prevVec.y;
		float num9 = y - prevVec.z;
		float num10 = num4 * num4 + num5 * num5 + num6 * num6;
		float num11 = num7 * num7 + num8 * num8 + num9 * num9;
		float rateMultiplier = num11 / num10;
		NewData(rateMultiplier);
	}

	public void Set(Quaternion rot)
	{
		float num = Rotation.x - lastRot.x;
		float num2 = Rotation.y - lastRot.y;
		float num3 = Rotation.z - lastRot.z;
		float num4 = Rotation.w - lastRot.w;
		float smoothness = NetworkScene.ServerSettings.smoothness;
		prevRot = new Quaternion(lastRot.x + num * smoothness, lastRot.y + num2 * smoothness, lastRot.z + num3 * smoothness, lastRot.w + num4 * smoothness);
		lastRot = rot;
		CacheQuatLerpData();
		float x = rot.x;
		float y = rot.y;
		float z = rot.z;
		float w = rot.w;
		float num5 = x * lastRot.x + y * lastRot.y + z * lastRot.z + w * lastRot.w;
		float num6 = x * prevRot.x + y * prevRot.y + z * prevRot.z + w * prevRot.w;
		num5 = ((!(num5 < 0f)) ? num5 : (0f - num5));
		num6 = ((!(num6 < 0f)) ? num6 : (0f - num6));
		float num7 = ((!(num5 < 1f)) ? acosOne : Mathf.Acos(num5));
		float num8 = ((!(num6 < 1f)) ? acosOne : Mathf.Acos(num6));
		float rateMultiplier = ((!(num8 < 0f)) ? num8 : (0f - num8)) / ((!(num7 < 0f)) ? num7 : (0f - num7));
		NewData(rateMultiplier);
	}

	private void NewData(float rateMultiplier)
	{
		float num = 1f - (1f - ((rateMultiplier < 0.3f) ? 0.3f : ((!(rateMultiplier > MAX_ALPHA)) ? rateMultiplier : maxAlpha))) * NetworkScene.ServerSettings.smoothness;
		float num2 = baseInterval * num;
		SetInterval(num2);
		currentDelta = 0f;
		newData = true;
		isActive = true;
	}

	public void SetData(float baseEnt, Vector3 vec)
	{
		Override(vec, vec);
		Vector = vec;
		OverrideInterval(baseEnt);
		newData = false;
		hasVec = true;
		alpha = 1f;
	}

	public void SetData(float baseEnt, Quaternion rot)
	{
		Override(rot, rot);
		Rotation = rot;
		OverrideInterval(baseEnt);
		newData = false;
		hasRot = true;
		alpha = 1f;
	}

	public void SetInterval(float newInterval)
	{
		interval = newInterval;
		maxDelta = interval * MAX_ALPHA;
	}

	public void Store(Vector3 vec)
	{
		lastVec = vec;
	}

	public void Store(Quaternion rot)
	{
		lastRot = rot;
	}

	public void Override(Vector3 vec, Vector3 prev)
	{
		lastVec = vec;
		prevVec = prev;
		CacheVecLerpData();
		NewData(baseInterval);
	}

	public void Override(Quaternion rot, Quaternion prev)
	{
		lastRot = rot;
		prevRot = prev;
		CacheQuatLerpData();
		NewData(baseInterval);
	}

	public void OverrideInterval(float newInterval)
	{
		baseInterval = newInterval;
		SetInterval(baseInterval);
	}

	private void CacheVecLerpData()
	{
		float x = lastVec.x;
		float y = lastVec.y;
		float z = lastVec.z;
		float x2 = prevVec.x;
		float y2 = prevVec.y;
		float z2 = prevVec.z;
		deltaVec = new Vector3(x - x2, y - y2, z - z2);
		lerpVec = deltaVec.x * deltaVec.x + deltaVec.y * deltaVec.y + deltaVec.z * deltaVec.z > 0f;
		if (!lerpVec)
		{
			Vector = lastVec;
		}
	}

	private void CacheQuatLerpData()
	{
		float x = lastRot.x;
		float y = lastRot.y;
		float z = lastRot.z;
		float w = lastRot.w;
		float x2 = prevRot.x;
		float y2 = prevRot.y;
		float z2 = prevRot.z;
		float w2 = prevRot.w;
		float num = x2 * x + y2 * y + z2 * z + w2 * w;
		float num2 = 0f;
		bool flag = false;
		if (num < 0f)
		{
			num2 = 0f - num;
			flag = true;
		}
		lerpQuat = false;
		if (((!flag) ? num : num2) >= 0.999999f)
		{
			Rotation = prevRot;
			return;
		}
		reverseQuat = false;
		if (flag)
		{
			reverseQuat = true;
			num = num2;
		}
		halfTheta = Mathf.Acos(num);
		sinHalfTheta = Mathf.Sqrt(1f - num * num);
		if (((!(sinHalfTheta < 0f)) ? sinHalfTheta : (0f - sinHalfTheta)) < 0.001f)
		{
			if (reverseQuat)
			{
				halfQuat.x = x2 * 0.5f + x * 0.5f;
				halfQuat.y = y2 * 0.5f + y * 0.5f;
				halfQuat.z = z2 * 0.5f + z * 0.5f;
				halfQuat.w = w2 * 0.5f + w * 0.5f;
			}
			else
			{
				halfQuat.x = x2 * 0.5f - x * 0.5f;
				halfQuat.y = y2 * 0.5f - y * 0.5f;
				halfQuat.z = z2 * 0.5f - z * 0.5f;
				halfQuat.w = w2 * 0.5f - w * 0.5f;
			}
			Rotation = halfQuat;
		}
		else
		{
			lerpQuat = true;
		}
	}

	public bool WithinThreshold(Vector3 vec)
	{
		float num = lastVec.x - vec.x;
		float num2 = lastVec.y - vec.y;
		float num3 = lastVec.z - vec.z;
		float num4 = ((!(num < 0f)) ? num : (0f - num)) + ((!(num2 < 0f)) ? num2 : (0f - num2)) + ((!(num3 < 0f)) ? num3 : (0f - num3));
		return num4 * num4 < VEC_THRESHOLD;
	}

	public bool WithinThreshold(Quaternion rot)
	{
		float num = lastRot.x * rot.x + lastRot.y * rot.y + lastRot.z * rot.z + lastRot.w * rot.w;
		return ((!(num < 0f)) ? num : (0f - num)) > ROT_THRESHOLD;
	}
}
