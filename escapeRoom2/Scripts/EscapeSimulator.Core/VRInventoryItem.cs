using System;
using UnityEngine;
using UnityEngine.UI;

public class VRInventoryItem : MonoBehaviour
{
	public Text itemCountText;

	public Text itemNameText;

	public SphereCollider itemCollider;

	public CanvasGroup cg;

	public Image pocket;

	public Image trashcanIcon;

	public Image keyIcon;

	public Image hintIcon;

	public Image containerIcon;

	[NonSerialized]
	public float currentScaleMultiplier = 1f;

	[NonSerialized]
	public float targetScaleMultiplier = 1f;

	[NonSerialized]
	public float targetItemNameTextAlpha;

	[NonSerialized]
	public Vector3 smoothDampVelocity;
}
