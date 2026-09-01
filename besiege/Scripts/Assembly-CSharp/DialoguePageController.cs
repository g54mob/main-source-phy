using UnityEngine;

public class DialoguePageController : MonoBehaviour
{
	public Transform[] pages;

	public int activePage;

	public TextMesh pageNumberTextMesh;

	public void NextPage()
	{
		SelectPage(activePage + 1);
	}

	public void PrevPage()
	{
		SelectPage(activePage - 1);
	}

	private void SelectPage(int id)
	{
		if (id >= pages.Length)
		{
			id = pages.Length - 1;
		}
		else if (id < 0)
		{
			id = 0;
		}
		activePage = id;
		for (int i = 0; i < pages.Length; i++)
		{
			if (i == id)
			{
				pages[i].gameObject.SetActive(true);
			}
			else
			{
				pages[i].gameObject.SetActive(false);
			}
		}
		if (pageNumberTextMesh != null)
		{
			SetText();
		}
	}

	private void SetText()
	{
		pageNumberTextMesh.text = activePage + 1 + "/" + pages.Length;
	}
}
