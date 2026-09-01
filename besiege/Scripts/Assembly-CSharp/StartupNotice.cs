using System.Collections;
using UnityEngine;

public class StartupNotice : MonoBehaviour
{
	private enum UIState
	{
		None = 0,
		Translocation = 1,
		Aspect = 2,
		GamePreview = 3
	}

	private bool inMenu;

	private Camera hudCam;

	[SerializeField]
	private GameObject translocationGO;

	[SerializeField]
	private GameObject aspectGO;

	[SerializeField]
	private GameObject gamePreviewGO;

	[SerializeField]
	private UIButton acceptButton;

	private static bool once;

	protected void Awake()
	{
		if (once)
		{
			base.gameObject.SetActive(false);
			return;
		}
		once = true;
		acceptButton.Click += OnAccept;
		hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
		if (OptionsMaster.isSandboxed)
		{
			ToggleState(UIState.Translocation);
		}
		else if (OptionsMaster.BesiegeConfig.ScreenWidth < OptionsMaster.BesiegeConfig.ScreenHeight)
		{
			ToggleState(UIState.Aspect);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	protected IEnumerator Start()
	{
		yield return null;
		yield return null;
		Bounds noticeBounds = GetComponent<MeshRenderer>().bounds;
		float minX = hudCam.WorldToScreenPoint(noticeBounds.min).x / (float)Screen.width;
		if (minX < 0f)
		{
			Vector3 oldScale = base.transform.localScale;
			float multiplier = 1f + minX * 2f;
			base.transform.localScale = new Vector3(oldScale.x * multiplier, oldScale.y * multiplier, oldScale.z);
		}
	}

	private void OnTranslocationWarning()
	{
		string[] array;
		switch (Application.systemLanguage)
		{
		case SystemLanguage.Japanese:
			array = new string[3] { "現在ゲームは通常と異なる方法で起動されおり、ファイルシステムにアクセスできない\n状態です。", "このため、設定、進行度、マシンやレベルが読み込まれません。\nさらに、スキンやモデルを有効化することもできません。", "問題を解決するために、Besiege appを'アプリケーション'フォルダに移動してください。\nこのことはゲームの正常な動作のためにも強く推奨されています。" };
			break;
		case SystemLanguage.German:
			array = new string[3] { "Das Spiel wurde 'translocated' gestartet, d.h. es darf nicht auf das Dateisystem\nzugreifen.", "So können keine Einstellungen, Maschinen, oder Level gespeichert oder geladen werden.\nAußerdem können keine Skins oder Mods geladen werden.", "Um das zu beheben, verschiebe das Besiege Programm in den 'Anwendungen' /\n'Applications' Ordner. Wir empfehlen stark dies zu tun, andernfalls könenn wir kein\nflüssiges Spielerlebnis garantieren." };
			break;
		case SystemLanguage.French:
			array = new string[3] { "Le jeu est actuellement en mode 'translocated'. Ceci signifie qu'il n'est pas autorisé\nà accéder aux fichiers système.", "De ce fait, vous ne pouvez pas sauvegarder et charger les paramètres, progrès,\nmachines ou niveaux. De plus, vous ne pourrez pas charger de skins ou mods.", "Pour solutionner ce problème, déplacez l'application Besiege vers le fichier 'Applications'.\nNous vous recommandons chaudement de faire ceci, afin de vous garantir la meilleure\nexperience possible." };
			break;
		case SystemLanguage.Portuguese:
			array = new string[3] { "O jogo está atualmente a rodar como 'translocated'. Isto significa que o jogo foi proibído\nde aceder ao sistema de ficheiros.", "Isto significa que não podes gravar ou carregar definições, progresso,\nmáquinas ou níveis. Adicionalmente, será impossível carregar skins ou mods.", "Para resolver este problema, mova a aplicação Besiege para a pasta 'Aplicações'.\nRecomendamos vivamente que o faça, pois não podemos garantir uma boa experiência\nde outro modo." };
			break;
		default:
			array = new string[3] { "The game is currently running as translocated. Which means the game has been prohibited\nfrom accessing the file system.", "This means you cannot save or load settings, progress, machines or levels.\nAdditionally, you'll be unable to load skins and mods.", "To resolve this issue, please move the Besiege app to the 'Applications' folder.\nWe strongly encourage this course of action, as we can't guarantee a smooth experience\notherwise." };
			break;
		}
		for (int i = 0; i < array.Length; i++)
		{
			DynamicText component = translocationGO.transform.FindChild("text" + (i + 1)).GetComponent<DynamicText>();
			component.cam = hudCam;
			ReferenceMaster.SetDynamicText(component, array[i]);
		}
		translocationGO.SetActive(true);
	}

	private void ToggleState(UIState state)
	{
		aspectGO.SetActive(state == UIState.Aspect);
		gamePreviewGO.SetActive(state == UIState.GamePreview);
		if (state == UIState.Translocation)
		{
			OnTranslocationWarning();
		}
	}

	private void SetInMenu(bool toggle)
	{
		if (inMenu != toggle)
		{
			inMenu = toggle;
			StatMaster.SetInMenu(toggle);
		}
	}

	private void OnEnable()
	{
		SetInMenu(true);
	}

	private void OnDisable()
	{
		SetInMenu(false);
	}

	private void OnAccept()
	{
		base.gameObject.SetActive(false);
	}
}
