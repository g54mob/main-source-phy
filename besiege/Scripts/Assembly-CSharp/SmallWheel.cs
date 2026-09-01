using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/SmallWheel")]
public class SmallWheel : BlockBehaviour
{
	public ConfigurableJoint myJoint;

	public Transform model;

	public float speed = 3f;

	private bool JointBroken;

	private Vector3 smoothVelocity = Vector3.zero;

	public int version = 1;

	[SerializeField]
	private List<BoxCollider> oldColliders = new List<BoxCollider>();

	[SerializeField]
	private SphereCollider wheelCollider;

	[SerializeField]
	private Transform trigger;

	[SerializeField]
	private GameObject occluder;

	public Transform other;

	public Vector3 jointPosRelativeToOther;

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating)
		{
			return;
		}
		Object.Destroy(occluder);
		if (version == 1)
		{
			List<Vector3> list = new List<Vector3>(5);
			for (int i = 0; i < base.transform.childCount; i++)
			{
				list.Add(base.transform.GetChild(i).position);
			}
			Vector3 localScale = base.transform.localScale;
			base.transform.position = base.transform.TransformPoint(wheelCollider.center);
			wheelCollider.center = Vector3.zero;
			base.transform.localScale = Vector3.one;
			model.localScale = Vector3.Scale(model.localScale, localScale);
			wheelCollider.radius *= localScale.MaxAxis();
			for (int j = 0; j < list.Count; j++)
			{
				base.transform.GetChild(j).position = list[j];
			}
		}
	}

	public override void UpdateBlock()
	{
		if (!JointBroken)
		{
			Vector3 vector = ((version != 1) ? base.transform.position : other.transform.TransformPoint(jointPosRelativeToOther));
			smoothVelocity = Vector3.Lerp(smoothVelocity, SimPhysics ? Rigidbody.velocity : NetBlock.Velocity, Time.deltaTime * speed);
			Vector3 worldUp = ((!(Mathf.Abs(smoothVelocity.sqrMagnitude) > 0.05f)) ? vector : smoothVelocity);
			model.LookAt(vector, worldUp);
		}
	}

	public override int CheckJoints()
	{
		if ((bool)myJoint && (bool)myJoint.connectedBody)
		{
			SetVisualConnection(myJoint.connectedBody.transform);
		}
		else
		{
			JointBroken = true;
		}
		return base.CheckJoints();
	}

	public void SetVisualConnection(Transform other)
	{
		this.other = other;
		jointPosRelativeToOther = other.InverseTransformPoint(trigger.position);
	}

	public void Break()
	{
		JointBroken = true;
	}

	private void OnJointBreak(float breakForce)
	{
		JointBroken = true;
		FragmentVisualController.EmitJointBreakMarker(base.transform.position);
	}

	public override void OnSave(XDataHolder data)
	{
		data.Write("bmt-version", version);
		base.OnSave(data);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (isSimulating)
		{
			return;
		}
		if (!data.HasKey("bmt-version"))
		{
			if (data.WasLoadedFromFile)
			{
				version = 0;
				data.Write("bmt-version", version);
			}
		}
		else
		{
			version = data.ReadInt("bmt-version");
		}
		oldColliders.ForEach(delegate(BoxCollider x)
		{
			x.enabled = version == 0;
		});
		wheelCollider.enabled = version == 1;
		if ((bool)myJoint)
		{
			if (version == 0)
			{
				ConfigurableJoint configurableJoint = myJoint;
				ConfigurableJointMotion configurableJointMotion = ConfigurableJointMotion.Locked;
				myJoint.angularZMotion = configurableJointMotion;
				configurableJointMotion = configurableJointMotion;
				myJoint.angularYMotion = configurableJointMotion;
				configurableJoint.angularXMotion = configurableJointMotion;
			}
			else
			{
				ConfigurableJoint configurableJoint2 = myJoint;
				ConfigurableJointMotion configurableJointMotion = ConfigurableJointMotion.Free;
				myJoint.angularZMotion = configurableJointMotion;
				configurableJointMotion = configurableJointMotion;
				myJoint.angularYMotion = configurableJointMotion;
				configurableJoint2.angularXMotion = configurableJointMotion;
			}
		}
	}
}
