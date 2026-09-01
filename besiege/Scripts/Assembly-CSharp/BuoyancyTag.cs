using UnityEngine;

public class BuoyancyTag : MonoBehaviour
{
	public ConstantForce myForce;

	private void Start()
	{
		OutOfWater();
	}

	public void InWater()
	{
		myForce.enabled = true;
	}

	public void OutOfWater()
	{
		myForce.enabled = false;
	}
}
