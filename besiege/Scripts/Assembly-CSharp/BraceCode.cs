using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[AddComponentMenu("Blocks/Block Behaviours/BraceCode")]
public class BraceCode : GenericDraggedBlock
{
	private const float CUBE_THRESHOLD = 0.001f;

	private const float CUBOID_THRESHOLD = 0.1f;

	private const float SCALED_THRESHOLD = 0.1f;

	private ConfigurableJoint[] createdJoints = new ConfigurableJoint[2];

	private bool destroying;

	protected ConfigurableJoint myJoint;

	private float destroyTime;

	private int jointNumber;

	[SerializeField]
	private BraceState setUpState;

	public static BraceState BraceType(Vector3 braceScale, float cylinderScaleY)
	{
		BraceState result = BraceState.Regular;
		if (cylinderScaleY < 0.001f)
		{
			result = BraceState.Cube;
		}
		else if (cylinderScaleY < 0.1f)
		{
			result = BraceState.Cuboid;
		}
		else if (ScaledBrace(braceScale))
		{
			result = BraceState.Scaled;
		}
		return result;
	}

	public static bool ScaledBrace(Vector3 braceScale)
	{
		return Mathf.Abs(braceScale.y - 1f) > 0.1f || Mathf.Abs(braceScale.z - 1f) > 0.1f;
	}

	protected override void Awake()
	{
		base.Awake();
		if (isSimulating && SimPhysics && !destroying)
		{
			myJoint = blockJoint as ConfigurableJoint;
		}
	}

	public override void OnAddRemote()
	{
		base.OnAddRemote();
		if (stripped && BraceType(base.transform.localScale, cylinder.localScale.y) == BraceState.Regular)
		{
			NetBlock.SetupBrace(this);
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, SimPhysics && Prefab.RegisterSimFixedUpdate, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (!SimPhysics)
		{
			return;
		}
		destroyTime += Time.deltaTime;
		if (destroyTime < 5f)
		{
			return;
		}
		if ((bool)blockJoint || (bool)createdJoints[0] || (bool)createdJoints[1])
		{
			if (!StatMaster.stressCoded)
			{
				_parentMachine.UnregisterUpdate(this, false);
			}
			destroying = false;
			return;
		}
		_parentMachine.UnregisterUpdate(this, false);
		if (StatMaster.isMP && SimPhysics && NetBlock != null)
		{
			NetBlock.Event(NetworkEntity.EntityEvent.Kill);
		}
		RemoveBrace();
	}

	public override void UpdateDragged()
	{
		if (!destroying)
		{
			base.UpdateDragged();
		}
	}

	public void OnJointBreak(float d)
	{
		if (SimPhysics && !IsDestroyed && !destroying)
		{
			destroying = true;
			destroyTime = 0f;
			_parentMachine.RegisterUpdate(this, false);
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
		}
	}

	private IEnumerator IEDestroyBrace()
	{
		Vector3 startPos = base.transform.position;
		for (float f = 0f; f <= 1f; f += Time.deltaTime)
		{
			base.transform.position = Vector3.Lerp(startPos, _parentMachine.MiddlePosition, f);
			yield return null;
		}
		destroying = false;
	}

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		if (SimPhysics && CheckJoints() == 0)
		{
			destroying = true;
		}
	}

	public void RemoveBrace()
	{
		isKinematic = true;
		if (!IsDestroyed)
		{
			if (!noRigidbody)
			{
				Rigidbody.isKinematic = true;
				Machine.RemoveBody(base.transform);
				noRigidbody = true;
			}
			base.gameObject.SetActive(false);
		}
		destroying = false;
		IsDestroyed = true;
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (!isSimulating || SimPhysics)
		{
			SetBraceCubeVisual();
		}
	}

	public void SetBraceCubeVisual()
	{
		BraceState braceState = BraceType(base.transform.localScale, cylinder.localScale.y);
		if (setUpState != braceState)
		{
			Renderer component = startPoint.GetComponent<Renderer>();
			Renderer component2 = endPoint.GetComponent<Renderer>();
			Renderer component3 = cylinder.GetComponent<Renderer>();
			switch (braceState)
			{
			case BraceState.Regular:
			case BraceState.Scaled:
				component.shadowCastingMode = ShadowCastingMode.Off;
				component2.shadowCastingMode = ShadowCastingMode.Off;
				component3.shadowCastingMode = ShadowCastingMode.On;
				component3.enabled = true;
				component2.enabled = true;
				break;
			case BraceState.Cuboid:
				component.shadowCastingMode = ShadowCastingMode.On;
				component2.shadowCastingMode = ShadowCastingMode.On;
				component3.shadowCastingMode = ShadowCastingMode.Off;
				component3.enabled = false;
				component2.enabled = true;
				break;
			case BraceState.Cube:
				component.shadowCastingMode = ShadowCastingMode.On;
				component2.shadowCastingMode = ShadowCastingMode.Off;
				component3.shadowCastingMode = ShadowCastingMode.Off;
				component3.enabled = false;
				component2.enabled = false;
				break;
			}
			setUpState = braceState;
		}
	}

	public bool CanCreateJoint(Rigidbody target, Vector3 anchorPoint)
	{
		if (target == null || target == Rigidbody)
		{
			return false;
		}
		if (target.GetComponent<BuildSurface>() != null)
		{
			CreateSimLists();
			BraceState braceState = setUpState;
			if (braceState == BraceState.Cube || braceState == BraceState.Cuboid)
			{
				for (int i = 0; i < iJointTo.Count; i++)
				{
					if (iJointTo[i] != null && iJointTo[i].connectedBody == target)
					{
						return false;
					}
				}
			}
			else
			{
				for (int j = 0; j < iJointTo.Count; j++)
				{
					Joint joint = iJointTo[j];
					if (joint != null && joint.connectedBody == target)
					{
						float sqrMagnitude = (anchorPoint - joint.anchor).sqrMagnitude;
						if (sqrMagnitude == 0f)
						{
							return false;
						}
					}
				}
			}
		}
		return true;
	}

	public ConfigurableJoint AddJointy(Rigidbody target)
	{
		if (blockJoint == null)
		{
			return null;
		}
		ConfigurableJoint configurableJoint = base.gameObject.AddComponent<ConfigurableJoint>();
		configurableJoint.anchor = endPoint.localPosition;
		configurableJoint.axis = myJoint.axis;
		configurableJoint.secondaryAxis = myJoint.secondaryAxis;
		configurableJoint.angularXMotion = myJoint.angularXMotion;
		configurableJoint.angularYMotion = myJoint.angularYMotion;
		configurableJoint.angularZMotion = myJoint.angularZMotion;
		configurableJoint.xMotion = myJoint.xMotion;
		configurableJoint.yMotion = myJoint.yMotion;
		configurableJoint.zMotion = myJoint.zMotion;
		configurableJoint.projectionMode = myJoint.projectionMode;
		configurableJoint.projectionDistance = myJoint.projectionDistance;
		configurableJoint.projectionAngle = myJoint.projectionAngle;
		configurableJoint.breakForce = myJoint.breakForce;
		configurableJoint.breakTorque = myJoint.breakTorque;
		configurableJoint.enablePreprocessing = false;
		configurableJoint.connectedBody = target;
		if (jointNumber < createdJoints.Length)
		{
			createdJoints[jointNumber] = configurableJoint;
		}
		CreateJointReferences(configurableJoint);
		jointNumber++;
		return configurableJoint;
	}

	public void DestroyJoint(Joint joint)
	{
		iJointTo.Remove(joint);
		if (joint.connectedBody != null)
		{
			BlockBehaviour component = joint.connectedBody.GetComponent<BlockBehaviour>();
			component.jointsToMe.Remove(joint);
		}
		Object.Destroy(joint);
	}

	public void CreateJointReferences(Joint aJoint)
	{
		CreateSimLists();
		iJointTo.Add(aJoint);
		BlockBehaviour blockBehaviour = ((!(aJoint.connectedBody != null)) ? null : aJoint.connectedBody.GetComponent<BlockBehaviour>());
		if (blockBehaviour != null)
		{
			blockBehaviour.CreateSimLists();
			blockBehaviour.jointsToMe.Add(aJoint);
		}
	}
}
