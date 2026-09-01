namespace ModIO
{
	public interface IMultiSizeImageLocator<E> : IImageLocator
	{
		string GetSizeURL(E size);

		SizeURLPair<E>[] GetAllURLs();

		E GetOriginalSize();
	}
}
