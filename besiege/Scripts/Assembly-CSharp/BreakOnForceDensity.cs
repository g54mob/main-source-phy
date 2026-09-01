using UnityEngine;

[AddComponentMenu("Destruction/Break On Force Density")]
public class BreakOnForceDensity : BreakOnForce
{
	public Sink[] objectsThatShouldSink;

	public override Transform BreakObj()
	{
		if (!CanDie || !base.enabled)
		{
			return null;
		}
		Init();
		isBroken = true;
		CanDie = false;
		if (BreakInto == null)
		{
			Debug.LogWarning("BreakInto is null (" + Machine.GetObjectPath(base.gameObject) + ")!");
			return null;
		}
		BrokenInstance = Object.Instantiate(BreakInto, base.transform.position, GetBreakRotation()) as Transform;
		if (BrokenInstance == null)
		{
			return null;
		}
		ParticleSystem[] componentsInChildren = BrokenInstance.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (StatMaster.isMP && NetworkBlock.applyingState)
			{
				Object.Destroy(componentsInChildren[i].gameObject);
			}
			else if (componentsInChildren[i].playOnAwake)
			{
				componentsInChildren[i].Stop();
				componentsInChildren[i].Clear();
				componentsInChildren[i].randomSeed = (uint)Random.Range(0, 9999999);
				componentsInChildren[i].Play();
			}
		}
		SinkRelated();
		DropSupports();
		SetParent(BrokenInstance);
		if (visCopyMaterialFrom != null)
		{
			CopyMaterial component = BrokenInstance.GetComponent<CopyMaterial>();
			if (component != null)
			{
				component.CopyMat(visCopyMaterialFrom);
			}
		}
		AddToPercentageBar();
		DestroyObjects();
		if (colHook != null)
		{
			colHook.gameObject.SetActive(false);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
		SendBreakEvent();
		return BrokenInstance;
	}

	private void SinkRelated()
	{
		for (int i = 0; i < objectsThatShouldSink.Length; i++)
		{
			if (objectsThatShouldSink[i] != null)
			{
				objectsThatShouldSink[i].SinkObject();
			}
		}
	}
}
