using System;
using System.Collections.Generic;
using Cpp2ILInjected;

[Serializable]
public class OperationState
{
	public class CardState
	{
		public string CardID;

		public int RemainingUses;
	}

	public class MissionState
	{
		public string MissionID;

		public bool Completed;

		public Dictionary<string, int> Medals;

		public MissionState()
		{
			Dictionary<string, int> medals = new Dictionary<string, int>(StringComparer.s_ordinalIgnoreCase);
			Medals = medals;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	public string OperationID;

	public Dictionary<string, MissionState> MissionStates;

	public Dictionary<string, CardState> CardStates;

	public int RequisitionPoints;

	public int PowderCharges;

	public OperationState()
	{
		Dictionary<string, MissionState> missionStates = new Dictionary<string, MissionState>(StringComparer.s_ordinalIgnoreCase);
		MissionStates = missionStates;
		Dictionary<string, CardState> cardStates = new Dictionary<string, CardState>(StringComparer.s_ordinalIgnoreCase);
		CardStates = cardStates;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
