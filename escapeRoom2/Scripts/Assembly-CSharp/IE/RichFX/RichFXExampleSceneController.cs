using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace IE.RichFX
{
	public class RichFXExampleSceneController : MonoBehaviour
	{
		[SerializeField]
		private Dropdown effectCategoryDropdown;

		[SerializeField]
		private Volume[] postProcessVolumes;

		[SerializeField]
		private Volume[] combinationVolumes;

		[SerializeField]
		private Text effectNameText;

		[SerializeField]
		private bool incrementCategory = true;

		private bool releaseMode;

		private List<int> volumeSelectedIndices = new List<int>();

		private int currentSelectedVolume;

		private bool categoryVolumesActive = true;

		private int activeCombinationVolume;

		private Coroutine activateCombVolume;

		private void Awake()
		{
			for (int i = 1; i < postProcessVolumes.Length; i++)
			{
				postProcessVolumes[i].weight = 0f;
			}
			for (int j = 0; j < postProcessVolumes.Length; j++)
			{
				volumeSelectedIndices.Add(0);
			}
		}

		private void Start()
		{
			DropdownValueChanged();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				NextButton();
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				PreviousButton();
			}
			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				ActivateCombinationVolume(0, "Combination: Security Cam");
			}
			if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				ActivateCombinationVolume(1, "Combination: Underwater");
			}
			if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				ActivateCombinationVolume(2, "Combination: Weapon Cam");
			}
			if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				DeactivateCombinationVolumes();
			}
		}

		public void NextButton()
		{
			if (!categoryVolumesActive)
			{
				if (activeCombinationVolume == 0)
				{
					ActivateCombinationVolume(1, "Combination: Underwater");
				}
				else if (activeCombinationVolume == 1)
				{
					ActivateCombinationVolume(2, "Combination: Weapon Cam");
				}
				return;
			}
			postProcessVolumes[currentSelectedVolume].profile.components[volumeSelectedIndices[currentSelectedVolume]].active = false;
			volumeSelectedIndices[currentSelectedVolume]++;
			if (volumeSelectedIndices[currentSelectedVolume] == postProcessVolumes[currentSelectedVolume].profile.components.Count)
			{
				volumeSelectedIndices[currentSelectedVolume] = 0;
				if (incrementCategory)
				{
					postProcessVolumes[currentSelectedVolume].profile.components[volumeSelectedIndices[currentSelectedVolume]].active = false;
					postProcessVolumes[currentSelectedVolume].weight = 0f;
					currentSelectedVolume++;
					if (currentSelectedVolume == volumeSelectedIndices.Count)
					{
						currentSelectedVolume = 0;
						ActivateCombinationVolume(0, "Combination: Security Cam");
						return;
					}
					volumeSelectedIndices[currentSelectedVolume] = 0;
					postProcessVolumes[currentSelectedVolume].weight = 1f;
					postProcessVolumes[currentSelectedVolume].profile.components[volumeSelectedIndices[currentSelectedVolume]].active = true;
					UpdateUI();
				}
			}
			else
			{
				postProcessVolumes[currentSelectedVolume].profile.components[volumeSelectedIndices[currentSelectedVolume]].active = true;
				UpdateUI();
			}
		}

		public void PreviousButton()
		{
			bool flag = true;
			if (!categoryVolumesActive)
			{
				if (activeCombinationVolume == 0)
				{
					currentSelectedVolume = postProcessVolumes.Length - 1;
					volumeSelectedIndices[currentSelectedVolume] = postProcessVolumes[currentSelectedVolume].profile.components.Count - 1;
					postProcessVolumes[currentSelectedVolume].profile.components[volumeSelectedIndices[currentSelectedVolume]].active = true;
					DeactivateCombinationVolumes();
					flag = false;
				}
				else
				{
					if (activeCombinationVolume == 1)
					{
						ActivateCombinationVolume(0, "Combination: Security Cam");
						return;
					}
					if (activeCombinationVolume == 2)
					{
						ActivateCombinationVolume(1, "Combination: Underwater");
						return;
					}
				}
			}
			postProcessVolumes[currentSelectedVolume].profile.components[volumeSelectedIndices[currentSelectedVolume]].active = false;
			if (flag)
			{
				volumeSelectedIndices[currentSelectedVolume]--;
			}
			if (volumeSelectedIndices[currentSelectedVolume] == -1)
			{
				volumeSelectedIndices[currentSelectedVolume] = postProcessVolumes[currentSelectedVolume].profile.components.Count - 1;
				if (incrementCategory)
				{
					postProcessVolumes[currentSelectedVolume].profile.components[volumeSelectedIndices[currentSelectedVolume]].active = false;
					postProcessVolumes[currentSelectedVolume].weight = 0f;
					currentSelectedVolume--;
					if (currentSelectedVolume == -1)
					{
						currentSelectedVolume = 0;
						volumeSelectedIndices[currentSelectedVolume] = 0;
					}
					else
					{
						volumeSelectedIndices[currentSelectedVolume] = postProcessVolumes[currentSelectedVolume].profile.components.Count - 1;
					}
					postProcessVolumes[currentSelectedVolume].weight = 1f;
				}
			}
			postProcessVolumes[currentSelectedVolume].profile.components[volumeSelectedIndices[currentSelectedVolume]].active = true;
			UpdateUI();
		}

		public void DropdownValueChanged()
		{
			postProcessVolumes[currentSelectedVolume].profile.components[volumeSelectedIndices[currentSelectedVolume]].active = false;
			postProcessVolumes[currentSelectedVolume].weight = 0f;
			currentSelectedVolume = effectCategoryDropdown.value;
			postProcessVolumes[currentSelectedVolume].weight = 1f;
			postProcessVolumes[currentSelectedVolume].profile.components[volumeSelectedIndices[currentSelectedVolume]].active = true;
			UpdateUI();
		}

		private void ActivateCombinationVolume(int index, string combVolumeName)
		{
			if (categoryVolumesActive)
			{
				CurrentCategoryVolumeActivation(enabled: false);
			}
			combinationVolumes[activeCombinationVolume].weight = 0f;
			if (activateCombVolume != null)
			{
				StopCoroutine(activateCombVolume);
			}
			activateCombVolume = StartCoroutine(ActivateCombinationVolumeRoutine(index));
			activeCombinationVolume = index;
			UpdateUI(combVolumeName);
		}

		private void DeactivateCombinationVolumes()
		{
			if (activateCombVolume != null)
			{
				StopCoroutine(activateCombVolume);
			}
			combinationVolumes[activeCombinationVolume].weight = 0f;
			CurrentCategoryVolumeActivation(enabled: true);
			UpdateUI();
		}

		private void CurrentCategoryVolumeActivation(bool enabled)
		{
			postProcessVolumes[currentSelectedVolume].weight = (enabled ? 1f : 0f);
			categoryVolumesActive = enabled;
		}

		private void UpdateUI(string overrideName = "")
		{
			string text = postProcessVolumes[currentSelectedVolume].profile.components[volumeSelectedIndices[currentSelectedVolume]].name.Replace("(Clone)", string.Empty);
			string text2 = "(" + (volumeSelectedIndices[currentSelectedVolume] + 1) + "/" + postProcessVolumes[currentSelectedVolume].profile.components.Count + ")";
			if (releaseMode)
			{
				text2 = "";
				text = text.ToUpper();
			}
			if (overrideName == "")
			{
				effectNameText.text = text + " " + text2;
			}
			else
			{
				effectNameText.text = overrideName;
			}
			if (combinationVolumes[activeCombinationVolume].weight == 0f)
			{
				effectCategoryDropdown.value = currentSelectedVolume;
			}
		}

		private IEnumerator ActivateCombinationVolumeRoutine(int index)
		{
			float i = 0f;
			float duration = 0.8f;
			while (i < 1f)
			{
				i += Time.deltaTime * 1f / duration;
				combinationVolumes[index].weight = Mathf.Lerp(0f, 1f, i);
				yield return null;
			}
		}

		private IEnumerator AutoIncrementEffects()
		{
			int i = 0;
			int target = 0;
			float duration = 4f;
			for (int j = 0; j < postProcessVolumes.Length; j++)
			{
				target += postProcessVolumes[j].profile.components.Count;
			}
			for (; i < target; i++)
			{
				yield return new WaitForSeconds(duration);
				NextButton();
			}
			CurrentCategoryVolumeActivation(enabled: false);
			ActivateCombinationVolume(0, "Combination: Security Cam");
			yield return new WaitForSeconds(duration);
			ActivateCombinationVolume(1, "Combination: Underwater");
			yield return new WaitForSeconds(duration);
			ActivateCombinationVolume(2, "Combination: Weapon Cam");
		}
	}
}
