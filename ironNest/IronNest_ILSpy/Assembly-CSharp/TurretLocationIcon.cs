using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class TurretLocationIcon : MonoBehaviour
{
	public GameObject VisualRoot;

	public bool UpdateVisualOnMove;

	public string RevealAreaTag;

	public float RectanglePadding;

	public bool StartWithVisualRootHidden;

	public bool IgnoreParentRotation;

	public float ScanWindowDurationSeconds;

	public float ScanIntervalSeconds;

	public UnityEvent<TurretLocationIcon> OnMove;

	public UnityEvent<TurretLocationIcon> OnRevealed;

	private static bool warnedMissingRevealTag;

	private Vector3 visualRootWorldPosition;

	private Quaternion visualRootWorldRotation;

	private bool hasVisualRootWorldPosition;

	private bool hasVisualRootWorldRotation;

	private bool scanActive;

	private float scanWindowEndTime;

	private float nextScanTime;

	private void Awake()
	{
		if (StartWithVisualRootHidden && VisualRoot != null)
		{
			GameObject gameObject = base.gameObject;
			if (VisualRoot != gameObject)
			{
				VisualRoot.SetActive(value: false);
			}
		}
	}

	private void Start()
	{
		CacheVisualRootWorldRotation();
	}

	private void OnEnable()
	{
		Action<Vector2, float> value = OnImpact;
		ImpactTracker.OnImpact += value;
	}

	private void OnDisable()
	{
		Action<Vector2, float> value = OnImpact;
		ImpactTracker.OnImpact -= value;
	}

	private void LateUpdate()
	{
		//IL_008a: Invalid comparison between I4 and F4
		//IL_009c: Expected F4, but got I4
		KeepVisualRootLocked();
		if (!scanActive)
		{
			return;
		}
		float time = Time.time;
		if (time < scanWindowEndTime)
		{
			float time2 = Time.time;
			if (time2 < nextScanTime)
			{
				return;
			}
			float time3 = Time.time;
			bool flag = !(0f < ScanIntervalSeconds);
			float num = 0f;
			if (!flag)
			{
				num = ScanIntervalSeconds;
			}
			float num2 = num + time3;
			nextScanTime = num2;
			if (!EvaluateRevealArea())
			{
				return;
			}
		}
		scanWindowEndTime = 0f;
		scanActive = false;
	}

	public unsafe void OnLocationMoved()
	{
		//IL_00d2: Expected O, but got Ref
		//IL_010d: Expected O, but got F4
		if (UpdateVisualOnMove && VisualRoot != null && hasVisualRootWorldPosition)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Transform transform = VisualRoot.transform;
			UnityEngine.Object obj = default(UnityEngine.Object);
			Transform transform2;
			if (obj != null)
			{
				transform2 = (Transform)obj;
			}
			else
			{
				Transform transform3 = base.transform;
				transform2 = transform3;
			}
			Vector3 position = transform2.position;
			object obj2 = default(object);
			transform.position = (Vector3)(&obj2);
			ApplyVisualRootWorldRotation();
			Transform transform4 = VisualRoot.transform;
			Vector3 position2 = transform4.position;
			visualRootWorldPosition = (Vector3)position2.x;
			_ = position2.z;
		}
		KeepVisualRootLocked();
		if (OnMove != null)
		{
			OnMove.Invoke(this);
		}
	}

	public void StartScanWindow()
	{
		//IL_003a: Invalid comparison between I4 and F4
		//IL_004c: Expected F4, but got I4
		float time = Time.time;
		scanActive = true;
		float num = time + ScanWindowDurationSeconds;
		scanWindowEndTime = num;
		bool flag = !(0f < ScanIntervalSeconds);
		float num2 = 0f;
		if (!flag)
		{
			num2 = ScanIntervalSeconds;
		}
		float num3 = num2 + time;
		nextScanTime = num3;
	}

	public void StopScanWindow()
	{
		scanActive = false;
		scanWindowEndTime = 0f;
	}

	private void OnImpact(Vector2 impactLocation, float impactRadius)
	{
		//IL_0064: Expected F4, but got I4
		//IL_006d: Expected F4, but got I4
		//IL_0076: Expected O, but got I4
		//IL_00b2: Expected O, but got I4
		//IL_00bb: Expected O, but got I4
		//IL_002c: Invalid comparison between I4 and F4
		//IL_003e: Expected F4, but got I4
		//IL_00a4: Expected O, but got I4
		float num2;
		if (!EvaluateRevealArea())
		{
			float time = Time.time;
			bool flag = !(0f < ScanIntervalSeconds);
			float num = 0f;
			if (!flag)
			{
				num = ScanIntervalSeconds;
			}
			num2 = num + time;
			float num3 = time + ScanWindowDurationSeconds;
			object obj = 1;
		}
		else
		{
			num2 = 0f;
			float num3 = 0f;
			object obj = 0;
		}
		object obj2 = 118;
		object obj3 = 120;
		nextScanTime = num2;
	}

	private unsafe bool EvaluateRevealArea()
	{
		//IL_01e5: Expected I4, but got O
		//IL_0210: Expected O, but got Ref
		//IL_0123: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		Transform transform;
		if (obj != null)
		{
			if ((object)obj == null)
			{
				goto IL_01d7;
			}
			transform = (Transform)obj;
		}
		else
		{
			Transform transform2 = base.transform;
			if ((object)transform2 == null)
			{
				goto IL_01d7;
			}
			transform = transform2;
		}
		Vector3 position = transform.position;
		Vector3 vector = default(Vector3);
		if (VisualRoot != null)
		{
			if ((object)VisualRoot == null)
			{
				goto IL_01d7;
			}
			if (VisualRoot.activeSelf && hasVisualRootWorldPosition)
			{
				bool flag = CheckTaggedRectangles((Vector3)(&vector));
				bool flag2 = !flag;
				vector = visualRootWorldPosition;
				if (!flag2)
				{
					if (VisualRoot != null)
					{
						if ((object)VisualRoot == null)
						{
							goto IL_01d7;
						}
						VisualRoot.SetActive(value: false);
					}
					hasVisualRootWorldPosition = false;
					vector = visualRootWorldPosition;
				}
			}
		}
		bool flag3 = CheckTaggedRectangles((Vector3)(&vector));
		if (!flag3)
		{
			return flag3;
		}
		RevealVisualRoot();
		return true;
		IL_01d7:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private unsafe bool CheckTaggedRectangles(Vector3 worldPos)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_0067: Expected O, but got I4
		//IL_018e: Expected I4, but got O
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0113: Expected O, but got Ref
		if (!string.IsNullOrEmpty(RevealAreaTag))
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(RevealAreaTag);
			if (array != null && array.Length != 0)
			{
				object obj = array + 32;
				object obj2 = 0;
				float num = default(float);
				while ((nint)obj2 < array.Length)
				{
					if ((nint)obj2 < array.Length)
					{
						if ((UnityEngine.Object)obj != null && ((GameObject)obj).activeInHierarchy)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							if (this != null && IsWorldPointInsideRectTransform((RectTransform)(object)this, (Vector3)(&num), RectanglePadding))
							{
								return true;
							}
						}
						obj2++;
						obj += 8;
						continue;
					}
					IndexOutOfRangeException ex = new IndexOutOfRangeException();
					return (byte)(int)ex != 0;
				}
			}
		}
		return false;
	}

	private unsafe void RevealVisualRoot()
	{
		//IL_00d5: Expected O, but got Ref
		//IL_00ef: Expected O, but got F4
		scanWindowEndTime = 0f;
		scanActive = false;
		if (VisualRoot != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj = default(UnityEngine.Object);
			Transform transform;
			if (obj != null)
			{
				transform = (Transform)obj;
			}
			else
			{
				Transform transform2 = base.transform;
				transform = transform2;
			}
			Vector3 position = transform.position;
			if (!VisualRoot.activeSelf)
			{
				VisualRoot.SetActive(value: true);
			}
			Transform transform3 = VisualRoot.transform;
			object obj2 = default(object);
			transform3.position = (Vector3)(&obj2);
			ApplyVisualRootWorldRotation();
			visualRootWorldPosition = (Vector3)position.x;
			_ = position.z;
			hasVisualRootWorldPosition = true;
			if (OnRevealed != null)
			{
				OnRevealed.Invoke(this);
			}
		}
	}

	private void HideVisualRoot()
	{
		if (VisualRoot != null)
		{
			VisualRoot.SetActive(value: false);
		}
		hasVisualRootWorldPosition = false;
	}

	private unsafe void KeepVisualRootLocked()
	{
		//IL_009d: Expected O, but got Ref
		bool flag = VisualRoot == null;
		if (!flag && hasVisualRootWorldPosition != flag)
		{
			Transform transform = VisualRoot.transform;
			if (UpdateVisualOnMove)
			{
				Transform transform2 = base.transform;
				Vector3 position = transform2.position;
			}
			object obj = default(object);
			transform.position = (Vector3)(&obj);
			ApplyVisualRootWorldRotation();
		}
	}

	private void CacheVisualRootWorldRotation()
	{
		//IL_005e: Expected O, but got F4
		if (VisualRoot != null)
		{
			Transform transform = VisualRoot.transform;
			Quaternion rotation = transform.rotation;
			hasVisualRootWorldRotation = true;
			visualRootWorldRotation = (Quaternion)rotation.x;
		}
	}

	private unsafe void ApplyVisualRootWorldRotation()
	{
		//IL_0075: Expected O, but got Ref
		if (!IgnoreParentRotation)
		{
			return;
		}
		bool flag = VisualRoot == null;
		if (!flag)
		{
			if (hasVisualRootWorldRotation == flag)
			{
				CacheVisualRootWorldRotation();
			}
			Transform transform = VisualRoot.transform;
			object obj = default(object);
			transform.rotation = (Quaternion)(&obj);
		}
	}

	private unsafe static bool IsWorldPointInsideRectTransform(RectTransform rect, Vector3 worldPoint, float padding)
	{
		//IL_022b: Expected I4, but got O
		//IL_0012: Expected O, but got Ref
		//IL_003b: Invalid comparison between F4 and I4
		//IL_0193: Invalid comparison between O and F4
		//IL_01c3: Invalid comparison between F4 and O
		//IL_01e1: Invalid comparison between F4 and I4
		if ((object)rect != null)
		{
			object obj = default(object);
			Vector3 vector = rect.InverseTransformPoint((Vector3)(&obj));
			Rect rect2 = rect.rect;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018047C5EDh\"");
			float num;
			float num2;
			float num3 = default(float);
			float num4;
			float num5 = default(float);
			float num6;
			float num7 = default(float);
			if (padding == 0f)
			{
				num = rect2.m_XMin;
				num2 = num3;
				num4 = num5;
				num6 = num7;
			}
			else
			{
				num = rect2.m_XMin - padding;
				float num8 = num5 + rect2.m_XMin;
				num2 = num3 - padding;
				float num9 = num8 - num;
				float num10 = num9 + num;
				float num11 = num7 + num3;
				float num12 = num10 + padding;
				float num13 = num11 - num2;
				num4 = num12 - num;
				float num14 = num13 + num2;
				float num15 = num14 + padding;
				num6 = num15 - num2;
			}
			if (!(vector.x < num))
			{
				float num16 = num4 + num;
				object obj2 = default(object);
				if (num16 > vector.x && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
				{
					float num17 = num6 + num2;
					bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num17) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
					float num18 = num17 - (float)obj2;
					bool flag2 = num18 == 0f;
					bool flag3 = !flag;
					bool flag4 = !flag2;
					return flag4 & flag3;
				}
			}
			return false;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public TurretLocationIcon()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A3BD]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		RevealAreaTag = "ImpactRevealArea";
		StartWithVisualRootHidden = true;
		ScanWindowDurationSeconds = 3f;
		ScanIntervalSeconds = 0.2f;
		base._002Ector();
	}
}
