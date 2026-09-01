using System;
using Localisation;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UI/Align GUI Object")]
public class AlignGUIObject : MonoBehaviour, ILocalisationAware
{
	public enum ObjectAlignment
	{
		Right = 0,
		Left = 1
	}

	[SerializeField]
	protected Renderer anchorRenderer;

	[SerializeField]
	protected float offset = 0.2f;

	[SerializeField]
	[FormerlySerializedAs("Alignment")]
	protected ObjectAlignment alignment;

	[SerializeField]
	protected bool preferCurrentOffset;

	[SerializeField]
	protected float yOffset;

	[SerializeField]
	protected bool localOffsets;

	[SerializeField]
	protected bool doManualAlignment;

	protected float preferredXPosition;

	protected bool initialized;

	protected virtual void Awake()
	{
		if (AnchorExists())
		{
			Vector3 vector = CalculateTargetPosition();
			preferredXPosition = base.transform.position.x - vector.x;
			initialized = true;
		}
	}

	public void RealignObject()
	{
		DoUpdateAlignment();
	}

	public void OnLocalisationChange()
	{
		if (!initialized)
		{
			Awake();
		}
		DoUpdateAlignment();
	}

	protected virtual void Start()
	{
		if (!doManualAlignment)
		{
			DoUpdateAlignment();
		}
	}

	private bool AnchorExists()
	{
		if (anchorRenderer == null)
		{
			Debug.LogWarning("Anchor renderer is not assigned, can not align GUI object without it.", base.gameObject);
			base.enabled = false;
			return false;
		}
		return true;
	}

	[ContextMenu("DoUpdateAlignment")]
	protected void DoUpdateAlignment()
	{
		if (AnchorExists())
		{
			UpdateAlignment();
		}
	}

	protected virtual void UpdateAlignment()
	{
		Vector3 position = CalculateTargetPosition();
		base.transform.position = position;
		if (localOffsets)
		{
			Vector3 localPosition = base.transform.localPosition;
			if (alignment == ObjectAlignment.Left)
			{
				localPosition.x -= offset;
			}
			else
			{
				localPosition.x += offset;
			}
			localPosition.y += yOffset;
			base.transform.localPosition = localPosition;
		}
	}

	private Vector3 CalculateTargetPosition()
	{
		Vector3 result;
		if (alignment == ObjectAlignment.Left)
		{
			result = anchorRenderer.bounds.min;
			if (!localOffsets)
			{
				result.x -= offset;
			}
		}
		else
		{
			result = anchorRenderer.bounds.max;
			if (!localOffsets)
			{
				result.x += offset;
			}
		}
		if (preferCurrentOffset)
		{
			result.x = ((alignment != ObjectAlignment.Left) ? Math.Max(result.x, preferredXPosition) : Math.Min(result.x, preferredXPosition));
		}
		result.y = anchorRenderer.bounds.center.y + yOffset;
		return result;
	}
}
