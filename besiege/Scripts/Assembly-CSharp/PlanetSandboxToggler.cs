using System.Collections;
using BesiegeDlc;
using UnityEngine;

public class PlanetSandboxToggler : MonoBehaviour
{
	[SerializeField]
	private Animator dlcAnimation;

	[SerializeField]
	private Transform mainMover;

	[SerializeField]
	private Transform sandboxesMover;

	[SerializeField]
	private Transform dlcMover;

	[SerializeField]
	private Transform creditsButton;

	[SerializeField]
	private float transitionTime = 0.8f;

	[SerializeField]
	private AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	[SerializeField]
	private GameObject backToPlanetObject;

	private Coroutine transitionRoutine;

	private void Awake()
	{
	}

	private void Start()
	{
		if (DlcManager.Instance.GetDlcStatus(DlcManager.DlcType.Water) != DlcManager.DlcStatusType.MissingDlc && TutorialFileManager.GetTutorialState("WaterExpansion") != 1 && (bool)dlcAnimation)
		{
			dlcAnimation.enabled = true;
		}
	}

	public void Toggle(Vector3 moverOffset)
	{
		if (transitionRoutine != null)
		{
			StopCoroutine(transitionRoutine);
		}
		creditsButton.gameObject.SetActive(moverOffset.x < float.Epsilon && moverOffset.x > -1E-45f);
		if (moverOffset.x != 0f)
		{
			backToPlanetObject.SetActive(false);
		}
		transitionRoutine = StartCoroutine(TransitionIE(moverOffset));
	}

	private IEnumerator TransitionIE(Vector3 moverOffset)
	{
		Vector3 startPos = base.transform.localPosition;
		float time = 0f;
		sandboxesMover.gameObject.SetActive(true);
		mainMover.gameObject.SetActive(true);
		if ((bool)dlcMover)
		{
			dlcMover.gameObject.SetActive(true);
		}
		while (time < transitionTime)
		{
			time += Time.deltaTime;
			float lerpTime = transitionCurve.Evaluate(time / transitionTime);
			base.transform.localPosition = Vector3.Lerp(startPos, moverOffset, lerpTime);
			yield return null;
		}
		base.transform.localPosition = moverOffset;
		backToPlanetObject.SetActive(moverOffset.x != 0f);
		sandboxesMover.gameObject.SetActive(moverOffset.x < -1E-45f);
		mainMover.gameObject.SetActive(moverOffset.x == 0f);
		if ((bool)dlcMover)
		{
			dlcMover.gameObject.SetActive(moverOffset.x > float.Epsilon);
		}
	}
}
