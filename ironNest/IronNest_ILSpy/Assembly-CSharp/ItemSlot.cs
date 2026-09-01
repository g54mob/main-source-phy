using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class ItemSlot : MonoBehaviour
{
	public static readonly List<ItemSlot> AllSlots;

	public DraggableItem CurrentItem;

	public bool ejectExistingOnNewDrop = true;

	public Transform itemAnchor;

	public DragSurface ejectSurfaceOverride;

	public DraggableItem.EjectAxis ejectAxis = DraggableItem.EjectAxis.NegativeX;

	public float ejectDistance = 0.8f;

	public float ejectDistanceRandomness = 0.4f;

	public float spreadAmount = 0.15f;

	public float ejectSlideDuration = 0.35f;

	public UnityEvent<GameObject> onItemAdded;

	public UnityEvent<GameObject> onItemRemoved;

	public UnityEvent onSlotFilled;

	public UnityEvent onSlotCleared;

	public bool debugLogs;

	private BoxCollider boxCol;

	public bool HasItem => CurrentItem != null;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		BoxCollider boxCollider = default(BoxCollider);
		boxCol = boxCollider;
		if (!boxCol)
		{
			GameObject gameObject = base.gameObject;
			BoxCollider boxCollider2 = gameObject.AddComponent<BoxCollider>();
			boxCol = boxCollider2;
		}
	}

	private void OnEnable()
	{
		if (!AllSlots.Contains(this))
		{
			AllSlots.Add(this);
		}
	}

	private void OnDisable()
	{
		bool flag = AllSlots.Remove(this);
	}

	private void Update()
	{
		if (!(CurrentItem != null))
		{
			return;
		}
		Transform transform;
		if (itemAnchor != null)
		{
			transform = itemAnchor;
		}
		else
		{
			Transform transform2 = base.transform;
			transform = transform2;
		}
		Transform transform3 = CurrentItem.transform;
		Transform parent = transform3.parent;
		if (!(parent == transform) || !Overlaps(CurrentItem))
		{
			if (debugLogs)
			{
				Debug.Log("[ItemSlot] Auto-detected item exit.", this);
			}
			CurrentItem = null;
			if (onItemRemoved != null)
			{
				GameObject arg = CurrentItem.gameObject;
				onItemRemoved.Invoke(arg);
			}
			if (onSlotCleared != null)
			{
				onSlotCleared.Invoke();
			}
		}
	}

	public unsafe void PlaceItem(DraggableItem item)
	{
		//IL_015b: Expected O, but got Ref
		if (!item)
		{
			return;
		}
		if (CurrentItem != null && CurrentItem != item)
		{
			if (!ejectExistingOnNewDrop)
			{
				return;
			}
			RemoveItem(CurrentItem, autoEject: true);
		}
		Transform parent;
		if (itemAnchor != null)
		{
			parent = itemAnchor;
		}
		else
		{
			Transform transform = base.transform;
			parent = transform;
		}
		CurrentItem = item;
		Transform transform2 = item.transform;
		transform2.SetParent(parent, worldPositionStays: true);
		Transform transform3 = item.transform;
		object obj = default(object);
		transform3.localPosition = (Vector3)(&obj);
		if (debugLogs)
		{
			string text = item.name;
			string message = "[ItemSlot] Item placed: '" + text + "'.";
			Debug.Log(message, this);
		}
		if (onItemAdded != null)
		{
			GameObject arg = item.gameObject;
			onItemAdded.Invoke(arg);
		}
		if (onSlotFilled != null)
		{
			onSlotFilled.Invoke();
		}
	}

	public unsafe void RemoveItem(DraggableItem item, bool autoEject = false)
	{
		//IL_0008: Expected O, but got Ref
		//IL_03f8: Expected O, but got Ref
		//IL_0436: Expected I, but got O
		//IL_04a1: Expected O, but got I
		//IL_0a70: Expected F4, but got I4
		//IL_058f: Expected O, but got I4
		//IL_02a0: Expected O, but got Ref
		//IL_06a8: Expected O, but got I
		//IL_0738: Expected O, but got I
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Expected O, but got Unknown
		//IL_083a: Expected O, but got Ref
		//IL_084a: Expected I4, but got O
		//IL_0877: Expected O, but got Ref
		//IL_0b1f: Expected O, but got Ref
		//IL_08a1: Expected O, but got Ref
		//IL_08db: Expected O, but got Ref
		//IL_08ff: Expected O, but got Ref
		//IL_0352: Expected I, but got O
		//IL_0960: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		bool flag = !debugLogs;
		DraggableItem currentItem = null;
		if (!flag)
		{
			string[] array = new string[12]
			{
				"[ItemSlot] RemoveItem called on '", null, null, null, null, null, null, null, null, null,
				null, null
			};
			string text = base.name;
			array[1] = text;
			array[2] = "'. item='";
			string text2 = ((!item) ? "null" : item.name);
			array[3] = text2;
			array[4] = "' (id=";
			string text3;
			if ((bool)item)
			{
				int instanceID = item.GetInstanceID();
				int num = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 85));
				text3 = ((int*)num)->ToString();
			}
			else
			{
				text3 = "-";
			}
			array[5] = text3;
			array[6] = "), CurrentItem='";
			string text4 = ((!CurrentItem) ? "null" : CurrentItem.name);
			array[7] = text4;
			array[8] = "' (id=";
			string text5;
			if ((bool)CurrentItem)
			{
				int instanceID2 = CurrentItem.GetInstanceID();
				int num2 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 85));
				text5 = ((int*)num2)->ToString();
			}
			else
			{
				text5 = "-";
			}
			array[9] = text5;
			array[10] = "), ";
			object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			if (!item)
			{
				bool flag2 = autoEject;
				currentItem = null;
				bool flag3 = true;
			}
			else
			{
				bool flag3 = CurrentItem != item;
				bool flag2 = false;
				currentItem = null;
			}
			object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			string text6 = $"autoEject={arg}, willReturnEarly={arg2}";
			array[11] = text6;
			string message = string.Concat(array);
			Debug.Log(message, this);
			nint num3 = unchecked((nint)null);
		}
		if (!item || !(CurrentItem == item))
		{
			return;
		}
		UnityEngine.Object currentItem2 = CurrentItem;
		CurrentItem = currentItem;
		if (debugLogs)
		{
			string arg3 = CurrentItem.name;
			object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg4 = default(object);
			string message2 = $"[ItemSlot] Item removed: '{arg3}'. autoEject={arg4}";
			Debug.Log(message2, this);
			nint num3 = unchecked((nint)null);
		}
		UnityEngine.Object obj6;
		float z;
		Vector3 up;
		float num5;
		if (autoEject)
		{
			if (ejectSurfaceOverride != null)
			{
				obj6 = ejectSurfaceOverride;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ r14_v11 (UnityEngine.Object)+40]");
				obj6 = (UnityEngine.Object)0;
			}
			if (debugLogs)
			{
				if (obj6 != null)
				{
					Transform transform = ((Component)obj6).transform;
					Vector3 right = transform.right;
					z = right.z;
					_ = right.x;
					Transform transform2 = ((Component)obj6).transform;
					up = transform2.up;
					bool flag4 = this.ejectAxis == DraggableItem.EjectAxis.PositiveX;
					if (!flag4)
					{
						object obj7 = this.ejectAxis - 1;
						if (flag4)
						{
							goto IL_05d6;
						}
						object obj8 = obj7 - 1;
						if (!flag4)
						{
							if ((nint)obj8 != 1)
							{
								goto IL_05d6;
							}
							_ = up.x;
							float num4 = up.z ^ -0f;
							_ = right.x;
							num5 = num4;
						}
						else
						{
							num5 = up.z;
							_ = up.x;
							_ = right.x;
						}
						goto IL_0671;
					}
					_ = right.x;
					num5 = right.z;
					goto IL_0b68;
				}
				string text7 = CurrentItem.name;
				string message3 = "[ItemSlot] Ejecting '" + text7 + "' but no surface is available (ejectSurfaceOverride is null and removed.surfaceRef is null) — MoveToSurface will no-op.";
				Debug.LogWarning(message3, this);
			}
			goto IL_0a3f;
		}
		goto IL_0a75;
		IL_05d6:
		float num6 = z ^ -0f;
		num5 = num6;
		goto IL_0b68;
		IL_0a75:
		if (onItemRemoved != null)
		{
			GameObject arg5 = CurrentItem.gameObject;
			onItemRemoved.Invoke(arg5);
		}
		if (onSlotCleared != null)
		{
			onSlotCleared.Invoke();
		}
		return;
		IL_0b68:
		z = up.z;
		_ = up.x;
		goto IL_0671;
		IL_0a3f:
		DraggableItem.EjectAxis ejectAxis = default(DraggableItem.EjectAxis);
		float num7 = default(float);
		float num8 = default(float);
		float num9 = default(float);
		CurrentItem.MoveToSurface(ejectSurfaceOverride, slideLeft: true, positionAlreadySet: false, ejectAxis, num7, num8, num9, (float)this.ejectAxis);
		goto IL_0a75;
		IL_0671:
		Transform transform3 = CurrentItem.transform;
		Vector3 position = transform3.position;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-49]");
		object obj9 = (nint)0 * (nint)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-39]");
		float num10 = 0f * ejectDistance;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-35]");
		float num11 = 0f * ejectDistance;
		float num12 = num10 + position.x;
		float num13 = num5 * ejectDistance;
		float num14 = num13 + position.z;
		float num15 = num12 + (float)obj9;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-45]");
		object obj10 = (nint)0 * (nint)0;
		object obj11 = default(object);
		float num16 = num11 + (float)obj11;
		float num17 = num16 + (float)obj10;
		float num18 = z * 0f;
		float num19 = num14 + num18;
		Plane surfacePlane = ((DragSurface)obj6).GetSurfacePlane();
		float num20 = num15 * (float)surfacePlane.m_Normal;
		float num21 = num17 * (float)obj11;
		float num22 = num19 * (float)obj11;
		float num23 = num21 + num20;
		float num24 = num23 + num22;
		float num25 = num24 + (float)obj11;
		float num26 = (float)surfacePlane.m_Normal * num25;
		float num27 = num25 * (float)obj11;
		float num28 = num19 - num27;
		string arg6 = CurrentItem.name;
		object obj12 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		_ = this.ejectAxis;
		object arg7 = (DraggableItem.EjectAxis)obj12;
		string text8 = $"[ItemSlot] Ejecting '{arg6}' along axis={arg7}, ";
		object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
		object arg8 = (Vector3)obj13;
		object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 37));
		_ = ejectDistance;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg9 = default(object);
		string text9 = $"approxTarget={arg8}, dist={arg9}, ";
		object obj15 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 33));
		_ = spreadAmount;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object obj16 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 29));
		_ = ejectSlideDuration;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg10 = default(object);
		object arg11 = default(object);
		string text10 = $"spread=±{arg10}, dur={arg11}s, ";
		string text11 = obj6.name;
		bool flag5 = ejectSurfaceOverride != null;
		object obj17 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg12 = default(object);
		string text12 = $"(override={arg12}).";
		string message4 = text8 + text9 + text10 + "surface='" + text11 + "' " + text12;
		Debug.Log(message4, this);
		goto IL_0a3f;
	}

	public void ClearSlot()
	{
		bool flag = CurrentItem == null;
		if (!flag)
		{
			if (debugLogs != flag)
			{
				Debug.Log("[ItemSlot] ClearSlot called.", this);
			}
			CurrentItem = null;
			if (onItemRemoved != null)
			{
				GameObject arg = CurrentItem.gameObject;
				onItemRemoved.Invoke(arg);
			}
			if (onSlotCleared != null)
			{
				onSlotCleared.Invoke();
			}
		}
	}

	public unsafe bool Overlaps(DraggableItem item)
	{
		//IL_01b4: Expected I4, but got O
		//IL_018f: Expected O, but got Ref
		//IL_018f: Expected O, but got Ref
		if ((bool)item)
		{
			if ((object)item != null)
			{
				if (!item.Col)
				{
					goto IL_0198;
				}
				Transform transform = base.transform;
				if ((object)transform != null)
				{
					Vector3 position = transform.position;
					Transform transform2 = base.transform;
					if ((object)transform2 != null)
					{
						Quaternion rotation = transform2.rotation;
						Transform transform3 = item.transform;
						if ((object)transform3 != null)
						{
							Vector3 position2 = transform3.position;
							Transform transform4 = item.transform;
							if ((object)transform4 != null)
							{
								Quaternion rotation2 = transform4.rotation;
								object obj = default(object);
								object obj2 = default(object);
								Vector3 positionB = default(Vector3);
								Quaternion rotationB = default(Quaternion);
								ref Vector3 direction = default(ref Vector3);
								ref float distance = default(ref float);
								return Physics.ComputePenetration(boxCol, (Vector3)(&obj), (Quaternion)(&obj2), item.Col, positionB, rotationB, out direction, out distance);
							}
						}
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		goto IL_0198;
		IL_0198:
		return false;
	}

	private unsafe static void ResolveEjectAxes(DragSurface surf, DraggableItem.EjectAxis axis, out Vector3 ejectDir, out Vector3 spreadDir)
	{
		//IL_0154: Expected Ref, but got F4
		//IL_0171: Expected Ref, but got F4
		//IL_0069: Expected O, but got I4
		//IL_0135: Expected O, but got F4
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_00fc: Expected Ref, but got F4
		//IL_0113: Expected Ref, but got F4
		//IL_00c6: Expected O, but got F4
		//IL_00e0: Expected Ref, but got F4
		Transform transform = surf.transform;
		Vector3 right = transform.right;
		Transform transform2 = surf.transform;
		Vector3 up = transform2.up;
		bool flag = axis == DraggableItem.EjectAxis.PositiveX;
		ref Vector3 reference2;
		if (!flag)
		{
			object obj = axis - 1;
			object obj4 = default(object);
			ref Vector3 reference;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (flag)
				{
					reference = ref *(Vector3*)up.x;
					_ = up.z;
					reference2 = ref *(Vector3*)right.x;
					_ = right.z;
					return;
				}
				if ((nint)obj2 == 1)
				{
					object obj3 = up.z ^ -0f;
					reference = ref *(Vector3*)obj4;
					reference2 = ref *(Vector3*)right.x;
					_ = right.z;
					return;
				}
			}
			object obj5 = right.z ^ -0f;
			reference = ref *(Vector3*)obj4;
		}
		else
		{
			ref Vector3 reference = ref *(Vector3*)right.x;
			_ = right.z;
		}
		reference2 = ref *(Vector3*)up.x;
		_ = up.z;
	}

	static ItemSlot()
	{
		List<ItemSlot> allSlots = new List<ItemSlot>();
		AllSlots = allSlots;
	}
}
