using UnityEngine;

public class InvertModelDependingOnHand : MonoBehaviour
{
	public enum TargetHand
	{
		rightHand = 0,
		leftHand = 1
	}

	private Holdable holdable;

	private HoldingHandler holdingHandler;

	private GameObject thisWeapon;

	public TargetHand handToInvert;

	private void Start()
	{
		holdable = GetComponentInParent<Holdable>();
		holdingHandler = holdable.holdingHandler;
		thisWeapon = base.transform.GetComponentInParent<Weapon>().gameObject;
		if ((holdingHandler.leftHandActivity == HoldingHandler.HandActivity.HoldingLeftObject || holdingHandler.leftHandActivity == HoldingHandler.HandActivity.NotHolding) && (holdingHandler.rightHandActivity == HoldingHandler.HandActivity.HoldingRightObject || holdingHandler.leftHandActivity == HoldingHandler.HandActivity.NotHolding))
		{
			if ((bool)holdingHandler.leftObject && thisWeapon == holdingHandler.leftObject.gameObject && handToInvert == TargetHand.leftHand)
			{
				base.transform.localScale = new Vector3(base.transform.localScale.x * -1f, base.transform.localScale.y, base.transform.localScale.z);
			}
			if ((bool)holdingHandler.rightObject && thisWeapon == holdingHandler.rightObject.gameObject && handToInvert == TargetHand.rightHand)
			{
				base.transform.localScale = new Vector3(base.transform.localScale.x * -1f, base.transform.localScale.y, base.transform.localScale.z);
			}
		}
	}
}
