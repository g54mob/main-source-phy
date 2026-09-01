using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SafeAreaStartScreen : MonoBehaviour
{
	[SerializeField]
	private MenuSettingsButton settingsButton;

	[SerializeField]
	private string sceneToLoad = "EarlyAccessDisclamer";

	[SerializeField]
	private Button acceptButton;

	[SerializeField]
	private Selectable firstSelectable;

	private SettingsInstance settings;

	private bool setToNextScene;

	private void Awake()
	{
		if (acceptButton != null)
		{
			acceptButton.onClick.AddListener(LoadNextScene);
		}
		if (firstSelectable != null)
		{
			EventSystem.current.SetSelectedGameObject(firstSelectable.gameObject);
		}
	}

	private void Start()
	{
		GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
		if (service != null)
		{
			settings = service.GetSettingsInstance(SafeArea.SAFE_AREA_SETTINGS_KEY);
		}
		else
		{
			Debug.LogError("Error: Unable to loacate settings with key: " + SafeArea.SAFE_AREA_SETTINGS_KEY);
		}
		if (settings != null)
		{
			settingsButton.Init(settings);
		}
	}

	private void Update()
	{
		if (PlayerActions.Instance.m_accept.WasPressed)
		{
			LoadNextScene();
		}
	}

	private void LoadNextScene()
	{
		TABSSceneManager.LoadScene(sceneToLoad, forceInstantLoad: true);
	}
}
