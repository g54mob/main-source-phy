using System;
using System.Collections;
using UnityEngine;

public class SandParticles : MonoBehaviour
{
	protected ParticleSystem.EmitParams emitter = default(ParticleSystem.EmitParams);

	private Vector3 offset = new Vector3(0f, 2.5f, 0f);

	private float timeToIgnoreCollisions = 0.5f;

	private bool allowCollisionHandling;

	private Coroutine waitAfterSim;

	private void Awake()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelStateChange));
	}

	private void OnLevelStateChange(bool toggle)
	{
		if (toggle)
		{
			allowCollisionHandling = false;
			if (StatMaster.isMP)
			{
				waitAfterSim = ReferenceMaster.Instance.StartCoroutine(WaitAfterSim());
			}
			else
			{
				waitAfterSim = StartCoroutine(WaitAfterSim());
			}
		}
		else if (waitAfterSim != null)
		{
			StopCoroutine(waitAfterSim);
			waitAfterSim = null;
		}
	}

	private IEnumerator WaitAfterSim()
	{
		yield return new WaitForSeconds(timeToIgnoreCollisions);
		allowCollisionHandling = true;
		waitAfterSim = null;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!WaterController.Exist || !allowCollisionHandling)
		{
			return;
		}
		float num = collision.relativeVelocity.sqrMagnitude * 0.002f;
		if (num > 0.01f && collision.contacts.Length > 0)
		{
			Vector3 position = collision.contacts[0].point + offset;
			if (WaterController.waterTransformHeight > position.y + 7.5f)
			{
				emitter.applyShapeToPosition = true;
				emitter.position = position;
				emitter.startColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)(255f * num * UnityEngine.Random.Range(0.8f, 0.6f)));
				emitter.startSize = UnityEngine.Random.Range(2f, 4f) * Mathf.Clamp01(num);
				GlobalParticles.EmitParticleBursts(10, emitter);
			}
		}
	}

	private void OnDestroy()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelStateChange));
	}
}
