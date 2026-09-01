using UnityEngine;

public class TimePause : MonoBehaviour
{
	[Tooltip("The time scale to restore when this component is disabled. Defaults to 1 (normal speed). Adjust if your game uses a non-standard base time scale.")]
	[SerializeField]
	private float _resumeTimeScale;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}
}
