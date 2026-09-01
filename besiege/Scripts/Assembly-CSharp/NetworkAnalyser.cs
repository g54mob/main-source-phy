using System;
using System.Collections;
using Localisation;
using UnityEngine;

public class NetworkAnalyser : SingleInstanceFindOnly<NetworkAnalyser>, ILocalisationAware
{
	private bool usingAlternativeConnection;

	private bool overwriteDoneTesting;

	private ExternalIpResolver externalIpResolver;

	private ExtendedNATHelper extendedNatHelper;

	public override string Name
	{
		get
		{
			return "NetworkAnalyser";
		}
	}

	public bool DoneTesting
	{
		get
		{
			if (usingAlternativeConnection)
			{
				return overwriteDoneTesting;
			}
			return NATConnectionTester.DoneTesting && FacilitatorController.DoneTesting && RegionController.DoneTesting;
		}
	}

	public NATConnectionTester NATConnectionTester { get; private set; }

	public FacilitatorController FacilitatorController { get; private set; }

	public UPNPController UPNPController { get; private set; }

	public ExtendedNATHelper NATHelper
	{
		get
		{
			return extendedNatHelper;
		}
	}

	public RegionController RegionController { get; private set; }

	public string ExternalIP
	{
		get
		{
			return externalIpResolver.ExternalIP;
		}
	}

	public bool UsingAlternativeConnection
	{
		get
		{
			return usingAlternativeConnection;
		}
	}

	public void OnLocalisationChange()
	{
		if (!(NATConnectionTester == null))
		{
			NATConnectionTester.TestConnection();
		}
	}

	protected override void Awake()
	{
		base.Awake();
		externalIpResolver = UnityEngine.Object.FindObjectOfType<ExternalIpResolver>();
		if (externalIpResolver == null)
		{
			externalIpResolver = base.gameObject.AddComponent<ExternalIpResolver>();
		}
		ReferenceMaster.RegionChanged = (Action<Region>)Delegate.Combine(ReferenceMaster.RegionChanged, new Action<Region>(OnRegionChanged));
		if (SteamManager.Initialized)
		{
			usingAlternativeConnection = true;
			SetupNatHelper();
			StartCoroutine(VerifyExternalIP());
		}
		else
		{
			usingAlternativeConnection = false;
			SetupNatHelper();
			AddControllers();
			StartCoroutine(SetupControllers());
		}
	}

	private IEnumerator VerifyExternalIP()
	{
		yield return new WaitUntil(() => externalIpResolver.HasResolvedIP);
		overwriteDoneTesting = true;
	}

	private IEnumerator SetupControllers()
	{
		RegionController.Setup(extendedNatHelper);
		yield return new WaitUntil(() => RegionController.DoneTesting);
		UpdateServicesAddresses();
		ConfigureNatHelper();
		NATConnectionTester.Setup(extendedNatHelper);
		UPNPController.Setup(extendedNatHelper);
		yield return new WaitUntil(() => UPNPController.DoneTesting);
		FacilitatorController.Setup(extendedNatHelper);
	}

	private void SetupNatHelper()
	{
		extendedNatHelper = base.gameObject.GetComponent<ExtendedNATHelper>();
		if (extendedNatHelper == null)
		{
			extendedNatHelper = base.gameObject.AddComponent<ExtendedNATHelper>();
		}
	}

	private void ConfigureNatHelper()
	{
		extendedNatHelper.DisconnectFromFacilitator(0u);
		extendedNatHelper.facilitatorIP = OptionsMaster.BesiegeConfig.FacilitatorIP;
		extendedNatHelper.facilitatorPort = (ushort)OptionsMaster.BesiegeConfig.FacilitatorPort;
		extendedNatHelper.portForwardingTimeOut = OptionsMaster.BesiegeConfig.PortForwardingTimeout;
		extendedNatHelper.punchthroughTimeout = OptionsMaster.BesiegeConfig.PunchThroughTimeout;
	}

	private void AddControllers()
	{
		UPNPController = base.gameObject.AddComponent<UPNPController>();
		NATConnectionTester = base.gameObject.AddComponent<NATConnectionTester>();
		FacilitatorController = base.gameObject.AddComponent<FacilitatorController>();
		RegionController = base.gameObject.AddComponent<RegionController>();
	}

	private void OnRegionChanged(Region newRegion)
	{
		UpdateServicesAddresses();
		ReconfigureControllers();
	}

	private void UpdateServicesAddresses()
	{
		Region region = OptionsMaster.BesiegeConfig.Region;
		string resolvedAddress = ReferenceMaster.RegionServers[region].ResolvedAddress;
		if (BesiegeLogFilter.logDev)
		{
			Debug.Log(string.Concat("Updating services addresses for region '", region, "', servicesAddress: ", resolvedAddress));
		}
		BesiegeConfig besiegeConfig = OptionsMaster.BesiegeConfig;
		string text = resolvedAddress;
		OptionsMaster.BesiegeConfig.ConnectiontesterIP = text;
		text = text;
		OptionsMaster.BesiegeConfig.MasterserverIP = text;
		besiegeConfig.FacilitatorIP = text;
		ReferenceMaster.SaveConfig();
	}

	private void ReconfigureControllers()
	{
		ConfigureNatHelper();
		if (BesiegeNetworkManager.Instance != null)
		{
			BesiegeNetworkManager.Instance.ConfigureMasterServer();
			BesiegeNetworkManager.Instance.ResetConnection();
		}
		NATConnectionTester.Retest();
		FacilitatorController.Retest();
	}

	private void OnDestroy()
	{
		ReferenceMaster.RegionChanged = (Action<Region>)Delegate.Remove(ReferenceMaster.RegionChanged, new Action<Region>(OnRegionChanged));
	}
}
