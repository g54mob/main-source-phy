using InControl;
using Landfall.TABS_Input;
using UnityEngine;

namespace LevelCreator
{
	public class EnabledByInputMode : MonoBehaviour
	{
		[SerializeField]
		public InputType inputType;

		private void Start()
		{
			PlayerActions.Instance.OnLastInputTypeChanged += OnLastInputTypeChanged;
			ToggleActivation();
		}

		private void OnDestroy()
		{
			PlayerActions.Instance.OnLastInputTypeChanged -= OnLastInputTypeChanged;
		}

		private void OnLastInputTypeChanged(BindingSourceType obj)
		{
			ToggleActivation();
		}

		private void ToggleActivation()
		{
			base.gameObject.SetActive(PlayerActions.Instance.InputType == inputType);
		}
	}
}
