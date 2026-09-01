using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/NauticalScrew")]
public class NauticalScrew : CogMotorControllerHinge
{
	[SerializeField]
	protected Collider frontCollider;

	[SerializeField]
	protected Collider[] backCollider;

	[SerializeField]
	protected float power = 100f;

	[SerializeField]
	protected FixedJoint staticJoint;

	[SerializeField]
	protected HingeJoint spinningJoint;

	[SerializeField]
	[HideInInspector]
	protected bool hasScrewBase;

	protected NauticalScrew parentScrew;

	protected NauticalScrew childScrew;

	[Header("Particles")]
	[SerializeField]
	protected ParticleSystem bubbles;

	protected ParticleSystem.VelocityOverLifetimeModule velModule;

	protected ParticleSystem.EmissionModule emiss;

	[SerializeField]
	protected float particleRate = 1f;

	[SerializeField]
	protected float particleSpeed = 0.5f;

	private bool hasFixedJoint;

	public bool limitVelocity = true;

	public bool projectVelocityLimit = true;

	public float limitingBias = 1f;

	protected MToggle chirality;

	public bool allowChiralityChange = true;

	private float[] colX = new float[0];

	private Vector3[] colEuler = new Vector3[0];

	private Vector3 lastEuler = Vector3.zero;

	public MToggle Chirality
	{
		get
		{
			return chirality;
		}
	}

	protected int Chiral
	{
		get
		{
			return (!allowChiralityChange || !chirality.IsActive) ? 1 : (-1);
		}
	}

	protected override int FlipInvert
	{
		get
		{
			return (!Flipped) ? (-Chiral) : Chiral;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!allowChiralityChange)
		{
			return;
		}
		chirality = AddToggle("MIRRORED", "chirality", false);
		if (isSimulating)
		{
			return;
		}
		if (Prefab.hasMyBounds && myBounds != null)
		{
			colX = new float[myBounds.childColliders.Count];
			colEuler = new Vector3[colX.Length];
			for (int i = 0; i < colX.Length; i++)
			{
				Transform transform = myBounds.childColliders[i].transform;
				colX[i] = transform.localPosition.x;
				colEuler[i] = transform.localEulerAngles;
			}
		}
		chirality.Toggled += SetChirality;
		SetChirality(chirality.IsActive);
	}

	public void SetChirality(bool b)
	{
		Vector3 localScale = MeshRenderer.transform.localScale;
		localScale.x = ((!b) ? 0.27f : (-0.27f));
		MeshRenderer.transform.localScale = localScale;
		localScale = VisualController.arrow.transform.localEulerAngles;
		localScale.y = ((!b) ? (-22.5f) : 22.5f);
		VisualController.arrow.transform.localEulerAngles = localScale;
		if (Prefab.hasMyBounds && myBounds != null)
		{
			int num = 0;
			foreach (Collider childCollider in myBounds.childColliders)
			{
				if (childCollider.transform.parent == base.transform)
				{
					Vector3 localPosition = childCollider.transform.localPosition;
					localPosition.x = colX[num];
					Vector3 localEulerAngles = colEuler[num];
					if (b)
					{
						localPosition.x = 0f - localPosition.x;
						localEulerAngles.y = 0f - localEulerAngles.y;
						localEulerAngles.z = 0f - localEulerAngles.z;
					}
					childCollider.transform.localPosition = localPosition;
					childCollider.transform.localEulerAngles = localEulerAngles;
				}
				num++;
			}
		}
		FlipArrow(Flipped);
		if (!hasScrewBase)
		{
			UpdateScrews();
		}
	}

	protected float abs(float val)
	{
		return (!(val < 0f)) ? val : (0f - val);
	}

	public override void OnMapperOpen()
	{
		if (!hasScrewBase)
		{
			base.OnMapperOpen();
			SelectGroup(true);
		}
		else
		{
			BlockMapper.Open(GetRootScrew());
		}
	}

	public override void OnMapperClose()
	{
		base.OnMapperClose();
		if (!hasScrewBase)
		{
			SelectGroup(false);
		}
	}

	protected void SelectGroup(bool select)
	{
		int state = 0;
		if (select)
		{
			state = ((!hasScrewBase) ? 1 : 2);
		}
		else if (IsSelected)
		{
			state = 1;
		}
		VisualController.UpdateOutline(state);
		VisualController.freezeOutline = select;
		if (childScrew != null)
		{
			childScrew.SelectGroup(select);
		}
	}

	public void EnableFrontCollider()
	{
		frontCollider.enabled = true;
	}

	protected override void Start()
	{
		if (isSimulating)
		{
			if (hasScrewBase)
			{
				Object.DestroyImmediate(spinningJoint);
				hasFixedJoint = true;
			}
			else
			{
				Object.DestroyImmediate(staticJoint);
				base.Start();
			}
		}
		velModule = bubbles.velocityOverLifetime;
		emiss = bubbles.emission;
		bubbles.randomSeed = (uint)Random.Range(0, 9999999);
		if (isSimulating && SimPhysics)
		{
			emiss.rate = 0f;
			if (WaterController.Exist)
			{
				bubbles.Play();
			}
		}
	}

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		if (!Prefab.hasMyBounds || !(myBounds != null))
		{
			return;
		}
		float num = 0.22f * base.transform.localScale.z;
		if (base.transform.localScale.x < num || base.transform.localScale.y < num)
		{
			foreach (Collider childCollider in myBounds.childColliders)
			{
				if (childCollider.transform.parent == base.transform)
				{
					childCollider.enabled = false;
				}
			}
		}
		num = 0.6f * Mathf.Max(base.transform.localScale.x, base.transform.localScale.y);
		if (base.transform.localScale.z < num)
		{
			myBounds.childColliders[myBounds.childColliders.Count - 1].enabled = false;
		}
	}

	public override void FixedUpdateBlock()
	{
		if (!hasScrewBase)
		{
			base.FixedUpdateBlock();
		}
		if (!base.InWater || StatMaster.GodTools.GravityDisabled)
		{
			return;
		}
		float num;
		if (noRigidbody)
		{
			Vector3 localEulerAngles = base.transform.localEulerAngles;
			num = 0f - (base.transform.worldToLocalMatrix * (localEulerAngles - lastEuler)).z;
			emiss.rate = Mathf.Abs(num) * particleRate;
			velModule.z = num * particleSpeed;
			lastEuler = localEulerAngles;
			return;
		}
		num = (base.transform.worldToLocalMatrix * Rigidbody.angularVelocity).z * ((!allowChiralityChange || !chirality.IsActive) ? (-1f) : 1f);
		float num2 = 1f;
		if (limitVelocity)
		{
			Vector3 velocity = Rigidbody.velocity;
			float num3 = velocity.sqrMagnitude;
			if (projectVelocityLimit)
			{
				float num4 = Vector3.Dot(velocity.normalized, base.transform.forward);
				num3 = Vector3.Project(velocity, base.transform.forward).sqrMagnitude;
				if (num < 0f)
				{
					num4 = 0f - num4;
				}
				num3 = ((!(num4 > 0f)) ? 0f : (num3 * num4));
			}
			float num5 = Mathf.Abs(num) * 75f;
			num2 = 1f - Mathf.Clamp01(num3 / num5);
			num2 = num2 * limitingBias + (1f - limitingBias);
		}
		if (num2 > 0f)
		{
			Vector3 velocity2 = Rigidbody.velocity;
			Vector3 vector = base.transform.forward * (num2 * num * power * submergedPercent);
			Rigidbody.AddForce(vector - velocity2);
			if (!hasJoint && !hasFixedJoint)
			{
				Rigidbody.AddTorque(0f, power * velocity2.sqrMagnitude * 0.0005f, 0f);
			}
		}
		emiss.rate = Mathf.Abs(num) * particleRate;
		velModule.z = num * particleSpeed;
	}

	public override void SetFlip(bool flip)
	{
		if (!hasScrewBase)
		{
			Flipped = flip;
		}
	}

	protected override void FlipArrow(bool flipped)
	{
		if (allowChiralityChange && chirality.IsActive)
		{
			flipped = !flipped;
		}
		VisualController.FlipArrow(flipped, Axes.y);
	}

	public override void ClusterChanged(float value)
	{
		if (isSimulating || value < 0f)
		{
			return;
		}
		parentScrew = (childScrew = null);
		bool flag = false;
		List<BlockLink> neighbours = _parentMachine.LinkManager.GetNode(this).Neighbours;
		for (int i = 0; i < neighbours.Count; i++)
		{
			BlockBehaviour block = neighbours[i].Other.Block;
			if (block.BlockID == base.BlockID)
			{
				if (neighbours[i].isOwnLink)
				{
					flag = true;
					parentScrew = block as NauticalScrew;
				}
				else
				{
					childScrew = block as NauticalScrew;
				}
			}
		}
		hasScrewBase = flag;
		if (flag)
		{
			blockJoint = staticJoint;
			VisualController.arrow.enabled = false;
		}
		else
		{
			blockJoint = spinningJoint;
			VisualController.arrow.enabled = true;
		}
		if (!flag)
		{
			StartCoroutine(QueueUpdateChildScrews());
		}
	}

	private void FixLinks(ref bool connectedToScrew)
	{
		if (!parentScrew)
		{
			Collider[] array = Physics.OverlapSphere(base.transform.position, 0.2256f, AddPiece.CreateLayerMask(new int[1] { 12 }));
			for (int i = 0; i < array.Length; i++)
			{
				Rigidbody attachedRigidbody = array[i].attachedRigidbody;
				if ((bool)attachedRigidbody)
				{
					NauticalScrew component = attachedRigidbody.GetComponent<NauticalScrew>();
					if ((bool)component)
					{
						connectedToScrew = true;
						parentScrew = component;
						break;
					}
				}
			}
		}
		if ((bool)childScrew)
		{
			return;
		}
		Collider[] array2 = Physics.OverlapBox(base.transform.position + base.transform.forward, new Vector3(0.5f, 0.5f, float.Epsilon), base.transform.rotation, AddPiece.CreateLayerMask(new int[1] { 22 }));
		for (int j = 0; j < array2.Length; j++)
		{
			NauticalScrew componentInParent = array2[j].GetComponentInParent<NauticalScrew>();
			if ((bool)componentInParent)
			{
				childScrew = componentInParent;
				break;
			}
		}
	}

	protected void UpdateState()
	{
		forwardKey.DisplayInMapper = !hasScrewBase;
		backwardKey.DisplayInMapper = !hasScrewBase;
		automaticToggle.DisplayInMapper = !hasScrewBase;
		toggleMode.DisplayInMapper = !hasScrewBase;
		autoBrakeMode.DisplayInMapper = !hasScrewBase;
		speedSlider.DisplayInMapper = !hasScrewBase;
		accSlider.DisplayInMapper = !hasScrewBase;
		if (allowChiralityChange)
		{
			chirality.DisplayInMapper = !hasScrewBase;
		}
		if (hasScrewBase)
		{
			if (allowChiralityChange && parentScrew.Chiral != Chiral)
			{
				chirality.SetValue(parentScrew.Chirality.IsActive);
				SetChirality(chirality.IsActive);
				XDataHolder data = new XDataHolder();
				OnSave(data);
			}
			if (parentScrew.Flipped != Flipped)
			{
				Flipped = parentScrew.Flipped;
				PostFlip(false, true);
			}
		}
	}

	protected NauticalScrew GetRootScrew()
	{
		if (parentScrew == null)
		{
			return this;
		}
		return parentScrew.GetRootScrew();
	}

	private IEnumerator QueueUpdateChildScrews()
	{
		yield return new WaitForEndOfFrame();
		if (!hasScrewBase)
		{
			UpdateScrews();
		}
	}

	protected void UpdateScrews()
	{
		UpdateState();
		if (childScrew != null)
		{
			childScrew.UpdateScrews();
		}
	}

	public override void FinishPhysics()
	{
		base.FinishPhysics();
		if (!isSimulating || !SimPhysics)
		{
			return;
		}
		if (blockJoint != null && blockJoint.connectedBody != null)
		{
			NauticalScrew component = blockJoint.connectedBody.GetComponent<NauticalScrew>();
			if (component == null)
			{
				hasScrewBase = false;
			}
			if (hasScrewBase)
			{
				for (int i = 0; i < backCollider.Length; i++)
				{
					backCollider[i].enabled = true;
				}
				component.EnableFrontCollider();
			}
		}
		else
		{
			OnJointBreak();
		}
	}

	protected override void OnJointBreak()
	{
		base.OnJointBreak();
		FragmentVisualController.EmitJointBreakMarker(base.transform.position);
		if (!hasJoint)
		{
			calcAngularDragInWater = true;
			power /= 2f;
			hasScrewBase = false;
		}
		if (hasFixedJoint)
		{
			hasFixedJoint = false;
		}
	}
}
