using Landfall.TABS;
using UnityEngine;
using UnityEngine.Events;

public class AttackTrigger : MonoBehaviour
{
	public UnityEvent eventToTrigger;

	private void Start()
	{
		base.transform.root.GetComponent<Unit>().AddAttackAction(Trigger);
	}

	public void Trigger(Vector3 v1, Rigidbody r, Vector3 v2)
	{
		eventToTrigger.Invoke();
	}
}
