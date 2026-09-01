using System.Collections.Generic;
using UnityEngine;

public class ScatterRoot : MonoBehaviour
{
	[HideInInspector]
	public List<ScatteredObject> scatteredObjects;

	private void Awake()
	{
		scatteredObjects.Clear();
	}
}
