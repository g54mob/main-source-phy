using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAreaSpawner : MonoBehaviour
{
	public GameObject prefab;

	public float burstWait = 10f;

	public float minDelay;

	public float maxDelay = 1f;

	public int amount = 10;

	public float radius = 10f;

	public bool loop = true;

	public float minSize = 1f;

	public float maxSize = 2f;

	public bool crossFacingDir;

	public float elipseHeight = 10f;

	private List<GameObject> pool = new List<GameObject>();

	private void OnEnable()
	{
		StartCoroutine(Spawn());
	}

	private IEnumerator Spawn()
	{
		for (int i = 0; i < amount; i++)
		{
			float delay = Random.Range(minDelay, maxDelay);
			yield return new WaitForSeconds(delay);
			Vector3 pos = GetPosition();
			GameObject spawn = Object.Instantiate(prefab, pos, GetRotation(pos), base.transform) as GameObject;
			spawn.transform.localScale = Random.Range(minSize, maxSize) * Vector3.one;
			pool.Add(spawn);
		}
		yield return new WaitForSeconds(burstWait);
		while (loop)
		{
			for (int j = 0; j < amount; j++)
			{
				float delay2 = Random.Range(minDelay, maxDelay);
				yield return new WaitForSeconds(delay2);
				GameObject spawn2 = pool[j];
				Vector3 pos2 = GetPosition();
				spawn2.transform.position = pos2;
				spawn2.transform.rotation = GetRotation(pos2);
				spawn2.transform.localScale = Random.Range(minSize, maxSize) * Vector3.one;
				spawn2.SetActive(false);
				spawn2.SetActive(true);
			}
			yield return new WaitForSeconds(burstWait);
		}
		base.gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		for (int i = 0; i < pool.Count; i++)
		{
			GameObject obj = pool[i];
			Object.Destroy(obj);
		}
		pool.Clear();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = (Color.yellow + Color.red) / 2f;
		Gizmos.DrawWireSphere(base.transform.position, radius);
	}

	private Vector3 GetPosition()
	{
		Vector3 result = base.transform.position + Random.insideUnitSphere * radius;
		result.y = elipseHeight * ((result.y - base.transform.position.y) / radius) + base.transform.position.y;
		return result;
	}

	private Quaternion GetRotation(Vector3 pos)
	{
		if (crossFacingDir)
		{
			Vector3 vector = pos - base.transform.position;
			Vector3 vector2 = Vector3.Cross(Vector3.up, vector.normalized);
			return Quaternion.LookRotation(Vector3.Cross(Random.insideUnitSphere, vector2).normalized, vector2);
		}
		return Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
	}
}
