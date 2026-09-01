using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorVoiceSelectCell : UnitEditorSelectableListItem
	{
		[SerializeField]
		protected Button previewButton;

		public Image m_image;

		public LocalizeText m_text;

		private VoiceBundle voiceBundle;

		private PlayerActions playerActions;

		protected override void Awake()
		{
			base.Awake();
			playerActions = PlayerActions.Instance;
		}

		private void Update()
		{
			if (isSelected && (bool)playerActions.m_previewUnitVoice)
			{
				Play();
			}
		}

		public void Init(VoiceBundle voiceBundle)
		{
			this.voiceBundle = voiceBundle;
			m_image.sprite = voiceBundle.Entity.SpriteIcon;
			m_text.LocaleID = voiceBundle.Entity.Name;
			base.gameObject.SetActive(value: true);
		}

		public void Play()
		{
			ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(voiceBundle.VocalRef, 5f, Vector3.zero);
		}

		public void SelectVoice()
		{
			UnitEditorManager unitEditorManager = Object.FindObjectOfType<UnitEditorManager>();
			if (unitEditorManager != null)
			{
				unitEditorManager.EquipedVoiceBundle(voiceBundle);
				unitEditorManager.UIManager.NavigateToPage("UNIT");
			}
		}

		public override bool ValidInFilter(string filter)
		{
			if (m_text.Text.text.ToLower().Contains(filter.ToLower()))
			{
				return true;
			}
			return false;
		}

		protected override void OnInputChanged(InputType inputType)
		{
			base.OnInputChanged(inputType);
			if (previewButton != null)
			{
				previewButton.gameObject.SetActive(inputType != InputType.Controller);
			}
		}
	}
}
