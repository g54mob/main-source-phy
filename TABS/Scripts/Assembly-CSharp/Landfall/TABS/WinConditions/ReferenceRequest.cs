using Newtonsoft.Json;

namespace Landfall.TABS.WinConditions
{
	[JsonConverter(typeof(ReferenceConverter))]
	public class ReferenceRequest<T> : RuntimeReference
	{
		public ReferenceRequest(string guid)
			: base(guid, isRequest: true)
		{
			base.ReferenceType = typeof(T);
			ServiceLocator.GetService<RuntimeReferenceService>().CreateRequest(this);
		}

		public void Release()
		{
			ServiceLocator.GetService<RuntimeReferenceService>().ReleaseRequest(this);
		}
	}
}
