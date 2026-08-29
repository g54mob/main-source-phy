using System;
using UnityEngine;

[HelpURL("https://developer.oculus.com/documentation/unity/unity-scene-use-scene-anchors/#further-scene-model-unity-components")]
[Obsolete("OVRSceneManager and associated classes are deprecated (v65), please use MR Utility Kit instead (https://developer.oculus.com/documentation/unity/unity-mr-utility-kit-overview)")]
public class OVRSceneObjectTransformType : MonoBehaviour
{
	[Serializable]
	public enum Transformation
	{
		Volume = 0,
		Plane = 1,
		None = 2
	}

	[Tooltip("Choose the type of scene anchor (volume/plane) that may modify this transform.")]
	public Transformation TransformType;
}
