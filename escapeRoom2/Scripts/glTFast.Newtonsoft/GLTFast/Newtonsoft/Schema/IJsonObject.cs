namespace GLTFast.Newtonsoft.Schema
{
	public interface IJsonObject
	{
		bool TryGetValue<T>(string key, out T value);
	}
}
