using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using InternalModding.Mods;
using Modding;
using UnityEngine;

[Serializable]
public class ControlScheme
{
	[Serializable]
	public class ControlEntry
	{
		[XmlAttribute]
		public string Name;

		public ControlOption[] Options;

		[XmlIgnore]
		public string category = string.Empty;

		[XmlIgnore]
		public int NameLocID = -1;

		[XmlIgnore]
		public int SplitLocID = -1;

		[XmlIgnore]
		public bool Rebindable = true;

		public ControlEntry()
		{
		}

		public ControlEntry(string category, string name, int locId, params ControlOption[] o)
		{
			Name = name;
			NameLocID = locId;
			SplitLocID = 914;
			Options = o;
			this.category = category;
		}

		public ControlEntry(string category, object entryType, int[] converter, params ControlOption[] o)
		{
			Name = entryType.ToString();
			NameLocID = converter[(int)entryType];
			SplitLocID = 914;
			Options = o;
			this.category = category;
		}

		public ControlEntry(string category, object entryType, int[] converter, int splitID, params ControlOption[] o)
		{
			Name = entryType.ToString();
			NameLocID = converter[(int)entryType];
			SplitLocID = splitID;
			Options = o;
			this.category = category;
		}

		public ControlEntry NonRebindable()
		{
			Rebindable = false;
			return this;
		}

		public void Set(ControlOption[] options)
		{
			Options = options;
		}
	}

	[Serializable]
	public class ControlOption
	{
		[XmlIgnore]
		public Action<ControlOption> onChanged;

		public KeyCode[] Keys;

		public ControlOption()
		{
		}

		public ControlOption(params KeyCode[] g)
		{
			Keys = g;
		}

		public void Set(KeyCode[] keys)
		{
			Keys = keys;
			if (onChanged != null)
			{
				onChanged(this);
			}
			if (ReferenceMaster.onControlsChanged != null)
			{
				ReferenceMaster.onControlsChanged();
			}
		}
	}

	public enum GeneralControls
	{
		Click = 0,
		Simulation = 1,
		MoveCamera = 2,
		RotateCamera = 3,
		FocusCamera = 4,
		PanCamera = 5,
		ScrollCamera = 6,
		SnapCamera = 7,
		ToggleUI = 8,
		Escape = 9,
		ResetCamera = 10,
		ExtendedInfo = 11,
		PlayerList = 12,
		Screenshot = 13,
		Chat = 14,
		Console = 15
	}

	public enum BuildingControls
	{
		DeleteBlock = 0,
		Flip = 1,
		Rotate = 2,
		PickBlock = 3,
		CopyInformation = 4,
		PasteInformation = 5,
		Undo = 6,
		Redo = 7,
		FindBlock = 8
	}

	public enum AdvancedControls
	{
		Toggle = 0,
		Translate = 1,
		Rotate = 2,
		Mirror = 3,
		Modify = 4,
		PaintTool = 5,
		SelectMoreObjects = 6,
		ToggleGrid = 7,
		InverseGizmo = 8,
		SelectAll = 9,
		SelectInverse = 10,
		Duplicate = 11,
		BreakSurface = 12
	}

	public enum EditorControls
	{
		Translate = 0,
		Rotate = 1,
		Scale = 2,
		Mirror = 3,
		Modify = 4,
		SelectMoreObjects = 5,
		ToggleGrid = 6,
		InverseGizmo = 7,
		Paintbrush = 8,
		RotateGhost = 9,
		ScaleGhost = 10,
		CenterOnObject = 11,
		SelectAll = 12,
		Duplicate = 13
	}

	public enum BlockControls
	{
		None = -1,
		Wheels = 0,
		Steering = 1,
		Piston = 2,
		Detach = 3,
		Spring = 4,
		Rope = 5,
		FlyingBlock = 6,
		Balloon = 7,
		Cannons = 8,
		Crossbow = 9,
		Rocket = 10,
		Flamethrower = 11,
		WaterCannon = 12,
		Vacuum = 13,
		Jaw = 14,
		Grenade = 15,
		Clutch = 16,
		Activate = 17,
		Activate2 = 18,
		Automate = 19,
		Sail = 20,
		Barrel = 21,
		Harpoon = 22,
		Pin = 23,
		Camera = 24
	}

	public int version;

	public ControlEntry[] General;

	public ControlEntry[] Building;

	public ControlEntry[] AdvancedBuilding;

	public ControlEntry[] LevelEditor;

	public ControlEntry[] Blocks;

	public static int[] indexToLocGeneral = new int[16]
	{
		3601, 3022, 3024, 916, 920, 917, 918, 5030, 3053, 3603,
		1533, 3021, 3054, 911, 4593, 3598
	};

	public static int[] indexToLocBuilding = new int[9] { 3027, 3029, 910, 5091, 3030, 3032, 846, 847, 3597 };

	public static int[] indexToLocAdvanced = new int[13]
	{
		2406, 3041, 3042, 3044, 3604, 3563, 3036, 3048, 3047, 3602,
		3600, 3051, 3866
	};

	public static int[] indexToLocLevelEditor = new int[14]
	{
		3041, 3042, 3043, 3044, 3604, 3036, 3048, 3047, 3045, 3046,
		3599, 3049, 3050, 3051
	};

	public static int[] indexToLocBlocks = new int[25]
	{
		4645, 3676, 641, 2494, 1955, 1988, 1960, 4284, 687, 699,
		732, 1967, 1997, 2002, 4427, 727, 4591, 3768, 3768, 3769,
		4475, 2224, 4440, 584, 2937
	};

	public ControlScheme()
	{
		SetDefaultValues();
	}

	public void Save(string controlFile, BesiegeFileManager.FileLocation controlLocation)
	{
		using (MemoryStream memoryStream = new MemoryStream())
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ControlScheme));
			xmlSerializer.Serialize(memoryStream, this);
			if (!BesiegeFileManager.Save(controlFile, controlLocation, memoryStream.ToArray()))
			{
				Debug.LogError("Couldn't save to " + controlFile + "!");
			}
		}
	}

	private void CopyOptions(ControlEntry[] from, ControlEntry[] to, bool checkResync = false)
	{
		if (from.Length != to.Length)
		{
			checkResync = true;
		}
		int f = 0;
		for (int i = 0; i < to.Length; i++)
		{
			if (checkResync && f < from.Length && to[i].Name != from[f].Name && ResyncOptions(from, to, ref f, ref i))
			{
				checkResync = false;
			}
			if (f < from.Length)
			{
				to[i].Options = from[f].Options;
				FixOldScroll(to[i].Options);
				f++;
			}
		}
	}

	private void FixOldScroll(ControlOption[] o)
	{
		if (version != 0)
		{
			return;
		}
		for (int i = 0; i < o.Length; i++)
		{
			for (int j = 0; j < o[i].Keys.Length; j++)
			{
				switch (o[i].Keys[j])
				{
				case KeyCode.Mouse3:
					o[i].Keys[j] = KeyCode.DoubleQuote;
					break;
				case KeyCode.Mouse4:
					o[i].Keys[j] = KeyCode.Caret;
					break;
				}
			}
		}
	}

	private bool ResyncOptions(ControlEntry[] from, ControlEntry[] to, ref int f, ref int t)
	{
		for (int i = 1; i < from.Length - f; i++)
		{
			if (f + i < from.Length && to[t].Name == from[f + i].Name)
			{
				f += i;
				return false;
			}
		}
		for (int j = 1; j < to.Length - t; j++)
		{
			if (f < from.Length && to[t + j].Name == from[f].Name)
			{
				t += j;
				return false;
			}
		}
		return true;
	}

	public void Load(string controlFile, BesiegeFileManager.FileLocation controlLocation)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(ControlScheme));
		byte[] data;
		if (BesiegeFileManager.Load(controlFile, controlLocation, out data))
		{
			ControlScheme controlScheme;
			using (MemoryStream stream = new MemoryStream(data))
			{
				controlScheme = xmlSerializer.Deserialize(stream) as ControlScheme;
			}
			version = controlScheme.version;
			CopyOptions(controlScheme.General, General, true);
			CopyOptions(controlScheme.Building, Building, true);
			CopyOptions(controlScheme.AdvancedBuilding, AdvancedBuilding, true);
			CopyOptions(controlScheme.LevelEditor, LevelEditor, true);
			CopyOptions(controlScheme.Blocks, Blocks, true);
			version = 1;
		}
	}

	private void SetDefaultValues()
	{
		KeyCode keyCode = KeyCode.LeftControl;
		General = new ControlEntry[16]
		{
			new ControlEntry("General", GeneralControls.Click, indexToLocGeneral, new ControlOption(KeyCode.Mouse0)).NonRebindable(),
			new ControlEntry("General", GeneralControls.Simulation, indexToLocGeneral, new ControlOption(KeyCode.Space)).NonRebindable(),
			new ControlEntry("General", GeneralControls.MoveCamera, indexToLocGeneral, 3463, new ControlOption(KeyCode.W), new ControlOption(KeyCode.A), new ControlOption(KeyCode.S), new ControlOption(KeyCode.D)),
			new ControlEntry("General", GeneralControls.RotateCamera, indexToLocGeneral, new ControlOption(KeyCode.Mouse1)).NonRebindable(),
			new ControlEntry("General", GeneralControls.FocusCamera, indexToLocGeneral, new ControlOption(KeyCode.Mouse2)).NonRebindable(),
			new ControlEntry("General", GeneralControls.PanCamera, indexToLocGeneral, new ControlOption(KeyCode.Mouse2)).NonRebindable(),
			new ControlEntry("General", GeneralControls.ScrollCamera, indexToLocGeneral, 3463, new ControlOption(KeyCode.DoubleQuote), new ControlOption(KeyCode.Caret)).NonRebindable(),
			new ControlEntry("General", GeneralControls.SnapCamera, indexToLocGeneral, new ControlOption(KeyCode.LeftAlt)),
			new ControlEntry("General", GeneralControls.ToggleUI, indexToLocGeneral, new ControlOption(KeyCode.Tab)),
			new ControlEntry("General", GeneralControls.Escape, indexToLocGeneral, new ControlOption(KeyCode.Escape)).NonRebindable(),
			new ControlEntry("General", GeneralControls.ResetCamera, indexToLocGeneral, new ControlOption(KeyCode.F1)).NonRebindable(),
			new ControlEntry("General", GeneralControls.ExtendedInfo, indexToLocGeneral, new ControlOption(KeyCode.F2)).NonRebindable(),
			new ControlEntry("General", GeneralControls.PlayerList, indexToLocGeneral, new ControlOption(KeyCode.F3)).NonRebindable(),
			new ControlEntry("General", GeneralControls.Screenshot, indexToLocGeneral, new ControlOption(KeyCode.F12)).NonRebindable(),
			new ControlEntry("General", GeneralControls.Chat, indexToLocGeneral, new ControlOption(KeyCode.Return)),
			new ControlEntry("General", GeneralControls.Console, indexToLocGeneral, new ControlOption(keyCode, KeyCode.K))
		};
		Building = new ControlEntry[9]
		{
			new ControlEntry("Building", BuildingControls.DeleteBlock, indexToLocBuilding, new ControlOption(KeyCode.Delete), new ControlOption(KeyCode.X)),
			new ControlEntry("Building", BuildingControls.Flip, indexToLocBuilding, new ControlOption(KeyCode.F)),
			new ControlEntry("Building", BuildingControls.Rotate, indexToLocBuilding, new ControlOption(KeyCode.R)),
			new ControlEntry("Building", BuildingControls.PickBlock, indexToLocBuilding, new ControlOption(keyCode, KeyCode.Mouse2)),
			new ControlEntry("Building", BuildingControls.CopyInformation, indexToLocBuilding, new ControlOption(keyCode, KeyCode.C)),
			new ControlEntry("Building", BuildingControls.PasteInformation, indexToLocBuilding, new ControlOption(keyCode, KeyCode.V)),
			new ControlEntry("Building", BuildingControls.Undo, indexToLocBuilding, new ControlOption(keyCode, KeyCode.Z)),
			new ControlEntry("Building", BuildingControls.Redo, indexToLocBuilding, new ControlOption(keyCode, KeyCode.Y), new ControlOption(keyCode, KeyCode.LeftShift, KeyCode.Z)),
			new ControlEntry("Building", BuildingControls.FindBlock, indexToLocBuilding, new ControlOption(keyCode, KeyCode.F))
		};
		AdvancedBuilding = new ControlEntry[13]
		{
			new ControlEntry("AdvancedBuilding", AdvancedControls.Toggle, indexToLocAdvanced, new ControlOption(default(KeyCode))),
			new ControlEntry("AdvancedBuilding", AdvancedControls.Translate, indexToLocAdvanced, new ControlOption(default(KeyCode))),
			new ControlEntry("AdvancedBuilding", AdvancedControls.Rotate, indexToLocAdvanced, new ControlOption(default(KeyCode))),
			new ControlEntry("AdvancedBuilding", AdvancedControls.Mirror, indexToLocAdvanced, new ControlOption(default(KeyCode))),
			new ControlEntry("AdvancedBuilding", AdvancedControls.Modify, indexToLocAdvanced, new ControlOption(default(KeyCode))),
			new ControlEntry("AdvancedBuilding", AdvancedControls.PaintTool, indexToLocAdvanced, new ControlOption(default(KeyCode))),
			new ControlEntry("AdvancedBuilding", AdvancedControls.SelectMoreObjects, indexToLocAdvanced, new ControlOption(KeyCode.LeftShift)),
			new ControlEntry("AdvancedBuilding", AdvancedControls.ToggleGrid, indexToLocAdvanced, new ControlOption(keyCode)),
			new ControlEntry("AdvancedBuilding", AdvancedControls.InverseGizmo, indexToLocAdvanced, new ControlOption(KeyCode.LeftAlt)),
			new ControlEntry("AdvancedBuilding", AdvancedControls.SelectAll, indexToLocAdvanced, new ControlOption(keyCode, KeyCode.A)),
			new ControlEntry("AdvancedBuilding", AdvancedControls.SelectInverse, indexToLocAdvanced, new ControlOption(keyCode, KeyCode.I)),
			new ControlEntry("AdvancedBuilding", AdvancedControls.Duplicate, indexToLocAdvanced, new ControlOption(keyCode, KeyCode.D)),
			new ControlEntry("AdvancedBuilding", AdvancedControls.BreakSurface, indexToLocAdvanced, new ControlOption(keyCode, KeyCode.B))
		};
		LevelEditor = new ControlEntry[14]
		{
			new ControlEntry("LevelEditor", EditorControls.Translate, indexToLocLevelEditor, new ControlOption(KeyCode.Alpha1)),
			new ControlEntry("LevelEditor", EditorControls.Rotate, indexToLocLevelEditor, new ControlOption(KeyCode.Alpha2)),
			new ControlEntry("LevelEditor", EditorControls.Scale, indexToLocLevelEditor, new ControlOption(KeyCode.Alpha3)),
			new ControlEntry("LevelEditor", EditorControls.Mirror, indexToLocLevelEditor, new ControlOption(KeyCode.Alpha4)),
			new ControlEntry("LevelEditor", EditorControls.Modify, indexToLocLevelEditor, new ControlOption(KeyCode.Alpha5)),
			new ControlEntry("LevelEditor", EditorControls.SelectMoreObjects, indexToLocLevelEditor, new ControlOption(KeyCode.LeftShift)),
			new ControlEntry("LevelEditor", EditorControls.ToggleGrid, indexToLocLevelEditor, new ControlOption(keyCode)),
			new ControlEntry("LevelEditor", EditorControls.InverseGizmo, indexToLocLevelEditor, new ControlOption(KeyCode.LeftAlt)),
			new ControlEntry("LevelEditor", EditorControls.Paintbrush, indexToLocLevelEditor, new ControlOption(KeyCode.B)),
			new ControlEntry("LevelEditor", EditorControls.RotateGhost, indexToLocLevelEditor, new ControlOption(KeyCode.R)),
			new ControlEntry("LevelEditor", EditorControls.ScaleGhost, indexToLocLevelEditor, new ControlOption(KeyCode.T)),
			new ControlEntry("LevelEditor", EditorControls.CenterOnObject, indexToLocLevelEditor, new ControlOption(KeyCode.C)),
			new ControlEntry("LevelEditor", EditorControls.SelectAll, indexToLocLevelEditor, new ControlOption(keyCode, KeyCode.A)),
			new ControlEntry("LevelEditor", EditorControls.Duplicate, indexToLocLevelEditor, new ControlOption(keyCode, KeyCode.D))
		};
		Blocks = new ControlEntry[25]
		{
			new ControlEntry("Blocks", BlockControls.Wheels, indexToLocBlocks, 3463, new ControlOption(KeyCode.UpArrow), new ControlOption(KeyCode.DownArrow)),
			new ControlEntry("Blocks", BlockControls.Steering, indexToLocBlocks, 3463, new ControlOption(KeyCode.LeftArrow), new ControlOption(KeyCode.RightArrow)),
			new ControlEntry("Blocks", BlockControls.Piston, indexToLocBlocks, new ControlOption(KeyCode.H)),
			new ControlEntry("Blocks", BlockControls.Detach, indexToLocBlocks, new ControlOption(KeyCode.V)),
			new ControlEntry("Blocks", BlockControls.Spring, indexToLocBlocks, new ControlOption(KeyCode.L)),
			new ControlEntry("Blocks", BlockControls.Rope, indexToLocBlocks, 3463, new ControlOption(KeyCode.N), new ControlOption(KeyCode.M)),
			new ControlEntry("Blocks", BlockControls.FlyingBlock, indexToLocBlocks, new ControlOption(KeyCode.O)),
			new ControlEntry("Blocks", BlockControls.Balloon, indexToLocBlocks, 3463, new ControlOption(KeyCode.U), new ControlOption(KeyCode.J)),
			new ControlEntry("Blocks", BlockControls.Cannons, indexToLocBlocks, new ControlOption(KeyCode.C)),
			new ControlEntry("Blocks", BlockControls.Crossbow, indexToLocBlocks, new ControlOption(KeyCode.C)),
			new ControlEntry("Blocks", BlockControls.Rocket, indexToLocBlocks, new ControlOption(KeyCode.T)),
			new ControlEntry("Blocks", BlockControls.Flamethrower, indexToLocBlocks, new ControlOption(KeyCode.Y)),
			new ControlEntry("Blocks", BlockControls.WaterCannon, indexToLocBlocks, new ControlOption(KeyCode.Y)),
			new ControlEntry("Blocks", BlockControls.Vacuum, indexToLocBlocks, new ControlOption(KeyCode.Y)),
			new ControlEntry("Blocks", BlockControls.Jaw, indexToLocBlocks, new ControlOption(KeyCode.X)),
			new ControlEntry("Blocks", BlockControls.Grenade, indexToLocBlocks, new ControlOption(KeyCode.K)),
			new ControlEntry("Blocks", BlockControls.Clutch, indexToLocBlocks, new ControlOption(KeyCode.J)),
			new ControlEntry("Blocks", BlockControls.Activate, indexToLocBlocks, new ControlOption(KeyCode.B)),
			new ControlEntry("Blocks", BlockControls.Activate2, indexToLocBlocks, 3463, new ControlOption(KeyCode.U), new ControlOption(KeyCode.I)),
			new ControlEntry("Blocks", BlockControls.Automate, indexToLocBlocks, new ControlOption(KeyCode.C)),
			new ControlEntry("Blocks", BlockControls.Sail, indexToLocBlocks, 3463, new ControlOption(KeyCode.I), new ControlOption(KeyCode.K)),
			new ControlEntry("Blocks", BlockControls.Barrel, indexToLocBlocks, 3463, new ControlOption(KeyCode.U), new ControlOption(KeyCode.J)),
			new ControlEntry("Blocks", BlockControls.Harpoon, indexToLocBlocks, 3463, new ControlOption(KeyCode.C), new ControlOption(KeyCode.V)),
			new ControlEntry("Blocks", BlockControls.Pin, indexToLocBlocks, new ControlOption(KeyCode.P)),
			new ControlEntry("Blocks", BlockControls.Camera, indexToLocBlocks, new ControlOption(KeyCode.F))
		};
	}

	public static ControlEntry[] GenerateModControlList(out ControlEntry[] defaults)
	{
		Dictionary<ModContainer, Dictionary<string, ModKey>> keys = ModKeys.Keys;
		List<ControlEntry> list = new List<ControlEntry>();
		List<ControlEntry> list2 = new List<ControlEntry>();
		foreach (KeyValuePair<ModContainer, Dictionary<string, ModKey>> item in keys)
		{
			string name = item.Key.Info.Name;
			foreach (ModInfo.KeyInfo key in item.Key.Info.Keys)
			{
				list.Add(ModKeyToControlEntry(name + ": " + key.Name, 0, key.DefaultModifier, key.DefaultTrigger));
			}
			foreach (KeyValuePair<string, ModKey> item2 in item.Value)
			{
				list2.Add(ModKeyToControlEntry(name + ": " + item2.Key, 0, item2.Value));
			}
		}
		defaults = list.ToArray();
		return list2.ToArray();
	}

	public static ControlEntry ModKeyToControlEntry(string name, int loc, ModKey modKey, Action<ControlOption> callback = null)
	{
		ControlEntry controlEntry = ModKeyToControlEntry(name, loc, modKey.Modifier, modKey.Trigger);
		controlEntry.Options[0].onChanged = callback;
		return controlEntry;
	}

	public static ControlEntry ModKeyToControlEntry(string name, int loc, KeyCode modifier, KeyCode trigger)
	{
		ControlOption controlOption = ((modifier != KeyCode.None) ? new ControlOption(modifier, trigger) : new ControlOption(trigger));
		return new ControlEntry("Mods", name, loc, controlOption);
	}
}
