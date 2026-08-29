using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battlehub.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Battlehub.RTCommon
{
	[DefaultExecutionOrder(-90)]
	public class RTEBase : MonoBehaviour, IRTE
	{
		[SerializeField]
		private CameraLayerSettings m_cameraLayerSettings = CameraLayerSettings.Default;

		[SerializeField]
		[HideInInspector]
		private bool m_createHierarchyRoot;

		[SerializeField]
		private bool m_useBuiltinUndo = true;

		[SerializeField]
		[HideInInspector]
		[Obsolete]
		private bool m_enableVRIfAvailable = true;

		[SerializeField]
		private bool m_isOpened = true;

		[SerializeField]
		private UnityEvent IsOpenedEvent;

		[SerializeField]
		private UnityEvent IsClosedEvent;

		private DisabledInput m_disabledInput;

		private IInput m_input;

		private IInput m_activeInput;

		private ITouchInput m_touchInput;

		private ITouchInput m_activeTouchInput;

		private RuntimeSelection m_selection;

		private RuntimeTools m_tools = new RuntimeTools();

		private CursorHelper m_cursorHelper = new CursorHelper();

		private IRuntimeUndo m_undo;

		private DragDrop m_dragDrop;

		private IRuntimeObjects m_object;

		protected GameObject m_currentSelectedGameObject;

		protected TMP_InputField m_currentInputFieldTMP;

		protected InputField m_currentInputFieldUI;

		protected float m_zAxis;

		private IUIRaycaster m_uiRaycaster;

		[SerializeField]
		protected EventSystem m_eventSystem;

		protected readonly HashSet<GameObject> m_windows = new HashSet<GameObject>();

		protected RuntimeWindow[] m_windowsArray;

		private RuntimeWindow m_activeWindow;

		private RuntimeWindow m_pointerOverWindow;

		private bool m_isDirty;

		private int m_counter;

		private bool m_isPlayModeStateChanging;

		private bool m_isPlaying;

		private bool m_isPaused;

		private bool m_appQuit;

		public IUIRaycaster Raycaster => m_uiRaycaster;

		public EventSystem EventSystem => m_eventSystem;

		public bool IsInputFieldActive
		{
			get
			{
				if (!(m_currentInputFieldTMP != null))
				{
					return m_currentInputFieldUI != null;
				}
				return true;
			}
		}

		public bool IsInputFieldFocused
		{
			get
			{
				if (m_currentInputFieldTMP != null)
				{
					return m_currentInputFieldTMP.isFocused;
				}
				if (m_currentInputFieldUI != null)
				{
					return m_currentInputFieldUI.isFocused;
				}
				return false;
			}
		}

		public virtual RuntimeWindow ActiveWindow => m_activeWindow;

		public virtual RuntimeWindow PointerOverWindow => m_pointerOverWindow;

		public virtual RuntimeWindow[] Windows => m_windowsArray;

		public virtual CameraLayerSettings CameraLayerSettings => m_cameraLayerSettings;

		public virtual bool IsVR { get; private set; }

		public virtual IInput Input => m_activeInput;

		public virtual ITouchInput TouchInput => m_activeTouchInput;

		public virtual IRuntimeSelection Selection => m_selection;

		public virtual IRuntimeUndo Undo => m_undo;

		public virtual RuntimeTools Tools => m_tools;

		public virtual CursorHelper CursorHelper => m_cursorHelper;

		public virtual IRuntimeObjects Object => m_object;

		public virtual IDragDrop DragDrop => m_dragDrop;

		public virtual bool IsDirty
		{
			get
			{
				return m_isDirty;
			}
			set
			{
				if (m_isDirty != value)
				{
					m_isDirty = value;
					if (this.IsDirtyChanged != null)
					{
						this.IsDirtyChanged();
					}
				}
			}
		}

		public virtual bool IsOpened
		{
			get
			{
				return m_isOpened;
			}
			set
			{
				if (m_isOpened == value || IsBusy)
				{
					return;
				}
				m_isOpened = value;
				SetInput();
				if (!m_isOpened)
				{
					IsPlaying = false;
				}
				if (!m_isOpened)
				{
					ActivateWindow(GetWindow(RuntimeWindowType.Game));
				}
				if (Root != null)
				{
					Root.gameObject.SetActive(m_isOpened);
				}
				if (this.IsOpenedChanged != null)
				{
					this.IsOpenedChanged();
				}
				if (m_isOpened)
				{
					if (IsOpenedEvent != null)
					{
						IsOpenedEvent.Invoke();
					}
				}
				else if (IsClosedEvent != null)
				{
					IsClosedEvent.Invoke();
				}
			}
		}

		public virtual bool IsBusy
		{
			get
			{
				return m_counter > 0;
			}
			set
			{
				int val = (value ? (m_counter + 1) : (m_counter - 1));
				val = Math.Max(0, val);
				if (m_counter != val)
				{
					m_counter = val;
					if (m_counter == 1)
					{
						Application.logMessageReceived += OnApplicationLogMessageReceived;
					}
					else if (m_counter == 0)
					{
						Application.logMessageReceived -= OnApplicationLogMessageReceived;
					}
					SetInput();
					if (this.IsBusyChanged != null)
					{
						this.IsBusyChanged();
					}
				}
			}
		}

		public virtual bool IsPlaymodeStateChanging => m_isPlayModeStateChanging;

		public virtual bool IsPlaying
		{
			get
			{
				return m_isPlaying;
			}
			set
			{
				if (!IsBusy && !(!m_isOpened && value) && m_isPlaying != value)
				{
					if (this.BeforePlaymodeStateChange != null)
					{
						this.BeforePlaymodeStateChange();
					}
					m_isPlayModeStateChanging = true;
					m_isPlaying = value;
					if (base.gameObject.activeInHierarchy)
					{
						StartCoroutine(CoIsPlayingChanged());
					}
					else
					{
						RaisePlaymodeStateChangeEvents();
					}
				}
			}
		}

		protected bool CreateHierarchyRoot
		{
			get
			{
				return m_createHierarchyRoot;
			}
			set
			{
				m_createHierarchyRoot = value;
			}
		}

		public virtual GameObject SceneRoot => HierarchyRoot;

		public virtual GameObject InstanceRoot => HierarchyRoot;

		public virtual GameObject HierarchyRoot { get; protected set; }

		public virtual Transform Root => base.transform;

		private static IRTE Instance
		{
			get
			{
				return IOC.Resolve<IRTE>("Instance");
			}
			set
			{
				if (value != null)
				{
					IOC.Register("Instance", value);
				}
				else
				{
					IOC.Unregister("Instance", Instance);
				}
			}
		}

		public bool IsApplicationPaused => m_isPaused;

		public event RTEEvent BeforePlaymodeStateChange;

		public event RTEEvent PlaymodeStateChanging;

		public event RTEEvent PlaymodeStateChanged;

		public event RTEEvent<RuntimeWindow> ActiveWindowChanging;

		public event RTEEvent<RuntimeWindow> ActiveWindowChanged;

		public event RTEEvent<RuntimeWindow> WindowRegistered;

		public event RTEEvent<RuntimeWindow> WindowUnregistered;

		public event RTEEvent IsOpenedChanged;

		public event RTEEvent IsDirtyChanged;

		public event RTEEvent IsBusyChanged;

		public event RTEEvent<GameObject[]> ObjectsRegistered;

		public event RTEEvent<GameObject[]> ObjectsDuplicated;

		public event RTEEvent<GameObject[]> ObjectsDeleted;

		public bool Contains(RuntimeWindow window)
		{
			return m_windows.Contains(window.gameObject);
		}

		private void OnApplicationLogMessageReceived(string condition, string stackTrace, LogType type)
		{
			if (type == LogType.Exception)
			{
				m_counter = 1;
				IsBusy = false;
			}
		}

		public BusyContext SetBusy()
		{
			return new BusyContext(this);
		}

		private IEnumerator CoIsPlayingChanged()
		{
			yield return new WaitForEndOfFrame();
			RaisePlaymodeStateChangeEvents();
		}

		private void RaisePlaymodeStateChangeEvents()
		{
			if (this.PlaymodeStateChanging != null)
			{
				this.PlaymodeStateChanging();
			}
			if (this.PlaymodeStateChanged != null)
			{
				this.PlaymodeStateChanged();
			}
			m_isPlayModeStateChanging = false;
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Init()
		{
			IOC.RegisterFallback(RegisterRTE);
		}

		private static IRTE RegisterRTE()
		{
			if (Instance == null)
			{
				GameObject gameObject = new GameObject("RTE");
				gameObject.AddComponent<RTEBase>().BuildUp(gameObject);
			}
			return Instance;
		}

		protected virtual void BuildUp(GameObject editor)
		{
			editor.AddComponent<RTEGraphics>();
			GameObject obj = new GameObject("Scene");
			obj.transform.SetParent(editor.transform, worldPositionStays: false);
			obj.AddComponent<RectTransform>();
			obj.SetActive(value: false);
			obj.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
			RTESceneWindow rTESceneWindow = obj.AddComponent<RTESceneWindow>();
			if (Camera.main == null)
			{
				rTESceneWindow.Camera = new GameObject
				{
					name = "RTE SceneView Camera"
				}.AddComponent<Camera>();
			}
			else
			{
				rTESceneWindow.Camera = Camera.main;
			}
			obj.SetActive(value: true);
		}

		private void OnApplicationQuit()
		{
			m_appQuit = true;
			m_isPaused = true;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			if (m_dragDrop != null)
			{
				m_dragDrop.Reset();
			}
			if (!Application.isEditor)
			{
				m_isPaused = !hasFocus;
			}
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			m_isPaused = pauseStatus;
		}

		protected virtual void Awake()
		{
			if (Instance != null)
			{
				Debug.LogWarning("Another instance of RTE exists");
				return;
			}
			if (m_useBuiltinUndo)
			{
				m_undo = new RuntimeUndo(this);
			}
			else
			{
				m_undo = new DisabledUndo();
			}
			m_uiRaycaster = IOC.Resolve<IUIRaycaster>();
			if (m_uiRaycaster == null)
			{
				m_uiRaycaster = GetComponentInChildren<IUIRaycaster>();
			}
			IsVR = false;
			m_selection = new RuntimeSelection(this);
			m_dragDrop = new DragDrop(this);
			m_object = base.gameObject.GetComponent<RuntimeObjects>();
			m_disabledInput = new DisabledInput();
			m_activeInput = m_disabledInput;
			m_activeTouchInput = m_disabledInput;
			Instance = this;
			bool isOpened = m_isOpened;
			m_isOpened = !isOpened;
			IsOpened = isOpened;
			TryCreateHierarchyRoot();
			if (m_eventSystem == null)
			{
				m_eventSystem = UnityObjectExt.FindAnyObjectByType<EventSystem>();
			}
		}

		protected virtual void Start()
		{
			InputLow inputLow = new InputLow();
			m_input = IOC.Resolve<IInput>();
			if (m_input == null)
			{
				m_input = inputLow;
			}
			m_touchInput = IOC.Resolve<ITouchInput>();
			if (m_touchInput == null)
			{
				m_touchInput = inputLow;
			}
			SetInput();
			if (m_eventSystem == null)
			{
				m_eventSystem = UnityObjectExt.FindAnyObjectByType<EventSystem>();
			}
			if (m_object == null)
			{
				m_object = base.gameObject.AddComponent<RuntimeObjects>();
			}
		}

		protected virtual void OnDestroy()
		{
			IsOpened = false;
			if (m_object != null)
			{
				m_object = null;
			}
			if (m_dragDrop != null)
			{
				m_dragDrop.Reset();
			}
			if (Instance == this)
			{
				Instance = null;
			}
		}

		private void SetInput()
		{
			if (!IsOpened || IsBusy || m_input == null)
			{
				m_activeInput = m_disabledInput;
			}
			else
			{
				m_activeInput = m_input;
			}
			if (!IsOpened || IsBusy || m_touchInput == null)
			{
				m_activeTouchInput = m_disabledInput;
			}
			else
			{
				m_activeTouchInput = m_touchInput;
			}
		}

		public void RegisterWindow(RuntimeWindow window)
		{
			if (!m_windows.Contains(window.gameObject))
			{
				m_windows.Add(window.gameObject);
			}
			this.WindowRegistered?.Invoke(window);
			m_windowsArray = m_windows.Select((GameObject w) => w.GetComponent<RuntimeWindow>()).ToArray();
			if (m_windows.Count == 1)
			{
				ActivateWindow(window);
			}
		}

		public void UnregisterWindow(RuntimeWindow window)
		{
			m_windows.Remove(window.gameObject);
			if (IsApplicationPaused)
			{
				return;
			}
			this.WindowUnregistered?.Invoke(window);
			if (m_activeWindow == window)
			{
				RuntimeWindow runtimeWindow = (from w in m_windows
					select w.GetComponent<RuntimeWindow>() into w
					where w.WindowType == window.WindowType && w.WindowType != RuntimeWindowType.Custom
					select w).FirstOrDefault();
				if (runtimeWindow == null)
				{
					runtimeWindow = m_windows.Select((GameObject w) => w.GetComponent<RuntimeWindow>()).FirstOrDefault();
				}
				if (IsOpened)
				{
					ActivateWindow(runtimeWindow);
				}
			}
			m_windowsArray = m_windows.Select((GameObject w) => w.GetComponent<RuntimeWindow>()).ToArray();
		}

		protected virtual void Update()
		{
			UpdateCurrentInputField();
			bool flag = false;
			if (m_zAxis != (float)Mathf.CeilToInt(Mathf.Abs(Input.GetAxis(InputAxis.Z))))
			{
				flag = m_zAxis == 0f;
				m_zAxis = Mathf.CeilToInt(Mathf.Abs(Input.GetAxis(InputAxis.Z)));
			}
			bool flag2 = Input.GetPointerDown(0) || Input.GetPointerDown(1) || Input.GetPointerDown(2) || Input.GetPointerUp(0);
			if (!(flag2 || flag) && (!Input.IsAnyKeyDown() || IsInputFieldFocused))
			{
				return;
			}
			if (m_uiRaycaster == null)
			{
				if (IsPointerOverGameObject())
				{
					ActivateWindow(null);
					return;
				}
				RuntimeWindow runtimeWindow = GetWindow(RuntimeWindowType.Scene);
				if (runtimeWindow != null && (runtimeWindow.Camera == null || !runtimeWindow.Camera.isActiveAndEnabled))
				{
					runtimeWindow = null;
				}
				ActivateWindow(runtimeWindow);
				return;
			}
			List<RaycastResult> list = new List<RaycastResult>();
			m_uiRaycaster.Raycast(list);
			IEnumerable<Selectable> source = from r in list
				select r.gameObject.GetComponent<Selectable>() into s
				where s != null
				select s;
			if (source.Count() == 1)
			{
				Selectable selectable = source.First();
				if (selectable != null)
				{
					selectable.Select();
				}
			}
			foreach (RaycastResult item in list)
			{
				if (m_windows.Contains(item.gameObject))
				{
					RuntimeWindow component = item.gameObject.GetComponent<RuntimeWindow>();
					if (flag2 || component.ActivateOnAnyKey)
					{
						ActivateWindow(component);
						break;
					}
				}
			}
		}

		private bool IsPointerOverGameObject()
		{
			if (m_eventSystem == null)
			{
				return false;
			}
			if (m_eventSystem.IsPointerOverGameObject())
			{
				return true;
			}
			if (TouchInput != null)
			{
				for (int i = 0; i < TouchInput.TouchCount; i++)
				{
					Touch touch = TouchInput.GetTouch(i);
					if (m_eventSystem.IsPointerOverGameObject(touch.fingerId))
					{
						return true;
					}
				}
			}
			return false;
		}

		public void UpdateCurrentInputField()
		{
			if (m_eventSystem != null && m_eventSystem.currentSelectedGameObject != null && m_eventSystem.currentSelectedGameObject.activeInHierarchy)
			{
				if (!(m_eventSystem.currentSelectedGameObject != m_currentSelectedGameObject))
				{
					return;
				}
				m_currentSelectedGameObject = m_eventSystem.currentSelectedGameObject;
				if (m_currentSelectedGameObject != null)
				{
					m_currentInputFieldTMP = m_currentSelectedGameObject.GetComponent<TMP_InputField>();
					if (m_currentInputFieldTMP == null)
					{
						m_currentInputFieldUI = m_currentSelectedGameObject.GetComponent<InputField>();
					}
					return;
				}
				if (m_currentInputFieldTMP != null)
				{
					m_currentInputFieldTMP.DeactivateInputField();
				}
				m_currentInputFieldTMP = null;
				if (m_currentInputFieldUI != null)
				{
					m_currentInputFieldUI.DeactivateInputField();
				}
				m_currentInputFieldUI = null;
			}
			else
			{
				m_currentSelectedGameObject = null;
				if (m_currentInputFieldTMP != null)
				{
					m_currentInputFieldTMP.DeactivateInputField();
				}
				m_currentInputFieldTMP = null;
				if (m_currentInputFieldUI != null)
				{
					m_currentInputFieldUI.DeactivateInputField();
				}
				m_currentInputFieldUI = null;
			}
		}

		public int GetIndex(RuntimeWindowType windowType)
		{
			IOrderedEnumerable<RuntimeWindow> orderedEnumerable = from w in m_windows
				select w.GetComponent<RuntimeWindow>() into w
				where w.WindowType == windowType
				orderby w.Index
				select w;
			int num = 0;
			foreach (RuntimeWindow item in orderedEnumerable)
			{
				if (item.Index != num)
				{
					return num;
				}
				num++;
			}
			return num;
		}

		public RuntimeWindow GetWindow(RuntimeWindowType window)
		{
			return m_windows.Select((GameObject w) => w.GetComponent<RuntimeWindow>()).FirstOrDefault((RuntimeWindow w) => w.WindowType == window);
		}

		public virtual void ActivateWindow(RuntimeWindowType windowType)
		{
			RuntimeWindow window = GetWindow(windowType);
			if (window != null)
			{
				ActivateWindow(window);
			}
		}

		public virtual void ActivateWindow(RuntimeWindow window)
		{
			if (m_activeWindow != window && (window == null || window.CanActivate))
			{
				RuntimeWindow activeWindow = m_activeWindow;
				this.ActiveWindowChanging?.Invoke(window);
				m_activeWindow = window;
				this.ActiveWindowChanged?.Invoke(activeWindow);
			}
		}

		public virtual void SetPointerOverWindow(RuntimeWindow window)
		{
			m_pointerOverWindow = window;
		}

		public void Close()
		{
			IsOpened = false;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		public virtual void RegisterCreatedObjects(GameObject[] gameObjects, bool select = true)
		{
			ExposeToEditor[] array = (from o in gameObjects
				select o.GetComponent<ExposeToEditor>() into o
				where o != null
				orderby o.transform.GetSiblingIndex() descending
				select o).ToArray();
			if (array.Length == 0)
			{
				if (select)
				{
					IRuntimeSelection selection = Selection;
					UnityEngine.Object[] objects = gameObjects;
					selection.objects = objects;
				}
				return;
			}
			bool isRecording = Undo.IsRecording;
			if (!isRecording)
			{
				Undo.BeginRecord();
			}
			if (array.Length == 0)
			{
				Debug.LogWarning("To register created object GameObject add ExposeToEditor script to it");
			}
			else
			{
				Undo.RegisterCreatedObjects(array);
			}
			if (select)
			{
				IRuntimeSelection selection2 = Selection;
				UnityEngine.Object[] objects = gameObjects;
				selection2.objects = objects;
			}
			if (!isRecording)
			{
				Undo.EndRecord();
			}
			RaiseObjectsRegistered(gameObjects);
		}

		protected void RaiseObjectsRegistered(GameObject[] gameObjects)
		{
			if (this.ObjectsRegistered != null)
			{
				this.ObjectsRegistered(gameObjects);
			}
		}

		public virtual void AddGameObjectToHierarchy(GameObject go, bool scaleStays = true)
		{
			if (!m_createHierarchyRoot)
			{
				return;
			}
			GameObject instanceRoot = InstanceRoot;
			if (!(instanceRoot != null))
			{
				return;
			}
			Transform parent = go.transform.parent;
			while (parent != null)
			{
				parent = parent.parent;
				if (parent == instanceRoot.transform)
				{
					return;
				}
			}
			Vector3 localScale = go.transform.localScale;
			go.transform.SetParent(instanceRoot.transform, worldPositionStays: true);
			if (scaleStays)
			{
				go.transform.localScale = localScale;
			}
		}

		public virtual void Duplicate(GameObject[] gameObjects)
		{
			DuplicateAsync(gameObjects);
		}

		public virtual Task DuplicateAsync(GameObject[] gameObjects)
		{
			if (gameObjects == null || gameObjects.Length == 0)
			{
				return Task.CompletedTask;
			}
			List<GameObject> list = new List<GameObject>();
			foreach (GameObject gameObject in gameObjects)
			{
				if (!(gameObject == null))
				{
					ExposeToEditor component = gameObject.GetComponent<ExposeToEditor>();
					if (!(component != null) || component.CanDuplicate)
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
						gameObject2.SetActive(value: true);
						gameObject2.SetActive(gameObject.activeSelf);
						list.Add(gameObject2);
					}
				}
			}
			GameObject[] array = list.ToArray();
			if (array.Length != 0)
			{
				ExposeToEditor[] createdObjects = (from o in list
					select o.GetComponent<ExposeToEditor>() into o
					orderby o.transform.GetSiblingIndex() descending
					select o).ToArray();
				Undo.BeginRecord();
				Undo.RegisterCreatedObjects(createdObjects);
				IRuntimeSelection selection = Selection;
				UnityEngine.Object[] objects = array;
				selection.objects = objects;
				Undo.EndRecord();
			}
			RaiseObjectsDuplicated(array);
			return Task.CompletedTask;
		}

		protected void RaiseObjectsDuplicated(GameObject[] duplicatesArray)
		{
			if (this.ObjectsDuplicated != null)
			{
				this.ObjectsDuplicated(duplicatesArray);
			}
		}

		public virtual void Delete(GameObject[] gameObjects)
		{
			DeleteAsync(gameObjects);
		}

		public virtual Task DeleteAsync(GameObject[] gameObjects)
		{
			if (gameObjects == null || gameObjects.Length == 0)
			{
				return Task.CompletedTask;
			}
			ExposeToEditor[] array = (from o in gameObjects
				select o.GetComponent<ExposeToEditor>() into o
				where o != null && o.CanDelete
				orderby o.transform.GetSiblingIndex() descending
				select o).ToArray();
			if (array.Length == 0)
			{
				return Task.CompletedTask;
			}
			HashSet<GameObject> source = new HashSet<GameObject>(array.Select((ExposeToEditor exposed) => exposed.gameObject));
			bool isRecording = Undo.IsRecording;
			if (!isRecording)
			{
				Undo.BeginRecord();
			}
			if (Selection.objects != null)
			{
				List<UnityEngine.Object> list = Selection.objects.ToList();
				for (int num = list.Count - 1; num >= 0; num--)
				{
					if (source.Contains(list[num]))
					{
						list.RemoveAt(num);
					}
				}
				Selection.objects = list.ToArray();
			}
			Undo.DestroyObjects(array);
			if (!isRecording)
			{
				Undo.EndRecord();
			}
			RaiseObjectsDeleted(gameObjects);
			return Task.CompletedTask;
		}

		protected void RaiseObjectsDeleted(GameObject[] gameObjects)
		{
			if (this.ObjectsDeleted != null)
			{
				this.ObjectsDeleted(gameObjects);
			}
		}

		protected void TryCreateHierarchyRoot()
		{
			if (m_createHierarchyRoot)
			{
				GameObject gameObject = FindHierarchyRoot();
				if (gameObject == null)
				{
					gameObject = new GameObject("Scene");
				}
				gameObject.transform.position = Vector3.zero;
				gameObject.tag = ExposeToEditor.HierarchyRootTag;
				HierarchyRoot = gameObject;
			}
		}

		internal static GameObject FindHierarchyRoot()
		{
			GameObject result = null;
			try
			{
				result = GameObject.FindGameObjectWithTag(ExposeToEditor.HierarchyRootTag);
			}
			catch (Exception)
			{
				Debug.LogWarning("Add '" + ExposeToEditor.HierarchyRootTag + "' tag in Tags & Layers Window.");
				Debug.LogWarning("Using 'Respawn' tag instead of '" + ExposeToEditor.HierarchyRootTag + "'");
				ExposeToEditor.HierarchyRootTag = "Respawn";
			}
			return result;
		}

		Coroutine IRTE.StartCoroutine(IEnumerator method)
		{
			return StartCoroutine(method);
		}

		void IRTE.StopCoroutine(IEnumerator method)
		{
			StopCoroutine(method);
		}
	}
}
