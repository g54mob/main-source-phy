using System;
using System.Collections;
using UnityEngine;

public class IceTag : MonoBehaviour
{
	public bool takesDamage;

	public SaveableDataHolder SDH;

	public float blackLerpSpeed = 0.5f;

	[HideInInspector]
	public bool frozen;

	[HideInInspector]
	public Machine machine;

	[HideInInspector]
	public BlockVisualController bvc;

	private BlockBehaviour block;

	private bool isBlock;

	private GenericEntity entity;

	private ServerMachine serverMachine;

	public void Start()
	{
		if (SDH == null)
		{
			SDH = GetComponent<SaveableDataHolder>();
		}
		block = SDH as BlockBehaviour;
		isBlock = block != null;
		if (isBlock)
		{
			machine = block._parentMachine;
			if (StatMaster.isHosting && SDH.SimPhysics && (bool)machine)
			{
				serverMachine = machine as ServerMachine;
			}
			bvc = block.VisualController;
		}
	}

	public void Freeze()
	{
		if (!SDH.isSimulating || frozen)
		{
			return;
		}
		if (isBlock && StatMaster.isMP && SDH.SimPhysics)
		{
			if (block.NetBlock != null)
			{
				block.NetBlock.Event(NetworkEntity.EntityEvent.Freeze);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		frozen = true;
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine(FadeIce());
		}
	}

	private void DisableScripts()
	{
		if (isBlock && !machine.UnbreakableMode)
		{
			block.FreezeMe();
		}
	}

	private IEnumerator FadeIce()
	{
		if (bvc == null || !block.isSimulating)
		{
			yield break;
		}
		float cTime = 0f;
		float rate = 1f / blackLerpSpeed;
		float bT = 0f;
		float bF = 0f;
		if (isBlock && block.blockJoint != null)
		{
			bT = block.blockJoint.breakTorque;
			bF = block.blockJoint.breakForce;
		}
		float endF = UnityEngine.Random.Range(0.1f, 1f);
		float endT = UnityEngine.Random.Range(0.1f, 1f);
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			if (isBlock)
			{
				if (bvc != null)
				{
					bvc.SetFrozenLevel(cTime);
				}
				if (takesDamage && SDH.SimPhysics && !machine.UnbreakableMode && block.blockJoint != null)
				{
					block.blockJoint.breakForce = Mathf.Lerp(bF, endF, cTime);
					block.blockJoint.breakTorque = Mathf.Lerp(bT, endT, cTime);
				}
			}
			yield return null;
		}
		if (isBlock && takesDamage && StatMaster.isHosting && SDH.SimPhysics && (bool)machine)
		{
			serverMachine.ApplyDamage(block, MachineDamageType.Freeze);
		}
		if (isBlock && takesDamage && block.isParented && SDH.SimPhysics && !machine.UnbreakableMode)
		{
			block.jointBreakForce = 0f;
			block.StartCoroutine(block.VirtualJointBreakExplosion(block.Rigidbody.velocity));
		}
		DisableScripts();
		if (!block.noRigidbody)
		{
			block.Rigidbody.drag = 0f;
		}
	}
}
