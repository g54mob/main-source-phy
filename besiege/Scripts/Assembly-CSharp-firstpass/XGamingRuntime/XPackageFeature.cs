using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XPackageFeature
	{
		public string Id { get; private set; }

		public string DisplayName { get; private set; }

		public string Tags { get; private set; }

		public bool Hidden { get; private set; }

		internal XPackageFeature(XGamingRuntime.Interop.XPackageFeature interopStruct)
		{
			Id = interopStruct.id.GetString();
			DisplayName = interopStruct.displayName.GetString();
			Tags = interopStruct.tags.GetString();
			Hidden = interopStruct.hidden.Value;
		}
	}
}
