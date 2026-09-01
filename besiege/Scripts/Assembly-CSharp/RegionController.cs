using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionController : MonoBehaviour, IConnectionController
{
	private const float PingTimeout = 3f;

	private int lowestPing = int.MaxValue;

	private int testedRegions;

	private Region fastestRegion;

	public bool DoneTesting { get; private set; }

	public bool IsInitialized { get; private set; }

	public void Retest()
	{
	}

	public void Setup(ExtendedNATHelper natHelper)
	{
		Initialize();
	}

	private void Initialize()
	{
		StartCoroutine(DetermineFastestRegion());
	}

	private IEnumerator DetermineFastestRegion()
	{
		DoneTesting = false;
		foreach (KeyValuePair<Region, FallbackHost> regionHost in ReferenceMaster.RegionServers)
		{
			string address = regionHost.Value.ResolvedAddress;
			Region region = regionHost.Key;
			StartCoroutine(PingRegion(region, address));
		}
		IsInitialized = true;
		yield break;
	}

	private IEnumerator PingRegion(Region region, string address)
	{
		Ping ping = new Ping(address);
		float pingStartTime = Time.time;
		yield return new WaitUntil(() => Time.time - pingStartTime >= 3f || ping.isDone);
		if (BesiegeLogFilter.logDev)
		{
			if (!ping.isDone)
			{
				Debug.LogWarning(string.Concat("Region '", region, "' timed out, ping: ", ping.time));
			}
			else
			{
				Debug.Log(string.Concat("Region '", region, "' ping: ", ping.time));
			}
		}
		if (ping.isDone && ping.time < lowestPing)
		{
			lowestPing = ping.time;
			fastestRegion = region;
		}
		if (++testedRegions != ReferenceMaster.RegionServers.Count)
		{
			yield break;
		}
		if (fastestRegion != Region.InvalidRegion)
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log("Determined the best region is: " + fastestRegion);
			}
			OptionsMaster.BesiegeConfig.Region = fastestRegion;
			ReferenceMaster.SaveConfig();
		}
		else
		{
			Debug.LogWarning("Unable to determine fastest region, using default settings");
		}
		DoneTesting = true;
	}
}
