using UnityEngine;

public interface ITriggerHandler
{
	int indexInScene { get; }

	Transform transform { get; }

	Object asObject { get; }

	void DoOnTriggerStay(Collider other, bool enter);
}
