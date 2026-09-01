using Landfall.TABS;
using UnityEngine;

public class ClockHandle : MonoBehaviour
{
	private Unit unit;

	private void Start()
	{
		unit = base.transform.GetComponentInParent<SetParent>().parentBefore.transform.root.GetComponent<Unit>();
	}

	private void Update()
	{
		base.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f, -360f, unit.damageDealt / 1200f));
	}
}
