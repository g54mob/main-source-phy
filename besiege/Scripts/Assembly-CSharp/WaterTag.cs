using UnityEngine;

[AddComponentMenu("Water/Objects/Water Tag")]
public class WaterTag : MonoBehaviour
{
	public float waterAmount;

	public FireTag fireTagCode;

	public FireController fireControllerCode;

	private void Start()
	{
		fireControllerCode = fireTagCode.fireControllerCode;
	}

	private void WaterHit()
	{
		fireControllerCode.DouseFire();
	}
}
