using System.Collections.Generic;
using INab.Common;
using TMPro;
using UnityEngine;

namespace INab.Demo
{
	public class ControlShowcase : MonoBehaviour
	{
		private int currentIndex;

		public List<GameObject> effects;

		public int startingIndex;

		public TextMeshProUGUI textMeshProUGUI;

		public bool automatic;

		public float animationDuration = 4f;

		[Range(-1f, 1f)]
		public float offset = 0.2f;

		public int numberOfEffects = 30;

		[Space]
		public int automaticIndex;

		public float time;

		public float maxDuration;

		public float motionTime;

		public Animator currentAnimator;

		public bool updateAllNames;

		public bool customVideoShowcase;

		public void Start()
		{
			if (!customVideoShowcase)
			{
				foreach (GameObject effect in effects)
				{
					effect.SetActive(value: false);
				}
			}
			currentIndex = startingIndex;
			if (effects.Count > 0)
			{
				ActivateEffect(currentIndex, currentIndex);
			}
			if (automatic)
			{
				PlayEffect();
				CalculateDuration(automaticIndex);
			}
		}

		private void IncreaseAutomaticIndex()
		{
			automaticIndex++;
			if (automaticIndex >= effects.Count)
			{
				automaticIndex = 0;
			}
		}

		private void CalculateDuration(int index)
		{
			maxDuration = 0f;
			foreach (InteractiveEffect effect in effects[index].GetComponent<ControlVFXGraphs>().effects)
			{
				maxDuration = Mathf.Max(maxDuration, effect.duration);
			}
			maxDuration += offset * maxDuration;
		}

		private void PlayEffect()
		{
			GetControlVFXGraphs().RefreshEffects();
			GetControlVFXGraphs().PlayEffects();
		}

		private void ReverseEffect()
		{
			GetControlVFXGraphs().RefreshEffects();
			GetControlVFXGraphs().ReverseEffects();
		}

		private void Update()
		{
			motionTime += Time.deltaTime / animationDuration;
			if (!customVideoShowcase)
			{
				currentAnimator.SetFloat("MotionTime", motionTime);
			}
			if (automatic)
			{
				time += Time.deltaTime;
				if (time > maxDuration)
				{
					IncreaseAutomaticIndex();
					CalculateDuration(automaticIndex);
					ActivateNextEffect();
					PlayEffect();
					time = 0f;
				}
			}
			if (customVideoShowcase)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					PlayEffect();
				}
				if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					ActivateNextEffect();
					PlayEffect();
				}
				return;
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				ActivateNextEffect();
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				ActivatePreviousEffect();
			}
			if (Input.GetKeyDown(KeyCode.P))
			{
				PlayEffect();
			}
			if (Input.GetKeyDown(KeyCode.R))
			{
				ReverseEffect();
			}
		}

		private void OnValidate()
		{
			if (updateAllNames)
			{
				UpdateAllNames();
			}
		}

		public void UpdateAllNames()
		{
			foreach (GameObject effect in effects)
			{
				string text = effect.GetComponent<ControlVFXGraphs>().effectToReplace.name[..effect.name.LastIndexOf(' ')];
				effect.name = text;
			}
		}

		private void ActivateNextEffect()
		{
			int lastIndex = currentIndex;
			currentIndex++;
			if (currentIndex >= effects.Count)
			{
				currentIndex = 0;
			}
			if (currentIndex >= 0 && currentIndex < effects.Count)
			{
				ActivateEffect(currentIndex, lastIndex);
			}
		}

		private void ActivatePreviousEffect()
		{
			int lastIndex = currentIndex;
			currentIndex--;
			if (currentIndex < 0)
			{
				currentIndex = effects.Count - 1;
			}
			if (currentIndex >= 0 && currentIndex < effects.Count)
			{
				ActivateEffect(currentIndex, lastIndex);
			}
		}

		private ControlVFXGraphs GetControlVFXGraphs()
		{
			return effects[currentIndex].GetComponent<ControlVFXGraphs>();
		}

		private void UpdateUiEffectName()
		{
			string text = currentIndex + 1 + "/" + numberOfEffects + " " + GetControlVFXGraphs().gameObject.name;
			textMeshProUGUI.text = text;
		}

		private void ActivateEffect(int newIndex, int lastIndex)
		{
			if (!customVideoShowcase)
			{
				effects[lastIndex].SetActive(value: false);
				effects[newIndex].SetActive(value: true);
			}
			effects[newIndex].GetComponent<ControlVFXGraphs>().RefreshEffects();
			UpdateUiEffectName();
			if (!customVideoShowcase)
			{
				currentAnimator = effects[newIndex].GetComponent<ControlVFXGraphs>().effects[1].GetComponentInParent<Animator>();
				currentAnimator.SetFloat("MotionTime", motionTime);
			}
		}
	}
}
