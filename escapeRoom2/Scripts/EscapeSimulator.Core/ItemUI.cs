using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
	public CanvasGroup panel;

	public Vector2 initialAnchoredPosition;

	public Text nameLbl;

	public Button buttonKey;

	public Button buttonHint;

	public Button buttonTool;

	public Button buttonContainer;

	public Button buttonTrashcan;

	public bool shouldInvertPopup;

	public Vector2 offsetPosition;

	public float addHeight;

	public Sprite wearIcon;

	[HideInInspector]
	public GameObject selectedObject;
}
