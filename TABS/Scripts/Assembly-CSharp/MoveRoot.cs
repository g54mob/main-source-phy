using Landfall.TABS;
using UnityEngine;

public class MoveRoot : MonoBehaviour
{
	public Vector3 worldDelta;

	public bool playOnAwake;

	private void Awake()
	{
		if (playOnAwake)
		{
			Go();
		}
	}

	public void Go()
	{
		base.transform.root.GetComponent<Unit>().Hip.parent.position += worldDelta;
		base.transform.position += worldDelta;
	}
}
