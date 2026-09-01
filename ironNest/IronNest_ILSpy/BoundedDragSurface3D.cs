using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class BoundedDragSurface3D : MonoBehaviour
{
	public enum SurfaceAxis
	{
		Up,
		Forward,
		Right
	}

	private BoxCollider boundsBox;

	private SurfaceAxis planeNormalAxis;

	private Collider raycastTargetCollider;

	private float defaultDragLift = 0.02f;

	private float surfaceScaleMultiplier = 1f;

	private bool preferAlignRotationOnEnter = true;

	public BoxCollider BoundsBox => boundsBox;

	public Collider RaycastTargetCollider => raycastTargetCollider;

	public float DefaultDragLift => defaultDragLift;

	public float SurfaceScaleMultiplier => surfaceScaleMultiplier;

	public bool PreferAlignRotationOnEnter => preferAlignRotationOnEnter;

	public unsafe Vector3 GetPlaneNormal()
	{
		//IL_01d3: Expected native int or pointer, but got O
		//IL_01e5: Expected native int or pointer, but got O
		//IL_0176: Expected native int or pointer, but got O
		//IL_0188: Expected native int or pointer, but got O
		Vector3 vector;
		Vector3 vector2 = default(Vector3);
		if ((bool)boundsBox)
		{
			if (planeNormalAxis == SurfaceAxis.Forward)
			{
				if ((object)boundsBox != null)
				{
					Transform transform = boundsBox.transform;
					if ((object)transform != null)
					{
						vector = transform.forward;
						goto IL_0169;
					}
				}
			}
			else if ((object)boundsBox != null)
			{
				if (planeNormalAxis == SurfaceAxis.Right)
				{
					Transform transform2 = boundsBox.transform;
					if ((object)transform2 != null)
					{
						vector = transform2.right;
						goto IL_0169;
					}
				}
				else
				{
					Transform transform3 = boundsBox.transform;
					if ((object)transform3 != null)
					{
						vector = transform3.up;
						goto IL_0169;
					}
				}
			}
		}
		else
		{
			Transform transform4 = base.transform;
			if ((object)transform4 != null)
			{
				Vector3 up = transform4.up;
				((Vector3*)(nint)vector2)->x = up.x;
				((Vector3*)(nint)vector2)->z = up.z;
				return vector2;
			}
		}
		return (Vector3)new NullReferenceException();
		IL_0169:
		((Vector3*)(nint)vector2)->x = vector.x;
		((Vector3*)(nint)vector2)->z = vector.z;
		return vector2;
	}

	public unsafe Vector3 GetPlaneOriginPoint()
	{
		//IL_0102: Expected native int or pointer, but got O
		//IL_0114: Expected native int or pointer, but got O
		//IL_00b3: Expected O, but got Ref
		Vector3 vector;
		if ((bool)boundsBox)
		{
			if ((object)boundsBox != null)
			{
				Transform transform = boundsBox.transform;
				if ((object)boundsBox != null)
				{
					Vector3 center = boundsBox.center;
					if ((object)transform != null)
					{
						object obj = default(object);
						vector = transform.TransformPoint((Vector3)(&obj));
						goto IL_00f5;
					}
				}
			}
		}
		else
		{
			Transform transform2 = base.transform;
			if ((object)transform2 != null)
			{
				vector = transform2.position;
				goto IL_00f5;
			}
		}
		return (Vector3)new NullReferenceException();
		IL_00f5:
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = vector.x;
		((Vector3*)(nint)vector2)->z = vector.z;
		return vector2;
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

	public unsafe Plane GetPlane()
	{
		//IL_0022: Expected native int or pointer, but got O
		//IL_0039: Expected O, but got F4
		//IL_0034: Expected native int or pointer, but got O
		//IL_00db: Expected O, but got F4
		//IL_00d6: Expected native int or pointer, but got O
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Expected F4, but got Unknown
		//IL_018b: Expected native int or pointer, but got O
		//IL_0082: Expected O, but got I4
		//IL_007d: Expected native int or pointer, but got O
		//IL_008b: Expected F4, but got I4
		//IL_0094: Expected F4, but got I4
		Vector3 planeNormal = GetPlaneNormal();
		Vector3 planeOriginPoint = GetPlaneOriginPoint();
		Plane plane = default(Plane);
		((Plane*)(nint)plane)->m_Distance = 0f;
		((Plane*)(nint)plane)->m_Normal = (Vector3)planeNormal.x;
		_ = planeNormal.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		if (!(planeNormal.x > 1E-05f))
		{
			((Plane*)(nint)plane)->m_Normal = (Vector3)0;
			float num = 0f;
			float num2 = 0f;
		}
		else
		{
			float num3 = (float)plane.m_Normal / planeNormal.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnBuffer @ rcx (UnityEngine.Plane)+4]");
			float num = 0f / planeNormal.x;
			((Plane*)(nint)plane)->m_Normal = (Vector3)num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnBuffer @ rcx (UnityEngine.Plane)+8]");
			float num2 = 0f / planeNormal.x;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnBuffer @ rcx (UnityEngine.Plane)+4]");
		object obj2 = default(object);
		object obj = obj2 * 0;
		float num4 = planeOriginPoint.x * (float)plane.m_Normal;
		float num5 = planeOriginPoint.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnBuffer @ rcx (UnityEngine.Plane)+8]");
		float num6 = num5 * 0f;
		float num7 = (float)obj + num4;
		float num8 = num7 + num6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		float distance = num8 ^ 0;
		((Plane*)(nint)plane)->m_Distance = distance;
		return plane;
	}

	public unsafe Vector3 ClampToSurfaceBounds(Vector3 worldPos)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0591: Expected native int or pointer, but got O
		//IL_05a3: Expected native int or pointer, but got O
		//IL_008e: Expected O, but got Ref
		//IL_04c8: Expected F4, but got I
		//IL_04d8: Invalid comparison between F4 and I
		//IL_02f7: Expected O, but got I4
		//IL_0401: Expected F4, but got I
		//IL_0411: Invalid comparison between F4 and I
		//IL_0699: Expected F4, but got I
		//IL_06a9: Invalid comparison between F4 and I
		//IL_05d6: Expected O, but got Ref
		//IL_0606: Expected native int or pointer, but got O
		//IL_0618: Expected native int or pointer, but got O
		//IL_0335: Expected F4, but got I
		//IL_0345: Invalid comparison between F4 and I
		//IL_067f: Expected F4, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Transform transform;
		float z;
		if ((bool)boundsBox)
		{
			if ((object)boundsBox != null)
			{
				transform = boundsBox.transform;
				if ((object)transform != null)
				{
					Vector3 position = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
					_ = worldPos.z;
					_ = worldPos.x;
					Vector3 vector = transform.InverseTransformPoint(position);
					z = vector.z;
					_ = vector.x;
					if ((object)boundsBox != null)
					{
						Vector3 size = boundsBox.size;
						object obj3 = default(object);
						float num = (float)obj3 * 0.5f;
						float num2 = size.x * 0.5f;
						_ = size.x;
						float num3 = size.z * 0.5f;
						if ((object)boundsBox != null)
						{
							float num4 = boundsBox.center.x - num2;
							if ((object)boundsBox != null)
							{
								float num5 = num2 + boundsBox.center.x;
								if ((object)boundsBox != null)
								{
									float num6 = boundsBox.center.y - num;
									if ((object)boundsBox != null)
									{
										float num7 = num + boundsBox.center.y;
										if ((object)boundsBox != null)
										{
											float num8 = boundsBox.center.z - num3;
											if ((object)boundsBox != null)
											{
												float num9 = num3 + boundsBox.center.z;
												bool flag = planeNormalAxis == SurfaceAxis.Up;
												if (!flag)
												{
													object obj4 = planeNormalAxis - 1;
													if (!flag)
													{
														if ((nint)obj4 == 1)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-35]");
															float num10 = 0f;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-35]");
															if (!(num6 > 0f))
															{
																if (num10 > num7)
																{
																	num10 = num7;
																}
															}
															else
															{
																num10 = num6;
															}
															if (!(num8 > z))
															{
																bool flag2 = !(z > num9);
																float num11 = z;
																if (!flag2)
																{
																	num11 = num9;
																}
															}
															else
															{
																float num11 = num8;
															}
															if ((object)boundsBox == null)
															{
																goto IL_05b5;
															}
															_ = boundsBox.center.x;
															goto IL_066f;
														}
													}
													else
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-39]");
														float num12 = 0f;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-39]");
														if (!(num4 > 0f))
														{
															if (num12 > num5)
															{
																num12 = num5;
															}
														}
														else
														{
															num12 = num4;
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-35]");
														float num13 = 0f;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-35]");
														if (!(num6 > 0f))
														{
															if (num13 > num7)
															{
																num13 = num7;
															}
														}
														else
														{
															num13 = num6;
														}
														if ((object)boundsBox == null)
														{
															goto IL_05b5;
														}
														z = boundsBox.center.z;
													}
													goto IL_05c8;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-39]");
												float num14 = 0f;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-39]");
												if (!(num4 > 0f))
												{
													if (num14 > num5)
													{
														num14 = num5;
													}
												}
												else
												{
													num14 = num4;
												}
												if (!(num8 > z))
												{
													bool flag3 = !(z > num9);
													float num15 = z;
													if (!flag3)
													{
														num15 = num9;
													}
												}
												else
												{
													float num15 = num8;
												}
												if ((object)boundsBox != null)
												{
													_ = boundsBox.center.y;
													goto IL_066f;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			goto IL_05b5;
		}
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = worldPos.x;
		((Vector3*)(nint)vector2)->z = worldPos.z;
		return vector2;
		IL_05b5:
		return (Vector3)new NullReferenceException();
		IL_05c8:
		Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-39]");
		_ = 0;
		Vector3 vector3 = transform.TransformPoint(position2);
		((Vector3*)(nint)vector2)->x = vector3.x;
		((Vector3*)(nint)vector2)->z = vector3.z;
		return vector2;
		IL_066f:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-31]");
		z = 0f;
		goto IL_05c8;
	}

	public unsafe Vector3 ClampToSurfaceBoundsPreserveNormalOffset(Vector3 worldPos)
	{
		//IL_0008: Expected O, but got Ref
		//IL_04ed: Expected native int or pointer, but got O
		//IL_04ff: Expected native int or pointer, but got O
		//IL_008e: Expected O, but got Ref
		//IL_0442: Expected F4, but got I
		//IL_0452: Invalid comparison between F4 and I
		//IL_02f7: Expected O, but got I4
		//IL_039f: Expected F4, but got I
		//IL_03af: Invalid comparison between F4 and I
		//IL_05c1: Expected F4, but got I
		//IL_05d1: Invalid comparison between F4 and I
		//IL_0532: Expected O, but got Ref
		//IL_0562: Expected native int or pointer, but got O
		//IL_0574: Expected native int or pointer, but got O
		//IL_0335: Expected F4, but got I
		//IL_0345: Invalid comparison between F4 and I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Transform transform;
		float num8;
		float num9;
		float z;
		if ((bool)boundsBox)
		{
			if ((object)boundsBox != null)
			{
				transform = boundsBox.transform;
				if ((object)transform != null)
				{
					Vector3 position = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
					_ = worldPos.z;
					_ = worldPos.x;
					Vector3 vector = transform.InverseTransformPoint(position);
					z = vector.z;
					_ = vector.x;
					if ((object)boundsBox != null)
					{
						Vector3 size = boundsBox.size;
						object obj3 = default(object);
						float num = (float)obj3 * 0.5f;
						float num2 = size.x * 0.5f;
						_ = size.x;
						float num3 = size.z * 0.5f;
						if ((object)boundsBox != null)
						{
							float num4 = boundsBox.center.x - num2;
							if ((object)boundsBox != null)
							{
								float num5 = num2 + boundsBox.center.x;
								if ((object)boundsBox != null)
								{
									float num6 = boundsBox.center.y - num;
									if ((object)boundsBox != null)
									{
										float num7 = num + boundsBox.center.y;
										if ((object)boundsBox != null)
										{
											num8 = boundsBox.center.z - num3;
											if ((object)boundsBox != null)
											{
												num9 = num3 + boundsBox.center.z;
												bool flag = planeNormalAxis == SurfaceAxis.Up;
												if (!flag)
												{
													object obj4 = planeNormalAxis - 1;
													if (!flag)
													{
														if ((nint)obj4 == 1)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-35]");
															float num10 = 0f;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-35]");
															if (!(num6 > 0f))
															{
																if (!(num10 > num7))
																{
																}
															}
															else
															{
																num10 = num6;
															}
															goto IL_0590;
														}
													}
													else
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-39]");
														float num11 = 0f;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-39]");
														if (!(num4 > 0f))
														{
															if (num11 > num5)
															{
																num11 = num5;
															}
														}
														else
														{
															num11 = num4;
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-35]");
														float num12 = 0f;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-35]");
														if (!(num6 > 0f))
														{
															if (!(num12 > num7))
															{
															}
														}
														else
														{
															num12 = num6;
														}
													}
													goto IL_0524;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-39]");
												float num13 = 0f;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-39]");
												if (!(num4 > 0f))
												{
													if (num13 > num5)
													{
														num13 = num5;
													}
												}
												else
												{
													num13 = num4;
												}
												goto IL_0590;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return (Vector3)new NullReferenceException();
		}
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = worldPos.x;
		((Vector3*)(nint)vector2)->z = worldPos.z;
		return vector2;
		IL_0524:
		Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-39]");
		_ = 0;
		Vector3 vector3 = transform.TransformPoint(position2);
		((Vector3*)(nint)vector2)->x = vector3.x;
		((Vector3*)(nint)vector2)->z = vector3.z;
		return vector2;
		IL_0590:
		float num14;
		if (!(num8 > z))
		{
			bool flag2 = !(z > num9);
			num14 = z;
			if (!flag2)
			{
				num14 = num9;
			}
		}
		else
		{
			num14 = num8;
		}
		z = num14;
		goto IL_0524;
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

	private void OnValidate()
	{
		//IL_0046: Invalid comparison between I4 and F4
		if (!boundsBox)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			BoxCollider boxCollider = default(BoxCollider);
			boundsBox = boxCollider;
		}
		if (!(0f < surfaceScaleMultiplier))
		{
			surfaceScaleMultiplier = 0.0001f;
		}
	}
}
