using Landfall.TABS;
using UnityEngine;

public class UnitDontWalkFor : MonoBehaviour
{
	public float time;

	public void Go()
	{
		GetComponentInParent<Unit>().data.DontWalkFor(time);
	}
}
