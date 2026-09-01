using System;
using UnityEngine;

[Serializable]
public class ScatteredObject : ICloneable
{
	public bool enable;

	public GameObject prefab;

	public bool isPrefab;

	public bool isScaleOption;

	public bool isScale;

	public bool uniformScale = true;

	public bool isScaleParticle = true;

	public bool isScaleLight = true;

	public float xMinScale = 0.8f;

	public float xmaxScale = 1.2f;

	public float yMinScale = 0.8f;

	public float yMaxScale = 1.2f;

	public float zMinScale = 0.8f;

	public float zMaxScale = 1.2f;

	public bool isRotationOption;

	public bool isRotationX;

	public float xMinRot = -25f;

	public float xMaxRot = 25f;

	public bool isRotationY;

	public float yMinRot = -180f;

	public float yMaxRot = 180f;

	public bool isRotationZ;

	public float zMinRot = -25f;

	public float zMaxRot = 25f;

	public float offset;

	public bool isWait2delete;

	public bool showOption;

	public Texture2D preview;

	public object Clone()
	{
		return MemberwiseClone();
	}

	public static void ApplyRS(Transform tr, ScatteredObject scatter)
	{
		if (scatter.isScale)
		{
			if (scatter.uniformScale)
			{
				float num = UnityEngine.Random.Range(scatter.xMinScale, scatter.xmaxScale);
				tr.localScale = new Vector3(num, num, num);
				ScaleEffect(tr, num, scatter.isScaleLight, scatter.isScaleParticle);
			}
			else
			{
				tr.localScale = new Vector3(UnityEngine.Random.Range(scatter.xMinScale, scatter.xmaxScale), UnityEngine.Random.Range(scatter.yMinScale, scatter.yMaxScale), UnityEngine.Random.Range(scatter.zMinScale, scatter.zMaxScale));
				ScaleEffect(tr, (tr.localScale.x + tr.localScale.y + tr.localScale.z) / 3f, scatter.isScaleLight, scatter.isScaleParticle);
			}
		}
		if (scatter.isRotationX || scatter.isRotationY || scatter.isRotationZ)
		{
			Vector3 eulerAngles = tr.eulerAngles;
			if (scatter.isRotationX)
			{
				eulerAngles.x = UnityEngine.Random.Range(scatter.xMinRot, scatter.xMaxRot);
			}
			if (scatter.isRotationY)
			{
				eulerAngles.y = UnityEngine.Random.Range(scatter.yMinRot, scatter.yMaxRot);
			}
			if (scatter.isRotationZ)
			{
				eulerAngles.z = UnityEngine.Random.Range(scatter.zMinRot, scatter.zMaxRot);
			}
			tr.eulerAngles = eulerAngles;
		}
	}

	public static void ScaleEffect(Transform tr, float scaleFactor, bool isScaleLight, bool isScaleParticle)
	{
	}
}
