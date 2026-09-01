namespace XGamingRuntime.Interop
{
	internal struct XPackageFeature
	{
		internal readonly UTF8StringPtr id;

		internal readonly UTF8StringPtr displayName;

		internal readonly UTF8StringPtr tags;

		internal readonly NativeBool hidden;

		internal XPackageFeature(XGamingRuntime.XPackageFeature publicObject, DisposableCollection disposableCollection)
		{
			id = new UTF8StringPtr(publicObject.Id, disposableCollection);
			displayName = new UTF8StringPtr(publicObject.DisplayName, disposableCollection);
			tags = new UTF8StringPtr(publicObject.Tags, disposableCollection);
			hidden = new NativeBool(publicObject.Hidden);
		}
	}
}
