using System;
using Localisation;
using UnityEngine;

public class AlignUI : MonoBehaviour, ILocalisationAware
{
	public enum Mode
	{
		Screen = 0,
		Component = 1,
		Transform = 2
	}

	public enum Horizontal
	{
		Left = 0,
		Middle = 1,
		Right = 2
	}

	public enum Vertical
	{
		Top = 0,
		Middle = 1,
		Bottom = 2
	}

	public Mode mode;

	public AlignUI target;

	public Transform quad;

	public Transform[] boundingContent = new Transform[0];

	public bool autoAlign = true;

	public AlignUI parent;

	public Horizontal horizontal = Horizontal.Middle;

	public Vertical vertical = Vertical.Middle;

	public Horizontal targetX = Horizontal.Middle;

	public Vertical targetY = Vertical.Middle;

	public bool moveHorizontally = true;

	public bool moveVertically = true;

	private Camera cam;

	protected bool started;

	public Action OnAlign;

	public float leftMost;

	public float rightMost;

	public float topMost;

	public float bottomMost;

	public Vector2 padding = Vector2.zero;

	public virtual Vector3 AlignTo
	{
		get
		{
			float num = 0f;
			float num2 = 0f;
			switch (mode)
			{
			case Mode.Transform:
			{
				num = quad.position.x;
				num2 = quad.position.y;
				Vector3 vector = quad.lossyScale / 2f;
				switch (targetX)
				{
				case Horizontal.Left:
					num -= vector.x;
					break;
				case Horizontal.Right:
					num += vector.x;
					break;
				}
				switch (targetY)
				{
				case Vertical.Bottom:
					num2 -= vector.y;
					break;
				case Vertical.Top:
					num2 += vector.y;
					break;
				}
				return new Vector3(num, num2);
			}
			case Mode.Component:
				if (!Application.isPlaying)
				{
					target.Align();
				}
				switch (targetX)
				{
				case Horizontal.Left:
					num = target.leftMost;
					break;
				case Horizontal.Right:
					num = target.rightMost;
					break;
				default:
					num = (target.leftMost + target.rightMost) * 0.5f;
					break;
				}
				switch (targetY)
				{
				case Vertical.Bottom:
					num2 = target.bottomMost;
					break;
				case Vertical.Top:
					num2 = target.topMost;
					break;
				default:
					num2 = (target.bottomMost + target.topMost) * 0.5f;
					break;
				}
				return new Vector3(num, num2);
			default:
				if (cam == null)
				{
					if (StatMaster.isMainMenu)
					{
						if (base.gameObject.layer == 0)
						{
							cam = Camera.main;
						}
						else
						{
							cam = GameObject.FindGameObjectWithTag("hudCamera").GetComponent<Camera>();
						}
					}
					else
					{
						cam = GameObject.FindGameObjectWithTag("hudCamera").GetComponent<Camera>();
					}
				}
				num = 0.5f;
				num2 = 0.5f;
				switch (targetX)
				{
				case Horizontal.Left:
					num = 0f;
					break;
				case Horizontal.Right:
					num = 1f;
					break;
				}
				switch (targetY)
				{
				case Vertical.Bottom:
					num2 = 0f;
					break;
				case Vertical.Top:
					num2 = 1f;
					break;
				}
				return cam.ViewportToWorldPoint(new Vector3(num, num2, 0f));
			}
		}
	}

	protected bool Auto
	{
		get
		{
			return autoAlign && mode != Mode.Component;
		}
	}

	protected void Awake()
	{
		started = true;
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Combine(ReferenceMaster.onResolutionChanged, new Action(AttemptAlign));
		if (mode == Mode.Component)
		{
			AlignUI alignUI = target;
			alignUI.OnAlign = (Action)Delegate.Combine(alignUI.OnAlign, new Action(Align));
		}
		else if (!autoAlign && (bool)parent)
		{
			AlignUI alignUI2 = parent;
			alignUI2.OnAlign = (Action)Delegate.Combine(alignUI2.OnAlign, new Action(Align));
		}
	}

	public void ChangeTarget(AlignUI t)
	{
		if (!started)
		{
			if (mode == Mode.Component)
			{
				target = t;
			}
			else if (!autoAlign)
			{
				parent = t;
			}
		}
		else if (mode == Mode.Component)
		{
			if ((bool)target)
			{
				AlignUI alignUI = target;
				alignUI.OnAlign = (Action)Delegate.Remove(alignUI.OnAlign, new Action(Align));
			}
			target = t;
			AlignUI alignUI2 = target;
			alignUI2.OnAlign = (Action)Delegate.Combine(alignUI2.OnAlign, new Action(Align));
		}
		else if (!autoAlign)
		{
			if ((bool)parent)
			{
				AlignUI alignUI3 = parent;
				alignUI3.OnAlign = (Action)Delegate.Remove(alignUI3.OnAlign, new Action(Align));
			}
			parent = t;
			AlignUI alignUI4 = parent;
			alignUI4.OnAlign = (Action)Delegate.Combine(alignUI4.OnAlign, new Action(Align));
		}
	}

	protected virtual void Start()
	{
		if (mode == Mode.Component)
		{
			Align();
		}
		else
		{
			AttemptAlign();
		}
	}

	public void OnLocalisationChange()
	{
		if (Auto)
		{
			Align();
		}
	}

	protected virtual void OnDestroy()
	{
		if (started)
		{
			ReferenceMaster.onResolutionChanged = (Action)Delegate.Remove(ReferenceMaster.onResolutionChanged, new Action(AttemptAlign));
			if (mode == Mode.Component)
			{
				AlignUI alignUI = target;
				alignUI.OnAlign = (Action)Delegate.Remove(alignUI.OnAlign, new Action(Align));
			}
			else if (!autoAlign && (bool)parent)
			{
				AlignUI alignUI2 = parent;
				alignUI2.OnAlign = (Action)Delegate.Remove(alignUI2.OnAlign, new Action(Align));
			}
		}
	}

	public virtual void AttemptAlign()
	{
		if (Auto)
		{
			Align();
		}
	}

	public virtual void Align()
	{
		Vector3 alignTo = AlignTo;
		float num = alignTo.x + padding.x;
		float num2 = alignTo.y + padding.y;
		if (boundingContent.Length > 0)
		{
			GetBounds();
			Vector3 zero = Vector3.zero;
			Vector3 position = new Vector3(leftMost, topMost);
			Vector3 position2 = new Vector3(rightMost, bottomMost);
			position = base.transform.InverseTransformPoint(position);
			position.z = 0f;
			position2 = base.transform.InverseTransformPoint(position2);
			position2.z = 0f;
			switch (horizontal)
			{
			case Horizontal.Left:
				zero.x -= position.x;
				break;
			case Horizontal.Right:
				zero.x -= position2.x;
				break;
			case Horizontal.Middle:
				zero.x -= (position.x + position2.x) * 0.5f;
				break;
			}
			switch (vertical)
			{
			case Vertical.Bottom:
				zero.y -= position2.y;
				break;
			case Vertical.Top:
				zero.y -= position.y;
				break;
			case Vertical.Middle:
				zero.x -= (position.y + position2.y) * 0.5f;
				break;
			}
			zero.z = 0f;
			foreach (Transform item in base.transform)
			{
				item.localPosition += zero;
			}
			base.transform.position = new Vector3(moveHorizontally ? num : base.transform.position.x, moveVertically ? num2 : base.transform.position.y, base.transform.position.z);
			GetBounds();
		}
		else
		{
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = base.transform.lossyScale / 2f;
			Renderer component = base.gameObject.GetComponent<Renderer>();
			if ((bool)component)
			{
				vector = component.bounds.center - base.transform.position;
				vector2 = component.bounds.extents;
			}
			switch (horizontal)
			{
			case Horizontal.Left:
				num += vector2.x;
				break;
			case Horizontal.Right:
				num -= vector2.x;
				break;
			}
			switch (vertical)
			{
			case Vertical.Bottom:
				num2 += vector2.y;
				break;
			case Vertical.Top:
				num2 -= vector2.y;
				break;
			}
			num -= vector.x;
			num2 -= vector.y;
			base.transform.position = new Vector3(moveHorizontally ? num : base.transform.position.x, moveVertically ? num2 : base.transform.position.y, base.transform.position.z);
			leftMost = base.transform.position.x - vector2.x + vector.x;
			rightMost = base.transform.position.x + vector2.x + vector.x;
			topMost = base.transform.position.y + vector2.y + vector.y;
			bottomMost = base.transform.position.y - vector2.y + vector.y;
		}
		if (OnAlign != null)
		{
			OnAlign();
		}
	}

	private void GetBounds()
	{
		Bounds bounds = default(Bounds);
		for (int i = 0; i < boundingContent.Length; i++)
		{
			Transform transform = boundingContent[i];
			if (!transform)
			{
				continue;
			}
			Renderer component = transform.GetComponent<Renderer>();
			if ((bool)component)
			{
				if (transform == boundingContent[0])
				{
					bounds = component.bounds;
				}
				else
				{
					bounds.Encapsulate(component.bounds);
				}
			}
			else if (transform == boundingContent[0])
			{
				bounds = new Bounds(transform.position, transform.lossyScale);
			}
			else
			{
				bounds.Encapsulate(new Bounds(transform.position, transform.lossyScale));
			}
		}
		leftMost = bounds.center.x - bounds.extents.x;
		rightMost = bounds.center.x + bounds.extents.x;
		topMost = bounds.center.y + bounds.extents.y;
		bottomMost = bounds.center.y - bounds.extents.y;
	}
}
