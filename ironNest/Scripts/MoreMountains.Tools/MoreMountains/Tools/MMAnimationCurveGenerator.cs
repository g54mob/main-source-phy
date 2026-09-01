using System;
using System.Reflection;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMAnimationCurveGenerator : MonoBehaviour
	{
		[Header("Save settings")]
		public string AnimationCurveFilePath;

		public string AnimationCurveFileName;

		[Header("Animation Curves")]
		public int Resolution;

		public bool GenerateAntiCurves;

		[MMInspectorButton("GenerateAnimationCurvesAsset")]
		public bool GenerateAnimationCurvesButton;

		protected Type _scriptableObjectType;

		protected Keyframe _keyframe;

		protected MethodInfo _addMethodInfo;

		protected object[] _parameters;

		public virtual void GenerateAnimationCurvesAsset()
		{
		}

		protected virtual void CreateAnimationCurve(ScriptableObject asset, MMTween.MMTweenCurve curveType, int curveResolution, bool anti)
		{
		}
	}
}
