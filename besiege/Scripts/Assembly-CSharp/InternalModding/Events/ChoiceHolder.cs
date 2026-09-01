using System.Collections.Generic;
using Selectors;
using UnityEngine;

namespace InternalModding.Events
{
	public class ChoiceHolder : BaseHolder
	{
		public UIButton NextButton;

		public UIButton PrevButton;

		public Transform OptionParent;

		public GameObject OptionTemplate;

		public int CurrentOption;

		private string[] _options;

		private List<GameObject> OptionObjects;

		public string[] Options
		{
			get
			{
				return _options;
			}
			set
			{
				_options = value;
				InitOptions();
			}
		}

		public event OptionChangeHandler OptionChanged;

		public void Awake()
		{
			NextButton.Click += OnNext;
			PrevButton.Click += OnPrev;
		}

		private void InitOptions()
		{
			if (OptionObjects != null)
			{
				foreach (GameObject optionObject in OptionObjects)
				{
					Object.Destroy(optionObject);
				}
			}
			OptionObjects = new List<GameObject>();
			string[] options = Options;
			foreach (string text in options)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(OptionTemplate, OptionParent);
				gameObject.GetComponent<DynamicText>().SetText(text);
				gameObject.SetActive(false);
				OptionObjects.Add(gameObject);
			}
		}

		private void OnNext()
		{
			SetChoice((CurrentOption != Options.Length - 1) ? (CurrentOption + 1) : 0);
		}

		private void OnPrev()
		{
			SetChoice((CurrentOption != 0) ? (CurrentOption - 1) : (Options.Length - 1));
		}

		public void SetChoiceNoEvent(int newIndex)
		{
			OptionObjects[CurrentOption].SetActive(false);
			CurrentOption = newIndex;
			OptionObjects[CurrentOption].SetActive(true);
		}

		public void SetChoice(int newIndex)
		{
			SetChoiceNoEvent(newIndex);
			if (this.OptionChanged != null)
			{
				this.OptionChanged(CurrentOption);
			}
		}
	}
}
