using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Dropdowns : MonoBehaviour
{
	[Dropdown("intValues")]
	public int intValue;

	[Dropdown("stringValues")]
	public string stringValue;

	[Dropdown("vectorValues")]
	public Vector3 vectorValue;

	private int[] intValues = new int[3] { 1, 2, 3 };

	private List<string> stringValues = new List<string> { "A", "B", "C" };

	private DropdownList<Vector3> vectorValues = new DropdownList<Vector3>
	{
		{
			"Right",
			Vector3.right
		},
		{
			"Up",
			Vector3.up
		},
		{
			"Forward",
			Vector3.forward
		}
	};
}
