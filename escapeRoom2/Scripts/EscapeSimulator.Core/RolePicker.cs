using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RolePicker : MonoBehaviour
{
	public List<GameObject> spawnPointPlace;

	public List<Text> roleNameTexts;

	public List<Text> playerNumTexts;

	[NonSerialized]
	public List<int> currentPlayerNums;

	[NonSerialized]
	public List<int> spotsToFill;
}
