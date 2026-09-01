using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class LoginDialog : MonoBehaviour, IBrowserView
	{
		public enum LoginResult
		{
			Success = 0,
			Cancelled = 1,
			Failed = 2
		}

		[Serializable]
		public struct InputStateDisplays
		{
			public GameObject invalid;

			public GameObject email;

			public GameObject securityCode;
		}

		public Action<LoginResult> dialogCallback;

		[Tooltip("Invalid Submission Message")]
		[Header("Settings")]
		public string invalidSubmissionMessage = "Input needs to be either a valid email address or the 5-Digit authentication code.";

		[Tooltip("Email Refused Message")]
		public string emailRefusedMessage = "The email address was rejected by the mod.io server.\nPlease correct any mistakes, or try another email address.";

		[Tooltip("Objects to toggle depending on the state of the input field validation.")]
		[Header("UI Components")]
		public InputStateDisplays displayForInputState;

		public InputField inputField;

		private List<Selectable> m_onFocusPriority = new List<Selectable>();

		bool IBrowserView.resetSelectionOnHide
		{
			get
			{
				return true;
			}
		}

		bool IBrowserView.isRootView
		{
			get
			{
				return false;
			}
		}

		List<Selectable> IBrowserView.onFocusPriority
		{
			get
			{
				return m_onFocusPriority;
			}
		}

		public CanvasGroup canvasGroup
		{
			get
			{
				return base.gameObject.GetComponent<CanvasGroup>();
			}
		}

		virtual GameObject IBrowserView.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		[Obsolete("No longer trigger by this object.")]
		public event Action<string> onInvalidSubmissionAttempted;

		[Obsolete("No longer trigger by this object.")]
		public event Action<string> onEmailRefused;

		[Obsolete("No longer trigger by this object.")]
		public event Action<APIMessage> onSecurityCodeSent;

		[Obsolete("No longer trigger by this object.")]
		public event Action<string> onSecurityCodeRefused;

		[Obsolete("No longer trigger by this object.")]
		public event Action<string> onUserOAuthTokenReceived;

		[Obsolete("No longer trigger by this object.")]
		public event Action<WebRequestError> onWebRequestError;

		private void Awake()
		{
			m_onFocusPriority = new List<Selectable> { inputField };
		}

		private void OnEnable()
		{
			OnTextInputUpdated();
		}

		private void Start()
		{
			inputField.onEndEdit.AddListener(delegate
			{
				if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
				{
					TrySubmitAuthentication();
				}
			});
		}

		public void OnTextInputUpdated()
		{
			string toCheck = inputField.text.Trim().Replace(" ", string.Empty);
			bool flag = Utility.IsEmail(toCheck);
			bool flag2 = !flag && Utility.IsSecurityCode(toCheck);
			if (displayForInputState.invalid != null)
			{
				displayForInputState.invalid.SetActive(!flag && !flag2);
			}
			if (displayForInputState.email != null)
			{
				displayForInputState.email.SetActive(flag);
			}
			if (displayForInputState.securityCode != null)
			{
				displayForInputState.securityCode.SetActive(flag2);
			}
		}

		public void TrySubmitAuthentication()
		{
			string text = inputField.text.Trim();
			inputField.interactable = false;
			if (Utility.IsEmail(text))
			{
				APIClient.SendSecurityCode(text, OnSecurityCodeSent, delegate(WebRequestError e)
				{
					ProcessWebRequestError(e, false);
				});
			}
			else if (Utility.IsSecurityCode(text))
			{
				UserAccountManagement.AuthenticateWithSecurityCode(text.ToUpper(), OnAuthenticated, delegate(WebRequestError e)
				{
					ProcessWebRequestError(e, true);
				});
			}
			else
			{
				StartCoroutine(DisableInteractivity(2f));
				MessageSystem.QueueMessage(MessageDisplayData.Type.Error, invalidSubmissionMessage);
			}
		}

		private void OnSecurityCodeSent(APIMessage apiMessage)
		{
			inputField.text = string.Empty;
			inputField.interactable = true;
			MessageSystem.QueueMessage(MessageDisplayData.Type.Success, apiMessage.message);
		}

		private void OnAuthenticated(UserProfile u)
		{
			inputField.text = string.Empty;
			inputField.interactable = true;
			MessageSystem.QueueMessage(MessageDisplayData.Type.Success, "Login Successful");
			ViewManager.instance.CloseWindowedView(this);
			if (dialogCallback != null)
			{
				dialogCallback(LoginResult.Success);
			}
			ModBrowser.instance.OnUserLogin();
		}

		private void ProcessWebRequestError(WebRequestError e, bool isSecurityCode)
		{
			if (e.webRequest.responseCode == 401 && isSecurityCode)
			{
				MessageSystem.QueueMessage(MessageDisplayData.Type.Error, e.errorMessage);
			}
			else if (e.webRequest.responseCode == 422 && !isSecurityCode)
			{
				MessageSystem.QueueMessage(MessageDisplayData.Type.Error, emailRefusedMessage);
			}
			else
			{
				MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, e.displayMessage);
			}
			inputField.interactable = true;
		}

		private IEnumerator DisableInteractivity(float seconds)
		{
			inputField.interactable = false;
			yield return new WaitForSecondsRealtime(seconds);
			inputField.interactable = true;
		}
	}
}
