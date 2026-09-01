using UnityEngine;

public class CubeVoteUI : MonoBehaviour
{
	public GameObject VoteDecide;

	public GameObject OngoingVote;

	public GameObject AddedList;

	public GameObject NotAddedList;

	public TwitchVoteTrigger VoteTrigger;

	private bool votingActive;

	private void Start()
	{
		TwitchCubeVoteAddButton[] componentsInChildren = GetComponentsInChildren<TwitchCubeVoteAddButton>();
		foreach (TwitchCubeVoteAddButton obj in componentsInChildren)
		{
			obj.AddedList = AddedList;
			obj.NotAddedList = NotAddedList;
			obj.VoteTrigger = VoteTrigger;
		}
	}

	private void Update()
	{
		if (VoteTrigger.GetVotingActive() != votingActive)
		{
			votingActive = !votingActive;
			VoteDecide.SetActive(!votingActive);
			OngoingVote.SetActive(votingActive);
		}
	}
}
