using UnityEngine;

public class MeshSwapLoop_Desert : MonoBehaviour
{
	public Mesh[] meshes;

	public MeshFilter meshFilter;

	public int animationdirection;

	public int activeMesh;

	public float animateRate = 0.2f;

	private void Start()
	{
		activeMesh = Random.Range(0, meshes.Length - 1);
		meshFilter.mesh = meshes[activeMesh];
		animationdirection = 1;
		InvokeRepeating("Animate", Random.Range(0f, animateRate), animateRate);
	}

	private void Animate()
	{
		if (activeMesh == meshes.Length - 1)
		{
			animationdirection = -1;
		}
		else if (activeMesh == 0)
		{
			animationdirection = 1;
		}
		activeMesh += animationdirection;
		meshFilter.mesh = meshes[activeMesh];
	}
}
