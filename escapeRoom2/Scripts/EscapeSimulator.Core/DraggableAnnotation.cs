public class DraggableAnnotation : InteractiveAnnotation
{
	public Draggable.ManipulationType manipulationTypeOverride = Draggable.ManipulationType.OnlyRotation;

	public override bool isAnnotationCompatible(Game.ScreenTargetType screenTargetType)
	{
		return screenTargetType == Game.ScreenTargetType.Draggable;
	}
}
