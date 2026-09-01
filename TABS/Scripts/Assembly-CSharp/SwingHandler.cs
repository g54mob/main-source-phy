using System.Collections;
using Landfall.TABS;
using UnityEngine;

public class SwingHandler : MonoBehaviour
{
	public enum HoldDirection
	{
		Left = 0,
		Up = 1,
		Right = 2,
		Down = 3,
		None = 4
	}

	public HoldDirection holdDirection;

	public Transform swingDirectionObject;

	public Transform swingRotObject;

	public Transform weaponTiltObject;

	public Transform weaponTarget;

	public Transform stabWeaponDirection;

	public Transform stabWeaponTarget;

	public SwingData defaultdata;

	public bool IsSwinging;

	private Transform cam;

	private float defaultShoulderHeight;

	private PlayerWeaponHandler playerWeaponHandler;

	private MouseLook mouseLook;

	private PlayCurveRotation curveAnim;

	private PlayerHolding holding;

	private SoundPlayer soundPlayer;

	private float threshold = 1f;

	private bool isAtTheEndOfSwing;

	public bool isHoldingSwing;

	private bool swingWasInterupted;

	private void Start()
	{
		soundPlayer = ServiceLocator.GetService<SoundPlayer>();
		holding = GetComponentInParent<PlayerHolding>();
		curveAnim = GetComponentInParent<PlayCurveRotation>();
		mouseLook = GetComponentInParent<MouseLook>();
		playerWeaponHandler = GetComponentInParent<PlayerWeaponHandler>();
		cam = GetComponentInParent<MainCam>().transform;
		defaultShoulderHeight = base.transform.localPosition.y;
	}

	private void Update()
	{
		SwingUI();
	}

	private void SwingUI()
	{
		if ((IsSwinging || isHoldingSwing) && !isAtTheEndOfSwing)
		{
			return;
		}
		holdDirection = HoldDirection.None;
		if (!playerWeaponHandler.fpsWeapon)
		{
			return;
		}
		if (Mathf.Abs(mouseLook.mouseDirection.x) > Mathf.Abs(mouseLook.mouseDirection.y))
		{
			if (mouseLook.mouseDirection.x > threshold && (bool)playerWeaponHandler.fpsWeapon.right)
			{
				holdDirection = HoldDirection.Right;
			}
			else if (mouseLook.mouseDirection.x < 0f - threshold && (bool)playerWeaponHandler.fpsWeapon.left)
			{
				holdDirection = HoldDirection.Left;
			}
		}
		else if (mouseLook.mouseDirection.y > threshold && (bool)playerWeaponHandler.fpsWeapon.up)
		{
			holdDirection = HoldDirection.Up;
		}
		else if (mouseLook.mouseDirection.y < 0f - threshold && (bool)playerWeaponHandler.fpsWeapon.down)
		{
			holdDirection = HoldDirection.Down;
		}
	}

	private SwingData GetSwing(FPSWeapon fpsWeapon)
	{
		SwingData result = null;
		switch (holdDirection)
		{
		case HoldDirection.Left:
			result = fpsWeapon.left.swingData;
			break;
		case HoldDirection.Up:
			result = fpsWeapon.up.swingData;
			break;
		case HoldDirection.Right:
			result = fpsWeapon.right.swingData;
			break;
		case HoldDirection.Down:
			result = fpsWeapon.down.swingData;
			break;
		}
		return result;
	}

	public void HoldSwing(FPSWeapon fpsWeapon)
	{
		SwingData swing = GetSwing(fpsWeapon);
		isHoldingSwing = true;
		swingDirectionObject.localEulerAngles = new Vector3(0f, 0f, swing.swingDirectionAngle);
		swingRotObject.localEulerAngles = new Vector3(0f, swing.startAngle, 0f);
		weaponTiltObject.transform.rotation = Quaternion.LookRotation(cam.TransformDirection(swing.swingHoldForward), cam.TransformDirection(swing.swingHoldUp));
		stabWeaponTarget.transform.rotation = Quaternion.LookRotation(cam.TransformDirection(swing.swingHoldForward), cam.TransformDirection(swing.swingHoldUp));
		stabWeaponTarget.localPosition = new Vector3(0f, 0f, swing.swingCurve.Evaluate(0f) * swing.stabDistanceMultiplier);
		base.transform.localPosition = new Vector3(0f, swing.shoulderHeightMultiplierCurve.Evaluate(0f) * defaultShoulderHeight, 0f);
	}

	public void StartSwing(FPSWeapon fpsWeapon)
	{
		StopAllCoroutines();
		SwingData swing = GetSwing(fpsWeapon);
		StartCoroutine(DelaySound(fpsWeapon.meleeWeapon.soundDelay, fpsWeapon));
		curveAnim.Play(swing.curveAnimationCurve, swing.curveAnimation);
		isHoldingSwing = false;
		swingDirectionObject.localEulerAngles = new Vector3(0f, 0f, swing.swingDirectionAngle);
		StartCoroutine(DoSwing(swing, fpsWeapon));
		holding.targetType = ((swing.swingType == SwingData.SwingType.Stab) ? PlayerHolding.WeaponTargetType.Stab : PlayerHolding.WeaponTargetType.Swing);
	}

	private IEnumerator DelaySound(float delay, FPSWeapon fpsWeapon)
	{
		yield return new WaitForSeconds(delay);
		ServiceLocator.GetService<SoundPlayer>().PlaySoundEffectNonAlloc(fpsWeapon.meleeWeapon.SoundPathData, 1f, base.transform.position);
	}

	private IEnumerator DoSwing(SwingData swing, FPSWeapon fpsWeapon)
	{
		MeleeWeapon meleeWeapon = fpsWeapon.meleeWeapon;
		CollisionWeapon collisionWeapon = null;
		if ((bool)meleeWeapon)
		{
			collisionWeapon = meleeWeapon.GetComponent<CollisionWeapon>();
		}
		if ((bool)collisionWeapon)
		{
			collisionWeapon.AddCollisionAction(WeaponCollision);
		}
		playerWeaponHandler.StartSwing();
		swingWasInterupted = false;
		IsSwinging = true;
		float c = 0f;
		float t = swing.swingCurve.keys[swing.swingCurve.keys.Length - 1].time;
		float lastValue = 0f;
		while (c < t)
		{
			c += Time.deltaTime * swing.animationSpeed;
			if (c + 0.2f > t)
			{
				isAtTheEndOfSwing = true;
			}
			float num = swing.swingCurve.Evaluate(c) - lastValue;
			if (swing.swingType == SwingData.SwingType.Swing)
			{
				swingRotObject.localEulerAngles = new Vector3(0f, Mathf.LerpUnclamped(swing.startAngle, swing.endAngle, swing.swingCurve.Evaluate(c)), 0f);
				weaponTiltObject.transform.localEulerAngles = new Vector3(num * swing.tiltFactor * Time.deltaTime * swing.animationSpeed * 70f, 0f, 0f);
			}
			if (swing.swingType == SwingData.SwingType.Stab)
			{
				stabWeaponTarget.localPosition = new Vector3(0f, 0f, swing.swingCurve.Evaluate(c) * swing.stabDistanceMultiplier);
			}
			base.transform.localPosition = new Vector3(0f, swing.shoulderHeightMultiplierCurve.Evaluate(c) * defaultShoulderHeight, 0f);
			Vector3 velocity = fpsWeapon.meleeWeapon.rigidbody.velocity;
			playerWeaponHandler.SetSwingData(velocity.normalized);
			lastValue = swing.swingCurve.Evaluate(c);
			if (swingWasInterupted)
			{
				GetComponentInParent<CharacterData>().screenShake.AddForce(-velocity.normalized * 3f, weaponTarget.position);
				fpsWeapon.meleeWeapon.rigidbody.AddForce(-velocity.normalized * 15f, ForceMode.VelocityChange);
				c = t + 1f;
			}
			yield return null;
		}
		weaponTiltObject.transform.localEulerAngles = Vector3.zero;
		IsSwinging = false;
		playerWeaponHandler.EndSwing();
		isAtTheEndOfSwing = false;
	}

	public void WeaponCollision(Collision collision, float damageDealt)
	{
		swingWasInterupted = true;
	}
}
