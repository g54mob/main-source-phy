using System.Collections.Generic;
using UnityEngine;

public class CamToggleControl : MonoBehaviour
{
	private List<GameObject> objectsThatRequireCam = new List<GameObject>();

	public GameObject camToEnable;

	private Camera cam;

	public Camera Cam
	{
		get
		{
			if (object.ReferenceEquals(cam, null))
			{
				Awake();
			}
			return cam;
		}
	}

	private void Awake()
	{
		cam = camToEnable.GetComponent<Camera>();
	}

	public void AddObject(GameObject obj)
	{
		objectsThatRequireCam.Add(obj);
		if ((bool)camToEnable && !camToEnable.activeSelf)
		{
			camToEnable.SetActive(true);
		}
	}

	public void RemoveObject(GameObject obj)
	{
		if (objectsThatRequireCam.Contains(obj))
		{
			objectsThatRequireCam.Remove(obj);
			if ((bool)camToEnable && camToEnable.activeSelf && objectsThatRequireCam.Count <= 0)
			{
				camToEnable.SetActive(false);
			}
		}
	}
}
