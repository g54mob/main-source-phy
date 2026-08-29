using System;
using System.Collections.Generic;
using UnityEngine;

public class StaggeredTextureLoadInfo
{
	public string filePath;

	public Action<Texture> callback;

	public List<Action<Texture>> callbacks;

	public string killID;
}
