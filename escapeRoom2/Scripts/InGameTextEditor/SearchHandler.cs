using InGameTextEditor;
using UnityEngine;
using UnityEngine.UI;

public class SearchHandler : MonoBehaviour
{
	public InGameTextEditor.TextEditor textEditor;

	public InputField inputField;

	private bool searchFieldActive;

	public void FindNext()
	{
		textEditor.Find(inputField.text, forward: true);
		ActivateSearchField();
	}

	public void FindPrevious()
	{
		textEditor.Find(inputField.text, forward: false);
		ActivateSearchField();
	}

	public void ActivateSearchField()
	{
		inputField.ActivateInputField();
		searchFieldActive = true;
		textEditor.EditorActive = false;
	}

	public void DeactivateSearchField()
	{
		searchFieldActive = false;
		inputField.DeactivateInputField();
	}

	private void Update()
	{
		if (searchFieldActive)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					FindPrevious();
				}
				else
				{
					FindNext();
				}
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				DeactivateSearchField();
				textEditor.EditorActive = true;
			}
		}
		bool flag = false;
		if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftMeta) || Input.GetKey(KeyCode.RightMeta))
		{
			flag = true;
		}
		if (flag && Input.GetKeyDown(KeyCode.F))
		{
			ActivateSearchField();
		}
	}
}
