using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleFileBrowser
{
	public class FileBrowserRenamedItem : MonoBehaviour
	{
		public delegate void OnRenameCompleted(string filename);

		[SerializeField]
		private Image background;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private TMP_InputField nameInputField;

		private OnRenameCompleted onRenameCompleted;

		private RectTransform m_transform;

		public TMP_InputField InputField => nameInputField;

		public RectTransform TransformComponent
		{
			get
			{
				if (m_transform == null)
				{
					m_transform = (RectTransform)base.transform;
				}
				return m_transform;
			}
		}

		private void Awake()
		{
			nameInputField.onEndEdit.AddListener(OnInputFieldEndEdit);
		}

		public void Show(string initialFilename, Color backgroundColor, Sprite icon, OnRenameCompleted onRenameCompleted)
		{
			background.color = backgroundColor;
			this.icon.sprite = icon;
			this.onRenameCompleted = onRenameCompleted;
			base.transform.SetAsLastSibling();
			base.gameObject.SetActive(value: true);
			nameInputField.text = initialFilename;
			nameInputField.ActivateInputField();
		}

		private void LateUpdate()
		{
			if (Input.mouseScrollDelta.y != 0f)
			{
				nameInputField.DeactivateInputField();
			}
		}

		private void OnInputFieldEndEdit(string filename)
		{
			base.gameObject.SetActive(value: false);
			if ((bool)EventSystem.current && !EventSystem.current.alreadySelecting && EventSystem.current.currentSelectedGameObject == nameInputField.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			if (onRenameCompleted != null)
			{
				onRenameCompleted(filename);
			}
		}
	}
}
