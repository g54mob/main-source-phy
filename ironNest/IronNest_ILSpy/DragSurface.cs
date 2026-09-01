using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class DragSurface : MonoBehaviour
{
	public enum SurfaceAxis
	{
		Up,
		Forward,
		Right
	}

	public static readonly List<DragSurface> AllSurfaces;

	public SurfaceAxis planeNormalAxis;

	public float additionalSurfaceOffset;

	public Collider raycastTargetCollider;

	public bool clampToBounds = true;

	public float clampInset = 0.01f;

	public float defaultDragLift = 0.02f;

	public float surfaceScaleMultiplier = 1f;

	public bool preferAlignRotationOnEnter = true;

	public int handoffPriority;

	public DraggableItemDeckArea sourceDeckArea;

	public List<DraggableItem> items;

	private BoxCollider _boundsBox;

	public BoxCollider BoundsBox => _boundsBox;

	private void Awake()
	{
		//IL_0097: Invalid comparison between I4 and F4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		BoxCollider boundsBox = default(BoxCollider);
		_boundsBox = boundsBox;
		if (!_boundsBox)
		{
			GameObject gameObject = base.gameObject;
			BoxCollider boundsBox2 = gameObject.AddComponent<BoxCollider>();
			_boundsBox = boundsBox2;
		}
		if (raycastTargetCollider == null)
		{
			raycastTargetCollider = _boundsBox;
		}
		if (!(0f < surfaceScaleMultiplier))
		{
			surfaceScaleMultiplier = 0.0001f;
		}
	}

	private void OnEnable()
	{
		if (!AllSurfaces.Contains(this))
		{
			AllSurfaces.Add(this);
		}
	}

	private void OnDisable()
	{
		bool flag = AllSurfaces.Remove(this);
	}

	private void OnValidate()
	{
		//IL_000b: Invalid comparison between I4 and F4
		if (!(0f < surfaceScaleMultiplier))
		{
			surfaceScaleMultiplier = 0.0001f;
		}
	}

	public unsafe Vector3 GetPlaneNormal()
	{
		//IL_0009: Expected native int or pointer, but got O
		//IL_0017: Expected native int or pointer, but got O
		//IL_0118: Expected native int or pointer, but got O
		//IL_012a: Expected native int or pointer, but got O
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = 0f;
		((Vector3*)(nint)vector)->z = 0f;
		Vector3 vector2;
		if (planeNormalAxis == SurfaceAxis.Forward)
		{
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				vector2 = transform.forward;
				goto IL_010b;
			}
		}
		else if (planeNormalAxis == SurfaceAxis.Right)
		{
			Transform transform2 = base.transform;
			if ((object)transform2 != null)
			{
				vector2 = transform2.right;
				goto IL_010b;
			}
		}
		else
		{
			Transform transform3 = base.transform;
			if ((object)transform3 != null)
			{
				vector2 = transform3.up;
				goto IL_010b;
			}
		}
		return (Vector3)new NullReferenceException();
		IL_010b:
		((Vector3*)(nint)vector)->x = vector2.x;
		((Vector3*)(nint)vector)->z = vector2.z;
		return vector;
	}

	public unsafe Vector3 GetPlaneOriginPoint()
	{
		//IL_00de: Expected native int or pointer, but got O
		//IL_00f0: Expected native int or pointer, but got O
		//IL_008f: Expected O, but got Ref
		Vector3 vector;
		if ((bool)_boundsBox)
		{
			Transform transform = base.transform;
			if ((object)_boundsBox != null)
			{
				Vector3 center = _boundsBox.center;
				if ((object)transform != null)
				{
					object obj = default(object);
					vector = transform.TransformPoint((Vector3)(&obj));
					goto IL_00d1;
				}
			}
		}
		else
		{
			Transform transform2 = base.transform;
			if ((object)transform2 != null)
			{
				vector = transform2.position;
				goto IL_00d1;
			}
		}
		return (Vector3)new NullReferenceException();
		IL_00d1:
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = vector.x;
		((Vector3*)(nint)vector2)->z = vector.z;
		return vector2;
	}

	public unsafe Plane GetSurfacePlane()
	{
		//IL_014c: Expected native int or pointer, but got O
		//IL_0163: Expected O, but got F4
		//IL_015e: Expected native int or pointer, but got O
		//IL_018a: Invalid comparison between O and F4
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01f2: Expected native int or pointer, but got O
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected O, but got Unknown
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Expected O, but got Unknown
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Expected F4, but got Unknown
		//IL_02a0: Expected native int or pointer, but got O
		//IL_01ac: Expected O, but got I4
		//IL_01a7: Expected native int or pointer, but got O
		//IL_01b5: Expected O, but got I4
		//IL_01be: Expected O, but got I4
		Vector3 vector;
		if (planeNormalAxis == SurfaceAxis.Forward)
		{
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				vector = transform.forward;
				goto IL_00ef;
			}
		}
		else if (planeNormalAxis == SurfaceAxis.Right)
		{
			Transform transform2 = base.transform;
			if ((object)transform2 != null)
			{
				vector = transform2.right;
				goto IL_00ef;
			}
		}
		else
		{
			Transform transform3 = base.transform;
			if ((object)transform3 != null)
			{
				vector = transform3.up;
				goto IL_00ef;
			}
		}
		return (Plane)new NullReferenceException();
		IL_00ef:
		Vector3 planeOriginPoint = GetPlaneOriginPoint();
		object obj = default(object);
		float num = additionalSurfaceOffset * (float)obj;
		float num2 = additionalSurfaceOffset * vector.z;
		object obj2 = default(object);
		float num3 = num + (float)obj2;
		float num4 = num2 + planeOriginPoint.z;
		Plane plane = default(Plane);
		((Plane*)(nint)plane)->m_Distance = 0f;
		((Plane*)(nint)plane)->m_Normal = (Vector3)vector.x;
		_ = vector.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			((Plane*)(nint)plane)->m_Normal = (Vector3)0;
			object obj3 = 0;
			object obj4 = 0;
		}
		else
		{
			Vector3 normal = (Vector3)((object)plane.m_Normal / obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnBuffer @ rcx (UnityEngine.Plane)+4]");
			object obj3 = 0 / obj2;
			((Plane*)(nint)plane)->m_Normal = normal;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnBuffer @ rcx (UnityEngine.Plane)+8]");
			object obj4 = 0 / obj2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnBuffer @ rcx (UnityEngine.Plane)+4]");
		object obj5 = obj * 0;
		object obj6 = obj2 * (object)plane.m_Normal;
		float num5 = num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnBuffer @ rcx (UnityEngine.Plane)+8]");
		float num6 = num5 * 0f;
		object obj7 = obj5 + obj6;
		float num7 = (float)obj7 + num6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		float distance = num7 ^ 0;
		((Plane*)(nint)plane)->m_Distance = distance;
		return plane;
	}

	public unsafe Vector3 ProjectOntoSurface(Vector3 worldPos)
	{
		//IL_009e: Expected native int or pointer, but got O
		//IL_00ab: Expected native int or pointer, but got O
		Plane surfacePlane = GetSurfacePlane();
		float num2 = default(float);
		float num = num2 * num2;
		float num3 = (float)surfacePlane.m_Normal * worldPos.x;
		float num4 = num2 * worldPos.z;
		float num5 = num + num3;
		float num6 = num5 + num4;
		float num7 = num6 + num2;
		float num8 = num7 * num2;
		float z = worldPos.z - num8;
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = num2;
		((Vector3*)(nint)vector)->z = z;
		return vector;
	}

	public unsafe Vector3 ClampOnSurface(Vector3 worldPos)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_056a: Expected native int or pointer, but got O
		//IL_0577: Expected native int or pointer, but got O
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_01da: Invalid comparison between I4 and F4
		//IL_01ec: Expected F4, but got I4
		//IL_0426: Expected F4, but got I
		//IL_0454: Invalid comparison between F4 and I
		//IL_0219: Expected O, but got I4
		//IL_0341: Expected F4, but got I
		//IL_036f: Invalid comparison between F4 and I
		//IL_0779: Expected F4, but got I
		//IL_0798: Invalid comparison between F4 and I
		//IL_0257: Expected F4, but got I
		//IL_0285: Invalid comparison between F4 and I
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Expected O, but got Unknown
		//IL_053e: Expected native int or pointer, but got O
		//IL_0550: Expected native int or pointer, but got O
		//IL_0750: Expected F4, but got I
		object obj2 = default(object);
		object obj = obj2 - 95;
		Plane surfacePlane = GetSurfacePlane();
		bool flag = !clampToBounds;
		_ = worldPos.x;
		float num2 = default(float);
		float num = num2 * num2;
		float num3 = (float)surfacePlane.m_Normal * worldPos.x;
		float num4 = num2 * worldPos.z;
		float num5 = num + num3;
		float num6 = num5 + num4;
		float num7 = num6 + num2;
		_ = worldPos.x;
		float num8 = num2 * num7;
		float z = worldPos.z - num8;
		float z2;
		if (!flag && (bool)_boundsBox)
		{
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				Vector3 position = (Vector3)(obj - 57);
				Vector3 vector = transform.InverseTransformPoint(position);
				z2 = vector.z;
				_ = vector.x;
				if ((object)_boundsBox != null)
				{
					Vector3 size = _boundsBox.size;
					float num9 = num2 * 0.5f;
					float num10 = size.x * 0.5f;
					_ = size.x;
					float num11 = size.z * 0.5f;
					if ((object)_boundsBox != null)
					{
						Vector3 center = _boundsBox.center;
						float num12 = center.x - num10;
						float num13 = center.y - num9;
						float num14 = center.z - num11;
						if ((object)_boundsBox != null)
						{
							Vector3 center2 = _boundsBox.center;
							bool flag2 = !(0f < clampInset);
							float num15 = 0f;
							if (!flag2)
							{
								num15 = clampInset;
							}
							float num16 = center2.x + num10;
							float num17 = center2.z + num11;
							float num18 = num2 + num9;
							bool flag3 = planeNormalAxis == SurfaceAxis.Up;
							if (!flag3)
							{
								object obj3 = planeNormalAxis - 1;
								if (!flag3)
								{
									if ((nint)obj3 == 1)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-35]");
										float num19 = 0f;
										float num20 = num18 - num15;
										float num21 = num15 + num13;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-35]");
										if (!(num21 > 0f))
										{
											if (num19 > num20)
											{
												num19 = num20;
											}
										}
										else
										{
											num19 = num21;
										}
										float num22 = num17 - num15;
										float num23 = num15 + num14;
										if (!(num23 > z2))
										{
											bool flag4 = !(z2 > num22);
											float num24 = z2;
											if (!flag4)
											{
												num24 = num22;
											}
										}
										else
										{
											float num24 = num23;
										}
										if ((object)_boundsBox == null)
										{
											goto IL_0589;
										}
										_ = _boundsBox.center.x;
										goto IL_0740;
									}
								}
								else
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-39]");
									float num25 = 0f;
									float num26 = num16 - num15;
									float num27 = num15 + num12;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-39]");
									if (!(num27 > 0f))
									{
										if (num25 > num26)
										{
											num25 = num26;
										}
									}
									else
									{
										num25 = num27;
									}
									float num28 = num18 - num15;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-35]");
									float num29 = 0f;
									float num30 = num15 + num13;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-35]");
									if (!(num30 > 0f))
									{
										if (num29 > num28)
										{
											num29 = num28;
										}
									}
									else
									{
										num29 = num30;
									}
									if ((object)_boundsBox == null)
									{
										goto IL_0589;
									}
									z2 = _boundsBox.center.z;
								}
								goto IL_06b6;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-39]");
							float num31 = 0f;
							float num32 = num16 - num15;
							float num33 = num15 + num12;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-39]");
							if (!(num33 > 0f))
							{
								if (num31 > num32)
								{
									num31 = num32;
								}
							}
							else
							{
								num31 = num33;
							}
							float num34 = num17 - num15;
							float num35 = num15 + num14;
							if (!(num35 > z2))
							{
								bool flag5 = !(z2 > num34);
								float num36 = z2;
								if (!flag5)
								{
									num36 = num34;
								}
							}
							else
							{
								float num36 = num35;
							}
							if ((object)_boundsBox != null)
							{
								_ = _boundsBox.center.y;
								goto IL_0740;
							}
						}
					}
				}
			}
			goto IL_0589;
		}
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = num2;
		((Vector3*)(nint)vector2)->z = z;
		return vector2;
		IL_06b6:
		Transform transform2 = base.transform;
		if ((object)transform2 != null)
		{
			Vector3 position2 = (Vector3)(obj - 57);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-39]");
			_ = 0;
			Vector3 vector3 = transform2.TransformPoint(position2);
			((Vector3*)(nint)vector2)->x = vector3.x;
			((Vector3*)(nint)vector2)->z = vector3.z;
			return vector2;
		}
		goto IL_0589;
		IL_0740:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-31]");
		z2 = 0f;
		goto IL_06b6;
		IL_0589:
		return (Vector3)new NullReferenceException();
	}

	public unsafe Vector3 ClampOnSurfacePreserveNormalOffset(Vector3 worldPos)
	{
		//IL_0008: Expected O, but got Ref
		//IL_04b1: Expected native int or pointer, but got O
		//IL_04c3: Expected native int or pointer, but got O
		//IL_006f: Expected O, but got Ref
		//IL_01c0: Invalid comparison between I4 and F4
		//IL_01d2: Expected F4, but got I4
		//IL_0386: Expected F4, but got I
		//IL_03b4: Invalid comparison between F4 and I
		//IL_01ff: Expected O, but got I4
		//IL_02c5: Expected F4, but got I
		//IL_02f3: Invalid comparison between F4 and I
		//IL_05e7: Expected F4, but got I
		//IL_0606: Invalid comparison between F4 and I
		//IL_023d: Expected F4, but got I
		//IL_026b: Invalid comparison between F4 and I
		//IL_0450: Expected O, but got Ref
		//IL_0480: Expected native int or pointer, but got O
		//IL_0492: Expected native int or pointer, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		float num6;
		float num7;
		float num9;
		float z;
		if (clampToBounds && (bool)_boundsBox)
		{
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				Vector3 position = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
				_ = worldPos.z;
				_ = worldPos.x;
				Vector3 vector = transform.InverseTransformPoint(position);
				z = vector.z;
				_ = vector.x;
				if ((object)_boundsBox != null)
				{
					Vector3 size = _boundsBox.size;
					object obj3 = default(object);
					float num = (float)obj3 * 0.5f;
					float num2 = size.x * 0.5f;
					_ = size.x;
					float num3 = size.z * 0.5f;
					if ((object)_boundsBox != null)
					{
						Vector3 center = _boundsBox.center;
						float num4 = center.x - num2;
						float num5 = center.y - num;
						num6 = center.z - num3;
						if ((object)_boundsBox != null)
						{
							Vector3 center2 = _boundsBox.center;
							bool flag = !(0f < clampInset);
							num7 = 0f;
							if (!flag)
							{
								num7 = clampInset;
							}
							float num8 = center2.x + num2;
							num9 = center2.z + num3;
							float num10 = (float)obj3 + num;
							bool flag2 = planeNormalAxis == SurfaceAxis.Up;
							if (!flag2)
							{
								object obj4 = planeNormalAxis - 1;
								if (!flag2)
								{
									if ((nint)obj4 == 1)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-35]");
										float num11 = 0f;
										float num12 = num10 - num7;
										float num13 = num7 + num5;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-35]");
										if (!(num13 > 0f))
										{
											if (!(num11 > num12))
											{
											}
										}
										else
										{
											num11 = num13;
										}
										goto IL_0589;
									}
								}
								else
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-39]");
									float num14 = 0f;
									float num15 = num8 - num7;
									float num16 = num7 + num4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-39]");
									if (!(num16 > 0f))
									{
										if (num14 > num15)
										{
											num14 = num15;
										}
									}
									else
									{
										num14 = num16;
									}
									float num17 = num10 - num7;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-35]");
									float num18 = 0f;
									float num19 = num7 + num5;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-35]");
									if (!(num19 > 0f))
									{
										if (!(num18 > num17))
										{
										}
									}
									else
									{
										num18 = num19;
									}
								}
								goto IL_0558;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-39]");
							float num20 = 0f;
							float num21 = num8 - num7;
							float num22 = num7 + num4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-39]");
							if (!(num22 > 0f))
							{
								if (num20 > num21)
								{
									num20 = num21;
								}
							}
							else
							{
								num20 = num22;
							}
							goto IL_0589;
						}
					}
				}
			}
			goto IL_04d5;
		}
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = worldPos.x;
		((Vector3*)(nint)vector2)->z = worldPos.z;
		return vector2;
		IL_0589:
		float num23 = num9 - num7;
		float num24 = num7 + num6;
		float num25;
		if (!(num24 > z))
		{
			bool flag3 = !(z > num23);
			num25 = z;
			if (!flag3)
			{
				num25 = num23;
			}
		}
		else
		{
			num25 = num24;
		}
		z = num25;
		goto IL_0558;
		IL_04d5:
		return (Vector3)new NullReferenceException();
		IL_0558:
		Transform transform2 = base.transform;
		if ((object)transform2 != null)
		{
			Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-39]");
			_ = 0;
			Vector3 vector3 = transform2.TransformPoint(position2);
			((Vector3*)(nint)vector2)->x = vector3.x;
			((Vector3*)(nint)vector2)->z = vector3.z;
			return vector2;
		}
		goto IL_04d5;
	}

	public unsafe Vector3 GetSurfaceCenterWorldPosition()
	{
		//IL_0017: Expected native int or pointer, but got O
		//IL_0029: Expected native int or pointer, but got O
		Vector3 planeOriginPoint = GetPlaneOriginPoint();
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = planeOriginPoint.x;
		((Vector3*)(nint)vector)->z = planeOriginPoint.z;
		return vector;
	}

	public unsafe bool IsPointerOverSurface(Camera cam, Vector2 screenPos, float maxDistance, out RaycastHit hit)
	{
		//IL_00e1: Expected I4, but got O
		//IL_0082: Expected O, but got Ref
		//IL_00bc: Expected O, but got Ref
		ref RaycastHit hitInfo = ref *(RaycastHit*)null;
		_ = 0;
		_ = 0;
		_ = 0;
		if ((bool)cam && (bool)raycastTargetCollider)
		{
			if ((object)cam != null)
			{
				object obj = default(object);
				Ray ray = cam.ScreenPointToRay((Vector3)(&obj));
				if ((object)raycastTargetCollider != null)
				{
					return raycastTargetCollider.Raycast((Ray)(&obj), out hitInfo, maxDistance);
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	public void AddItem(DraggableItem item)
	{
		if (item != null && !items.Contains(item))
		{
			items.Add(item);
		}
	}

	public void RemoveItem(DraggableItem item)
	{
		bool flag = items.Remove(item);
	}

	public void MoveItemsToDeck()
	{
		//IL_0136: Expected O, but got I4
		//IL_013f: Expected O, but got I4
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		if (!(sourceDeckArea != null))
		{
			return;
		}
		List<DraggableItem> list = new List<DraggableItem>();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<DraggableItem>.Enumerator enumerator = default(List<DraggableItem>.Enumerator);
		DraggableItem item = default(DraggableItem);
		DraggableItem draggableItem = default(DraggableItem);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (list == null)
				{
					break;
				}
				list.Add(item);
				continue;
			}
			enumerator.Dispose();
			object obj = 0;
			object obj2 = 0;
			while ((nint)obj2 < list._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				draggableItem.MoveToDeck(sourceDeckArea);
				obj++;
				obj2 = obj;
			}
			return;
		}
		throw new NullReferenceException();
	}

	public unsafe static Vector3 ProjectPointOnPlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
	{
		//IL_0013: Invalid comparison between O and F4
		//IL_0090: Expected native int or pointer, but got O
		//IL_009d: Expected native int or pointer, but got O
		//IL_00aa: Expected native int or pointer, but got O
		//IL_0175: Expected native int or pointer, but got O
		//IL_0182: Expected native int or pointer, but got O
		//IL_0030: Expected F4, but got I4
		//IL_0039: Expected F4, but got I4
		//IL_0042: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		object obj = default(object);
		float x;
		float y;
		float z;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			x = 0f;
			y = 0f;
			z = 0f;
		}
		else
		{
			z = planeNormal.z / (float)obj;
			y = planeNormal.y / (float)obj;
			x = planeNormal.x / (float)obj;
		}
		((Vector3*)(nint)planeNormal)->x = x;
		((Vector3*)(nint)planeNormal)->y = y;
		((Vector3*)(nint)planeNormal)->z = z;
		float num = point.x - planePoint.x;
		float num3 = default(float);
		float num2 = num3 - num3;
		float num4 = num * planeNormal.x;
		float num5 = point.z - planePoint.z;
		float num6 = num2 * num3;
		float num7 = num5 * planeNormal.z;
		float num8 = num6 + num4;
		float num9 = num8 + num7;
		float num10 = num9 * planeNormal.z;
		float z2 = point.z - num10;
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = num3;
		((Vector3*)(nint)vector)->z = z2;
		return vector;
	}

	public DragSurface()
	{
		List<DraggableItem> list = new List<DraggableItem>();
		items = list;
		base._002Ector();
	}

	static DragSurface()
	{
		List<DragSurface> allSurfaces = new List<DragSurface>();
		AllSurfaces = allSurfaces;
	}
}
