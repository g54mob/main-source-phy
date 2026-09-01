using System;
using Localisation;
using UnityEngine;

[AddComponentMenu("UI/Multiplayer/Voting Player View")]
public class VotingPlayerView : PlayerView
{
	public enum State
	{
		VoteToStart = 0,
		VoteToStop = 1,
		CancelVote = 9
	}

	public DynamicText votingText;

	public GameObject votingBar;

	public UIButton button;

	public MeshRenderer ready;

	public MeshRenderer stop;

	public MeshRenderer cancel;

	public AudioSource audioSource;

	public AudioClip voteSfx;

	public AudioClip cancelSfx;

	public GameObject StateBox;

	public State voteState;

	private NetworkAddPiece addPiece;

	public override void UpdateView(int index, PlayerData playerData)
	{
		base.UpdateView(index, playerData);
		bool flag = StatMaster.levelSimulating && !StatMaster.isLocalSim;
		bool flag2 = playerData.isLocalPlayer && !playerData.isSpectator;
		if (flag2)
		{
			PlayerViewer.voteIndex = index;
		}
		if (playerData.isSpectator)
		{
			StateBox.SetActive(true);
		}
		else
		{
			if (playerData.voteState)
			{
				voteState = (flag ? State.VoteToStop : State.CancelVote);
			}
			else
			{
				voteState = (flag ? State.CancelVote : State.VoteToStart);
			}
			SetState(voteState, false);
		}
		votingBar.SetActive(flag2);
	}

	public override bool Init()
	{
		if (base.Init())
		{
			return true;
		}
		addPiece = NetworkAddPiece.Instance;
		button.Down += ButtonClick;
		SetState(State.VoteToStart);
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
		return false;
	}

	private void OnDestroy()
	{
		if (inited)
		{
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
		}
	}

	private void OnSimulationToggle(bool toggle)
	{
		if (base.gameObject.activeInHierarchy)
		{
			UpdateView(lastIndex, player);
		}
	}

	public void ButtonClick()
	{
		if (voteState == State.CancelVote)
		{
			audioSource.PlayOneShot(cancelSfx, 0.5f);
		}
		else
		{
			audioSource.PlayOneShot(voteSfx, 0.5f);
		}
		if (player.voteState)
		{
			Unready();
		}
		else
		{
			Ready();
		}
	}

	public void SetState(State s, bool updateReady = true)
	{
		voteState = s;
		switch (s)
		{
		case State.VoteToStart:
			ready.gameObject.SetActive(true);
			stop.gameObject.SetActive(false);
			cancel.gameObject.SetActive(false);
			StateBox.SetActive(false);
			ReferenceMaster.SetDynamicText(votingText, LocalisationManager.GetTranslation(3184));
			if (updateReady)
			{
				Unready();
			}
			break;
		case State.VoteToStop:
			stop.gameObject.SetActive(true);
			ready.gameObject.SetActive(false);
			cancel.gameObject.SetActive(false);
			StateBox.SetActive(false);
			ReferenceMaster.SetDynamicText(votingText, LocalisationManager.GetTranslation(3185));
			if (updateReady)
			{
				Unready();
			}
			break;
		case State.CancelVote:
			cancel.gameObject.SetActive(true);
			ready.gameObject.SetActive(false);
			stop.gameObject.SetActive(false);
			StateBox.SetActive(true);
			ReferenceMaster.SetDynamicText(votingText, LocalisationManager.GetTranslation(3369));
			if (updateReady)
			{
				Ready();
			}
			break;
		}
	}

	public void Ready()
	{
		if (StatMaster.waitingForServerResponse || player == null || player.voteState)
		{
			return;
		}
		if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
		{
			ServerMachine machine = PlayerData.localPlayer.machine;
			if (machine.HasBannedBlocks)
			{
				GenericUIPopup instance = SingleInstanceFindOnly<GenericUIPopup>.Instance;
				if (instance != null)
				{
					instance.Show(LocalisationManager.GetTranslation(3011), 3f);
				}
				return;
			}
			if (!machine.isSimulating && SingleInstanceFindOnly<AddPiece>.Instance.OutOfBounds)
			{
				if (StatMaster.Bounding.Enabled || !StatMaster.Bounding.inGround)
				{
					OutOfBoundsWarning.current.OutOfBounds();
				}
				else
				{
					OutOfBoundsWarning.current.InFloor();
				}
				return;
			}
		}
		addPiece.RequestPlayerReadyVote(true);
	}

	public void Unready()
	{
		if (!StatMaster.waitingForServerResponse && player != null && player.voteState)
		{
			addPiece.RequestPlayerReadyVote(false);
		}
	}
}
