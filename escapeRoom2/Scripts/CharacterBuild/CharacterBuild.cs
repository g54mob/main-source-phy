using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterBuild : MonoBehaviour
{
	public enum Outfit
	{
		None = 0,
		Dracula = 1,
		DraculaPremium = 2,
		Space = 3,
		SpacePremium = 4,
		Pirate = 5,
		PiratePremium = 6,
		Reserved1 = 7,
		Reserved1Premium = 8,
		Reserved2 = 9,
		Reserved2Premium = 10
	}

	public enum Hair
	{
		None = 0,
		Hair1 = 1,
		Hair2 = 2,
		Hair3 = 3,
		Hair4 = 4,
		Hair5 = 5
	}

	public enum Gender
	{
		Male = 0,
		Female = 1
	}

	public enum SkinColor
	{
		Light = 0,
		White = 1,
		Tan = 2,
		Brown = 3,
		Dark = 4
	}

	public enum EyeColor
	{
		Amber = 0,
		Blue = 1,
		Brown = 2,
		Green = 3,
		Hazel = 4
	}

	public enum CharacterLayer
	{
		Remote = 0,
		Local = 1
	}

	[Flags]
	public enum HideBodyPart : short
	{
		Head = 1,
		HandLeft = 2,
		HandRight = 4,
		FootLeft = 8,
		FootRight = 0x10,
		Torso = 0x20,
		Legs = 0x40,
		TorsoUpper = 0x80,
		ArmLowerLeft = 0x100,
		ArmLowerRight = 0x200,
		ArmUpperLeft = 0x400,
		ArmUpperRight = 0x800
	}

	[Serializable]
	public struct MaterialTarget
	{
		public Renderer renderer;

		public int materialSlot;
	}

	private struct EyeBlendShapes
	{
		public int eyeBlinkLeft;

		public int eyeBlinkRight;
	}

	public bool debugDisableFadeout;

	public const int maxOutfitIndexExclusive = 7;

	public const int maxHairIndexExclusive = 6;

	private static readonly int ShaderID_BaseColor = Shader.PropertyToID("_BaseColor");

	private static readonly int ShaderID_BaseColorMap = Shader.PropertyToID("_BaseColorMap");

	private static readonly int ShaderID_CharacterPosition = Shader.PropertyToID("_CharacterPosition");

	private static readonly int ShaderID_HardLightRatio = Shader.PropertyToID("_HardLightRatio");

	private short[] bodyHideFlagsMale = new short[23]
	{
		0, 4088, 4088, 4094, 4094, 3198, 3198, 4088, 4088, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0
	};

	private short[] bodyHideFlagsFemale = new short[23]
	{
		0, 4094, 4094, 4094, 4094, 3192, 3192, 4088, 4088, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0
	};

	public SkinnedMeshRenderer[] headRenderers;

	private List<EyeBlendShapes> blendShapeIds = new List<EyeBlendShapes>();

	public Transform target;

	public AnimationCurve gptCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, -10f), new Keyframe(0.2f, 1f, -10f, 0f), new Keyframe(0.4f, 1f, 0f, 0f), new Keyframe(1f, 0f, 0f, 2f));

	private Animator animator;

	private float blinkMin = 3.52f;

	private float blinkMax = 4.28f;

	private float blinkDuration = 0.25f;

	private float timeToBlink = -1f;

	private float blinkAnimation = -1f;

	public float lookOffset;

	public bool isTrailerCharacter = true;

	public Gender gender;

	public int skinType = 1;

	public EyeColor eyeColor = EyeColor.Brown;

	public Outfit outfit;

	public Hair hair;

	public int hairColor;

	[HideInInspector]
	public CharacterLayer characterLayer;

	public GameObject outfitGameObject;

	public GameObject hairGameObject;

	private CharacterLayer lastCharacterLayer;

	private Outfit lastOutfit;

	private Hair lastHair;

	private int lastHairColor;

	private int lastSkinType = 1;

	private EyeColor lastEyeColor = EyeColor.Brown;

	public CCOptionsScriptable characterPresets;

	public MaterialTarget[] lowerBodyMaterialTargets;

	public MaterialTarget[] upperBodyMaterialTargets;

	public MaterialTarget[] headMaterialTargets;

	public MaterialTarget eyeMaterialTarget;

	public Texture2D[] skinColorsLowerBody;

	public Texture2D[] skinColorsUpperBody;

	public Texture2D[] skinColorsHead;

	public GameObject[] outfits;

	public GameObject[] hairs;

	public Texture2D[] eyeColors;

	public Avatar avatar;

	public Transform skeletonRoot;

	[HideInInspector]
	private HumanPoseHandler humanPoseHandler;

	[Header("Bones")]
	public GameObject neck;

	public GameObject hip;

	public GameObject[] fingers;

	public GameObject[] pickupHandFingers;

	[Header("VR")]
	public GameObject vrHead;

	public GameObject vrHandLeft;

	public GameObject vrHandRight;

	public GameObject[] vrIgnoreHeadSkinnedMesh;

	public GameObject vrTorso;

	public GameObject vrTorsoPivot;

	public GameObject vrTorsoTarget;

	public GameObject vrHip;

	public GameObject[] vrRigHandModels;

	public GameObject[] vrRigSleeveModels;

	private bool forceRebuild = true;

	private List<Renderer> characterPositionRenderers = new List<Renderer>(16);

	private void Awake()
	{
		if (avatar != null && skeletonRoot != null)
		{
			humanPoseHandler = new HumanPoseHandler(avatar, skeletonRoot);
		}
		animator = GetComponent<Animator>();
		SkinnedMeshRenderer[] array = headRenderers;
		for (int i = 0; i < array.Length; i++)
		{
			Mesh sharedMesh = array[i].sharedMesh;
			blendShapeIds.Add(new EyeBlendShapes
			{
				eyeBlinkLeft = sharedMesh.GetBlendShapeIndex("eyeBlinkLeft"),
				eyeBlinkRight = sharedMesh.GetBlendShapeIndex("eyeBlinkRight")
			});
		}
		timeToBlink = UnityEngine.Random.Range(blinkMin, blinkMax);
		syncCharacter();
	}

	private void OnAnimatorIK(int layerIndex)
	{
		Vector3 lookAtPosition = target.position + Vector3.up * lookOffset;
		if (animator != null)
		{
			Transform boneTransform = animator.GetBoneTransform(HumanBodyBones.Head);
			if (boneTransform != null)
			{
				lookAtPosition = boneTransform.TransformPoint(Vector3.forward) + Vector3.up * lookOffset;
			}
		}
		animator.SetLookAtPosition(lookAtPosition);
		animator.SetLookAtWeight(1f, 0.1f, 0.7f, 0.2f);
	}

	public HumanPoseHandler getHumanPoseHandler()
	{
		if (humanPoseHandler == null && avatar != null && skeletonRoot != null)
		{
			humanPoseHandler = new HumanPoseHandler(avatar, skeletonRoot);
		}
		return humanPoseHandler;
	}

	private void updateEyes()
	{
		if (blinkAnimation <= -1f)
		{
			timeToBlink -= Time.deltaTime;
		}
		if (timeToBlink <= 0f)
		{
			blinkAnimation = 0f;
			timeToBlink = UnityEngine.Random.Range(blinkMin, blinkMax);
		}
		if (blinkAnimation >= 0f)
		{
			blinkAnimation += Time.deltaTime / blinkDuration;
		}
		if (blinkAnimation >= 1f)
		{
			blinkAnimation = -1f;
		}
		float value = gptCurve.Evaluate(blinkAnimation) * 100f;
		for (int i = 0; i < headRenderers.Length; i++)
		{
			SkinnedMeshRenderer obj = headRenderers[i];
			EyeBlendShapes eyeBlendShapes = blendShapeIds[i];
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeBlinkLeft, value);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeBlinkRight, value);
		}
	}

	public void forceOpenEyes()
	{
		for (int i = 0; i < headRenderers.Length; i++)
		{
			SkinnedMeshRenderer obj = headRenderers[i];
			EyeBlendShapes eyeBlendShapes = blendShapeIds[i];
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeBlinkLeft, 0f);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeBlinkRight, 0f);
		}
	}

	private void LateUpdate()
	{
		updateEyes();
		syncCharacter();
	}

	public void syncCharacter()
	{
		bool flag = false;
		Dictionary<string, Transform> boneMap;
		short bodyHideFlags;
		if (lastOutfit != outfit || forceRebuild)
		{
			flag = true;
			lastOutfit = outfit;
			if (outfitGameObject != null)
			{
				UnityEngine.Object.Destroy(outfitGameObject);
				outfitGameObject = null;
			}
			if (outfit != Outfit.None)
			{
				int num = (int)(outfit - 1);
				outfitGameObject = UnityEngine.Object.Instantiate(outfits[num]);
			}
			Transform[] componentsInChildren;
			if (outfitGameObject != null)
			{
				outfitGameObject.transform.SetParent(base.transform);
				outfitGameObject.transform.localPosition = Vector3.zero;
				outfitGameObject.transform.localRotation = Quaternion.identity;
				outfitGameObject.transform.localScale = Vector3.one;
				boneMap = new Dictionary<string, Transform>();
				componentsInChildren = base.transform.Find("Skeleton").GetComponentsInChildren<Transform>(includeInactive: true);
				foreach (Transform transform in componentsInChildren)
				{
					boneMap[transform.name] = transform;
				}
				SkinnedMeshRenderer[] componentsInChildren2 = outfitGameObject.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive: true);
				foreach (SkinnedMeshRenderer skinnedMeshRenderer in componentsInChildren2)
				{
					if (skinnedMeshRenderer.rootBone != null)
					{
						skinnedMeshRenderer.rootBone = lookupBone(skinnedMeshRenderer.rootBone.name);
					}
					Transform[] bones = skinnedMeshRenderer.bones;
					for (int j = 0; j < bones.Length; j++)
					{
						if (bones[j] != null)
						{
							bones[j] = lookupBone(bones[j].name);
						}
					}
					skinnedMeshRenderer.bones = bones;
				}
			}
			short[] array = ((gender == Gender.Male) ? bodyHideFlagsMale : bodyHideFlagsFemale);
			bodyHideFlags = array[(int)outfit];
			componentsInChildren = base.transform.GetComponentsInChildren<Transform>(includeInactive: true);
			foreach (Transform t in componentsInChildren)
			{
				hideIf(t, HideBodyPart.Head, "head");
				hideIf(t, HideBodyPart.HandLeft, "lefthand");
				hideIf(t, HideBodyPart.HandRight, "righthand");
				hideIf(t, HideBodyPart.FootLeft, "leftfeet");
				hideIf(t, HideBodyPart.FootRight, "rightfeet");
				hideIf(t, HideBodyPart.Torso, "torsolower");
				hideIf(t, HideBodyPart.Legs, "lowerbody");
				hideIf(t, HideBodyPart.Legs, "leg");
				hideIf(t, HideBodyPart.TorsoUpper, "torsoupper");
				hideIf(t, HideBodyPart.ArmLowerLeft, "leftlowerarm");
				hideIf(t, HideBodyPart.ArmLowerRight, "rightlowerarm");
				hideIf(t, HideBodyPart.ArmUpperLeft, "leftupperarm");
				hideIf(t, HideBodyPart.ArmUpperRight, "rightupperarm");
			}
		}
		if (lastHair != hair || forceRebuild)
		{
			flag = true;
			lastHair = hair;
			if (hairGameObject != null)
			{
				UnityEngine.Object.Destroy(hairGameObject);
				hairGameObject = null;
			}
			if (hair != Hair.None)
			{
				int num2 = (int)(hair - 1);
				if (num2 < hairs.Length)
				{
					hairGameObject = UnityEngine.Object.Instantiate(hairs[num2]);
				}
				if (hairGameObject != null)
				{
					hairGameObject.transform.SetParent(base.transform);
					hairGameObject.transform.localPosition = Vector3.zero;
					hairGameObject.transform.localRotation = Quaternion.identity;
					hairGameObject.transform.localScale = Vector3.one;
					Transform transform2 = base.transform.Find("HeadAttachment");
					Vector3 localPosition = transform2.transform.localPosition;
					Quaternion localRotation = transform2.transform.localRotation;
					Vector3 localScale = transform2.transform.localScale;
					hairGameObject.transform.SetParent(transform2);
					Transform parent = base.transform.Find("Skeleton/Hips/Spine/Chest/UpperChest/Neck/Head");
					transform2.SetParent(parent);
					transform2.localPosition = Vector3.zero;
					transform2.localRotation = Quaternion.identity;
					transform2.localScale = Vector3.one;
					hairGameObject.transform.SetParent(parent);
					transform2.SetParent(base.transform);
					transform2.localPosition = localPosition;
					transform2.localRotation = localRotation;
					transform2.localScale = localScale;
					lastHairColor = -1;
				}
			}
		}
		if (hairColor != lastHairColor || forceRebuild)
		{
			flag = true;
			lastHairColor = hairColor;
			Renderer[] componentsInChildren3 = GetComponentsInChildren<Renderer>(includeInactive: true);
			foreach (Renderer renderer in componentsInChildren3)
			{
				Material[] materials = renderer.materials;
				Material[] array2 = new Material[materials.Length];
				for (int k = 0; k < materials.Length; k++)
				{
					if (materials[k] != null && materials[k].shader.name == "HDRP/ES2Hair")
					{
						array2[k] = new Material(materials[k]);
						if (characterPresets != null && hairColor < characterPresets.hairColors.Length)
						{
							array2[k].SetColor(ShaderID_BaseColor, characterPresets.hairColors[hairColor]);
						}
					}
					else
					{
						array2[k] = materials[k];
					}
				}
				renderer.materials = array2;
			}
		}
		if (eyeColor != lastEyeColor || forceRebuild)
		{
			flag = true;
			lastEyeColor = eyeColor;
			eyeMaterialTarget.renderer.materials[eyeMaterialTarget.materialSlot].SetTexture(ShaderID_BaseColorMap, eyeColors[(int)eyeColor]);
		}
		if (this.skinType != lastSkinType || forceRebuild)
		{
			flag = true;
			lastSkinType = this.skinType;
			if (characterPresets != null && this.skinType < characterPresets.skinTypes.Length)
			{
				CCOptionsScriptable.SkinType skinType = characterPresets.skinTypes[this.skinType];
				replaceSection(lowerBodyMaterialTargets, skinColorsLowerBody[(int)skinType.texture], skinType.tint, skinType.hardLight);
				replaceSection(upperBodyMaterialTargets, skinColorsUpperBody[(int)skinType.texture], skinType.tint, skinType.hardLight);
				replaceSection(headMaterialTargets, skinColorsHead[(int)skinType.texture], skinType.tint, skinType.hardLight);
			}
		}
		if (characterLayer != lastCharacterLayer || forceRebuild)
		{
			lastCharacterLayer = characterLayer;
			flag = true;
		}
		forceRebuild = false;
		if (flag)
		{
			int num3 = LayerMask.NameToLayer("CharacterLocal");
			int num4 = LayerMask.NameToLayer("Characters");
			characterPositionRenderers.Clear();
			Renderer[] componentsInChildren3 = GetComponentsInChildren<Renderer>(includeInactive: true);
			foreach (Renderer renderer2 in componentsInChildren3)
			{
				renderer2.renderingLayerMask = 64u;
				renderer2.gameObject.layer = ((characterLayer == CharacterLayer.Local) ? num3 : num4);
				Material[] sharedMaterials = renderer2.sharedMaterials;
				for (int l = 0; l < sharedMaterials.Length; l++)
				{
					if (sharedMaterials[l] != null && sharedMaterials[l].HasVector(ShaderID_CharacterPosition))
					{
						characterPositionRenderers.Add(renderer2);
						break;
					}
				}
			}
		}
		Vector4 vector = new Vector4(base.transform.position.x, base.transform.position.y, base.transform.position.z, 1f);
		foreach (Renderer characterPositionRenderer in characterPositionRenderers)
		{
			if (!(characterPositionRenderer != null))
			{
				continue;
			}
			Material[] materials2 = characterPositionRenderer.materials;
			for (int m = 0; m < materials2.Length; m++)
			{
				if (materials2[m] != null && materials2[m].HasVector(ShaderID_CharacterPosition))
				{
					Vector4 vector2 = (debugDisableFadeout ? (Vector4.one * 10000f) : Vector4.zero);
					materials2[m].SetVector(ShaderID_CharacterPosition, vector + vector2);
				}
			}
			characterPositionRenderer.materials = materials2;
		}
		void hideIf(Transform transform3, HideBodyPart bodyPart, string key)
		{
			bool num5 = transform3.name.ToLower().Replace("_", "").Contains(key.ToLower());
			bool flag2 = ((uint)bodyHideFlags & (uint)bodyPart) != 0;
			if (num5 && (!(outfitGameObject != null) || !isAncestorOf(transform3, outfitGameObject.transform)) && (!(hairGameObject != null) || !isAncestorOf(transform3, hairGameObject.transform)))
			{
				transform3.gameObject.SetActive(!flag2);
			}
		}
		static bool isAncestorOf(Transform transform4, Transform root)
		{
			Transform transform3 = transform4;
			while (transform3 != null)
			{
				if (transform3 == root)
				{
					return true;
				}
				transform3 = transform3.parent;
			}
			return false;
		}
		Transform lookupBone(string name)
		{
			if (boneMap.TryGetValue(name, out var value))
			{
				return value;
			}
			return null;
		}
		static void replaceSection(MaterialTarget[] materialTargets, Texture2D texture, Color color, float hardLight)
		{
			for (int n = 0; n < materialTargets.Length; n++)
			{
				MaterialTarget materialTarget = materialTargets[n];
				Renderer renderer3 = materialTarget.renderer;
				Material[] materials3 = renderer3.materials;
				Material[] array3 = new Material[materials3.Length];
				for (int num5 = 0; num5 < materials3.Length; num5++)
				{
					array3[num5] = materials3[num5];
				}
				array3[materialTarget.materialSlot] = new Material(materials3[materialTarget.materialSlot]);
				array3[materialTarget.materialSlot].SetTexture(ShaderID_BaseColorMap, texture);
				array3[materialTarget.materialSlot].SetColor(ShaderID_BaseColor, color);
				array3[materialTarget.materialSlot].SetFloat(ShaderID_HardLightRatio, hardLight);
				renderer3.materials = array3;
			}
		}
	}

	public static bool isPremiumOutfit(Outfit outfit)
	{
		return (int)(outfit + 1) % 2 != 0;
	}
}
