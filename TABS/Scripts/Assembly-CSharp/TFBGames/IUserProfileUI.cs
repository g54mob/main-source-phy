namespace TFBGames
{
	public interface IUserProfileUI : IService
	{
		bool IsVisible { get; }

		bool CanChangeProfile { get; }

		void Show(bool canChangeProfile);

		void Hide();

		void SetCanChangeProfile(bool canChangeProfile);
	}
}
