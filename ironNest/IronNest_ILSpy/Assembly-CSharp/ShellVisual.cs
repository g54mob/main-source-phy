using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class ShellVisual : MonoBehaviour
{
	private RectTransform rectTransform;

	private RectTransform boardRect;

	private ShellVisualBoundaryConfig config;

	private Vector2 startLocalPos;

	private Vector2 targetLocalPos;

	private float travelTime;

	private double startedAt;

	private double endsAt;

	private ShellDefinition impactShell;

	private Vector2 previousPos;

	private bool hasExitedMap;

	private float totalPathDistance;

	public unsafe void Initialize(Vector2 startPos, Vector2 targetPos, float travelDuration, ShellDefinition shell)
	{
		//IL_0108: Expected O, but got Ref
		//IL_013a: Expected F4, but got O
		//IL_0165: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		RectTransform rectTransform = default(RectTransform);
		this.rectTransform = rectTransform;
		Transform transform;
		if (this.rectTransform != null)
		{
			Transform parent = this.rectTransform.parent;
			bool flag = (object)parent == null;
			transform = null;
			if (!flag)
			{
				bool flag2 = (object)parent.GetType() != typeof(RectTransform);
				transform = null;
				if (!flag2)
				{
					transform = parent;
				}
			}
		}
		else
		{
			transform = null;
		}
		boardRect = (RectTransform)transform;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		ShellVisualBoundaryConfig shellVisualBoundaryConfig = default(ShellVisualBoundaryConfig);
		config = shellVisualBoundaryConfig;
		ShellDefinition shellDefinition = default(ShellDefinition);
		impactShell = shellDefinition;
		startLocalPos = startPos;
		targetLocalPos = targetPos;
		bool flag3 = !(0.0001f < travelDuration);
		float num = 0.0001f;
		if (!flag3)
		{
			num = travelDuration;
		}
		travelTime = num;
		double timeAsDouble = Time.timeAsDouble;
		startedAt = timeAsDouble;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
		endsAt = travelTime;
		this.rectTransform.localPosition = (Vector3)(&shellVisualBoundaryConfig);
		previousPos = startLocalPos;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ShellVisual)+3C]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
		Vector2 vector = default(Vector2);
		totalPathDistance = (float)vector;
		Vector2 value = default(Vector2);
		Vector2 vector2 = Vector2.Normalize(ref value);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
		Vector2 vector3 = default(Vector2);
		this.rectTransform.localEulerAngles = (Vector3)(&vector3);
		if (!IsInsideBoard(vector))
		{
			MapBorderSide borderSide = DetermineBorderSide(vector);
			HandleBoundaryExit(vector, borderSide);
		}
	}

	private unsafe void Update()
	{
		//IL_00d4: Expected F4, but got I4
		//IL_0292: Invalid comparison between I4 and F4
		//IL_009d: Invalid comparison between I4 and F4
		//IL_00af: Expected F4, but got I4
		//IL_011a: Expected F4, but got I4
		//IL_02d4: Expected O, but got I
		//IL_015e: Expected O, but got Ref
		//IL_0168: Expected O, but got F4
		//IL_0234: Expected O, but got Ref
		bool flag = rectTransform == null;
		if (flag || hasExitedMap != flag)
		{
			return;
		}
		double timeAsDouble = Time.timeAsDouble;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,qword ptr [rdi+50h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rdi+50h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm6,xmm0\"");
		float num;
		if (0 <= 0)
		{
			bool flag2 = !(0f > 1f);
			num = 0f;
			if (!flag2)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		float num2 = ((0f > num) ? 0f : ((num > 1f) ? 1f : num));
		object obj = targetLocalPos - startLocalPos;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ShellVisual)+44]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ShellVisual)+3C]");
		object obj2 = num3 - 0;
		float num4 = (float)obj * num2;
		float num5 = (float)obj2 * num2;
		float num6 = num4 + (float)startLocalPos;
		float num7 = num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ShellVisual)+3C]");
		float num8 = num7 + 0f;
		Vector2 vector = default(Vector2);
		bool flag3 = IsInsideBoard(vector);
		bool flag4 = IsInsideBoard(vector);
		if (flag3 && !flag4)
		{
			MapBorderSide borderSide = DetermineBorderSide(vector);
			HandleBoundaryExit(vector, borderSide);
			return;
		}
		Vector2 vector2 = default(Vector2);
		rectTransform.localPosition = (Vector3)(&vector2);
		previousPos = (Vector2)num6;
		if (!(num < 1f) && !hasExitedMap)
		{
			ShellDefinition shellDefinition = impactShell;
			if (shellDefinition.ImpactEffectPrefab != null)
			{
				ShellDefinition shellDefinition2 = impactShell;
				Transform parent = rectTransform.parent;
				ImpactLocation impactLocation = UnityEngine.Object.Instantiate(shellDefinition2.ImpactEffectPrefab, parent);
				Transform transform = impactLocation.transform;
				transform.localPosition = (Vector3)(&vector2);
				impactLocation.Init(impactShell);
			}
			else
			{
				Debug.LogError("[ShellVisual] Cannot spawn impact effect because 'impactEffectPrefab' is NULL on the shell definition", this);
			}
			GameObject obj3 = base.gameObject;
			UnityEngine.Object.Destroy(obj3);
		}
	}

	private bool IsInsideBoard(Vector2 localPos)
	{
		//IL_00b2: Expected I4, but got O
		//IL_00c4: Invalid comparison between O and F4
		//IL_00f4: Invalid comparison between F4 and O
		if (boardRect != null)
		{
			if ((object)boardRect != null)
			{
				Rect rect = boardRect.rect;
				if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref localPos) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)rect.m_XMin))
				{
					object obj = default(object);
					float num = (float)obj + rect.m_XMin;
					object obj2 = default(object);
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) >= System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref localPos) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
					{
						object obj3 = obj + obj;
						bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
						return !flag;
					}
				}
				return false;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return true;
	}

	private Vector2 ClampToBorder(Vector2 pos)
	{
		//IL_0077: Invalid comparison between F4 and O
		//IL_0093: Invalid comparison between O and F4
		if (boardRect != null)
		{
			if ((object)boardRect != null)
			{
				Rect rect = boardRect.rect;
				Vector2 vector = default(Vector2);
				float num = (float)vector + rect.m_XMin;
				float xMin = rect.m_XMin;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)xMin) > System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref pos) || System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref pos) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num))
				{
				}
				object obj = vector + vector;
				object obj2 = default(object);
				if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
				{
					return vector;
				}
				return vector;
			}
			return (Vector2)new NullReferenceException();
		}
		return pos;
	}

	private MapBorderSide DetermineBorderSide(Vector2 outsidePos)
	{
		//IL_01e6: Expected I4, but got O
		//IL_00f1: Invalid comparison between F4 and I4
		//IL_0231: Invalid comparison between O and F4
		//IL_0139: Invalid comparison between F4 and I4
		//IL_016f: Invalid comparison between F4 and I4
		MapBorderSide result;
		if (boardRect != null)
		{
			if ((object)boardRect == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (MapBorderSide)ex;
			}
			Rect rect = boardRect.rect;
			float num = rect.m_XMin - (float)outsidePos;
			object obj = default(object);
			float num2 = (float)obj + rect.m_XMin;
			object obj2 = default(object);
			float num3 = (float)obj - (float)obj2;
			object obj3 = obj + obj;
			float num4 = (float)outsidePos - num2;
			object obj4 = obj2 - obj3;
			bool flag = !(num > -1f);
			float num5 = -1f;
			result = MapBorderSide.Top;
			if (!flag)
			{
				bool flag2 = !(num > 0f);
				num5 = -1f;
				result = MapBorderSide.Top;
				if (!flag2)
				{
					num5 = num;
					result = MapBorderSide.Left;
				}
			}
			if (num4 > num5 && num4 > 0f)
			{
				num5 = num4;
				result = MapBorderSide.Right;
			}
			if (num3 > num5 && num3 > 0f)
			{
				num5 = num3;
				result = MapBorderSide.Bottom;
			}
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5))
			{
				if ((nint)obj4 > 0)
				{
					result = MapBorderSide.Top;
				}
				return result;
			}
		}
		else
		{
			result = MapBorderSide.Top;
		}
		return result;
	}

	private unsafe void HandleBoundaryExit(Vector2 lastInsidePos, MapBorderSide borderSide)
	{
		//IL_0154: Expected O, but got Ref
		//IL_017d: Invalid comparison between I4 and F4
		//IL_018f: Expected F4, but got I4
		//IL_00c7: Invalid comparison between F4 and O
		//IL_00e3: Invalid comparison between O and F4
		if (hasExitedMap)
		{
			return;
		}
		hasExitedMap = true;
		Vector2 vector = default(Vector2);
		Vector2 localPos;
		if (config != null)
		{
			ShellVisualBoundaryConfig shellVisualBoundaryConfig = config;
			if (shellVisualBoundaryConfig.clampToBorder)
			{
				if (!(boardRect != null))
				{
					goto IL_0246;
				}
				Rect rect = boardRect.rect;
				float num = (float)vector + rect.m_XMin;
				float xMin = rect.m_XMin;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)xMin) > System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref lastInsidePos) || System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref lastInsidePos) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num))
				{
				}
				object obj = vector + vector;
				object obj2 = default(object);
				if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
				{
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
					{
						goto IL_0246;
					}
					localPos = vector;
				}
				else
				{
					localPos = vector;
				}
				goto IL_0145;
			}
		}
		localPos = lastInsidePos;
		goto IL_0145;
		IL_0145:
		object obj3 = default(object);
		rectTransform.localPosition = (Vector3)(&obj3);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
		float num2 = totalPathDistance - (float)vector;
		bool flag = !(0f < num2);
		float remainingDistance = 0f;
		if (!flag)
		{
			remainingDistance = num2;
		}
		MapBorderSide borderSide2 = default(MapBorderSide);
		SpawnOutOfBoundsEffectAt(localPos, rectTransform.localEulerAngles.z, remainingDistance, borderSide2);
		GameObject obj4 = base.gameObject;
		UnityEngine.Object.Destroy(obj4);
		return;
		IL_0246:
		localPos = vector;
		goto IL_0145;
	}

	private unsafe void SpawnOutOfBoundsEffectAt(Vector2 localPos, float exitAngleDeg, float remainingDistance, MapBorderSide borderSide)
	{
		//IL_00ef: Expected O, but got Ref
		ShellVisualBoundaryConfig shellVisualBoundaryConfig = config;
		UnityEngine.Object obj = (((object)config == null) ? null : shellVisualBoundaryConfig.outOfBoundsEffectPrefab);
		UnityEngine.Object obj2;
		if (obj != null)
		{
			ShellVisualBoundaryConfig shellVisualBoundaryConfig2 = config;
			obj2 = shellVisualBoundaryConfig2.outOfBoundsEffectPrefab;
		}
		else
		{
			ShellDefinition shellDefinition = impactShell;
			obj2 = shellDefinition.ImpactEffectPrefab;
		}
		if (obj2 != null)
		{
			Transform parent = rectTransform.parent;
			ImpactLocation impactLocation = UnityEngine.Object.Instantiate((ImpactLocation)obj2, parent);
			Transform transform = impactLocation.transform;
			object obj3 = default(object);
			transform.localPosition = (Vector3)(&obj3);
			impactLocation.Init(impactShell);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj4 = default(UnityEngine.Object);
			if (obj4 != null)
			{
				MapBorderSide borderSide2 = default(MapBorderSide);
				((OutOfBoundsEffectReceiver)obj4).Initialize(exitAngleDeg, remainingDistance, borderSide2);
			}
		}
		else
		{
			Debug.LogWarning("[ShellVisual] No effect prefab available for OOB event.", this);
		}
	}

	private unsafe void SpawnImpactEffectAt(Vector2 localPos)
	{
		//IL_0088: Expected O, but got Ref
		ShellDefinition shellDefinition = impactShell;
		if (shellDefinition.ImpactEffectPrefab != null)
		{
			ShellDefinition shellDefinition2 = impactShell;
			Transform parent = rectTransform.parent;
			ImpactLocation impactLocation = UnityEngine.Object.Instantiate(shellDefinition2.ImpactEffectPrefab, parent);
			Transform transform = impactLocation.transform;
			object obj = default(object);
			transform.localPosition = (Vector3)(&obj);
			impactLocation.Init(impactShell);
		}
		else
		{
			Debug.LogError("[ShellVisual] Cannot spawn impact effect because 'impactEffectPrefab' is NULL on the shell definition", this);
		}
	}
}
