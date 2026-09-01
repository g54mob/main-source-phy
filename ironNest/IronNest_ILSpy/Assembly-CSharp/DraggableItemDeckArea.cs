using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class DraggableItemDeckArea : MonoBehaviour
{
	private sealed class _003CLerpToTargetRoutine_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DraggableItem item;

		public DraggableItemDeckArea _003C_003E4__this;

		public Vector3 targetLocalPos;

		public Quaternion targetLocalRot;

		private float _003Cdur_003E5__2;

		private float _003Ct_003E5__3;

		private Vector3 _003CstartPos_003E5__4;

		private Quaternion _003CstartRot_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CLerpToTargetRoutine_003Ed__34(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_018f: Expected I4, but got I8
			//IL_053f: Expected I4, but got O
			//IL_0468: Expected O, but got Ref
			//IL_00e8: Expected O, but got F4
			//IL_04c0: Expected O, but got Ref
			//IL_0159: Expected O, but got F4
			//IL_029d: Invalid comparison between I4 and F4
			//IL_02e8: Expected F4, but got I4
			//IL_0307: Invalid comparison between I4 and F4
			//IL_034a: Expected O, but got Ref
			//IL_03bd: Expected O, but got Ref
			DraggableItemDeckArea draggableItemDeckArea = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((bool)item)
				{
					if ((object)_003C_003E4__this != null)
					{
						bool flag = !(0.0001f < draggableItemDeckArea.layoutAnimationDuration);
						float num = 0.0001f;
						if (!flag)
						{
							num = draggableItemDeckArea.layoutAnimationDuration;
						}
						_003Ct_003E5__3 = 0f;
						_003Cdur_003E5__2 = num;
						if ((object)item != null)
						{
							Transform transform = item.transform;
							if ((object)transform != null)
							{
								Vector3 localPosition = transform.localPosition;
								_003CstartPos_003E5__4 = (Vector3)localPosition.x;
								_ = localPosition.z;
								if ((object)item != null)
								{
									Transform transform2 = item.transform;
									if ((object)transform2 != null)
									{
										_003CstartRot_003E5__5 = (Quaternion)transform2.localRotation.x;
										goto IL_059f;
									}
								}
							}
						}
					}
					goto IL_0531;
				}
			}
			else if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				goto IL_059f;
			}
			goto IL_0523;
			IL_0531:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0523:
			return false;
			IL_04c5:
			if ((object)_003C_003E4__this != null && draggableItemDeckArea._layoutRoutines != null)
			{
				bool flag2 = draggableItemDeckArea._layoutRoutines.Remove(item);
				goto IL_0523;
			}
			goto IL_0531;
			IL_059f:
			Vector3 vector = default(Vector3);
			if (1f > _003Ct_003E5__3)
			{
				if (!(item != null))
				{
					goto IL_0523;
				}
				Component component = item;
				if ((object)item != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ rax_v16 (UnityEngine.Component)+34]");
					if ((nint)0 != 0)
					{
						goto IL_0523;
					}
					GameObject gameObject = item.gameObject;
					if ((object)gameObject != null)
					{
						if (!gameObject.activeSelf)
						{
							goto IL_0523;
						}
						float deltaTime = Time.deltaTime;
						float num2 = deltaTime / _003Cdur_003E5__2;
						float num3 = (_003Ct_003E5__3 = num2 + _003Ct_003E5__3);
						if (!(0f > num3))
						{
							if (num3 > 1f)
							{
								num3 = 1f;
							}
						}
						else
						{
							num3 = 0f;
						}
						float num4 = num3 + num3;
						float num5 = num3 * num3;
						float num6 = 3f - num4;
						float num7 = num6 * num5;
						if ((object)item != null)
						{
							Transform transform3 = item.transform;
							if (0f > num7 || !(num7 > 1f))
							{
							}
							if ((object)transform3 != null)
							{
								transform3.localPosition = (Vector3)(&vector);
								if ((object)item != null)
								{
									Transform transform4 = item.transform;
									Quaternion a = default(Quaternion);
									Quaternion b = default(Quaternion);
									Quaternion quaternion = Quaternion.Internal_Slerp(ref a, ref b, num7);
									if ((object)transform4 != null)
									{
										Quaternion quaternion2 = default(Quaternion);
										transform4.localRotation = (Quaternion)(&quaternion2);
										_003C_003E2__current = null;
										_003C_003E1__state = 1;
										return true;
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (!item)
				{
					goto IL_04c5;
				}
				if ((object)item != null)
				{
					Transform transform5 = item.transform;
					if ((object)transform5 != null)
					{
						transform5.localPosition = (Vector3)(&vector);
						if ((object)item != null)
						{
							Transform transform6 = item.transform;
							if ((object)transform6 != null)
							{
								transform6.localRotation = (Quaternion)(&vector);
								goto IL_04c5;
							}
						}
					}
				}
			}
			goto IL_0531;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public static readonly List<DraggableItemDeckArea> AllDecks;

	public float fanAngle = 60f;

	public float radius = 0.35f;

	public float cardZ;

	public float verticalOffset;

	public float deckWidth;

	public float depthArcBlend = 1f;

	public float maxGapBetweenCards;

	public float deckZSeparationStep = -0.0005f;

	public float layoutAnimationDuration = 0.25f;

	public DraggableItem Prefab_DraggableItemFallback;

	public bool debugLayout;

	public bool debugOverlap;

	public bool debugSpacing;

	public bool debugSpawn;

	public List<DraggableItem> items;

	private BoxCollider boxCol;

	private readonly Dictionary<DraggableItem, Coroutine> _layoutRoutines;

	public unsafe Bounds DeckBounds
	{
		get
		{
			//IL_003b: Expected native int or pointer, but got O
			if ((object)boxCol != null)
			{
				Bounds bounds = default(Bounds);
				((Bounds*)(nint)bounds)->m_Center = boxCol.bounds.m_Center;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rax_v2 (UnityEngine.Bounds)+10]");
				_ = 0;
				return bounds;
			}
			return (Bounds)new NullReferenceException();
		}
	}

	private void OnEnable()
	{
		if (!AllDecks.Contains(this))
		{
			AllDecks.Add(this);
		}
	}

	private void OnDisable()
	{
		bool flag = AllDecks.Remove(this);
	}

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

	public float GetOverlapVolume(DraggableItem item)
	{
		//IL_0373: Expected F4, but got I4
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		//IL_0365: Expected F4, but got I4
		//IL_01ee: Invalid comparison between O and F4
		//IL_020a: Invalid comparison between F4 and O
		//IL_04ac: Invalid comparison between I4 and F4
		//IL_04be: Expected F4, but got I4
		//IL_0434: Expected O, but got I4
		//IL_056d: Expected O, but got I4
		if ((bool)item)
		{
			if ((object)item != null)
			{
				if (!item.Col)
				{
					goto IL_036a;
				}
				if ((object)boxCol != null)
				{
					Bounds bounds = boxCol.bounds;
					if ((object)item.Col != null)
					{
						Bounds bounds2 = item.Col.bounds;
						object obj2 = default(object);
						object obj3 = default(object);
						object obj = obj2 - obj3;
						object obj4 = obj2 + obj3;
						float num = (float)obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v10 (UnityEngine.Bounds)+10]");
						float num2 = num - 0f;
						object obj5 = (object)bounds2.m_Center - obj2;
						object obj7 = default(object);
						object obj6 = obj2 - obj7;
						object obj8 = obj2 + obj7;
						object obj9 = obj2 + (object)bounds2.m_Center;
						object obj10 = (object)bounds.m_Center - obj2;
						object obj11 = obj2 + (object)bounds.m_Center;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rax_v11 (UnityEngine.Bounds)+10]");
						object obj12 = obj2 - 0;
						float num3 = (float)obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v10 (UnityEngine.Bounds)+10]");
						float num4 = num3 + 0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rax_v11 (UnityEngine.Bounds)+10]");
						object obj13 = obj2 + 0;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj13) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
						{
							float num5 = (float)obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v10 (UnityEngine.Bounds)+10]");
							float num6 = num5 + 0f;
							float num7 = (float)obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v10 (UnityEngine.Bounds)+10]");
							float num8 = num7 - 0f;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rax_v11 (UnityEngine.Bounds)+10]");
							float num9 = 0f + (float)obj2;
							float num10 = (float)obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rax_v11 (UnityEngine.Bounds)+10]");
							float num11 = num10 - 0f;
							if (num6 > num9)
							{
								num6 = num9;
							}
							if (num8 < num11)
							{
								num8 = num11;
							}
							object obj14 = obj2 + (object)bounds2.m_Center;
							float num12 = num6 - num8;
							bool flag = !(0f < num12);
							float num13 = 0f;
							if (!flag)
							{
								num13 = num12;
							}
							object obj15 = obj2 + (object)bounds.m_Center;
							object obj16 = (object)bounds.m_Center - obj2;
							object obj17 = obj2 + obj3;
							object obj18 = obj2 - obj3;
							if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj15) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14))
							{
								obj15 = obj14;
							}
							object obj19 = (object)bounds2.m_Center - obj2;
							if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj16) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj19))
							{
								obj16 = obj19;
							}
							object obj20 = obj15 - obj16;
							bool flag2 = 0 >= (nint)obj20;
							object obj21 = 0;
							if (!flag2)
							{
								obj21 = obj20;
							}
							object obj23 = default(object);
							object obj22 = obj23 + obj2;
							object obj24 = obj2 - obj23;
							float num14 = num13 * (float)obj21;
							if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj22))
							{
								obj17 = obj22;
							}
							if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj18) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj24))
							{
								obj18 = obj24;
							}
							object obj25 = obj17 - obj18;
							bool flag3 = 0 >= (nint)obj25;
							object obj26 = 0;
							if (!flag3)
							{
								obj26 = obj25;
							}
							return num14 * (float)obj26;
						}
						return 0f;
					}
				}
			}
			throw new NullReferenceException();
		}
		goto IL_036a;
		IL_036a:
		return 0f;
	}

	public void AddItems(List<DeckSpawnEntry> entries, DragSurface surface, ItemSlot slot)
	{
		if (slot != null)
		{
			List<ItemSlot> list = new List<ItemSlot>();
			list.Add(slot);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 107 Invalid \"Jump target not found in method: 0x180531FB0\"");
		throw new NullReferenceException();
	}

	public unsafe void AddItems(List<DeckSpawnEntry> entries, DragSurface surface, List<ItemSlot> slots)
	{
		//IL_007e: Expected O, but got I
		//IL_00ab: Expected O, but got I
		//IL_035f: Expected I, but got O
		//IL_0186: Expected O, but got I
		//IL_045a: Expected O, but got I
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Expected I, but got Unknown
		//IL_02b1: Expected O, but got I
		if (entries == null || entries._size == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<DeckSpawnEntry>.Enumerator enumerator = default(List<DeckSpawnEntry>.Enumerator);
		object obj = default(object);
		ItemSlot slot = default(ItemSlot);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj == null;
				nint num = (nint)(&enumerator);
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ stack_10_v3+18]");
					UnityEngine.Object obj2;
					if ((UnityEngine.Object)0 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ stack_10_v3+18]");
						obj2 = (UnityEngine.Object)0;
					}
					else
					{
						obj2 = Prefab_DraggableItemFallback;
					}
					if (obj2 != null)
					{
						Transform parent = base.transform;
						DraggableItem draggableItem = UnityEngine.Object.Instantiate((DraggableItem)obj2, parent);
						if ((object)draggableItem == null)
						{
							throw new NullReferenceException();
						}
						draggableItem.SetReferences(surface, this, slots);
						draggableItem.SetState(DraggableItem.ItemLocation.Deck, this, surface, slot);
						if (items == null)
						{
							throw new NullReferenceException();
						}
						items.Add(draggableItem);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ stack_10_v3+20]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ stack_10_v3+20]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v698 @ rcx_v38+18] (should have been resolved before IL gen)");
						}
						if (debugSpawn)
						{
							string[] array = new string[5];
							if (array == null)
							{
								throw new NullReferenceException();
							}
							array[0] = "[";
							string text = base.name;
							if (array.Length <= 1)
							{
								break;
							}
							array[1] = text;
							if (array.Length <= 2)
							{
								throw new IndexOutOfRangeException();
							}
							array[2] = "] Spawned item '";
							if (array.Length <= 3)
							{
								throw new IndexOutOfRangeException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ stack_10_v3+10]");
							array[3] = (string)0;
							if (array.Length <= 4)
							{
								throw new IndexOutOfRangeException();
							}
							array[4] = "'.";
							string message = string.Concat(array);
							Debug.Log(message, draggableItem);
						}
					}
					else if (debugSpawn)
					{
						string[] array2 = new string[5];
						bool flag2 = array2 == null;
						num = (nint)typeof(string[]);
						if (flag2)
						{
							throw new NullReferenceException();
						}
						if (array2.Length <= 0)
						{
							throw new IndexOutOfRangeException();
						}
						array2[0] = "[";
						string text2 = base.name;
						if (array2.Length <= 1)
						{
							throw new IndexOutOfRangeException();
						}
						array2[1] = text2;
						if (array2.Length <= 2)
						{
							throw new IndexOutOfRangeException();
						}
						array2[2] = "] No prefab available for entry '";
						if (array2.Length <= 3)
						{
							throw new IndexOutOfRangeException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ stack_10_v3+10]");
						array2[3] = (string)0;
						num = (nint)(array2 + 56);
						if (array2.Length <= 4)
						{
							throw new IndexOutOfRangeException();
						}
						array2[4] = "'. Skipping.";
						string message2 = string.Concat(array2);
						Debug.LogWarning(message2, this);
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			LayoutFan(animate: false);
			return;
		}
		throw new IndexOutOfRangeException();
	}

	public void AddBack(DraggableItem item)
	{
		int index = ComputeInsertionIndex(item);
		if (!items.Contains(item))
		{
			items.Insert(index, item);
		}
		Transform transform = item.transform;
		Transform parent = base.transform;
		transform.SetParent(parent, worldPositionStays: true);
		ItemSlot slot = default(ItemSlot);
		item.SetState(DraggableItem.ItemLocation.Deck, this, item.surfaceRef, slot);
		if (debugSpawn)
		{
			string arg = base.name;
			string arg2 = item.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg3 = default(object);
			string message = $"[{arg}] AddBack '{arg2}' at index {arg3}.";
			Debug.Log(message, item);
		}
		LayoutFan();
	}

	public void RemoveItem(DraggableItem item)
	{
		bool flag = items.Remove(item);
		LayoutFan();
	}

	private unsafe int ComputeInsertionIndex(DraggableItem item)
	{
		//IL_011c: Expected O, but got Ref
		//IL_0138: Expected O, but got Ref
		//IL_0195: Invalid comparison between O and F4
		if (items != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			int num = 0;
			List<DraggableItem>.Enumerator enumerator = default(List<DraggableItem>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null && obj != item)
				{
					num++;
				}
			}
			enumerator.Dispose();
			if (num == 0)
			{
				return 0;
			}
			Transform transform = base.transform;
			if ((object)item != null)
			{
				Transform transform2 = item.transform;
				if ((object)transform2 != null)
				{
					Vector3 position = transform2.position;
					bool flag = (object)transform == null;
					int num2 = default(int);
					Component component = (Component)(&num2);
					if (!flag)
					{
						float num3 = default(float);
						Vector3 vector = transform.InverseTransformPoint((Vector3)(&num3));
						List<float> list = ComputeLayoutXPositions(num);
						bool flag2 = list == null;
						int num4 = 0;
						int num5 = 0;
						if (!flag2)
						{
							object obj2 = default(object);
							while (true)
							{
								int num6 = num5;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v17 (System.Collections.Generic.List`1<System.Single>)+18]");
								if ((nint)num6 >= (nint)0)
								{
									break;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)vector.x))
								{
									num4++;
									num5 = num4;
									continue;
								}
								return num4;
							}
							return num;
						}
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	private unsafe List<float> ComputeLayoutXPositions(int count)
	{
		//IL_024f: Expected O, but got F4
		//IL_026a: Invalid comparison between F4 and I4
		//IL_003d: Invalid comparison between F4 and I4
		//IL_00c5: Expected O, but got I4
		//IL_00f7: Expected O, but got I4
		//IL_02ac: Invalid comparison between F4 and I4
		//IL_0191: Expected O, but got F4
		//IL_01d1: Expected F4, but got Ref
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Expected O, but got Unknown
		List<float> list = new List<float>(count);
		float num = deckWidth;
		object obj = fanAngle ^ -0f;
		float num2 = (float)obj * 0.5f;
		if (deckWidth > 0f && maxGapBetweenCards > 0f)
		{
			float num3 = (float)count + 1f;
			float num4 = deckWidth / num3;
			if (num4 > maxGapBetweenCards)
			{
				float num5 = (float)count + 1f;
				num = num5 * maxGapBetweenCards;
			}
		}
		if (count > 0)
		{
			object obj2 = 0;
			int capacity = count;
			List<float> list2 = list;
			float num14 = default(float);
			bool flag;
			do
			{
				float num6;
				if (count == 1)
				{
					num6 = 0.5f;
				}
				else
				{
					object obj3 = count - 1;
					num6 = (float)obj2 / (float)obj3;
				}
				float num10;
				if (!(deckWidth > 0f))
				{
					float num7 = num6 * fanAngle;
					float num8 = num7 + num2;
					float num9 = num8 * ((float)Math.PI / 180f);
					list2._002Ector(capacity);
					num10 = num9 * radius;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm1,edi\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,ebx\"");
					float num11 = 1f / 1f;
					object obj4 = num ^ -0f;
					float num12 = num11 * num;
					float num13 = (float)obj4 * 0.5f;
					num10 = num12 + num13;
				}
				if (list != null)
				{
					list.Add((nint)(&num14));
					obj2++;
					flag = (nint)obj2 < count;
					num14 = num10;
					capacity = (int)(&num14);
					list2 = list;
					continue;
				}
				return (List<float>)(object)new NullReferenceException();
			}
			while (flag);
		}
		return list;
	}

	public unsafe void LayoutFan(bool animate = true)
	{
		//IL_0075: Expected I, but got O
		//IL_022e: Invalid comparison between F4 and I4
		//IL_00bf: Expected I, but got O
		//IL_03c1: Expected O, but got Ref
		//IL_0250: Invalid comparison between F4 and I4
		//IL_03d7: Expected O, but got I4
		//IL_03e0: Expected O, but got I4
		//IL_02ba: Expected O, but got Ref
		//IL_02f3: Expected O, but got Ref
		//IL_012e: Expected I, but got O
		//IL_041f: Expected O, but got I
		//IL_0183: Expected I, but got O
		//IL_0454: Expected O, but got I4
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Expected O, but got Unknown
		//IL_01b0: Expected I, but got O
		//IL_0930: Invalid comparison between F4 and I4
		//IL_048f: Invalid comparison between I4 and F4
		//IL_04e8: Invalid comparison between F4 and I4
		//IL_04f9: Invalid comparison between F4 and I4
		//IL_0522: Expected O, but got I4
		//IL_04d8: Expected F4, but got I4
		//IL_055b: Expected O, but got Ref
		//IL_055b: Expected O, but got Ref
		//IL_05eb: Expected O, but got Ref
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d4: Expected O, but got Unknown
		//IL_064e: Expected O, but got Ref
		//IL_06b1: Expected O, but got Ref
		//IL_0736: Expected O, but got Ref
		//IL_0736: Expected O, but got Ref
		//IL_0736: Expected O, but got Ref
		//IL_077c: Expected O, but got Ref
		List<DraggableItem> list = new List<DraggableItem>();
		if (items != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num = 0;
			List<DraggableItem>.Enumerator enumerator = default(List<DraggableItem>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj != null;
				num = unchecked((nint)null);
				if (!flag)
				{
					continue;
				}
				if ((object)obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v445 @ stack_20_v13 (UnityEngine.Object)+30]");
					bool flag2 = (nint)0 != 0;
					num = unchecked((nint)null);
					if (flag2)
					{
						continue;
					}
					Transform transform = ((Component)obj).transform;
					if ((object)transform != null)
					{
						Transform parent = transform.parent;
						Transform transform2 = base.transform;
						bool flag3 = parent == transform2;
						num = unchecked((nint)null);
						if (!flag3)
						{
							continue;
						}
						GameObject gameObject = ((Component)obj).gameObject;
						if ((object)gameObject != null)
						{
							bool activeSelf = gameObject.activeSelf;
							bool flag4 = !activeSelf;
							num = unchecked((nint)null);
							if (flag4)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v445 @ stack_20_v13 (UnityEngine.Object)+34]");
							bool flag5 = (nint)0 != 0;
							num = unchecked((nint)null);
							if (!flag5)
							{
								if (list == null)
								{
									throw new NullReferenceException();
								}
								list.Add((DraggableItem)obj);
								num = 0;
							}
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			if (list != null)
			{
				if (list._size == 0)
				{
					return;
				}
				float num4;
				List<DraggableItem>.Enumerator enumerator2;
				if (deckWidth > 0f && maxGapBetweenCards > 0f)
				{
					float num2 = (float)list._size + 1f;
					float num3 = deckWidth / num2;
					bool flag6 = !(num3 > maxGapBetweenCards);
					num4 = 1f;
					List<DraggableItem> list2 = list;
					enumerator2 = (List<DraggableItem>.Enumerator)(&enumerator);
					if (!flag6)
					{
						bool flag7 = !debugSpacing;
						num4 = 1f;
						list2 = list;
						enumerator2 = (List<DraggableItem>.Enumerator)(&enumerator);
						if (!flag7)
						{
							string arg = base.name;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object arg2 = default(object);
							object arg3 = default(object);
							string text = $"[{arg}] Clamping width {arg2:F3} -> {arg3:F3} ";
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object arg4 = default(object);
							object arg5 = default(object);
							string text2 = $"(gap limit {arg4:F3}, natural {arg5:F3}).";
							string text3 = text + text2;
							Debug.Log(text3, this);
							num4 = 1f;
							enumerator2 = (List<DraggableItem>.Enumerator)text3;
						}
					}
				}
				else
				{
					num4 = 1f;
					enumerator2 = (List<DraggableItem>.Enumerator)(&enumerator);
				}
				if (list._size <= 0)
				{
					return;
				}
				object obj3 = default(object);
				object obj2 = obj3;
				object obj4 = 0;
				object obj5 = 0;
				float num6 = default(float);
				float num5 = num6;
				Component component = default(Component);
				Vector3 euler = default(Vector3);
				DraggableItem item = default(DraggableItem);
				object obj9 = default(object);
				float num7 = default(float);
				object obj10 = default(object);
				DraggableItem item2 = default(DraggableItem);
				Component component2 = default(Component);
				object obj11 = default(object);
				Component component3 = default(Component);
				Vector3 zeroVector = default(Vector3);
				Component component4 = default(Component);
				float x = default(float);
				float x2 = default(float);
				while (true)
				{
					if (list._size == 1)
					{
					}
					List<DraggableItem>.Enumerator enumerator3 = ((List<DraggableItem>)enumerator2).GetEnumerator();
					List<DraggableItem>.Enumerator enumerator4 = ((List<DraggableItem>)enumerator2).GetEnumerator();
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if ((object)component == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ stack_20_v10 (UnityEngine.Component)+9C]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ stack_20_v10 (UnityEngine.Component)+9C]");
					if ((nint)0 == 0)
					{
						obj6 = 0;
					}
					object obj7 = obj4 + 1;
					if (obj6 == null)
					{
						obj7 = obj4;
					}
					Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
					if (deckWidth > 0f)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm7,ebx\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,r15d\"");
						num5 = depthArcBlend;
						if (!(0f > depthArcBlend))
						{
							if (num5 > num4)
							{
								num5 = num4;
							}
						}
						else
						{
							num5 = 0f;
						}
					}
					Transform transform3;
					if (obj2 != null)
					{
						bool flag8 = layoutAnimationDuration < 0f;
						bool flag9 = layoutAnimationDuration == 0f;
						bool flag10 = !flag8;
						bool flag11 = !flag9;
						object obj8 = flag11 & flag10;
						if (obj8 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							AnimateItemToTarget(item, (Vector3)(&obj9), (Quaternion)(&num7));
							obj9 = obj10;
							transform3 = (Transform)(object)this;
							goto IL_0664;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					StopLayoutRoutine(item2);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if ((object)component2 == null)
					{
						break;
					}
					Transform transform4 = component2.transform;
					if ((object)transform4 == null)
					{
						break;
					}
					transform4.localPosition = (Vector3)(&obj11);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if ((object)component3 == null)
					{
						break;
					}
					Transform transform5 = component3.transform;
					if ((object)transform5 == null)
					{
						break;
					}
					transform5.localRotation = (Quaternion)(&num7);
					obj11 = obj10;
					transform3 = transform5;
					goto IL_0664;
					IL_0664:
					bool flag12 = !debugLayout;
					obj4 = obj7;
					enumerator2 = (List<DraggableItem>.Enumerator)transform3;
					if (!flag12)
					{
						Transform transform6 = base.transform;
						if ((object)transform6 == null)
						{
							break;
						}
						Vector3 vector = transform6.TransformPoint((Vector3)(&zeroVector));
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if ((object)component4 == null)
						{
							break;
						}
						Transform transform7 = component4.transform;
						if ((object)transform7 == null)
						{
							break;
						}
						Vector3 position = transform7.position;
						Debug.DrawLine((Vector3)(&x), (Vector3)(&x2), (Color)(&num7), 0.15f);
						float num8 = 0.15f;
						x = vector.x;
						x2 = position.x;
						zeroVector = Vector3.zeroVector;
						obj2 = obj3;
						obj4 = obj7;
						enumerator2 = (List<DraggableItem>.Enumerator)(&x);
					}
					obj5++;
					if ((nint)obj5 >= list._size)
					{
						return;
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	private void AnimateItemToTarget(DraggableItem item, Vector3 targetLocalPos, Quaternion targetLocalRot)
	{
		//IL_0024: Expected O, but got F4
		//IL_0040: Expected O, but got F4
		StopLayoutRoutine(item);
		_003CLerpToTargetRoutine_003Ed__34 obj = new _003CLerpToTargetRoutine_003Ed__34(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.item = item;
		obj.targetLocalPos = (Vector3)targetLocalPos.x;
		_ = targetLocalPos.z;
		obj.targetLocalRot = (Quaternion)targetLocalRot.x;
		Coroutine value = StartCoroutine(obj);
		_layoutRoutines.set_Item(item, value);
	}

	private void StopLayoutRoutine(DraggableItem item)
	{
		if (_layoutRoutines.TryGetValue(item, out var value) && value != null)
		{
			StopCoroutine(value);
		}
		bool flag = _layoutRoutines.Remove(item);
	}

	private IEnumerator LerpToTargetRoutine(DraggableItem item, Vector3 targetLocalPos, Quaternion targetLocalRot)
	{
		//IL_0024: Expected O, but got F4
		//IL_0040: Expected O, but got F4
		_003CLerpToTargetRoutine_003Ed__34 obj = new _003CLerpToTargetRoutine_003Ed__34(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.item = item;
		obj.targetLocalPos = (Vector3)targetLocalPos.x;
		_ = targetLocalPos.z;
		obj.targetLocalRot = (Quaternion)targetLocalRot.x;
		return obj;
	}

	private static float SmoothStep01(float t)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_005c: Expected F4, but got I4
		float num;
		if (!(0f > t))
		{
			bool flag = !(t > 1f);
			num = t;
			if (!flag)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		float num2 = num + num;
		float num3 = num * num;
		float num4 = 3f - num2;
		return num4 * num3;
	}

	public DraggableItemDeckArea()
	{
		List<DraggableItem> list = new List<DraggableItem>();
		items = list;
		_layoutRoutines = new Dictionary<DraggableItem, Coroutine>();
		base._002Ector();
	}

	static DraggableItemDeckArea()
	{
		List<DraggableItemDeckArea> allDecks = new List<DraggableItemDeckArea>();
		AllDecks = allDecks;
	}
}
