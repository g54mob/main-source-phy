using System.Collections.Generic;
using UnityEngine;

public class IcicleCollisionReporter : MonoBehaviour
{
	public List<Icicles> iciclesList = new List<Icicles>();

	public float forceMinimum = 100f;

	private void OnCollisionEnter(Collision other)
	{
		if (!(other.relativeVelocity.sqrMagnitude > forceMinimum))
		{
			return;
		}
		for (int i = 0; i < iciclesList.Count; i++)
		{
			if ((bool)iciclesList[i] && iciclesList[i].enabled)
			{
				iciclesList[i].Hit();
			}
		}
	}
}
