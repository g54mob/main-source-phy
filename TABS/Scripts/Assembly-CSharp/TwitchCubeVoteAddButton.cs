using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TwitchCubeVoteAddButton : MonoBehaviour
{
	public TwitchCube TwitchCube;

	[HideInInspector]
	public GameObject AddedList;

	[HideInInspector]
	public GameObject NotAddedList;

	[HideInInspector]
	public TwitchVoteTrigger VoteTrigger;

	public bool isAdded;

	private TwitchVoteCubeSpawn spawn;

	private TwitchVoteTrigger.VoteData voteData;

	public void Clicked()
	{
		isAdded = !isAdded;
		UpdateAdded();
	}

	private void Start()
	{
		StartCoroutine(LateStart(0.1f));
	}

	private IEnumerator LateStart(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		UpdateAdded();
		GetComponentInChildren<Text>().text = TwitchCube.CubeName;
		spawn = new TwitchVoteCubeSpawn();
		spawn.Cube = TwitchCube;
		voteData = default(TwitchVoteTrigger.VoteData);
		voteData.Action = spawn;
		voteData.ActionInput = TwitchCube.CubeName;
	}

	private void UpdateAdded()
	{
		if (isAdded)
		{
			base.transform.SetParent(AddedList.transform);
			VoteTrigger.Actions.Add(voteData);
		}
		else
		{
			base.transform.SetParent(NotAddedList.transform);
			VoteTrigger.Actions.Remove(voteData);
		}
	}
}
