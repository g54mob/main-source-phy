using System;
using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint (Wire)")]
public class TriggerSetJointWire : MonoBehaviour
{
	public int layerToCheck = 12;

	public int layerToCheck2 = 14;

	private Transform myParent;

	private Machine machine;

	[NonSerialized]
	private bool isInitialized;

	private void Start()
	{
		Init();
	}

	private void Init()
	{
		if (!isInitialized)
		{
			machine = GetComponentInParent<Machine>();
			myParent = base.transform.parent;
			isInitialized = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Init();
		if (!(machine == null) && machine.SimPhysics && machine.isSimulating)
		{
			int layer = other.gameObject.layer;
			Transform parent = other.transform.parent;
			if (layer == layerToCheck || (layer == layerToCheck2 && parent != myParent))
			{
				base.transform.parent = other.attachedRigidbody.transform;
				UnityEngine.Object.Destroy(this);
			}
		}
	}
}
