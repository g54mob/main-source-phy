using Landfall.TABS;
using UnityEngine;
using UnityEngine.Events;

public class ConditionSpeed : MonoBehaviour
{
	public UnityEvent eventToCall;

	public float threshold = 5f;

	private Rigidbody rig;

	private void Start()
	{
		Unit component = base.transform.root.GetComponent<Unit>();
		if ((bool)component)
		{
			rig = component.data.mainRig;
		}
	}

	public void Go()
	{
		if ((bool)rig && !(rig.velocity.magnitude < threshold))
		{
			eventToCall.Invoke();
		}
	}
}
