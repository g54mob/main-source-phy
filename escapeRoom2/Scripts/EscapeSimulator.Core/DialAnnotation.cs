public class DialAnnotation : InteractiveAnnotation
{
	public int clickDirectionOverride = -1;

	public int dragDirectionOverride = -1;

	public override bool isAnnotationCompatible(Game.ScreenTargetType screenTargetType)
	{
		return screenTargetType == Game.ScreenTargetType.Dial;
	}
}
