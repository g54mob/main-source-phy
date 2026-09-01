public interface IFileBrowserViewExtension
{
	void Initialize(FileBrowserView view, FileBrowserController controller);

	void OnPageViewCreated(FileBrowserPageView pageView);
}
