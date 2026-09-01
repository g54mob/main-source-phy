using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class DMRadioButtonSelector : MonoBehaviour
{
	[SerializeField]
	private Toggle defaultSelection;

	[SerializeField]
	private Toggle[] toggles;

	private void Start()
	{
		toggles.ForEach(delegate(Toggle x)
		{
			x.onValueChanged.AddListener(delegate
			{
				UpdateSelection(x);
			});
		});
		UpdateSelection(defaultSelection);
	}

	private void UpdateSelection(Toggle selectedToggle)
	{
		toggles.ForEach(delegate(Toggle x)
		{
			x.SetIsOnWithoutNotify(value: false);
			x.interactable = true;
		});
		selectedToggle.SetIsOnWithoutNotify(value: true);
		selectedToggle.interactable = false;
	}
}
