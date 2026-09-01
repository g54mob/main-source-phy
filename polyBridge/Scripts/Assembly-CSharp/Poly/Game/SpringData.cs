using Poly.Physics;

namespace Poly.Game
{
	public struct SpringData
	{
		public EdgeHandle edge;

		public float timeSinceExpansionLastTriggered;

		public float timeSinceCompressionLastTriggered;

		public SpringData(EdgeHandle edge)
		{
			this.edge = edge;
			timeSinceExpansionLastTriggered = 1.7014117E+38f;
			timeSinceCompressionLastTriggered = 1.7014117E+38f;
		}
	}
}
