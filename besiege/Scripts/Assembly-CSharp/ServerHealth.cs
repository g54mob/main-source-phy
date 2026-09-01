using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UI/Multiplayer/Server Health")]
public class ServerHealth : MonoBehaviour
{
	[Serializable]
	public class UIRefs
	{
		public GameObject parent;

		public Transform barScaler;

		public Transform pingLabel;

		public DynamicText pingText;

		public DynamicText fpsText;

		public MeshRenderer lightningIcon;

		public MeshRenderer checkIcon;

		public MeshRenderer warningIcon;

		public MeshRenderer burningIcon;

		public UIButton[] openServerSettings;
	}

	public enum PerformanceLevel
	{
		Good = 0,
		Average = 1,
		Bad = 2,
		Terrible = 3
	}

	public static ServerHealth Instance;

	public Color badColor;

	public Color terribleColor;

	public float okayThreshold = 0.8f;

	public float badThreshold = 0.5f;

	public float dyingThreshold = 0.3f;

	private int terriblePing = 100;

	public static bool countDirty;

	public float deadZone = 0.03f;

	public UIRefs clientLayout;

	public UIRefs hostLayout;

	public UIRefs spLayout;

	public GameObject settingsWindow;

	private float MAX_FPS = 60f;

	private float serverHealth;

	private float lastHealth;

	private int activeBlockCount;

	private Coroutine lerpHealth;

	private int cpuLoad;

	private PerformanceAnalyser perfAnalyser;

	private int lastPing = 1000;

	private ProjectileManager projectileManager;

	private CustomLevel level;

	public PerformanceLevel serverHealthState;

	protected Dictionary<PerformanceLevel, float> performanceBarWidths = new Dictionary<PerformanceLevel, float>
	{
		{
			PerformanceLevel.Good,
			0.05f
		},
		{
			PerformanceLevel.Average,
			0.35f
		},
		{
			PerformanceLevel.Bad,
			0.67f
		},
		{
			PerformanceLevel.Terrible,
			1f
		}
	};

	private Color startPingColor = Color.white;

	private int pingFails;

	protected Coroutine updateFPS;

	protected bool wasSimulating;

	private bool destroying;

	public int CPULoad
	{
		get
		{
			return cpuLoad;
		}
	}

	public float Health
	{
		get
		{
			return serverHealth;
		}
	}

	public int ActiveBlockCount
	{
		get
		{
			UpdateEntities();
			return activeBlockCount;
		}
	}

	protected void Awake()
	{
		if (clientLayout.pingText != null)
		{
			startPingColor = clientLayout.pingText.color;
		}
		Instance = this;
		Reset();
	}

	protected void Start()
	{
		perfAnalyser = SingleInstance<PerformanceAnalyser>.Instance;
		projectileManager = ProjectileManager.Instance;
		level = CustomLevel.Instance;
	}

	public void Reset()
	{
		AssignNetworkSettingsDel(clientLayout);
		if (lerpHealth != null)
		{
			StopCoroutine(lerpHealth);
		}
		serverHealth = 1f;
		lastHealth = 0f;
		UpdateState();
		UpdateIcons(clientLayout, serverHealthState);
		clientLayout.barScaler.localScale = new Vector3(performanceBarWidths[serverHealthState], clientLayout.barScaler.localScale.y, clientLayout.barScaler.localScale.z);
		if (StatMaster.isMP)
		{
			AssignNetworkSettingsDel(hostLayout);
			UpdateIcons(hostLayout, serverHealthState);
			hostLayout.barScaler.localScale = new Vector3(performanceBarWidths[serverHealthState], hostLayout.barScaler.localScale.y, hostLayout.barScaler.localScale.z);
		}
	}

	private void AssignNetworkSettingsDel(UIRefs u)
	{
		for (int i = 0; i < u.openServerSettings.Length; i++)
		{
			u.openServerSettings[i].ResetDelegates();
			u.openServerSettings[i].Down += OpenServerSettings;
		}
	}

	protected void OpenServerSettings()
	{
		if (StatMaster.isMP)
		{
			settingsWindow.SetActive(true);
		}
	}

	public void SetPing(int ping)
	{
		if (StatMaster.isHeadless || (StatMaster.isMP && !StatMaster.isClient))
		{
			return;
		}
		UIRefs uIRefs = clientLayout;
		if ((float)ping < 0f)
		{
			pingFails++;
			if (pingFails > 60)
			{
				Debug.LogWarning("Ping in ServerHealth was set to a negative number 60 times in a row while 'Steam Connection' was " + SingleInstanceFindOnly<NetworkAnalyser>.Instance.UsingAlternativeConnection);
				pingFails = 0;
				if (uIRefs.pingText.color != terribleColor)
				{
					uIRefs.pingText.color = terribleColor;
				}
				if (uIRefs.pingText.GetText() != "???")
				{
					ReferenceMaster.SetDynamicText(uIRefs.pingText, "???");
				}
			}
		}
		else
		{
			pingFails = 0;
			if (ping != lastPing)
			{
				if (!StatMaster.isClient)
				{
					if (uIRefs.pingText.color != startPingColor)
					{
						uIRefs.pingText.color = startPingColor;
					}
					ReferenceMaster.SetDynamicText(uIRefs.pingText, "0");
				}
				else if (ping >= 0)
				{
					if (ping > terriblePing)
					{
						if (uIRefs.pingText.color != terribleColor)
						{
							uIRefs.pingText.color = terribleColor;
						}
					}
					else if (uIRefs.pingText.color != startPingColor)
					{
						uIRefs.pingText.color = startPingColor;
					}
					ReferenceMaster.SetDynamicText(uIRefs.pingText, ping.ToString("f0"));
				}
				lastPing = ping;
			}
		}
		wasSimulating = StatMaster.levelSimulating;
	}

	protected IEnumerator UpdateFPS(float wait, float rate)
	{
		yield return new WaitForSecondsRealtime(wait);
		UIRefs u = ((!spLayout.parent) ? clientLayout : spLayout);
		if (StatMaster.isMP && !StatMaster.IsLevelEditorOnly)
		{
			u = ((!StatMaster.isClient) ? hostLayout : clientLayout);
		}
		while (base.enabled)
		{
			float FPS = perfAnalyser.UncappedFPS;
			ReferenceMaster.SetDynamicText(u.fpsText, FPS.ToString("f2"));
			if (!StatMaster.levelSimulating && wasSimulating)
			{
				Reset();
			}
			wasSimulating = StatMaster.levelSimulating;
			if (!StatMaster.isMP)
			{
				SetServerFPS(FPS);
			}
			yield return new WaitForSecondsRealtime(rate);
		}
	}

	private void OnEnable()
	{
		if (destroying)
		{
			return;
		}
		if (!StatMaster.isMP || StatMaster.IsLevelEditorOnly)
		{
			if ((bool)spLayout.parent)
			{
				spLayout.parent.SetActive(true);
				clientLayout.parent.SetActive(false);
				hostLayout.parent.SetActive(false);
			}
			else
			{
				clientLayout.pingLabel.gameObject.SetActive(false);
				clientLayout.pingText.gameObject.SetActive(false);
				clientLayout.parent.SetActive(true);
				hostLayout.parent.SetActive(false);
			}
		}
		else
		{
			clientLayout.openServerSettings[clientLayout.openServerSettings.Length - 1].gameObject.SetActive(true);
			if (StatMaster.isClient)
			{
				clientLayout.parent.SetActive(true);
				hostLayout.parent.SetActive(false);
			}
			else
			{
				clientLayout.parent.SetActive(false);
				hostLayout.parent.SetActive(true);
			}
			spLayout.parent.SetActive(false);
		}
		if (!StatMaster.isHeadless)
		{
			updateFPS = StartCoroutine(UpdateFPS(0.1f, 0.5f));
		}
	}

	private void OnDisable()
	{
		if (!StatMaster.isHeadless && updateFPS != null)
		{
			StopCoroutine(updateFPS);
		}
	}

	private void OnApplicationQuit()
	{
		destroying = true;
	}

	public void SetServerFPS(float fps)
	{
		UIRefs uIRefs = ((!spLayout.parent) ? clientLayout : spLayout);
		if (StatMaster.isMP && !StatMaster.IsLevelEditorOnly)
		{
			uIRefs = ((!StatMaster.isClient) ? hostLayout : clientLayout);
		}
		MAX_FPS = ((StatMaster.MaxFPS > 30) ? 60f : 30f);
		float num = fps / MAX_FPS;
		num = ((!(num > 1f)) ? num : 1f);
		if (Mathf.Abs(num - lastHealth) > deadZone)
		{
			lastHealth = num;
			if (lerpHealth != null)
			{
				StopCoroutine(lerpHealth);
			}
			if (base.gameObject.activeInHierarchy)
			{
				lerpHealth = StartCoroutine(LerpHealth(uIRefs, fps));
				return;
			}
			serverHealth = num;
			UpdateState();
			UpdateIcons(uIRefs, serverHealthState);
			uIRefs.barScaler.localScale = new Vector3(performanceBarWidths[serverHealthState], uIRefs.barScaler.localScale.y, uIRefs.barScaler.localScale.z);
		}
		wasSimulating = StatMaster.levelSimulating;
	}

	private void UpdateState()
	{
		if (serverHealth >= okayThreshold)
		{
			serverHealthState = PerformanceLevel.Good;
		}
		else if (serverHealth >= badThreshold)
		{
			serverHealthState = PerformanceLevel.Average;
		}
		else if (serverHealth >= dyingThreshold)
		{
			serverHealthState = PerformanceLevel.Bad;
		}
		else
		{
			serverHealthState = PerformanceLevel.Terrible;
		}
	}

	private void UpdateIcons(UIRefs u, PerformanceLevel state)
	{
		switch (state)
		{
		case PerformanceLevel.Good:
			u.lightningIcon.enabled = true;
			u.checkIcon.enabled = false;
			u.warningIcon.enabled = false;
			u.burningIcon.enabled = false;
			break;
		case PerformanceLevel.Average:
			u.lightningIcon.enabled = false;
			u.checkIcon.enabled = true;
			u.warningIcon.enabled = false;
			u.burningIcon.enabled = false;
			break;
		case PerformanceLevel.Bad:
			u.lightningIcon.enabled = false;
			u.checkIcon.enabled = false;
			u.warningIcon.enabled = true;
			u.burningIcon.enabled = false;
			break;
		case PerformanceLevel.Terrible:
			u.lightningIcon.enabled = false;
			u.checkIcon.enabled = false;
			u.warningIcon.enabled = false;
			u.burningIcon.enabled = true;
			break;
		}
	}

	internal void SetServerCPULoad(int cpuLoad)
	{
		this.cpuLoad = cpuLoad;
	}

	private IEnumerator LerpHealth(UIRefs u, float fps)
	{
		float cTime = 0f;
		float rate = 2f / (NetworkScene.ServerSettings.sendRate * 5f);
		float startWidth = u.barScaler.localScale.x;
		bool changedIcon = false;
		serverHealth = fps / MAX_FPS;
		UpdateState();
		float target = performanceBarWidths[serverHealthState];
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			u.barScaler.localScale = new Vector3(Mathf.Lerp(startWidth, target, cTime), u.barScaler.localScale.y, u.barScaler.localScale.z);
			if (!changedIcon && cTime >= 0.49f)
			{
				UpdateIcons(u, serverHealthState);
				changedIcon = true;
			}
			yield return null;
		}
		u.barScaler.localScale = new Vector3(target, u.barScaler.localScale.y, u.barScaler.localScale.z);
	}

	private PerformanceLevel ClosestState(float width)
	{
		float num = 10f;
		float num2 = 0f;
		for (int i = 0; i < performanceBarWidths.Count; i++)
		{
			float num3 = Mathf.Abs(performanceBarWidths[(PerformanceLevel)i] - width);
			if (num3 < num)
			{
				num2 = i;
				num = num3;
			}
		}
		return (PerformanceLevel)num2;
	}

	protected void OnDestroy()
	{
		countDirty = false;
		destroying = true;
	}

	private void UpdateEntities()
	{
		if (!countDirty)
		{
			return;
		}
		activeBlockCount = 0;
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			PlayerData playerData = Playerlist.Players[i];
			if (playerData.isSpectator)
			{
				continue;
			}
			if (playerData.machine != null)
			{
				if (playerData.machine.isSimulating)
				{
					activeBlockCount += playerData.machine.DisplayBlockCount;
				}
			}
			else
			{
				Debug.Log("Player machine is null in ServerHealth::UpdateEntities!");
				playerData.isSpectator = true;
			}
		}
		if (level != null)
		{
			activeBlockCount += level.TotalEntityCount;
		}
		else
		{
			Debug.LogWarning("Level null in ServerHealth::UpdateEntities!");
		}
		if (projectileManager != null)
		{
			activeBlockCount += projectileManager.ProjectileCount;
		}
		else
		{
			Debug.LogWarning("ProjectileManager null in ServerHealth::UpdateEntities!");
		}
		countDirty = false;
	}
}
