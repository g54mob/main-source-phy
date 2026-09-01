using System.Collections.Generic;
using UnityEngine;

public class MyNeighbours : MonoBehaviour
{
	public List<Transform> neighbours;

	private void AddNeighbour(Transform obj)
	{
		neighbours.Add(obj);
	}
}
