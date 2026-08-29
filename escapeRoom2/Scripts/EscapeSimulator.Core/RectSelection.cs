using System.Collections.Generic;
using UnityEngine;

public struct RectSelection
{
	public RSP phase;

	public RSM mode;

	public Vector2 startPos;

	public HashSet<InstanceID> selected;
}
