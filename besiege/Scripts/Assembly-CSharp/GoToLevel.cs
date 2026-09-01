using System.Collections;
using UnityEngine;

public class GoToLevel : MonoBehaviour
{
	public string levelToLoad;

	public FadeScreen FadeCodey;

	private void Awake()
	{
		StartCoroutine(loadLevel());
	}

	private IEnumerator loadLevel()
	{
		Arguments args = ((!BesiegeEntryPoint.IsSPLevel(levelToLoad)) ? new Arguments(new string[2] { "+load_scene", levelToLoad }) : new Arguments(new string[2] { "+load_level", levelToLoad }));
		BesiegeEntryPoint.CreateEntryPoint(args);
		yield return null;
	}
}
