using Poly.Math;

namespace Poly.Physics
{
	public interface IEntity
	{
		Transform2 t2 { get; }

		short worldIndex { get; }

		void CacheTransform2();
	}
}
