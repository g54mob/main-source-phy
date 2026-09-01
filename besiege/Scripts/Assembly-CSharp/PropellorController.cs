using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/PropellorController")]
public class PropellorController : AxialDrag
{
	public Transform vis;

	[SerializeField]
	protected Vector3 upEuler;

	public AudioSource sfx;

	private float size = 1f;

	private float randomPitch;

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating && !stripped && upEuler == Vector3.zero)
		{
			upEuler = upTransform.localEulerAngles;
		}
		if (isSimulating)
		{
			sfx.volume = 0f;
			sfx.Play();
			sfx.timeSamples = Random.Range(0, sfx.clip.samples);
			size = base.transform.localScale.z;
			randomPitch = Random.Range(-0.05f, 0.05f);
		}
	}

	protected override void Start()
	{
		base.Start();
		CheckFlipDirection();
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(!SimPhysics || Prefab.RegisterSimUpdate, Prefab.RegisterSimFixedUpdate, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		UpdateSound();
	}

	public override void FixedUpdateBlock()
	{
		base.FixedUpdateBlock();
		UpdateSound();
	}

	protected virtual void UpdateSound()
	{
		Vector3 vector = upTransform.InverseTransformDirection((!SimPhysics) ? NetBlock.Velocity : Rigidbody.velocity);
		Vector3 vector2 = ((!SimPhysics) ? NetBlock.AngularVelocity : Rigidbody.angularVelocity);
		vector2.z = 0f;
		float num = 0.25f + Mathf.Clamp01(vector2.sqrMagnitude * 0.2f) * 0.75f;
		float num2 = Mathf.Abs(vector.x);
		float num3 = Mathf.Clamp01(xyz.sqrMagnitude * 10f) * 0.5f + 0.5f;
		sfx.volume = Mathf.Clamp01(num2 * 0.1f - 1f) * size * num * num3;
		sfx.pitch = Mathf.Clamp((num2 * 0.04f + 0.96f) * 0.5f * num3 + randomPitch, 0.5f, 4f);
	}

	public override bool OnFlip(bool sound, bool isUndo)
	{
		if (sound)
		{
			ReferenceMaster.PlayFlip();
		}
		CheckFlipDirection();
		return true;
	}

	private void CheckFlipDirection()
	{
		if (Flipped)
		{
			if (!stripped)
			{
				upTransform.localEulerAngles = new Vector3(upEuler.x, upEuler.y, 0f - upEuler.z);
			}
			vis.localEulerAngles = new Vector3(0f, -180f, 23f);
		}
		else
		{
			if (!stripped)
			{
				upTransform.localEulerAngles = new Vector3(upEuler.x, upEuler.y, upEuler.z);
			}
			vis.localEulerAngles = new Vector3(0f, -180f, -23f);
		}
	}

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		data.Write("flipped", Flipped);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (data.HasKey("flipped"))
		{
			Flipped = data.ReadBool("flipped");
			PostFlip(false, false);
		}
	}
}
