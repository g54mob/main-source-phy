using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiverseScreen : MonoBehaviour
{
	[Serializable]
	public class TabData
	{
		public Button button;

		public Image buttonBg;

		public MultiverseTab tab;

		public GameObject[] content;
	}

	private const int JoinTabIndex = 0;

	private const int HostTabIndex = 1;

	public TabData[] tabs;

	public Material activeTabMat;

	public Material inactiveTabMat;

	public InputField playerName;

	public Toggle spectatorToggle;

	public Toggle levelEditorToggle;

	public Toggle dclToggle;

	public Toggle lanToggle;

	public GameObject internetLanToggleGroup;

	public GameObject regionSelectionObject;

	public GameObject multiverseConnectionInfoObject;

	public GameObject connectionInfoBarObject;

	public GameObject playListGO;

	public Image fadeImage;

	public string mainMenuScene;

	public GameObject joinFriendsWidget;

	public GameObject hostButton;

	public GameObject joinButton;

	public Toggle[] connectionTypes;

	private RectTransform playListTransform;

	private bool playListVisible;

	private float listAnimateSpeed = 0.3f;

	private CanvasGroup playListGroup;

	private int currentTab = -1;

	protected bool isReassigning;

	private IEnumerator fadeCoroutine;

	private bool startedFade;

	private bool needFade = true;

	private IEnumerator animatePlaylistCoroutine;

	private bool isClosing;

	private bool IsVisible
	{
		get
		{
			return currentTab == 1 && !StatMaster.Mode.levelEdit;
		}
	}

	protected void Awake()
	{
		for (int i = 0; i < tabs.Length; i++)
		{
			InitTab(i, tabs[i].button);
		}
		playListGroup = playListGO.GetComponent<CanvasGroup>();
		playListTransform = playListGO.transform as RectTransform;
		UpdatePlaylistOffset(0f);
		ChangeTab(0);
		playerName.onEndEdit.AddListener(OnValidateName);
		isReassigning = true;
		spectatorToggle.onValueChanged.AddListener(OnSpectatorChanged);
		levelEditorToggle.onValueChanged.AddListener(OnEditorChanged);
		dclToggle.onValueChanged.AddListener(OnDLCChanged);
		isReassigning = false;
		if (ReferenceMaster.IsPlatformReady())
		{
			regionSelectionObject.SetActive(false);
		}
		SetupNetworkTypes();
	}

	public void OnConnectionTypeChanged(int toggleIndex)
	{
		if (connectionTypes[toggleIndex].isOn)
		{
			switch (toggleIndex)
			{
			case 0:
				OptionsMaster.networkType = PlayerNetworkType.Steam;
				break;
			case 1:
				OptionsMaster.networkType = PlayerNetworkType.Playfab;
				break;
			case 2:
				OptionsMaster.networkType = PlayerNetworkType.DirectConnect;
				break;
			case 3:
				OptionsMaster.networkType = PlayerNetworkType.LAN;
				break;
			}
			UpdateLanModeVisibility();
			for (int i = 0; i < tabs.Length; i++)
			{
				tabs[i].tab.UpdateUI();
			}
		}
	}

	private void UpdateLanModeVisibility()
	{
		UpdateJoinFriendsVisibility();
		if (OptionsMaster.networkType != PlayerNetworkType.DirectConnect)
		{
			regionSelectionObject.SetActive(false);
			multiverseConnectionInfoObject.SetActive(false);
			connectionInfoBarObject.SetActive(false);
		}
		else
		{
			regionSelectionObject.SetActive(!ReferenceMaster.IsPlatformReady());
			connectionInfoBarObject.SetActive(true);
		}
	}

	private void ResetButtons()
	{
		spectatorToggle.isOn = OptionsMaster.spectatorEnabled;
		levelEditorToggle.isOn = StatMaster.Mode.levelEdit;
		dclToggle.isOn = !StatMaster.hostDisabledDLC;
	}

	protected void OnEnable()
	{
		ResetButtons();
		if (needFade)
		{
			fadeCoroutine = FadeIn();
			StartCoroutine(fadeCoroutine);
			startedFade = true;
			needFade = false;
			return;
		}
		if (startedFade)
		{
			if (fadeCoroutine != null)
			{
				StopCoroutine(fadeCoroutine);
			}
			startedFade = false;
		}
		SetFade(0f);
	}

	private void SetupNetworkTypes()
	{
		int num = 2;
		PlayerNetworkType networkType = PlayerNetworkType.DirectConnect;
		if (SteamManager.Initialized)
		{
			num = 0;
			networkType = PlayerNetworkType.Steam;
			if (OptionsMaster.BesiegeConfig.Crossplay)
			{
				num = 1;
				networkType = PlayerNetworkType.Playfab;
			}
		}
		else
		{
			connectionTypes[0].gameObject.SetActive(false);
			connectionTypes[1].gameObject.SetActive(false);
			connectionTypes[2].gameObject.SetActive(true);
		}
		if (OptionsMaster.networkType == PlayerNetworkType.LAN)
		{
			networkType = PlayerNetworkType.LAN;
		}
		OptionsMaster.networkType = networkType;
		for (int i = 0; i < connectionTypes.Length; i++)
		{
			connectionTypes[i].isOn = i == num;
		}
	}

	private void SetFade(float progress)
	{
		fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, progress);
	}

	private void OnEditorChanged(bool editorEnabled)
	{
		if (!isReassigning)
		{
			StatMaster.Mode.levelEdit = editorEnabled;
			UpdatePlaylistVisibility();
		}
	}

	private void OnDLCChanged(bool dlcDisabled)
	{
		StatMaster.hostDisabledDLC = !dlcDisabled;
	}

	private void UpdatePlaylistVisibility()
	{
		bool isVisible = IsVisible;
		if (isVisible == playListVisible)
		{
			return;
		}
		if (isVisible)
		{
			playListGO.SetActive(true);
			animatePlaylistCoroutine = AnimatePlaylist();
			StartCoroutine(animatePlaylistCoroutine);
		}
		else
		{
			playListGO.SetActive(false);
			if (animatePlaylistCoroutine != null)
			{
				StopCoroutine(animatePlaylistCoroutine);
			}
		}
		playListVisible = isVisible;
	}

	private void UpdatePlaylistOffset(float perc)
	{
		playListGroup.alpha = perc;
		playListTransform.anchoredPosition = new Vector2(0f, (1f - perc) * playListTransform.rect.height);
	}

	public void OnCloseClicked()
	{
		Close();
	}

	private void Update()
	{
		if (InputManager.CloseKey())
		{
			Close();
		}
	}

	private void Close()
	{
		if (isClosing)
		{
			return;
		}
		isClosing = true;
		if (startedFade)
		{
			if (fadeCoroutine != null)
			{
				StopCoroutine(fadeCoroutine);
			}
			startedFade = false;
		}
		StartCoroutine(OnClose());
	}

	private IEnumerator FadeIn()
	{
		float cTime = 1f;
		SetFade(cTime);
		yield return 0;
		while (cTime >= 0f)
		{
			yield return 0;
			cTime -= Time.unscaledDeltaTime;
			float progress = Mathf.Clamp01(Mathf.Sin(cTime * (float)Math.PI * 0.5f));
			SetFade(progress);
		}
		SetFade(0f);
		startedFade = false;
	}

	private IEnumerator OnClose()
	{
		float cTime = 0f;
		SetFade(cTime);
		while (cTime < 1f)
		{
			yield return 0;
			cTime += Time.unscaledDeltaTime;
			SetFade(Mathf.Clamp01(cTime));
		}
		StatMaster.SetInMenu(false);
		StatMaster.StopHotKeys(false);
		SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Single);
	}

	private IEnumerator AnimatePlaylist()
	{
		float cTime = 0f;
		float rate = 1f / listAnimateSpeed;
		UpdatePlaylistOffset(0f);
		while (cTime < 1f)
		{
			yield return 0;
			cTime += Time.unscaledDeltaTime * rate;
			float perc = Mathf.Sin(cTime * (float)Math.PI * 0.5f);
			UpdatePlaylistOffset(Mathf.Clamp01(perc));
		}
		UpdatePlaylistOffset(1f);
	}

	private void OnSpectatorChanged(bool isSpectator)
	{
		if (!isReassigning)
		{
			OptionsMaster.spectatorEnabled = isSpectator;
		}
	}

	private void OnValidateName(string newName)
	{
		joinButton.SetActive(false);
		hostButton.SetActive(false);
		WorkshopManager.VerifyString(newName, delegate(WorkshopManager.VerifyStringResult res, string str)
		{
			if (playerName != null)
			{
				playerName.text = StaticSettings.SanatizeString(str);
				joinButton.SetActive(true);
				hostButton.SetActive(true);
			}
		});
	}

	private void InitTab(int index, Button btn)
	{
		btn.onClick.AddListener(delegate
		{
			ChangeTab(index);
		});
	}

	private void UpdateJoinFriendsVisibility()
	{
		if (!(joinFriendsWidget == null))
		{
			bool active = currentTab == 0 && OptionsMaster.networkType != PlayerNetworkType.LAN;
			joinFriendsWidget.SetActive(active);
		}
	}

	private void UpdateJoinInternetLanVisibility()
	{
		internetLanToggleGroup.SetActive(currentTab == 0);
	}

	public void ChangeTab(int index)
	{
		if (currentTab == index)
		{
			return;
		}
		for (int i = 0; i < tabs.Length; i++)
		{
			TabData tabData = tabs[i];
			bool flag = i == index;
			for (int j = 0; j < tabData.content.Length; j++)
			{
				tabData.content[j].SetActive(flag);
			}
			tabData.buttonBg.material = ((!flag) ? inactiveTabMat : activeTabMat);
		}
		currentTab = index;
		ResetButtons();
		UpdatePlaylistVisibility();
		UpdateJoinFriendsVisibility();
		UpdateJoinInternetLanVisibility();
		UpdateLanModeVisibility();
	}
}
