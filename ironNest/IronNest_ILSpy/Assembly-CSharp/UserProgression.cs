using System;
using System.Collections.Generic;
using Cpp2ILInjected;

[Serializable]
public class UserProgression
{
	[Serializable]
	public class UserCardState
	{
		public string CardID;

		public int RemainingUses;
	}

	public List<string> UnlockedCards;

	public Dictionary<string, UserCardState> CardStates;

	public List<string> UnlockedSceneObjects;

	public string LastOperationID;

	public UserProgression()
	{
		List<string> unlockedCards = new List<string>();
		UnlockedCards = unlockedCards;
		Dictionary<string, UserCardState> cardStates = new Dictionary<string, UserCardState>();
		CardStates = cardStates;
		List<string> unlockedSceneObjects = new List<string>();
		UnlockedSceneObjects = unlockedSceneObjects;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
