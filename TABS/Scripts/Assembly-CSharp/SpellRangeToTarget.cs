using UnityEngine;

public class SpellRangeToTarget : MonoBehaviour
{
	private void Start()
	{
		SpellSequence component = GetComponent<SpellSequence>();
		float distance = GetComponent<SpellTarget>().distance;
		component.spacing = distance / (float)component.instances.Length;
	}
}
