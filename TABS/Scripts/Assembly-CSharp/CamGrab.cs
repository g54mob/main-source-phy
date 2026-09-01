using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

public class CamGrab : MonoBehaviour
{
	private Rigidbody hand;

	public LayerMask mask;

	private Animator anim;

	private FixedJoint joint;

	private DataHandler heldUnit;

	private CameraMovement camVel;

	private Transform mainCamTransform;

	private Vector3 relativePos;

	private Vector3 targetPos;

	private Vector3 targetForward;

	private Vector3 vel;

	private Vector3 angVel;

	private PlayerActions m_playerActions;

	private void Start()
	{
		MainCam mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
		mainCamTransform = ((mainCam != null) ? mainCam.transform : null);
		anim = GetComponentInChildren<Animator>();
		hand = GetComponentInChildren<Rigidbody>();
		camVel = mainCamTransform.GetComponentInParent<CameraMovement>();
		m_playerActions = PlayerActions.Instance;
	}

	private void LateUpdate()
	{
		if ((bool)joint && joint.connectedBody == null)
		{
			Object.Destroy(joint);
			anim.Play("GrabbyHand");
		}
		if (joint != null)
		{
			hand.interpolation = RigidbodyInterpolation.Interpolate;
			hand.isKinematic = false;
			hand.AddForce(Time.deltaTime * 8000f * (mainCamTransform.TransformPoint(relativePos) + camVel.Velocity * 0.05f - hand.transform.position), ForceMode.Acceleration);
			relativePos = Vector3.Lerp(relativePos, Vector3.forward * 5f, Time.unscaledDeltaTime * 2f);
			if ((bool)heldUnit)
			{
				heldUnit.sinceGrounded = 0f;
			}
			if (!m_playerActions.m_TriggerAbility.IsPressed)
			{
				Object.Destroy(joint);
				anim.Play("GrabbyHand");
				if ((bool)heldUnit)
				{
					heldUnit.allRigs.forceInterpolation = false;
					heldUnit.allRigs.UpdateInterpolationMode();
					ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect("Bugs/HandLetGo", 1f, heldUnit.mainRig.position);
				}
			}
			return;
		}
		hand.isKinematic = true;
		hand.interpolation = RigidbodyInterpolation.None;
		hand.transform.position += vel * Time.unscaledDeltaTime;
		hand.transform.Rotate(angVel * Time.unscaledDeltaTime, Space.World);
		vel += 800f * Time.unscaledDeltaTime * (targetPos - hand.transform.position);
		vel -= 35f * Time.unscaledDeltaTime * vel;
		angVel += 60f * Vector3.Angle(targetForward, hand.transform.forward) * 10f * Time.unscaledDeltaTime * -Vector3.Cross(targetForward, hand.transform.forward).normalized;
		angVel += 60f * Vector3.Angle(Vector3.up, hand.transform.up) * 2f * Time.unscaledDeltaTime * -Vector3.Cross(Vector3.up, hand.transform.up).normalized;
		angVel -= 25f * Time.unscaledDeltaTime * angVel;
		Ray ray = new Ray(mainCamTransform.position, mainCamTransform.forward);
		Physics.SphereCast(ray, 0.1f, out var hitInfo, 10f, mask);
		if (!hitInfo.transform)
		{
			Physics.SphereCast(ray, 0.3f, out hitInfo, 10f, mask);
		}
		if (!hitInfo.transform)
		{
			Physics.SphereCast(ray, 0.6f, out hitInfo, 10f, mask);
		}
		if ((bool)hitInfo.transform && (bool)hitInfo.collider.attachedRigidbody)
		{
			anim.speed = 10f;
			targetPos = hitInfo.point + mainCamTransform.forward * -0.3f + hitInfo.normal * 0.3f;
			targetForward = (-hitInfo.normal + mainCamTransform.forward * 2f).normalized;
		}
		else
		{
			anim.speed = 1f;
			targetPos = mainCamTransform.position + camVel.Velocity * 0.05f + mainCamTransform.forward * 3f + mainCamTransform.right * 0.2f;
			targetForward = mainCamTransform.forward;
		}
		if ((bool)hitInfo.transform && (bool)hitInfo.collider.attachedRigidbody && m_playerActions.m_TriggerAbility.IsPressed)
		{
			hand.transform.SetPositionAndRotation(hitInfo.point + hitInfo.normal * 0.1f, Quaternion.LookRotation(-hitInfo.normal));
			joint = hand.gameObject.AddComponent<FixedJoint>();
			joint.connectedBody = hitInfo.collider.attachedRigidbody;
			relativePos = mainCamTransform.InverseTransformPoint(hand.position);
			heldUnit = joint.connectedBody.transform.root.GetComponentInChildren<DataHandler>();
			anim.Play("GrabbyHandHold");
			ScreenShake.Instance.AddForce(-hitInfo.normal * 1f, mainCamTransform.position + mainCamTransform.forward * 1f);
			if (heldUnit != null)
			{
				heldUnit.allRigs.forceInterpolation = true;
				heldUnit.allRigs.UpdateInterpolationMode();
				ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect("Bugs/HandGrab", 1f, heldUnit.mainRig.position);
			}
		}
	}
}
