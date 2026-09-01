using UnityEngine;
using UnityEngine.Events;

public class InvokeEventDependingOnHand : MonoBehaviour
{
	private Holdable holdable;

	private HoldingHandler holdingHandler;

	private GameObject thisWeapon;

	public UnityEvent rightHandEvent;

	public UnityEvent leftHandEvent;

	private void Start()
	{
		holdable = GetComponentInParent<Holdable>();
		holdingHandler = holdable.holdingHandler;
		thisWeapon = base.transform.GetComponentInParent<Weapon>().gameObject;
		if ((holdingHandler.leftHandActivity == HoldingHandler.HandActivity.HoldingLeftObject || holdingHandler.leftHandActivity == HoldingHandler.HandActivity.NotHolding) && (holdingHandler.rightHandActivity == HoldingHandler.HandActivity.HoldingRightObject || holdingHandler.leftHandActivity == HoldingHandler.HandActivity.NotHolding))
		{
			if ((bool)holdingHandler.leftObject && thisWeapon == holdingHandler.leftObject.gameObject)
			{
				leftHandEvent?.Invoke();
			}
			if ((bool)holdingHandler.rightObject && thisWeapon == holdingHandler.rightObject.gameObject)
			{
				rightHandEvent?.Invoke();
			}
		}
	}
}
