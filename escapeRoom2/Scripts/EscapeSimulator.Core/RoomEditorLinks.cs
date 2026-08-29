using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomEditorLinks
{
	public List<LockArgument> locks = new List<LockArgument>();

	public List<GameObject> targets = new List<GameObject>();

	public int output = 1;
}
