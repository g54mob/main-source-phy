using System.Collections.Generic;
using UnityEngine;

public class SelectableGroupHint : MonoBehaviour
{
	public enum Hint
	{
		CantNavigateTo = 0,
		FirstSelectable = 1
	}

	public List<Hint> hints = new List<Hint>();
}
