using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Steam/Leaderboard Score Controller")]
public class SteamLeaderboardScoreController : MonoBehaviour
{
	[Header("Leaderboard")]
	[Tooltip("Steam leaderboard API name (exact name as defined in Steamworks).")]
	[SerializeField]
	private string leaderboardApiName;

	[Header("Score Settings")]
	[SerializeField]
	private int startingScore;

	[SerializeField]
	private bool resetScoreAfterSubmit;

	[Header("Upload Options")]
	[Tooltip("If enabled, submissions will use ForceUpdate instead of KeepBest (useful to seed or lower scores).")]
	[SerializeField]
	private bool useForceUpdate;

	[Header("Logging")]
	[Tooltip("If enabled, the controller will log detailed information whenever it attempts to submit a score.")]
	[SerializeField]
	private bool verboseLogging;

	[Header("Live State")]
	[Tooltip("Cumulative score that will be submitted. This updates at runtime and can be edited in the Inspector if needed.")]
	[SerializeField]
	private int pendingScore;

	public static SteamLeaderboardScoreController Instance { get; private set; }

	public int CurrentScore => 0;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void AddToScore(int amount)
	{
	}

	public void SetScore(int value)
	{
	}

	public void ResetScore()
	{
	}

	public void SubmitScore()
	{
	}

	public void SubmitScoreForceUpdate()
	{
	}

	private void SubmitScoreInternal(bool forceUpdate)
	{
	}

	public void SubmitScoreWithValue(int value)
	{
	}
}
