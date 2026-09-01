using System.Collections.Generic;
using UnityEngine;

public class ScoreboardTester : MonoBehaviour
{
	public static readonly string[] RandomWords = new string[50]
	{
		"grass", "scale", "vague", "bloody", "harsh", "regular", "lake", "scene", "Bible", "astonishing",
		"perform", "dome", "accident", "experience", "appreciate", "main", "woman", "recover", "surround", "dough",
		"teach", "potential", "strap", "salon", "wealth", "directory", "passion", "exemption", "service", "module",
		"serve", "sandwich", "trainer", "mark", "articulate", "dirty", "facility", "obscure", "pilot", "environmental",
		"midnight", "employ", "promotion", "inject", "deputy", "opera", "reflect", "dish", "orchestra", "sailor"
	};

	[SerializeField]
	private int randomPlayers = 10;

	private Scoreboard scoreboard;

	private int lastItemId;

	private void Start()
	{
		scoreboard = Object.FindObjectOfType<Scoreboard>();
		if (!(scoreboard == null))
		{
			if (LevelEditor.Instance.Settings == null)
			{
				LevelEditor.Instance.Settings = new LevelSettings();
			}
			FillDummyData();
			FillDummyData();
		}
	}

	private void FillDummyData()
	{
		List<PlayerData> list = new List<PlayerData>();
		for (lastItemId = 0; lastItemId < randomPlayers; lastItemId++)
		{
			int team = Random.Range(0, 5);
			PlayerData playerData = new PlayerData((ushort)lastItemId);
			playerData.name = GetRandomName();
			playerData.ping = Random.Range(5, 700);
			playerData.team = (MPTeam)team;
			playerData.isSpectator = GetIsSpectator();
			list.Add(playerData);
		}
		scoreboard.UpdateScoreboard(list);
	}

	public static string GetRandomName()
	{
		int num = Random.Range(0, RandomWords.Length);
		int num2 = Random.Range(0, RandomWords.Length);
		return RandomWords[num] + " " + RandomWords[num2];
	}

	private bool GetIsSpectator()
	{
		int num = Random.Range(0, 99999);
		if ((num & 5) == 0)
		{
			return true;
		}
		return false;
	}
}
