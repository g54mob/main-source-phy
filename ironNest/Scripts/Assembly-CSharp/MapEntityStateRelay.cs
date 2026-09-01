using System.Collections.Generic;
using UnityEngine;

public class MapEntityStateRelay : MonoBehaviour
{
	public MapEntityStates State;

	public bool DisableIfActive;

	public List<GameObject> GameObjects;

	public List<Behaviour> Components;

	private EntityLocation entity;

	public void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void UpdateVisauls()
	{
	}
}
