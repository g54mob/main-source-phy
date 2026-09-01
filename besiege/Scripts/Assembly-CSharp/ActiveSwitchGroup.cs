using System.Collections.Generic;
using UnityEngine;

public class ActiveSwitchGroup : MonoBehaviour
{
	public ActiveSwitch[] Switches;

	public string[] tags = new string[8] { "Pyro enabled", "Bomb enabled", "Projectiles enabled", "Bounding box required", "Infinite ammo allowed", "Invincibility allowed", "Explosive ammo allowed", "Zero-G allowed" };

	public bool[] AllSwitchStates()
	{
		bool[] array = new bool[Switches.Length];
		for (int i = 0; i < Switches.Length; i++)
		{
			array[i] = Switches[i].CurrentState;
		}
		return array;
	}

	public List<string> AllSwitchTags()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < Switches.Length; i++)
		{
			if (Switches[i].CurrentState)
			{
				list.Add(tags[i]);
			}
		}
		return list;
	}
}
