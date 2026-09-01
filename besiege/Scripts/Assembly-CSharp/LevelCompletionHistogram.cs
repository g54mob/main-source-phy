using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class LevelCompletionHistogram : MonoBehaviour
{
	public Transform[] histogram;

	private float[] histogramPcts;

	private static List<SteamLeaderboardDataHandle> leaderboardData = new List<SteamLeaderboardDataHandle>();

	private static int totalLeaderboards = 1;

	private static int receivedLeaderboards = 0;

	private void Start()
	{
		if (!SteamManager.Initialized)
		{
			Debug.LogError("Steam is not initialized!");
			return;
		}
		string[] leaderboardNames = new string[70]
		{
			"1_COTTAGE", "2_WINDMILL", "55_CARAVAN", "3_OLD_HOWL_BATTLEFIELD", "4_PERIMETER_WALL", "5_QUEENS_FODDER", "6_OLD_MINING_SITE", "7_STANDING_STONE", "8_THINSIDE_FORT", "9_MIDLANDS_ENCAMPMENT",
			"10_LYRE_PEAK", "11_HIGHLAND_TOWER", "12_PINE_LUMBER_SITE", "13_SOLOMONS_FLOCK", "14_MARKSMANS_PASS", "15_WYNNFRITH'S_KEEP", "16_THE_DUKE'S_PLEA", "17_SOUTHERN_SHRINE", "18_SCOUTS_OF_TOLBRYND", "19_THE_DUKES_PROTOTYPES",
			"20_THE_DUKES_DEAR_FREIGHTERS", "21_GRAND_CRYSTAL", "22_FARMER_GASCOIGNE", "23_VILLAGE_OF_DIOM", "24_MIDLAND_PATROL", "25_VALLEY_OF_THE_WIND", "26_ODD_CONTRAPTION", "27_DIOM_WELL", "28_SURROUNDED", "29_SACRED_FLAME",
			"30_ARGUS'_GROUNDS", "31_THE_DUKE'S_KNOWLEDGE", "32_THE_VENERATED_HEART", "33_SHATTERED_FIELD", "34_ARAS'_REFUGE", "35_THE_FROZEN_PATH", "36_THE_AWAKENING_BELLS", "37_PECULIAR_CLEARING", "38_The_Martyr_Knights", "39_ORDYCE_LODE",
			"40_MOUNTAIN_BARRIER", "41_RELICT_FROST", "42_CONSUMED_KING", "43_REVOLVING_MONOLITH", "44_PERNITENT_TOWER", "45_TOWERING_EYE", "46_DAHOR_VAULT", "47_FORGOTTEN_SANCTUM_", "48_MESA_OUTPOST", "49_TREE_OF_AKHMORA",
			"50_AMBUSH", "51_STRANGE_ARTEFACT", "52_KAHRAZ_VILLAGE", "53_STOCK_TOWER", "54_THE_LAST_STAND", "56_GULL_ROCK_SHACK", "57_THE_CRAG_NETS", "58_SERPENTs_CREST", "59_WENLEYs_PASS", "60_WRECK_REEF",
			"61_THE_ANGLER_DEEPS", "62_THE_ARMADA", "66_SALTROCK_FORTRESS", "67_STORMRUNNERs_GRAVE", "68_IRONWEAVE_PASSAGE", "69_FEEDING_FRENZY", "63_DRASCKARs_BASTILLE", "64_SHIPWRECK_CAY", "65_RAZORTOOTH_CAVE", "70_THE_DEVOURING_PIT"
		};
		StartCoroutine(FetchLeaderboards(leaderboardNames));
	}

	private IEnumerator FetchLeaderboards(string[] leaderboardNames)
	{
		totalLeaderboards = leaderboardNames.Length;
		for (int i = 0; i < totalLeaderboards; i++)
		{
			SteamLeaderboardDataHandle l = new SteamLeaderboardDataHandle(LeaderboardDataType.BlockScore, "LEVEL_" + leaderboardNames[i]);
			leaderboardData.Add(l);
			l.GetLeaderboardData(ProcessLeaderboards, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 1);
			yield return null;
		}
	}

	private void ProcessLeaderboards(LeaderboardScoresDownloaded_t s)
	{
		receivedLeaderboards++;
		if (receivedLeaderboards == totalLeaderboards)
		{
			int[] array = new int[totalLeaderboards];
			int num = 0;
			for (int i = 0; i < totalLeaderboards; i++)
			{
				num += (array[i] = leaderboardData[i].GetLeaderBoardEntryCount());
			}
			float[] pct = CalculatePercentages(array, num);
			PopulateHistogram(pct);
			base.enabled = false;
		}
	}

	private float[] CalculatePercentages(int[] entryCounts, int totalEntries)
	{
		float[] array = new float[entryCounts.Length];
		for (int i = 0; i < entryCounts.Length; i++)
		{
			array[i] = ((totalEntries <= 0) ? 0f : ((float)entryCounts[i] / (float)totalEntries));
		}
		return array;
	}

	internal void PopulateHistogram(float[] pct)
	{
		float num = 0f;
		for (int i = 0; i < pct.Length; i++)
		{
			if (pct[i] > num)
			{
				num = pct[i];
			}
		}
		if (float.IsNaN(num))
		{
			return;
		}
		for (int j = 0; j < pct.Length; j++)
		{
			float num2 = 0f;
			if (j < pct.Length)
			{
				num2 = Mathf.Sqrt(pct[j] / num);
			}
			if (num2 <= 0.001f || float.IsNaN(num2))
			{
				num2 = 0.001f;
			}
			if (histogram[j] != null)
			{
				histogram[j].localScale = new Vector3(0.1f, num2, 1f);
				histogram[j].localPosition = new Vector3(0.05f + (float)j * 0.1f, num2 * 0.5f, 0f);
			}
		}
		if (pct.Length >= histogram.Length)
		{
			return;
		}
		for (int k = pct.Length - 1; k < histogram.Length; k++)
		{
			if (histogram[k] != null)
			{
				histogram[k].localScale = new Vector3(0.1f, 0.05f, 1f);
				histogram[k].localPosition = new Vector3(0.05f + (float)k * 0.1f, 0.025f, 0f);
			}
		}
	}
}
