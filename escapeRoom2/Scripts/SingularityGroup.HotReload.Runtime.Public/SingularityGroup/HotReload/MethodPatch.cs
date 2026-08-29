using System.Reflection;

namespace SingularityGroup.HotReload
{
	public class MethodPatch
	{
		public MethodBase originalMethod;

		public MethodBase previousMethod;

		public MethodBase newMethod;

		public MethodPatch(MethodBase originalMethod, MethodBase previousMethod, MethodBase newMethod)
		{
			this.originalMethod = originalMethod;
			this.previousMethod = previousMethod;
			this.newMethod = newMethod;
		}
	}
}
