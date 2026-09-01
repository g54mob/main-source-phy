using System.Collections.Generic;
using Landfall.TABS_Input;
using UnityEngine;

namespace LevelCreator
{
	public class WaterTool : MonoBehaviour
	{
		private Vector3 targetPosition;

		private Material waterMaterial;

		private DMEditor dmEditor;

		private List<float> vertDistances = new List<float>();

		private InputState inputState = new InputState("WaterToolInputState");

		private void Start()
		{
			dmEditor = DMEditor.Instance;
			dmEditor.SetVisualTargetMode(DMEditor.VisualTargetMode.Dot);
			waterMaterial = dmEditor.water.GetComponentInChildren<MeshRenderer>().material;
			AssignInput();
		}

		private void AssignInput()
		{
			PlayerActions instance = PlayerActions.Instance;
			inputState.AddOnKeyDownListener(instance.m_toolPrimary, delegate
			{
				AddWater();
			});
			inputState.AddOnKeyDownListener(instance.m_toolSecondary, delegate
			{
				RemoveWater();
			});
			InputManager.PushState(inputState);
		}

		private void AddWater()
		{
			Mesh mesh = BuildMesh();
			if (!(mesh == null))
			{
				GameObject obj = new GameObject();
				obj.name = "WaterPlane";
				obj.layer = LayerMask.NameToLayer("Water");
				obj.transform.SetParent(dmEditor.water.transform.parent);
				obj.transform.localScale = Vector3.zero;
				obj.AddComponent<ScaleLerp>();
				obj.AddComponent<MeshFilter>().mesh = mesh;
				obj.AddComponent<MeshRenderer>().material = waterMaterial;
				obj.AddComponent<MeshCollider>().sharedMesh = mesh;
			}
		}

		private Mesh BuildMesh()
		{
			List<Vector3> verticies = GetVerticies();
			if (verticies == null || verticies.Count < 3)
			{
				return null;
			}
			int[] indicies = GetIndicies(verticies);
			Mesh mesh = new Mesh();
			mesh.SetVertices(verticies);
			mesh.SetIndices(indicies, MeshTopology.Triangles, 0);
			return mesh;
		}

		private List<Vector3> GetVerticies()
		{
			List<Vector3> list = new List<Vector3>();
			list.Add(targetPosition);
			vertDistances.Add(0f);
			int num = 100;
			int sampleCount = 25;
			SampleVerticies(list, targetPosition, num, sampleCount);
			SampleSubVerticies(list, num, sampleCount);
			SampleSubVerticies(list, num, sampleCount);
			return list;
		}

		private void SampleVerticies(List<Vector3> verticies, Vector3 startPos, float rayDistance, int sampleCount)
		{
			if (sampleCount == 0)
			{
				return;
			}
			float num = 360f / (float)sampleCount;
			for (int i = 0; i < sampleCount; i++)
			{
				Vector3 vector = Quaternion.Euler(0f, (float)i * num, 0f) * Vector3.forward;
				if (Physics.Raycast(startPos - vector * 0.5f, vector, out var hitInfo, rayDistance, 1 << LayerMask.NameToLayer("Map")))
				{
					verticies.Add(hitInfo.point);
					vertDistances.Add(hitInfo.distance);
					continue;
				}
				break;
			}
		}

		private void SampleSubVerticies(List<Vector3> verticies, float rayDistance, int sampleCount)
		{
			int count = verticies.Count;
			for (int i = 0; i < count; i++)
			{
				SampleVerticies(verticies, verticies[i], rayDistance, sampleCount);
			}
		}

		private void PruneVerticies(List<Vector3> verticies, float radius)
		{
			for (int i = 0; i < verticies.Count; i++)
			{
				float num = vertDistances[i];
				for (int j = 0; j < verticies.Count; j++)
				{
					if (Mathf.Abs(num - vertDistances[j]) < radius)
					{
						vertDistances.RemoveAt(j);
						verticies.RemoveAt(j);
					}
				}
			}
		}

		private int[] GetIndicies(List<Vector3> verticies)
		{
			int[] array = new int[verticies.Count * 3];
			for (int i = 1; i < verticies.Count; i++)
			{
				if (i > 1)
				{
					int num = (i - 2) * 3;
					array[num] = 0;
					array[num + 1] = i - 1;
					array[num + 2] = i;
				}
			}
			return array;
		}

		private void RemoveWater()
		{
			Collider[] array = Physics.OverlapSphere(targetPosition, 0.01f, 1 << LayerMask.NameToLayer("Water"));
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					Object.Destroy(array[i].gameObject);
				}
			}
		}

		private void Update()
		{
			Transform transform = dmEditor.playerCamera.transform;
			targetPosition = Utility.GetTargetPositionIncludingWater(transform.position, transform.forward, dmEditor.rayDistance);
		}

		private void OnDrawGizmos()
		{
		}
	}
}
