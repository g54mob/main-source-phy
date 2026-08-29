using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTCommon
{
	[DefaultExecutionOrder(-100)]
	public class CreateEditor : MonoBehaviour, IRTEState
	{
		[SerializeField]
		private Button m_createEditorButton;

		[SerializeField]
		private RTEBase m_editorPrefab;

		[SerializeField]
		private Splash m_splashPrefab;

		private RTEBase m_editor;

		public bool IsCreated => m_editor != null;

		public event Action<object> Created;

		public event Action<object> Destroyed;

		private void Awake()
		{
			IOC.RegisterFallback((IRTEState)this);
			m_editor = (RTEBase)UnityObjectExt.FindAnyObjectByType(m_editorPrefab.GetType());
			if (m_editor != null && m_editor.IsOpened)
			{
				m_editor.IsOpenedChanged += OnIsOpenedChanged;
				base.gameObject.SetActive(value: false);
			}
			m_createEditorButton.onClick.AddListener(OnOpen);
		}

		private void OnDestroy()
		{
			IOC.UnregisterFallback((IRTEState)this);
			if (m_createEditorButton != null)
			{
				m_createEditorButton.onClick.RemoveListener(OnOpen);
			}
			if (m_editor != null)
			{
				m_editor.IsOpenedChanged -= OnIsOpenedChanged;
			}
		}

		private void OnOpen()
		{
			if (m_splashPrefab != null)
			{
				m_createEditorButton.gameObject.SetActive(value: false);
				UnityEngine.Object.Instantiate(m_splashPrefab).Show(delegate
				{
					InstantiateRuntimeEditor();
				});
			}
			else
			{
				InstantiateRuntimeEditor();
			}
		}

		private void InstantiateRuntimeEditor()
		{
			m_editor = UnityEngine.Object.Instantiate(m_editorPrefab);
			m_editor.name = "RuntimeEditor";
			m_editor.IsOpenedChanged += OnIsOpenedChanged;
			m_editor.transform.SetAsFirstSibling();
			if (this.Created != null)
			{
				this.Created(m_editor);
			}
			base.gameObject.SetActive(value: false);
		}

		private void OnIsOpenedChanged()
		{
			if (m_editor != null && !m_editor.IsOpened)
			{
				m_createEditorButton.gameObject.SetActive(value: true);
				m_editor.IsOpenedChanged -= OnIsOpenedChanged;
				if (this != null)
				{
					base.gameObject.SetActive(value: true);
				}
				UnityEngine.Object.Destroy(m_editor);
				if (this.Destroyed != null)
				{
					this.Destroyed(m_editor);
				}
			}
		}
	}
}
