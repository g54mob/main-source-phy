using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class DraggableItemGridArea : MonoBehaviour
{
	public enum RowAxis
	{
		LocalY,
		LocalZ
	}

	public enum StackingFillMode
	{
		EvenFill,
		FillFirst
	}

	private sealed class _003CResetRoutine_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DraggableItemGridArea _003C_003E4__this;

		private List<(DraggableItem, int, int)> _003Cassignments_003E5__2;

		private int _003Cstarted_003E5__3;

		private int _003Ci_003E5__4;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CResetRoutine_003Ed__41(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0008: Expected O, but got Ref
			//IL_001c: Expected I4, but got I8
			//IL_017a: Expected I4, but got I8
			//IL_0a00: Expected I4, but got O
			//IL_0a9c: Expected O, but got I4
			//IL_01bf: Expected O, but got Ref
			//IL_01d9: Expected O, but got I
			//IL_0927: Invalid comparison between I4 and F4
			//IL_0205: Expected O, but got I
			//IL_0857: Expected I4, but got O
			//IL_0258: Expected O, but got I
			//IL_0873: Expected O, but got Ref
			//IL_0898: Expected O, but got Ref
			//IL_08f5: Expected I4, but got O
			//IL_0777: Expected I4, but got O
			//IL_0793: Expected O, but got Ref
			//IL_07b8: Expected O, but got Ref
			//IL_030d: Expected O, but got I
			//IL_07f3: Expected O, but got I
			//IL_0350: Expected O, but got I
			//IL_0364: Expected I, but got O
			//IL_0a63: Invalid comparison between I4 and F4
			//IL_0382: Expected O, but got I
			//IL_072d: Expected O, but got Ref
			//IL_074d: Expected O, but got I
			//IL_04b3: Expected O, but got Ref
			//IL_04d8: Expected O, but got Ref
			//IL_05d1: Expected O, but got Ref
			//IL_060b: Expected I4, but got O
			//IL_060b: Expected O, but got I
			//IL_0513: Expected O, but got I
			//IL_064f: Invalid comparison between F4 and I4
			//IL_052a: Expected O, but got Ref
			//IL_03e9: Expected O, but got I
			//IL_03e9: Expected O, but got I
			//IL_059e: Expected F4, but got I
			//IL_0411: Expected O, but got I
			//IL_06af: Expected O, but got I
			//IL_0478: Expected O, but got I
			//IL_0478: Expected O, but got I
			object obj2 = default(object);
			object obj = (object)(&obj2);
			DraggableItemGridArea draggableItemGridArea = _003C_003E4__this;
			Coroutine coroutine;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					List<(DraggableItem, int, int)> list = _003C_003E4__this.BuildSlotAssignments();
					_003Cassignments_003E5__2 = list;
					List<(DraggableItem, int, int)> list2 = _003Cassignments_003E5__2;
					if (_003Cassignments_003E5__2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v146 @ rax_v74 (System.Collections.Generic.List`1<System.ValueTuple`3<DraggableItem, System.Int32, System.Int32>>)+18]");
						if ((nint)0 != 0)
						{
							if (draggableItemGridArea.onResetStarted != null)
							{
								draggableItemGridArea.onResetStarted.Invoke();
							}
							draggableItemGridArea._pendingSlideCount = 0;
							_003Cstarted_003E5__3 = 0;
							coroutine = null;
							goto IL_0a2c;
						}
						if (draggableItemGridArea.debugLogs)
						{
							Debug.Log("[DraggableItemGridArea] ResetRoutine: no valid items to move.", _003C_003E4__this);
						}
						goto IL_013b;
					}
				}
				goto IL_09f2;
			}
			if (_003C_003E1__state != 1)
			{
				goto IL_013b;
			}
			_003C_003E1__state = -1;
			int num = 0;
			goto IL_0a7a;
			IL_081c:
			if ((object)_003C_003E4__this == null)
			{
				goto IL_09f2;
			}
			bool flag = !draggableItemGridArea.debugLogs;
			num = (int)coroutine;
			string message;
			if (!flag)
			{
				object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-1]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 85));
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				string text = $"[DraggableItemGridArea] Slot {arg} layer {arg2}: ";
				message = text + "item null/disabled — skipped.";
				goto IL_08de;
			}
			goto IL_0a7a;
			IL_09f2:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_09e0:
			draggableItemGridArea._resetRoutine = coroutine;
			goto IL_013b;
			IL_0a2c:
			List<(DraggableItem, int, int)> list3 = _003Cassignments_003E5__2;
			Vector3 vector;
			DraggableItem draggableItem = default(DraggableItem);
			if (_003Cassignments_003E5__2 != null)
			{
				int num2 = _003Ci_003E5__4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rax_v6 (System.Collections.Generic.List`1<System.ValueTuple`3<DraggableItem, System.Int32, System.Int32>>)+18]");
				if ((nint)num2 < (nint)0)
				{
					object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
					UnityEngine.Object obj6 = (UnityEngine.Object)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-1]");
					int stackLayer = (int)((nint)0 >> 32);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
					if (!(UnityEngine.Object)0)
					{
						goto IL_081c;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
						if (!((Behaviour)0).isActiveAndEnabled)
						{
							goto IL_081c;
						}
						if ((object)_003C_003E4__this != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r14_v5 (UnityEngine.Object)+34]");
							if ((nint)0 != 0)
							{
								bool flag2 = !draggableItemGridArea.debugLogs;
								num = (int)coroutine;
								if (!flag2)
								{
									object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-1]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									object arg3 = default(object);
									object arg4 = default(object);
									string text2 = $"[DraggableItemGridArea] Slot {arg3} layer {arg4}: ";
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
									string name = ((UnityEngine.Object)0).name;
									message = text2 + "'" + name + "' is being dragged — skipped.";
									goto IL_08de;
								}
								goto IL_0a7a;
							}
							DraggableItemGridArea draggableItemGridArea2 = _003C_003E4__this;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-1]");
							vector = draggableItemGridArea2.ComputeTargetWorldPos(0, stackLayer, draggableItem);
							float z = vector.z;
							_ = vector.z;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r14_v5 (UnityEngine.Object)+40]");
							if ((UnityEngine.Object)0 == null)
							{
								_ = draggableItemGridArea.dragSurface;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r14_v5 (UnityEngine.Object)+40]");
							bool flag3 = (UnityEngine.Object)0 != null;
							bool flag4 = !flag3;
							nint num3 = unchecked((nint)null);
							if (flag4)
							{
								goto IL_0483;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r14_v5 (UnityEngine.Object)+40]");
							object obj9 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r14_v5 (UnityEngine.Object)+40]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rax_v60+50]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rax_v60+50]");
									nint num4 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
									bool flag5 = ((List<DraggableItem>)num4).Contains((DraggableItem)0);
									num3 = 0;
									if (flag5)
									{
										goto IL_0483;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r14_v5 (UnityEngine.Object)+40]");
									object obj10 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r14_v5 (UnityEngine.Object)+40]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ rax_v62+50]");
										if ((nint)0 != 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ rax_v62+50]");
											nint num5 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
											((List<DraggableItem>)num5).Add((DraggableItem)0);
											num3 = 0;
											goto IL_0483;
										}
									}
								}
							}
						}
					}
				}
				else if ((object)_003C_003E4__this != null)
				{
					if (!(0f < draggableItemGridArea.slideDuration))
					{
						if (_003Cstarted_003E5__3 <= 0)
						{
							List<(DraggableItem, int, int)> list4 = _003Cassignments_003E5__2;
							if (_003Cassignments_003E5__2 == null)
							{
								goto IL_09f2;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ rax_v12 (System.Collections.Generic.List`1<System.ValueTuple`3<DraggableItem, System.Int32, System.Int32>>)+18]");
							if ((nint)0 <= (nint)0)
							{
								goto IL_09e0;
							}
						}
						if (draggableItemGridArea.onResetCompleted != null)
						{
							draggableItemGridArea.onResetCompleted.Invoke();
						}
					}
					goto IL_09e0;
				}
			}
			goto IL_09f2;
			IL_0483:
			if (draggableItemGridArea.debugLogs)
			{
				object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-1]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj12 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 127));
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg5 = default(object);
				object arg6 = default(object);
				string text3 = $"[DraggableItemGridArea] Slot {arg5} layer {arg6}: ";
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
				string name2 = ((UnityEngine.Object)0).name;
				object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 81));
				_ = vector.x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+67]");
				_ = 0;
				object arg7 = (Vector3)obj13;
				string text4 = $"sliding '{name2}' to {arg7}.";
				string message2 = text3 + text4;
				Debug.Log(message2, _003C_003E4__this);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+67]");
				float z = 0f;
			}
			if (0f < draggableItemGridArea.slideDuration)
			{
				int pendingSlideCount = draggableItemGridArea._pendingSlideCount + 1;
				draggableItemGridArea._pendingSlideCount = pendingSlideCount;
				Vector3 targetWorldPos = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
				_ = vector.x;
				DraggableItemGridArea draggableItemGridArea3 = _003C_003E4__this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-1]");
				IEnumerator routine = draggableItemGridArea3.SlideItemToSlot((DraggableItem)num6, targetWorldPos, 0, (int)draggableItem);
				Coroutine coroutine2 = _003C_003E4__this.StartCoroutine(routine);
				int num7 = _003Cstarted_003E5__3 + 1;
				_003Cstarted_003E5__3 = num7;
				bool flag6 = !(draggableItemGridArea.slideStaggerDelay > 0f);
				num = 0;
				if (!flag6)
				{
					List<(DraggableItem, int, int)> list5 = _003Cassignments_003E5__2;
					if (_003Cassignments_003E5__2 == null)
					{
						goto IL_09f2;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rax_v42 (System.Collections.Generic.List`1<System.ValueTuple`3<DraggableItem, System.Int32, System.Int32>>)+18]");
					object obj14 = -1;
					bool flag7 = _003Ci_003E5__4 >= (nint)obj14;
					num = 0;
					if (!flag7)
					{
						WaitForSeconds waitForSeconds = new WaitForSeconds(draggableItemGridArea.slideStaggerDelay);
						_003C_003E2__current = waitForSeconds;
						_003C_003E1__state = 1;
						return true;
					}
				}
			}
			else
			{
				_ = vector.x;
				Vector3 targetWorldPos2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
				DraggableItemGridArea draggableItemGridArea4 = _003C_003E4__this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
				draggableItemGridArea4.SnapItemToSlot((DraggableItem)0, targetWorldPos2);
				num = 0;
			}
			goto IL_0a7a;
			IL_08de:
			Debug.Log(message, _003C_003E4__this);
			num = (int)coroutine;
			goto IL_0a7a;
			IL_0a7a:
			int num8 = _003Ci_003E5__4 + 1;
			_003Ci_003E5__4 = num8;
			coroutine = (Coroutine)num;
			goto IL_0a2c;
			IL_013b:
			return false;
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

	private sealed class _003CSlideItemToSlot_003Ed__42 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DraggableItem item;

		public DraggableItemGridArea _003C_003E4__this;

		public Vector3 targetWorldPos;

		public int slotIndex;

		public int stackLayer;

		private DragSurface _003Csurf_003E5__2;

		private Vector3 _003Cstart_003E5__3;

		private float _003Celapsed_003E5__4;

		private float _003Cdur_003E5__5;

		private Vector3 _003CsurfNormal_003E5__6;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CSlideItemToSlot_003Ed__42(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_03f4: Expected I4, but got I8
			//IL_0085: Expected O, but got F4
			//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ca: Expected O, but got Unknown
			//IL_0732: Expected F4, but got O
			//IL_073b: Expected O, but got I4
			//IL_09e7: Expected I, but got O
			//IL_0a10: Expected F4, but got I
			//IL_0498: Invalid comparison between I4 and F4
			//IL_0987: Unknown result type (might be due to invalid IL or missing references)
			//IL_098c: Expected O, but got Unknown
			//IL_04e3: Expected F4, but got I4
			//IL_016f: Expected I, but got O
			//IL_0198: Expected F4, but got I
			//IL_0776: Expected F4, but got O
			//IL_077f: Expected O, but got I4
			//IL_0a9d: Expected I4, but got O
			//IL_053d: Invalid comparison between I4 and F4
			//IL_08ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b2: Expected O, but got Unknown
			//IL_0588: Expected F4, but got I4
			//IL_0272: Expected O, but got I4
			//IL_08e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ec: Expected O, but got Unknown
			//IL_0a55: Expected O, but got I
			//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d5: Expected O, but got Unknown
			//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d2: Expected O, but got Unknown
			//IL_0817: Unknown result type (might be due to invalid IL or missing references)
			//IL_081c: Expected O, but got Unknown
			//IL_0846: Expected O, but got I4
			//IL_084f: Expected O, but got I4
			//IL_0302: Unknown result type (might be due to invalid IL or missing references)
			//IL_0307: Expected O, but got Unknown
			//IL_0393: Expected O, but got I4
			//IL_039c: Expected O, but got I4
			DraggableItemGridArea draggableItemGridArea = _003C_003E4__this;
			object obj2 = default(object);
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if (!item)
				{
					goto IL_066d;
				}
				_003Csurf_003E5__2 = draggableItemGridArea.dragSurface;
				Transform transform = item.transform;
				Vector3 position = transform.position;
				_003Cstart_003E5__3 = (Vector3)position.x;
				_ = position.z;
				_003Celapsed_003E5__4 = 0f;
				bool flag = !(0.0001f < draggableItemGridArea.slideDuration);
				float num = 0.0001f;
				if (!flag)
				{
					num = draggableItemGridArea.slideDuration;
				}
				_003Cdur_003E5__5 = num;
				Vector3 vector;
				if (_003Csurf_003E5__2 != null)
				{
					Vector3 planeNormal = _003Csurf_003E5__2.GetPlaneNormal();
					_ = planeNormal.x;
					_ = planeNormal.z;
					object obj = obj2 - 80;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
					if (planeNormal.x > 1E-05f)
					{
						float num2 = planeNormal.z / planeNormal.x;
						Vector3 vector2 = default(Vector3);
						vector = vector2;
						Vector3 vector3 = vector2;
						float num3 = num2;
					}
					else
					{
						nint num4 = (nint)typeof(Vector3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1318 @ rax_v98 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num5 = 0;
						Vector3 vector3 = Vector3.zeroVector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1221 @ rcx_v75 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
						float num3 = 0f;
						vector = Vector3.zeroVector;
					}
				}
				else
				{
					nint num6 = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1150 @ rax_v92 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num7 = 0;
					vector = Vector3.upVector;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1151 @ rcx_v70 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
					float num3 = 0f;
				}
				_003CsurfNormal_003E5__6 = vector;
				DraggableItem draggableItem = item;
				if (draggableItem.surfaceRef != null)
				{
					DraggableItem draggableItem2 = item;
					if (draggableItem2.surfaceRef != _003Csurf_003E5__2)
					{
						DraggableItem draggableItem3 = item;
						draggableItem3.surfaceRef.RemoveItem(item);
					}
				}
				bool flag2 = _003Csurf_003E5__2 != null;
				bool flag3 = !flag2;
				object obj3 = 0;
				if (!flag3)
				{
					Transform transform2 = item.transform;
					Transform transform3 = _003Csurf_003E5__2.transform;
					transform2.SetParent(transform3, worldPositionStays: true);
					Transform transform4 = item.transform;
					Transform transform5 = _003Csurf_003E5__2.transform;
					Quaternion rotation = transform5.rotation;
					Quaternion rotation2 = (Quaternion)(obj2 - 64);
					_ = rotation.x;
					transform4.rotation = rotation2;
					item.ApplySurfaceScaleForSurface(_003Csurf_003E5__2);
					DraggableItem draggableItem4 = item;
					draggableItem4.surfaceRef = _003Csurf_003E5__2;
					DraggableItem draggableItem5 = item;
					draggableItem5.CurrentLocation = DraggableItem.ItemLocation.Surface;
					_003Csurf_003E5__2.AddItem(item);
					object obj4 = 0;
					obj3 = 0;
				}
				DraggableItem draggableItem6 = item;
				draggableItem6.IsSliding = true;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_066d;
				}
				_003C_003E1__state = -1;
			}
			if (_003Cdur_003E5__5 > _003Celapsed_003E5__4)
			{
				if ((bool)item)
				{
					DraggableItem draggableItem7 = item;
					if (!draggableItem7.IsBeingDragged)
					{
						float deltaTime = Time.deltaTime;
						float num8 = (_003Celapsed_003E5__4 = deltaTime + _003Celapsed_003E5__4) / _003Cdur_003E5__5;
						if (!(0f > num8))
						{
							if (num8 > 1f)
							{
								num8 = 1f;
							}
						}
						else
						{
							num8 = 0f;
						}
						Transform transform6 = item.transform;
						_ = targetWorldPos;
						_ = _003Cstart_003E5__3;
						float num9 = 1f - num8;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
						float num10 = 1f - num9;
						if (!(0f > num10))
						{
							if (num10 > 1f)
							{
								num10 = 1f;
							}
						}
						else
						{
							num10 = 0f;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItemGridArea+<SlideItemToSlot>d__42)+38]");
						nint num11 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItemGridArea+<SlideItemToSlot>d__42)+58]");
						object obj5 = num11 - 0;
						float num12 = (float)obj5 * num10;
						float num13 = num12;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItemGridArea+<SlideItemToSlot>d__42)+58]");
						float num14 = num13 + 0f;
						DraggableItem draggableItem8 = item;
						_ = _003CsurfNormal_003E5__6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItemGridArea+<SlideItemToSlot>d__42)+6C]");
						float num15 = 0f * draggableItem8.ejectSlideLift;
						float num16 = num15 + num14;
						Vector3 position2 = (Vector3)(obj2 - 64);
						transform6.position = position2;
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
				}
				if ((bool)item)
				{
					DraggableItem draggableItem9 = item;
					draggableItem9.IsSliding = false;
				}
			}
			else if ((bool)item)
			{
				Transform transform7 = item.transform;
				Vector3 position3 = (Vector3)(obj2 - 64);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItemGridArea+<SlideItemToSlot>d__42)+38]");
				_ = 0;
				_ = targetWorldPos;
				transform7.position = position3;
				DraggableItem draggableItem10 = item;
				draggableItem10.IsSliding = false;
				bool flag4 = _003Csurf_003E5__2 != null;
				bool flag5 = !flag4;
				float num17 = (float)targetWorldPos;
				object obj6 = 0;
				if (!flag5)
				{
					DragSurface dragSurface = _003Csurf_003E5__2;
					bool flag6 = !dragSurface.clampToBounds;
					num17 = (float)targetWorldPos;
					obj6 = 0;
					if (!flag6)
					{
						Transform transform8 = item.transform;
						Transform transform9 = item.transform;
						Vector3 position4 = transform9.position;
						Vector3 worldPos = (Vector3)(obj2 - 64);
						_ = position4.x;
						_ = position4.z;
						Vector3 vector4 = _003Csurf_003E5__2.ClampOnSurfacePreserveNormalOffset(worldPos);
						num17 = vector4.x;
						Vector3 position5 = (Vector3)(obj2 - 64);
						_ = vector4.x;
						_ = vector4.z;
						transform8.position = position5;
						object obj4 = 0;
						obj6 = 0;
					}
				}
				if (draggableItemGridArea.debugLogs)
				{
					if ((object)item == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					string name = item.name;
					object obj7 = obj2 + 32;
					_ = slotIndex;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string text = $"[DraggableItemGridArea] '{name}' arrived at slot {arg} ";
					object obj8 = obj2 + 48;
					_ = stackLayer;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg2 = default(object);
					string text2 = $"layer {arg2}.";
					string message = text + text2;
					Debug.Log(message, _003C_003E4__this);
				}
			}
			_003C_003E4__this.DecrementPendingAndCheckCompletion();
			goto IL_066d;
			IL_066d:
			return false;
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

	private DragSurface dragSurface;

	private int columnCount;

	private int rowCount;

	private float cellWidth;

	private float cellHeight;

	private RowAxis rowAxis;

	private Vector3 gridOriginLocalOffset;

	private int maxStackDepth;

	private StackingFillMode stackingFillMode;

	private List<DraggableItem> gridItems;

	private bool compactNullEntries;

	private string taggedItemsTag;

	private bool skipDuplicateTaggedItems;

	private bool skipInactiveTaggedItems;

	private float slideDuration;

	private float slideStaggerDelay;

	private DraggableItem.EjectAxis slideEjectAxis;

	private bool resetOnStart;

	private bool overwriteOccupiedSlots;

	private UnityEvent onResetStarted;

	private UnityEvent onResetCompleted;

	private bool drawGizmos;

	private bool drawGizmosWhenNotSelected;

	private Color gizmoColor;

	private float gizmoSphereRadius;

	private bool drawSlotIndexLabels;

	private float gizmoNormalLift;

	private bool debugLogs;

	private Coroutine _resetRoutine;

	private int _pendingSlideCount;

	private void Awake()
	{
		EnsureSurfaceReference();
	}

	private void Start()
	{
		if (!resetOnStart)
		{
			return;
		}
		if ((bool)dragSurface)
		{
			CollectTaggedItems();
			if (_resetRoutine != null)
			{
				StopCoroutine(_resetRoutine);
				_resetRoutine = null;
			}
			_003CResetRoutine_003Ed__41 obj = new _003CResetRoutine_003Ed__41(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine resetRoutine = StartCoroutine(obj);
			_resetRoutine = resetRoutine;
		}
		else
		{
			Debug.LogWarning("[DraggableItemGridArea] ResetAllToGrid: no DragSurface assigned.", this);
		}
	}

	private void OnValidate()
	{
		//IL_01ac: Invalid comparison between I4 and F4
		//IL_01be: Expected F4, but got I4
		//IL_0102: Invalid comparison between I4 and F4
		//IL_0114: Expected F4, but got I4
		//IL_01e1: Invalid comparison between I4 and F4
		//IL_01f3: Expected F4, but got I4
		//IL_0137: Invalid comparison between I4 and F4
		//IL_0149: Expected F4, but got I4
		//IL_0216: Invalid comparison between I4 and F4
		//IL_0228: Expected F4, but got I4
		//IL_016c: Invalid comparison between I4 and F4
		//IL_017e: Expected F4, but got I4
		EnsureSurfaceReference();
		int num = columnCount;
		if (columnCount < 1)
		{
			num = 1;
		}
		columnCount = num;
		int num2 = rowCount;
		if (rowCount < 1)
		{
			num2 = 1;
		}
		rowCount = num2;
		bool flag = !(0f < cellWidth);
		float num3 = 0f;
		if (!flag)
		{
			num3 = cellWidth;
		}
		cellWidth = num3;
		bool flag2 = !(0f < cellHeight);
		float num4 = 0f;
		if (!flag2)
		{
			num4 = cellHeight;
		}
		cellHeight = num4;
		bool flag3 = !(0f < slideDuration);
		float num5 = 0f;
		if (!flag3)
		{
			num5 = slideDuration;
		}
		slideDuration = num5;
		bool flag4 = !(0f < slideStaggerDelay);
		float num6 = 0f;
		if (!flag4)
		{
			num6 = slideStaggerDelay;
		}
		slideStaggerDelay = num6;
		bool flag5 = !(0f < gizmoSphereRadius);
		float num7 = 0f;
		if (!flag5)
		{
			num7 = gizmoSphereRadius;
		}
		gizmoSphereRadius = num7;
		bool flag6 = !(0f < gizmoNormalLift);
		float num8 = 0f;
		if (!flag6)
		{
			num8 = gizmoNormalLift;
		}
		gizmoNormalLift = num8;
		bool flag7 = maxStackDepth < 0;
		int num9 = 0;
		if (!flag7)
		{
			num9 = maxStackDepth;
		}
		maxStackDepth = num9;
	}

	private void EnsureSurfaceReference()
	{
		if (!this.dragSurface)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DragSurface dragSurface = default(DragSurface);
			this.dragSurface = dragSurface;
		}
	}

	public void ResetAllToGrid()
	{
		if ((bool)dragSurface)
		{
			CollectTaggedItems();
			if (_resetRoutine != null)
			{
				StopCoroutine(_resetRoutine);
				_resetRoutine = null;
			}
			_003CResetRoutine_003Ed__41 obj = new _003CResetRoutine_003Ed__41(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine resetRoutine = StartCoroutine(obj);
			_resetRoutine = resetRoutine;
		}
		else
		{
			Debug.LogWarning("[DraggableItemGridArea] ResetAllToGrid: no DragSurface assigned.", this);
		}
	}

	public void AddGridItem(DraggableItem item)
	{
		if (item != null && !gridItems.Contains(item))
		{
			gridItems.Add(item);
		}
	}

	public void RemoveGridItem(DraggableItem item)
	{
		bool flag = gridItems.Remove(item);
	}

	public unsafe bool TryGetSlotWorldPosition(int slotIndex, out Vector3 worldPosition, int stackLayer = 0)
	{
		//IL_00ce: Expected O, but got I4
		//IL_0088: Expected Ref, but got F4
		ref Vector3 reference = ref *(Vector3*)null;
		_ = 0;
		object obj = rowCount * columnCount;
		if ((bool)dragSurface && slotIndex >= 0 && slotIndex < (nint)obj)
		{
			Vector3 vector = ComputeSlotWorldPosition(slotIndex, stackLayer);
			reference = ref *(Vector3*)vector.x;
			_ = vector.z;
			return true;
		}
		return false;
	}

	private void CollectTaggedItems()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0030: Expected O, but got I4
		//IL_0039: Expected O, but got I4
		//IL_0042: Expected O, but got I4
		//IL_0064: Expected I, but got O
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected O, but got Unknown
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Expected O, but got Unknown
		//IL_00db: Expected I, but got O
		//IL_00ad: Expected I, but got O
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_0212: Expected I, but got O
		if (string.IsNullOrEmpty(taggedItemsTag))
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag(taggedItemsTag);
		object obj = array + 32;
		object obj2 = 0;
		object obj3 = 0;
		for (object obj4 = 0; (nint)obj4 < array.Length; obj2++, obj += 8, obj4 = obj2)
		{
			bool flag = (UnityEngine.Object)obj == null;
			nint num = unchecked((nint)null);
			if (flag)
			{
				continue;
			}
			if (skipInactiveTaggedItems != flag)
			{
				bool activeInHierarchy = ((GameObject)obj).activeInHierarchy;
				bool flag2 = !activeInHierarchy;
				num = unchecked((nint)null);
				if (flag2)
				{
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			bool flag3 = this == null;
			num = unchecked((nint)null);
			if (flag3)
			{
				continue;
			}
			if (skipDuplicateTaggedItems != flag3)
			{
				bool flag4 = gridItems.Contains((DraggableItem)(object)this);
				num = 0;
				if (flag4)
				{
					continue;
				}
			}
			gridItems.Add((DraggableItem)(object)this);
			obj3++;
			bool flag5 = !debugLogs;
			num = 0;
			if (!flag5)
			{
				string text = ((UnityEngine.Object)obj).name;
				string message = "[DraggableItemGridArea] CollectTaggedItems: added '" + text + "' (tag='" + taggedItemsTag + "') to gridItems.";
				Debug.Log(message, this);
				num = unchecked((nint)null);
			}
		}
		if (debugLogs && (nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string text2 = $"[DraggableItemGridArea] CollectTaggedItems: collected {arg} item(s) ";
			string message2 = text2 + "via tag '" + taggedItemsTag + "'.";
			Debug.Log(message2, this);
		}
	}

	private IEnumerator ResetRoutine()
	{
		_003CResetRoutine_003Ed__41 obj = new _003CResetRoutine_003Ed__41(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator SlideItemToSlot(DraggableItem item, Vector3 targetWorldPos, int slotIndex, int stackLayer)
	{
		//IL_0024: Expected O, but got F4
		_003CSlideItemToSlot_003Ed__42 obj = new _003CSlideItemToSlot_003Ed__42(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.item = item;
		obj.targetWorldPos = (Vector3)targetWorldPos.x;
		_ = targetWorldPos.z;
		int stackLayer2 = default(int);
		obj.stackLayer = stackLayer2;
		obj.slotIndex = slotIndex;
		return obj;
	}

	private unsafe void SnapItemToSlot(DraggableItem item, Vector3 targetWorldPos)
	{
		//IL_01b3: Expected O, but got Ref
		//IL_0149: Expected O, but got Ref
		//IL_0242: Expected O, but got Ref
		//IL_0258: Expected O, but got Ref
		UnityEngine.Object obj = dragSurface;
		if (item.surfaceRef != null && item.surfaceRef != dragSurface)
		{
			DragSurface surfaceRef = item.surfaceRef;
			bool flag = surfaceRef.items.Remove(item);
		}
		if (dragSurface != null)
		{
			Transform transform = item.transform;
			Transform parent = dragSurface.transform;
			transform.SetParent(parent, worldPositionStays: true);
			Transform transform2 = item.transform;
			Transform transform3 = dragSurface.transform;
			Quaternion rotation = transform3.rotation;
			object obj2 = default(object);
			transform2.rotation = (Quaternion)(&obj2);
			item.ApplySurfaceScaleForSurface(dragSurface);
			item.surfaceRef = dragSurface;
			item.CurrentLocation = DraggableItem.ItemLocation.Surface;
			dragSurface.AddItem(item);
		}
		Transform transform4 = item.transform;
		float num = default(float);
		transform4.position = (Vector3)(&num);
		if (dragSurface != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rdi_v2 (UnityEngine.Object)+30]");
			if ((nint)0 != 0)
			{
				Transform transform5 = item.transform;
				Transform transform6 = item.transform;
				Vector3 position = transform6.position;
				Vector3 vector = dragSurface.ClampOnSurfacePreserveNormalOffset((Vector3)(&num));
				transform5.position = (Vector3)(&num);
			}
		}
	}

	private void DecrementPendingAndCheckCompletion()
	{
		if (--_pendingSlideCount <= 0)
		{
			_pendingSlideCount = 0;
			if (onResetCompleted != null)
			{
				onResetCompleted.Invoke();
			}
			if (debugLogs)
			{
				Debug.Log("[DraggableItemGridArea] All slides complete — onResetCompleted fired.", this);
			}
		}
	}

	private unsafe List<(DraggableItem, int, int)> BuildSlotAssignments()
	{
		//IL_09c0: Expected O, but got I4
		//IL_0108: Expected O, but got Ref
		//IL_0131: Expected O, but got Ref
		//IL_0544: Expected O, but got I4
		//IL_054c: Expected O, but got I4
		//IL_057b: Expected I, but got O
		//IL_0aeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af0: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_025d: Expected O, but got Ref
		//IL_07d7: Expected I, but got O
		//IL_07dc: Expected I, but got O
		//IL_0278: Expected O, but got Ref
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Expected O, but got Unknown
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_087d: Expected O, but got Unknown
		//IL_0886: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Expected O, but got Unknown
		//IL_02f2: Expected O, but got I4
		//IL_0826: Expected O, but got Ref
		//IL_0349: Expected O, but got I4
		List<(DraggableItem, int, int)> list = new List<(DraggableItem, int, int)>();
		List<(DraggableItem, int, int)> list2 = (List<(DraggableItem, int, int)>)(rowCount * columnCount);
		int size;
		if (maxStackDepth == 0)
		{
			List<DraggableItem> list3 = gridItems;
			bool flag = gridItems == null;
			List<(DraggableItem, int, int)> list4 = list;
			if (flag)
			{
				goto IL_0911;
			}
			size = list3._size;
		}
		else
		{
			size = maxStackDepth;
		}
		UnityEngine.Object obj = default(UnityEngine.Object);
		List<(DraggableItem, int, int)> list6 = default(List<(DraggableItem, int, int)>);
		if (stackingFillMode == StackingFillMode.FillFirst || size == 1)
		{
			int[] array = new int[(object)list2];
			List<(DraggableItem, int, int)> list4 = (List<(DraggableItem, int, int)>)(object)gridItems;
			if (gridItems != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<(DraggableItem, int, int)> list5 = null;
				List<DraggableItem>.Enumerator enumerator = default(List<DraggableItem>.Enumerator);
				List<DraggableItem>.Enumerator enumerator2 = default(List<DraggableItem>.Enumerator);
				List<DraggableItem>.Enumerator enumerator3 = default(List<DraggableItem>.Enumerator);
				while (true)
				{
					if (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						while (System.Runtime.CompilerServices.Unsafe.As<List<(DraggableItem, int, int)>, UIntPtr>(ref list5) < System.Runtime.CompilerServices.Unsafe.As<List<(DraggableItem, int, int)>, UIntPtr>(ref list2))
						{
							if (size > 0)
							{
								bool flag2 = array == null;
								list4 = (List<(DraggableItem, int, int)>)(&enumerator);
								if (flag2)
								{
									throw new NullReferenceException();
								}
								bool flag3 = (nint)list5 >= array.Length;
								list4 = (List<(DraggableItem, int, int)>)(&enumerator);
								if (flag3)
								{
									throw new IndexOutOfRangeException();
								}
								if (array[(object)list5] >= size)
								{
									list5 = (List<(DraggableItem, int, int)>)(list5 + 1);
									continue;
								}
							}
							goto IL_017c;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
					}
					else
					{
						enumerator.Dispose();
					}
					break;
					IL_017c:
					bool flag4 = obj != null;
					bool flag5 = !flag4;
					UnityEngine.Object obj2 = obj;
					List<(DraggableItem, int, int)> list7;
					if (!flag5)
					{
						bool flag6 = (object)obj == null;
						list4 = (List<(DraggableItem, int, int)>)(object)obj;
						if (flag6)
						{
							throw new NullReferenceException();
						}
						bool flag7 = ((Behaviour)obj).isActiveAndEnabled;
						bool flag8 = !flag7;
						obj2 = obj;
						if (!flag8)
						{
							if (array != null)
							{
								if ((nint)list5 < array.Length)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
									bool flag9 = list == null;
									list4 = (List<(DraggableItem, int, int)>)(&enumerator2);
									if (!flag9)
									{
										list.Add(((DraggableItem, int, int))(&enumerator3));
										if ((nint)list5 < array.Length)
										{
											int num = array[(object)list5] + 1;
											array[(object)list5] = num;
											bool flag10 = size <= 0;
											nint num2 = (nint)(&list6);
											nint num3 = 0;
											list6 = (List<(DraggableItem, int, int)>)array[(object)list5];
											enumerator3 = enumerator2;
											if (flag10)
											{
												continue;
											}
											if ((nint)list5 < array.Length)
											{
												num2 = (nint)(&list6);
												num3 = 0;
												list6 = (List<(DraggableItem, int, int)>)array[(object)list5];
												enumerator3 = enumerator2;
												list7 = list5;
												goto IL_043b;
											}
											throw new IndexOutOfRangeException();
										}
										throw new IndexOutOfRangeException();
									}
									throw new NullReferenceException();
								}
								throw new IndexOutOfRangeException();
							}
							throw new NullReferenceException();
						}
					}
					if (compactNullEntries)
					{
						continue;
					}
					bool flag11 = array == null;
					list4 = (List<(DraggableItem, int, int)>)(object)obj2;
					if (!flag11)
					{
						if ((nint)list5 < array.Length)
						{
							int num4 = array[(object)list5] + 1;
							array[(object)list5] = num4;
							if (size <= 0)
							{
								continue;
							}
							bool flag12 = (nint)list5 >= array.Length;
							list7 = list5;
							if (!flag12)
							{
								goto IL_043b;
							}
							throw new IndexOutOfRangeException();
						}
						throw new IndexOutOfRangeException();
					}
					throw new NullReferenceException();
					IL_043b:
					if (array[(object)list7] >= size)
					{
						list5 = (List<(DraggableItem, int, int)>)(list5 + 1);
					}
				}
				goto IL_090c;
			}
		}
		else
		{
			List<DraggableItem> list8 = new List<DraggableItem>();
			List<bool> list9 = new List<bool>();
			list9._002Ector();
			List<(DraggableItem, int, int)> list4 = (List<(DraggableItem, int, int)>)(object)gridItems;
			if (gridItems != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				nint num5 = 0;
				DraggableItemGridArea draggableItemGridArea = this;
				List<DraggableItem>.Enumerator enumerator4 = default(List<DraggableItem>.Enumerator);
				DraggableItemGridArea draggableItemGridArea3 = default(DraggableItemGridArea);
				while (enumerator4.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					DraggableItemGridArea draggableItemGridArea2;
					if (!(obj != null))
					{
						draggableItemGridArea = null;
					}
					else
					{
						if ((object)obj == null)
						{
							throw new NullReferenceException();
						}
						bool flag13 = ((Behaviour)obj).isActiveAndEnabled;
						draggableItemGridArea = (DraggableItemGridArea)flag13;
						draggableItemGridArea2 = (DraggableItemGridArea)flag13;
						if (flag13)
						{
							goto IL_0589;
						}
					}
					bool flag14 = compactNullEntries;
					draggableItemGridArea2 = draggableItemGridArea;
					num5 = unchecked((nint)null);
					if (flag14)
					{
						continue;
					}
					goto IL_0589;
					IL_0589:
					bool flag15 = (object)draggableItemGridArea2 != null;
					List<(DraggableItem, int, int)> item = (List<(DraggableItem, int, int)>)(object)obj;
					if (!flag15)
					{
						item = null;
					}
					if (list8 != null)
					{
						list8.Add((DraggableItem)(object)item);
						if (list9 != null)
						{
							list9.Add((byte)(&draggableItemGridArea3) != 0);
							num5 = 0;
							draggableItemGridArea = draggableItemGridArea2;
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				enumerator4.Dispose();
				if (size > 0)
				{
					bool flag16 = list8 == null;
					list6 = null;
					List<(DraggableItem, int, int)> list10 = null;
					List<DraggableItem> list11 = list8;
					List<DraggableItem>.Enumerator enumerator6 = default(List<DraggableItem>.Enumerator);
					List<DraggableItem>.Enumerator enumerator5 = enumerator6;
					List<(DraggableItem, int, int)> list12 = null;
					List<(DraggableItem, int, int)> list13 = list2;
					nint num6 = 0;
					List<(DraggableItem, int, int)> list14 = null;
					List<(DraggableItem, int, int)> list15 = null;
					List<(DraggableItem, int, int)> list16 = null;
					list4 = null;
					if (flag16)
					{
						goto IL_0911;
					}
					nint num3 = default(nint);
					nint num2 = default(nint);
					List<DraggableItem>.Enumerator enumerator9 = default(List<DraggableItem>.Enumerator);
					List<DraggableItem>.Enumerator enumerator10 = default(List<DraggableItem>.Enumerator);
					while ((nint)list12 < list8._size)
					{
						bool flag17 = (nint)list2 <= 0;
						nint num7 = num3;
						List<(DraggableItem, int, int)> list17 = null;
						IntPtr intPtr = num2;
						List<(DraggableItem, int, int)> list18 = list10;
						List<DraggableItem>.Enumerator enumerator7 = enumerator5;
						List<DraggableItem>.Enumerator enumerator8 = enumerator6;
						IntPtr intPtr2 = num5;
						List<(DraggableItem, int, int)> list19 = list12;
						IntPtr intPtr3 = num6;
						List<(DraggableItem, int, int)> list20 = list14;
						DraggableItemGridArea draggableItemGridArea4 = draggableItemGridArea;
						if (!flag17)
						{
							bool flag20;
							do
							{
								bool flag18 = (nint)list12 >= list8._size;
								intPtr = num2;
								num3 = num7;
								list18 = list10;
								enumerator7 = enumerator5;
								enumerator8 = enumerator6;
								intPtr2 = num5;
								list19 = list12;
								intPtr3 = num6;
								list20 = list14;
								draggableItemGridArea4 = draggableItemGridArea;
								if (flag18)
								{
									break;
								}
								object obj3 = (object)list14 + (object)list17;
								if ((nint)obj3 >= list8._size)
								{
									goto end_IL_0671;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								bool flag19 = (UnityEngine.Object)(object)list8 != null;
								num2 = 0;
								num5 = unchecked((nint)null);
								num6 = unchecked((nint)null);
								draggableItemGridArea = (DraggableItemGridArea)(object)list8;
								if (flag19)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
									if (list == null)
									{
										return (List<(DraggableItem, int, int)>)(object)new NullReferenceException();
									}
									list.Add(((DraggableItem, int, int))(&enumerator9));
									num2 = (nint)(&list13);
									num7 = 0;
									list10 = (List<(DraggableItem, int, int)>)enumerator10;
									enumerator5 = enumerator10;
									enumerator6 = enumerator10;
									num5 = 0;
									list13 = list16;
									num6 = (nint)(&enumerator9);
									draggableItemGridArea = (DraggableItemGridArea)(object)list;
								}
								list17 = (List<(DraggableItem, int, int)>)(list17 + 1);
								list12 = (List<(DraggableItem, int, int)>)(list12 + 1);
								flag20 = System.Runtime.CompilerServices.Unsafe.As<List<(DraggableItem, int, int)>, UIntPtr>(ref list17) < System.Runtime.CompilerServices.Unsafe.As<List<(DraggableItem, int, int)>, UIntPtr>(ref list2);
								list14 = list6;
								intPtr = num2;
								num3 = num7;
								list18 = list10;
								enumerator7 = enumerator5;
								enumerator8 = enumerator6;
								intPtr2 = num5;
								list19 = list12;
								intPtr3 = num6;
								list20 = list6;
								draggableItemGridArea4 = draggableItemGridArea;
							}
							while (flag20);
							list15 = list16;
						}
						list15 = (List<(DraggableItem, int, int)>)(list15 + 1);
						list14 = (List<(DraggableItem, int, int)>)(object)((object)list20 + (object)list2);
						bool flag21 = (nint)list15 < size;
						num2 = intPtr;
						list6 = list14;
						list10 = list18;
						enumerator5 = enumerator7;
						enumerator6 = enumerator8;
						num5 = intPtr2;
						list12 = list19;
						num6 = intPtr3;
						draggableItemGridArea = draggableItemGridArea4;
						list16 = list15;
						if (!flag21)
						{
							break;
						}
						continue;
						end_IL_0671:
						break;
					}
				}
				goto IL_090c;
			}
		}
		goto IL_0911;
		IL_090c:
		return list;
		IL_0911:
		throw new NullReferenceException();
	}

	private unsafe Vector3 ComputeSlotWorldPosition(int slotIndex, int stackLayer = 0)
	{
		//IL_032e: Expected O, but got I4
		//IL_0337: Expected native int or pointer, but got O
		//IL_0345: Expected native int or pointer, but got O
		//IL_035a: Expected F4, but got I
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Expected O, but got Unknown
		//IL_03a4: Expected O, but got I4
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Expected O, but got Unknown
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Expected O, but got Unknown
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Expected O, but got Unknown
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Expected O, but got Unknown
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00d6: Expected native int or pointer, but got O
		//IL_00e8: Expected native int or pointer, but got O
		//IL_0284: Expected O, but got I4
		//IL_01ce: Expected O, but got I4
		//IL_023d: Expected O, but got I4
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Expected O, but got Unknown
		//IL_02e5: Expected I, but got O
		//IL_0305: Expected F4, but got I
		//IL_051f: Expected native int or pointer, but got O
		//IL_052c: Expected native int or pointer, but got O
		object obj = rowCount - 1;
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = 0f;
		((Vector3*)(nint)vector)->z = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (DraggableItemGridArea)+44]");
		float num = 0f;
		int num2 = slotIndex / columnCount;
		int num3 = slotIndex % columnCount;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (DraggableItemGridArea)+44]");
		_ = 0;
		object obj2 = obj * cellHeight;
		object obj3 = columnCount - 1;
		_ = gridOriginLocalOffset;
		object obj4 = num2 * cellHeight;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj5 = obj2 ^ 0;
		float num4 = (float)obj5 * 0.5f;
		float num5 = num4 + (float)obj4;
		object obj6 = obj3 * cellWidth;
		object obj7 = num3 * cellWidth;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj8 = obj6 ^ 0;
		float num6 = (float)obj8 * 0.5f;
		float num7 = num6 + (float)obj7;
		float num8 = num7 + (float)gridOriginLocalOffset;
		if (rowAxis == RowAxis.LocalY)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-1C]");
			float num9 = 0f - num5;
		}
		else if (rowAxis == RowAxis.LocalZ)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-18]");
			float num10 = 0f + num5;
			num = num10;
		}
		object obj9 = default(object);
		Vector3 vector3;
		if ((object)dragSurface != null)
		{
			Transform transform = dragSurface.transform;
			if ((object)transform != null)
			{
				Vector3 position = (Vector3)(obj9 - 32);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-20]");
				_ = 0;
				Vector3 vector2 = transform.TransformPoint(position);
				((Vector3*)(nint)vector)->x = vector2.x;
				((Vector3*)(nint)vector)->z = vector2.z;
				if (stackLayer <= 0 || !(dragSurface != null))
				{
					goto IL_0497;
				}
				Component component = dragSurface;
				if ((object)dragSurface != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v141 @ rcx_v10 (UnityEngine.Component)+20]");
					if ((nint)0 == 1)
					{
						Transform transform2 = dragSurface.transform;
						if ((object)transform2 != null)
						{
							vector3 = transform2.forward;
							object obj10 = 0;
							goto IL_0289;
						}
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v141 @ rcx_v10 (UnityEngine.Component)+20]");
						if ((nint)0 == 2)
						{
							Transform transform3 = dragSurface.transform;
							if ((object)transform3 != null)
							{
								vector3 = transform3.right;
								object obj10 = 0;
								goto IL_0289;
							}
						}
						else
						{
							Transform transform4 = dragSurface.transform;
							if ((object)transform4 != null)
							{
								vector3 = transform4.up;
								object obj10 = 0;
								goto IL_0289;
							}
						}
					}
				}
			}
		}
		return (Vector3)new NullReferenceException();
		IL_0289:
		_ = vector3.x;
		_ = vector3.x;
		_ = vector3.z;
		object obj11 = obj9 - 16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num12;
		if (vector3.x > 1E-05f)
		{
			float num11 = vector3.z / vector3.x;
			num12 = num11;
		}
		else
		{
			nint num13 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v22 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v432 @ rcx_v15 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			num12 = 0f;
			_ = Vector3.zeroVector;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm5,r14d\"");
		float num15 = 0f * num12;
		float num16 = num15 * 0.002f;
		float z = num16 + vector.z;
		float x = default(float);
		((Vector3*)(nint)vector)->x = x;
		((Vector3*)(nint)vector)->z = z;
		goto IL_0497;
		IL_0497:
		return vector;
	}

	private unsafe Vector3 ComputeSlotWorldPosition(int slotIndex, int stackLayer, DraggableItem item)
	{
		//IL_0144: Expected native int or pointer, but got O
		//IL_0156: Expected native int or pointer, but got O
		//IL_00b0: Expected I, but got O
		//IL_00d0: Expected F4, but got I
		//IL_00f5: Expected O, but got I
		//IL_0120: Expected native int or pointer, but got O
		//IL_012d: Expected native int or pointer, but got O
		Vector3 vector = ComputeSlotWorldPosition(slotIndex);
		UnityEngine.Object obj = default(UnityEngine.Object);
		Vector3 vector2 = default(Vector3);
		if (stackLayer > 0 && obj != null && dragSurface != null)
		{
			if ((object)dragSurface != null)
			{
				Vector3 planeNormal = dragSurface.GetPlaneNormal();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				float num2;
				if (planeNormal.x > 1E-05f)
				{
					float num = planeNormal.z / planeNormal.x;
					num2 = num;
				}
				else
				{
					nint num3 = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rax_v16 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ rcx_v14 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
					num2 = 0f;
				}
				if ((object)obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm4,r15d\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ stack_28 (UnityEngine.Object)+A0]");
					object obj2 = (nint)0 * (nint)0;
					float num5 = (float)obj2 * num2;
					float z = num5 + vector.z;
					float x = default(float);
					((Vector3*)(nint)vector2)->x = x;
					((Vector3*)(nint)vector2)->z = z;
					goto IL_01ed;
				}
			}
			return (Vector3)new NullReferenceException();
		}
		((Vector3*)(nint)vector2)->x = vector.x;
		((Vector3*)(nint)vector2)->z = vector.z;
		goto IL_01ed;
		IL_01ed:
		return vector2;
	}

	private unsafe Vector3 ComputeTargetWorldPos(int slotIndex, int stackLayer, DraggableItem item)
	{
		//IL_01dc: Expected native int or pointer, but got O
		//IL_01e9: Expected native int or pointer, but got O
		//IL_00b0: Expected I, but got O
		//IL_00d0: Expected F4, but got I
		//IL_00f5: Expected O, but got I
		Vector3 vector = ComputeSlotWorldPosition(slotIndex);
		float num = vector.z;
		UnityEngine.Object obj = default(UnityEngine.Object);
		float x;
		if (stackLayer > 0 && obj != null && dragSurface != null)
		{
			if ((object)dragSurface != null)
			{
				Vector3 planeNormal = dragSurface.GetPlaneNormal();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				float num3;
				if (planeNormal.x > 1E-05f)
				{
					float num2 = planeNormal.z / planeNormal.x;
					num3 = num2;
				}
				else
				{
					nint num4 = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v362 @ rax_v16 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v363 @ rcx_v14 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
					num3 = 0f;
				}
				if ((object)obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm4,r15d\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ stack_28 (UnityEngine.Object)+A0]");
					object obj2 = (nint)0 * (nint)0;
					float num6 = (float)obj2 * num3;
					float num7 = num6 + num;
					float num8 = default(float);
					x = num8;
					num = num7;
					goto IL_01d4;
				}
			}
			return (Vector3)new NullReferenceException();
		}
		x = vector.x;
		goto IL_01d4;
		IL_01d4:
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = x;
		((Vector3*)(nint)vector2)->z = num;
		return vector2;
	}

	private void OnDrawGizmos()
	{
		if (drawGizmos && drawGizmosWhenNotSelected)
		{
			DrawGizmosInternal();
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (drawGizmos)
		{
			DrawGizmosInternal();
		}
	}

	private unsafe void DrawGizmosInternal()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0046: Expected O, but got I4
		//IL_0127: Expected O, but got I4
		//IL_00a5: Expected O, but got I4
		//IL_00fa: Expected O, but got I4
		//IL_016d: Expected I, but got O
		//IL_018d: Expected F4, but got I
		//IL_049c: Expected O, but got Ref
		//IL_04ce: Expected F4, but got O
		//IL_0152: Expected O, but got F4
		//IL_01cd: Expected O, but got Ref
		//IL_021e: Expected O, but got I4
		//IL_02c6: Expected O, but got I4
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Expected I4, but got Unknown
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Expected I4, but got Unknown
		//IL_031b: Expected O, but got Ref
		//IL_031b: Expected O, but got Ref
		//IL_0328: Expected O, but got Ref
		//IL_0328: Expected O, but got Ref
		//IL_052a: Expected O, but got Ref
		//IL_033a: Expected O, but got Ref
		//IL_033a: Expected O, but got Ref
		//IL_0347: Expected O, but got Ref
		//IL_0347: Expected O, but got Ref
		//IL_035c: Expected F4, but got I
		//IL_0377: Expected O, but got I4
		//IL_058c: Expected O, but got Ref
		//IL_05bb: Expected O, but got I4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		EnsureSurfaceReference();
		if (!dragSurface)
		{
			return;
		}
		object obj3 = rowCount * columnCount;
		Component component = dragSurface;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rcx_v6 (UnityEngine.Component)+20]");
		Vector3 vector;
		if ((nint)0 == 1)
		{
			Transform transform = component.transform;
			vector = transform.forward;
			object obj4 = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rcx_v6 (UnityEngine.Component)+20]");
			if ((nint)0 == 2)
			{
				Transform transform2 = component.transform;
				vector = transform2.right;
				object obj4 = 0;
			}
			else
			{
				Transform transform3 = component.transform;
				vector = transform3.up;
				object obj4 = 0;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		Vector3 vector2;
		float num2 = default(float);
		float num3;
		if (vector.x > 1E-05f)
		{
			float num = vector.z / vector.x;
			vector2 = (Vector3)num2;
			num3 = num;
		}
		else
		{
			nint num4 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v469 @ rax_v34 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v470 @ rcx_v32 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			num3 = 0f;
			vector2 = Vector3.zeroVector;
		}
		float num6 = (float)vector2 * gizmoNormalLift;
		object obj5 = default(object);
		float num7 = (float)obj5 * gizmoNormalLift;
		float num8 = num3 * gizmoNormalLift;
		float num9 = default(float);
		Gizmos.color = (Color)(&num9);
		bool flag = (nint)obj3 <= 0;
		float num10 = vector.x;
		int num11 = 0;
		num9 = (float)gizmoColor;
		num10 = vector.x;
		if (!flag)
		{
			bool flag2;
			do
			{
				Vector3 vector3 = ComputeSlotWorldPosition(num11);
				float num = num7 + num2;
				Gizmos.DrawWireSphere((Vector3)(&num10), gizmoSphereRadius);
				num11++;
				flag2 = num11 < (nint)obj3;
				num10 = num2;
				num9 = vector3.x;
				int num12 = 0;
				num10 = num2;
				object obj4 = 0;
			}
			while (flag2);
		}
		if ((nint)obj3 > 1)
		{
			Vector3 vector4 = ComputeSlotWorldPosition(0);
			int slotIndex = columnCount - 1;
			float num13 = num6 + vector4.x;
			float num = num7 + num2;
			Vector3 vector5 = ComputeSlotWorldPosition(slotIndex);
			object obj6 = rowCount - 1;
			int slotIndex2 = obj6 * columnCount;
			Vector3 vector6 = ComputeSlotWorldPosition(slotIndex2);
			int slotIndex3 = obj3 - 1;
			Vector3 vector7 = ComputeSlotWorldPosition(slotIndex3);
			Gizmos.DrawLine((Vector3)(&vector2), (Vector3)(&num10));
			float num14 = default(float);
			Gizmos.DrawLine((Vector3)(&num14), (Vector3)(&num10));
			float num15 = default(float);
			Gizmos.DrawLine((Vector3)(&num15), (Vector3)(&num10));
			float num16 = default(float);
			Gizmos.DrawLine((Vector3)(&num16), (Vector3)(&num10));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+50]");
			num8 = 0f;
			int num12 = 0;
			num10 = num2;
			object obj4 = 0;
		}
		if (maxStackDepth == 1)
		{
			return;
		}
		int num17;
		if (maxStackDepth == 0)
		{
			num17 = 3;
		}
		else
		{
			bool flag3 = maxStackDepth >= 4;
			num17 = 4;
			if (!flag3)
			{
				num17 = maxStackDepth;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItemGridArea)+A0]");
		float num18 = 0f * 0.4f;
		Gizmos.color = (Color)(&num9);
		if ((nint)obj3 <= 0)
		{
			return;
		}
		int num19 = 0;
		do
		{
			if (num17 > 1)
			{
				bool flag4;
				do
				{
					Vector3 vector8 = ComputeSlotWorldPosition(num19, 1);
					float num = num7 + num2;
					num18 = gizmoSphereRadius * 0.6f;
					Gizmos.DrawWireSphere((Vector3)(&num10), num18);
					int num20 = 1 + 1;
					flag4 = num20 < num17;
					num10 = num2;
					object obj4 = 0;
				}
				while (flag4);
			}
			num19++;
		}
		while (num19 < (nint)obj3);
	}

	public DraggableItemGridArea()
	{
		//IL_00fa: Expected I, but got O
		//IL_0020: Expected O, but got I
		//IL_0030: Expected O, but got I
		//IL_0057: Expected O, but got I
		columnCount = 3;
		rowCount = 2;
		cellWidth = 0.12f;
		cellHeight = 0.16f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		gridOriginLocalOffset = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		maxStackDepth = 1;
		List<DraggableItem> list = new List<DraggableItem>();
		gridItems = list;
		compactNullEntries = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rax_v8+B8]");
		taggedItemsTag = (string)0;
		skipDuplicateTaggedItems = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206F00]");
		gizmoColor = (Color)0;
		slideDuration = 0.35f;
		slideStaggerDelay = 0.04f;
		slideEjectAxis = DraggableItem.EjectAxis.NegativeX;
		resetOnStart = true;
		drawGizmos = true;
		gizmoSphereRadius = 0.015f;
		drawSlotIndexLabels = true;
		gizmoNormalLift = 0.003f;
		base._002Ector();
	}
}
