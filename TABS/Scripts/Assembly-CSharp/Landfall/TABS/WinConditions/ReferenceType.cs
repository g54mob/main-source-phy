using Newtonsoft.Json;

namespace Landfall.TABS.WinConditions
{
	[JsonConverter(typeof(ReferenceConverter))]
	public class ReferenceType<T> : RuntimeReference
	{
		public ReferenceType(string guid)
			: base(guid, isRequest: false)
		{
			base.ReferenceType = typeof(T);
		}
	}
}
