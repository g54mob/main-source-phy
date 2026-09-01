using UnityEngine;

public class ArrowVolley : MonoBehaviour
{
	public float metersAboveToSpawwnAt = 10f;

	public float radius = 3f;

	public float timeBetweenSpawns = 0.05f;

	public int totalSpawns = 25;

	public int arrowsPerSpawn = 2;

	private SpawnObject spawn;

	private void Start()
	{
		spawn = GetComponent<SpawnObject>();
		Debug.LogError("Commented out code in ArrowVolley. If this error is visible something might have to be done about it.");
	}
}
