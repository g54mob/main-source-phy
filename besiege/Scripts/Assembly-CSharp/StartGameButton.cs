using System.Collections;
using UnityEngine;

public class StartGameButton : ClickBehaviour
{
	public Transform button;

	public string levelToLoad;

	public Transform explodedObj;

	public FadeScreen FadeCodey;

	public bool triggerOnStart;

	public bool registerMouse = true;

	private static bool loadingLevels;

	public static bool isLoadingLevel
	{
		get
		{
			return loadingLevels;
		}
	}

	private void Awake()
	{
		if (triggerOnStart)
		{
			LoadLevel();
		}
		releaseOnlyOver = true;
	}

	public override void OnClicked()
	{
		if (registerMouse)
		{
			if (explodedObj != null)
			{
				base.gameObject.GetComponent<Collider>().enabled = false;
				GetComponent<Renderer>().enabled = false;
				explodedObj.gameObject.SetActive(true);
			}
			AudioSource component = GetComponent<AudioSource>();
			if (component != null)
			{
				component.Play();
			}
		}
	}

	public override void OnClickReleased()
	{
		if (registerMouse)
		{
			AudioSource component = GetComponent<AudioSource>();
			if (component != null)
			{
				component.Play();
			}
			LoadLevel();
		}
	}

	public void LoadLevel()
	{
		if (!loadingLevels)
		{
			StartCoroutine(IELoadLevel());
		}
	}

	public IEnumerator IELoadLevel()
	{
		loadingLevels = true;
		if (FadeCodey != null && !registerMouse && (!StatMaster.isMP || StatMaster.IsLevelEditorOnly))
		{
			yield return FadeCodey.FadeIn();
		}
		if (StatMaster.isHosting || StatMaster.isClient)
		{
			BesiegeNetworkManager.Instance.Stop();
			yield return new WaitUntil(() => !BesiegeNetworkManager.Instance.isConnected);
		}
		else if (StatMaster.levelSimulating)
		{
			SingleInstanceFindOnly<AddPiece>.Instance.ToggleSimulateNoSound();
			yield return new WaitForSecondsRealtime(0.5f);
		}
		Arguments args = ((!BesiegeEntryPoint.IsSPLevel(levelToLoad)) ? new Arguments(new string[2] { "+load_scene", levelToLoad }) : new Arguments(new string[2] { "+load_level", levelToLoad }));
		BesiegeEntryPoint.CreateEntryPoint(args);
		yield return null;
		loadingLevels = false;
	}
}
