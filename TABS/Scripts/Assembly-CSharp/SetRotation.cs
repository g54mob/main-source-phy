using UnityEngine;

public class SetRotation : MonoBehaviour
{
	public enum Dir
	{
		Up = 0,
		Forward = 1
	}

	public Dir dir;

	public void Go()
	{
		if (dir == Dir.Up)
		{
			base.transform.rotation = Quaternion.LookRotation(Vector3.up);
		}
		if (dir == Dir.Forward)
		{
			base.transform.rotation = Quaternion.LookRotation(Vector3.forward);
		}
	}
}
