using System.Collections;
using Landfall.TABS.Services;
using UnityEngine;

public class MainMenuTimeHandler : MonoBehaviour
{
	public float delay;

	public AnimationCurve curve;

	public AnimationCurve cameraDistanceCurve;

	public float targetCamDistance = -10f;

	private float startTargetCamDistance;

	public bool done;

	private bool m_pushedTimeState;

	private IntroSequence introSequence;

	private bool freezeRequested;

	public bool FreezeRequested => freezeRequested;

	private void Awake()
	{
		ITimeService service = ServiceLocator.GetService<ITimeService>();
		service.Unlock();
		service.UnPause();
		service.SetState(1f, 0f);
		service.SetState(curve.Evaluate(0f), 0f, useAudioMixer: false);
		service.ResetAudioMixer();
		introSequence = GetComponent<IntroSequence>();
	}

	private void Start()
	{
		startTargetCamDistance = base.transform.GetChild(0).transform.localPosition.z;
	}

	public void Freeze()
	{
		if (!done)
		{
			if (introSequence != null && !introSequence.AnimationsFinished && !freezeRequested)
			{
				freezeRequested = true;
				return;
			}
			done = true;
			StartCoroutine(DoFreeze());
			m_pushedTimeState = true;
		}
	}

	private IEnumerator DoFreeze()
	{
		yield return new WaitForSeconds(delay);
		ITimeService timeService = ServiceLocator.GetService<ITimeService>();
		timeService.SetState(curve, useAudioMixer: false);
		float t = curve.keys[curve.keys.Length - 1].time;
		while (timeService.TransitionProgress < t)
		{
			base.transform.GetChild(0).transform.localPosition = Vector3.Lerp(new Vector3(0f, 0f, startTargetCamDistance), new Vector3(0f, 0f, targetCamDistance), cameraDistanceCurve.Evaluate(timeService.TransitionProgress));
			yield return null;
		}
	}
}
