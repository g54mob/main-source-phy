using NaughtyAttributes;
using UnityEngine;

public class Validation : MonoBehaviour
{
	[MinValue(0f)]
	[MaxValue(1f)]
	public float minMaxValidated;

	[Required(null)]
	public Transform requiredTransform;

	[Required("Must not be null")]
	public GameObject requiredGameObject;

	[ValidateInput("IsNotNull", "must not be null")]
	public Sprite notNullSprite;

	private bool IsNotNull(Sprite sprite)
	{
		return sprite != null;
	}
}
