using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorUIBuilder : MonoBehaviour
{
	public GameObject[] EnablePostMerge;

	public GameObject[] SinglePlayerOnly;

	public GameObject[] MultiverseOnly;

	public Transform[] HierarchyOrder;

	private void Start()
	{
		Object.Destroy(this);
	}

	public void PostMerge()
	{
		WinCondition winCondition = Object.FindObjectOfType<WinCondition>();
		for (int i = 0; i < EnablePostMerge.Length; i++)
		{
			EnablePostMerge[i].SetActive(true);
		}
		string text = SceneManager.GetActiveScene().name;
		bool flag = text.Contains("Multiplayer");
		for (int j = 0; j < SinglePlayerOnly.Length; j++)
		{
			if (!flag)
			{
				SinglePlayerOnly[j].SetActive(true);
			}
			else
			{
				Object.DestroyImmediate(SinglePlayerOnly[j]);
			}
		}
		for (int k = 0; k < MultiverseOnly.Length; k++)
		{
			if (flag)
			{
				if (MultiverseOnly[k] == null)
				{
					Debug.LogError("Missing object at index " + k);
				}
				else
				{
					MultiverseOnly[k].SetActive(true);
				}
			}
			else
			{
				Object.DestroyImmediate(MultiverseOnly[k]);
			}
		}
		if (!flag)
		{
			EntitySelectionTool entitySelectionTool = Object.FindObjectOfType<EntitySelectionTool>();
			if ((bool)entitySelectionTool)
			{
				Object.DestroyImmediate(entitySelectionTool);
			}
		}
		for (int l = 0; l < HierarchyOrder.Length; l++)
		{
			if (HierarchyOrder[l] != null)
			{
				HierarchyOrder[l].SetAsLastSibling();
			}
		}
		if (winCondition != null)
		{
			winCondition.transform.SetAsLastSibling();
		}
		else if (!flag)
		{
			Debug.LogError("No Level found");
		}
		WorldBoundsManager worldBoundsManager = Object.FindObjectOfType<WorldBoundsManager>();
		if ((bool)worldBoundsManager)
		{
			worldBoundsManager.SetAllBorders(!flag && BesiegeEntryPoint.IsSPLevel(text), flag);
		}
		Object.DestroyImmediate(base.gameObject);
	}
}
