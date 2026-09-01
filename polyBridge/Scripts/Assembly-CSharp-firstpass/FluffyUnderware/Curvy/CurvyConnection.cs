using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	[ExecuteInEditMode]
	[HelpURL("https://curvyeditor.com/doclink/curvyconnection")]
	public class CurvyConnection : MonoBehaviour, ISerializationCallbackReceiver
	{
		[SerializeField]
		[Hide]
		private List<CurvySplineSegment> m_ControlPoints = new List<CurvySplineSegment>();

		private ReadOnlyCollection<CurvySplineSegment> readOnlyControlPoints;

		private Couple<Vector3, Quaternion> processedConnectionCoordinates;

		private Dictionary<CurvySplineSegment, Couple<Vector3, Quaternion>> processedControlPointsCoordinates = new Dictionary<CurvySplineSegment, Couple<Vector3, Quaternion>>();

		public ReadOnlyCollection<CurvySplineSegment> ControlPointsList
		{
			get
			{
				if (readOnlyControlPoints == null)
				{
					readOnlyControlPoints = m_ControlPoints.AsReadOnly();
				}
				return readOnlyControlPoints;
			}
		}

		public int Count => m_ControlPoints.Count;

		public CurvySplineSegment this[int idx] => m_ControlPoints[idx];

		private void OnEnable()
		{
			Transform transform = base.transform;
			processedConnectionCoordinates = new Couple<Vector3, Quaternion>(transform.position, transform.rotation);
			processedControlPointsCoordinates.Clear();
			for (int i = 0; i < m_ControlPoints.Count; i++)
			{
				CurvySplineSegment curvySplineSegment = m_ControlPoints[i];
				processedControlPointsCoordinates[curvySplineSegment] = new Couple<Vector3, Quaternion>(curvySplineSegment.transform.position, curvySplineSegment.transform.rotation);
			}
		}

		private void OnDisable()
		{
		}

		private void Update()
		{
			if (Application.isPlaying)
			{
				DoUpdate();
			}
		}

		private void LateUpdate()
		{
			if (Application.isPlaying)
			{
				DoUpdate();
			}
		}

		private void FixedUpdate()
		{
			if (Application.isPlaying)
			{
				DoUpdate();
			}
		}

		private void OnDestroy()
		{
			if (1 == 0)
			{
				return;
			}
			foreach (CurvySplineSegment controlPoint in m_ControlPoints)
			{
				controlPoint.ResetConnectionRelatedData();
			}
			m_ControlPoints.Clear();
			processedControlPointsCoordinates.Clear();
		}

		public static CurvyConnection Create(params CurvySplineSegment[] controlPoints)
		{
			CurvyGlobalManager instance = DTSingleton<CurvyGlobalManager>.Instance;
			if (instance == null)
			{
				DTLog.LogError("[Curvy] Couldn't find Curvy Global Manager. Please raise a bug report.");
				return null;
			}
			CurvyConnection curvyConnection = instance.AddChildGameObject<CurvyConnection>("Connection");
			if (!curvyConnection)
			{
				return null;
			}
			if (controlPoints.Length != 0)
			{
				curvyConnection.transform.position = controlPoints[0].transform.position;
				curvyConnection.AddControlPoints(controlPoints);
			}
			return curvyConnection;
		}

		public void AddControlPoints(params CurvySplineSegment[] controlPoints)
		{
			foreach (CurvySplineSegment curvySplineSegment in controlPoints)
			{
				if (!curvySplineSegment.Connection)
				{
					m_ControlPoints.Add(curvySplineSegment);
					processedControlPointsCoordinates[curvySplineSegment] = new Couple<Vector3, Quaternion>(curvySplineSegment.transform.position, curvySplineSegment.transform.rotation);
					curvySplineSegment.Connection = this;
				}
			}
			AutoSetFollowUp();
		}

		public void AutoSetFollowUp()
		{
			if (Count != 2)
			{
				return;
			}
			CurvySplineSegment curvySplineSegment = m_ControlPoints[0];
			CurvySplineSegment curvySplineSegment2 = m_ControlPoints[1];
			if (curvySplineSegment.transform.position == curvySplineSegment2.transform.position && curvySplineSegment.ConnectionSyncPosition && curvySplineSegment2.ConnectionSyncPosition)
			{
				if (curvySplineSegment.FollowUp == null && (bool)curvySplineSegment.Spline && curvySplineSegment.Spline.CanControlPointHaveFollowUp(curvySplineSegment))
				{
					curvySplineSegment.SetFollowUp(curvySplineSegment2);
				}
				if (curvySplineSegment2.FollowUp == null && (bool)curvySplineSegment2.Spline && curvySplineSegment2.Spline.CanControlPointHaveFollowUp(curvySplineSegment2))
				{
					curvySplineSegment2.SetFollowUp(curvySplineSegment);
				}
			}
		}

		public void RemoveControlPoint(CurvySplineSegment controlPoint, bool destroySelfIfEmpty = true)
		{
			controlPoint.Connection = null;
			m_ControlPoints.Remove(controlPoint);
			processedControlPointsCoordinates.Remove(controlPoint);
			if (m_ControlPoints.Count == 0 && destroySelfIfEmpty)
			{
				Delete();
			}
		}

		public void Delete()
		{
			if (Application.isPlaying)
			{
				Object.Destroy(base.gameObject);
			}
		}

		public List<CurvySplineSegment> OtherControlPoints(CurvySplineSegment source)
		{
			List<CurvySplineSegment> list = new List<CurvySplineSegment>(m_ControlPoints);
			list.Remove(source);
			return list;
		}

		public void SetSynchronisationPositionAndRotation(Vector3 referencePosition, Quaternion referenceRotation)
		{
			Transform obj = base.transform;
			obj.position = referencePosition;
			obj.rotation = referenceRotation;
			obj.hasChanged = false;
			processedConnectionCoordinates.First = referencePosition;
			processedConnectionCoordinates.Second = referenceRotation;
			for (int i = 0; i < m_ControlPoints.Count; i++)
			{
				CurvySplineSegment curvySplineSegment = m_ControlPoints[i];
				bool flag = curvySplineSegment.ConnectionSyncPosition && curvySplineSegment.transform.position.NotApproximately(referencePosition);
				bool flag2 = curvySplineSegment.ConnectionSyncRotation && curvySplineSegment.transform.rotation.DifferentOrientation(referenceRotation);
				if (flag)
				{
					curvySplineSegment.transform.position = referencePosition;
				}
				if (flag2)
				{
					curvySplineSegment.transform.rotation = referenceRotation;
				}
				Couple<Vector3, Quaternion> couple = processedControlPointsCoordinates[curvySplineSegment];
				couple.First = curvySplineSegment.transform.position;
				couple.Second = curvySplineSegment.transform.rotation;
				if (flag || (flag2 && curvySplineSegment.OrientatinInfluencesSpline))
				{
					curvySplineSegment.Spline.SetDirtyPartial(curvySplineSegment, flag ? SplineDirtyingType.Everything : SplineDirtyingType.OrientationOnly);
				}
			}
		}

		private void DoUpdate()
		{
			Transform transform = base.transform;
			bool flag;
			if (transform.hasChanged)
			{
				transform.hasChanged = false;
				if (transform.position.NotApproximately(processedConnectionCoordinates.First) || transform.rotation.DifferentOrientation(processedConnectionCoordinates.Second))
				{
					SetSynchronisationPositionAndRotation(transform.position, transform.rotation);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				return;
			}
			Vector3? vector = null;
			Quaternion? quaternion = null;
			foreach (CurvySplineSegment controlPoint in m_ControlPoints)
			{
				Couple<Vector3, Quaternion> couple = processedControlPointsCoordinates[controlPoint];
				Transform transform2 = controlPoint.transform;
				if (controlPoint.ConnectionSyncPosition && transform2.position.NotApproximately(couple.First))
				{
					vector = transform2.position;
				}
				if (controlPoint.ConnectionSyncRotation && transform2.rotation.DifferentOrientation(couple.Second))
				{
					quaternion = transform2.rotation;
				}
				if (vector.HasValue && quaternion.HasValue)
				{
					break;
				}
			}
			if (vector.HasValue || quaternion.HasValue)
			{
				SetSynchronisationPositionAndRotation(vector ?? base.transform.position, quaternion ?? base.transform.rotation);
			}
		}

		public void OnBeforeSerialize()
		{
			m_ControlPoints.RemoveAll((CurvySplineSegment cp) => (object)cp == null);
		}

		public void OnAfterDeserialize()
		{
			m_ControlPoints.RemoveAll((CurvySplineSegment cp) => (object)cp == null);
		}
	}
}
