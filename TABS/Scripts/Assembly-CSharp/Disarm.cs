using UnityEngine;

public class Disarm : MonoBehaviour
{
	private Holdable[] holdable;

	private void Start()
	{
		holdable = base.transform.root.GetComponentsInChildren<Holdable>();
	}

	public void DisarmUnit()
	{
		if (holdable == null)
		{
			holdable = base.transform.root.GetComponentsInChildren<Holdable>();
		}
		for (int i = 0; i < holdable.Length; i++)
		{
			holdable[i].Dissarm();
		}
	}
}
