using System.Collections.Generic;
using UnityEngine;

public static class Stitcher
{
	public class TransformCatalog : Dictionary<string, Transform>
	{
		public enum RigType
		{
			None = 0,
			Human = 1,
			Halfling = 2,
			Mammoth = 3,
			Horse = 4,
			Minotaur = 5
		}

		public static List<string> TRANSFORM_NAMES = new List<string>
		{
			"Armature", "Hip", "Torso", "Neck", "Head", "Leg_Left", "Leg_Right", "Knee_Left", "Knee_Right", "Foot_Left",
			"Foot_Right", "Arm_Left", "Arm_Right", "Elbow_Left", "Elbow_Right", "Wrist_Left", "Wrist_Right", "Hand_Left", "Hand_Right"
		};

		public static List<string> TRANSFORM_NAMES_HALFLING = new List<string>
		{
			"Armature", "Hip", "Torso", "Neck", "Head", "Leg_Left", "Leg_Right", "Knee_Left", "Knee_Right", "Foot_Left",
			"Foot_Right", "Arm_Left", "Arm_Right", "Elbow_Left", "Elbow_Right", "Wrist_Left", "Wrist_Right", "Hand_Left", "Hand_Right"
		};

		public static List<string> TRANSFORM_NAMES_MAMMOTH = new List<string>
		{
			"Armature", "Hip", "Spine", "Head", "Trunk001", "Trunk002", "Trunk003", "Trunk004", "Trunk_End", "Leg_Back_Left",
			"Leg_Back_Right", "Knee_Back_Left", "Knee_Back_Right", "Foot_Back_Left", "Foot_Back_Right", "Leg_Front_Left", "Leg_Front_Right", "Knee_Front_Left", "Knee_Front_Right", "Foot_Front_Left",
			"Foot_Front_Right"
		};

		public static List<string> TRANSFORM_NAMES_HORSE = new List<string>
		{
			"Armature", "Bones", "Spine", "Neck", "Head", "Head_End", "Hip_Back_L", "Hip_Back_R", "Leg_Back_L", "Leg_Back_R",
			"Knee_Back_L", "Knee_Back_R", "Hoof_Back_L", "Hoof_Back_R", "Leg_Front_L", "Leg_Front_R", "Knee_Front_L", "Knee_Front_R", "Hoof_Front_L", "Hoof_Front_R"
		};

		public static List<string> TRANSFORM_NAMES_MINOTAUR = new List<string>
		{
			"Armature", "Hip", "Torso", "Neck", "Head", "Leg_Left", "Leg_Right", "Knee_Left", "Knee_Right", "Foot_Left",
			"Foot_Right", "Arm_Left", "Arm_Right", "Elbow_Left", "Elbow_Right", "Wrist_Left", "Wrist_Right", "Hand_Left", "Hand_Right"
		};

		public static List<string> GetBoneNames(RigType type)
		{
			switch (type)
			{
			case RigType.Human:
				return TRANSFORM_NAMES;
			case RigType.Halfling:
				return TRANSFORM_NAMES_HALFLING;
			case RigType.Mammoth:
				return TRANSFORM_NAMES_MAMMOTH;
			case RigType.Horse:
				return TRANSFORM_NAMES_HORSE;
			case RigType.Minotaur:
				return TRANSFORM_NAMES_MINOTAUR;
			default:
				return new List<string>();
			}
		}

		public TransformCatalog(Transform transform, RigType type = RigType.None, string prefix = "")
		{
			Catalog(transform, type, prefix);
		}

		public TransformCatalog(GameObject humanoid, RigType type = RigType.None, string prefix = "")
		{
			Transform transform = humanoid.transform.FindChildRecursive("Rigidbodies");
			Catalog(transform, type, prefix);
		}

		private void Catalog(Transform transform, RigType type, string prefix)
		{
			if (type != RigType.None)
			{
				string name = transform.name;
				if (name.Contains(prefix))
				{
					string text = name.Remove(0, prefix.Length);
					if (GetBoneNames(type).Contains(text))
					{
						Add(text, transform);
					}
				}
			}
			else
			{
				Add(transform.name, transform);
			}
			foreach (Transform item in transform)
			{
				Catalog(item, type, prefix);
			}
		}
	}

	public class DictionaryExtensions
	{
		public static TValue Find<TKey, TValue>(Dictionary<TKey, TValue> source, TKey key)
		{
			source.TryGetValue(key, out var value);
			return value;
		}
	}

	public static GameObject[] Stitch(GameObject sourceClothing, GameObject targetAvatar, TransformCatalog boneCatalog)
	{
		sourceClothing.SetActive(value: true);
		SkinnedMeshRenderer[] componentsInChildren = sourceClothing.GetComponentsInChildren<SkinnedMeshRenderer>();
		GameObject[] array = new GameObject[componentsInChildren.Length];
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = componentsInChildren[i];
			skinnedMeshRenderer.bones = TranslateTransforms(skinnedMeshRenderer.bones, boneCatalog);
			skinnedMeshRenderer.rootBone = GetRootBone(skinnedMeshRenderer.rootBone, boneCatalog);
			skinnedMeshRenderer.transform.SetParent(targetAvatar.transform, worldPositionStays: false);
			array[i] = skinnedMeshRenderer.gameObject;
		}
		Object.Destroy(sourceClothing);
		return array;
	}

	public static GameObject[] StitchUnitEditor(GameObject sourceClothing, GameObject targetAvatar, TransformCatalog boneCatalog)
	{
		sourceClothing.SetActive(value: true);
		SkinnedMeshRenderer[] componentsInChildren = sourceClothing.GetComponentsInChildren<SkinnedMeshRenderer>();
		GameObject[] array = new GameObject[componentsInChildren.Length];
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = componentsInChildren[i];
			skinnedMeshRenderer.bones = TranslateTransforms(skinnedMeshRenderer.bones, boneCatalog);
			skinnedMeshRenderer.rootBone = GetRootBone(skinnedMeshRenderer.rootBone, boneCatalog);
			array[i] = skinnedMeshRenderer.gameObject;
		}
		sourceClothing.transform.SetParent(targetAvatar.transform, worldPositionStays: false);
		Transform transform = sourceClothing.transform.Find("Armature");
		if (transform != null)
		{
			Object.Destroy(transform.gameObject);
		}
		return array;
	}

	public static void ConnectToBones(SkinnedMeshRenderer renderer, TransformCatalog boneCatalog)
	{
		renderer.bones = TranslateTransforms(renderer.bones, boneCatalog);
		renderer.rootBone = GetRootBone(renderer.rootBone, boneCatalog);
	}

	private static GameObject[] AddChildren(SkinnedMeshRenderer[] skinnedMeshRenderers, Transform parent)
	{
		GameObject[] array = new GameObject[skinnedMeshRenderers.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new GameObject(skinnedMeshRenderers[i].name);
			array[i].transform.SetParent(parent, worldPositionStays: false);
		}
		return array;
	}

	private static GameObject AddChild(GameObject source, Transform parent)
	{
		GameObject gameObject = new GameObject(source.name);
		gameObject.transform.SetParent(parent, worldPositionStays: false);
		return gameObject;
	}

	private static SkinnedMeshRenderer AddSkinnedMeshRenderer(SkinnedMeshRenderer source, GameObject parent)
	{
		SkinnedMeshRenderer skinnedMeshRenderer = parent.AddComponent<SkinnedMeshRenderer>();
		skinnedMeshRenderer.sharedMesh = source.sharedMesh;
		skinnedMeshRenderer.materials = source.materials;
		return skinnedMeshRenderer;
	}

	private static Transform[] TranslateTransforms(Transform[] sources, TransformCatalog transformCatalog)
	{
		Transform[] array = new Transform[sources.Length];
		for (int i = 0; i < sources.Length; i++)
		{
			array[i] = DictionaryExtensions.Find(transformCatalog, sources[i].name);
		}
		return array;
	}

	private static Transform GetRootBone(Transform source, TransformCatalog transformCatalog)
	{
		return DictionaryExtensions.Find(transformCatalog, source.name);
	}

	private static void ResetAnimator(GameObject rootObject)
	{
		Animator component = rootObject.GetComponent<Animator>();
		if (!(component == null))
		{
			AnimatorStateInfo currentAnimatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
			float normalizedTime = currentAnimatorStateInfo.normalizedTime;
			int shortNameHash = currentAnimatorStateInfo.shortNameHash;
			rootObject.SetActive(value: false);
			rootObject.SetActive(value: true);
			component.Play(shortNameHash, 0, normalizedTime);
		}
	}

	private static void ResetCloth(GameObject rootObject)
	{
		Cloth[] componentsInChildren = rootObject.GetComponentsInChildren<Cloth>(includeInactive: true);
		foreach (Cloth obj in componentsInChildren)
		{
			obj.enabled = false;
			obj.enabled = true;
		}
	}
}
