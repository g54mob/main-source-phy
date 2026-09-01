using System;
using UnityEngine;

public class IslandConqueredFX : MonoBehaviour
{
	public Renderer islandVis;

	public Material islandNormalMaterial;

	public Material islandBurnedMaterial;

	public Transform objDisableOnConquer;

	public Transform objEnableOnConquer;

	public int levelIdToCheck = 14;

	public Transform conqueredTextMesh;

	public bool levelSelectMode;

	private void Start()
	{
		Check();
		ReferenceMaster.onConquerToggled = (Action)Delegate.Combine(ReferenceMaster.onConquerToggled, new Action(Check));
	}

	private void OnDestroy()
	{
		ReferenceMaster.onConquerToggled = (Action)Delegate.Remove(ReferenceMaster.onConquerToggled, new Action(Check));
	}

	private void Check()
	{
		if (levelIdToCheck >= LEVELLORD.levelsComplete.Length)
		{
			Debug.LogError("index out of range: " + levelIdToCheck);
			levelIdToCheck = LEVELLORD.levelsComplete.Length - 1;
		}
		if (OptionsMaster.BesiegeConfig.ShowConquered && LEVELLORD.levelsComplete[levelIdToCheck] == 1)
		{
			if (levelSelectMode)
			{
				SetConqueredVisSimple();
			}
			else
			{
				SetConqueredVis();
			}
		}
		else if (levelSelectMode)
		{
			SetNormalVisSimple();
		}
		else
		{
			SetNormalVis();
		}
	}

	private void SetConqueredVis()
	{
		objDisableOnConquer.gameObject.SetActive(false);
		objEnableOnConquer.gameObject.SetActive(true);
		islandVis.material = islandBurnedMaterial;
		conqueredTextMesh.gameObject.SetActive(true);
	}

	private void SetNormalVis()
	{
		objDisableOnConquer.gameObject.SetActive(true);
		objEnableOnConquer.gameObject.SetActive(false);
		islandVis.material = islandNormalMaterial;
		conqueredTextMesh.gameObject.SetActive(false);
	}

	private void SetConqueredVisSimple()
	{
		objEnableOnConquer.gameObject.SetActive(true);
	}

	private void SetNormalVisSimple()
	{
		objEnableOnConquer.gameObject.SetActive(false);
	}
}
