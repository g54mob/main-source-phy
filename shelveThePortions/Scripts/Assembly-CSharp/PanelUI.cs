using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PanelUI<T> : SceneSingleton<T>, IUISelectable where T : PanelUI<T>
{
	[SerializeField]
	protected GameObject selectedButton;

	public virtual void SetSelectedButton()
	{
		EventSystem.current.SetSelectedGameObject(selectedButton);
	}
}
