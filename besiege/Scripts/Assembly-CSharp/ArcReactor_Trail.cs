using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Arc Reactor Rays/Ray Trail")]
public class ArcReactor_Trail : MonoBehaviour
{
	public class SegmentInfo
	{
		public Vector3 pos;

		public float birthtime;

		public SegmentInfo(Vector3 pos, float birthtime)
		{
			this.pos = pos;
			this.birthtime = birthtime;
		}
	}

	public GameObject arcPrefab;

	public bool truncateByDistance;

	public float distanceThreshold = 10f;

	public bool truncateByLifetime;

	public float lifetimeThreshold = 1f;

	public float precision = 0.01f;

	public Transform globalSpaceTransform;

	public List<SegmentInfo> segments;

	[HideInInspector]
	public ArcReactor_Arc currentArc;

	protected bool initFlag;

	public ArcReactor_Arc DetachRay(bool newshape = false)
	{
		ArcReactor_Arc result = currentArc;
		currentArc = null;
		if (newshape)
		{
			Initialize();
		}
		return result;
	}

	private void Awake()
	{
		segments = new List<SegmentInfo>();
	}

	private void Start()
	{
		initFlag = true;
	}

	private void Initialize()
	{
		segments.Clear();
		segments.Add(new SegmentInfo(base.transform.position, Time.time));
	}

	private void LateUpdate()
	{
		if (initFlag)
		{
			initFlag = false;
			Initialize();
			return;
		}
		if (Vector3.SqrMagnitude(base.transform.position - segments[segments.Count - 1].pos) > precision * precision)
		{
			segments.Add(new SegmentInfo(base.transform.position, Time.time));
		}
		if (truncateByLifetime && segments.Count > 1)
		{
			if (Time.time - segments[segments.Count - 1].birthtime > lifetimeThreshold)
			{
				Initialize();
			}
			else
			{
				for (int i = 0; i < segments.Count - 1; i++)
				{
					if (Time.time - segments[segments.Count - 1 - i].birthtime > lifetimeThreshold)
					{
						segments.RemoveRange(0, segments.Count - 2 - i);
						break;
					}
				}
			}
		}
		if (truncateByDistance && segments.Count > 1)
		{
			float num = Vector3.Distance(base.transform.position, segments[segments.Count - 1].pos);
			if (num > distanceThreshold)
			{
				Initialize();
			}
			else
			{
				for (int j = 0; j < segments.Count - 1; j++)
				{
					num += Vector3.Distance(segments[segments.Count - 1 - j].pos, segments[segments.Count - 2 - j].pos);
					if (num > distanceThreshold)
					{
						segments.RemoveRange(0, segments.Count - 2 - j);
						break;
					}
				}
			}
		}
		if (currentArc == null && segments.Count > 1)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(arcPrefab);
			currentArc = gameObject.GetComponent<ArcReactor_Arc>();
			if (globalSpaceTransform != null)
			{
				gameObject.transform.parent = globalSpaceTransform;
			}
		}
		if (currentArc != null)
		{
			if (currentArc.shapePoints.Length != segments.Count)
			{
				Array.Resize(ref currentArc.shapePoints, Mathf.Max(segments.Count, 2));
			}
			currentArc.shapePoints[0] = base.transform.position;
			for (int k = 0; k < segments.Count - 1; k++)
			{
				currentArc.shapePoints[segments.Count - k - 1] = segments[k].pos;
			}
		}
	}
}
