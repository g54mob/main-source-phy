using System.Collections.Generic;
using UnityEngine;

public class ConquestNodeManager : MonoBehaviour
{
	public List<ConquestNodeConnection> currentNodeConnections;

	private List<LineRenderer> lineRenders = new List<LineRenderer>();

	public GameObject linePrefab;

	public float connectionRangeThreshold = 7f;

	private void Start()
	{
		PopulateCurrentNodeConnection();
		OnNodeConnectionWasUpdated();
	}

	private void PopulateCurrentNodeConnection()
	{
		currentNodeConnections.Clear();
		ConquestNode[] componentsInChildren = GetComponentsInChildren<ConquestNode>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			for (int j = i; j < componentsInChildren.Length; j++)
			{
				if (Vector3.Distance(componentsInChildren[i].transform.position, componentsInChildren[j].transform.position) < connectionRangeThreshold && !ConnectionExists(componentsInChildren[i], componentsInChildren[j]))
				{
					ConquestNodeConnection conquestNodeConnection = new ConquestNodeConnection();
					conquestNodeConnection.node1 = componentsInChildren[i];
					conquestNodeConnection.node2 = componentsInChildren[j];
					currentNodeConnections.Add(conquestNodeConnection);
				}
			}
		}
	}

	private bool ConnectionExists(ConquestNode node1, ConquestNode node2)
	{
		for (int i = 0; i < currentNodeConnections.Count; i++)
		{
			if (currentNodeConnections[i].node1 == node1 && currentNodeConnections[i].node2 == node2)
			{
				return true;
			}
			if (currentNodeConnections[i].node1 == node2 && currentNodeConnections[i].node2 == node1)
			{
				return true;
			}
		}
		return false;
	}

	private void OnNodeConnectionWasUpdated()
	{
		ClearNodeConnectionLines();
		DrawNodeConnectionLines();
	}

	private void ClearNodeConnectionLines()
	{
		for (int i = 0; i < lineRenders.Count; i++)
		{
			Object.Destroy(lineRenders[i].gameObject);
		}
		lineRenders.Clear();
	}

	private void DrawNodeConnectionLines()
	{
		linePrefab.SetActive(value: true);
		for (int i = 0; i < currentNodeConnections.Count; i++)
		{
			LineRenderer component = Object.Instantiate(linePrefab).GetComponent<LineRenderer>();
			component.SetPosition(0, currentNodeConnections[i].node1.transform.position);
			component.SetPosition(1, currentNodeConnections[i].node2.transform.position);
			lineRenders.Add(component);
		}
		linePrefab.SetActive(value: false);
	}
}
