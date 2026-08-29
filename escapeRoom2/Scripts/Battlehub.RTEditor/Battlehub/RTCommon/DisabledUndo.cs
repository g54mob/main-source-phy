using System;
using System.Reflection;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public class DisabledUndo : IRuntimeUndo
	{
		public bool Enabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public bool CanUndo => false;

		public bool CanRedo => false;

		public bool IsRecording => false;

		public bool IsRecordingValues => false;

		public event RuntimeUndoEventHandler BeforeUndo;

		public event RuntimeUndoEventHandler UndoCompleted;

		public event RuntimeUndoEventHandler BeforeRedo;

		public event RuntimeUndoEventHandler RedoCompleted;

		public event RuntimeUndoEventHandler StateChanged;

		private void GetRidOfWarnings()
		{
			this.BeforeUndo();
			this.UndoCompleted();
			this.BeforeRedo();
			this.RedoCompleted();
			this.StateChanged();
		}

		public void BeginRecord()
		{
		}

		public void EndRecord()
		{
		}

		public void CancelRecord()
		{
		}

		public void GroupRecords(int count)
		{
		}

		public void Redo()
		{
		}

		public void Undo()
		{
		}

		public void Purge()
		{
		}

		public void Erase(object oldRef, object newRef, bool ignoreLock)
		{
		}

		public void Store()
		{
		}

		public void Restore()
		{
		}

		public Record CreateRecord(UndoRedoCallback redoCallback, UndoRedoCallback undoCallback, PurgeCallback purgeCallback = null, EraseReferenceCallback eraseCallback = null)
		{
			return CreateRecord(null, null, null, redoCallback, undoCallback, purgeCallback, eraseCallback);
		}

		public Record CreateRecord(object target, object newState, object oldState, UndoRedoCallback redoCallback, UndoRedoCallback undoCallback, PurgeCallback purgeCallback = null, EraseReferenceCallback eraseCallback = null)
		{
			return null;
		}

		public void Select(IRuntimeSelection selection, UnityEngine.Object[] objects, UnityEngine.Object activeObject)
		{
		}

		public void EraseFromSelection(UnityEngine.Object[] objects)
		{
		}

		[Obsolete]
		public void Select(UnityEngine.Object[] objects, UnityEngine.Object activeObject)
		{
		}

		public void RegisterCreatedObjects(ExposeToEditor[] createdObjects, Action afterRedo = null, Action afterUndo = null)
		{
		}

		public void DestroyObjects(ExposeToEditor[] destoryedObjects, Action afterRedo = null, Action afterUndo = null)
		{
			foreach (ExposeToEditor exposeToEditor in destoryedObjects)
			{
				if (exposeToEditor == null || exposeToEditor.CanDelete)
				{
					UnityEngine.Object.Destroy(exposeToEditor.gameObject);
				}
			}
		}

		public void RecordValue(object target, MemberInfo memberInfo, Action afterRedo, Action afterUndo)
		{
		}

		public void RecordValue(object target, object accessor, MemberInfo memberInfo, Action afterRedo, Action afterUndo)
		{
		}

		public void BeginRecordValue(object target, MemberInfo memberInfo)
		{
		}

		public void BeginRecordValue(object target, object accessor, MemberInfo memberInfo)
		{
		}

		public void EndRecordValue(object target, MemberInfo memberInfo, Action afterRedo, Action afterUndo)
		{
		}

		public void EndRecordValue(object target, object accessor, MemberInfo memberInfo, Action<object, object> targetErased, Action afterRedo, Action afterUndo)
		{
		}

		public Component AddComponent(ExposeToEditor obj, Type type)
		{
			return null;
		}

		public Component[] AddComponentWithRequirements(ExposeToEditor obj, Type type)
		{
			return new Component[0];
		}

		public void DestroyComponent(Component destroy, MemberInfo[] memberInfo)
		{
		}

		public void BeginRecordTransform(Transform target, Action<Transform> afterUndo = null)
		{
		}

		public void EndRecordTransform(Transform target, Action<Transform> afterRedo = null)
		{
		}

		public void BeginRecordTransform(Transform target, Transform parent, int siblingIndex = -1, Action<Transform> afterUndo = null)
		{
		}

		public void EndRecordTransform(Transform target, Transform parent, int siblingIndex = -1, Action<Transform> afterRedo = null)
		{
		}
	}
}
