using System.Collections;
using UnityEngine;

public class IslandController : ClickBehaviour
{
	public int levelToCheckForUnlock;

	public string levelToLoad;

	public bool onlyLoadOnHover;

	public bool avalibleFromCleanInstall;

	public HighlightOnMouseOver highlightCode;

	public Color lockedColour;

	public Color unlockedColour;

	public TextMesh tooltipName;

	public Transform[] tooltipsToDisable;

	private bool hoverOver;

	private bool IsUnlocked
	{
		get
		{
			return avalibleFromCleanInstall || LEVELLORD.levelsComplete[levelToCheckForUnlock] == 1;
		}
	}

	protected void Start()
	{
		if (IsUnlocked)
		{
			highlightCode.colourToLerpTo = unlockedColour;
			tooltipName.color = Color.white;
			for (int i = 0; i < tooltipsToDisable.Length; i++)
			{
				tooltipsToDisable[i].gameObject.SetActive(false);
			}
		}
		else
		{
			highlightCode.colourToLerpTo = lockedColour;
		}
		releaseOnlyOver = true;
	}

	public override void OnClickReleased()
	{
		if (IsUnlocked && (!onlyLoadOnHover || hoverOver))
		{
			StartCoroutine(loadLevel());
		}
	}

	public override void OnCursorOver()
	{
		base.OnCursorOver();
		hoverOver = true;
	}

	protected void OnMouseExit()
	{
		hoverOver = false;
	}

	private IEnumerator loadLevel()
	{
		Arguments args = new Arguments(new string[2] { "+load_scene", levelToLoad });
		BesiegeEntryPoint.CreateEntryPoint(args);
		yield break;
	}
}
