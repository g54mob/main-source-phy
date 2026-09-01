using System.Collections;
using System.Linq;
using UnityEngine;

public class ReactorA5ArcBehaviour : MonoBehaviour
{
	public LineRenderer LineRenderer;

	public Transform OtherElectrode;

	public ReactorCoreBehaviour Reactor;

	public float NoiseFrequency = 2.95f;

	public float NoiseAmplitude = 2f;

	public float FlashDuration = 0.15f;

	public float OverlapCapsuleRadius = 3f;

	public Collider2D[] Exclude;

	public LayerMask OverlapMask;

	public float HitTemperatureAdjustment = 300f;

	public float HitChargeAdjustment = 1000f;

	public float HitForce = 15f;

	[ColorUsage(true, true)]
	public Color Color;

	public Vector3[] Points = new Vector3[64];

	private static readonly Collider2D[] buffer = new Collider2D[32];

	private void Start()
	{
		LineRenderer.enabled = false;
		LineRenderer.positionCount = Points.Length;
	}

	public void Flash()
	{
		GenerateBolt();
		StartCoroutine(FlashRoutine());
	}

	public void GenerateSmallArc()
	{
		SimpleLightningEmitterBehaviour.Instance.Emit(base.transform.position, (OtherElectrode.position - base.transform.position).normalized, 5f, 0.5f, OnSmallArcHit, Color, OverlapMask, 3f, 32, 0.5f, 0.5f);
	}

	private void OnSmallArcHit(PhysicalBehaviour phys, Vector2 point)
	{
		if (!float.IsNaN(point.x) && !float.IsNaN(point.y))
		{
			phys.Charge += HitChargeAdjustment / 5f;
			phys.Temperature += HitTemperatureAdjustment / 5f;
		}
	}

	private IEnumerator FlashRoutine()
	{
		Vector3 position = base.transform.position;
		Vector3 position2 = OtherElectrode.position;
		Vector3 vector = position - position2;
		float magnitude = vector.magnitude;
		Vector3 normalized = vector.normalized;
		Vector3 vector2 = (position + position2) * 0.5f;
		int num = Physics2D.OverlapCapsuleNonAlloc(vector2, new Vector2(magnitude, OverlapCapsuleRadius * 2f), CapsuleDirection2D.Horizontal, Mathf.Atan2(normalized.y, normalized.x) * 57.29578f, buffer, OverlapMask);
		for (int i = 0; i < num; i++)
		{
			Collider2D c = buffer[i];
			Affect(c, vector2);
		}
		LineRenderer.enabled = true;
		float t = 0f;
		while (t < FlashDuration)
		{
			t += Time.deltaTime;
			Color color = Color;
			color *= 1f - t / FlashDuration;
			LineRenderer lineRenderer = LineRenderer;
			Color endColor = (LineRenderer.startColor = color);
			lineRenderer.endColor = endColor;
			yield return null;
		}
		LineRenderer.enabled = false;
	}

	private void Affect(Collider2D c, Vector2 center)
	{
		if ((bool)c && !Exclude.Contains(c) && Global.main.PhysicalObjectsInWorldByTransform.TryGetValue(c.transform, out var value))
		{
			if (value.SimulateTemperature)
			{
				value.Temperature += HitTemperatureAdjustment;
			}
			if (value.ConductOverride && !value.ForceNoCharge)
			{
				value.Charge += HitChargeAdjustment;
			}
			if (value.rigidbody.bodyType == RigidbodyType2D.Dynamic)
			{
				Vector2 normalized = ((Vector2)value.transform.position - center).normalized;
				normalized += Random.insideUnitCircle * 0.2f;
				value.rigidbody.AddForce(HitForce * normalized);
			}
			if (Random.value > 0.9f)
			{
				value.SendMessage("Slice", SendMessageOptions.DontRequireReceiver);
			}
			if (Random.value > 0.5f)
			{
				value.SendMessage("Break", Random.insideUnitCircle, SendMessageOptions.DontRequireReceiver);
			}
			if (Random.value > 0.5f)
			{
				value.SendMessage("Damage", Random.value * HitForce, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	private void GenerateBolt()
	{
		Vector3 position = base.transform.position;
		Vector3 position2 = OtherElectrode.position;
		float x = Random.Range(-100f, 100f);
		for (int i = 0; i < Points.Length; i++)
		{
			float t = (float)i / (float)(Points.Length - 1);
			Points[i] = Vector3.Lerp(position, position2, t);
			if (i != 0 && i != Points.Length - 1)
			{
				Points[i] += Utils.GetPerlin2Mapped(x, (float)i * NoiseFrequency) * NoiseAmplitude;
				Points[i] += 0.2f * NoiseAmplitude * Utils.GetPerlin2Mapped(x, (float)i * NoiseFrequency * 8f);
			}
		}
		LineRenderer.SetPositions(Points);
	}
}
