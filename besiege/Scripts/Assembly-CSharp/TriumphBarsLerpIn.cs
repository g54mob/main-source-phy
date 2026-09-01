using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("UI/Win Screen/Triumph Bars Lerp In")]
public class TriumphBarsLerpIn : MonoBehaviour
{
	public Transform bottomBar;

	public Transform topBar;

	public Transform nextZoneBox;

	public GameObject nextZoneText;

	public GameObject nextIslandText;

	public GameObject creditsNextText;

	public Transform nextZoneEditModeTargetBox;

	public float nextZoneBuildModePosY;

	public float nextZoneNotCurrentlyWonPosY = 1f;

	public Renderer nextZoneBg;

	public float lerpSpeed = 0.1f;

	protected float bottomBarStartHeight;

	protected float bottomBarEndHeight;

	public float bottomLerpUpAmount = 0.802f;

	protected float topBarStartHeight;

	protected float topBarEndHeight;

	public float topLerpDownAmount = 0.802f;

	public Collider nextZoneCollider;

	private IEnumerator animateNextBoxCoroutine;

	private IEnumerator hideBarsCoroutine;

	private IEnumerator showBarsCoroutine;

	private Camera hudCam;

	private bool lerpedIn;

	private AudioSource audioSource;

	public bool NextZoneShown
	{
		get
		{
			return nextZoneBg.enabled;
		}
	}

	protected float _DeltaTime
	{
		get
		{
			return TimeSlider.Instance.deltaTime;
		}
	}

	protected WinCondition winControl
	{
		get
		{
			return WinCondition.Instance ?? UnityEngine.Object.FindObjectOfType<WinCondition>();
		}
	}

	private IEnumerator Start()
	{
		hudCam = GameObject.FindGameObjectWithTag("hudCamera").GetComponent<Camera>();
		if (winControl.finalCampaignLevel)
		{
			creditsNextText.SetActive(true);
			nextZoneText.SetActive(false);
			nextIslandText.SetActive(false);
		}
		else if (!StatMaster.isMP && StampFanfareController.endOfIslandLevel)
		{
			nextZoneText.SetActive(false);
			nextIslandText.SetActive(true);
			creditsNextText.SetActive(false);
		}
		ReferenceMaster.onLevelLoad = (Action)Delegate.Combine(ReferenceMaster.onLevelLoad, new Action(OnLevelLoad));
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulationToggle));
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Combine(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChanged));
		audioSource = GetComponent<AudioSource>();
		nextZoneBg.enabled = false;
		yield return null;
		audioSource.clip.LoadAudioData();
		SetReferencePositions();
		HideBars();
	}

	private void OnLevelSimulationStoppedMP()
	{
		if (!StatMaster.isHosting)
		{
			HideNextZoneButton();
		}
		else if (WinCondition.hasWonBefore)
		{
			ShowNextZoneButtonBuildModeMP();
		}
		else
		{
			HideNextZoneButton();
		}
	}

	private void OnLevelSimulationStoppedSP()
	{
		if (WinCondition.hasWonBefore)
		{
			ShowNextZoneButtonBuildModeSP();
		}
		else
		{
			HideNextZoneButton();
		}
	}

	public void ShowNextZoneButtonBuildModeSP()
	{
		ShowNextZoneButton(nextZoneBuildModePosY);
	}

	public void ShowNextZoneButtonBuildModeMP()
	{
		if (!StatMaster.Mode.levelEdit && !StatMaster.isLocalSim && NetworkScene.ServerSettings.playList.Count > 1 && WinCondition.hasWonBefore)
		{
			ShowNextZoneButton(nextZoneBuildModePosY);
		}
	}

	public void HideNextZoneButton()
	{
		StopAnimateNextZoneBox();
		animateNextBoxCoroutine = AnimateNextBoxIE(-10f);
		StartCoroutine(animateNextBoxCoroutine);
		nextZoneBg.enabled = false;
	}

	public void ShowBars()
	{
		lerpedIn = true;
		nextZoneBg.enabled = false;
		nextZoneCollider.enabled = true;
		StopAnimateNextZoneBox();
		if (hideBarsCoroutine != null)
		{
			StopCoroutine(hideBarsCoroutine);
		}
		showBarsCoroutine = ShowBarsIE();
		StartCoroutine(showBarsCoroutine);
		audioSource.Play();
	}

	public void HideBars()
	{
		lerpedIn = false;
		if (showBarsCoroutine != null)
		{
			StopCoroutine(showBarsCoroutine);
		}
		hideBarsCoroutine = HideBarsIE();
		StartCoroutine(hideBarsCoroutine);
	}

	public void OnLevelLoad()
	{
		if (!StatMaster.isHeadless && !StatMaster.Mode.levelEdit && NetworkScene.ServerSettings.playList.Count <= 1)
		{
			if (!StatMaster.isMP)
			{
				OnLevelSimulationStoppedSP();
			}
			else if (StatMaster.isHosting)
			{
				OnLevelSimulationStoppedMP();
			}
		}
	}

	private void OnDestroy()
	{
		ReferenceMaster.onLevelLoad = (Action)Delegate.Remove(ReferenceMaster.onLevelLoad, new Action(OnLevelLoad));
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulationToggle));
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Remove(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChanged));
	}

	private void OnLevelSimulationStartMP()
	{
		bool flag = !StatMaster.Mode.levelEdit && !StatMaster.isLocalSim && NetworkScene.ServerSettings.playList.Count > 1 && WinCondition.hasWonBefore;
		if (PlayerData.localPlayer.PlayMode == BesiegePlayMode.BuildMode && flag)
		{
			ShowNextZoneButtonBuildModeMP();
		}
		else
		{
			HideNextZoneButton();
		}
	}

	private void OnLevelSimulationStartSP()
	{
		if (WinCondition.hasWonBefore)
		{
			ShowNextZoneButton(nextZoneNotCurrentlyWonPosY);
		}
	}

	private void StopAnimateNextZoneBox()
	{
		if (animateNextBoxCoroutine != null)
		{
			StopCoroutine(animateNextBoxCoroutine);
		}
	}

	private void ShowNextZoneButton(float zoneYPosition)
	{
		StopAnimateNextZoneBox();
		animateNextBoxCoroutine = AnimateNextBoxIE(zoneYPosition);
		StartCoroutine(animateNextBoxCoroutine);
		nextZoneBg.enabled = true;
		nextZoneBox.gameObject.SetActive(true);
	}

	private IEnumerator ShowBarsIE()
	{
		float cTime = 0f;
		float rate = 1f / lerpSpeed;
		float bottomBarStartPos = bottomBar.localPosition.y;
		float topBarStartPos = topBar.localPosition.y;
		float nextZoneStartPos = nextZoneBox.localPosition.y;
		while (cTime < 1f)
		{
			cTime += _DeltaTime * rate;
			bottomBar.localPosition = new Vector3(bottomBar.localPosition.x, Mathf.Lerp(bottomBarStartPos, bottomBarEndHeight, cTime), bottomBar.localPosition.z);
			nextZoneBox.localPosition = new Vector3(nextZoneBox.localPosition.x, Mathf.Lerp(nextZoneStartPos, -0.161602f, cTime), nextZoneBox.localPosition.z);
			topBar.localPosition = new Vector3(topBar.localPosition.x, Mathf.Lerp(topBarStartPos, topBarEndHeight, cTime), topBar.localPosition.z);
			yield return null;
		}
	}

	private IEnumerator HideBarsIE()
	{
		float cTime = 0f;
		float rate = 1f / lerpSpeed;
		float bottomBarStartPos = bottomBar.localPosition.y;
		float topBarStartPos = topBar.localPosition.y;
		float nextZoneStartPos = nextZoneBox.localPosition.y;
		Vector3 nextZoneLocal = nextZoneBox.localPosition;
		bool showNext = !StatMaster.isMP || (!StatMaster.Mode.levelEdit && !StatMaster.isLocalSim && NetworkScene.ServerSettings.playList.Count > 1);
		while (cTime < 1f)
		{
			cTime += _DeltaTime * rate;
			Vector3 bottomLocal = bottomBar.localPosition;
			bottomBar.localPosition = new Vector3(bottomLocal.x, Mathf.Lerp(bottomBarStartPos, bottomBarStartHeight, cTime), bottomLocal.z);
			if (!showNext)
			{
				nextZoneBox.localPosition = new Vector3(nextZoneLocal.x, Mathf.Lerp(nextZoneStartPos, -0.161602f, cTime), nextZoneLocal.z);
			}
			Vector3 topLocal = topBar.localPosition;
			topBar.localPosition = new Vector3(topLocal.x, Mathf.Lerp(topBarStartPos, topBarStartHeight, cTime), topLocal.z);
			yield return null;
		}
		if (StatMaster.isMP && StatMaster.isHosting && WinCondition.hasWonBefore && showNext)
		{
			ShowNextZoneButtonBuildModeMP();
		}
	}

	private IEnumerator AnimateNextBoxIE(float endHeighty)
	{
		float cTime = 0f;
		float rate = 1f / lerpSpeed;
		Vector3 oldPos = nextZoneBox.localPosition;
		float nextZoneStartPos = oldPos.y;
		while (cTime < 1f)
		{
			cTime += _DeltaTime * rate;
			nextZoneBox.localPosition = new Vector3(oldPos.x, Mathf.Lerp(nextZoneStartPos, endHeighty, cTime), oldPos.z);
			yield return null;
		}
		nextZoneBox.localPosition = new Vector3(oldPos.x, endHeighty, oldPos.z);
	}

	public void OnLevelSimulationToggle(bool toggle)
	{
		if (StatMaster.isHeadless || (StatMaster.isMP && StatMaster.Mode.levelEdit))
		{
			return;
		}
		bool flag = NetworkScene.ServerSettings.playList.Count > 1;
		if (StatMaster.isMP && !flag)
		{
			return;
		}
		bool flag2 = !StatMaster.isMP;
		if (toggle)
		{
			if (flag2)
			{
				OnLevelSimulationStartSP();
			}
			else if (StatMaster.isHosting)
			{
				OnLevelSimulationStartMP();
			}
		}
		else if (flag2)
		{
			OnLevelSimulationStoppedSP();
		}
		else if (StatMaster.isHosting)
		{
			OnLevelSimulationStoppedMP();
		}
	}

	private void OnResolutionChanged()
	{
		SetReferencePositions();
		bool flag = lerpedIn;
		if (lerpedIn)
		{
			HideBars();
			nextZoneBg.enabled = false;
		}
		if (flag)
		{
			ShowBars();
		}
	}

	private void SetReferencePositions()
	{
		if (!(hudCam == null))
		{
			float y = hudCam.ScreenToWorldPoint(new Vector2(0f, 0f)).y;
			float y2 = hudCam.ScreenToWorldPoint(new Vector2(0f, hudCam.pixelHeight)).y;
			bottomBarStartHeight = GetY(bottomBar, y);
			bottomBarEndHeight = bottomBarStartHeight + bottomLerpUpAmount;
			topBarStartHeight = GetY(topBar, y2);
			topBarEndHeight = topBarStartHeight - topLerpDownAmount;
			if (lerpedIn)
			{
				bottomBar.localPosition = new Vector3(bottomBar.localPosition.x, bottomBarEndHeight, bottomBar.localPosition.z);
				topBar.localPosition = new Vector3(topBar.localPosition.x, topBarEndHeight, topBar.localPosition.z);
			}
			else
			{
				bottomBar.localPosition = new Vector3(bottomBar.localPosition.x, bottomBarStartHeight, bottomBar.localPosition.z);
				topBar.localPosition = new Vector3(topBar.localPosition.x, topBarStartHeight, topBar.localPosition.z);
			}
		}
	}

	private float GetY(Transform t, float y)
	{
		return t.parent.InverseTransformPoint(t.position.x, y, t.position.z).y;
	}
}
