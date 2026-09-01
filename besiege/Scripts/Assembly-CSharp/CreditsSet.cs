using System;
using UnityEngine;

[Serializable]
public class CreditsSet
{
	public string Title = string.Empty;

	[Multiline]
	public string List = string.Empty;

	public float TimeToShow = 10f;

	public Transform Diorama;
}
