using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class WobbleButton : MonoBehaviour, IPointerExitHandler, IEventSystemHandler, IPointerEnterHandler
{
	private RenderMode _003CParentCanvasRenderMode_003Ek__BackingField;

	public Camera TargetCamera;

	public AudioSource SpringAudioSource;

	public Animator TargetAnimator;

	public HapticSource SpringHapticSource;

	public Image TargetModel;

	public float OffDuration = 0.1f;

	public float MaxRange;

	public AnimationCurve WobbleCurve;

	public float DragResetDuration = 4f;

	public float WobbleFactor = 2f;

	protected Vector3 _neutralPosition;

	protected Canvas _canvas;

	protected Vector3 _newTargetPosition;

	protected Vector3 _eventPosition;

	protected Vector2 _workPosition;

	protected float _initialZPosition;

	protected bool _dragging;

	protected int _pointerID;

	protected PointerEventData _pointerEventData;

	protected RectTransform _rectTransform;

	protected Vector3 _dragEndedPosition;

	protected float _dragEndedAt;

	protected Vector3 _dragResetDirection;

	protected bool _pointerOn;

	protected bool _draggedOnce;

	protected int _sparkAnimationParameter;

	protected long[] _wobbleAndroidPattern = new long[4] { 0L, 40L, 40L, 80L };

	protected int[] _wobbleAndroidAmplitude = new int[4] { 0, 40, 0, 80 };

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
	}

	public virtual void SetPitch(float newPitch)
	{
		SpringAudioSource.pitch = newPitch;
		float num = newPitch - 0.3f;
		float num2 = num / 0.7f;
		float num3 = num2 + num2;
		float frequencyShift = num3 - 1f;
		SpringHapticSource.frequencyShift = frequencyShift;
	}

	public virtual void Initialization()
	{
		//IL_008d: Expected I, but got O
		//IL_009d: Expected O, but got I
		//IL_00ad: Expected O, but got I
		Canvas canvas = default(Canvas);
		RectTransform rectTransform = default(RectTransform);
		while (true)
		{
			int sparkAnimationParameter = Animator.StringToHash("Spark");
			_sparkAnimationParameter = sparkAnimationParameter;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			RenderMode renderMode = canvas.renderMode;
			_003CParentCanvasRenderMode_003Ek__BackingField = renderMode;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			_canvas = canvas;
			Transform transform = base.transform;
			_initialZPosition = transform.position.z;
			GameObject gameObject = base.gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			_rectTransform = rectTransform;
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rdx_v12 (Il2CppClass<Lofelt.NiceVibrations.WobbleButton>)+1C8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rdx_v12 (Il2CppClass<Lofelt.NiceVibrations.WobbleButton>)+1D0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v109 @ rax_v14 (should have been resolved before IL gen)");
		}
	}

	public virtual void SetNeutralPosition()
	{
		//IL_0030: Expected O, but got F4
		Transform transform = _rectTransform.transform;
		Vector3 position = transform.position;
		_neutralPosition = (Vector3)position.x;
		_ = position.z;
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
				bool flag3 = RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, worldCamera, out *(Vector2*)(this + 152));
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

	protected unsafe virtual void Update()
	{
		//IL_011b: Expected I, but got O
		//IL_0046: Expected I, but got O
		//IL_0050: Expected O, but got Ref
		//IL_0063: Expected O, but got F4
		//IL_00aa: Invalid comparison between F4 and O
		//IL_00ca: Invalid comparison between F4 and I4
		if (_pointerOn && !_dragging)
		{
			nint num = (nint)this;
			float num2 = default(float);
			Vector3 worldPosition = GetWorldPosition((Vector3)(&num2));
			_newTargetPosition = (Vector3)worldPosition.x;
			float num3 = worldPosition.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.WobbleButton)+70]");
			float num4 = num3 - 0f;
			object obj2 = default(object);
			object obj3 = default(object);
			object obj = obj2 - obj3;
			_ = worldPosition.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			float maxRange = MaxRange;
			bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)maxRange) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3);
			float num5 = MaxRange - (float)obj3;
			bool flag2 = num5 == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			bool dragging = flag4 & flag3;
			_dragging = dragging;
		}
		while (true)
		{
			nint num6 = (nint)this;
			if (!_dragging)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v103 @ rdx_v2 (Il2CppClass<Lofelt.NiceVibrations.WobbleButton>)+208] (should have been resolved before IL gen)");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v103 @ rdx_v2 (Il2CppClass<Lofelt.NiceVibrations.WobbleButton>)+1F8] (should have been resolved before IL gen)");
		}
	}

	protected unsafe virtual void StickToPointer()
	{
		//IL_0015: Expected I, but got O
		//IL_002f: Expected O, but got Ref
		//IL_0042: Expected O, but got F4
		//IL_0090: Expected O, but got Ref
		_draggedOnce = true;
		nint num = (nint)this;
		Vector3 vector = default(Vector3);
		_eventPosition = vector;
		_ = 0;
		float num2 = default(float);
		Vector3 worldPosition = GetWorldPosition((Vector3)(&num2));
		_newTargetPosition = (Vector3)worldPosition.x;
		object obj2 = default(object);
		object obj = obj2 - (object)vector;
		_ = worldPosition.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180467E20");
		_newTargetPosition = vector;
		_ = _initialZPosition;
		Transform transform = base.transform;
		transform.position = (Vector3)(&num2);
	}

	protected unsafe virtual void GoBackToInitialPosition()
	{
		//IL_00f8: Expected O, but got I
		//IL_0149: Expected O, but got Ref
		if (_draggedOnce)
		{
			float time = Time.time;
			float num = time - _dragEndedAt;
			Vector3 neutralPosition = default(Vector3);
			if (!(DragResetDuration > num))
			{
				_newTargetPosition = _neutralPosition;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.WobbleButton)+70]");
				_ = 0;
			}
			else
			{
				float time2 = Time.time;
				float num2 = time2 - _dragEndedAt;
				float num3 = Remap(num2, 0f, DragResetDuration, 0f, 0f);
				float num4 = WobbleCurve.Evaluate(num2);
				float num5 = num4 * WobbleFactor;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.WobbleButton)+C8]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.WobbleButton)+70]");
				object obj = num6 - 0;
				float num7 = (float)obj * num5;
				float num8 = num7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.WobbleButton)+70]");
				float num9 = num8 + 0f;
				Vector3 newTargetPosition = default(Vector3);
				_newTargetPosition = newTargetPosition;
				neutralPosition = _neutralPosition;
			}
			_ = _initialZPosition;
			Transform transform = base.transform;
			transform.position = (Vector3)(&neutralPosition);
		}
	}

	public virtual void OnPointerEnter(PointerEventData data)
	{
		_pointerID = data._003CpointerId_003Ek__BackingField;
		_pointerEventData = data;
		_pointerOn = true;
	}

	public unsafe virtual void OnPointerExit(PointerEventData data)
	{
		//IL_0005: Expected I, but got O
		//IL_001f: Expected O, but got Ref
		//IL_0032: Expected O, but got F4
		//IL_00c2: Expected O, but got I
		nint num = (nint)this;
		Vector3 vector = default(Vector3);
		_eventPosition = vector;
		_ = 0;
		object obj = default(object);
		Vector3 worldPosition = GetWorldPosition((Vector3)(&obj));
		_newTargetPosition = (Vector3)worldPosition.x;
		object obj3 = default(object);
		object obj2 = obj3 - (object)vector;
		_ = worldPosition.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180467E20");
		_dragging = false;
		_newTargetPosition = vector;
		_ = _initialZPosition;
		_dragEndedPosition = _newTargetPosition;
		_ = _initialZPosition;
		float time = Time.time;
		_dragEndedAt = time;
		_pointerOn = false;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.WobbleButton)+C8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.WobbleButton)+70]");
		object obj4 = num2 - 0;
		_dragResetDirection = vector;
		TargetAnimator.SetTrigger(_sparkAnimationParameter);
		SpringAudioSource.Play();
		SpringHapticSource.Play();
	}

	protected virtual float Remap(float x, float A, float B, float C, float D)
	{
		float num = x - A;
		object obj2 = default(object);
		object obj3 = default(object);
		object obj = obj2 - obj3;
		float num2 = B - A;
		float num3 = num / num2;
		float num4 = num3 * (float)obj;
		return num4 + (float)obj3;
	}
}
