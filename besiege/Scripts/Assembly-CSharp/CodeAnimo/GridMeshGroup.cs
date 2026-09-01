using System;
using System.Collections.Generic;
using CodeAnimo.UnityExtensionMethods;
using UnityEngine;

namespace CodeAnimo
{
	[Serializable]
	public class GridMeshGroup : MonoBehaviour
	{
		public delegate void SegmentEventHandler(UnityEngine.Object sender, SegmentEventData segmentData);

		public delegate void MeshGroupEventHandler(GridMeshGroup meshGroup);

		[SerializeField]
		private Material m_selectedMaterial;

		public InteractiveLoader meshLoader;

		public GridHeightData heightData;

		public int totalGridUnitCountU = 512;

		public int totalGridUnitCountV = 512;

		public int segmentGridUnitCountU = 64;

		public int segmentGridUnitCountV = 64;

		public float totalMeshWidth = 512f;

		public float totalMeshDepth = 512f;

		public string meshNamePrefix = "Grouped GridMesh";

		[SerializeField]
		protected List<GridMesh> groupedMeshes = new List<GridMesh>();

		private bool creatingGroup;

		private int sessionTotalGridUnitCountU;

		private int sessionTotalGridUnitCountV;

		private int sessionSegmentGridUnitCountU;

		private int sessionSegmentGridUnitCountV;

		private float sessionGridUnitSizeU;

		private float sessionGridUnitSizeV;

		protected Vector3 sessionStartPosition;

		private int nextOffsetU;

		private int nextOffsetV;

		public Material selectedMaterial
		{
			get
			{
				return m_selectedMaterial;
			}
			set
			{
				m_selectedMaterial = value;
				ApplyMaterialToAllMeshes(value);
			}
		}

		public bool isCreatingGroup
		{
			get
			{
				return creatingGroup;
			}
		}

		public bool containsSegments
		{
			get
			{
				return groupedMeshes.Count > 0;
			}
		}

		public int groupSize
		{
			get
			{
				return groupedMeshes.Count;
			}
		}

		public event MeshGroupEventHandler groupComplete;

		public event SegmentEventHandler segmentCreated;

		protected virtual void Reset()
		{
			InteractiveLoader interactiveLoader = base.gameObject.AddComponentIfMissing<InteractiveLoader>();
			if (interactiveLoader != null)
			{
				meshLoader = interactiveLoader;
			}
			else
			{
				meshLoader = GetComponent<InteractiveLoader>();
			}
		}

		protected void OnValidate()
		{
			selectedMaterial = m_selectedMaterial;
		}

		public virtual void StartCreatingGroup()
		{
			if (creatingGroup)
			{
				return;
			}
			creatingGroup = true;
			sessionStartPosition = base.transform.position;
			GameObjectExtensions.Unity4_3_4UndoCrashWorkaroundEnabled = false;
			if (heightData != null && heightData.hasData)
			{
				int num = Mathf.Min(heightData.maximumU, totalGridUnitCountU);
				int num2 = Mathf.Min(heightData.maximumV, totalGridUnitCountV);
				if (num < 1)
				{
					Debug.LogWarning("Group has no width (U)", this);
					StopCreatingGroup();
					return;
				}
				if (num2 < 1)
				{
					Debug.LogWarning("Group has no Height (V)", this);
					StopCreatingGroup();
					return;
				}
				totalGridUnitCountU = num;
				totalGridUnitCountV = num2;
			}
			sessionTotalGridUnitCountU = totalGridUnitCountU;
			sessionTotalGridUnitCountV = totalGridUnitCountV;
			sessionSegmentGridUnitCountU = segmentGridUnitCountU;
			sessionSegmentGridUnitCountV = segmentGridUnitCountV;
			sessionGridUnitSizeU = totalMeshWidth / (float)totalGridUnitCountU;
			sessionGridUnitSizeV = totalMeshDepth / (float)totalGridUnitCountV;
			nextOffsetU = 0;
			nextOffsetV = 0;
			int num3 = Mathf.CeilToInt(sessionTotalGridUnitCountU / sessionSegmentGridUnitCountU);
			int num4 = Mathf.CeilToInt(sessionTotalGridUnitCountV / sessionSegmentGridUnitCountV);
			int num5 = num3 * num4;
			for (int i = 0; i < num5; i++)
			{
				EnqueueSegmentCreation();
			}
			meshLoader.StartLoading();
		}

		protected void EnqueueSegmentCreation()
		{
			meshLoader.AddMethod(CreateNextSegment);
			meshLoader.AddMethod(UpdateGroupCreationLoop);
		}

		public void StopCreatingGroup()
		{
			GameObjectExtensions.Unity4_3_4UndoCrashWorkaroundEnabled = true;
			creatingGroup = false;
		}

		public void AddGroupCompleteHandler(MeshGroupEventHandler handler)
		{
			this.groupComplete = (MeshGroupEventHandler)Delegate.Remove(this.groupComplete, handler);
			this.groupComplete = (MeshGroupEventHandler)Delegate.Combine(this.groupComplete, handler);
		}

		private void CreateNextSegment()
		{
			Vector3 position = sessionStartPosition;
			position.x += sessionGridUnitSizeU * (float)nextOffsetU;
			position.z += sessionGridUnitSizeV * (float)nextOffsetV;
			GridMesh gridMesh = CreateSegment();
			gridMesh.transform.position = position;
			SetupSegment(gridMesh);
			OnSegmentCreated(new SegmentEventData(gridMesh));
			if (heightData != null)
			{
				gridMesh.SetupEvents();
			}
			else
			{
				gridMesh.GenerateGrid();
			}
		}

		protected virtual void SetupSegment(GridMesh segment)
		{
			segment.gridUnitCountU = sessionSegmentGridUnitCountU;
			segment.gridUnitCountV = sessionSegmentGridUnitCountV;
			segment.groupUnitCountU = sessionTotalGridUnitCountU;
			segment.groupUnitCountV = sessionTotalGridUnitCountU;
			segment.gridUnitSizeU = sessionGridUnitSizeU;
			segment.gridUnitSizeV = sessionGridUnitSizeV;
			segment.heightData = heightData;
			segment.offsetU = nextOffsetU;
			segment.offsetV = nextOffsetV;
			segment.hideFlags = HideFlags.HideInHierarchy;
			segment.name = CreateSegmentName();
			segment.transform.parent = base.transform;
			groupedMeshes.Add(segment);
			segment.GetComponent<Renderer>().sharedMaterial = m_selectedMaterial;
			segment.GenerateGrid();
		}

		private void UpdateGroupCreationLoop()
		{
			nextOffsetU += sessionSegmentGridUnitCountU;
			if (sessionTotalGridUnitCountU - nextOffsetU < 1)
			{
				nextOffsetU = 0;
				nextOffsetV += sessionSegmentGridUnitCountV;
				if (sessionTotalGridUnitCountV - nextOffsetV < 1)
				{
					StopCreatingGroup();
					OnGroupCreated();
				}
			}
		}

		protected virtual GridMesh CreateSegment()
		{
			GameObject gameObject = new GameObject("incomplete grouped mesh");
			return gameObject.AddComponent<GridMesh>();
		}

		public void DeleteGroupMembers()
		{
			foreach (GridMesh groupedMesh in groupedMeshes)
			{
				if (!(groupedMesh == null))
				{
					groupedMesh.DestroyGeneratedMesh();
					UnityEngine.Object.DestroyImmediate(groupedMesh.gameObject);
				}
			}
			groupedMeshes.Clear();
		}

		private string CreateSegmentName()
		{
			int num = nextOffsetU / sessionSegmentGridUnitCountU;
			int num2 = nextOffsetV / sessionSegmentGridUnitCountV;
			return meshNamePrefix + " (" + num + "; " + num2 + ")";
		}

		private void OnGroupCreated()
		{
			if (this.groupComplete != null)
			{
				this.groupComplete(this);
			}
		}

		private void OnSegmentCreated(SegmentEventData e)
		{
			if (this.segmentCreated != null)
			{
				this.segmentCreated(this, e);
			}
		}

		protected void ApplyMaterialToAllMeshes(Material newMaterial)
		{
			for (int i = 0; i < groupSize; i++)
			{
				groupedMeshes[i].GetComponent<Renderer>().sharedMaterial = newMaterial;
			}
		}
	}
}
