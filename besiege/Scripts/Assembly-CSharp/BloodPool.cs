using System;
using UnityEngine;

[AddComponentMenu("VFX/BloodPool")]
public class BloodPool : MonoBehaviour
{
	public ParticleSystem pSystem;

	public ParticleSystemRenderer[] rens = new ParticleSystemRenderer[0];

	public Material overMat;

	public Material underMat;

	public float manualOffset;

	private float offset;

	private Vector3 pos;

	private float startHeight;

	private void Start()
	{
		pos = base.transform.position;
		ParticleSystem.ShapeModule shape = pSystem.shape;
		if (shape.shapeType == ParticleSystemShapeType.Sphere)
		{
			offset = pSystem.startSize * 0.2f + 2.1f;
		}
		else if (shape.shapeType == ParticleSystemShapeType.Hemisphere)
		{
			offset = pSystem.startSize * 0.2f + 2.1f;
		}
		if (pos.y + offset + manualOffset < WaterController.waterTransformHeight)
		{
			base.enabled = false;
			return;
		}
		if (!WaterController.Exist || WaterController.simInstance == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		startHeight = Mathf.Min(pos.y, WaterController.waterTransformHeight);
		SetRenderers(WaterController.Exist && !WaterFogController.overWater);
		WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Combine(WaterFogController.UnderwaterToggled, new Action<bool>(SetRenderers));
	}

	private void SetRenderers(bool under)
	{
		for (int i = 0; i < rens.Length; i++)
		{
			rens[i].sharedMaterial = ((!under) ? overMat : underMat);
		}
	}

	private void LateUpdate()
	{
		if (!pSystem.IsAlive())
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		pos = base.transform.position;
		float a = WaterController.CheckHeightMap(pos.x, pos.z);
		float t = Mathf.Clamp01((WaterController.waterTransformHeight - pos.y) / 10f);
		a = Mathf.Lerp(a, startHeight, t);
		base.transform.position = new Vector3(pos.x, a - offset - manualOffset, pos.z);
	}

	private void OnDestroy()
	{
		WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Remove(WaterFogController.UnderwaterToggled, new Action<bool>(SetRenderers));
	}
}
