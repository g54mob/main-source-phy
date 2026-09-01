using UnityEngine;

[AddComponentMenu("Unused/Wire End Point")]
public class WireEndPoint : SimBehaviour
{
	public WireController WireControllerCode;

	public bool endJoint;

	public Transform joinedObj;

	public int layerToCheck = 12;

	public int layerToCheck2 = 14;

	public WirePower wirePower;

	private void OnTriggerEnter(Collider other)
	{
		if (base.isSimulating && ((joinedObj == null && other.gameObject.layer == layerToCheck) || (other.gameObject.layer == layerToCheck2 && other.transform.parent != base.transform.parent)))
		{
			joinedObj = other.attachedRigidbody.transform;
			if ((bool)joinedObj.GetComponent<WirePower>())
			{
				wirePower = joinedObj.GetComponent<WirePower>();
			}
		}
	}

	public void WireOn()
	{
		joinedObj.SendMessage("setActiveFunction", 1, SendMessageOptions.DontRequireReceiver);
	}

	public void WireOff()
	{
		joinedObj.SendMessage("setActiveFunction", 0, SendMessageOptions.DontRequireReceiver);
	}

	private void Update()
	{
		if (base.isSimulating && wirePower != null)
		{
			if (wirePower.powerOn)
			{
				WireControllerCode.WireOn();
			}
			else
			{
				WireControllerCode.WireOff();
			}
		}
	}
}
