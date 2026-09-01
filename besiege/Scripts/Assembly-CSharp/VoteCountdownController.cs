using System;
using System.Collections;
using UnityEngine;

public class VoteCountdownController : MonoBehaviour
{
	public static VoteCountdownController Instance;

	public static Action onFinish;

	[SerializeField]
	private Texture[] numberTextures;

	[SerializeField]
	private Renderer numberRenderer;

	[HideInInspector]
	public bool isRunning;

	[SerializeField]
	private GameObject container;

	private IEnumerator startCountdownCoroutine;

	public void Toggle(bool toggle)
	{
		container.SetActive(toggle);
	}

	protected void Awake()
	{
		Instance = this;
		Toggle(false);
	}

	public void StartCountdown()
	{
		if (!isRunning)
		{
			Toggle(true);
			startCountdownCoroutine = IEStartCountdown();
			NetworkAddPiece.Instance.StartCoroutine(startCountdownCoroutine);
		}
	}

	private IEnumerator IEStartCountdown()
	{
		isRunning = true;
		bool singlePlayer = StatMaster.activePlayerCount == 1;
		for (int i = (singlePlayer ? 2 : 0); i < numberTextures.Length; i++)
		{
			numberRenderer.material.mainTexture = numberTextures[i];
			yield return new WaitForSecondsRealtime((!singlePlayer) ? 1f : 0.55f);
		}
		if (onFinish != null)
		{
			onFinish();
		}
		StopCountdown();
	}

	public void StopCountdown()
	{
		if (isRunning)
		{
			if (startCountdownCoroutine != null)
			{
				NetworkAddPiece.Instance.StopCoroutine(startCountdownCoroutine);
			}
			isRunning = false;
			Toggle(false);
		}
	}
}
