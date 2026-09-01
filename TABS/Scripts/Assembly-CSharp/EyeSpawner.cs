using System.Collections.Generic;
using UnityEngine;

public class EyeSpawner : MonoBehaviour
{
	public AnimationCurve eyeSetCurve;

	public EyeSet[] eyeSets;

	public GameObject eyeObject;

	private EyeSet selectedEyeSet;

	public List<GooglyEye> spawnedEyes;

	public bool randomizeEyes = true;

	private bool m_eyesVisible = true;

	private SettingsInstance m_hideGooglyEyesOption;

	private void Awake()
	{
		if (eyeSets.Length == 0)
		{
			return;
		}
		selectedEyeSet = eyeSets[(int)Mathf.Round(Mathf.Clamp(eyeSetCurve.Evaluate((Random.value + Random.value) / 2f), 0f, (float)eyeSets.Length - 1f))];
		if (selectedEyeSet?.obj == null)
		{
			return;
		}
		selectedEyeSet.obj.SetActive(value: true);
		spawnedEyes = new List<GooglyEye>();
		int childCount = selectedEyeSet.obj.transform.childCount;
		float num = selectedEyeSet.allEyesScaleCurve.Evaluate((Random.value + Random.value) / 2f);
		float num2 = selectedEyeSet.parentObjectScaleCurve.Evaluate((Random.value + Random.value) / 2f);
		float num3 = selectedEyeSet.allPupilSize.Evaluate((Random.value + Random.value) / 2f);
		for (int i = 0; i < childCount; i++)
		{
			Transform child = selectedEyeSet.obj.transform.GetChild(i);
			if (selectedEyeSet.useCurves && randomizeEyes)
			{
				child.transform.localScale *= selectedEyeSet.separateEyeScaleCurve.Evaluate((Random.value + Random.value) / 2f);
				child.transform.localScale *= num;
				child.transform.localPosition *= num2;
			}
			GameObject gameObject = Object.Instantiate(eyeObject, child.transform.position, child.transform.rotation);
			gameObject.transform.SetParent(child.transform, worldPositionStays: true);
			gameObject.transform.localScale = Vector3.one;
			if (randomizeEyes)
			{
				if (num3 != 0f)
				{
					gameObject.transform.GetChild(0).GetChild(1).localScale *= num3;
				}
				if (selectedEyeSet.pupilSize.keys.Length != 0)
				{
					gameObject.transform.GetChild(0).GetChild(1).localScale *= selectedEyeSet.pupilSize.Evaluate((Random.value + Random.value) / 2f);
				}
			}
			spawnedEyes.Add(gameObject.GetComponent<GooglyEye>());
		}
		for (int j = 0; j < spawnedEyes.Count; j++)
		{
			spawnedEyes[j].blinkBuddies = spawnedEyes;
		}
		if (selectedEyeSet.useCurves && randomizeEyes)
		{
			selectedEyeSet.obj.transform.localScale *= selectedEyeSet.parentObjectScaleCurve.Evaluate((Random.value + Random.value) / 2f);
		}
		m_hideGooglyEyesOption = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_EYES");
		m_hideGooglyEyesOption.OnValueChanged += UpdateHideEyes;
		UpdateHideEyes(m_hideGooglyEyesOption.currentValue);
	}

	private void OnDestroy()
	{
		if (m_hideGooglyEyesOption != null)
		{
			m_hideGooglyEyesOption.OnValueChanged -= UpdateHideEyes;
		}
	}

	public void SetEyesActive(bool enableEyes)
	{
		if (eyeSets != null && eyeSets.Length != 0)
		{
			eyeSets[0].obj.transform.parent.gameObject.SetActive(enableEyes);
		}
	}

	public void SetEyesVisable(bool active)
	{
		m_eyesVisible = active;
		for (int i = 0; i < spawnedEyes.Count; i++)
		{
			spawnedEyes[i].gameObject.SetActive(active);
		}
	}

	private void UpdateHideEyes(int value)
	{
		for (int i = 0; i < spawnedEyes.Count; i++)
		{
			spawnedEyes[i].gameObject.SetActive(value == 0 && m_eyesVisible);
		}
	}
}
