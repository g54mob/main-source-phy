using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadTextureOp
{
	public UnityWebRequestAsyncOperation asyncOp;

	public Action<Texture> callback;

	public List<Action<Texture>> callbacks;

	public string killID;
}
