using UnityEngine;

public class PineTweenSystemEnableNoHandles : PineTweenSystem
{
	public PineTween tween(GameObject gameObject, float duration = 1f, float delay = 0f, bool deactivate = false, bool activate = false, bool destroy = false, Vector3? anchoredPositionTo = null, Vector3? anchoredPositionFrom = null, Color? colorTo = null, Color? colorFrom = null, Vector3? positionFrom = null, Vector3? positionTo = null, Vector3? localPositionFrom = null, Vector3? localPositionTo = null, Quaternion? rotationFrom = null, Quaternion? rotationTo = null, Quaternion? localRotationFrom = null, Quaternion? localRotationTo = null, Vector3? scaleFrom = null, Vector3? scaleTo = null, float? alphaTo = null, float? alphaFrom = null, float? heightTo = null, float? heightFrom = null, CustomProcessor custom = null, OnComplete onComplete = null, Interpolation interpolation = Interpolation.Linear, int handle = -1, int dataInt = 0)
	{
		return tweenInternal(gameObject, duration, delay, deactivate, activate, destroy, anchoredPositionTo, anchoredPositionFrom, colorTo, colorFrom, positionFrom, positionTo, localPositionFrom, localPositionTo, rotationFrom, rotationTo, localRotationFrom, localRotationTo, scaleFrom, scaleTo, alphaTo, alphaFrom, heightTo, heightFrom, custom, onComplete, interpolation, handle, dataInt);
	}

	public PineTween animate(CustomAnimation animation, float delay = 0f)
	{
		return animateInternal(-1, null, animation, delay);
	}

	public PineTween animate(GameObject gameObject, CustomAnimation animation, float delay = 0f)
	{
		return animateInternal(-1, gameObject, animation, delay);
	}
}
