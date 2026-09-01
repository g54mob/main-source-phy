using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lofelt.NiceVibrations;

public class BallTouchZone : MonoBehaviour, IPointerExitHandler, IEventSystemHandler, IPointerEnterHandler
{
	private RenderMode _003CParentCanvasRenderMode_003Ek__BackingField;

	public RectTransform BallMover;

	protected bool _holding;

	protected PointerEventData _pointerEventData;

	protected Vector3 _newPosition;

	protected Canvas _canvas;

	protected float _initialZPosition;

	protected Vector2 _workPosition;

	public RenderMode ParentCanvasRenderMode
	{
		get
		{
			return _003CParentCanvasRenderMode_003Ek__BackingField;
		}
		protected set
		{
			_003CParentCanvasRenderMode_003Ek__BackingField = value;
		}
	}

	protected virtual void Start()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.BallTouchZone>)+1A8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.BallTouchZone>)+1B0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected virtual void Initialization()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
		Canvas canvas = default(Canvas);
		RenderMode renderMode = canvas.renderMode;
		_003CParentCanvasRenderMode_003Ek__BackingField = renderMode;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
		_canvas = canvas;
		Transform transform = base.transform;
		_initialZPosition = transform.position.z;
	}

	protected unsafe virtual void Update()
	{
		//IL_0031: Expected O, but got Ref
		//IL_0044: Expected O, but got F4
		//IL_006a: Expected O, but got Ref
		//IL_0079: Expected I, but got O
		Vector3 vector = default(Vector3);
		Vector3 vector2 = default(Vector3);
		if (_holding)
		{
			Vector3 worldPosition = GetWorldPosition((Vector3)(&vector));
			_newPosition = (Vector3)worldPosition.x;
			_ = worldPosition.z;
			vector = vector2;
		}
		else
		{
			nint num = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v10 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rcx_v7 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
			float num3 = 0f * 5000f;
			_newPosition = vector2;
			vector = Vector3.oneVector;
		}
		_ = _initialZPosition;
		BallMover.position = (Vector3)(&vector);
	}

	protected unsafe virtual Vector3 GetWorldPosition(Vector3 testPosition)
	{
		//IL_0012: Expected native int or pointer, but got O
		//IL_0024: Expected native int or pointer, but got O
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected Ref, but got Unknown
		//IL_0127: Expected O, but got Ref
		//IL_0138: Expected native int or pointer, but got O
		//IL_014a: Expected native int or pointer, but got O
		Vector3 vector = default(Vector3);
		if (_003CParentCanvasRenderMode_003Ek__BackingField != RenderMode.ScreenSpaceCamera)
		{
			((Vector3*)(nint)vector)->x = testPosition.x;
			((Vector3*)(nint)vector)->z = testPosition.z;
			return vector;
		}
		if ((object)_canvas != null)
		{
			Transform transform = _canvas.transform;
			if ((object)_canvas != null)
			{
				Camera worldCamera = _canvas.worldCamera;
				bool flag = (object)transform == null;
				RectTransform rect = null;
				if (!flag)
				{
					bool flag2 = (object)transform.GetType() != typeof(RectTransform);
					rect = null;
					if (!flag2)
					{
						rect = (RectTransform)transform;
					}
				}
				Vector2 screenPoint = default(Vector2);
				bool flag3 = RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, worldCamera, out *(Vector2*)(this + 92));
				if ((object)_canvas != null)
				{
					Transform transform2 = _canvas.transform;
					if ((object)transform2 != null)
					{
						float num = default(float);
						Vector3 vector2 = transform2.TransformPoint((Vector3)(&num));
						((Vector3*)(nint)vector)->x = vector2.x;
						((Vector3*)(nint)vector)->z = vector2.z;
						return vector;
					}
				}
			}
		}
		return (Vector3)new NullReferenceException();
	}

	public virtual void OnPointerEnter(PointerEventData data)
	{
		_holding = true;
		_pointerEventData = data;
	}

	public virtual void OnPointerExit(PointerEventData data)
	{
		_holding = false;
	}
}
