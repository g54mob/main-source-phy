using System;
using System.Collections.Generic;

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
	}

	public string OperationID;

	public Dictionary<string, MissionState> MissionStates;

	public Dictionary<string, CardState> CardStates;

	public int RequisitionPoints;

	public int PowderCharges;
}
