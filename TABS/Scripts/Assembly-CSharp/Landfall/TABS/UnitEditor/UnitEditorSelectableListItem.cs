using Landfall.TABS_Input;
using TFBGames;

namespace Landfall.TABS.UnitEditor
{
	public abstract class UnitEditorSelectableListItem : UnitEditorSelectableItem
	{
		protected InputService inputService;

		protected override void Awake()
		{
			base.Awake();
			inputService = ServiceLocator.GetService<InputService>();
			inputService.InputChanged += OnInputChanged;
			OnInputChanged(inputService.CurrentInputType);
		}

		protected override void OnDestroy()
		{
			if (inputService != null)
			{
				inputService.InputChanged -= OnInputChanged;
			}
			base.OnDestroy();
		}

		protected virtual void OnInputChanged(InputType inputType)
		{
			if (correspondingButton != null)
			{
				correspondingButton.gameObject.SetActive(inputType != InputType.Controller);
			}
		}

		public abstract bool ValidInFilter(string filter);
	}
}
