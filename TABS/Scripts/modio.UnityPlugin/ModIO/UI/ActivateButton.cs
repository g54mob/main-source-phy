using System.Collections;
using UnityEngine;

namespace ModIO.UI
{
	public class ActivateButton : MonoBehaviour
	{
		public ModBrowser modBrowser;

		private void Awake()
		{
			modBrowser.gameObject.SetActive(value: false);
		}

		public void Do()
		{
			StartCoroutine(StartModBrowser());
		}

		private IEnumerator StartModBrowser()
		{
			int userId = -1;
			Debug.Log("---------[ MOD.IO STARTING ]------------");
			bool isInitialized = false;
			UserDataStorage.SetActiveUser(userId, delegate(int id, bool success)
			{
				isInitialized = true;
				if (success)
				{
					Debug.Log("Successfully set active user for the UserDataStorage. UserId: " + userId);
				}
				else
				{
					Debug.Log("Failed to set active user for the UserDataStorage. UserId: " + userId);
				}
			});
			while (!isInitialized)
			{
				yield return null;
			}
			modBrowser.gameObject.SetActive(value: true);
			base.gameObject.SetActive(value: false);
		}
	}
}
