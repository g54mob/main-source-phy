using UnityEngine;

public class ChangeGoToBodyPartDependingOnHand : MonoBehaviour
{
	private Holdable holdable;

	private HoldingHandler holdingHandler;

	private GameObject thisWeapon;

	private GoToBodyPart goToBodyPart;

	public bool invertRight;

	public bool invertLeft;

	private void Start()
	{
		holdable = GetComponentInParent<Holdable>();
		holdingHandler = holdable.holdingHandler;
		thisWeapon = base.transform.GetComponentInParent<Weapon>().gameObject;
		goToBodyPart = base.transform.GetComponentInChildren<GoToBodyPart>();
		if ((holdingHandler.leftHandActivity != HoldingHandler.HandActivity.HoldingLeftObject && holdingHandler.leftHandActivity != HoldingHandler.HandActivity.NotHolding) || (holdingHandler.rightHandActivity != HoldingHandler.HandActivity.HoldingRightObject && holdingHandler.leftHandActivity != HoldingHandler.HandActivity.NotHolding))
		{
			return;
		}
		if ((bool)holdingHandler.leftObject && thisWeapon == holdingHandler.leftObject.gameObject)
		{
			if (invertLeft)
			{
				goToBodyPart.transform.localScale = new Vector3(goToBodyPart.transform.localScale.x * -1f, goToBodyPart.transform.localScale.y, goToBodyPart.transform.localScale.z);
			}
			goToBodyPart.targetPart = GoToBodyPart.TargetPart.ElbowRight;
			goToBodyPart.targetPart = GoToBodyPart.TargetPart.ElbowLeft;
			goToBodyPart.GoToPart();
		}
		if ((bool)holdingHandler.rightObject && thisWeapon == holdingHandler.rightObject.gameObject)
		{
			if (invertRight)
			{
				goToBodyPart.transform.localScale = new Vector3(goToBodyPart.transform.localScale.x * -1f, goToBodyPart.transform.localScale.y, goToBodyPart.transform.localScale.z);
			}
			goToBodyPart.targetPart = GoToBodyPart.TargetPart.ElbowRight;
			goToBodyPart.GoToPart();
		}
	}
}
