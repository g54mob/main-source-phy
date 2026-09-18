using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class EnvironmentManager : SceneSingleton<EnvironmentManager>
{
	[SerializeField]
	private List<GameObject> roofObjects = new List<GameObject>();

	[SerializeField]
	private List<GameObject> environmentObjects = new List<GameObject>();

	[SerializeField]
	private List<GameObject> lightObjects = new List<GameObject>();

	[Space]
	[SerializeField]
	private GameObject batsParticle;

	private void Start()
	{
		ActivateAll();
		UpdateBats();
	}

	public void UpdateBats()
	{
		batsParticle.SetActive(!SaveSystem.GetDisableBatsSetting());
	}

	[Button]
	private void ActivateAll()
	{
		foreach (GameObject environmentObject in environmentObjects)
		{
			environmentObject.SetActive(value: true);
		}
	}

	[Button]
	public void HideAllRoofObjects()
	{
		foreach (GameObject roofObject in roofObjects)
		{
			roofObject.SetActive(value: false);
		}
	}

	[Button]
	public void DisableLights()
	{
		foreach (GameObject lightObject in lightObjects)
		{
			lightObject.SetActive(value: false);
		}
	}
}
