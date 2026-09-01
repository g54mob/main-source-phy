using BitCode.Attributes;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public sealed class DebugPlayerPrefs
	{
		private static readonly DebugPlayerPrefs unkCMXdDaHlgFnStRuNbxzrbnMID = new DebugPlayerPrefs();

		[DebugCommand(Name = "PlayerPrefs", Description = "Push the PlayerPrefs context onto the stack.")]
		public static DebugPlayerPrefs PushPlayerPrefs()
		{
			return unkCMXdDaHlgFnStRuNbxzrbnMID;
		}

		[DebugCommand(Description = "Delete all PlayerPrefs.")]
		public void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

		[DebugCommand(Description = "Save PlayerPrefs.")]
		public void Save()
		{
			PlayerPrefs.Save();
		}

		[DebugCommand(Description = "Delete preference with the given key.")]
		public void Delete(string key)
		{
			PlayerPrefs.DeleteKey(key);
		}

		[DebugCommand(Description = "Gets whether a given key exists in PlayerPrefs.")]
		public bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		[DebugCommand(Description = "Gets an integer value associated with a given key.")]
		public int GetInt(string key)
		{
			return PlayerPrefs.GetInt(key);
		}

		[DebugCommand(Description = "Gets a floating point value associated with a given key.")]
		public float GetFloat(string key)
		{
			return PlayerPrefs.GetFloat(key);
		}

		[DebugCommand(Description = "Gets a string associated with a given key.")]
		public string GetString(string key)
		{
			return PlayerPrefs.GetString(key);
		}

		[DebugCommand(Description = "Sets an integer value associated with a given key.")]
		public void SetInt(string key, int value)
		{
			PlayerPrefs.SetInt(key, value);
		}

		[DebugCommand(Description = "Sets a floating point value associated with a given key.")]
		public void SetFloat(string key, float value)
		{
			PlayerPrefs.SetFloat(key, value);
		}

		[DebugCommand(Description = "Sets a string associated with a given key.")]
		public void SetString(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
		}
	}
}
