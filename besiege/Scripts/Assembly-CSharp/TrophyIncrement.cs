using System;
using UnityEngine;

internal interface TrophyIncrement
{
	Action<MonoBehaviour> trophyIncrease { get; set; }
}
