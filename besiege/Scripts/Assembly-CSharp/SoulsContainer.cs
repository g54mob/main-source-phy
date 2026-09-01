using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsContainer : MonoBehaviour
{
	public static bool hasInstance;

	public static SoulsContainer instance;

	public GameObject target;

	public Light lightSource;

	public MeshRenderer[] levelEnablers = new MeshRenderer[0];

	public int aiDeadToComplete = 20;

	private int aiDead;

	public float distanceToActivate = 25f;

	public AnimationCurve lift = AnimationCurve.Linear(0f, 0f, 1f, 0f);

	public float height = 10f;

	private bool playingAnim;

	private bool campaignComplete;

	private BlockBehaviour sourceCube;

	private float lightIntensity = 1f;

	private void Start()
	{
		hasInstance = true;
		instance = this;
		campaignComplete = true;
		for (int i = 0; i < levelEnablers.Length; i++)
		{
			bool flag = LEVELLORD.levelsComplete[i] == 1;
			levelEnablers[i].enabled = flag;
			if (!flag)
			{
				campaignComplete = false;
			}
		}
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(StartLevel));
		distanceToActivate *= distanceToActivate;
		lightIntensity = lightSource.intensity;
	}

	private void OnDestroy()
	{
		if (instance == this)
		{
			hasInstance = false;
		}
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(StartLevel));
	}

	private void StartLevel(bool sim)
	{
		if (sim)
		{
			StartCoroutine(IEStartLevel());
			return;
		}
		lightSource.enabled = false;
		target.SetActive(false);
		playingAnim = false;
		aiDead = 0;
	}

	private IEnumerator IEStartLevel()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		foreach (BlockBehaviour block in Machine.Active().SimulationBlocks)
		{
			if (block.BlockID == 0)
			{
				sourceCube = block;
				break;
			}
		}
	}

	public void HarvestSoul()
	{
		aiDead++;
	}

	private void Update()
	{
		if (campaignComplete && !playingAnim && aiDead >= aiDeadToComplete && sourceCube != null && (base.transform.position - sourceCube.transform.position).sqrMagnitude < distanceToActivate)
		{
			StartCoroutine(Fill(5f));
			playingAnim = true;
		}
	}

	private IEnumerator Fill(float duration)
	{
		if (sourceCube == null)
		{
			yield break;
		}
		SingleInstanceFindOnly<MouseOrbit>.Instance.FocusBlock(sourceCube);
		List<BlockBehaviour> blocks = new List<BlockBehaviour>();
		GrassFireBlock(sourceCube, ref blocks);
		yield return new WaitForFixedUpdate();
		if (sourceCube == null)
		{
			yield break;
		}
		foreach (BlockBehaviour block in blocks)
		{
			if ((bool)block && !block.noRigidbody)
			{
				block.Rigidbody.useGravity = false;
			}
		}
		yield return new WaitForFixedUpdate();
		if (sourceCube == null)
		{
			yield break;
		}
		List<Vector3> pos = new List<Vector3>();
		foreach (BlockBehaviour block2 in blocks)
		{
			if ((bool)block2 && !block2.noRigidbody)
			{
				pos.Add(block2.Rigidbody.position - sourceCube.Rigidbody.position);
			}
		}
		if (pos.Count == 0)
		{
			pos.Add(Vector3.zero);
		}
		foreach (BlockBehaviour block3 in blocks)
		{
			if (!block3 || block3.noRigidbody)
			{
				continue;
			}
			foreach (Joint joint in block3.iJointTo)
			{
				float breakForce = (joint.breakTorque = 0f);
				joint.breakForce = breakForce;
			}
			block3.iJointTo.Clear();
			block3.jointsToMe.Clear();
			block3.Rigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * 1000f);
			SoundOnCollide junk = block3.GetComponent<SoundOnCollide>();
			if ((bool)junk)
			{
				UnityEngine.Object.Destroy(junk.particles);
				UnityEngine.Object.Destroy(junk);
			}
		}
		yield return new WaitForFixedUpdate();
		Vector3 startPos = sourceCube.Rigidbody.position;
		float dot = Vector3.Dot((base.transform.position - startPos).normalized, Vector3.up);
		if (dot < 0f)
		{
			dot = 0f;
		}
		float count = blocks.Count - 1;
		Vector3 offset = Vector3.forward * (count / (float)Math.PI + 1f);
		for (float t = 0f; t < duration; t += Time.fixedDeltaTime)
		{
			float pct = t / duration;
			Vector3 lerp = Vector3.Lerp(startPos, base.transform.position, pct) + lift.Evaluate(pct) * Vector3.up * height * dot * 1.4f;
			if (!blocks[0].noRigidbody)
			{
				blocks[0].Rigidbody.MovePosition(pos[0] + lerp);
			}
			lerp += lift.Evaluate(pct) * Vector3.down * 2f;
			for (int i = 1; i < blocks.Count; i++)
			{
				if ((bool)blocks[i] && !blocks[i].noRigidbody)
				{
					blocks[i].Rigidbody.MovePosition(Vector3.Lerp(pos[i] + lerp, lerp + Quaternion.AngleAxis((float)i * 360f / count, Vector3.up) * offset, pct * pct));
				}
			}
			yield return new WaitForFixedUpdate();
		}
		for (float t2 = 0f; t2 < 2f; t2 += Time.fixedDeltaTime)
		{
			Vector3 lerp2 = base.transform.position;
			if (!blocks[0].noRigidbody)
			{
				blocks[0].Rigidbody.MovePosition(pos[0] + lerp2);
			}
			for (int j = 1; j < blocks.Count; j++)
			{
				if ((bool)blocks[j] && !blocks[j].noRigidbody)
				{
					blocks[j].Rigidbody.MovePosition(lerp2 + Quaternion.AngleAxis((float)j * 360f / count, Vector3.up) * offset);
				}
			}
			yield return new WaitForFixedUpdate();
		}
		foreach (BlockBehaviour block4 in blocks)
		{
			if (!block4 || block4.noRigidbody)
			{
				continue;
			}
			if (block4.BlockID != 0)
			{
				block4.Rigidbody.AddExplosionForce(3000f, base.transform.position, 50f, 2f);
				block4.Rigidbody.useGravity = true;
				continue;
			}
			foreach (Renderer mesh in block4.visAddedToMe)
			{
				UnityEngine.Object.Destroy(mesh.gameObject);
			}
		}
		lightSource.intensity = 0f;
		lightSource.enabled = true;
		for (float t3 = 0f; t3 < 1f; t3 += Time.fixedDeltaTime)
		{
			lightSource.intensity = t3 * lightIntensity;
			if ((bool)sourceCube && !sourceCube.noRigidbody)
			{
				sourceCube.Rigidbody.MovePosition(base.transform.position);
			}
			yield return new WaitForFixedUpdate();
		}
		target.SetActive(true);
		while (StatMaster.levelSimulating)
		{
			if ((bool)sourceCube && !sourceCube.noRigidbody)
			{
				sourceCube.Rigidbody.MovePosition(base.transform.position);
			}
			yield return new WaitForFixedUpdate();
		}
	}

	private void GrassFireBlock(BlockBehaviour block, ref List<BlockBehaviour> list)
	{
		if (block == null || list.Contains(block))
		{
			return;
		}
		list.Add(block);
		block.CreateSimLists();
		foreach (Joint item in block.jointsToMe)
		{
			if (!item)
			{
				continue;
			}
			Rigidbody component = item.GetComponent<Rigidbody>();
			if (!(component == null))
			{
				BlockBehaviour component2 = component.GetComponent<BlockBehaviour>();
				if (!list.Contains(component2))
				{
					GrassFireBlock(component2, ref list);
				}
			}
		}
		foreach (Joint item2 in block.iJointTo)
		{
			if (!(item2.connectedBody == null))
			{
				BlockBehaviour component3 = item2.connectedBody.GetComponent<BlockBehaviour>();
				if (!list.Contains(component3))
				{
					GrassFireBlock(component3, ref list);
				}
			}
		}
	}
}
