using System.Collections.Generic;
using UnityEngine;

public class RightBarPositioner : MonoBehaviour
{
	[SerializeField]
	private GameObject[] observedGameObjects;

	[SerializeField]
	private float yOffset = 0.2f;

	private MeshRenderer[] observedMeshRenderers;

	private Bounds combinedBounds = default(Bounds);

	private Camera hudCamera;

	private void Awake()
	{
		hudCamera = GameObject.FindGameObjectWithTag("hudCamera").GetComponent<Camera>();
		if (observedGameObjects.Length <= 0 || !(hudCamera != null))
		{
			Object.Destroy(this);
			return;
		}
		CalculateOurBounds();
		SetupObservedMeshRenderers();
	}

	private void SetupObservedMeshRenderers()
	{
		List<MeshRenderer> list = new List<MeshRenderer>();
		GameObject[] array = observedGameObjects;
		foreach (GameObject gameObject in array)
		{
			list.AddRange(gameObject.GetComponentsInChildren<MeshRenderer>());
		}
		observedMeshRenderers = list.ToArray();
	}

	private void CalculateOurBounds()
	{
		MeshRenderer[] componentsInChildren = GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (i == 0)
			{
				combinedBounds = componentsInChildren[i].bounds;
			}
			else
			{
				combinedBounds.Encapsulate(componentsInChildren[i].bounds);
			}
		}
	}

	private void Reposition()
	{
		Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
		int num = 0;
		for (int i = 0; i < observedMeshRenderers.Length; i++)
		{
			Bounds bounds2 = observedMeshRenderers[i].bounds;
			if (observedMeshRenderers[i].enabled && observedMeshRenderers[i].gameObject.activeInHierarchy)
			{
				if (num++ == 0)
				{
					bounds = bounds2;
				}
				else
				{
					bounds.Encapsulate(bounds2);
				}
			}
		}
		float y = hudCamera.ScreenToWorldPoint(new Vector2(0f, 0f)).y;
		float num2 = combinedBounds.min.y - base.transform.position.y;
		float num3 = y - num2;
		if (combinedBounds.size == Vector3.zero)
		{
			base.transform.position = new Vector3(base.transform.position.x, num3 + yOffset, base.transform.position.z);
			return;
		}
		float num4 = Mathf.Abs(y - bounds.max.y);
		num3 += num4 + yOffset;
		base.transform.position = new Vector3(base.transform.position.x, num3, base.transform.position.z);
	}

	private void Update()
	{
		CalculateOurBounds();
		Reposition();
	}
}
