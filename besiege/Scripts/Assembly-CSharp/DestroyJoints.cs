using UnityEngine;

public class DestroyJoints : MonoBehaviour
{
	public Joint[] joints = new Joint[0];

	private void Start()
	{
		for (int i = 0; i < joints.Length; i++)
		{
			if ((bool)joints[i])
			{
				Object.Destroy(joints[i]);
			}
		}
		joints = new Joint[0];
	}
}
