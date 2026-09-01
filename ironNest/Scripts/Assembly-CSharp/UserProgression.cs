using System;
using System.Collections.Generic;

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
}
