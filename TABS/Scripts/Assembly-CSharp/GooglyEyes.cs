using System.Collections;
using System.Collections.Generic;
using TFBGames;
using UnityEngine;

public class GooglyEyes : MonoBehaviour
{
	private const float BlinkDistanceFactor = 10f;

	private Vector2 eyePosition;

	private Transform mainCamTransform;

	private bool isRunning = true;

	public List<GooglyEye> eyes = new List<GooglyEye>();

	public static GooglyEyes instance;

	private WaitForSeconds blinkWait = new WaitForSeconds(0.2f);

	private int checkedThisFrame;

	private int eyeCheckID;

	private Vector3 eyeDelta;

	private Vector3 eyeMovement;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		MainCam mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
		mainCamTransform = ((mainCam != null) ? mainCam.transform : null);
	}

	public void AddEye(GooglyEye eye)
	{
		eyes.Add(eye);
	}

	public void RemoveEye(GooglyEye eye)
	{
		eyes.Remove(eye);
	}

	private void Update()
	{
		if (!isRunning)
		{
			return;
		}
		checkedThisFrame = 0;
		for (int i = 0; i < eyes.Count; i++)
		{
			GooglyEye googlyEye = eyes[i];
			if (googlyEye == null || checkedThisFrame >= 10 || i != eyeCheckID)
			{
				continue;
			}
			eyeCheckID++;
			checkedThisFrame++;
			if (eyeCheckID >= eyes.Count)
			{
				eyeCheckID = 0;
			}
			if (Vector3.Distance(mainCamTransform.position, googlyEye.pupil.position) < 10f * googlyEye.transform.lossyScale.magnitude)
			{
				if (Time.time > googlyEye.nextBlink)
				{
					for (int j = 0; j < googlyEye.blinkBuddies.Count; j++)
					{
						StartCoroutine(DoBlink(googlyEye.blinkBuddies[j]));
					}
				}
				googlyEye.enabled = true;
				if (googlyEye.currentEyeState == GooglyEye.EyeState.Open)
				{
					MovePupil(googlyEye);
				}
			}
			else
			{
				googlyEye.enabled = false;
			}
		}
	}

	public void Startle(GooglyEye eye, float dmg)
	{
		StartCoroutine(DoStartle(eye, dmg));
	}

	private IEnumerator DoStartle(GooglyEye eye, float dmg)
	{
		if (eye.currentEyeState == GooglyEye.EyeState.Open)
		{
			eye.SetState(GooglyEye.EyeState.Startle);
			yield return new WaitForSeconds(dmg);
			eye.SetState(GooglyEye.EyeState.Open);
		}
	}

	private IEnumerator DoBlink(GooglyEye eye)
	{
		if (eye.currentEyeState == GooglyEye.EyeState.Open)
		{
			eye.SetNextBlink();
			eye.SetState(GooglyEye.EyeState.Blink);
			yield return blinkWait;
			eye.SetState(GooglyEye.EyeState.Open);
		}
	}

	private void MovePupil(GooglyEye eye)
	{
		eyeMovement = eye.transform.position - eye.lastPos;
		eye.lastPos = eye.transform.position;
		eye.velocity -= eyeMovement * eye.inheritedMovement;
		eyeDelta = eye.transform.TransformPoint(eye.eyeTarget) - eye.pupil.position;
		eye.velocity += eye.spring * Time.deltaTime * 100f * eyeDelta;
		eye.velocity += (0f - Time.deltaTime) * eye.drag * eye.velocity;
		eye.velocity = Vector3.ProjectOnPlane(eye.velocity, eye.transform.forward);
		eye.pupil.transform.position += eye.velocity * Time.deltaTime;
		eye.pupil.localPosition = Vector3.ClampMagnitude(eye.pupil.localPosition, eye.size);
		eye.pupil.localPosition = new Vector3(eye.pupil.localPosition.x, eye.pupil.localPosition.y, 0f);
	}

	public void SetRunning(bool running)
	{
		isRunning = running;
	}
}
