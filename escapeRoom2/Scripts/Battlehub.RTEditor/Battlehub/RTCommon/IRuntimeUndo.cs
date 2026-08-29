using System;
using System.Reflection;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public interface IRuntimeUndo
	{
		bool Enabled { get; set; }

		bool CanUndo { get; }

		bool CanRedo { get; }

		bool IsRecording { get; }

		bool IsRecordingValues { get; }

		event RuntimeUndoEventHandler BeforeUndo;

		event RuntimeUndoEventHandler UndoCompleted;

		event RuntimeUndoEventHandler BeforeRedo;

		event RuntimeUndoEventHandler RedoCompleted;

		event RuntimeUndoEventHandler StateChanged;

		void BeginRecord();

		void EndRecord();

		void CancelRecord();

		void GroupRecords(int count = -1);

		void Redo();

		void Undo();

		void Purge();

		void Erase(object oldRef, object newRef = null, bool ignoreLock = false);

		void Store();

		void Restore();

		Record CreateRecord(UndoRedoCallback redoCallback, UndoRedoCallback undoCallback, PurgeCallback purgeCallback = null, EraseReferenceCallback eraseCallback = null);

		Record CreateRecord(object target, object newState, object oldState, UndoRedoCallback redoCallback, UndoRedoCallback undoCallback, PurgeCallback purgeCallback = null, EraseReferenceCallback eraseCallback = null);

		void Select(IRuntimeSelection selection, UnityEngine.Object[] objects, UnityEngine.Object activeObject);

		void EraseFromSelection(UnityEngine.Object[] objects);

		void RegisterCreatedObjects(ExposeToEditor[] createdObjects, Action afterRedo = null, Action afterUndo = null);

		void DestroyObjects(ExposeToEditor[] destoryedObjects, Action afterRedo = null, Action afterUndo = null);

		void BeginRecordValue(object target, MemberInfo memberInfo);

		void BeginRecordValue(object target, object accessor, MemberInfo memberInfo);

		void EndRecordValue(object target, MemberInfo memberInfo, Action afterRedo = null, Action afterUndo = null);

		void EndRecordValue(object target, object accessor, MemberInfo memberInfo, Action<object, object> targetErased = null, Action afterRedo = null, Action afterUndo = null);

		void RecordValue(object target, MemberInfo memberInfo, Action afterRedo = null, Action afterUndo = null);

		void RecordValue(object target, object accessor, MemberInfo memberInfo, Action afterRedo = null, Action afterUndo = null);

		void BeginRecordTransform(Transform target, Action<Transform> afterUndo = null);

		void EndRecordTransform(Transform target, Action<Transform> afterRedo = null);

		void BeginRecordTransform(Transform target, Transform parent, int siblingIndex = -1, Action<Transform> afterUndo = null);

		void EndRecordTransform(Transform target, Transform parent, int siblingIndex = -1, Action<Transform> afterRedo = null);

		Component AddComponent(ExposeToEditor obj, Type type);

		Component[] AddComponentWithRequirements(ExposeToEditor obj, Type type);

		void DestroyComponent(Component destroy, MemberInfo[] memberInfo);
	}
}
