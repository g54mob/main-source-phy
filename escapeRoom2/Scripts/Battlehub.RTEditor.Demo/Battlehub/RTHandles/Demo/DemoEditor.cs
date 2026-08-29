using System;
using System.Collections;
using System.Linq;
using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTHandles.Demo
{
	[DefaultExecutionOrder(-10)]
	public class DemoEditor : SimpleEditor, IRTEState
	{
		[SerializeField]
		private Button m_focusButton;

		[SerializeField]
		private Button m_deleteButton;

		[SerializeField]
		private Button m_play;

		[SerializeField]
		private Button m_stop;

		[SerializeField]
		private GameObject m_components;

		[SerializeField]
		private GameObject m_ui;

		[SerializeField]
		private GameObject m_prefabSpawnPoints;

		[SerializeField]
		private GameObject m_editorCamera;

		[SerializeField]
		private GameObject m_gameCamera;

		private IResourcePreviewUtility m_resourcePreview;

		public bool IsCreated => true;

		public event Action<object> Created;

		public event Action<object> Destroyed;

		protected override void Awake()
		{
			base.Awake();
			IOC.Register((IRTEState)this);
			m_resourcePreview = GetComponent<IResourcePreviewUtility>();
			if (m_resourcePreview == null)
			{
				m_resourcePreview = base.gameObject.AddComponent<ResourcePreviewUtility>();
			}
			IOC.Register(m_resourcePreview);
		}

		protected override void Start()
		{
			base.Start();
			base.Editor.IsOpened = true;
			base.Editor.IsPlaying = false;
			OnPlaymodeStateChanged();
			base.Editor.PlaymodeStateChanged += OnPlaymodeStateChanged;
			base.Editor.Selection.SelectionChanged += OnSelectionChanged;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (base.Editor != null)
			{
				base.Editor.PlaymodeStateChanged -= OnPlaymodeStateChanged;
				base.Editor.Selection.SelectionChanged -= OnSelectionChanged;
			}
			IOC.Unregister((IRTEState)this);
			IOC.Unregister(m_resourcePreview);
		}

		protected virtual void Update()
		{
			if (base.Editor.Input.GetKeyDown(KeyCode.Delete))
			{
				DeleteSelected();
			}
		}

		protected override void SubscribeUIEvents()
		{
			base.SubscribeUIEvents();
			if (m_play != null)
			{
				m_play.onClick.AddListener(OnPlayClick);
			}
			if (m_stop != null)
			{
				m_stop.onClick.AddListener(OnStopClick);
			}
			if (m_focusButton != null)
			{
				m_focusButton.onClick.AddListener(OnFocusClick);
			}
			if (m_deleteButton != null)
			{
				m_deleteButton.onClick.AddListener(OnDeleteClick);
			}
		}

		protected override void UnsubscribeUIEvents()
		{
			base.UnsubscribeUIEvents();
			if (m_play != null)
			{
				m_play.onClick.RemoveListener(OnPlayClick);
			}
			if (m_stop != null)
			{
				m_stop.onClick.RemoveListener(OnStopClick);
			}
			if (m_focusButton != null)
			{
				m_focusButton.onClick.RemoveListener(OnFocusClick);
			}
			if (m_deleteButton != null)
			{
				m_deleteButton.onClick.RemoveListener(OnDeleteClick);
			}
		}

		protected virtual void OnPlaymodeStateChanged()
		{
			if (m_components != null)
			{
				m_components.SetActive(!base.Editor.IsPlaying);
			}
			if (m_ui != null)
			{
				m_ui.SetActive(!base.Editor.IsPlaying);
			}
			if (m_prefabSpawnPoints != null)
			{
				m_prefabSpawnPoints.SetActive(!base.Editor.IsPlaying);
			}
			if (m_stop != null)
			{
				m_stop.gameObject.SetActive(base.Editor.IsPlaying);
			}
			if (m_editorCamera != null)
			{
				m_editorCamera.SetActive(!base.Editor.IsPlaying);
			}
			if (m_gameCamera != null)
			{
				m_gameCamera.SetActive(base.Editor.IsPlaying);
			}
		}

		private void OnSelectionChanged(UnityEngine.Object[] unselectedObjects)
		{
			if (m_focusButton != null)
			{
				m_focusButton.interactable = base.Editor.Selection.Length > 0;
			}
			if (m_deleteButton != null)
			{
				m_deleteButton.interactable = base.Editor.Selection.Length > 0;
			}
		}

		private void OnFocusClick()
		{
			base.Editor.GetWindow(RuntimeWindowType.Scene).IOCContainer.Resolve<IScenePivot>().Focus(FocusMode.Selected);
		}

		private void OnPlayClick()
		{
			base.Editor.Undo.Purge();
			StartCoroutine(CoPlay());
		}

		private IEnumerator CoPlay()
		{
			yield return new WaitForEndOfFrame();
			base.Editor.IsPlaying = true;
		}

		private void OnStopClick()
		{
			base.Editor.IsPlaying = false;
		}

		private void OnDeleteClick()
		{
			DeleteSelected();
		}

		private void DeleteSelected()
		{
			if (base.Editor.Selection.Length > 0)
			{
				ExposeToEditor[] destoryedObjects = (from o in base.Editor.Selection.gameObjects
					where o != null
					select o.GetComponent<ExposeToEditor>() into o
					where o != null
					select o).ToArray();
				base.Editor.Undo.BeginRecord();
				base.Editor.Selection.objects = null;
				base.Editor.Undo.DestroyObjects(destoryedObjects);
				base.Editor.Undo.EndRecord();
			}
		}

		private void Use()
		{
			this.Created(null);
			this.Destroyed(null);
		}
	}
}
