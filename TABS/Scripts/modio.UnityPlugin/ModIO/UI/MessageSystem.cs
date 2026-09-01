using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	public class MessageSystem : MonoBehaviour
	{
		private static MessageSystem _instance;

		[Header("Settings")]
		[Tooltip("Default base time to display a message (in seconds)")]
		public float defaultBaseTime = 1f;

		[Tooltip("Additional time per character in the message (in seconds)")]
		public float defaultCharacterTime = 0.1f;

		[Header("UI Components")]
		public MessageDisplay successDialog;

		public MessageDisplay warningDialog;

		public MessageDisplay errorDialog;

		public MessageDisplay infoDialog;

		[Header("Display Data")]
		public List<MessageDisplayData> queuedMessages;

		private Dictionary<MessageDisplayData.Type, MessageDisplay> m_typeDialogMap = new Dictionary<MessageDisplayData.Type, MessageDisplay>();

		private Coroutine m_displayRoutine;

		private bool m_cancelCurrentMessage;

		public static MessageSystem instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = Object.FindObjectOfType<MessageSystem>();
				}
				return _instance;
			}
		}

		private void OnEnable()
		{
			_instance = this;
			m_typeDialogMap.Clear();
			if (infoDialog != null)
			{
				infoDialog.gameObject.SetActive(value: false);
				m_typeDialogMap[MessageDisplayData.Type.Info] = infoDialog;
				infoDialog.onClick -= OnMessageDisplayClicked;
				infoDialog.onClick += OnMessageDisplayClicked;
			}
			else
			{
				m_typeDialogMap[MessageDisplayData.Type.Info] = null;
			}
			if (successDialog != null)
			{
				successDialog.gameObject.SetActive(value: false);
				m_typeDialogMap[MessageDisplayData.Type.Success] = successDialog;
				successDialog.onClick -= OnMessageDisplayClicked;
				successDialog.onClick += OnMessageDisplayClicked;
			}
			else
			{
				m_typeDialogMap[MessageDisplayData.Type.Success] = null;
			}
			if (warningDialog != null)
			{
				warningDialog.gameObject.SetActive(value: false);
				m_typeDialogMap[MessageDisplayData.Type.Warning] = warningDialog;
				warningDialog.onClick -= OnMessageDisplayClicked;
				warningDialog.onClick += OnMessageDisplayClicked;
			}
			else
			{
				m_typeDialogMap[MessageDisplayData.Type.Warning] = null;
			}
			if (errorDialog != null)
			{
				errorDialog.gameObject.SetActive(value: false);
				m_typeDialogMap[MessageDisplayData.Type.Error] = errorDialog;
				errorDialog.onClick -= OnMessageDisplayClicked;
				errorDialog.onClick += OnMessageDisplayClicked;
			}
			else
			{
				m_typeDialogMap[MessageDisplayData.Type.Error] = null;
			}
			queuedMessages = new List<MessageDisplayData>();
		}

		private void OnDisable()
		{
			if (m_displayRoutine != null)
			{
				StopCoroutine(m_displayRoutine);
				m_displayRoutine = null;
			}
			if (_instance == this)
			{
				_instance = null;
			}
		}

		public static void QueueMessage(MessageDisplayData.Type messageType, string messageContent, float displayDuration = 0f)
		{
			if (!(instance == null))
			{
				if (displayDuration <= 0f)
				{
					displayDuration = instance.defaultBaseTime + (float)messageContent.Length * instance.defaultCharacterTime;
				}
				MessageDisplayData item = new MessageDisplayData
				{
					type = messageType,
					content = messageContent,
					displayDuration = displayDuration
				};
				instance.queuedMessages.Add(item);
				if (Application.isPlaying && instance.isActiveAndEnabled && instance.m_displayRoutine == null)
				{
					instance.m_displayRoutine = instance.StartCoroutine(instance.DisplayNextMessageRoutine());
				}
			}
		}

		private IEnumerator DisplayNextMessageRoutine()
		{
			MessageDisplayData message = queuedMessages[0];
			MessageDisplay dialog = m_typeDialogMap[message.type];
			ToastAnimationSettings anim = dialog.GetComponent<ToastAnimationSettings>();
			RectTransform rectTransform = dialog.GetComponent<RectTransform>();
			Vector2 origin = rectTransform.anchoredPosition;
			if (dialog != null)
			{
				dialog.content.text = message.content;
				dialog.gameObject.SetActive(value: true);
				if (anim != null)
				{
					for (float animTimer = 0f; animTimer < anim.duration; animTimer += Time.unscaledDeltaTime)
					{
						rectTransform.anchoredPosition = Vector2.Lerp(origin + anim.offset, origin, animTimer / anim.duration);
						yield return null;
					}
					rectTransform.anchoredPosition = origin;
				}
				float displayTimer = 0f;
				m_cancelCurrentMessage = false;
				for (; displayTimer < message.displayDuration; displayTimer += Time.unscaledDeltaTime)
				{
					if (m_cancelCurrentMessage)
					{
						break;
					}
					yield return null;
				}
				if (anim != null)
				{
					for (float animTimer = 0f; animTimer < anim.duration; animTimer += Time.unscaledDeltaTime)
					{
						rectTransform.anchoredPosition = Vector2.Lerp(origin, origin + anim.offset, animTimer / anim.duration);
						yield return null;
					}
					rectTransform.anchoredPosition = origin;
				}
				dialog.gameObject.SetActive(value: false);
			}
			queuedMessages.Remove(message);
			m_displayRoutine = null;
			if (Application.isPlaying && this != null && base.isActiveAndEnabled && queuedMessages.Count > 0)
			{
				m_displayRoutine = StartCoroutine(DisplayNextMessageRoutine());
			}
		}

		private void OnMessageDisplayClicked(MessageDisplay display)
		{
			if (instance.queuedMessages.Count > 0 && instance.queuedMessages[0].content == display.content.text)
			{
				m_cancelCurrentMessage = true;
			}
		}
	}
}
