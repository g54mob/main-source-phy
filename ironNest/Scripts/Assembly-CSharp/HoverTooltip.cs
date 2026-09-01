using Localisation;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Gameplay/Hover Tooltip")]
public class HoverTooltip : MonoBehaviour
{
	[Header("References")]
	[Tooltip("Camera used to project world positions to screen space.\n\nMust match the camera assigned to DynamicCursorManager.\n\nIf left null, auto-fetched via Camera.main in Awake.")]
	[SerializeField]
	private Camera raycastCamera;

	[SerializeField]
	private TextMeshProUGUI tmpText;

	[SerializeField]
	private TextIdentifier textIdentifier;

	[Header("Positioning")]
	[Tooltip("Pixel offset applied to the projected world anchor position.\n\nPositive Y moves the tooltip above the anchor point.\nPositive X moves it to the right.\n\nSafe default: (0, 60) — centred above the item.")]
	[SerializeField]
	private Vector2 tooltipScreenOffset;

	[Header("Debug")]
	[Tooltip("If true, logs Show and Hide calls to the Console.\n\nSafe default: false.")]
	[SerializeField]
	private bool debugLogs;

	private RectTransform _rectTransform;

	private Transform _worldAnchor;

	private bool _visible;

	private void Awake()
	{
	}

	private void LateUpdate()
	{
	}

	public void Show(Transform worldAnchor)
	{
	}

	public void Show(Transform worldAnchor, string addition)
	{
	}

	public void Show(string addition)
	{
	}

	public void Hide()
	{
	}

	private void UpdateScreenPosition()
	{
	}
}
