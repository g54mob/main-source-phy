using System;
using System.Text;
using Localisation;
using Steamworks;
using UnityEngine;

public class ChatController : CanvasInputController, IChatController
{
	private const int MaxSayText = 200;

	private const string ClearCommand = "clear";

	private const string TestCommand = "test";

	private const string GlobalChatFormat = "<color=#{0}>{1}:  </color>{2}";

	private const string TeamChatFormat = "<color=#{0}>{1}:  </color><color=#{2}>{3}</color>";

	private const string ChatModeFormat = "<color=#{0}>{1}</color>";

	private const float TeamChatColorMultiplier = 0.7f;

	private bool hasNetworkAuxAddPiece = true;

	private bool inTestMode;

	private int numUnreadMessages;

	private NetworkAuxAddPiece networkAuxAddPiece;

	private PlayerData localPlayer;

	private MPTeam currentPlayerTeam;

	private IChatView chatView;

	private ChatMode chatMode;

	private bool hasPlayer;

	public override void Initialize(ICanvasInputView view)
	{
		base.Initialize(view);
		chatView = (IChatView)view;
		ReferenceMaster.ChatController = this;
		networkAuxAddPiece = NetworkAuxAddPiece.Instance;
		if (networkAuxAddPiece == null)
		{
			hasNetworkAuxAddPiece = false;
		}
		inTestMode = ((ChatView)view).TestMode;
		if (inTestMode)
		{
			SetupTestMode();
		}
		UpdateChatModeText();
		UpdateUnreadText(false);
	}

	private void UpdateUnreadText(bool show)
	{
		string unreadText = string.Empty;
		if (numUnreadMessages != 0 && numUnreadMessages <= 9)
		{
			unreadText = numUnreadMessages.ToString();
		}
		else if (numUnreadMessages > 9)
		{
			unreadText = string.Format("{0}+", 9);
		}
		chatView.ChangeUnreadText(unreadText, show);
	}

	private void SetupTestMode()
	{
		localPlayer = CreateRandomPlayer();
		currentPlayerTeam = localPlayer.team;
		SayWelcomeMessage();
		hasPlayer = true;
	}

	private void SetChatPlayer()
	{
		if (PlayerData.hasLocalPlayer)
		{
			localPlayer = PlayerData.localPlayer;
			currentPlayerTeam = localPlayer.team;
			SayWelcomeMessage();
		}
	}

	private void SayWelcomeMessage()
	{
		string arg = "Besiege Player";
		if (PlayerData.hasLocalPlayer)
		{
			arg = localPlayer.name;
		}
		string textEntry = "<color=#" + ColorUtility.ToHtmlStringRGBA(new Color(1f, 1f, 1f, 0.5f)) + ">" + string.Format(LocalisationManager.GetTranslation(3346), arg) + "</color>";
		view.AddTextEntry(textEntry);
	}

	private Color GetTeamColor()
	{
		ChatView chatView = (ChatView)view;
		return chatView.ChatTeamColors[(int)localPlayer.team];
	}

	private void UpdateChatModeText()
	{
		string chatModeText = string.Empty;
		if (!PlayerData.hasLocalPlayer || (localPlayer != null && localPlayer.team == MPTeam.None))
		{
			chatMode = ChatMode.Global;
		}
		if (chatMode == ChatMode.Global)
		{
			chatModeText = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGBA(new Color(0.91f, 0.97f, 1f, 0.45f)), LocalisationManager.GetTranslation(3285));
		}
		else if (chatMode == ChatMode.Team)
		{
			Color teamColor = GetTeamColor();
			chatModeText = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGBA(teamColor), LocalisationManager.GetTranslation(1759));
		}
		chatView.ChangeChatModeText(chatModeText, true);
	}

	public override void HandleInput(string inputText)
	{
		if (inputText.Equals("clear"))
		{
			view.Clear();
		}
		else if (inTestMode && inputText.Equals("test"))
		{
			int num = (int)(localPlayer.team + 1);
			if (num > 4)
			{
				num = 0;
			}
			localPlayer.team = (MPTeam)num;
		}
		else if (!hasNetworkAuxAddPiece && !inTestMode)
		{
			view.AddTextEntry("You're not in the Multiverse...");
		}
		else
		{
			PerformChat(inputText);
		}
	}

	private void PerformChat(string inputText)
	{
		if (inputText.Length > 200)
		{
			string translation = LocalisationManager.GetTranslation(3368);
			view.AddTextEntry(translation);
		}
		else
		{
			PerformPlayerChat(localPlayer, inputText);
		}
	}

	private void PerformPlayerChat(PlayerData player, string chatText)
	{
		WorkshopManager.VerifyString(chatText, delegate(WorkshopManager.VerifyStringResult res, string str)
		{
			if (view != null)
			{
				Color teamColor = GetTeamColor();
				string text = ((chatMode != ChatMode.Global) ? string.Format("<color=#{0}>{1}:  </color><color=#{2}>{3}</color>", ColorUtility.ToHtmlStringRGBA(teamColor), player.name, ColorUtility.ToHtmlStringRGBA(teamColor), str) : string.Format("<color=#{0}>{1}:  </color>{2}", ColorUtility.ToHtmlStringRGBA(teamColor), player.name, str));
				if (hasNetworkAuxAddPiece)
				{
					networkAuxAddPiece.SendSay(chatMode, text);
				}
				else
				{
					view.AddTextEntry(text);
				}
			}
		});
	}

	private PlayerData CreateRandomPlayer()
	{
		PlayerData playerData = new PlayerData(0);
		playerData.team = (MPTeam)UnityEngine.Random.Range(0, ReferenceMaster.Instance.teamColors.Length);
		playerData.name = ScoreboardTester.GetRandomName();
		playerData.isLocalPlayer = true;
		PlayerData.localPlayer = playerData;
		PlayerData.hasLocalPlayer = true;
		return playerData;
	}

	public override void OnUpdate()
	{
		if (PlayerData.hasLocalPlayer)
		{
			if (localPlayer == null)
			{
				localPlayer = PlayerData.localPlayer;
			}
			if (currentPlayerTeam != localPlayer.team)
			{
				currentPlayerTeam = localPlayer.team;
				UpdateChatModeText();
			}
		}
	}

	public void ToggleChatMode()
	{
		if (chatMode == ChatMode.Global)
		{
			if (localPlayer.team == MPTeam.None)
			{
				string translation = LocalisationManager.GetTranslation(3367);
				view.AddTextEntry("<color=#" + ColorUtility.ToHtmlStringRGBA(new Color(1f, 1f, 1f, 0.5f)) + ">" + translation + ".</color>");
			}
			else
			{
				chatMode = ChatMode.Team;
			}
		}
		else
		{
			chatMode = ChatMode.Global;
		}
		UpdateChatModeText();
	}

	private string FilterChatMessage(string chatMessage)
	{
		return chatMessage;
	}

	public void HandleSayCommand(PlayerData source, ChatMode chatMode, string chatMessage)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(chatMessage);
		byte[] array = new byte[2 + bytes.Length];
		NetworkCompression.WriteUInt16(source.networkId, array, 0);
		Buffer.BlockCopy(bytes, 0, array, 2, bytes.Length);
		if (chatMode == ChatMode.Global)
		{
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.Say, array);
			HandleSayMessage(source, chatMessage);
			return;
		}
		foreach (PlayerData player in Playerlist.Players)
		{
			if (player.team == source.team)
			{
				networkAuxAddPiece.SendPlayerMessage(player.networkId, RPCMessageType.Say, array);
			}
		}
		if (StatMaster.isHosting && localPlayer.team == source.team)
		{
			HandleSayMessage(source, chatMessage);
		}
	}

	public void HandleSayMessage(PlayerData source, string message)
	{
		if (SingleInstance<WorkshopManager>.hasInstance())
		{
			SingleInstance<WorkshopManager>.Instance.AllowCommunicationWithUser(source, delegate(bool allow)
			{
				if (allow)
				{
					Debug.Log("[ChatController] HandleSayMessage source=" + source.name + " " + message);
					WorkshopManager.VerifyString(message, delegate(WorkshopManager.VerifyStringResult res, string str)
					{
						if (view != null)
						{
							str = FilterChatMessage(str);
							if (!view.IsVisible)
							{
								numUnreadMessages++;
								UpdateUnreadText(true);
							}
							view.AddTextEntry(str);
						}
					});
				}
				else
				{
					Debug.Log("[ChatController] HandleSayMessage Ignoring message from blocked user " + source.name);
				}
			});
		}
		else
		{
			Debug.Log("[ChatController] HandleSayMessage source=" + source.name + " " + message);
			message = FilterChatMessage(message);
			if (!view.IsVisible)
			{
				numUnreadMessages++;
				UpdateUnreadText(true);
			}
			view.AddTextEntry(message);
		}
	}

	public override void OnVisibilityChanged(bool visible)
	{
		if (visible)
		{
			numUnreadMessages = 0;
			UpdateUnreadText(false);
			if (!hasPlayer)
			{
				SetChatPlayer();
				hasPlayer = true;
			}
		}
	}

	public void OpenInviteFriendScreen()
	{
		ulong lobbyID = BesiegeNetworkManager.Instance.LobbyID;
		SteamFriends.ActivateGameOverlayInviteDialog((CSteamID)lobbyID);
	}
}
