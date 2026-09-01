using System;
using System.Collections.Generic;

public class LevelSettings
{
	public enum LevelEnvironment
	{
		None = 0,
		Barren = 1,
		Tolbrynd = 3,
		MountainTop = 4,
		LoadingMultiverse = 5,
		Desert = 6,
		Water = 7
	}

	public class LevelMachine
	{
		public byte[] thumbBytes;

		public string thumbString;

		public byte[] machineInfoBytes;

		private MachineInfo machineInfo;

		private string infoString;

		public LevelMachine(MachineInfo info, byte[] thumb)
		{
			machineInfo = info;
			machineInfoBytes = info.Encode();
			thumbString = Encode(thumb);
			thumbBytes = thumb;
		}

		public LevelMachine(string info)
		{
			infoString = info;
			machineInfoBytes = Decode(infoString);
		}

		public LevelMachine(string info, string thumb)
		{
			infoString = info;
			machineInfoBytes = Decode(infoString);
			thumbString = thumb;
			thumbBytes = Decode(thumb);
		}

		public string GetInfoString()
		{
			if (infoString != null)
			{
				return infoString;
			}
			infoString = Encode(machineInfo.Encode());
			return infoString;
		}

		public MachineInfo GetInfo()
		{
			if (machineInfo != null)
			{
				return machineInfo;
			}
			machineInfo = MachineInfo.Decode(machineInfoBytes);
			return machineInfo;
		}

		public byte[] GetMachineData()
		{
			return machineInfoBytes;
		}

		private string Encode(byte[] array)
		{
			return Convert.ToBase64String(CLZF2.Compress(array));
		}

		private byte[] Decode(string str)
		{
			return CLZF2.Decompress(Convert.FromBase64String(str));
		}
	}

	public class GodPowerSetting
	{
		public bool Locked;

		public bool Enabled;

		public GodPowerSetting(bool enabled, bool locked)
		{
			Locked = locked;
			Enabled = enabled;
		}

		public GodPowerSetting()
		{
			Locked = false;
			Enabled = false;
		}
	}

	private static Dictionary<string, LevelEnvironment> envLookup = new Dictionary<string, LevelEnvironment>
	{
		{
			"None",
			LevelEnvironment.None
		},
		{
			"Barren",
			LevelEnvironment.Barren
		},
		{
			"Tolbrynd",
			LevelEnvironment.Tolbrynd
		},
		{
			"Desert",
			LevelEnvironment.Desert
		},
		{
			"MountainTop",
			LevelEnvironment.MountainTop
		},
		{
			"Water",
			LevelEnvironment.Water
		}
	};

	public string Name;

	public int MusicID;

	public int MusicVolume;

	public LevelEnvironment Environment;

	public bool UseVoting;

	public bool AllowCopyMachine;

	public bool AllowExcessPlayers;

	public bool CurtainMode;

	public bool HidePlayerLabels;

	public int MinPlayers;

	public int MaxPlayers;

	public int WaterHeight;

	public int EnvType;

	public Dictionary<string, GodPowerSetting> GodPowerSettings;

	public int BlockCountLimiter;

	public Dictionary<int, int> BlockTypeLimiter;

	public Dictionary<int, int> BaseBlockTypeLimiter;

	public List<LevelMachine> AllowedMachines;

	public bool allowModMachines = true;

	public bool AllowModMachines
	{
		get
		{
			if (LevelEditor.Instance.isActive)
			{
				return true;
			}
			return allowModMachines;
		}
		set
		{
			allowModMachines = value;
		}
	}

	public LevelSettings()
	{
		Name = null;
		AllowExcessPlayers = true;
		AllowCopyMachine = true;
		UseVoting = false;
		CurtainMode = false;
		MusicID = 0;
		MusicVolume = 100;
		MinPlayers = (MaxPlayers = -1);
		BlockCountLimiter = -1;
		WaterHeight = 0;
		EnvType = 0;
		Environment = LevelEnvironment.Barren;
		GodPowerSettings = new Dictionary<string, GodPowerSetting>();
		BlockTypeLimiter = new Dictionary<int, int>();
		BaseBlockTypeLimiter = new Dictionary<int, int>();
		string[] godPowers = ReferenceMaster.Instance.godPowers;
		foreach (string key in godPowers)
		{
			GodPowerSettings.Add(key, new GodPowerSetting());
		}
		AllowedMachines = new List<LevelMachine>();
		allowModMachines = true;
	}

	public bool IsRuleEnabled(string ruleName)
	{
		GodPowerSetting value;
		if (!GodPowerSettings.TryGetValue(ruleName, out value))
		{
			return false;
		}
		return value.Enabled;
	}

	public bool IsRuleLocked(string ruleName)
	{
		GodPowerSetting value;
		if (!GodPowerSettings.TryGetValue(ruleName, out value))
		{
			return false;
		}
		return value.Locked;
	}

	public void SetBlockLimit(BlockType blockType, int limit)
	{
		if (BaseBlockTypeLimiter.ContainsKey((int)blockType))
		{
			if (limit != -1)
			{
				Dictionary<int, int> blockTypeLimiter = BlockTypeLimiter;
				BaseBlockTypeLimiter[(int)blockType] = limit;
				blockTypeLimiter[(int)blockType] = limit;
			}
			else
			{
				BlockTypeLimiter.Remove((int)blockType);
				BaseBlockTypeLimiter.Remove((int)blockType);
			}
		}
		else if (limit != -1)
		{
			BlockTypeLimiter.Add((int)blockType, limit);
			BaseBlockTypeLimiter.Add((int)blockType, limit);
		}
	}

	public void ResetBlockTypeLimiter()
	{
		BaseBlockTypeLimiter.Clear();
		foreach (int key in BlockTypeLimiter.Keys)
		{
			BaseBlockTypeLimiter.Add(key, BlockTypeLimiter[key]);
		}
	}

	public int GetBlockLimit(BlockType blockType)
	{
		int value;
		return (!BaseBlockTypeLimiter.TryGetValue((int)blockType, out value)) ? (-1) : value;
	}

	public static bool ParseEnvironment(string str, out LevelEnvironment env)
	{
		if (envLookup.TryGetValue(str, out env))
		{
			return true;
		}
		env = LevelEnvironment.None;
		return false;
	}
}
