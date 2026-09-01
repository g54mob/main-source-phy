using UnityEngine;

public class MeshSwapLoop : MonoBehaviour
{
	public Mesh[] meshes;

	public MeshFilter meshFilter;

	public int activeMesh;

	public float animateRate = 0.2f;

	private void Start()
	{
		InvokeRepeating("Animate", Random.Range(0f, animateRate), animateRate);
		activeMesh = Random.Range(0, meshes.Length);
		meshFilter.mesh = meshes[activeMesh];
	}

	private void Animate()
	{
		if (activeMesh == 1)
		{
			activeMesh = 0;
		}
		else
		{
			activeMesh = 1;
		}
		meshFilter.mesh = meshes[activeMesh];
	}
}
