using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleSettingsArray : MonoBehaviour
	{
		[Header("References")]
		public TMP_Text valueLabel;

		public Button left;

		public Button right;

		public Selectable selectable;

		[Space]
		public UnityEvent<int, string> onValueChanged;

		[Header("Parameters")]
		public string[] options;

		public int defaultOption;

		public bool loop;

		private int optionIndex;

		public int OptionIndex => optionIndex;

		public string Option
		{
			get
			{
				if (options.Length == 0)
				{
					return "";
				}
				return options[optionIndex];
			}
		}

		private void OnValidate()
		{
			SetDefaultOption();
		}

		private void Start()
		{
			SetDefaultOption();
			if (left != null)
			{
				left.onClick.AddListener(OnLeftClick);
			}
			if (right != null)
			{
				right.onClick.AddListener(OnRightClick);
			}
		}

		private void Update()
		{
			if (selectable != null && EventSystem.current.currentSelectedGameObject == selectable.gameObject)
			{
				if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
				{
					OnLeftClick();
				}
				else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
				{
					OnRightClick();
				}
			}
		}

		private void UpdateUI()
		{
			if (options.Length == 0)
			{
				valueLabel.SetText("");
			}
			else
			{
				valueLabel.SetText(options[optionIndex]);
			}
		}

		public void SetOption(int newOption)
		{
			if (newOption < 0)
			{
				newOption = (loop ? (options.Length - 1) : 0);
			}
			if (newOption >= options.Length)
			{
				newOption = ((!loop) ? (options.Length - 1) : 0);
			}
			if (left != null)
			{
				left.interactable = loop || newOption > 0;
			}
			if (right != null)
			{
				right.interactable = loop || newOption < options.Length - 1;
			}
			if (optionIndex != newOption)
			{
				optionIndex = newOption;
				onValueChanged?.Invoke(optionIndex, options[optionIndex]);
			}
			UpdateUI();
		}

		public void OnLeftClick()
		{
			SetOption(optionIndex - 1);
		}

		public void OnRightClick()
		{
			SetOption(optionIndex + 1);
		}

		public void SetDefaultOption()
		{
			SetOption(defaultOption);
		}
	}
}
