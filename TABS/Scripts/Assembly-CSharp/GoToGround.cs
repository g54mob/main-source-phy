using UnityEngine;

public class GoToGround : MonoBehaviour
{
	public LayerMask mapMask;

	public bool goOnAwake = true;

	private void Awake()
	{
		if (goOnAwake)
		{
			Go();
		}
	}

	public void Go()
	{
		Physics.Raycast(new Ray(base.transform.position, Vector3.down), out var hitInfo, 150f, mapMask);
		if ((bool)hitInfo.transform)
		{
			base.transform.position = hitInfo.point;
		}
	}
}
