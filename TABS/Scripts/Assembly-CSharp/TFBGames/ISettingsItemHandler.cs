namespace TFBGames
{
	public interface ISettingsItemHandler
	{
		void SelectedItem(IUISelectionItem item);

		void DeselectedItem(IUISelectionItem item);
	}
}
