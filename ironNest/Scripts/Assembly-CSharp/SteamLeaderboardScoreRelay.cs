using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Steam/Leaderboard Score Relay")]
public class SteamLeaderboardScoreRelay : MonoBehaviour
{
	[Header("Target Resolution")]
	[Tooltip("Explicit reference to the controller in the scene (recommended if available).")]
	[SerializeField]
	private SteamLeaderboardScoreController target;

	[Tooltip("Attempt to find the controller by tag if no direct reference is provided.")]
	[SerializeField]
	private bool autoFindByTag;

	[Tooltip("Tag on the GameObject that has SteamLeaderboardScoreController. Only used if Auto Find By Tag is enabled.")]
	[SerializeField]
	private string targetTag;

	[Header("Fixed Add Amount (for no-arg UnityEvents)")]
	[SerializeField]
	private int fixedAmount;

	private void OnEnable()
	{
	}

	private bool ResolveTargetIfNeeded()
	{
		return false;
	}

	public void RelayAdd()
	{
	}

	public void RelayAddValue(int amount)
	{
	}

	[Button(null)]
	public void RelaySubmit()
	{
	}

	public void RelaySetScore(int value)
	{
	}

	public void RelayResetScore()
	{
	}

	public void SetTarget(SteamLeaderboardScoreController newTarget)
	{
	}
}
