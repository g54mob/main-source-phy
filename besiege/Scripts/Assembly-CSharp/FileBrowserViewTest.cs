using System.Collections.Generic;
using UnityEngine;

public class FileBrowserViewTest : MonoBehaviour
{
	[SerializeField]
	private FileBrowserView pageView;

	[SerializeField]
	private int amountOfFiles = 50;

	[SerializeField]
	private FileBrowserType fileBrowserType;

	[SerializeField]
	private bool useTestData;

	[SerializeField]
	private bool isSaveMenu;

	private void Start()
	{
		Invoke("OpenWindow", 1.5f);
	}

	private void OpenWindow()
	{
		if (useTestData)
		{
			Generate();
		}
		else
		{
			pageView.Open(fileBrowserType, isSaveMenu);
		}
	}

	private void Generate()
	{
		pageView.Open(fileBrowserType, isSaveMenu);
		VirtualFolder existingFolder = pageView.Controller.Collection.CurrentFolder;
		List<IVirtualObject> list = FileBrowserPageViewTest.GenerateVirtualFiles(fileBrowserType, amountOfFiles);
		list.ForEach(delegate(IVirtualObject x)
		{
			existingFolder.AddObject(x);
		});
	}
}
