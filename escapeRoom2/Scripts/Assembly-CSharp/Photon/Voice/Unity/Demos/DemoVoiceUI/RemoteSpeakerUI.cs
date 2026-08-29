using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Voice.Unity.Demos.DemoVoiceUI
{
	[RequireComponent(typeof(Speaker))]
	public class RemoteSpeakerUI : MonoBehaviour, IInRoomCallbacks
	{
		[SerializeField]
		private Text nameText;

		[SerializeField]
		protected Image remoteIsMuting;

		[SerializeField]
		private Image remoteIsTalking;

		[SerializeField]
		private InputField minDelaySoftInputField;

		[SerializeField]
		private InputField maxDelaySoftInputField;

		[SerializeField]
		private InputField maxDelayHardInputField;

		[SerializeField]
		private Text bufferLagText;

		protected Speaker speaker;

		protected VoiceConnection voiceConnection;

		protected LoadBalancingClient loadBalancingClient;

		protected virtual void Start()
		{
			speaker = GetComponent<Speaker>();
			minDelaySoftInputField.text = speaker.PlaybackDelayMinSoft.ToString();
			minDelaySoftInputField.SetSingleOnEndEditCallback(OnMinDelaySoftChanged);
			maxDelaySoftInputField.text = speaker.PlaybackDelayMaxSoft.ToString();
			maxDelaySoftInputField.SetSingleOnEndEditCallback(OnMaxDelaySoftChanged);
			maxDelayHardInputField.text = speaker.PlaybackDelayMaxHard.ToString();
			maxDelayHardInputField.SetSingleOnEndEditCallback(OnMaxDelayHardChanged);
			SetNickname();
			SetMutedState();
		}

		private void OnMinDelaySoftChanged(string newMinDelaySoftString)
		{
			int playbackDelayMaxSoft = speaker.PlaybackDelayMaxSoft;
			int playbackDelayMaxHard = speaker.PlaybackDelayMaxHard;
			if (int.TryParse(newMinDelaySoftString, out var result) && result >= 0 && result < playbackDelayMaxSoft)
			{
				speaker.SetPlaybackDelaySettings(result, playbackDelayMaxSoft, playbackDelayMaxHard);
			}
			else
			{
				minDelaySoftInputField.text = speaker.PlaybackDelayMinSoft.ToString();
			}
		}

		private void OnMaxDelaySoftChanged(string newMaxDelaySoftString)
		{
			int playbackDelayMinSoft = speaker.PlaybackDelayMinSoft;
			int playbackDelayMaxHard = speaker.PlaybackDelayMaxHard;
			if (int.TryParse(newMaxDelaySoftString, out var result) && playbackDelayMinSoft < result)
			{
				speaker.SetPlaybackDelaySettings(playbackDelayMinSoft, result, playbackDelayMaxHard);
			}
			else
			{
				maxDelaySoftInputField.text = speaker.PlaybackDelayMaxSoft.ToString();
			}
		}

		private void OnMaxDelayHardChanged(string newMaxDelayHardString)
		{
			int playbackDelayMinSoft = speaker.PlaybackDelayMinSoft;
			int playbackDelayMaxSoft = speaker.PlaybackDelayMaxSoft;
			if (int.TryParse(newMaxDelayHardString, out var result) && result >= playbackDelayMaxSoft)
			{
				speaker.SetPlaybackDelaySettings(playbackDelayMinSoft, playbackDelayMaxSoft, result);
			}
			else
			{
				maxDelayHardInputField.text = speaker.PlaybackDelayMaxHard.ToString();
			}
		}

		private void Update()
		{
			remoteIsTalking.enabled = speaker.IsPlaying;
			bufferLagText.text = "Buffer Lag: " + speaker.Lag;
		}

		private void OnDestroy()
		{
			if (loadBalancingClient != null)
			{
				loadBalancingClient.RemoveCallbackTarget(this);
			}
		}

		private void SetNickname()
		{
			string text = speaker.name;
			if (speaker.Actor != null)
			{
				text = speaker.Actor.NickName;
				if (string.IsNullOrEmpty(text))
				{
					text = "user " + speaker.Actor.ActorNumber;
				}
			}
			nameText.text = text;
		}

		private void SetMutedState()
		{
			SetMutedState(speaker.Actor.IsMuted());
		}

		protected virtual void SetMutedState(bool isMuted)
		{
			remoteIsMuting.enabled = isMuted;
		}

		protected virtual void OnActorPropertiesChanged(Player targetPlayer, Hashtable changedProps)
		{
			if (targetPlayer.ActorNumber == speaker.Actor.ActorNumber)
			{
				SetMutedState();
				SetNickname();
			}
		}

		public virtual void Init(VoiceConnection vC)
		{
			voiceConnection = vC;
			loadBalancingClient = voiceConnection.Client;
			loadBalancingClient.AddCallbackTarget(this);
		}

		void IInRoomCallbacks.OnPlayerEnteredRoom(Player newPlayer)
		{
		}

		void IInRoomCallbacks.OnPlayerLeftRoom(Player otherPlayer)
		{
		}

		void IInRoomCallbacks.OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
		{
		}

		void IInRoomCallbacks.OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
		{
			OnActorPropertiesChanged(targetPlayer, changedProps);
		}

		void IInRoomCallbacks.OnMasterClientSwitched(Player newMasterClient)
		{
		}
	}
}
