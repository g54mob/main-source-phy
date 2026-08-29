using UnityEngine;

public class InteractiveAnnotation : MonoBehaviour
{
	public enum UnlockCursor
	{
		Default = 0,
		Lock = 1,
		Unlock = 2
	}

	public bool targetable = true;

	public UnlockCursor unlockCursorOverride;

	public Game.PCCrosshair overrideCursor = Game.PCCrosshair.Default;

	public virtual Game.PCCrosshair getOverrideCursor()
	{
		return overrideCursor;
	}

	public virtual bool isAnnotationCompatible(Game.ScreenTargetType screenTargetType)
	{
		return true;
	}
}
