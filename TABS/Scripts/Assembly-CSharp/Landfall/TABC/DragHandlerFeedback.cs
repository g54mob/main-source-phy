using System;
using Unity.Mathematics;
using UnityEngine;

namespace Landfall.TABC
{
	public class DragHandlerFeedback : MonoBehaviour
	{
		public RectTransform dragButton;

		private Canvas canvas;

		private bool isDragging;

		private void Awake()
		{
			canvas = dragButton.GetComponentInParent<Canvas>();
			dragButton.gameObject.SetActive(value: false);
		}

		private void Start()
		{
			DragHandler instance = DragHandler.instance;
			instance.startDragAction = (Action<UnitDataInstance, UnitButton, GameObject>)Delegate.Combine(instance.startDragAction, new Action<UnitDataInstance, UnitButton, GameObject>(StartDrag));
			DragHandler instance2 = DragHandler.instance;
			instance2.endDragAction = (Action<int2, UnitButton>)Delegate.Combine(instance2.endDragAction, new Action<int2, UnitButton>(StopDrag));
		}

		private void Update()
		{
			if (isDragging)
			{
				RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out var localPoint);
				dragButton.transform.position = canvas.transform.TransformPoint(localPoint);
				if (Input.mousePosition.y / (float)Screen.currentResolution.height > 0.2f)
				{
					dragButton.GetComponent<ScaleShake>().SetTarget(0.35f);
				}
				else
				{
					dragButton.GetComponent<ScaleShake>().SetTarget(0.7f);
				}
			}
		}

		private void StartDrag(UnitDataInstance unitDataToDrag, UnitButton bottonToDragFrom = null, GameObject draggedUnitObject = null)
		{
			if ((bool)bottonToDragFrom)
			{
				dragButton.transform.localScale = Vector3.one * 0.5f;
			}
			else
			{
				dragButton.transform.localScale = Vector3.one * 0f;
			}
			dragButton.gameObject.SetActive(value: true);
			dragButton.GetComponent<UnitButton>().SetUnit(unitDataToDrag, isOWned: false);
			Update();
			isDragging = true;
		}

		private void StopDrag(int2 boardPos, UnitButton unitButton)
		{
			dragButton.gameObject.SetActive(value: false);
			isDragging = false;
		}
	}
}
