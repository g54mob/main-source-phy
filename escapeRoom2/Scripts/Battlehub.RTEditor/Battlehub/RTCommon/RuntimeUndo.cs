using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTCommon
{
	public class RuntimeUndo : IRuntimeUndo
	{
		private class SelectionState
		{
			public UnityEngine.Object ActiveObject;

			public UnityEngine.Object[] Objects;

			public SelectionState(UnityEngine.Object[] objects, UnityEngine.Object activeObject)
			{
				ActiveObject = activeObject;
				if (objects != null)
				{
					Objects = objects.ToArray();
				}
				else
				{
					Objects = null;
				}
			}

			public SelectionState(IRuntimeSelection selection)
			{
				ActiveObject = selection.activeObject;
				if (selection.objects != null)
				{
					Objects = selection.objects.ToArray();
				}
				else
				{
					Objects = null;
				}
			}
		}

		public class SetValuesState
		{
			public object Accessor;

			public MemberInfo[] MemberInfo;

			public object[] Values;

			public SetValuesState(object accessor, MemberInfo[] memberInfo, object[] values)
			{
				Accessor = accessor;
				MemberInfo = memberInfo;
				Values = values;
			}
		}

		private class TransformState
		{
			public Vector3 position;

			public Quaternion rotation;

			public Vector3 scale;

			public Transform parent;

			public int siblingIndex = -1;

			public bool applyOnRedo;
		}

		private class RectTransformState
		{
			private Vector2 anchorMin;

			private Vector2 anchorMax;

			private Vector2 anchoredPosition;

			private Vector2 pivot;

			private Vector2 sizeDelta;

			private Vector2 offsetMin;

			private Vector2 offsetMax;

			public RectTransformState(RectTransform rt)
			{
				anchorMin = rt.anchorMin;
				anchorMax = rt.anchorMax;
				anchoredPosition = rt.anchoredPosition;
				pivot = rt.pivot;
				sizeDelta = rt.sizeDelta;
				offsetMin = rt.offsetMin;
				offsetMax = rt.offsetMax;
			}

			public void WriteTo(RectTransform rt)
			{
				rt.anchorMin = anchorMin;
				rt.anchorMax = anchorMax;
				rt.anchoredPosition = anchoredPosition;
				rt.pivot = pivot;
				rt.sizeDelta = sizeDelta;
				rt.offsetMin = offsetMin;
				rt.offsetMax = offsetMax;
			}
		}

		public const int Limit = 8192;

		private Dictionary<object, Dictionary<MemberInfo, object>> m_objToValue;

		private List<Record> m_group;

		private UndoStack<Record[]> m_stack;

		private Stack<UndoStack<Record[]>> m_stacks;

		private List<Record[]> m_purgeRecords;

		private List<UndoStack<Record[]>.Node> m_purgeNodes;

		private HashSet<ExposeToEditor> m_markAsDestroyedDuringLastOperation = new HashSet<ExposeToEditor>();

		public bool Enabled { get; set; }

		protected bool Locked { get; private set; }

		public bool CanUndo => m_stack.CanPop;

		public bool CanRedo => m_stack.CanRestore;

		public bool IsRecording => m_group != null;

		public bool IsRecordingValues => m_objToValue.Count > 0;

		public event RuntimeUndoEventHandler BeforeUndo;

		public event RuntimeUndoEventHandler UndoCompleted;

		public event RuntimeUndoEventHandler BeforeRedo;

		public event RuntimeUndoEventHandler RedoCompleted;

		public event RuntimeUndoEventHandler StateChanged;

		public RuntimeUndo(IRTE _)
		{
			Reset();
		}

		public void Reset()
		{
			Enabled = true;
			m_group = null;
			m_stack = new UndoStack<Record[]>(8192);
			m_stacks = new Stack<UndoStack<Record[]>>();
			m_purgeRecords = new List<Record[]>();
			m_purgeNodes = new List<UndoStack<Record[]>.Node>();
			m_objToValue = new Dictionary<object, Dictionary<MemberInfo, object>>();
		}

		public void BeginRecord()
		{
			if (Enabled && !Locked)
			{
				m_group = new List<Record>();
			}
		}

		public void EndRecord()
		{
			if (!Enabled || Locked)
			{
				return;
			}
			if (m_group != null && m_group.Count > 0)
			{
				m_stack.Push(m_group.ToArray(), m_purgeRecords);
				for (int i = 0; i < m_purgeRecords.Count; i++)
				{
					Record[] array = m_purgeRecords[i];
					if (array != null)
					{
						for (int j = 0; j < array.Length; j++)
						{
							array[j].Purge();
						}
					}
				}
				m_purgeRecords.Clear();
				m_markAsDestroyedDuringLastOperation.Clear();
				if (this.StateChanged != null)
				{
					this.StateChanged();
				}
			}
			m_group = null;
		}

		public void CancelRecord()
		{
			if (Enabled && !Locked)
			{
				m_objToValue.Clear();
				m_group = null;
			}
		}

		public void GroupRecords(int count)
		{
			if (!Enabled || Locked)
			{
				return;
			}
			BeginRecord();
			Stack<Record[]> stack = new Stack<Record[]>();
			while (m_stack.CanPop && count != 0)
			{
				Record[] item = m_stack.Pop();
				stack.Push(item);
				count--;
			}
			while (stack.Count > 0)
			{
				Record[] array = stack.Pop();
				for (int i = 0; i < array.Length; i++)
				{
					m_group.Add(array[i]);
				}
			}
			EndRecord();
		}

		public void Redo()
		{
			if (!Enabled || Locked || !m_stack.CanRestore)
			{
				return;
			}
			try
			{
				Locked = true;
				DoRedo();
			}
			finally
			{
				Locked = false;
			}
		}

		private void DoRedo()
		{
			if (this.BeforeRedo != null)
			{
				this.BeforeRedo();
			}
			bool flag;
			do
			{
				flag = false;
				Record[] array = m_stack.Restore();
				foreach (Record record in array)
				{
					flag |= record.Redo();
				}
			}
			while (!flag && m_stack.CanRestore);
			if (this.RedoCompleted != null)
			{
				this.RedoCompleted();
			}
		}

		public void Undo()
		{
			if (!Enabled || Locked || !m_stack.CanPop)
			{
				return;
			}
			try
			{
				Locked = true;
				DoUndo();
			}
			finally
			{
				Locked = false;
			}
		}

		private void DoUndo()
		{
			if (this.BeforeUndo != null)
			{
				this.BeforeUndo();
			}
			bool flag;
			do
			{
				flag = false;
				Record[] array = m_stack.Pop();
				for (int num = array.Length - 1; num >= 0; num--)
				{
					Record record = array[num];
					flag |= record.Undo();
				}
			}
			while (!flag && m_stack.CanPop);
			if (this.UndoCompleted != null)
			{
				this.UndoCompleted();
			}
		}

		public void Purge()
		{
			if (Enabled && !Locked)
			{
				_Purge();
				if (this.StateChanged != null)
				{
					this.StateChanged();
				}
			}
		}

		private void _Purge()
		{
			foreach (UndoStack<Record[]>.Node item in (IEnumerable<UndoStack<Record[]>.Node>)m_stack)
			{
				if (item.Data != null)
				{
					for (int i = 0; i < item.Data.Length; i++)
					{
						item.Data[i].Purge();
					}
				}
			}
			m_stack.Clear();
			m_group = null;
			if (m_objToValue.Count > 0)
			{
				Debug.LogWarning("Unifished RecordValue operations exists.");
				m_objToValue = new Dictionary<object, Dictionary<MemberInfo, object>>();
			}
		}

		public void Erase(object oldRef, object newRef, bool ignoreLock = false)
		{
			if (!Enabled || (Locked && !ignoreLock))
			{
				return;
			}
			if (m_objToValue.Count > 0)
			{
				Debug.LogWarning("Unifished RecordValue operations exists.");
				m_objToValue = new Dictionary<object, Dictionary<MemberInfo, object>>();
			}
			foreach (UndoStack<Record[]>.Node item in (IEnumerable<UndoStack<Record[]>.Node>)m_stack)
			{
				if (item.Data == null)
				{
					continue;
				}
				int num = 0;
				for (int i = 0; i < item.Data.Length; i++)
				{
					if (item.Data[i].Erase(oldRef, newRef))
					{
						num++;
					}
				}
				if (num > 0 && item.Data.Length == num)
				{
					m_purgeNodes.Add(item);
				}
			}
			for (int j = 0; j < m_purgeNodes.Count; j++)
			{
				UndoStack<Record[]>.Node node = m_purgeNodes[j];
				if (node != null)
				{
					for (int k = 0; k < node.Data.Length; k++)
					{
						node.Data[k].Purge();
					}
				}
				m_stack.Purge(node);
			}
			m_purgeNodes.Clear();
			if (this.StateChanged != null)
			{
				this.StateChanged();
			}
		}

		public void Store()
		{
			if (Enabled && !Locked)
			{
				m_stacks.Push(m_stack);
				m_stack = new UndoStack<Record[]>(8192);
				if (this.StateChanged != null)
				{
					this.StateChanged();
				}
			}
		}

		public void Restore()
		{
			if (Enabled && !Locked && m_stacks.Count > 0)
			{
				_Purge();
				m_stack = m_stacks.Pop();
				if (this.StateChanged != null)
				{
					this.StateChanged();
				}
			}
		}

		public Record CreateRecord(UndoRedoCallback redoCallback, UndoRedoCallback undoCallback, PurgeCallback purgeCallback = null, EraseReferenceCallback eraseCallback = null)
		{
			return CreateRecord(null, null, null, redoCallback, undoCallback, purgeCallback, eraseCallback);
		}

		public Record CreateRecord(object target, object newState, object oldState, UndoRedoCallback redoCallback, UndoRedoCallback undoCallback, PurgeCallback purgeCallback = null, EraseReferenceCallback eraseCallback = null)
		{
			if (!Enabled)
			{
				return null;
			}
			if (Locked)
			{
				return null;
			}
			if (purgeCallback == null)
			{
				purgeCallback = delegate
				{
				};
			}
			Record record = new Record(target, newState, oldState, redoCallback, undoCallback, purgeCallback, eraseCallback);
			if (m_group != null)
			{
				m_group.Add(record);
			}
			else
			{
				m_stack.Push(new Record[1] { record }, m_purgeRecords);
				for (int num = 0; num < m_purgeRecords.Count; num++)
				{
					Record[] array = m_purgeRecords[num];
					if (array != null)
					{
						for (int num2 = 0; num2 < array.Length; num2++)
						{
							array[num2].Purge();
						}
					}
				}
				m_purgeRecords.Clear();
				if (this.StateChanged != null)
				{
					this.StateChanged();
				}
			}
			return record;
		}

		private static bool HasSelectionChanged(UnityEngine.Object[] newObjects, UnityEngine.Object newActiveObject, IRuntimeSelection selection)
		{
			return HasSelectionChanged(newObjects, newActiveObject, selection.objects, selection.activeObject);
		}

		private static bool HasSelectionChanged(UnityEngine.Object[] newObjects, UnityEngine.Object newActiveObject, UnityEngine.Object[] objects, UnityEngine.Object activeObject)
		{
			if (activeObject != newActiveObject)
			{
				return true;
			}
			if (objects == newObjects)
			{
				return false;
			}
			if (objects == null || newObjects == null)
			{
				if (objects == null && newObjects != null && newObjects.Length == 0)
				{
					return false;
				}
				if (newObjects == null && objects != null && objects.Length == 0)
				{
					return false;
				}
				return true;
			}
			if (objects.Length != newObjects.Length)
			{
				return true;
			}
			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i] != newObjects[i])
				{
					return true;
				}
			}
			return false;
		}

		public void EraseFromSelection(UnityEngine.Object[] objects)
		{
			foreach (UndoStack<Record[]>.Node item in (IEnumerable<UndoStack<Record[]>.Node>)m_stack)
			{
				Record[] data = item.Data;
				foreach (Record record in data)
				{
					if (record.OldState is SelectionState state)
					{
						UnityEngine.Object[] array = objects;
						foreach (object oldReference in array)
						{
							EraseFromSelection(state, null, oldReference);
						}
					}
					if (record.NewState is SelectionState state2)
					{
						UnityEngine.Object[] array = objects;
						foreach (object oldReference2 in array)
						{
							EraseFromSelection(state2, null, oldReference2);
						}
					}
				}
			}
		}

		private static void EraseFromSelection(SelectionState state, object newReference, object oldReference)
		{
			if (state.ActiveObject == oldReference)
			{
				state.ActiveObject = newReference as UnityEngine.Object;
			}
			bool flag = false;
			if (state.Objects != null)
			{
				for (int i = 0; i < state.Objects.Length; i++)
				{
					if (state.Objects[i] == oldReference)
					{
						state.Objects[i] = newReference as UnityEngine.Object;
						if (state.Objects[i] == null)
						{
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				state.Objects = state.Objects.Where((UnityEngine.Object o) => o != null).ToArray();
				if (state.Objects.Length == 0)
				{
					state.Objects = null;
				}
			}
		}

		private bool ApplySelection(SelectionState state, IRuntimeSelection selection)
		{
			bool num = HasSelectionChanged(state.Objects, state.ActiveObject, selection);
			if (num)
			{
				selection.Select(state.ActiveObject, state.Objects);
			}
			return num;
		}

		public void Select(IRuntimeSelection selection, UnityEngine.Object[] objects, UnityEngine.Object activeObject)
		{
			if (!Enabled || Locked || !HasSelectionChanged(objects, activeObject, selection))
			{
				return;
			}
			CreateRecord(selection, new SelectionState(objects, activeObject), new SelectionState(selection), (Record record) => ApplySelection((SelectionState)record.NewState, (IRuntimeSelection)record.Target), (Record record) => ApplySelection((SelectionState)record.OldState, (IRuntimeSelection)record.Target), delegate
			{
			}, delegate(Record record, object oldReference, object newReference)
			{
				SelectionState selectionState = (SelectionState)record.NewState;
				SelectionState selectionState2 = (SelectionState)record.OldState;
				EraseFromSelection(selectionState2, newReference, oldReference);
				EraseFromSelection(selectionState, newReference, oldReference);
				bool result = false;
				if (!HasSelectionChanged(selectionState.Objects, selectionState.ActiveObject, selectionState2.Objects, selectionState2.ActiveObject))
				{
					result = true;
				}
				return result;
			})?.Redo();
		}

		private bool MarkAsDestroyed(Record record, bool destroyed)
		{
			ExposeToEditor[] array = (ExposeToEditor[])record.Target;
			bool result = false;
			foreach (ExposeToEditor exposeToEditor in array)
			{
				if (exposeToEditor != null)
				{
					exposeToEditor.MarkAsDestroyed = destroyed;
					result = true;
				}
			}
			return result;
		}

		private void PurgeMarkedAsDestoryed(Record record)
		{
			ExposeToEditor[] array = (ExposeToEditor[])record.Target;
			foreach (ExposeToEditor exposeToEditor in array)
			{
				if (exposeToEditor != null && exposeToEditor.MarkAsDestroyed && !m_markAsDestroyedDuringLastOperation.Contains(exposeToEditor))
				{
					UnityEngine.Object.DestroyImmediate(exposeToEditor.gameObject);
				}
			}
		}

		private static bool EraseMarkedAsDestroyed(Record record, object newReference, object oldReference)
		{
			ExposeToEditor[] array = (ExposeToEditor[])record.Target;
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				ExposeToEditor exposeToEditor = array[i];
				if (exposeToEditor == null)
				{
					continue;
				}
				if (oldReference is GameObject && exposeToEditor.gameObject == oldReference)
				{
					array[i] = null;
					GameObject gameObject = newReference as GameObject;
					if (gameObject != null)
					{
						array[i] = gameObject.GetComponent<ExposeToEditor>();
					}
				}
				if (array[i] == null)
				{
					flag = true;
				}
			}
			if (flag)
			{
				array = (ExposeToEditor[])(record.Target = array.Where((ExposeToEditor obj) => obj != null).ToArray());
			}
			return array.Length == 0;
		}

		public void RegisterCreatedObjects(ExposeToEditor[] createdObjects, Action afterRedo = null, Action afterUndo = null)
		{
			if (!Enabled || Locked)
			{
				return;
			}
			CreateRecord(createdObjects, false, true, delegate(Record record)
			{
				bool result = MarkAsDestroyed(record, (bool)record.NewState);
				Action action = afterRedo;
				if (action != null)
				{
					action();
					return result;
				}
				return result;
			}, delegate(Record record)
			{
				bool result = MarkAsDestroyed(record, (bool)record.OldState);
				Action action = afterUndo;
				if (action != null)
				{
					action();
					return result;
				}
				return result;
			}, delegate(Record record)
			{
				PurgeMarkedAsDestoryed(record);
			}, (Record record, object oldReference, object newReference) => EraseMarkedAsDestroyed(record, newReference, oldReference))?.Redo();
		}

		public void DestroyObjects(ExposeToEditor[] destoryedObjects, Action afterRedo = null, Action afterUndo = null)
		{
			if (!Enabled || Locked)
			{
				return;
			}
			for (int i = 0; i < destoryedObjects.Length; i++)
			{
				if (!m_markAsDestroyedDuringLastOperation.Contains(destoryedObjects[i]))
				{
					m_markAsDestroyedDuringLastOperation.Add(destoryedObjects[i]);
				}
			}
			CreateRecord(destoryedObjects, true, false, delegate(Record record)
			{
				bool result = MarkAsDestroyed(record, (bool)record.NewState);
				Action action = afterRedo;
				if (action != null)
				{
					action();
					return result;
				}
				return result;
			}, delegate(Record record)
			{
				bool result = MarkAsDestroyed(record, (bool)record.OldState);
				Action action = afterUndo;
				if (action != null)
				{
					action();
					return result;
				}
				return result;
			}, delegate(Record record)
			{
				PurgeMarkedAsDestoryed(record);
			}, (Record record, object oldReference, object newReference) => EraseMarkedAsDestroyed(record, newReference, oldReference))?.Redo();
			if (!IsRecording)
			{
				m_markAsDestroyedDuringLastOperation.Clear();
			}
		}

		private static object GetDefault(Type type)
		{
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		private static Array DuplicateArray(Array array)
		{
			Array array2 = (Array)Activator.CreateInstance(array.GetType(), array.Length);
			if (array != null)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					array2.SetValue(array.GetValue(i), i);
				}
			}
			return array;
		}

		private object GetValue(object accessor, MemberInfo m)
		{
			PropertyInfo propertyInfo = m as PropertyInfo;
			if (propertyInfo != null)
			{
				if (accessor == null || (accessor is UnityEngine.Object && null == (UnityEngine.Object)accessor))
				{
					return GetDefault(propertyInfo.PropertyType);
				}
				object obj = propertyInfo.GetValue(accessor, null);
				if (obj is Array)
				{
					obj = DuplicateArray((Array)obj);
				}
				return obj;
			}
			FieldInfo fieldInfo = m as FieldInfo;
			if (fieldInfo != null)
			{
				if (accessor == null || (accessor is UnityEngine.Object && null == (UnityEngine.Object)accessor))
				{
					return GetDefault(fieldInfo.FieldType);
				}
				object obj2 = fieldInfo.GetValue(accessor);
				if (obj2 is Array)
				{
					obj2 = DuplicateArray((Array)obj2);
				}
				return obj2;
			}
			if (m is MethodInfo)
			{
				return null;
			}
			throw new ArgumentException("member is not FieldInfo and is not PropertyInfo", "m");
		}

		private object[] GetValues(object accessor, MemberInfo[] memberInfo)
		{
			object[] array = new object[memberInfo.Length];
			for (int i = 0; i < memberInfo.Length; i++)
			{
				array[i] = GetValue(accessor, memberInfo[i]);
			}
			return array;
		}

		private void AssignValue(object accessor, MemberInfo m, object value)
		{
			if (accessor == null || (accessor is UnityEngine.Object && null == (UnityEngine.Object)accessor))
			{
				return;
			}
			PropertyInfo propertyInfo = m as PropertyInfo;
			if (propertyInfo != null)
			{
				propertyInfo.SetValue(accessor, value, null);
				return;
			}
			FieldInfo fieldInfo = m as FieldInfo;
			if (fieldInfo != null)
			{
				fieldInfo.SetValue(accessor, value);
			}
			else if (!(m is MethodInfo))
			{
				throw new ArgumentException("member is not FieldInfo and is not PropertyInfo", "m");
			}
		}

		private void AssingValues(object accessor, MemberInfo[] memberInfo, object[] values)
		{
			for (int i = 0; i < memberInfo.Length; i++)
			{
				AssignValue(accessor, memberInfo[i], values[i]);
			}
		}

		private bool AssignValues(SetValuesState state, Action callback)
		{
			if (state.Accessor == null || (state.Accessor is UnityEngine.Object && null == (UnityEngine.Object)state.Accessor))
			{
				return false;
			}
			bool result = false;
			for (int i = 0; i < state.Values.Length; i++)
			{
				object value = GetValue(state.Accessor, state.MemberInfo[i]);
				object obj = state.Values[i];
				if (IsValueChanged(value, obj))
				{
					result = true;
				}
				AssignValue(state.Accessor, state.MemberInfo[i], obj);
			}
			callback?.Invoke();
			return result;
		}

		private static bool IsValueChanged(object a, object b)
		{
			if (a == null && b == null)
			{
				return false;
			}
			if (a != null && b != null)
			{
				if (a is Vector3 && b is Vector3)
				{
					return (Vector3)a != (Vector3)b;
				}
				if (a is Vector2 && b is Vector2)
				{
					return (Vector2)a != (Vector2)b;
				}
				if (a is Vector4 && b is Vector4)
				{
					return (Vector4)a != (Vector4)b;
				}
				return !a.Equals(b);
			}
			return true;
		}

		private void EraseFromSetValuesState(SetValuesState state, object newReference, object oldReference)
		{
			if (state.Accessor == oldReference)
			{
				state.Accessor = newReference;
			}
			bool flag = false;
			if (state.Values != null)
			{
				for (int i = 0; i < state.Values.Length; i++)
				{
					object obj = state.Values[i];
					if (obj == oldReference)
					{
						state.Values[i] = newReference;
						if (newReference == null)
						{
							state.MemberInfo[i] = null;
							flag = true;
						}
					}
					else
					{
						if (!(obj is IList))
						{
							continue;
						}
						IList list = (IList)obj;
						for (int j = 0; j < list.Count; j++)
						{
							if (list[j] == oldReference)
							{
								list[j] = newReference;
							}
						}
					}
				}
			}
			if (flag)
			{
				state.Values = state.Values.Where((object o) => o != null).ToArray();
				state.MemberInfo = state.MemberInfo.Where((MemberInfo o) => o != null).ToArray();
			}
		}

		private void RecordValues(object target, object accessor, MemberInfo[] memberInfo, object[] oldValues, Action<object, object> targetErased, Action afterRedo, Action afterUndo)
		{
			if (!Enabled || Locked)
			{
				return;
			}
			CreateRecord(target, new SetValuesState(accessor, memberInfo, GetValues(accessor, memberInfo)), new SetValuesState(accessor, memberInfo, oldValues), (Record record) => AssignValues((SetValuesState)record.NewState, afterRedo), (Record record) => AssignValues((SetValuesState)record.OldState, afterUndo), delegate
			{
			}, delegate(Record record, object oldReference, object newReference)
			{
				if (record.Target == oldReference)
				{
					record.Target = newReference;
					if (targetErased != null)
					{
						targetErased(accessor, record.Target);
					}
					if (record.Target == null)
					{
						return true;
					}
				}
				SetValuesState setValuesState = (SetValuesState)record.NewState;
				SetValuesState setValuesState2 = (SetValuesState)record.OldState;
				EraseFromSetValuesState(setValuesState, newReference, oldReference);
				EraseFromSetValuesState(setValuesState2, newReference, oldReference);
				if (setValuesState.Values.Length == 0 && setValuesState2.Values.Length == 0)
				{
					return true;
				}
				return setValuesState.Accessor == null;
			});
		}

		private void RecordValue(object target, object accessor, MemberInfo memberInfo, object oldValue, Action<object, object> targetErased, Action afterRedo, Action afterUndo)
		{
			RecordValues(target, accessor, new MemberInfo[1] { memberInfo }, new object[1] { oldValue }, targetErased, afterRedo, afterUndo);
		}

		public void RecordValue(object target, MemberInfo memberInfo, Action afterRedo, Action afterUndo)
		{
			RecordValue(target, target, memberInfo, GetValue(target, memberInfo), null, afterRedo, afterUndo);
		}

		public void RecordValue(object target, object accessor, MemberInfo memberInfo, Action afterRedo, Action afterUndo)
		{
			RecordValue(target, accessor, memberInfo, GetValue(accessor, memberInfo), null, afterRedo, afterUndo);
		}

		public void BeginRecordValue(object target, MemberInfo memberInfo)
		{
			BeginRecordValue(target, target, memberInfo);
		}

		public void BeginRecordValue(object target, object accessor, MemberInfo memberInfo)
		{
			if (Enabled && !Locked)
			{
				if (!m_objToValue.TryGetValue(target, out var value))
				{
					value = new Dictionary<MemberInfo, object>();
					m_objToValue.Add(target, value);
				}
				if (value.ContainsKey(memberInfo))
				{
					Debug.LogWarning("Unfinished record value operation for " + memberInfo.Name + " exist");
				}
				value[memberInfo] = GetValue(accessor, memberInfo);
			}
		}

		public void EndRecordValue(object target, MemberInfo memberInfo, Action afterRedo, Action afterUndo)
		{
			EndRecordValue(target, target, memberInfo, null, afterRedo, afterUndo);
		}

		public void EndRecordValue(object target, object accessor, MemberInfo memberInfo, Action<object, object> targetErased, Action afterRedo, Action afterUndo)
		{
			if (Enabled && !Locked && m_objToValue.TryGetValue(target, out var value) && value.TryGetValue(memberInfo, out var value2))
			{
				value.Remove(memberInfo);
				if (value.Count == 0)
				{
					m_objToValue.Remove(target);
				}
				RecordValue(target, accessor, memberInfo, value2, targetErased, afterRedo, afterUndo);
			}
		}

		public void BeginRecordTransform(Transform target, Action<Transform> afterUndo = null)
		{
			RecordTransform(applyOnRedo: false, target, null, -1, afterUndo);
		}

		public void EndRecordTransform(Transform target, Action<Transform> afterRedo = null)
		{
			RecordTransform(applyOnRedo: true, target, null, -1, afterRedo);
		}

		public void BeginRecordTransform(Transform target, Transform parent, int siblingIndex = -1, Action<Transform> afterUndo = null)
		{
			RecordTransform(applyOnRedo: false, target, parent, siblingIndex, afterUndo);
		}

		public void EndRecordTransform(Transform target, Transform parent, int siblingIndex = -1, Action<Transform> afterRedo = null)
		{
			RecordTransform(applyOnRedo: true, target, parent, siblingIndex, afterRedo);
		}

		private void RecordTransform(bool applyOnRedo, Transform target, Transform parent = null, int siblingIndex = -1, Action<Transform> callback = null)
		{
			if (!Enabled || Locked)
			{
				return;
			}
			TransformState transformState = new TransformState
			{
				position = target.position,
				rotation = target.rotation,
				scale = target.localScale
			};
			transformState.parent = parent;
			transformState.siblingIndex = siblingIndex;
			transformState.applyOnRedo = applyOnRedo;
			CreateRecord(target, transformState, null, (Record record) => ApplyTransform(record, isRedo: true, callback), (Record record) => ApplyTransform(record, isRedo: false, callback), delegate
			{
			}, delegate(Record record, object oldReference, object newReference)
			{
				if (newReference == null)
				{
					record.Target = null;
				}
				return false;
			});
		}

		private static bool ApplyTransform(Record record, bool isRedo, Action<Transform> callback)
		{
			Transform transform = (Transform)record.Target;
			if (!transform)
			{
				return false;
			}
			TransformState transformState = (TransformState)record.NewState;
			if (transformState.applyOnRedo != isRedo)
			{
				return false;
			}
			bool flag = transform.position != transformState.position || transform.rotation != transformState.rotation || transform.localScale != transformState.scale;
			bool flag2 = transformState.siblingIndex == -1;
			if (!flag2)
			{
				int siblingIndex = transform.GetSiblingIndex();
				flag = flag || transform.parent != transformState.parent || siblingIndex != transformState.siblingIndex;
			}
			if (flag)
			{
				if (!flag2)
				{
					transform.SetParent(transformState.parent, worldPositionStays: true);
					transform.SetSiblingIndex(transformState.siblingIndex);
				}
				transform.position = transformState.position;
				transform.rotation = transformState.rotation;
				transform.localScale = transformState.scale;
			}
			callback?.Invoke(transform);
			return flag;
		}

		private object OnBeforeAddComponent(GameObject obj, Type componentType)
		{
			if (componentType.IsSubclassOf(typeof(LayoutGroup)))
			{
				RectTransformState[] array = new RectTransformState[obj.transform.childCount];
				int num = 0;
				{
					foreach (RectTransform item in obj.transform)
					{
						array[num] = new RectTransformState(item);
						num++;
					}
					return array;
				}
			}
			return null;
		}

		private object OnAfterAddComponentUndo(GameObject obj, Type componentType, object gameObjectState)
		{
			if (componentType.IsSubclassOf(typeof(LayoutGroup)))
			{
				RectTransformState[] array = (RectTransformState[])gameObjectState;
				int num = 0;
				{
					foreach (RectTransform item in obj.transform)
					{
						array[num].WriteTo(item);
						num++;
					}
					return array;
				}
			}
			return null;
		}

		private static Component AddComponent(GameObject go, Type type)
		{
			ExposeToEditor component = go.GetComponent<ExposeToEditor>();
			if (type == typeof(Rigidbody))
			{
				ExposeToEditor[] componentsInChildren = go.GetComponentsInChildren<ExposeToEditor>(includeInactive: true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Collider[] colliders = componentsInChildren[i].Colliders;
					if (colliders == null)
					{
						continue;
					}
					foreach (Collider collider in colliders)
					{
						if (collider is MeshCollider)
						{
							((MeshCollider)collider).convex = true;
						}
					}
				}
			}
			Component component2 = component.AddComponent(type);
			if (component2 is Rigidbody)
			{
				((Rigidbody)component2).isKinematic = true;
			}
			return component2;
		}

		public Component AddComponent(ExposeToEditor obj, Type type)
		{
			if (!Enabled)
			{
				return null;
			}
			if (Locked)
			{
				return null;
			}
			if (Reflection.GetCustomAttribute<DisallowMultipleComponent>(type, out var typeWithAttribute) != null)
			{
				Component component = obj.GetComponent(typeWithAttribute);
				if (component != null)
				{
					Debug.LogFormat("Can't add {0} because a {1} is already added to the game object!", type.Name, component.GetType().Name);
					return null;
				}
			}
			object gameObjectState = OnBeforeAddComponent(obj.gameObject, type);
			Record record = CreateRecord(obj, type, null, delegate(Record record2)
			{
				ExposeToEditor exposeToEditor = (ExposeToEditor)record2.Target;
				if (exposeToEditor == null)
				{
					return false;
				}
				Type type2 = (Type)record2.NewState;
				Component component2 = AddComponent(exposeToEditor.gameObject, type2);
				if (record2.OldState != null)
				{
					Erase(record2.OldState, component2, ignoreLock: true);
				}
				record2.OldState = component2;
				return true;
			}, delegate(Record record2)
			{
				Component component2 = (Component)record2.OldState;
				object obj2 = new object();
				Erase(component2, obj2, ignoreLock: true);
				DestroyComponent(component2);
				record2.OldState = obj2;
				OnAfterAddComponentUndo(component2.gameObject, component2.GetType(), gameObjectState);
				return true;
			}, delegate
			{
			}, delegate(Record record2, object oldReference, object newReference)
			{
				ExposeToEditor exposeToEditor = record2.Target as ExposeToEditor;
				if (exposeToEditor != null && exposeToEditor.gameObject == oldReference)
				{
					GameObject gameObject = newReference as GameObject;
					if (gameObject == null)
					{
						record2.Target = null;
					}
					else
					{
						record2.Target = gameObject.GetComponent<ExposeToEditor>();
					}
				}
				if (record2.OldState == oldReference)
				{
					record2.OldState = newReference;
					if (record2.NewState != null && newReference != null && record2.NewState is Type)
					{
						Type obj2 = (Type)record2.NewState;
						Type type2 = newReference.GetType();
						if (obj2.FullName == type2.FullName)
						{
							record2.NewState = type2;
						}
					}
				}
				return (record2.Target as ExposeToEditor == null) ? true : false;
			});
			record?.Redo();
			return record.OldState as Component;
		}

		public Component[] AddComponentWithRequirements(ExposeToEditor obj, Type type)
		{
			List<Component> list = new List<Component>();
			foreach (RequireComponent item5 in type.GetCustomAttributes(inherit: true).OfType<RequireComponent>())
			{
				if (item5.m_Type0 != null && !obj.GetComponent(item5.m_Type0))
				{
					Component item = AddComponent(obj, item5.m_Type0);
					list.Add(item);
				}
				if (item5.m_Type1 != null && !obj.GetComponent(item5.m_Type1))
				{
					Component item2 = AddComponent(obj, item5.m_Type1);
					list.Add(item2);
				}
				if (item5.m_Type2 != null && !obj.GetComponent(item5.m_Type2))
				{
					Component item3 = AddComponent(obj, item5.m_Type2);
					list.Add(item3);
				}
			}
			Component item4 = AddComponent(obj, type);
			list.Add(item4);
			return list.ToArray();
		}

		public void DestroyComponent(Component destroy, MemberInfo[] memberInfo)
		{
			if (!Enabled || Locked)
			{
				return;
			}
			if (memberInfo == null)
			{
				memberInfo = destroy.GetType().GetSerializableFields(declaredOnly: false).Cast<MemberInfo>()
					.Union(destroy.GetType().GetSerializableProperties())
					.ToArray();
			}
			Type componentType = destroy.GetType();
			CreateRecord(destroy.gameObject, null, destroy, delegate(Record record)
			{
				_ = record.Target;
				Component component = record.OldState as Component;
				object obj = new object();
				if ((bool)component)
				{
					Erase(component, obj, ignoreLock: true);
				}
				record.OldState = obj;
				record.NewState = GetValues(component, memberInfo);
				DestroyComponent(component);
				return true;
			}, delegate(Record record)
			{
				Component component = AddComponent(record.Target as GameObject, componentType);
				AssingValues(component, memberInfo, (object[])record.NewState);
				object oldState = record.OldState;
				Erase(oldState, component, ignoreLock: true);
				record.OldState = component;
				return true;
			}, delegate
			{
			}, delegate(Record record, object oldReference, object newReference)
			{
				GameObject gameObject = record.Target as GameObject;
				if (gameObject != null && gameObject.gameObject == oldReference)
				{
					GameObject gameObject2 = newReference as GameObject;
					if (gameObject2 == null)
					{
						record.Target = null;
					}
					else
					{
						record.Target = gameObject2;
					}
				}
				if (record.OldState == oldReference)
				{
					record.OldState = newReference;
				}
				if (record.NewState is object[])
				{
					object[] array = (object[])record.NewState;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] == oldReference)
						{
							array[i] = newReference;
						}
					}
				}
				return (record.Target as GameObject == null) ? true : false;
			})?.Redo();
		}

		private static void DestroyComponent(Component component)
		{
			if (component != null)
			{
				ExposeToEditor component2 = component.GetComponent<ExposeToEditor>();
				if (component2 != null)
				{
					component2.DestroyComponent(component);
				}
				else
				{
					UnityEngine.Object.Destroy(component);
				}
			}
		}
	}
}
