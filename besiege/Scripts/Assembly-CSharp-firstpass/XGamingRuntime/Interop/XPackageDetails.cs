namespace XGamingRuntime.Interop
{
	internal struct XPackageDetails
	{
		internal readonly UTF8StringPtr packageIdentifier;

		internal readonly XVersion version;

		internal readonly XPackageKind kind;

		internal readonly UTF8StringPtr displayName;

		internal readonly UTF8StringPtr description;

		internal readonly UTF8StringPtr publisher;

		internal readonly UTF8StringPtr storeId;

		internal readonly NativeBool installing;

		internal readonly uint index;

		internal readonly uint count;

		internal XPackageDetails(XGamingRuntime.XPackageDetails publicObject, DisposableCollection disposableCollection)
		{
			packageIdentifier = new UTF8StringPtr(publicObject.PackageIdentifier, disposableCollection);
			version = new XVersion(publicObject.Version);
			kind = publicObject.Kind;
			displayName = new UTF8StringPtr(publicObject.DisplayName, disposableCollection);
			description = new UTF8StringPtr(publicObject.Description, disposableCollection);
			publisher = new UTF8StringPtr(publicObject.Publisher, disposableCollection);
			storeId = new UTF8StringPtr(publicObject.StoreId, disposableCollection);
			installing = new NativeBool(publicObject.Installing);
			index = publicObject.Index;
			count = publicObject.Count;
		}
	}
}
