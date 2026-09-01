using UnityEngine;

namespace Selectors
{
	public class LogicToggleSelector : Selector
	{
		[SerializeField]
		private Renderer logicBackground;

		[SerializeField]
		private Renderer settingsBackground;

		private UIButton logicButton;

		private UIButton settingsButton;

		private Material normalMaterial;

		public override MapperType MapperType
		{
			get
			{
				return Toggle;
			}
			set
			{
				if (Toggle != value)
				{
					Toggle = (MToggle)value;
				}
			}
		}

		public MToggle Toggle { get; set; }

		private void Awake()
		{
			logicButton = logicBackground.gameObject.GetComponentInParent<UIButton>();
			logicButton.Click += OnLogicClick;
			settingsButton = settingsBackground.gameObject.GetComponentInParent<UIButton>();
			settingsButton.Click += OnSettingsClick;
		}

		public override void Init()
		{
			base.Init();
			UpdateVisual();
		}

		public void ToggleLogic(bool toggle)
		{
			if (Toggle != null && toggle != Toggle.IsActive)
			{
				Toggle.IsActive = toggle;
				UpdateVisual();
			}
		}

		private void OnSettingsClick()
		{
			Toggle.IsActive = false;
			UpdateVisual();
		}

		private void OnLogicClick()
		{
			Toggle.IsActive = true;
			UpdateVisual();
		}

		protected override void UpdateVisual()
		{
			logicBackground.gameObject.SetActive(!Toggle.IsActive);
			settingsBackground.gameObject.SetActive(Toggle.IsActive);
		}
	}
}
