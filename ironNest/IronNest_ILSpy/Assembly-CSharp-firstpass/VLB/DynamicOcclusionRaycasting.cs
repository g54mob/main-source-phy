using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class DynamicOcclusionRaycasting : DynamicOcclusionAbstractBase
{
	public struct HitResult
	{
		public Vector3 point;

		public Vector3 normal;

		public float distance;

		private Collider2D collider2D;

		private Collider collider3D;

		public bool hasCollider
		{
			get
			{
				if ((bool)collider2D)
				{
					return true;
				}
				return collider3D;
			}
		}

		public string name
		{
			get
			{
				UnityEngine.Object obj;
				if (!collider3D)
				{
					if (!collider2D)
					{
						return "null collider";
					}
					obj = collider2D;
				}
				else
				{
					obj = collider3D;
				}
				if ((object)obj != null)
				{
					return obj.name;
				}
				return (string)(object)new NullReferenceException();
			}
		}

		public unsafe Bounds bounds
		{
			get
			{
				//IL_00d0: Expected native int or pointer, but got O
				//IL_0052: Expected O, but got I4
				//IL_004d: Expected native int or pointer, but got O
				Bounds bounds = default(Bounds);
				Bounds bounds2;
				if (!collider3D)
				{
					if (!collider2D)
					{
						((Bounds*)(nint)bounds)->m_Center = (Vector3)0;
						_ = 0;
						return bounds;
					}
					if ((object)collider2D != null)
					{
						bounds2 = collider2D.bounds;
						goto IL_00c3;
					}
				}
				else if ((object)collider3D != null)
				{
					bounds2 = collider3D.bounds;
					goto IL_00c3;
				}
				return (Bounds)new NullReferenceException();
				IL_00c3:
				((Bounds*)(nint)bounds)->m_Center = bounds2.m_Center;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rax_v6 (UnityEngine.Bounds)+10]");
				_ = 0;
				return bounds;
			}
		}

		public HitResult(ref RaycastHit hit3D)
		{
			//IL_001c: Expected O, but got F4
			//IL_0047: Expected O, but got F4
			Vector3 vector = hit3D.point;
			point = (Vector3)vector.x;
			_ = vector.z;
			Vector3 vector2 = hit3D.normal;
			normal = (Vector3)vector2.x;
			_ = vector2.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
			float num = default(float);
			distance = num;
			Collider collider = hit3D.collider;
			collider3D = collider;
			collider2D = null;
		}

		public HitResult(ref RaycastHit2D hit2D)
		{
			Vector2 vector = hit2D.point;
			Vector3 vector2 = default(Vector3);
			point = vector2;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181E83320");
			normal = vector2;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D25090");
			float num = default(float);
			distance = num;
			Collider2D collider = hit2D.collider;
			collider2D = collider;
			collider3D = null;
		}

		public void SetNull()
		{
			collider2D = null;
			collider3D = null;
		}
	}

	private enum Direction
	{
		Up = 0,
		Down = 1,
		Left = 2,
		Right = 3,
		Max2D = Down,
		Max3D = Right
	}

	public new const string ClassName = "DynamicOcclusionRaycasting";

	public Dimensions dimensions;

	public LayerMask layerMask;

	public bool considerTriggers;

	public float minOccluderArea;

	public float minSurfaceRatio;

	public float maxSurfaceDot;

	public PlaneAlignment planeAlignment;

	public float planeOffset;

	public float fadeDistanceToSurface;

	private HitResult m_CurrentHit;

	private float m_RangeMultiplier;

	private Plane _003CplaneEquationWS_003Ek__BackingField;

	private uint m_PrevNonSubHitDirectionId;

	public float fadeDistanceToPlane
	{
		get
		{
			return fadeDistanceToSurface;
		}
		set
		{
			fadeDistanceToSurface = value;
		}
	}

	public unsafe Plane planeEquationWS
	{
		get
		{
			//IL_000a: Expected native int or pointer, but got O
			Plane plane = default(Plane);
			((Plane*)(nint)plane)->m_Normal = (Vector3)_003CplaneEquationWS_003Ek__BackingField;
			return plane;
		}
		private set
		{
			_003CplaneEquationWS_003Ek__BackingField = (Plane)value.m_Normal;
		}
	}

	private QueryTriggerInteraction queryTriggerInteraction
	{
		get
		{
			bool flag = !considerTriggers;
			bool flag2 = !flag;
			return (QueryTriggerInteraction)((flag2 ? 1 : 0) + 1);
		}
	}

	private float raycastMaxDistance
	{
		get
		{
			if ((object)m_Master != null)
			{
				float raycastDistance = m_Master.raycastDistance;
				if ((object)m_Master != null)
				{
					Vector3 lossyScale = m_Master.GetLossyScale();
					float num = m_RangeMultiplier * raycastDistance;
					return num * lossyScale.z;
				}
			}
			throw new NullReferenceException();
		}
	}

	public unsafe bool IsColliderHiddenByDynamicOccluder(Collider collider)
	{
		//IL_00fb: Invalid comparison between O and F4
		//IL_0120: Expected I4, but got O
		//IL_0090: Expected O, but got Ref
		object obj = (object)_003CplaneEquationWS_003Ek__BackingField * (object)_003CplaneEquationWS_003Ek__BackingField;
		object obj3 = default(object);
		object obj2 = obj3 * obj3;
		object obj4 = obj3 * obj3;
		object obj5 = obj2 + obj;
		object obj6 = obj5 + obj4;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.5f))
		{
			Plane[] array = new Plane[1];
			if (array != null)
			{
				_ = _003CplaneEquationWS_003Ek__BackingField;
				if ((object)collider != null)
				{
					Bounds bounds = collider.bounds;
					Span<Plane> span = new Span<Plane>(array);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18090CA50");
					object obj7 = default(object);
					Bounds bounds2 = default(Bounds);
					bool flag = GeometryUtility.Internal_TestPlanesAABB((ReadOnlySpan<Plane>)(&obj7), ref bounds2);
					return (byte)((flag ? 1u : 0u) ^ 1u) != 0;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	protected override string GetShaderKeyword()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39D21]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		return "VLB_OCCLUSION_CLIPPING_PLANE";
	}

	protected override MaterialManager.SD.DynamicOcclusion GetDynamicOcclusionMode()
	{
		return MaterialManager.SD.DynamicOcclusion.ClippingPlane;
	}

	protected override void OnValidateProperties()
	{
		//IL_00a0: Invalid comparison between F4 and I4
		//IL_00e1: Invalid comparison between F4 and I4
		//IL_006e: Expected F4, but got I4
		//IL_007c: Expected F4, but got I4
		int num = waitXFrames;
		if (waitXFrames >= 1)
		{
			if (num > 60)
			{
				num = 60;
			}
		}
		else
		{
			num = 1;
		}
		waitXFrames = num;
		float num2 = minOccluderArea;
		if (minOccluderArea < 0f)
		{
			num2 = 0f;
		}
		minOccluderArea = num2;
		float num3 = fadeDistanceToSurface;
		if (fadeDistanceToSurface < 0f)
		{
			num3 = 0f;
		}
		fadeDistanceToSurface = num3;
	}

	protected override void OnEnablePostValidate()
	{
		_ = 0;
		_ = 0;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		SetHitNull();
	}

	private void Start()
	{
		//IL_006d: Invalid comparison between F4 and I
		//IL_009d: Expected F4, but got I
		if (!Application.isPlaying)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if ((bool)obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ stack_18_v2 (UnityEngine.Object)+24]");
			bool flag = !(1f < 0f);
			float rangeMultiplier = 1f;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ stack_18_v2 (UnityEngine.Object)+24]");
				rangeMultiplier = 0f;
			}
			m_RangeMultiplier = rangeMultiplier;
		}
	}

	private unsafe Vector3 GetRandomVectorAround(Vector3 direction, float angleDiff)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected F4, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected F4, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected F4, but got Unknown
		//IL_00a7: Expected O, but got Ref
		//IL_00a7: Expected O, but got Ref
		//IL_00b8: Expected native int or pointer, but got O
		//IL_00ca: Expected native int or pointer, but got O
		float num = angleDiff * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		float minInclusive = num ^ 0;
		float num2 = UnityEngine.Random.Range(minInclusive, num);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		float minInclusive2 = num ^ 0;
		float num3 = UnityEngine.Random.Range(minInclusive2, num);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		float minInclusive3 = num ^ 0;
		float num4 = UnityEngine.Random.Range(minInclusive3, num);
		Vector3 euler = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		object obj = default(object);
		object obj2 = default(object);
		Vector3 vector = (Quaternion)(&obj) * (Vector3)(&obj2);
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = vector.x;
		((Vector3*)(nint)vector2)->z = vector.z;
		return vector2;
	}

	private unsafe HitResult GetBestHit(Vector3 rayPos, Vector3 rayDir)
	{
		//IL_0048: Expected O, but got Ref
		//IL_0048: Expected O, but got Ref
		//IL_005e: Expected native int or pointer, but got O
		//IL_007d: Expected native int or pointer, but got O
		//IL_0031: Expected O, but got Ref
		//IL_0031: Expected O, but got Ref
		float num = default(float);
		float num2 = default(float);
		HitResult hitResult = ((dimensions != Dimensions.Dim2D) ? GetBestHit3D((Vector3)(&num), (Vector3)(&num2)) : GetBestHit2D((Vector3)(&num2), (Vector3)(&num)));
		HitResult hitResult2 = default(HitResult);
		((HitResult*)(nint)hitResult2)->point = hitResult.point;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v2 (VLB.DynamicOcclusionRaycasting+HitResult)+10]");
		_ = 0;
		System.Runtime.CompilerServices.Unsafe.Write(&((HitResult*)(nint)hitResult2)->collider2D, hitResult.collider2D);
		return hitResult2;
	}

	private unsafe HitResult GetBestHit3D(Vector3 rayPos, Vector3 rayDir)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_00a2: Expected O, but got I8
		//IL_00b3: Expected O, but got I4
		//IL_00c5: Expected O, but got I4
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Expected O, but got Unknown
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Expected O, but got Unknown
		//IL_03cf: Expected O, but got I4
		//IL_03ca: Expected native int or pointer, but got O
		//IL_03de: Expected O, but got I4
		//IL_057f: Expected native int or pointer, but got O
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_04b3: Expected O, but got I
		//IL_04ae: Expected native int or pointer, but got O
		//IL_04c3: Expected O, but got I
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Expected O, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_022a: Expected O, but got I
		//IL_0247: Expected O, but got I
		//IL_0264: Expected O, but got I
		//IL_054c: Expected O, but got I
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Expected O, but got Unknown
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Expected O, but got Unknown
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Expected O, but got Unknown
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 87;
		float num = raycastMaxDistance;
		object obj3 = this + 116;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
		Vector3 direction = (Vector3)(obj - 65);
		Vector3 origin = (Vector3)(obj - 49);
		_ = rayDir.x;
		float x = rayPos.x;
		_ = rayDir.z;
		_ = rayPos.x;
		_ = rayPos.z;
		int num2 = default(int);
		QueryTriggerInteraction queryTriggerInteraction = default(QueryTriggerInteraction);
		RaycastHit[] array = Physics.RaycastAll(origin, direction, num, num2, queryTriggerInteraction);
		object obj4 = 4294967295L;
		float num3 = num;
		object obj5 = 0;
		float num4 = 3.4028235E+38f;
		object obj6 = 0;
		object obj15 = default(object);
		HitResult hitResult = default(HitResult);
		while (true)
		{
			if ((nint)obj6 < array.Length)
			{
				if ((nint)obj5 >= array.Length)
				{
					break;
				}
				object obj7 = obj5 * 44;
				object obj8 = obj7 + 32;
				RaycastHit raycastHit = (RaycastHit)(obj8 + (object)array);
				Collider collider = ((RaycastHit*)raycastHit)->collider;
				GameObject gameObject = collider.gameObject;
				GameObject gameObject2 = m_Master.gameObject;
				if (gameObject != gameObject2)
				{
					if ((nint)obj5 >= array.Length)
					{
						break;
					}
					object obj9 = obj5 * 44;
					object obj10 = obj9 + 32;
					RaycastHit raycastHit2 = (RaycastHit)(obj10 + (object)array);
					Collider collider2 = ((RaycastHit*)raycastHit2)->collider;
					Bounds bounds = collider2.bounds;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rax_v35 (UnityEngine.Bounds)+10]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rax_v35 (UnityEngine.Bounds)+10]");
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rax_v35 (UnityEngine.Bounds)+10]");
					object obj11 = num5 + 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rbp_v1-1D]");
					nint num6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rbp_v1-1D]");
					object obj12 = num6 + 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rax_v35 (UnityEngine.Bounds)+10]");
					nint num7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rax_v35 (UnityEngine.Bounds)+10]");
					object obj13 = num7 + 0;
					float num8 = (float)obj13 * (float)obj12;
					object obj14 = obj15 + obj15;
					x = (float)obj14 * (float)obj11;
					if (!(x > num8))
					{
						x = num8;
					}
					object obj16 = obj15 + obj15;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rbp_v1-1D]");
					nint num9 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rbp_v1-1D]");
					object obj17 = num9 + 0;
					num3 = (float)obj16 * (float)obj17;
					if (!(x > num3))
					{
						x = num3;
					}
					if (!(x < minOccluderArea))
					{
						if ((nint)obj5 >= array.Length)
						{
							break;
						}
						object obj18 = obj5 * 44;
						object obj19 = obj18 + 32;
						object obj20 = obj19 + (object)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
						if (num4 > x)
						{
							if ((nint)obj5 >= array.Length)
							{
								break;
							}
							object obj21 = obj5 * 44;
							object obj22 = obj21 + 32;
							object obj23 = obj22 + (object)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
							obj4 = obj5;
							num4 = x;
						}
					}
				}
				obj5++;
				obj6 = obj5;
				continue;
			}
			Vector3 collider2D;
			if ((nint)obj4 == -1)
			{
				((HitResult*)(nint)hitResult)->point = (Vector3)0;
				_ = 0;
				collider2D = (Vector3)0;
			}
			else
			{
				_ = 0;
				_ = 0;
				_ = 0;
				object obj24 = obj4 * 44;
				object obj25 = obj24 + 32;
				RaycastHit raycastHit3 = (RaycastHit)(obj25 + (object)array);
				Vector3 point = ((RaycastHit*)raycastHit3)->point;
				_ = point.x;
				_ = point.z;
				Vector3 normal = ((RaycastHit*)raycastHit3)->normal;
				_ = normal.x;
				_ = normal.z;
				float distance = ((RaycastHit*)raycastHit3)->distance;
				Collider collider3 = ((RaycastHit*)raycastHit3)->collider;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rbp_v1-11]");
				((HitResult*)(nint)hitResult)->point = (Vector3)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rbp_v1+F]");
				collider2D = (Vector3)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rbp_v1-1]");
				_ = 0;
			}
			System.Runtime.CompilerServices.Unsafe.Write(&((HitResult*)(nint)hitResult)->collider2D, (Collider2D)collider2D);
			return hitResult;
		}
		return (HitResult)new IndexOutOfRangeException();
	}

	private unsafe HitResult GetBestHit2D(Vector3 rayPos, Vector3 rayDir)
	{
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Expected O, but got Unknown
		//IL_003f: Expected O, but got I8
		//IL_0069: Expected O, but got I4
		//IL_0072: Expected O, but got I4
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Expected O, but got Unknown
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Expected O, but got Unknown
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Expected O, but got Unknown
		//IL_045d: Expected O, but got I4
		//IL_0458: Expected native int or pointer, but got O
		//IL_046c: Expected O, but got I4
		//IL_04fb: Expected native int or pointer, but got O
		//IL_05ac: Expected native int or pointer, but got O
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Expected O, but got Unknown
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_0294: Expected O, but got I
		//IL_02be: Expected O, but got I
		//IL_0590: Invalid comparison between O and F4
		//IL_05e1: Invalid comparison between O and F4
		//IL_0316: Expected O, but got F4
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Expected O, but got Unknown
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Expected O, but got Unknown
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Expected O, but got Unknown
		//IL_0392: Invalid comparison between F4 and O
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Expected O, but got Unknown
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Expected O, but got Unknown
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Expected O, but got Unknown
		//IL_042a: Expected F4, but got O
		float num = raycastMaxDistance;
		object obj = this + 116;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
		Vector2 vector = default(Vector2);
		int num2 = default(int);
		RaycastHit2D[] array = Physics2D.RaycastAll(vector, vector, num, num2);
		object obj2 = 4294967295L;
		float num3 = 3.4028235E+38f;
		float num4 = num;
		Vector2 vector2 = vector;
		Vector2 vector3 = vector;
		object obj3 = 0;
		object obj4 = 0;
		object obj18 = default(object);
		HitResult hitResult = default(HitResult);
		Vector3 vector5 = default(Vector3);
		while (true)
		{
			if ((nint)obj4 < array.Length)
			{
				if (!considerTriggers)
				{
					if ((nint)obj3 >= array.Length)
					{
						break;
					}
					object obj5 = obj3 + 1;
					object obj6 = obj5 * 8;
					object obj7 = obj3 + obj6;
					object obj8 = obj7 * 4;
					RaycastHit2D raycastHit2D = (RaycastHit2D)((object)array + obj8);
					Collider2D collider = ((RaycastHit2D*)raycastHit2D)->collider;
					if (collider.isTrigger)
					{
						goto IL_0544;
					}
				}
				if ((nint)obj3 >= array.Length)
				{
					break;
				}
				object obj9 = obj3 + 1;
				object obj10 = obj9 * 8;
				object obj11 = obj3 + obj10;
				object obj12 = obj11 * 4;
				RaycastHit2D raycastHit2D2 = (RaycastHit2D)((object)array + obj12);
				Collider2D collider2 = ((RaycastHit2D*)raycastHit2D2)->collider;
				GameObject gameObject = collider2.gameObject;
				GameObject gameObject2 = m_Master.gameObject;
				if (gameObject != gameObject2)
				{
					if ((nint)obj3 >= array.Length)
					{
						break;
					}
					object obj13 = obj3 + 1;
					object obj14 = obj13 * 8;
					object obj15 = obj3 + obj14;
					object obj16 = obj15 * 4;
					RaycastHit2D raycastHit2D3 = (RaycastHit2D)((object)array + obj16);
					Collider2D collider3 = ((RaycastHit2D*)raycastHit2D3)->collider;
					Bounds bounds = collider3.bounds;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v28 (UnityEngine.Bounds)+10]");
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v28 (UnityEngine.Bounds)+10]");
					vector2 = (Vector2)(num5 + 0);
					object obj17 = obj18 + obj18;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v28 (UnityEngine.Bounds)+10]");
					nint num6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v28 (UnityEngine.Bounds)+10]");
					object obj19 = num6 + 0;
					Vector2 vector4 = (Vector2)(obj19 * obj17);
					object obj20 = vector + vector;
					vector3 = (Vector2)(obj20 * (object)vector2);
					if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector3) <= System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector4))
					{
						vector3 = vector4;
					}
					object obj21 = obj18 + obj18;
					object obj22 = vector + vector;
					num4 = (float)obj21 * (float)obj22;
					if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector3) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4))
					{
						vector3 = (Vector2)num4;
					}
					if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector3) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)minOccluderArea))
					{
						if ((nint)obj3 >= array.Length)
						{
							break;
						}
						object obj23 = obj3 + 1;
						object obj24 = obj23 * 8;
						object obj25 = obj3 + obj24;
						object obj26 = obj25 * 4;
						object obj27 = (object)array + obj26;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D25090");
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3) > System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector3))
						{
							if ((nint)obj3 >= array.Length)
							{
								break;
							}
							object obj28 = obj3 + 1;
							object obj29 = obj28 * 8;
							object obj30 = obj3 + obj29;
							object obj31 = obj30 * 4;
							object obj32 = (object)array + obj31;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D25090");
							obj2 = obj3;
							num3 = (float)vector3;
						}
					}
				}
				goto IL_0544;
			}
			Vector3 collider2D;
			if ((nint)obj2 == -1)
			{
				((HitResult*)(nint)hitResult)->point = (Vector3)0;
				_ = 0;
				collider2D = (Vector3)0;
			}
			else
			{
				object obj33 = obj2 + 1;
				object obj34 = obj33 * 8;
				object obj35 = obj2 + obj34;
				object obj36 = obj35 * 4;
				RaycastHit2D raycastHit2D4 = (RaycastHit2D)((object)array + obj36);
				Vector2 point = ((RaycastHit2D*)raycastHit2D4)->point;
				Vector2 normal = ((RaycastHit2D*)raycastHit2D4)->normal;
				float distance = ((RaycastHit2D*)raycastHit2D4)->distance;
				Collider2D collider4 = ((RaycastHit2D*)raycastHit2D4)->collider;
				((HitResult*)(nint)hitResult)->point = point;
				collider2D = vector5;
			}
			System.Runtime.CompilerServices.Unsafe.Write(&((HitResult*)(nint)hitResult)->collider2D, (Collider2D)collider2D);
			return hitResult;
			IL_0544:
			obj3++;
			obj4 = obj3;
		}
		return (HitResult)new IndexOutOfRangeException();
	}

	private uint GetDirectionCount()
	{
		bool flag = dimensions != Dimensions.Dim2D;
		uint result = 4u;
		if (!flag)
		{
			result = 2u;
		}
		return result;
	}

	private unsafe Vector3 GetDirection(uint dirInt)
	{
		//IL_001a: Expected O, but got I4
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected I4, but got Unknown
		//IL_0031: Expected O, but got I4
		//IL_0044: Expected O, but got I4
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_01f1: Expected O, but got Ref
		//IL_0155: Expected native int or pointer, but got O
		//IL_0167: Expected native int or pointer, but got O
		//IL_0207: Expected O, but got Ref
		//IL_0226: Expected native int or pointer, but got O
		//IL_0233: Expected native int or pointer, but got O
		//IL_01b7: Expected I, but got O
		//IL_01c7: Expected O, but got I
		bool flag = dimensions == Dimensions.Dim2D;
		object obj = 2;
		if (!flag)
		{
			obj = 4;
		}
		int num = (int)(dirInt % obj);
		bool flag2 = num == 0;
		Vector3 vector;
		VolumetricLightBeamSD master;
		VolumetricLightBeamSD master2;
		if (!flag2)
		{
			object obj2 = num - 1;
			if (!flag2)
			{
				object obj3 = obj2 - 1;
				if (!flag2)
				{
					if ((nint)obj3 != 1)
					{
						nint num2 = (nint)typeof(Vector3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v301 @ rax_v34 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						vector = (Vector3)0;
						goto IL_0148;
					}
					master = m_Master;
					if ((object)m_Master != null)
					{
						goto IL_01e4;
					}
				}
				else
				{
					master2 = m_Master;
					if ((object)m_Master != null)
					{
						goto IL_01fa;
					}
				}
			}
			else
			{
				master2 = m_Master;
				if ((object)m_Master != null)
				{
					goto IL_01fa;
				}
			}
		}
		else
		{
			master = m_Master;
			if ((object)m_Master != null)
			{
				goto IL_01e4;
			}
		}
		return (Vector3)new NullReferenceException();
		IL_0148:
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = vector.x;
		((Vector3*)(nint)vector2)->z = vector.z;
		return vector2;
		IL_01e4:
		Vector3 vector3 = default(Vector3);
		vector = master.ComputeRaycastGlobalVector((Vector3)(&vector3));
		goto IL_0148;
		IL_01fa:
		float z = master2.ComputeRaycastGlobalVector((Vector3)(&vector3)).z ^ -0f;
		float x = default(float);
		((Vector3*)(nint)vector2)->x = x;
		((Vector3*)(nint)vector2)->z = z;
		return vector2;
	}

	private bool IsHitValid(ref HitResult hit, Vector3 forwardVec)
	{
		//IL_0033: Expected O, but got F4
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_0076: Expected O, but got F4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_00af: Invalid comparison between O and F4
		bool hasCollider = hit.hasCollider;
		if (!hasCollider)
		{
			return hasCollider;
		}
		object obj = forwardVec.x ^ -0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [hit @ rdx (HitResult&)+C]");
		object obj2 = obj * 0;
		object obj4 = default(object);
		object obj3 = obj4 ^ -0f;
		object obj5 = obj3 * obj4;
		object obj6 = forwardVec.z ^ -0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [hit @ rdx (HitResult&)+14]");
		object obj7 = obj6 * 0;
		object obj8 = obj5 + obj2;
		object obj9 = obj8 + obj7;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)maxSurfaceDot);
		return !flag;
	}

	protected unsafe override bool OnProcessOcclusion(ProcessOcclusionSource source)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_074a: Expected I4, but got O
		//IL_0759: Expected O, but got Ref
		//IL_0105: Expected O, but got Ref
		//IL_0105: Expected O, but got Ref
		//IL_00ee: Expected O, but got Ref
		//IL_00ee: Expected O, but got Ref
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_018e: Expected O, but got F4
		//IL_01a1: Expected O, but got F4
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01e7: Invalid comparison between O and F4
		//IL_06f2: Expected O, but got I
		//IL_0733: Expected O, but got I
		//IL_027d: Expected O, but got F4
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Expected O, but got Unknown
		//IL_02a3: Expected O, but got F4
		//IL_0905: Expected O, but got I4
		//IL_02cc: Expected O, but got I4
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Expected O, but got Unknown
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Expected O, but got Unknown
		//IL_049e: Expected O, but got I
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f5: Expected O, but got Unknown
		//IL_0895: Invalid comparison between O and F4
		//IL_04f1: Expected I, but got O
		//IL_0511: Expected F4, but got I
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Expected O, but got Unknown
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Expected O, but got Unknown
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Expected O, but got Unknown
		//IL_05c2: Expected O, but got I
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Expected O, but got Unknown
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Expected O, but got Unknown
		//IL_0619: Expected O, but got I
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Expected O, but got Unknown
		//IL_065f: Invalid comparison between O and F4
		object obj2 = default(object);
		object obj = obj2 - 360;
		_ = 0;
		_ = 0;
		HitResult hitResult2 = default(HitResult);
		uint num;
		if ((object)m_Master != null)
		{
			if (m_Master.hasMeshSkewing)
			{
				Vector3 skewingLocalForwardDirectionNormalized = m_Master.skewingLocalForwardDirectionNormalized;
			}
			object obj3 = default(object);
			Vector3 vector = m_Master.ComputeRaycastGlobalVector((Vector3)(&obj3));
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				Vector3 position = transform.position;
				object obj4 = default(object);
				object obj5 = default(object);
				HitResult hitResult = ((dimensions != Dimensions.Dim2D) ? GetBestHit3D((Vector3)(&obj4), (Vector3)(&obj5)) : GetBestHit2D((Vector3)(&obj5), (Vector3)(&obj4)));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v471 @ rax_v10 (VLB.DynamicOcclusionRaycasting+HitResult)+10]");
				_ = 0;
				_ = hitResult.collider2D;
				bool hasCollider = hitResult2.hasCollider;
				bool flag = !hasCollider;
				hitResult2 = (HitResult)hitResult.point;
				if (!flag)
				{
					object obj7 = default(object);
					object obj6 = obj7 ^ -0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-80]");
					object obj8 = obj6 * 0;
					object obj9 = vector.x ^ -0f;
					object obj10 = vector.z ^ -0f;
					object obj12 = default(object);
					object obj11 = obj9 * obj12;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-7C]");
					object obj13 = obj10 * 0;
					object obj14 = obj8 + obj11;
					object obj15 = obj14 + obj13;
					bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj15) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)maxSurfaceDot);
					hitResult2 = (HitResult)hitResult.point;
					if (!flag2)
					{
						bool flag3 = !(minSurfaceRatio > 0.5f);
						hitResult2 = (HitResult)hitResult.point;
						if (!flag3)
						{
							if ((object)m_Master == null)
							{
								goto IL_073c;
							}
							float raycastDistance = m_Master.raycastDistance;
							object obj16 = vector.x ^ -0f;
							object obj17 = obj7 ^ -0f;
							object obj18 = vector.z ^ -0f;
							num = 0u;
							hitResult2 = (HitResult)hitResult.point;
							object obj24 = default(object);
							while (true)
							{
								bool flag4 = dimensions == Dimensions.Dim2D;
								object obj19 = 2;
								if (!flag4)
								{
									obj19 = 4;
								}
								if ((int)num >= (nint)obj19)
								{
									break;
								}
								uint dirInt = m_PrevNonSubHitDirectionId + num;
								Vector3 direction = GetDirection(dirInt);
								float num2 = minSurfaceRatio + minSurfaceRatio;
								float num3 = num2 - 1f;
								float num4 = direction.z * num3;
								Transform transform2 = base.transform;
								if ((object)transform2 != null)
								{
									float num5 = num4 * transform2.localScale.z;
									Transform transform3 = base.transform;
									if ((object)transform3 != null)
									{
										Vector3 position2 = transform3.position;
										VolumetricLightBeamSD master = m_Master;
										if ((object)m_Master != null)
										{
											float num6 = master.coneRadiusStart * num5;
											float num7 = num6 + position2.z;
											Transform transform4 = base.transform;
											if ((object)transform4 != null)
											{
												object obj20 = obj + 80;
												Vector3 position3 = transform4.position;
												VolumetricLightBeamSD master2 = m_Master;
												if ((object)m_Master != null)
												{
													Vector2 tiltFactor = master2.tiltFactor;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
													object obj21 = tiltFactor & 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v271 @ rdi_v7 (VLB.VolumetricLightBeamSD)+100]");
													nint num8 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
													object obj22 = num8 & 0;
													if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj21) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj22))
													{
														obj21 = obj22;
													}
													float num9 = master2.spotAngle * ((float)Math.PI / 180f);
													float num10 = num9 * 0.5f;
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
													object obj23 = obj21 + master2.fallOffEnd;
													float num11 = num10 * (float)obj23;
													float num12 = vector.x;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1+170]");
													float num13 = num12 * 0f;
													float num14 = num11 * num5;
													float num15 = num14 + position3.z;
													float num16 = vector.z;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1+170]");
													float num17 = num16 * 0f;
													float num18 = num15 + num17;
													float num19 = num18 - num7;
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
													if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj24) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
													{
														float num20 = num19 / (float)obj24;
														float num21 = num20;
													}
													else
													{
														nint num22 = (nint)typeof(Vector3);
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v900 @ rax_v45 (Il2CppClass<UnityEngine.Vector3>)+B8]");
														nint num23 = 0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v901 @ rcx_v36 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
														float num21 = 0f;
													}
													HitResult hitResult3;
													if (dimensions == Dimensions.Dim2D)
													{
														Vector3 rayDir = (Vector3)(obj - 80);
														Vector3 rayPos = (Vector3)(obj - 64);
														hitResult3 = GetBestHit2D(rayPos, rayDir);
													}
													else
													{
														Vector3 rayDir2 = (Vector3)(obj - 48);
														Vector3 rayPos2 = (Vector3)(obj - 32);
														hitResult3 = GetBestHit3D(rayPos2, rayDir2);
													}
													HitResult hitResult4 = (HitResult)(obj - 16);
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v961 @ rax_v41 (VLB.DynamicOcclusionRaycasting+HitResult)+10]");
													obj = 0;
													_ = hitResult3.point;
													_ = hitResult3.collider2D;
													if (((HitResult*)hitResult4)->hasCollider)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v961 @ rax_v41 (VLB.DynamicOcclusionRaycasting+HitResult)+10]");
														nint num24 = 0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1+188]");
														object obj25 = num24 * 0;
														object obj26 = obj24 * obj16;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-60]");
														object obj27 = obj24 * 0;
														object obj28 = obj25 + obj26;
														obj15 = obj28 + obj27;
														if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj15) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)maxSurfaceDot))
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-78]");
															if ((nint)obj24 > 0)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v961 @ rax_v41 (VLB.DynamicOcclusionRaycasting+HitResult)+10]");
																_ = 0;
																_ = hitResult3.collider2D;
																hitResult2 = (HitResult)hitResult3.point;
															}
															num++;
															continue;
														}
													}
													goto IL_06c3;
												}
											}
										}
									}
								}
								goto IL_073c;
							}
						}
						goto IL_06d2;
					}
				}
				goto IL_0784;
			}
		}
		goto IL_073c;
		IL_06c3:
		m_PrevNonSubHitDirectionId = num;
		goto IL_0784;
		IL_0784:
		_ = 0;
		_ = 0;
		goto IL_06d2;
		IL_073c:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_06d2:
		SetHit(ref hitResult2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-70]");
		if ((bool)(UnityEngine.Object)0)
		{
			return true;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-68]");
		return (UnityEngine.Object)0;
	}

	private unsafe void SetHit(ref HitResult hit)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0133: Expected O, but got Ref
		//IL_018f: Invalid comparison between O and F4
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		//IL_03f3: Expected O, but got Ref
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Expected O, but got Unknown
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Expected O, but got Unknown
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Expected O, but got Unknown
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Expected O, but got Unknown
		//IL_01ac: Expected O, but got I4
		//IL_01b5: Expected O, but got I4
		//IL_01be: Expected O, but got I4
		//IL_031f: Expected I, but got O
		//IL_0338: Expected F4, but got O
		//IL_0348: Expected F4, but got I
		//IL_0262: Expected O, but got Ref
		//IL_0288: Expected O, but got Ref
		//IL_02a5: Expected O, but got F4
		//IL_02b8: Expected O, but got F4
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Expected O, but got Unknown
		//IL_02fd: Invalid comparison between O and F4
		//IL_035b: Expected O, but got Ref
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Expected O, but got Unknown
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Expected O, but got Unknown
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Expected O, but got Unknown
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Expected O, but got Unknown
		//IL_00d3: Expected O, but got I4
		//IL_00dc: Expected O, but got I4
		//IL_00e5: Expected O, but got I4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (hit.hasCollider)
		{
			if (planeAlignment != PlaneAlignment.Surface && planeAlignment == PlaneAlignment.Beam)
			{
				if (m_Master.hasMeshSkewing)
				{
					Vector3 skewingLocalForwardDirectionNormalized = m_Master.skewingLocalForwardDirectionNormalized;
					float x = skewingLocalForwardDirectionNormalized.x;
					float z = skewingLocalForwardDirectionNormalized.z;
				}
				else
				{
					nint num = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v322 @ rax_v16 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num2 = 0;
					float x = (float)Vector3.forwardVector;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ rcx_v14 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
					float z = 0f;
				}
				Vector3 localVec = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Vector3 vector = m_Master.ComputeRaycastGlobalVector(localVec);
				object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
				_ = vector.x;
				object obj4 = vector.x ^ -0f;
				object obj5 = vector.z ^ -0f;
				object obj7 = default(object);
				object obj6 = obj7 ^ -0f;
				_ = 0;
				_ = hit;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				object obj8;
				object obj9;
				object obj10;
				if (System.Runtime.CompilerServices.Unsafe.As<HitResult, UIntPtr>(ref hit) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
				{
					obj8 = 0;
					obj9 = 0;
					obj10 = 0;
				}
				else
				{
					obj8 = obj4 / (object)hit;
					obj9 = obj6 / (object)hit;
					obj10 = obj5 / (object)hit;
				}
				Plane clippingPlane = (Plane)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-15]");
				object obj11 = 0 * obj9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
				object obj12 = 0 * obj8;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [hit @ rdx (HitResult&)+8]");
				object obj13 = 0 * obj10;
				object obj14 = obj11 + obj12;
				object obj15 = obj14 + obj13;
				object obj16 = obj15 ^ -0f;
				SetClippingPlane(clippingPlane);
			}
			else
			{
				object obj17 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [hit @ rdx (HitResult&)+C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-5]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [hit @ rdx (HitResult&)+C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [hit @ rdx (HitResult&)+14]");
				_ = 0;
				_ = 0;
				_ = hit;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				object obj18;
				object obj19;
				object obj20;
				if (System.Runtime.CompilerServices.Unsafe.As<HitResult, UIntPtr>(ref hit) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
				{
					obj18 = 0;
					obj19 = 0;
					obj20 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [hit @ rdx (HitResult&)+C]");
					obj18 = 0 / hit;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-5]");
					obj19 = 0 / hit;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [hit @ rdx (HitResult&)+14]");
					obj20 = 0 / hit;
				}
				Plane clippingPlane2 = (Plane)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-15]");
				object obj21 = 0 * obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
				object obj22 = 0 * obj18;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [hit @ rdx (HitResult&)+8]");
				object obj23 = 0 * obj20;
				object obj24 = obj21 + obj22;
				object obj25 = obj24 + obj23;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				object obj26 = obj25 ^ 0;
				SetClippingPlane(clippingPlane2);
			}
			m_CurrentHit = hit;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [hit @ rdx (HitResult&)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [hit @ rdx (HitResult&)+20]");
			_ = 0;
		}
		else
		{
			SetHitNull();
		}
	}

	private void SetHitNull()
	{
		//IL_0015: Expected O, but got I4
		VolumetricLightBeamSD master = m_Master;
		_003CplaneEquationWS_003Ek__BackingField = (Plane)0;
		string shaderKeyword = GetShaderKeyword();
		master.m_INTERNAL_DynamicOcclusionMode_Runtime = false;
		if ((bool)master.m_BeamGeom)
		{
			master.m_BeamGeom.SetDynamicOcclusionCallback(shaderKeyword, null);
		}
		_ = 0;
		_ = 0;
	}

	protected unsafe override void OnModifyMaterialCallback(MaterialModifier.Interface owner)
	{
		//IL_001c: Expected O, but got Ref
		Plane plane = default(Plane);
		owner.SetMaterialProp(ShaderProperties.SD.DynamicOcclusionClippingPlaneWS, (Vector4)(&plane));
		owner.SetMaterialProp(ShaderProperties.SD.DynamicOcclusionClippingPlaneProps, fadeDistanceToSurface);
	}

	private void SetClippingPlane(Plane planeWS)
	{
		//IL_01b9: Invalid comparison between O and F4
		//IL_0211: Expected I, but got O
		_ = planeWS.m_Normal;
		float num = planeOffset;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [planeWS @ rdx (UnityEngine.Plane)+8]");
		float num2 = num * 0f;
		float num3 = planeOffset * (float)planeWS.m_Normal;
		Plane plane = default(Plane);
		float num4 = planeOffset * (float)plane;
		_ = planeWS.m_Normal;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		if (System.Runtime.CompilerServices.Unsafe.As<Plane, UIntPtr>(ref plane) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
		}
		nint num5 = (nint)typeof(Math);
		float num6 = num4 * num4;
		float num7 = num3 * num3;
		float num8 = num2 * num2;
		float num9 = num6 + num7;
		float num10 = num9 + num8;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v138 @ rcx_v5 (Il2CppClass<System.Math>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
		}
		else
		{
			double num11 = Math.Sqrt(num10);
		}
		VolumetricLightBeamSD master = m_Master;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
		_003CplaneEquationWS_003Ek__BackingField = plane;
		string shaderKeyword = GetShaderKeyword();
		bool flag = m_MaterialModifierCallbackCached == null;
		bool iNTERNAL_DynamicOcclusionMode_Runtime = !flag;
		master.m_INTERNAL_DynamicOcclusionMode_Runtime = iNTERNAL_DynamicOcclusionMode_Runtime;
		if ((bool)master.m_BeamGeom)
		{
			master.m_BeamGeom.SetDynamicOcclusionCallback(shaderKeyword, m_MaterialModifierCallbackCached);
		}
	}

	private void SetClippingPlaneOff()
	{
		//IL_0015: Expected O, but got I4
		VolumetricLightBeamSD master = m_Master;
		_003CplaneEquationWS_003Ek__BackingField = (Plane)0;
		string shaderKeyword = GetShaderKeyword();
		master.m_INTERNAL_DynamicOcclusionMode_Runtime = false;
		if ((bool)master.m_BeamGeom)
		{
			master.m_BeamGeom.SetDynamicOcclusionCallback(shaderKeyword, null);
		}
	}

	private void SetPlaneWS(Plane planeWS)
	{
		_003CplaneEquationWS_003Ek__BackingField = (Plane)planeWS.m_Normal;
	}

	public DynamicOcclusionRaycasting()
	{
		//IL_002a: Expected I4, but got I8
		layerMask = Consts.DynOcclusion.LayerMaskDefault;
		minSurfaceRatio = 0.5f;
		maxSurfaceDot = 0.25f;
		planeOffset = 0.1f;
		fadeDistanceToSurface = 0.25f;
		m_RangeMultiplier = 1f;
		updateRate = DynamicOcclusionUpdateRate.EveryXFrames;
		waitXFrames = 3;
		base.m_LastFrameRendered = -2147483648;
		((MonoBehaviour)this)._002Ector();
	}
}
