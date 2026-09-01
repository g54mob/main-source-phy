using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PrefabButton : MonoBehaviour, IEventSystemHandler, IPointerUpHandler
{
	public static PrefabButton LastPressed;

	public LevelPrefab container;

	private Button button;

	private Color normalColor;

	private LevelEditor levelEditor;

	protected void Start()
	{
		levelEditor = LevelEditor.Instance;
		button = GetComponent<Button>();
		normalColor = button.colors.normalColor;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		levelEditor.SetPrefab(container);
		LastPressed = this;
	}

	public void Reset()
	{
		ColorBlock colors = button.colors;
		colors.normalColor = normalColor;
		colors.highlightedColor = normalColor;
		button.colors = colors;
	}
}
