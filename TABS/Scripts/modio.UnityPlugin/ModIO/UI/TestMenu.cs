using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class TestMenu : MonoBehaviour
	{
		public ModBrowser modBrowser;

		public GameObject loadingIcon;

		public Text messageBox;

		public int messageBoxLineCount;

		public bool isTermsOfUseAccepted;

		public ulong XBLId { get; private set; }

		public ulong XBLOnlineId { get; private set; }

		public string XBLToken { get; private set; }

		private void Awake()
		{
			modBrowser.gameObject.SetActive(value: false);
			loadingIcon.SetActive(value: false);
		}

		private void Start()
		{
			messageBox.text = string.Empty;
			InitializeGDK();
		}

		private void OnEnable()
		{
			base.gameObject.GetComponentInChildren<Selectable>()?.Select();
		}

		public void ActivateModBrowser()
		{
			base.gameObject.SetActive(value: false);
			modBrowser.gameObject.SetActive(value: true);
		}

		public void ActivateTestMenu()
		{
			base.gameObject.SetActive(value: true);
			modBrowser.gameObject.SetActive(value: false);
		}

		public void SetTermsAccepted(bool termsFlag)
		{
			isTermsOfUseAccepted = termsFlag;
		}

		private bool InitializeGDK()
		{
			return true;
		}

		private void DispatchGXDKTaskQueue()
		{
		}

		private void InitializeForDefaultUser()
		{
			StartCoroutine(StartUserInit());
		}

		private IEnumerator StartUserInit()
		{
			SetLoadingState(isLoading: true);
			LogMessage("---------[ MOD.IO STARTING ]---------", logToConsole: false);
			bool isInitialized = false;
			UserDataStorage.SetActiveUser(string.Empty, delegate
			{
				isInitialized = true;
			});
			while (!isInitialized)
			{
				yield return null;
			}
			SetLoadingState(isLoading: false);
		}

		public void ClearUser()
		{
			UserDataStorage.ClearActiveUserData(delegate(bool success)
			{
				if (success)
				{
					LocalUser.instance = default(LocalUser);
					LogMessage("Successfully cleared the UserDataStorage.", logToConsole: true);
				}
				else
				{
					LogMessage("Failed to clear the UserDataStorage.", logToConsole: true);
				}
			});
		}

		public void Authenticate()
		{
			StartCoroutine(StartAuthentication());
		}

		private IEnumerator StartAuthentication()
		{
			string token = string.Empty;
			_ = string.Empty;
			yield return StartCoroutine(StartUserInit());
			SetLoadingState(isLoading: true);
			if (string.IsNullOrEmpty(token))
			{
				LogMessage("Unable to request an OAuthToken without a valid token", logToConsole: true);
			}
			else
			{
				bool isRequestComplete = false;
				UserAccountManagement.AuthenticateWithXboxLiveToken(token, isTermsOfUseAccepted, delegate
				{
					isRequestComplete = true;
					LogMessage("Successfully completed authentication", logToConsole: false);
				}, delegate(WebRequestError error)
				{
					isRequestComplete = true;
					LogMessage("Failed to authenticate:\n" + error.displayMessage, logToConsole: false);
				});
				while (!isRequestComplete)
				{
					yield return null;
				}
			}
			SetLoadingState(isLoading: false);
		}

		public void QueryInstalledMods()
		{
			SetLoadingState(isLoading: true);
			LogMessage("Querying Installed Mods...", logToConsole: false);
			ModManager.QueryInstalledMods(null, delegate(IList<KeyValuePair<ModfileIdPair, string>> installedMods)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Installed Mod Listing: [" + installedMods.Count + " MODS]");
				foreach (KeyValuePair<ModfileIdPair, string> installedMod in installedMods)
				{
					stringBuilder.AppendLine("Mod.File[" + installedMod.Key.modId + "." + installedMod.Key.modfileId + "]:" + installedMod.Value);
				}
				int length = stringBuilder.Length - 1;
				stringBuilder.Length = length;
				LogMessage(stringBuilder.ToString(), logToConsole: true);
				SetLoadingState(isLoading: false);
			});
		}

		private void SetLoadingState(bool isLoading)
		{
			loadingIcon.gameObject.SetActive(isLoading);
			Selectable selectable = null;
			Selectable[] componentsInChildren = base.gameObject.GetComponentsInChildren<Selectable>();
			foreach (Selectable selectable2 in componentsInChildren)
			{
				if (selectable == null)
				{
					selectable = selectable2;
				}
				selectable2.interactable = !isLoading;
			}
			if (!isLoading && selectable != null)
			{
				selectable.Select();
			}
		}

		private void LogMessage(string message, bool logToConsole)
		{
			if (logToConsole)
			{
				Debug.Log("[mod.io] " + message);
			}
			string text = messageBox.text + "\n" + message + "\n";
			int num = text.IndexOf('\n');
			int num2 = 0;
			while (num > 0 && num + 1 < text.Length)
			{
				num2++;
				num = text.IndexOf('\n', num + 1);
			}
			if (num2 > messageBoxLineCount)
			{
				int num3 = num2 - messageBoxLineCount;
				int startIndex = text.IndexOf('\n') + 1;
				for (int i = 1; i < num3; i++)
				{
					startIndex = text.IndexOf('\n', startIndex) + 1;
				}
				text = text.Substring(startIndex);
			}
			messageBox.text = text;
		}
	}
}
