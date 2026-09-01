using System;
using System.Collections;
using System.Net;
using UnityEngine;

public class ExternalIpResolver : MonoBehaviour
{
	private const string ExternalIPSource = "http://icanhazip.com";

	public Action OnIPResolved;

	private string externalIP = string.Empty;

	private Coroutine getExternalIPCoroutine;

	private bool isDoneFetchingExternalIP;

	public string ExternalIP
	{
		get
		{
			return externalIP;
		}
	}

	public bool HasResolvedIP
	{
		get
		{
			return isDoneFetchingExternalIP;
		}
	}

	public void ResolveAddress()
	{
		if (getExternalIPCoroutine != null)
		{
			StopCoroutine(getExternalIPCoroutine);
		}
		getExternalIPCoroutine = StartCoroutine(getExternalIP());
	}

	private void Awake()
	{
		ResolveAddress();
	}

	private IEnumerator getExternalIP()
	{
		bool failed = true;
		isDoneFetchingExternalIP = false;
		WWW www = new WWW("http://icanhazip.com");
		yield return new WaitUntil(() => www.isDone);
		if (string.IsNullOrEmpty(www.error))
		{
			IPAddress address = null;
			string ipString = www.text.Trim();
			if (IPAddress.TryParse(ipString, out address) && ipString == address.ToString())
			{
				externalIP = address.ToString();
				failed = false;
				StatMaster.ExternalIP = externalIP;
			}
		}
		if (failed && BesiegeLogFilter.logWarn)
		{
			Debug.LogWarning("Failed NATTraversal to fetch externalIP: " + www.error);
		}
		isDoneFetchingExternalIP = true;
	}
}
