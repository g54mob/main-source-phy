using System.Collections.Generic;
using INab.Common;
using UnityEngine;

public class InteractiveDissolveTLDR : MonoBehaviour
{
	public List<InteractiveEffect> interactiveEffects;

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			PlayEffectsInTheScene();
		}
	}

	public void PlayEffectsInTheScene()
	{
		foreach (InteractiveEffect interactiveEffect in interactiveEffects)
		{
			if ((bool)interactiveEffect)
			{
				interactiveEffect.PlayEffect();
			}
		}
	}
}
