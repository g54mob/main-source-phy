using System;
using UnityEngine;

public class AssetRequest
{
	public AssetBundleRequest op;

	public Action<object> callback;

	public string killID;
}
