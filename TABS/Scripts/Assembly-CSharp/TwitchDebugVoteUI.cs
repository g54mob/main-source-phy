using System;
using UnityEngine;
using UnityEngine.UI;

public class TwitchDebugVoteUI : MonoBehaviour
{
	public Text Text;

	public TwitchVoteTrigger VotingTrigger;

	private void Start()
	{
	}

	private void Update()
	{
		Text.text = "Voting Mode: " + VotingTrigger.VotingMode.ToString() + Environment.NewLine;
		if (!VotingTrigger.GetVotingActive())
		{
			Text.text += "No Vote Active";
			return;
		}
		Text text = Text;
		text.text = text.text + "Voting Active!" + Environment.NewLine;
		int num = 0;
		foreach (TwitchVoteTrigger.VoteData action in VotingTrigger.Actions)
		{
			Text text2 = Text;
			text2.text = text2.text + action.ActionInput + " - " + VotingTrigger.Votes[num] + Environment.NewLine;
			num++;
		}
		Text text3 = Text;
		text3.text = text3.text + "Time Left: " + VotingTrigger.GetCurrentVotingTimer();
	}
}
