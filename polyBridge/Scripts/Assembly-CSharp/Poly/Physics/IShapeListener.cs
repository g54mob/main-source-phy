namespace Poly.Physics
{
	public interface IShapeListener
	{
		void OnShapeAdded(ShapeHandleIndex s);

		void OnShapeModified(ShapeHandleIndex s);

		void OnShapeRemoved(ShapeHandleIndex s);
	}
}
