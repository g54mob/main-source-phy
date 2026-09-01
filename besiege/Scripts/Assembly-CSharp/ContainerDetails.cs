using System;
using BlockMapperInternal;
using Selectors;
using UnityEngine;

public class ContainerDetails : MonoBehaviour, IWidgetContainer
{
	public enum AnchorPos
	{
		Middle = 0,
		Top = 1
	}

	public Transform Background;

	public AnchorPos Anchor;

	public Selector selector;

	public ParameterWidget widget;

	protected Func<ContainerDetails, float> GetHeight = (ContainerDetails c) => c.BackgroundScale.y;

	public float Height
	{
		get
		{
			return GetHeight(this);
		}
	}

	public float TopOffset
	{
		get
		{
			return (Anchor != AnchorPos.Middle) ? 0f : (Height / 2f);
		}
	}

	public float BottomOffset
	{
		get
		{
			return (Anchor != AnchorPos.Middle) ? Height : (Height / 2f);
		}
	}

	protected Vector3 BackgroundPos
	{
		get
		{
			return (!Background) ? base.transform.position : Background.position;
		}
	}

	protected Vector3 BackgroundScale
	{
		get
		{
			return (!Background) ? Vector3.zero : Background.lossyScale;
		}
	}

	public float Top
	{
		get
		{
			return BackgroundPos.y + TopOffset;
		}
		set
		{
			base.transform.position = new Vector3(BackgroundPos.x, value - TopOffset);
		}
	}

	public float Bottom
	{
		get
		{
			return BackgroundPos.y - BottomOffset;
		}
		set
		{
			base.transform.position = new Vector3(BackgroundPos.x, value + BottomOffset);
		}
	}

	public float Z
	{
		get
		{
			return base.transform.position.z;
		}
		set
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, value);
		}
	}

	public float TopValue()
	{
		return Top;
	}

	public float ZValue()
	{
		return Z;
	}

	public void ExtendTop(float amount)
	{
		if (amount != 0f)
		{
		}
	}

	public void ExtendBottom(float amount)
	{
		if (amount != 0f)
		{
		}
	}
}
