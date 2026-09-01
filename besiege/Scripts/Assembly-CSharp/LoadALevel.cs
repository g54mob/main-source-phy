using System.Collections;

public class LoadALevel : ClickBehaviour
{
	public bool unlocked;

	public bool forceString;

	public string levelToLoad;

	public bool triggerOnStart;

	public IndividualProvinceController provinceController;

	private void Awake()
	{
		if (provinceController != null && !forceString)
		{
			levelToLoad = string.Empty + (provinceController.myIndex + 1);
		}
		if (triggerOnStart)
		{
			StartCoroutine(loadLevel());
		}
		releaseOnlyOver = true;
	}

	public override void OnClickReleased()
	{
		if (unlocked)
		{
			StartCoroutine(loadLevel());
		}
	}

	private IEnumerator loadLevel()
	{
		Arguments args = ((!BesiegeEntryPoint.IsSPLevel(levelToLoad)) ? new Arguments(new string[2] { "+load_scene", levelToLoad }) : new Arguments(new string[2] { "+load_level", levelToLoad }));
		BesiegeEntryPoint.CreateEntryPoint(args);
		yield break;
	}
}
