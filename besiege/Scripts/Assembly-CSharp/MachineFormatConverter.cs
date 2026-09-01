using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class MachineFormatConverter
{
	private static int[] prefabReferences;

	private static Vector3[] blockPositions;

	private static Quaternion[] blockRotations;

	private static int[] wheelFlipped;

	private static Vector3[] braceStartPos;

	private static Vector3[] braceEndPos;

	private static Vector3 machinePosition;

	private static float machineRotation;

	private static string[] myKeyMaps1;

	private static string[] myKeyMaps2;

	private static float[] extraFloat;

	private static bool[] extraBoolean;

	private static bool hasKeyLookup;

	private static Dictionary<string, KeyCode> keyLookup;

	public static MachineInfo ConvertBsgToMachineInfo(string name)
	{
		return ConvertBsgToMachineInfo(name, StaticSettings.MachinePath + "/" + name + ".bsg");
	}

	public static MachineInfo ConvertBsgToMachineInfo(string name, string path)
	{
		ReadData(path);
		MachineInfo machineInfo = new MachineInfo();
		machineInfo.Position = machinePosition;
		machineInfo.Rotation = Quaternion.Euler(0f, machineRotation, 0f);
		machineInfo.Name = name;
		machineInfo.Author = string.Empty;
		machineInfo.Type = ((!path.ToLower().Contains("workshop")) ? MachineInfo.MachineType.Local : MachineInfo.MachineType.Workshop);
		for (int i = 0; i < prefabReferences.Length; i++)
		{
			BlockInfo block;
			if (LoadBlock(i, out block))
			{
				machineInfo.Blocks.Add(block);
			}
		}
		return machineInfo;
	}

	private static void ReadData(string path)
	{
		StreamReader streamReader = new StreamReader(path);
		string text = null;
		int num = 0;
		do
		{
			switch (num)
			{
			case 2:
				ReadBlockID(text);
				break;
			case 4:
				ReadVector3(text);
				break;
			case 6:
				ReadQuaternion(text);
				break;
			case 8:
				ReadWheelFlipped(text);
				break;
			case 10:
				ReadBraceStartPos(text);
				break;
			case 12:
				ReadBraceEndPos(text);
				break;
			case 14:
				ReadParentPosition(text);
				break;
			case 16:
				ReadParentRotation(text);
				break;
			case 18:
				ReadKey1(text);
				break;
			case 20:
				ReadKey2(text);
				break;
			case 22:
				ReadExtraFloat(text);
				break;
			case 24:
				ReadExtraBoolean(text);
				break;
			}
			text = streamReader.ReadLine();
			num++;
		}
		while (text != null);
	}

	private static bool LoadBlock(int i, out BlockInfo block)
	{
		block = new BlockInfo();
		if (prefabReferences == null || i >= prefabReferences.Length)
		{
			return false;
		}
		int num = prefabReferences[i];
		block.ID = (BlockType)num;
		if (blockPositions == null || i >= blockPositions.Length)
		{
			return false;
		}
		block.Position = blockPositions[i];
		if (blockRotations == null || i >= blockRotations.Length)
		{
			return false;
		}
		block.Rotation = blockRotations[i];
		block.Scale = PrefabMaster.GetDefaultScale(block.ID);
		if (PrefabMaster.BlockPrefabs.ContainsKey(num))
		{
			block.Skin = PrefabMaster.BlockPrefabs[num].DefaultSkin;
		}
		else
		{
			Debug.LogError("Couldn't assign default skin for block ID " + num + "!");
		}
		XDataHolder blockData = block.BlockData;
		if (EqualsAny(num, new int[6] { 2, 17, 22, 39, 46, 48 }))
		{
			if (extraFloat == null || i >= extraFloat.Length)
			{
				return false;
			}
			blockData.Write("bmt-speed", extraFloat[i]);
			if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
			{
				return false;
			}
			blockData.Write("bmt-forward", GetStringArrayFromKey(myKeyMaps1[i]));
			if (myKeyMaps2 == null || i >= myKeyMaps2.Length)
			{
				return false;
			}
			blockData.Write("bmt-backward", GetStringArrayFromKey(myKeyMaps2[i]));
			if (EqualsAny(num, new int[3] { 17, 22, 48 }))
			{
				blockData.Write("bmt-automatic", true);
				blockData.Write("bmt-toggle-mode", true);
			}
			else
			{
				if (extraBoolean == null || i >= extraBoolean.Length)
				{
					return false;
				}
				blockData.Write("bmt-automatic", extraBoolean[i]);
				blockData.Write("bmt-toggle-mode", false);
			}
			if (wheelFlipped == null || i >= wheelFlipped.Length)
			{
				return false;
			}
			blockData.Write("flipped", wheelFlipped[i] <= 0);
		}
		else if (EqualsAny(num, new int[1] { 4 }))
		{
			if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
			{
				return false;
			}
			blockData.Write("bmt-explode", GetStringArrayFromKey(myKeyMaps1[i]));
		}
		else if (EqualsAny(num, new int[3] { 7, 9, 45 }))
		{
			blockData.Write("start-position", braceStartPos[i]);
			blockData.Write("end-position", braceEndPos[i]);
			if (EqualsAny(num, new int[2] { 9, 45 }))
			{
				if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
				{
					return false;
				}
				blockData.Write("bmt-contract", GetStringArrayFromKey(myKeyMaps1[i]));
				if (extraFloat == null || i >= extraFloat.Length)
				{
					return false;
				}
				blockData.Write("bmt-speed", extraFloat[i]);
			}
			switch (num)
			{
			case 9:
				if (extraBoolean == null || i >= extraBoolean.Length)
				{
					return false;
				}
				blockData.Write("bmt-toggle", extraBoolean[i]);
				break;
			case 45:
				if (myKeyMaps2 == null || i >= myKeyMaps2.Length)
				{
					return false;
				}
				blockData.Write("bmt-unwind", GetStringArrayFromKey(myKeyMaps2[i]));
				if (extraBoolean == null || i >= extraBoolean.Length)
				{
					return false;
				}
				blockData.Write("bmt-start-unwound", extraBoolean[i]);
				break;
			}
		}
		else if (EqualsAny(num, new int[2] { 11, 53 }))
		{
			if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
			{
				return false;
			}
			blockData.Write("bmt-shoot", GetStringArrayFromKey(myKeyMaps1[i]));
		}
		else if (EqualsAny(num, new int[2] { 13, 28 }))
		{
			if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
			{
				return false;
			}
			blockData.Write("bmt-left", GetStringArrayFromKey(myKeyMaps1[i]));
			if (myKeyMaps2 == null || i >= myKeyMaps2.Length)
			{
				return false;
			}
			blockData.Write("bmt-right", GetStringArrayFromKey(myKeyMaps2[i]));
			if (extraBoolean == null || i >= extraBoolean.Length)
			{
				return false;
			}
			blockData.Write("bmt-automatic", extraBoolean[i]);
			if (extraFloat == null || i >= extraFloat.Length)
			{
				return false;
			}
			blockData.Write("bmt-rotation-speed", extraFloat[i]);
			blockData.Write("bmt-uselimits", false);
			blockData.Write("flipped", wheelFlipped[i] <= 0);
		}
		else if (EqualsAny(num, new int[1] { 14 }))
		{
			if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
			{
				return false;
			}
			blockData.Write("bmt-spin", GetStringArrayFromKey(myKeyMaps1[i]));
			if (extraBoolean == null || i >= extraBoolean.Length)
			{
				return false;
			}
			blockData.Write("bmt-automatic", extraBoolean[i]);
			if (extraFloat == null || i >= extraFloat.Length)
			{
				return false;
			}
			blockData.Write("bmt-speed", extraFloat[i]);
		}
		else if (EqualsAny(num, new int[1] { 16 }))
		{
			if (extraFloat == null || i >= extraFloat.Length)
			{
				return false;
			}
			blockData.Write("bmt-spring", extraFloat[i]);
		}
		else if (EqualsAny(num, new int[1] { 18 }))
		{
			if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
			{
				return false;
			}
			blockData.Write("bmt-extend", GetStringArrayFromKey(myKeyMaps1[i]));
			if (extraBoolean == null || i >= extraBoolean.Length)
			{
				return false;
			}
			blockData.Write("bmt-toggle", extraBoolean[i]);
			if (extraFloat == null || i >= extraFloat.Length)
			{
				return false;
			}
			blockData.Write("bmt-speed", extraFloat[i]);
		}
		else if (EqualsAny(num, new int[1] { 21 }))
		{
			if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
			{
				return false;
			}
			blockData.Write("bmt-ignite", GetStringArrayFromKey(myKeyMaps1[i]));
			if (extraBoolean == null || i >= extraBoolean.Length)
			{
				return false;
			}
			blockData.Write("bmt-hold-to-fire", extraBoolean[i]);
		}
		else if (EqualsAny(num, new int[3] { 26, 52, 55 }))
		{
			if (wheelFlipped == null || i >= wheelFlipped.Length)
			{
				return false;
			}
			blockData.Write("flipped", wheelFlipped[i] > 0);
		}
		else if (EqualsAny(num, new int[1] { 27 }))
		{
			if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
			{
				return false;
			}
			blockData.Write("bmt-detach", GetStringArrayFromKey(myKeyMaps1[i]));
			if (extraBoolean == null || i >= extraBoolean.Length)
			{
				return false;
			}
			blockData.Write("bmt-grab-static", extraBoolean[i]);
		}
		else if (EqualsAny(num, new int[1] { 35 }))
		{
			if (extraFloat == null || i >= extraFloat.Length)
			{
				return false;
			}
			blockData.Write("bmt-mass", extraFloat[i]);
		}
		else if (EqualsAny(num, new int[2] { 38, 51 }))
		{
			if (extraBoolean == null || i >= extraBoolean.Length)
			{
				return false;
			}
			blockData.Write("bmt-freeze", extraBoolean[i]);
		}
		else if (EqualsAny(num, new int[1] { 43 }))
		{
			if (extraFloat == null || i >= extraFloat.Length)
			{
				return false;
			}
			blockData.Write("bmt-buoyancy", extraFloat[i]);
		}
		else if (EqualsAny(num, new int[1] { 54 }))
		{
			if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
			{
				return false;
			}
			blockData.Write("bmt-detonate", GetStringArrayFromKey(myKeyMaps1[i]));
		}
		else if (EqualsAny(num, new int[1] { 56 }))
		{
			if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
			{
				return false;
			}
			blockData.Write("bmt-shoot", GetStringArrayFromKey(myKeyMaps1[i]));
			if (extraBoolean == null || i >= extraBoolean.Length)
			{
				return false;
			}
			blockData.Write("bmt-hold-to-fire", extraBoolean[i]);
		}
		else if (EqualsAny(num, new int[1] { 57 }))
		{
			if (myKeyMaps1 == null || i >= myKeyMaps1.Length)
			{
				return false;
			}
			blockData.Write("bmt-unpin", GetStringArrayFromKey(myKeyMaps1[i]));
			if (extraBoolean == null || i >= extraBoolean.Length)
			{
				return false;
			}
			blockData.Write("bmt-hide-visual", extraBoolean[i]);
		}
		else if (!EqualsAny(num, new int[27]
		{
			0, 1, 3, 5, 6, 10, 15, 19, 20, 23,
			24, 25, 29, 30, 31, 32, 33, 34, 36, 37,
			40, 41, 42, 44, 47, 49, 50
		}))
		{
			Debug.LogWarning("Block isn't supported for conversion: " + num);
			return false;
		}
		blockData.WasLoadedFromFile = true;
		blockData.WasCreated = true;
		return true;
	}

	private static void ReadParentRotation(string liney)
	{
		machineRotation = float.Parse(liney);
	}

	private static void ReadParentPosition(string liney)
	{
		string[] array = liney.Split(","[0]);
		machinePosition = new Vector3(float.Parse(array[0], StaticSettings.Culture), float.Parse(array[1], StaticSettings.Culture), float.Parse(array[2], StaticSettings.Culture));
	}

	private static void ReadBlockID(string liney)
	{
		string[] array = liney.Split("|"[0]);
		prefabReferences = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			prefabReferences[i] = int.Parse(array[i]);
		}
	}

	private static void ReadVector3(string liney)
	{
		string[] array = liney.Split("|"[0]);
		blockPositions = new Vector3[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(","[0]);
			blockPositions[i] = new Vector3(float.Parse(array2[0], StaticSettings.Culture), float.Parse(array2[1], StaticSettings.Culture), float.Parse(array2[2], StaticSettings.Culture));
		}
	}

	private static void ReadQuaternion(string liney)
	{
		string[] array = liney.Split("|"[0]);
		blockRotations = new Quaternion[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(","[0]);
			blockRotations[i] = new Quaternion(float.Parse(array2[0], StaticSettings.Culture), float.Parse(array2[1], StaticSettings.Culture), float.Parse(array2[2], StaticSettings.Culture), float.Parse(array2[3], StaticSettings.Culture));
		}
	}

	private static void ReadWheelFlipped(string liney)
	{
		string[] array = liney.Split("|"[0]);
		wheelFlipped = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			wheelFlipped[i] = int.Parse(array[i]);
		}
	}

	private static void ReadBraceStartPos(string liney)
	{
		string[] array = liney.Split("|"[0]);
		braceStartPos = new Vector3[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(","[0]);
			braceStartPos[i] = new Vector3(float.Parse(array2[0], StaticSettings.Culture), float.Parse(array2[1], StaticSettings.Culture), float.Parse(array2[2], StaticSettings.Culture));
		}
	}

	private static void ReadBraceEndPos(string liney)
	{
		string[] array = liney.Split("|"[0]);
		braceEndPos = new Vector3[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(","[0]);
			braceEndPos[i] = new Vector3(float.Parse(array2[0], StaticSettings.Culture), float.Parse(array2[1], StaticSettings.Culture), float.Parse(array2[2], StaticSettings.Culture));
		}
	}

	private static void ReadKey1(string liney)
	{
		string[] array = liney.Split("|"[0]);
		myKeyMaps1 = new string[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			myKeyMaps1[i] = array[i];
		}
	}

	private static void ReadKey2(string liney)
	{
		string[] array = liney.Split("|"[0]);
		myKeyMaps2 = new string[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			myKeyMaps2[i] = array[i];
		}
	}

	private static void ReadExtraFloat(string liney)
	{
		string[] array = liney.Split("|"[0]);
		extraFloat = new float[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			extraFloat[i] = float.Parse(array[i], StaticSettings.Culture);
		}
	}

	private static void ReadExtraBoolean(string liney)
	{
		string[] array = liney.Split("|"[0]);
		extraBoolean = new bool[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			extraBoolean[i] = array[i] == "True";
		}
	}

	private static bool EqualsAny(int id, int[] ids)
	{
		for (int i = 0; i < ids.Length; i++)
		{
			if (id == ids[i])
			{
				return true;
			}
		}
		return false;
	}

	private static string[] GetStringArrayFromKey(string key)
	{
		return new string[1] { GetKeyCodeFromString(key).ToString() };
	}

	private static KeyCode GetKeyCodeFromString(string key)
	{
		if (!hasKeyLookup)
		{
			keyLookup = new Dictionary<string, KeyCode>();
		}
		KeyCode value;
		if (keyLookup.TryGetValue(key, out value))
		{
			return value;
		}
		string text = key.Replace(" ", string.Empty).ToLower();
		foreach (int value2 in Enum.GetValues(typeof(KeyCode)))
		{
			if (text.Equals(((KeyCode)value2).ToString().ToLower()))
			{
				keyLookup.Add(key, (KeyCode)value2);
				return (KeyCode)value2;
			}
		}
		switch (text)
		{
		case "up":
		case "down":
		case "left":
		case "right":
			value = (KeyCode)(int)Enum.Parse(typeof(KeyCode), text + "arrow", true);
			keyLookup.Add(key, value);
			return value;
		case "0":
		case "1":
		case "2":
		case "3":
		case "4":
		case "5":
		case "6":
		case "7":
		case "8":
		case "9":
			value = (KeyCode)(int)Enum.Parse(typeof(KeyCode), "alpha" + text, true);
			keyLookup.Add(key, value);
			return value;
		case ",":
			value = KeyCode.Comma;
			keyLookup.Add(key, value);
			return value;
		case ".":
			value = KeyCode.Period;
			keyLookup.Add(key, value);
			return value;
		case "+":
			value = KeyCode.Plus;
			keyLookup.Add(key, value);
			return value;
		case "-":
			value = KeyCode.Minus;
			keyLookup.Add(key, value);
			return value;
		case "*":
			value = KeyCode.Asterisk;
			keyLookup.Add(key, value);
			return value;
		case "/":
			value = KeyCode.Slash;
			keyLookup.Add(key, value);
			return value;
		case "\\":
			value = KeyCode.Backslash;
			keyLookup.Add(key, value);
			return value;
		case ">":
			value = KeyCode.Greater;
			keyLookup.Add(key, value);
			return value;
		case "<":
			value = KeyCode.Less;
			keyLookup.Add(key, value);
			return value;
		case "^":
			value = KeyCode.Caret;
			keyLookup.Add(key, value);
			return value;
		case "=":
			value = KeyCode.Equals;
			keyLookup.Add(key, value);
			return value;
		case "!":
			value = KeyCode.Exclaim;
			keyLookup.Add(key, value);
			return value;
		case "#":
			value = KeyCode.Hash;
			keyLookup.Add(key, value);
			return value;
		case "$":
			value = KeyCode.Dollar;
			keyLookup.Add(key, value);
			return value;
		case "&":
			value = KeyCode.Ampersand;
			keyLookup.Add(key, value);
			return value;
		case "'":
			value = KeyCode.Quote;
			keyLookup.Add(key, value);
			return value;
		case "\"":
			value = KeyCode.DoubleQuote;
			keyLookup.Add(key, value);
			return value;
		case "(":
			value = KeyCode.LeftParen;
			keyLookup.Add(key, value);
			return value;
		case ")":
			value = KeyCode.RightParen;
			keyLookup.Add(key, value);
			return value;
		case "?":
			value = KeyCode.Question;
			keyLookup.Add(key, value);
			return value;
		case "@":
			value = KeyCode.At;
			keyLookup.Add(key, value);
			return value;
		case "_":
			value = KeyCode.Underscore;
			keyLookup.Add(key, value);
			return value;
		case "`":
			value = KeyCode.BackQuote;
			keyLookup.Add(key, value);
			return value;
		default:
			if (text.EndsWith("ctrl"))
			{
				return GetKeyCodeFromString(key.Replace("ctrl", "control"));
			}
			if (text == "enter")
			{
				value = KeyCode.KeypadEnter;
				keyLookup.Add(key, value);
				return value;
			}
			if (text.StartsWith("[") && text.EndsWith("]"))
			{
				string text2 = text.Substring(1, 1);
				switch (text2)
				{
				case "0":
				case "1":
				case "2":
				case "3":
				case "4":
				case "5":
				case "6":
				case "7":
				case "8":
				case "9":
					value = (KeyCode)(int)Enum.Parse(typeof(KeyCode), "keypad" + text2, true);
					keyLookup.Add(key, value);
					return value;
				case "+":
					value = KeyCode.KeypadPlus;
					keyLookup.Add(key, value);
					return value;
				case "-":
					value = KeyCode.KeypadMinus;
					keyLookup.Add(key, value);
					return value;
				case "*":
					value = KeyCode.KeypadMultiply;
					keyLookup.Add(key, value);
					return value;
				case "/":
					value = KeyCode.KeypadDivide;
					keyLookup.Add(key, value);
					return value;
				case "=":
					value = KeyCode.KeypadEquals;
					keyLookup.Add(key, value);
					return value;
				case ".":
				case ",":
					value = KeyCode.KeypadPeriod;
					keyLookup.Add(key, value);
					return value;
				}
			}
			if (text == "[")
			{
				value = KeyCode.LeftBracket;
				keyLookup.Add(key, value);
				return value;
			}
			if (text == "]")
			{
				value = KeyCode.RightBracket;
				keyLookup.Add(key, value);
				return value;
			}
			Debug.LogWarning("Key '" + key + "' (" + text + ") matches no known KeyCodes.");
			value = KeyCode.None;
			keyLookup.Add(key, value);
			return value;
		}
	}
}
