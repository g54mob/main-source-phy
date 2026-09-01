using UnityEngine;

public class MoveToHandsWhenGrabbed : MonoBehaviour
{
	private void Start()
	{
		GetComponentInParent<Holdable>().moveTo = this;
	}
}
