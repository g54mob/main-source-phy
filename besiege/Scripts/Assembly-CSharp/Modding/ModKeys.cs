using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InternalModding.Assemblies;
using InternalModding.Misc;
using InternalModding.Mods;
using UnityEngine;

namespace Modding
{
	public static class ModKeys
	{
		internal static Dictionary<ModContainer, Dictionary<string, ModKey>> Keys = new Dictionary<ModContainer, Dictionary<string, ModKey>>();

		public static ModKey GetKey(string name)
		{
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(Assembly.GetCallingAssembly());
			if (modByAssembly == null)
			{
				throw new InvalidOperationException("ModKeys.GetKey called from an assembly not listed in the manifest.");
			}
			if (!Keys.ContainsKey(modByAssembly))
			{
				throw new InvalidOperationException(modByAssembly.Info.Name + " did not declare any keys!");
			}
			Dictionary<string, ModKey> dictionary = Keys[modByAssembly];
			if (!dictionary.ContainsKey(name))
			{
				throw new InvalidOperationException(modByAssembly.Info.Name + " did not declare the key " + name);
			}
			return dictionary[name];
		}

		internal static void Load(ModContainer mod)
		{
			XDataHolder data = Configuration.GetData(mod);
			Dictionary<string, ModKey> dictionary = new Dictionary<string, ModKey>();
			if (data.HasKey("modkeys"))
			{
				string[] array = data.ReadStringArray("modkeys");
				string[] array2 = array;
				foreach (string text in array2)
				{
					if (!string.IsNullOrEmpty(text))
					{
						string[] array3 = text.Split('|');
						if (array3[3] == "c")
						{
							string key = array3[0];
							KeyCode modifier = (KeyCode)(int)Enum.Parse(typeof(KeyCode), array3[1]);
							KeyCode trigger = (KeyCode)(int)Enum.Parse(typeof(KeyCode), array3[2]);
							dictionary[key] = new ModKey
							{
								Modifier = modifier,
								Trigger = trigger
							};
						}
					}
				}
			}
			foreach (ModInfo.KeyInfo key2 in mod.Info.Keys)
			{
				if (!dictionary.ContainsKey(key2.Name))
				{
					dictionary[key2.Name] = new ModKey
					{
						Modifier = key2.DefaultModifier,
						Trigger = key2.DefaultTrigger
					};
				}
			}
			IEnumerable<ModKey> enumerable = dictionary.Values.Where((ModKey k) => Keys.Values.SelectMany((Dictionary<string, ModKey> d) => d.Values).Any((ModKey modKey) => k.Modifier == modKey.Modifier && k.Trigger == modKey.Trigger));
			foreach (ModKey item in enumerable)
			{
				MLog.Warn(string.Concat("Keybinding conflict on ", item.Modifier, "+", item.Trigger, ". Disabling one of the conflicting keys."));
				item.RealTrigger = item.Trigger;
				item.Trigger = KeyCode.None;
			}
			Keys[mod] = dictionary;
		}

		internal static void SaveKeysFor(List<ModContainer> mods)
		{
			foreach (ModContainer mod in mods)
			{
				SaveKeysFor(mod);
			}
		}

		internal static void SaveKeysFor(ModContainer mod)
		{
			if (Keys == null)
			{
				throw new Exception("[ModKeys.SaveKeysFor]: Missing Keys");
			}
			if (!Keys.ContainsKey(mod))
			{
				return;
			}
			XDataHolder data = Configuration.GetData(mod);
			Dictionary<string, ModKey> source = Keys[mod];
			Func<KeyValuePair<string, ModKey>, bool> isDifferentToDefault = delegate(KeyValuePair<string, ModKey> pair)
			{
				if (mod.Info == null)
				{
					Debug.LogError("[ModKeys.SaveKeysFor]: Error, missing info in " + mod);
					return false;
				}
				if (mod.Info.Keys == null || mod.Info.Keys.Count == 0)
				{
					Debug.LogError("[ModKeys.SaveKeysFor]: Error, missing keys in: " + mod.Info.Name);
					return false;
				}
				ModInfo.KeyInfo keyInfo = mod.Info.Keys.FirstOrDefault((ModInfo.KeyInfo i) => i.Name == pair.Key);
				if (keyInfo == null)
				{
					Debug.LogError("[ModKeys.SaveKeysFor]: Error, missing first key in: " + mod.Info.Name);
					return false;
				}
				KeyCode keyCode = ((pair.Value.RealTrigger != KeyCode.None) ? pair.Value.RealTrigger : pair.Value.Trigger);
				return keyInfo.DefaultTrigger != keyCode || keyInfo.DefaultModifier != pair.Value.Modifier;
			};
			string[] data2 = source.Select((KeyValuePair<string, ModKey> p) => p.Key + "|" + p.Value.Modifier.ToString() + "|" + p.Value.Trigger.ToString() + ((!isDifferentToDefault(p)) ? "|d" : "|c")).ToArray();
			data.Write("modkeys", data2);
		}
	}
}
