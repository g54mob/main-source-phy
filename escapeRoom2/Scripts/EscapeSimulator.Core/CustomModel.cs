using System.Collections.Generic;
using UnityEngine;

public class CustomModel : MonoBehaviour
{
	public bool disableGeneratedCollider;

	public bool hasModelColliders;

	public List<string> usedFiles;

	public Animation modelAnimation;

	public AnimationSampler animationSampler;

	public List<string> animations;

	public int currentAnimation;

	public CustomModelColliderType colliderType;

	public Bounds calculatedBounds;
}
