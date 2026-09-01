using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class MMKnob : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	private RenderMode _003CParentCanvasRenderMode_003Ek__BackingField;

	public Camera TargetCamera;

	public float MinimumAngle;

	public float MaximumAngle;

	public float MaximumDistance;

	public Color ActiveColor;

	public Color InactiveColor;

	public bool Dragging;

	public float Value;

	public bool Active;

	public Image _image;

	protected PointerEventData _pointerEventData;

	protected float _distance;

	public RectTransform _rectTransform;

	protected Vector3 _rotation;

	protected Canvas _canvas;

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

	protected virtual void Awake()
	{
		//IL_007b: Expected I, but got O
		//IL_0095: Expected O, but got I
		//IL_00a5: Expected O, but got I
		Image image = default(Image);
		Canvas canvas = default(Canvas);
		Canvas canvas2 = default(Canvas);
		RectTransform rectTransform = default(RectTransform);
		while (true)
		{
			GameObject gameObject = base.gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			_image = image;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			_canvas = canvas;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			RenderMode renderMode = canvas2.renderMode;
			_003CParentCanvasRenderMode_003Ek__BackingField = renderMode;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			_rectTransform = rectTransform;
			nint num = (nint)this;
			float minimumAngle = MinimumAngle;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ r8_v6 (Il2CppClass<Lofelt.NiceVibrations.MMKnob>)+1B8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ r8_v6 (Il2CppClass<Lofelt.NiceVibrations.MMKnob>)+1C0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v97 @ rax_v13 (should have been resolved before IL gen)");
		}
	}

	protected unsafe virtual void Update()
	{
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected Ref, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		object obj = default(object);
		if (Active)
		{
			Color color = (Color)(obj - 64);
			_ = ActiveColor;
			_image.color = color;
			if (!Dragging)
			{
				return;
			}
			Transform transform = base.transform;
			_ = transform.position.x;
			Vector3 testPosition = (Vector3)(obj - 80);
			_ = 0;
			Vector3 worldPosition = GetWorldPosition(testPosition);
			_ = worldPosition.z;
			_ = worldPosition.x;
			Vector2 vector = default(Vector2);
			float num = Vector2.SignedAngle(vector, vector);
			float num2;
			if (!(-130f > num))
			{
				bool flag = !(num > 130f);
				num2 = num;
				if (!flag)
				{
					num2 = 130f;
				}
			}
			else
			{
				num2 = -130f;
			}
			float num3 = MinimumAngle - MaximumAngle;
			float num4 = num2 - -130f;
			float num5 = num4 / 260f;
			float num6 = num3 * num5;
			float num7 = num6 + MaximumAngle;
			Transform transform2 = base.transform;
			Vector3 position = transform2.position;
			ref Vector3 euler = ref *(Vector3*)(obj - 80);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.MMKnob)+90]");
			float num8 = 0f * ((float)Math.PI / 180f);
			_ = _rotation;
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			Quaternion rotation = (Quaternion)(obj - 64);
			Vector3 position2 = (Vector3)(obj - 80);
			_ = position.x;
			_ = quaternion.x;
			_ = position.z;
			_rectTransform.SetPositionAndRotation(position2, rotation);
			float num9 = num2 - -130f;
			float num10 = num9 / 260f;
			float num11 = num10 * -1f;
			float value = num11 + 1f;
			Value = value;
		}
		else
		{
			Dragging = false;
			Color color2 = (Color)(obj - 64);
			_ = InactiveColor;
			_image.color = color2;
		}
	}

	protected unsafe virtual void SetRotation(float angle)
	{
		//IL_0080: Expected O, but got Ref
		//IL_0080: Expected O, but got Ref
		float maximumAngle = MaximumAngle;
		float num = default(float);
		if (!(MaximumAngle > num))
		{
			maximumAngle = MinimumAngle;
			if (num > MinimumAngle)
			{
				goto IL_004e;
			}
		}
		Transform transform = base.transform;
		goto IL_004e;
		IL_004e:
		Vector3 position = transform.position;
		Vector3 euler = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		object obj = default(object);
		_rectTransform.SetPositionAndRotation((Vector3)(&euler), (Quaternion)(&obj));
	}

	public virtual void SetActive(bool status)
	{
		Active = status;
	}

	public unsafe virtual void SetValue(float value)
	{
		//IL_008f: Expected O, but got Ref
		//IL_008f: Expected O, but got Ref
		SetRotation(MinimumAngle);
		float num = MaximumAngle - MinimumAngle;
		Value = value;
		float num2 = num * value;
		float num3 = num2 + MinimumAngle;
		Transform transform = base.transform;
		Vector3 position = transform.position;
		Vector3 euler = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		object obj = default(object);
		_rectTransform.SetPositionAndRotation((Vector3)(&euler), (Quaternion)(&obj));
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_pointerEventData = eventData;
		Dragging = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_pointerEventData = null;
		Dragging = false;
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
				bool flag3 = RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, worldCamera, out *(Vector2*)(this + 160));
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

	public MMKnob()
	{
		//IL_003f: Expected I, but got O
		MinimumAngle = 45f;
		MaximumAngle = -225f;
		MaximumDistance = 50f;
		Active = true;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		_rotation = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rcx_v2 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		base._002Ector();
	}
}
