using UnityEngine;
using UnityEngine.Events;

public class IfDualWielding : MonoBehaviour
{
	private Holdable holdable;

	private HoldingHandler holdingHandler;

	public UnityEvent dualWieldEvent;

	private void Start()
	{
		holdable = GetComponentInParent<Holdable>();
		holdingHandler = holdable.holdingHandler;
		if ((holdingHandler.leftHandActivity == HoldingHandler.HandActivity.HoldingLeftObject || holdingHandler.leftHandActivity == HoldingHandler.HandActivity.NotHolding) && (holdingHandler.rightHandActivity == HoldingHandler.HandActivity.HoldingRightObject || holdingHandler.leftHandActivity == HoldingHandler.HandActivity.NotHolding))
		{
			dualWieldEvent.Invoke();
		}
	}
}
