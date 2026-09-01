namespace PlayFab.SharedModels
{
	public class PlayFabBaseModel
	{
		public string ToJson()
		{
			ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty);
			return plugin.SerializeObject(this);
		}
	}
}
