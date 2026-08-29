using System.Collections.Generic;
using Battlehub.RTCommon;
using Battlehub.RTHandles;
using UnityEngine;

public class TransformGizmoNeo : MonoBehaviour
{
	public PositionHandle positionHandle;

	public RotationHandle rotationHandle;

	public ScaleHandle scaleHandle;

	private bool wasTransformingLastFrame;

	public TransformSpace _space;

	private TransformPivot _pivot;

	private TransformType _transformType;

	private bool _isSnapping;

	private List<Transform> targets = new List<Transform>();

	private bool recalculateGizmos = true;

	private AxisInfo axisInfo;

	public Vector3 pivotPoint => transformType switch
	{
		TransformType.Move => positionHandle.Position, 
		TransformType.Rotate => rotationHandle.Position, 
		TransformType.Scale => scaleHandle.Position, 
		_ => positionHandle.Position, 
	};

	private bool isTransformingBase
	{
		get
		{
			if (!positionHandle.IsDragging && !rotationHandle.IsDragging)
			{
				return scaleHandle.IsDragging;
			}
			return true;
		}
	}

	public bool isTransforming
	{
		get
		{
			if (!isTransformingBase)
			{
				return wasTransformingLastFrame;
			}
			return true;
		}
	}

	public Transform mainTargetRoot
	{
		get
		{
			if (targets.Count <= 0)
			{
				return null;
			}
			return targets[0];
		}
	}

	public TransformSpace space
	{
		get
		{
			return _space;
		}
		set
		{
			if (_space != value)
			{
				_space = value;
				recalculateGizmos = true;
			}
		}
	}

	public TransformPivot pivot
	{
		get
		{
			return _pivot;
		}
		set
		{
			if (_pivot != value)
			{
				_pivot = value;
				recalculateGizmos = true;
			}
		}
	}

	public TransformType transformType
	{
		get
		{
			return _transformType;
		}
		set
		{
			if (_transformType != value)
			{
				_transformType = value;
				recalculateGizmos = true;
			}
		}
	}

	public bool isSnapping
	{
		get
		{
			return _isSnapping;
		}
		set
		{
			if (_isSnapping != value)
			{
				_isSnapping = value;
				recalculateGizmos = true;
			}
		}
	}

	public void doUpdate()
	{
		positionHandle.doUpdate();
		rotationHandle.doUpdate();
		scaleHandle.doUpdate();
	}

	public void LateUpdate()
	{
		wasTransformingLastFrame = isTransformingBase;
		if (recalculateGizmos)
		{
			recalculateGizmos = false;
			positionHandle.Editor.Tools.PivotRotation = ((space == TransformSpace.Global) ? RuntimePivotRotation.Global : RuntimePivotRotation.Local);
			rotationHandle.Editor.Tools.PivotRotation = ((space == TransformSpace.Global) ? RuntimePivotRotation.Global : RuntimePivotRotation.Local);
			scaleHandle.Editor.Tools.PivotRotation = ((space == TransformSpace.Global) ? RuntimePivotRotation.Global : RuntimePivotRotation.Local);
			positionHandle.Editor.Tools.PivotMode = ((pivot != TransformPivot.Center) ? RuntimePivotMode.Pivot : RuntimePivotMode.Center);
			rotationHandle.Editor.Tools.PivotMode = ((pivot != TransformPivot.Center) ? RuntimePivotMode.Pivot : RuntimePivotMode.Center);
			scaleHandle.Editor.Tools.PivotMode = ((pivot != TransformPivot.Center) ? RuntimePivotMode.Pivot : RuntimePivotMode.Center);
			positionHandle.SnapToGrid = isSnapping;
			rotationHandle.SnapToGrid = isSnapping;
			scaleHandle.SnapToGrid = isSnapping;
			positionHandle.gameObject.SetActive(transformType == TransformType.Move);
			rotationHandle.gameObject.SetActive(transformType == TransformType.Rotate);
			scaleHandle.gameObject.SetActive(transformType == TransformType.Scale);
			positionHandle.Targets = targets.ToArray();
			rotationHandle.Targets = targets.ToArray();
			scaleHandle.Targets = targets.ToArray();
		}
	}

	public void EndDragging()
	{
		positionHandle.EndDrag();
		rotationHandle.EndDrag();
		scaleHandle.EndDrag();
	}

	public void ClearTargets()
	{
		targets.Clear();
		recalculateGizmos = true;
	}

	public AxisInfo GetAxisInfo()
	{
		AxisInfo result = axisInfo;
		if (space == TransformSpace.Global)
		{
			_ = transformType;
			_ = 1;
		}
		return result;
	}

	public void AddTarget(Transform target, bool calcPivot = true)
	{
		targets.Add(target);
		recalculateGizmos = true;
	}

	public void ClearAndAddTarget(Transform target)
	{
		ClearTargets();
		AddTarget(target);
	}
}
