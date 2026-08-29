public class TurnableAnnotation : InteractiveAnnotation
{
	public int clickDirectionOverride = -1;

	public override bool isAnnotationCompatible(Game.ScreenTargetType screenTargetType)
	{
		return screenTargetType == Game.ScreenTargetType.Turnable;
	}
}
