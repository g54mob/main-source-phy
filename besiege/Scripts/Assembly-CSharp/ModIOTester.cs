using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ModIO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ModIOTester : MonoBehaviour
{
	public enum ModTestCase
	{
		GetSubscriptions = 0,
		GetUploadedMods = 1,
		DownloadModFile = 2,
		DownloadNonExiststentModFile = 3,
		SendLoginCode = 4,
		LoginWithCode = 5,
		WebRequestDownloadTest = 6
	}

	public float Delay = 2f;

	public ModTestCase TestCase;

	public int ModFileId = 11115;

	public string LoginEmail;

	public string LoginSecurityCode;

	public Text progressText;

	private IEnumerator Start()
	{
		Profiler.maxNumberOfSamplesPerFrame = -1;
		yield return new WaitForSeconds(Delay);
		DebugCurrentUser();
		PerformTest();
	}

	private void DebugCurrentUser()
	{
		UnityEngine.Debug.Log(string.Concat("[", LocalUser.AuthenticationState, "] Current user logged in as: ", LocalUser.UserId.ToString()));
		GetSubscriptionsTest();
	}

	private void PerformTest()
	{
		UnityEngine.Debug.Log("Performing test: " + TestCase);
		switch (TestCase)
		{
		case ModTestCase.GetSubscriptions:
			GetSubscriptionsTest();
			break;
		case ModTestCase.DownloadModFile:
			DownloadModTest();
			break;
		case ModTestCase.DownloadNonExiststentModFile:
			DownloadNonExistentModTest();
			break;
		case ModTestCase.SendLoginCode:
			SendLoginCodeTest();
			break;
		case ModTestCase.LoginWithCode:
			LoginWithCode();
			break;
		case ModTestCase.GetUploadedMods:
			GetUploadedModsTest();
			break;
		case ModTestCase.WebRequestDownloadTest:
			WebClientTest();
			break;
		}
	}

	private void WebClientTest()
	{
		StartCoroutine(WebClientTestIE());
	}

	private IEnumerator WebClientTestIE()
	{
		string url = "https://binary.test.modcdn.io/mods/84a9/5911/132762744618979396_5911.zip";
		UnityWebRequest www = new UnityWebRequest(url);
		int bufferSize = 65536;
		www.downloadHandler = new FileDownloadHandler(Application.persistentDataPath + "/temp3.bin", bufferSize);
		www.Send();
		Stopwatch sw = new Stopwatch();
		sw.Start();
		while (!www.isDone)
		{
			UpdateProgressText(www.downloadProgress);
			yield return null;
		}
		UnityEngine.Debug.Log("Request done in " + sw.ElapsedMilliseconds + " ms with bufferSize: " + bufferSize);
	}

	private void UpdateProgressText(float progress)
	{
		if (!(progressText == null))
		{
			float num = progress * 100f;
			progressText.text = string.Format("{0:0.##}%", num);
		}
	}

	private void OnCompleteCallback()
	{
		UnityEngine.Debug.Log("[WebClientTest] download completed");
	}

	private void GetUploadedModsTest()
	{
		ModManager.FetchAuthenticatedUserMods(OnFetchUserModsSuccess, OnFetchUserModsFailed);
	}

	private void OnFetchUserModsFailed(WebRequestError error)
	{
		UnityEngine.Debug.LogError("[OnFetchUserModsFailed] download failed, error: " + error.errorMessage);
	}

	private void OnFetchUserModsSuccess(List<ModProfile> uploadedMods)
	{
		UnityEngine.Debug.Log("[OnFetchUserModsSuccess] Upload user mods: " + string.Join(",", uploadedMods.Select((ModProfile x) => x.id.ToString()).ToArray()));
	}

	private void GetSubscriptionsTest()
	{
		List<int> subscribedModIds = LocalUser.SubscribedModIds;
		UnityEngine.Debug.Log("Subcribed mods: " + string.Join(",", subscribedModIds.Select((int x) => x.ToString()).ToArray()));
	}

	private void DownloadModTest()
	{
		ModManager.DownloadAndUpdateMod(ModFileId, OnDownloadSuccess, OnDownloadError);
	}

	private void DownloadNonExistentModTest()
	{
		ModManager.DownloadAndUpdateMod(int.MaxValue, OnDownloadSuccess, OnDownloadError);
	}

	private void OnDownloadError(WebRequestError error)
	{
		UnityEngine.Debug.LogError("[OnDownloadError] download failed, error: " + error.errorMessage);
	}

	private void OnDownloadSuccess()
	{
		UnityEngine.Debug.Log("[OnDownloadSuccess] download succeeded!");
	}

	private void SendLoginCodeTest()
	{
		if (ModIO.Utility.IsEmail(LoginEmail))
		{
			APIClient.SendSecurityCode(LoginEmail, OnSecurityCodeSent, OnSecurityCodeFailed);
		}
		else
		{
			UnityEngine.Debug.LogError("[SendLoginCodeTest] Invalid email provided");
		}
	}

	private void OnSecurityCodeFailed(WebRequestError error)
	{
		UnityEngine.Debug.LogError("[OnSecurityCodeFailed] failed to send login code, error: " + error.errorMessage);
	}

	private void OnSecurityCodeSent(APIMessage error)
	{
		UnityEngine.Debug.Log("[OnSecurityCodeSent] security code sent to email!");
	}

	private void LoginWithCode()
	{
		if (ModIO.Utility.IsSecurityCode(LoginSecurityCode))
		{
			UserAccountManagement.AuthenticateWithSecurityCode(LoginSecurityCode.ToUpper(), OnAuthenticated, OnAuthenticationFailed);
		}
		else
		{
			UnityEngine.Debug.LogError("[LoginWithCode] Invalid code provided");
		}
	}

	private void OnAuthenticationFailed(WebRequestError error)
	{
		UnityEngine.Debug.LogError("[OnAuthenticationFailed] failed to login, error: " + error.errorMessage);
	}

	private void OnAuthenticated(UserProfile userProfile)
	{
		if (userProfile != null)
		{
			UnityEngine.Debug.Log("[OnDownloadSuccess] Login succeeded, welcome back: " + userProfile.username + "!");
			GetUploadedModsTest();
		}
	}
}
