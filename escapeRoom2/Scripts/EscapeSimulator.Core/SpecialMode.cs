using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialMode
{
	public EditorMode mode;

	public Vector3 restoreCamPos;

	public Quaternion restoreCamRot;

	public int restoreCamCullingMask;

	public bool restoreSelection;

	public Toggle restoreTransformTool;

	public TransformData restorePivot;

	public TransformData cachedPivot;

	public List<TweenState.TweenStateRecord> restoreTweenState;

	public Transform pivotTransform;

	public bool multiTarget;

	public PropInstance target;

	public HashSet<PropInstance> targets;

	public Predicate<PropInstance> raycastPredicate;

	public List<TargetPin> targetPins = new List<TargetPin>();

	public int lockPasswordIndex;

	public HashSet<(PropInstance, int)> lockPasswordIndexes;

	public OriginalMaterials[] originalMaterials;

	public Action<PropInstance, int> targetModeApply;

	public Action<HashSet<PropInstance>, HashSet<(PropInstance, int)>> targetModeApplyMultiple;

	public List<PasswordPopup> passwordPopups;

	public bool updateZoomableCamera;

	public Vector3 zoomableEyeRotation;

	public Vector3 zoomableEyePosition;
}
