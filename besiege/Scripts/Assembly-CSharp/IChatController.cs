public interface IChatController
{
	void ToggleChatMode();

	void HandleSayCommand(PlayerData player, ChatMode chatMode, string chatMessage);

	void HandleSayMessage(PlayerData source, string message);

	void OpenInviteFriendScreen();
}
