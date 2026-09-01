using UnityEngine;

[AddComponentMenu("Unused/Wire Controller")]
public class WireController : SimBehaviour
{
	public WireEndPoint startPoint;

	public WireEndPoint endPoint;

	public bool isWireOn;

	private void Update()
	{
		if (base.isSimulating)
		{
			if (isWireOn)
			{
				startPoint.WireOn();
				endPoint.WireOn();
			}
			else
			{
				startPoint.WireOff();
				endPoint.WireOff();
			}
		}
	}

	public void WireOn()
	{
		isWireOn = true;
	}

	public void WireOff()
	{
		isWireOn = false;
	}
}
