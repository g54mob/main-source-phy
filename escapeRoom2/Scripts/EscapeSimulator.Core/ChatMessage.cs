using UnityEngine;
using UnityEngine.UI;

public class ChatMessage
{
	public enum Type
	{
		LocalPlayer = 0,
		OtherPlayer = 1,
		PlayerNote = 2,
		Ping = 3,
		PuzzleFinished = 4,
		PuzzleFinishedOtherPlayer = 5,
		PuzzleFinishedTeamwork = 6,
		PuzzleUnlocked = 7,
		ManyPuzzlesUnlocked = 8,
		OtherPlayerFinishedLevel = 9,
		PingOnObject = 10,
		LevelNote = 11,
		CodeDebug = 12,
		CodeDebugError = 13,
		RespawnedItem = 14,
		PlayerJoined = 15,
		PlayerLeft = 16,
		PlayerFoundToken = 17
	}

	public GameObject gameObject;

	public CanvasGroup canvasGroup;

	public Text text;

	public float timeLeft;

	public float alpha = 1f;
}
