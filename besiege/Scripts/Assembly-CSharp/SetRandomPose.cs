using UnityEngine;

public class SetRandomPose : MonoBehaviour
{
	public MeshFilter MeshFiltery;

	public Mesh[] poses;

	private void Awake()
	{
		if (!StatMaster.levelSimulating)
		{
			MeshFiltery.mesh = poses[Random.Range(0, poses.Length)];
		}
	}
}
