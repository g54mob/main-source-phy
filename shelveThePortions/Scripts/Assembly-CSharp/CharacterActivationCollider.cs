using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActivationCollider : MonoBehaviour
{
	[SerializeField]
	private int optimizationPerFrame = 25;

	private readonly Queue<IOptimizable> activateQueue = new Queue<IOptimizable>();

	private readonly Queue<IOptimizable> deactivateQueue = new Queue<IOptimizable>();

	private readonly HashSet<IOptimizable> activateSet = new HashSet<IOptimizable>();

	private readonly HashSet<IOptimizable> deactivateSet = new HashSet<IOptimizable>();

	private void Start()
	{
		StartCoroutine(ProcessQueue());
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<IOptimizable>(out var component))
		{
			deactivateSet.Remove(component);
			if (activateSet.Add(component))
			{
				activateQueue.Enqueue(component);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<IOptimizable>(out var component))
		{
			activateSet.Remove(component);
			if (deactivateSet.Add(component))
			{
				deactivateQueue.Enqueue(component);
			}
		}
	}

	private IEnumerator ProcessQueue()
	{
		while (true)
		{
			int num = 0;
			while (num < optimizationPerFrame && activateQueue.Count > 0)
			{
				IOptimizable optimizable = activateQueue.Dequeue();
				if (activateSet.Remove(optimizable))
				{
					optimizable.SetOptimizationSetting(flag: true);
					num++;
				}
			}
			while (num < optimizationPerFrame && deactivateQueue.Count > 0)
			{
				IOptimizable optimizable2 = deactivateQueue.Dequeue();
				if (deactivateSet.Remove(optimizable2))
				{
					optimizable2.SetOptimizationSetting(flag: false);
					num++;
				}
			}
			yield return null;
		}
	}
}
