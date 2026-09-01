using UnityEngine;

[ExecuteInEditMode]
public class PirateWaterManager : MonoBehaviour
{
	public float WaterLevel;

	public Material WaterMaterial;

	public Vector2 WaterDirection;

	[Header("Layer01")]
	public float Layer01ScaleFactor = 2f;

	public float Layer01AmplitudeFactor = 2f;

	public float Layer01WaveSpeed = 1f;

	[Header("Layer02")]
	public float Layer02ScaleFactor = 2f;

	public float Layer02AmplitudeFactor = 2f;

	public float Layer02WaveSpeed = 1f;

	private static PirateWaterManager instance;

	public static PirateWaterManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Object.FindObjectOfType<PirateWaterManager>();
			}
			return instance;
		}
	}

	private float WaterTime
	{
		get
		{
			if (Application.isPlaying)
			{
				return Time.time;
			}
			return Time.realtimeSinceStartup;
		}
	}

	public static float GetValue(Vector3 pos)
	{
		if ((bool)Instance)
		{
			return Instance.GetValueInternal(pos);
		}
		return 0f;
	}

	public static float GetYLevel(Vector3 pos)
	{
		return Instance.WaterLevel + GetValue(pos);
	}

	private Vector2Int WorldSpaceToPixelPos(Vector3 pos, float layerScaleFactor, float waveSpeed)
	{
		Vector2 vector = new Vector2(pos.x, pos.z);
		vector = 0.01f * layerScaleFactor * vector;
		vector += 0.2f * WaterTime * waveSpeed * WaterDirection;
		if (vector.x < 0f)
		{
			vector.x += Mathf.Ceil(Mathf.Abs(vector.x));
		}
		else
		{
			vector.x -= Mathf.Floor(vector.x);
		}
		if (vector.y < 0f)
		{
			vector.y += Mathf.Ceil(Mathf.Abs(vector.y));
		}
		else
		{
			vector.y -= Mathf.Floor(vector.y);
		}
		return Vector2Int.RoundToInt(vector);
	}

	private float Frac(float value)
	{
		return value - Mathf.Floor(value);
	}

	private Vector2 Frac2D(Vector2 value)
	{
		return new Vector2(Frac(value.x), Frac(value.y));
	}

	private Vector2 Sin2D(Vector2 value)
	{
		return new Vector2(Mathf.Sin(value.x), Mathf.Sin(value.y));
	}

	private Vector2 Floor2D(Vector2 value)
	{
		return new Vector2(Mathf.Floor(value.x), Mathf.Floor(value.y));
	}

	private Vector2 Hash22(Vector2 p)
	{
		p = new Vector2(Vector2.Dot(p, new Vector2(127.1f, 311.7f)), Vector2.Dot(p, new Vector2(269.5f, 183.3f)));
		return -Vector2.one + 2f * Frac2D(Sin2D(p) * 43758.547f);
	}

	private float PerlinNoise(Vector2 p)
	{
		Vector2 vector = Floor2D(p);
		Vector2 vector2 = p - vector;
		Vector2 vector3 = vector2 * vector2 * (Vector2.one * 3f - 2f * vector2);
		return Mathf.Lerp(Mathf.Lerp(Vector2.Dot(Hash22(vector + new Vector2(0f, 0f)), vector2 - new Vector2(0f, 0f)), Vector2.Dot(Hash22(vector + new Vector2(1f, 0f)), vector2 - new Vector2(1f, 0f)), vector3.x), Mathf.Lerp(Vector2.Dot(Hash22(vector + new Vector2(0f, 1f)), vector2 - new Vector2(0f, 1f)), Vector2.Dot(Hash22(vector + new Vector2(1f, 1f)), vector2 - new Vector2(1f, 1f)), vector3.x), vector3.y);
	}

	private float GetValueInternal(Vector3 pos)
	{
		Vector2 vector = new Vector2(pos.x, pos.z);
		float num = (PerlinNoise(0.01f * Layer01ScaleFactor * vector + 0.2f * Layer01WaveSpeed * WaterTime * WaterDirection) - 0.35f) * Layer01AmplitudeFactor;
		float num2 = PerlinNoise(0.01f * Layer02ScaleFactor * vector + 0.2f * Layer02WaveSpeed * WaterTime * WaterDirection) - 0.35f;
		num2 *= Layer02AmplitudeFactor;
		return num + num2;
	}

	private void Update()
	{
		if ((bool)WaterMaterial)
		{
			WaterMaterial.SetFloat("_SentTime", WaterTime);
			WaterMaterial.SetVector("_WaveDirection", WaterDirection);
			WaterMaterial.SetFloat("_Layer01ScaleFactor", Layer01ScaleFactor);
			WaterMaterial.SetFloat("_Layer01AmplutudeFactor", Layer01AmplitudeFactor);
			WaterMaterial.SetFloat("_Layer01WaveSpeed", Layer01WaveSpeed);
			WaterMaterial.SetFloat("_Layer02ScaleFactor", Layer02ScaleFactor);
			WaterMaterial.SetFloat("_Layer02AmplutudeFactor", Layer02AmplitudeFactor);
			WaterMaterial.SetFloat("_Layer02WaveSpeed", Layer02WaveSpeed);
		}
	}
}
