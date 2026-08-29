using System.Collections;
using System.Threading.Tasks;
using Battlehub.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battlehub.RTCommon
{
	public interface IRTE
	{
		CameraLayerSettings CameraLayerSettings { get; }

		IUIRaycaster Raycaster { get; }

		EventSystem EventSystem { get; }

		bool IsVR { get; }

		IInput Input { get; }

		ITouchInput TouchInput { get; }

		IRuntimeSelection Selection { get; }

		IRuntimeUndo Undo { get; }

		RuntimeTools Tools { get; }

		CursorHelper CursorHelper { get; }

		IRuntimeObjects Object { get; }

		IDragDrop DragDrop { get; }

		bool IsDirty { get; set; }

		bool IsOpened { get; set; }

		bool IsBusy { get; set; }

		bool IsPlaymodeStateChanging { get; }

		bool IsPlaying { get; set; }

		bool IsApplicationPaused { get; }

		GameObject SceneRoot { get; }

		GameObject InstanceRoot { get; }

		GameObject HierarchyRoot { get; }

		Transform Root { get; }

		bool IsInputFieldActive { get; }

		bool IsInputFieldFocused { get; }

		RuntimeWindow ActiveWindow { get; }

		RuntimeWindow PointerOverWindow { get; }

		RuntimeWindow[] Windows { get; }

		event RTEEvent BeforePlaymodeStateChange;

		event RTEEvent PlaymodeStateChanging;

		event RTEEvent PlaymodeStateChanged;

		event RTEEvent<RuntimeWindow> ActiveWindowChanging;

		event RTEEvent<RuntimeWindow> ActiveWindowChanged;

		event RTEEvent<RuntimeWindow> WindowRegistered;

		event RTEEvent<RuntimeWindow> WindowUnregistered;

		event RTEEvent IsBusyChanged;

		event RTEEvent IsOpenedChanged;

		event RTEEvent IsDirtyChanged;

		event RTEEvent<GameObject[]> ObjectsRegistered;

		event RTEEvent<GameObject[]> ObjectsDuplicated;

		event RTEEvent<GameObject[]> ObjectsDeleted;

		BusyContext SetBusy();

		void UpdateCurrentInputField();

		bool Contains(RuntimeWindow window);

		int GetIndex(RuntimeWindowType windowType);

		RuntimeWindow GetWindow(RuntimeWindowType windowType);

		void ActivateWindow(RuntimeWindowType window);

		void ActivateWindow(RuntimeWindow window);

		void SetPointerOverWindow(RuntimeWindow window);

		void RegisterWindow(RuntimeWindow window);

		void UnregisterWindow(RuntimeWindow window);

		void Close();

		Coroutine StartCoroutine(IEnumerator method);

		void StopCoroutine(IEnumerator method);

		void RegisterCreatedObjects(GameObject[] gameObjects, bool select = true);

		void AddGameObjectToHierarchy(GameObject gameObject, bool scaleStays = true);

		void Duplicate(GameObject[] gameObjects);

		Task DuplicateAsync(GameObject[] gameObjects);

		void Delete(GameObject[] gameObjects);

		Task DeleteAsync(GameObject[] gameObjects);
	}
}
