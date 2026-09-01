using System.Collections;
using UnityEngine;

public class StopMultiverseButton : ClickBehaviour
{
	public string levelToLoad;

	public Transform explodedObj;

	public FadeScreen FadeCodey;

	private void Awake()
	{
		releaseOnlyOver = true;
	}

	public override void OnClicked()
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

	public override void OnClickReleased()
	{
		AudioSource component = GetComponent<AudioSource>();
		if (component != null)
		{
			component.Play();
		}
		StartCoroutine(LoadLevel());
	}

	private IEnumerator LoadLevel()
	{
		yield return new WaitForSeconds(0.1f);
		StatMaster.StopHotKeys(false);
		Arguments args = ((!BesiegeEntryPoint.IsSPLevel(levelToLoad)) ? new Arguments(new string[2] { "+load_scene", levelToLoad }) : new Arguments(new string[2] { "+load_level", levelToLoad }));
		BesiegeEntryPoint.CreateEntryPoint(args);
		yield return null;
	}
}
