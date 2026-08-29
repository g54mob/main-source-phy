using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetBundleVersions", menuName = "Escape Simulator/AssetBundleVersions")]
public class AssetBundleVersions : ScriptableObject
{
	[Serializable]
	public class Entry
	{
		public string name;

		public int version;
	}

	public const string ASSET_PATH = "Assets/_Misc/AssetBundleVersions.asset";

	public List<Entry> versions = new List<Entry>();
}
