using UnityEngine;

public class FireRaycaster : MonoBehaviour
{
	public GameObject min;

	public GameObject max;

	public LayerMask mask;

	public float offset = 2f;

	private int number = 5;

	private float currentNumber;

	private bool cantFindGround;

	private void Start()
	{
		DoRay(base.transform.position);
		DoRay(base.transform.position + base.transform.up * offset);
		DoRay(base.transform.position - base.transform.up * offset);
		DoRay(base.transform.position + base.transform.right * offset);
		DoRay(base.transform.position - base.transform.right * offset);
		if (!cantFindGround)
		{
			if ((double)currentNumber >= 0.9)
			{
				max.SetActive(value: true);
			}
			else
			{
				min.SetActive(value: true);
			}
		}
	}

	private void DoRay(Vector3 pos)
	{
		pos += base.transform.forward * 1f;
		Physics.Raycast(new Ray(pos, -base.transform.forward), out var hitInfo, 2f);
		if ((bool)hitInfo.transform)
		{
			if ((bool)hitInfo.rigidbody)
			{
				GoDown();
			}
			currentNumber += 1f / (float)number;
		}
	}

	private void GoDown()
	{
		Physics.Raycast(new Ray(base.transform.position, Vector3.down), out var hitInfo, 3f, mask);
		if ((bool)hitInfo.transform)
		{
			base.transform.SetPositionAndRotation(hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
		}
		else
		{
			cantFindGround = true;
		}
	}
}
