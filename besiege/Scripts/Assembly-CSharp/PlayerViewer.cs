using System.Collections.Generic;
using UnityEngine;

public class PlayerViewer : MonoBehaviour
{
	public GameObject votingTemplate;

	public GameObject playerTemplate;

	public GameObject container;

	private List<PlayerView> players = new List<PlayerView>();

	public static int voteIndex = -1;

	protected void Awake()
	{
		playerTemplate.SetActive(false);
		votingTemplate.SetActive(false);
	}

	public void Toggle(bool toggle)
	{
		container.SetActive(toggle);
	}

	private PlayerView CreatePlayer()
	{
		GameObject gameObject = Object.Instantiate(votingTemplate);
		gameObject.SetActive(true);
		Transform transform = gameObject.transform;
		transform.SetParent(playerTemplate.transform.parent, true);
		float num = 0.43f * (float)players.Count;
		transform.localPosition = playerTemplate.transform.localPosition - Vector3.up * num;
		transform.localScale = Vector3.one;
		PlayerView component = gameObject.GetComponent<PlayerView>();
		players.Add(component);
		return component;
	}

	private void DestroyPlayer(PlayerView view)
	{
		if (players.IndexOf(view) == voteIndex)
		{
			voteIndex = -1;
		}
		Object.Destroy(view.gameObject);
		players.Remove(view);
	}

	public void ClearObsoletePlayers(int count)
	{
		while (count < players.Count)
		{
			DestroyPlayer(players[players.Count - 1]);
		}
	}

	public void UpdateView(int index, PlayerData player)
	{
		PlayerView playerView = ((index < players.Count) ? players[index] : CreatePlayer());
		playerView.UpdateView(index, player);
	}
}
