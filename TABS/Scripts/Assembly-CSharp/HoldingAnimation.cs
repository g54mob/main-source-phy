using System.Collections;
using UnityEngine;

public class HoldingAnimation : MonoBehaviour
{
	public enum Trigger
	{
		RangeWeaponShoot = 0
	}

	public HoldableDataInstance animationHoldingData;

	public Trigger trigger;

	public float delay = 0.1f;

	public bool animateRotation;

	public bool animatePosition;

	private Holdable holdable;

	private HoldableDataInstance defaultHoldingData;

	private RangeWeapon rangeWeapon;

	private Weapon weapon;

	private void Start()
	{
		weapon = GetComponent<Weapon>();
		holdable = GetComponent<Holdable>();
		defaultHoldingData = holdable.holdableData;
		if (!animateRotation)
		{
			animationHoldingData.forwardRotation = holdable.holdableData.forwardRotation;
			animationHoldingData.upRotation = holdable.holdableData.upRotation;
		}
		if (!animatePosition)
		{
			animationHoldingData.relativePosition = holdable.holdableData.relativePosition;
		}
		animationHoldingData.leftHand = holdable.holdableData.leftHand;
		animationHoldingData.rightHand = holdable.holdableData.rightHand;
		if (trigger == Trigger.RangeWeaponShoot)
		{
			rangeWeapon = GetComponent<RangeWeapon>();
			rangeWeapon.AddShootAction(Play);
		}
	}

	private void Play()
	{
		StopAllCoroutines();
		if (weapon.internalCooldown * 0.5f > 0f)
		{
			StartCoroutine(Animate());
		}
	}

	private IEnumerator Animate()
	{
		holdable.holdableData = defaultHoldingData;
		yield return new WaitForSeconds(delay);
		holdable.holdableData = animationHoldingData;
		while ((bool)weapon && weapon.internalCounter + 1f < weapon.internalCooldown)
		{
			yield return null;
		}
		holdable.holdableData = defaultHoldingData;
	}
}
