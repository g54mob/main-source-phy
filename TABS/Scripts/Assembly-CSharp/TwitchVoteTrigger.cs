using System;
using System.Collections.Generic;
using UnityEngine;

public class TwitchVoteTrigger : TwitchTrigger
{
	public enum TwitchVotingMode
	{
		WeightedRandom = 0,
		HighestVotesWin = 1
	}

	[Serializable]
	public struct VoteData
	{
		public TwitchAction Action;

		public string ActionInput;
	}

	public float VotingTimer = 60f;

	public TwitchVotingMode VotingMode;

	public List<VoteData> Actions = new List<VoteData>();

	private float votingTimer;

	private bool votingActive;

	public List<int> Votes = new List<int>();

	public List<string> ViewersAlreadyVoted = new List<string>();

	private void Update()
	{
		if (votingActive)
		{
			HandleVoting();
		}
	}

	public bool GetVotingActive()
	{
		return votingActive;
	}

	public float GetCurrentVotingTimer()
	{
		return votingTimer;
	}

	private void HandleVoting()
	{
		if (votingTimer >= 0f)
		{
			votingTimer -= Time.deltaTime;
		}
		if (!CheckData())
		{
			return;
		}
		Debug.Log("Voting Over!");
		int winningIndex = GetWinningIndex();
		if (winningIndex == -1)
		{
			Debug.Log("Vote Over, no votes");
			EndVote();
			return;
		}
		VoteData voteData = Actions[winningIndex];
		Debug.Log("Number of votes : " + Votes.Count + " Winner:" + winningIndex);
		voteData.Action.RunAction("Vote", voteData.ActionInput);
		TwitchIRC iRC = ServiceLocator.GetService<TwitchHandler>().IRC;
		if (iRC.IsConnectedToAuth())
		{
			iRC.SendChatMessage("Voting Ended! Winner is " + Actions[winningIndex].ActionInput);
		}
		EndVote();
	}

	private int GetWinningIndex()
	{
		int result = -1;
		switch (VotingMode)
		{
		case TwitchVotingMode.HighestVotesWin:
			result = GetHighestVoteIndex();
			break;
		case TwitchVotingMode.WeightedRandom:
			result = GetWeightedRandomVoteIndex();
			break;
		}
		return result;
	}

	private int GetWeightedRandomVoteIndex()
	{
		int result = -1;
		int num = 0;
		foreach (int vote in Votes)
		{
			num += vote;
		}
		int num2 = UnityEngine.Random.Range(0, num);
		int num3 = 0;
		for (int i = 0; i < Votes.Count; i++)
		{
			num3 += Votes[i];
			if (num2 <= Votes[i] && Votes[i] > 0)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	private int GetHighestVoteIndex()
	{
		int result = -1;
		int num = 0;
		for (int i = 0; i < Votes.Count; i++)
		{
			if (num < Votes[i])
			{
				result = i;
				num = Votes[i];
			}
		}
		return result;
	}

	public override void InputData(string name, string text)
	{
		if (!votingActive || ViewersAlreadyVoted.Contains(name))
		{
			return;
		}
		for (int i = 0; i < Actions.Count; i++)
		{
			if (text == (i + 1).ToString() || text.ToLower() == Actions[i].ActionInput.ToLower())
			{
				ViewersAlreadyVoted.Add(name);
				Debug.Log("Added vote for alternative:" + i);
				Votes[i]++;
				break;
			}
		}
	}

	public override bool CheckData()
	{
		if (votingActive && votingTimer <= 0f)
		{
			return true;
		}
		return false;
	}

	public override void HandleMessage(string name, string text)
	{
		InputData(name, text);
	}

	public void EndVote()
	{
		Votes.Clear();
		ViewersAlreadyVoted.Clear();
		votingActive = false;
	}

	public void StartVoting()
	{
		Debug.Log("Voting Started #" + Actions.Count + " number of choices");
		votingActive = true;
		votingTimer = VotingTimer;
		for (int i = 0; i < Actions.Count; i++)
		{
			Votes.Add(0);
		}
		TwitchIRC iRC = ServiceLocator.GetService<TwitchHandler>().IRC;
		if (iRC.IsConnectedToAuth())
		{
			iRC.SendChatMessage("Voting Started");
			for (int j = 0; j < Actions.Count; j++)
			{
				iRC.SendChatMessage(j + 1 + " : " + Actions[j].ActionInput);
			}
			iRC.SendChatMessage("Voting Ends in " + votingTimer + " seconds!");
		}
	}
}
