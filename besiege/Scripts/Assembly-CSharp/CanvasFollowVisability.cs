using System.Collections.Generic;
using UnityEngine;

public class CanvasFollowVisability : MonoBehaviour
{
	public static List<GameObject> activators = new List<GameObject>();

	public static List<GameObject> deactivators = new List<GameObject>();

	public bool inverse = true;

	public GameObject canvas;

	public static void Reset()
	{
		activators.Clear();
		deactivators.Clear();
	}

	private void OnEnable()
	{
		if (inverse)
		{
			deactivators.Add(base.gameObject);
		}
		else
		{
			activators.Add(base.gameObject);
		}
		Evaluate();
	}

	private void OnDisable()
	{
		if (inverse)
		{
			deactivators.Remove(base.gameObject);
		}
		else
		{
			activators.Remove(base.gameObject);
		}
		Evaluate();
	}

	private void Evaluate()
	{
		if (!(canvas == null))
		{
			if (activators.Count > 0)
			{
				canvas.SetActive(true);
			}
			else if (deactivators.Count > 0)
			{
				canvas.SetActive(false);
			}
			else
			{
				canvas.SetActive(true);
			}
		}
	}
}
