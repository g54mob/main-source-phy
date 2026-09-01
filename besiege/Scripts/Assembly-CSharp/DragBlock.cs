using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Directional Drag Block")]
public class DragBlock : BlockBehaviour
{
	public AudioSource sfx;

	private MSlider magSlider;

	private float drag;

	private float vm;

	private float randomPitch;

	public AnimationCurve clothScale = AnimationCurve.Linear(0.1f, -0.2f, 3f, 0.1f);

	private Vector3 v = Vector3.zero;

	public MSlider MagSlider
	{
		get
		{
			return magSlider;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		magSlider = AddSlider(4583, "magnitude", 1f, 0.1f, 3f, string.Empty);
		magSlider.ValueChanged += SetDrag;
		if (isSimulating)
		{
			sfx.volume = 0f;
			sfx.Play();
			sfx.timeSamples = Random.Range(0, sfx.clip.samples);
			randomPitch = Random.Range(-0.05f, 0.05f);
			if (SimPhysics && !noRigidbody)
			{
				Rigidbody.centerOfMass = Vector3.forward * 0.5f * base.transform.localScale.z;
			}
		}
	}

	protected void SetDrag(float d)
	{
		d = ((!float.IsNaN(d)) ? clothScale.Evaluate(d) : (-0.56f));
		VisualController.AssignMaterialProperty("_Deform", new Vector4(0f, d, 0f, 0f));
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(!SimPhysics || Prefab.RegisterSimUpdate, SimPhysics && Prefab.RegisterSimFixedUpdate, Prefab.RegisterSimLateUpdate, Prefab.RegisterEmulationUpdate);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		CalculateDrag();
		UpdateSound();
	}

	public override void FixedUpdateBlock()
	{
		base.FixedUpdateBlock();
		if (!noRigidbody && SimPhysics)
		{
			CalculateDrag();
			if (Rigidbody.drag != drag)
			{
				Rigidbody.drag = Mathf.Min(drag * 0.01f, 100f);
			}
			if (_parentMachine.finishedPhysics)
			{
				Rigidbody.AddForce(v);
			}
			UpdateSound();
		}
	}

	protected void CalculateDrag()
	{
		drag = magSlider.Value;
		if (!float.IsNaN(drag))
		{
			v = ((!SimPhysics) ? NetBlock.Velocity : Rigidbody.velocity) * 0.25f;
			v = CalculateDrag(v, drag * v.sqrMagnitude);
			drag *= 1f + base.GetSubmergedPctMV * 5f;
			v = drag * -v;
			drag = v.magnitude;
			originalDrag = Mathf.Pow(drag, 0.4f) * 0.5f;
		}
	}

	protected Vector3 CalculateDrag(Vector3 vel, float mag)
	{
		mag = Mathf.Lerp(0.1f, 0f, mag * 0.001f) * (1f - base.GetSubmergedPctMV * 0.9f);
		vel = base.transform.InverseTransformDirection(vel);
		vel.y = Mathf.Max(0f, vel.y);
		vm = vel.y * vel.y;
		vel.y *= 0.6f + Mathf.Min(vm * 0.5f, 100f);
		vel.x = Mathf.Clamp(vel.x, -1f, 1f) * mag;
		vel.z = Mathf.Clamp(vel.z, -1f, 1f) * mag;
		return base.transform.TransformDirection(vel);
	}

	protected virtual void UpdateSound()
	{
		float num = drag * vm * 0.08f;
		sfx.volume = Mathf.Clamp01(num * 0.1f - 0.1f);
		sfx.pitch = Mathf.Clamp(num * 0.015f + 0.985f + randomPitch, 0.5f, 3f);
	}
}
