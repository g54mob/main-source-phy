using UnityEngine;

[DisallowMultipleComponent]
public class MapRevealEntityFilterSuppressor : MonoBehaviour
{
	public GameObject[] RevealVisuals;

	public bool DisableIfAnyEntityMatches;

	public FilterEntitySet EntityFilter;

	public bool RevealVisualsBlocked { get; private set; }

	private void OnEnable()
	{
	}

	public void EvaluateEntityFilter()
	{
	}

	private void SetRevealVisualsActive(bool active)
	{
	}
}
