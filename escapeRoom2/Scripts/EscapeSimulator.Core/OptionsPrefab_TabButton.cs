using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPrefab_TabButton : MonoBehaviour
{
	[NonSerialized]
	public bool selected;

	public Button button;

	public Text Text;

	public Image selectedImage;

	public Image locked;
}
