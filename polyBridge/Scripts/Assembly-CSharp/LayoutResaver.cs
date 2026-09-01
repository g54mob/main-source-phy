using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LayoutResaver
{
	public static bool m_Resaving;

	private static LayoutResaverSubMode m_LayoutResaverSubMode;

	private static bool m_WaitOneMoreFrame;

	private static List<string> m_LayoutsToSave = new List<string>();

	private static int m_NextSlotToDumpIndex;

	private static string m_LayoutsDirectory;

	private static HashSet<string> m_DuplicatedLevels = new HashSet<string>();

	public static void Resave(string layoutsDir, List<string> layouts)
	{
		m_LayoutsDirectory = layoutsDir;
		m_LayoutsToSave = new List<string>(layouts);
		m_NextSlotToDumpIndex = 0;
		m_Resaving = true;
		m_LayoutResaverSubMode = LayoutResaverSubMode.PRELOAD_LEVEL;
		GameStatePreloadingAssets.PreloadLevel(m_LayoutsToSave[m_NextSlotToDumpIndex], null, PreloadCallback);
	}

	public static void Process()
	{
		if (!m_Resaving)
		{
			return;
		}
		if (m_NextSlotToDumpIndex >= m_LayoutsToSave.Count)
		{
			End();
			m_Resaving = false;
			GameStateManager.SwitchToState(GameState.MAIN_MENU);
		}
		else
		{
			if (m_LayoutResaverSubMode == LayoutResaverSubMode.PRELOAD_LEVEL)
			{
				return;
			}
			if (m_LayoutResaverSubMode == LayoutResaverSubMode.LOAD_LEVEL)
			{
				GameManager.SetGameMode(GameMode.SANDBOX, GameSubMode.NONE);
				GameStateManager.SwitchToState(GameState.SANDBOX);
				Sandbox.LoadLayout(m_LayoutsToSave[m_NextSlotToDumpIndex]);
				m_LayoutResaverSubMode = LayoutResaverSubMode.READY_TO_SAVE;
				m_WaitOneMoreFrame = true;
				return;
			}
			if (m_WaitOneMoreFrame)
			{
				m_WaitOneMoreFrame = false;
				return;
			}
			m_LayoutResaverSubMode = LayoutResaverSubMode.PRELOAD_LEVEL;
			Resave(m_LayoutsToSave[m_NextSlotToDumpIndex]);
			m_NextSlotToDumpIndex++;
			if (m_NextSlotToDumpIndex < m_LayoutsToSave.Count)
			{
				GameStatePreloadingAssets.PreloadLevel(m_LayoutsToSave[m_NextSlotToDumpIndex], null, PreloadCallback);
			}
		}
	}

	public static void End()
	{
		foreach (string duplicatedLevel in m_DuplicatedLevels)
		{
			Debug.Log(duplicatedLevel);
		}
	}

	private static void Resave(string fullpath)
	{
		int num = 0;
		bool flag = false;
		for (int i = 0; i < Decors.m_Decors.Count; i++)
		{
			if (Decors.m_Decors[i].m_NoSave)
			{
				continue;
			}
			for (int j = 0; j < Decors.m_Decors.Count; j++)
			{
				if (i != j && !Decors.m_Decors[j].m_NoSave && Utils.ApproximatelyEquals(Decors.m_Decors[i].transform.position, Decors.m_Decors[j].transform.position) && Decors.m_Decors[i].m_Id == Decors.m_Decors[j].m_Id)
				{
					Decors.m_Decors[j].m_NoSave = true;
					flag = true;
				}
			}
		}
		if (flag)
		{
			for (int num2 = Decors.m_Decors.Count - 1; num2 >= 0; num2--)
			{
				if (Decors.m_Decors[num2].m_NoSave)
				{
					Decors.m_Decors.RemoveAt(num2);
					num++;
				}
			}
			if (num > 0)
			{
				uConsole.Log($"Removed {num} decor for {Path.GetFileName(fullpath)}");
			}
		}
		if (flag)
		{
			Sandbox.Save(Path.Combine(m_LayoutsDirectory, Path.GetFileName(fullpath)));
		}
	}

	private static void PreloadCallback(string levelFilename, FileSlot slot)
	{
		m_LayoutResaverSubMode = LayoutResaverSubMode.LOAD_LEVEL;
	}
}
