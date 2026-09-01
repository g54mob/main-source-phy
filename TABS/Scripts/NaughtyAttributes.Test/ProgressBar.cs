using NaughtyAttributes;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
	[ProgressBar("Health", 100f, ProgressBarColor.Orange)]
	public float health = 50f;
}
