using System;
using UnityEngine.EventSystems;

public class PropertyData
{
	public PineUIComponent ui;

	public PointerEventData dragData;

	public Action onUpdate;

	public Action onCancel;
}
