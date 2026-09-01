using Poly.Physics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Main : MonoBehaviour
{
	public World m_World;

	public HydraulicController m_HydraulicController;

	public GameObject m_GameUI;

	public GameObject m_ConsoleUI;

	public GameObject m_DecorStubsPrefab;

	public GameObject m_MasterAudioPrefab;

	public PostFX m_PostFX;

	public EventSystem m_EventSystem;

	public static Main m_Instance;

	public static bool m_ShuttingDown;

	private void Awake()
	{
		if ((bool)m_Instance)
		{
			base.gameObject.SetActive(value: false);
			Object.Destroy(base.gameObject);
		}
		else
		{
			m_Instance = this;
			InstantiateGameUI();
			InstantiateMasterAudio();
			InstantiateDecorStubs();
			InstantiateConsole();
			GameManager.AwakeManual();
			Object.DontDestroyOnLoad(base.gameObject);
			Application.wantsToQuit += WantsToQuit;
		}
		m_World.autoPlay = false;
	}

	private bool WantsToQuit()
	{
		if (SteamManager.IsLoggedOn())
		{
			SteamManager.SteamCancelAuthSessionTicket();
		}
		return true;
	}

	private void OnApplicationQuit()
	{
		m_ShuttingDown = true;
	}

	private void Start()
	{
		GameManager.StartManual();
	}

	private void Update()
	{
		GameManager.UpdateManual();
	}

	private void LateUpdate()
	{
		GameManager.LateUpdateManual();
	}

	private void FixedUpdate()
	{
		GameManager.FixedUpdateManual();
	}

	private void OnDestroy()
	{
		GameRichPresence.Shutdown();
		SteamManager.ShutDown();
	}

	private void InstantiateGameUI()
	{
		GameObject gameObject = Object.Instantiate(m_Instance.m_GameUI);
		if (gameObject != null)
		{
			gameObject.name = m_Instance.m_GameUI.name;
			Object.DontDestroyOnLoad(gameObject);
		}
	}

	private void InstantiateMasterAudio()
	{
		GameObject gameObject = Object.Instantiate(m_Instance.m_MasterAudioPrefab);
		if (gameObject != null)
		{
			gameObject.name = m_Instance.m_MasterAudioPrefab.name;
			Object.DontDestroyOnLoad(gameObject);
		}
	}

	private void InstantiateDecorStubs()
	{
		GameObject gameObject = Object.Instantiate(m_Instance.m_DecorStubsPrefab);
		if (gameObject != null)
		{
			gameObject.name = m_Instance.m_DecorStubsPrefab.name;
			Object.DontDestroyOnLoad(gameObject);
		}
	}

	private void InstantiateConsole()
	{
		GameObject gameObject = Object.Instantiate(m_Instance.m_ConsoleUI, Vector3.zero, Quaternion.identity);
		if (gameObject != null)
		{
			Object.DontDestroyOnLoad(gameObject);
			gameObject.name = m_Instance.m_ConsoleUI.name;
		}
	}
}
