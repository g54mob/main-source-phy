using System;
using System.Collections;
using UnityEngine;

public class SplinterOnBreak : SimBehaviour
{
	[HideInInspector]
	public bool usingBreakableSkin = true;

	public Transform brokenBase;

	public Transform brokenTop;

	public Transform[] disableOnBreak;

	public int rotationAmount = 90;

	public Renderer mainVis;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating && base.SimPhysics)
		{
			StartCoroutine(StartSplinter());
		}
	}

	private IEnumerator StartSplinter()
	{
		rotationAmount *= Mathf.RoundToInt(UnityEngine.Random.Range(0, 4));
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		ConfigurableJoint configJoint = base.gameObject.GetComponent<ConfigurableJoint>();
		if (!(configJoint != null))
		{
			yield break;
		}
		Rigidbody body = configJoint.connectedBody;
		if (body != null)
		{
			BlockBehaviour block = body.GetComponent<BlockBehaviour>();
			if (!(block is TimedRocket) || !(block as TimedRocket).hasExploded)
			{
				brokenBase.parent = body.transform;
				block.visAddedToMe.Add(brokenBase.GetComponent<Renderer>());
			}
		}
	}

	private void OnJointBreak(float breakForce)
	{
		if (!usingBreakableSkin)
		{
			return;
		}
		for (int i = 0; i < disableOnBreak.Length; i++)
		{
			disableOnBreak[i].gameObject.SetActive(false);
		}
		brokenBase.gameObject.SetActive(true);
		brokenTop.gameObject.SetActive(true);
		brokenBase.GetComponent<Renderer>().material.SetFloat("_DamageAmount", mainVis.GetComponent<Renderer>().material.GetFloat("_DamageAmount"));
		brokenTop.GetComponent<Renderer>().material.SetFloat("_DamageAmount", mainVis.GetComponent<Renderer>().material.GetFloat("_DamageAmount"));
		if (StatMaster.isMP && base.SimPhysics)
		{
			NetworkBlock component = GetComponent<NetworkBlock>();
			if (component != null)
			{
				component.Event(NetworkEntity.EntityEvent.Break);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
	}

	private void SkinIsBreakable(bool b)
	{
		usingBreakableSkin = b;
	}

	private void OnDestroy()
	{
		if ((bool)brokenBase)
		{
			UnityEngine.Object.Destroy(brokenBase.gameObject);
		}
	}
}
