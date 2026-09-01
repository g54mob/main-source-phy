using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XPackageDetails
	{
		public string PackageIdentifier { get; private set; }

		public XVersion Version { get; private set; }

		public XPackageKind Kind { get; private set; }

		public string DisplayName { get; private set; }

		public string Description { get; private set; }

		public string Publisher { get; private set; }

		public string StoreId { get; private set; }

		public bool Installing { get; private set; }

		public uint Index { get; private set; }

		public uint Count { get; private set; }

		internal XPackageDetails(XGamingRuntime.Interop.XPackageDetails interopStruct)
		{
			PackageIdentifier = interopStruct.packageIdentifier.GetString();
			Version = new XVersion(interopStruct.version);
			Kind = interopStruct.kind;
			DisplayName = interopStruct.displayName.GetString();
			Description = interopStruct.description.GetString();
			Publisher = interopStruct.publisher.GetString();
			StoreId = interopStruct.storeId.GetString();
			Installing = interopStruct.installing.Value;
			Index = interopStruct.index;
			Count = interopStruct.count;
		}
	}
}
