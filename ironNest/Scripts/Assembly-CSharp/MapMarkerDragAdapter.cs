using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class MapMarkerDragAdapter : MonoBehaviour, ICursorDraggable
{
	[Header("References")]
	[Tooltip("DynamicCursorManager that detects hover and drives cursor state. Used here to confirm this object is hovered before starting a drag.")]
	[SerializeField]
	private DynamicCursorManager cursorManager;

	[Header("Input System")]
	[Tooltip("Primary click actions (buttons). Any enabled action here can start/end a drag.\nRecommended: Universal/PrimaryClick. You can also add Player/Interact and/or UI/Click.")]
	[SerializeField]
	private List<InputActionReference> primaryClickActions;

	[Tooltip("If true, the actions above are enabled on OnEnable() (useful if not managed by PlayerInput).")]
	[SerializeField]
	private bool enableActionsOnEnable;

	private Interactable _interactable;

	private bool _wasPressed;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _started;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _canceled;

	public bool IsDragging { get; private set; }

	public event Action DragStarted
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action DragEnded
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private bool IsAnyActionPressed()
	{
		return false;
	}

	private void OnAnyStarted()
	{
	}

	private void OnAnyCanceled()
	{
	}

	private void ResolveEdge()
	{
	}

	private void BeginDragInternal()
	{
	}

	private void EndDragInternal()
	{
	}
}
