using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("UI/GuideBook")]
public class GuideBook : MonoBehaviour
{
	[Serializable]
	public class Category
	{
		[Serializable]
		public class Tutorial
		{
			public UIButton button;

			public MeshRenderer buttonRenderer;

			public GameObject content;

			public GameObject[] pages;

			[HideInInspector]
			public int currentPage;

			public void Setup()
			{
				if ((bool)button)
				{
					button.ResetDelegates();
					button.Down += SetCurrentTut;
				}
			}

			public void SetCurrentTut()
			{
				Instance.selectedCategory.CurrentTut.currentPage = 0;
				Instance.selectedCategory.CurrentTut = this;
				currentPage = 0;
				for (int i = 0; i < Instance.selectedCategory.tutorials.Length; i++)
				{
					Instance.selectedCategory.tutorials[i].buttonRenderer.material.color = new Color(1f, 1f, 1f, 0.62f);
				}
				buttonRenderer.material.color = Color.white;
				Instance.selectedCategory.UpdateTuts();
			}
		}

		public int index;

		public UIButtonMultiState button;

		public GameObject content;

		public Tutorial[] tutorials;

		private Tutorial _currentTut;

		public Tutorial CurrentTut
		{
			get
			{
				if (_currentTut == null)
				{
					_currentTut = tutorials[0];
				}
				return _currentTut;
			}
			set
			{
				_currentTut = value;
			}
		}

		public void SetupTutorials()
		{
			for (int i = 0; i < tutorials.Length; i++)
			{
				tutorials[i].Setup();
			}
		}

		public void Set()
		{
			if (Instance.selectedCategory != null)
			{
				Instance.selectedCategory.CurrentTut.currentPage = 0;
			}
			Instance.HideAllContent();
			Instance.selectedCategory = this;
			content.SetActive(true);
			UpdateTuts();
		}

		public void UpdateTuts()
		{
			for (int i = 0; i < tutorials.Length; i++)
			{
				tutorials[i].content.SetActive(false);
			}
			CurrentTut.content.SetActive(true);
			SetPages();
		}

		public void SetPages()
		{
			Tutorial currentTut = CurrentTut;
			ReferenceMaster.SetDynamicText(Instance.pageCount, currentTut.currentPage + 1 + "/" + currentTut.pages.Length);
			for (int i = 0; i < currentTut.pages.Length; i++)
			{
				currentTut.pages[i].SetActive(i == currentTut.currentPage);
			}
		}
	}

	public class UIRect
	{
		public Transform upperLeft;

		public Transform lowerRight;
	}

	public static GuideBook Instance;

	public static bool display;

	public static bool open;

	public BlurCamTest blurArea;

	public Action OnBookOpened;

	public Action OnBookClosed;

	[SerializeField]
	protected Category camera;

	[SerializeField]
	protected Category building;

	[SerializeField]
	protected Category steering;

	[SerializeField]
	protected Category flying;

	[SerializeField]
	protected GameObject[] lines;

	[SerializeField]
	protected UIButton nextPage;

	[SerializeField]
	protected UIButton prevPage;

	[SerializeField]
	protected DynamicText pageCount;

	public UIButton bookButton;

	public UIButton openButton;

	public UIButton closeButton;

	[SerializeField]
	protected GameObject expanded;

	[SerializeField]
	protected GameObject collapsed;

	[SerializeField]
	protected Transform background;

	[SerializeField]
	protected Transform collapsedBG;

	[SerializeField]
	protected GameObject tutorialCollapse;

	[SerializeField]
	protected UIButton openTutorial;

	[NonSerialized]
	public Category selectedCategory;

	private bool categoryChosen;

	private MinimiseTutorial tutorialController;

	public UIRect moveArea = new UIRect();

	public Camera hudCam;

	public Transform dragBG;

	protected Vector3 dragMouseOffset = Vector3.zero;

	private float expandHeld;

	private static Vector3 defPos;

	private bool isSim;

	private Vector3 startPos;

	public static bool isSP
	{
		get
		{
			switch (SceneManager.GetActiveScene().name)
			{
			case "INITIALISER":
			case "TITLE SCREEN":
			case "LevelSelect":
			case "LevelSelect2":
			case "LevelSelect3":
			case "LevelSelect4":
			case "LevelSelectWater":
			case "MasterSceneMultiplayer":
				return false;
			default:
				return true;
			}
		}
	}

	protected static bool IsSimulating
	{
		get
		{
			return Machine.Active() != null && Machine.Active().isSimulating;
		}
	}

	private void Awake()
	{
		defPos = base.transform.position;
		SetupBlurArea();
		if (!moveArea.upperLeft)
		{
			moveArea.upperLeft = GameObject.FindWithTag("upperLeft").transform;
		}
		if (!moveArea.lowerRight)
		{
			moveArea.lowerRight = GameObject.FindWithTag("lowerRight").transform;
		}
		if (hudCam == null)
		{
			hudCam = GameObject.FindWithTag("hudCamera").GetComponent<Camera>();
		}
		Instance = this;
		camera.SetupTutorials();
		building.SetupTutorials();
		steering.SetupTutorials();
		flying.SetupTutorials();
		AssignButtonWithDrag(camera.button, SetCamera);
		AssignButtonWithDrag(building.button, SetBuilding);
		AssignButtonWithDrag(steering.button, SetSteering);
		AssignButtonWithDrag(flying.button, SetFlying);
		closeButton.Down += Collapse;
		openTutorial.Down += OpenTutorial;
		AssignButtonWithDrag(openButton, Expand);
		AssignButtonWithDrag(bookButton, Collapse);
		nextPage.Down += IncreasePage;
		prevPage.Down += DecreasePage;
		if (!categoryChosen)
		{
			SetCamera();
		}
		SetAllDynamicTextToHudCam();
		Display(OptionsMaster.BesiegeConfig.GuideBookShown);
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(ToggleSim));
	}

	public void OnDestroy()
	{
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(ToggleSim));
	}

	public static void Display(bool d, bool resetPos = true, bool bypassSimCheck = false)
	{
		if (isSP && (bypassSimCheck || !IsSimulating))
		{
			display = d;
			Instance.expanded.SetActive(false);
			Instance.collapsed.SetActive(d);
			open = false;
			if (!d)
			{
				Instance.UpdateBlur(null);
			}
			else
			{
				Instance.UpdateBlur(Instance.collapsedBG);
			}
			if (resetPos)
			{
				Instance.transform.position = defPos;
			}
		}
	}

	public static void Open()
	{
		if (isSP && !IsSimulating)
		{
			Instance.Expand();
			Instance.transform.position = defPos;
		}
	}

	public static void SetPage(string category, int page)
	{
		if (isSP)
		{
			switch (category)
			{
			default:
				return;
			case "Camera":
				Instance.SetCamera();
				break;
			case "Building":
				Instance.SetBuilding();
				break;
			case "Steering":
				Instance.SetSteering();
				break;
			case "Flying":
				Instance.SetFlying();
				break;
			}
			Instance.categoryChosen = true;
			if (page >= Instance.selectedCategory.CurrentTut.pages.Length)
			{
				page = 0;
			}
			Instance.selectedCategory.CurrentTut.currentPage = page;
			Instance.selectedCategory.SetPages();
		}
	}

	public static void SetTutorialCollapser(MinimiseTutorial tutorialController)
	{
		Instance.tutorialController = tutorialController;
	}

	public static void ShowTutorialCollapse(bool a)
	{
		if (isSP)
		{
			Instance.tutorialCollapse.SetActive(a);
		}
	}

	public void ToggleSim(bool simulating)
	{
		if (simulating != isSim)
		{
			isSim = simulating;
			base.gameObject.SetActive(!isSim);
			if (isSim)
			{
				Instance.UpdateBlur(null);
			}
			else
			{
				Instance.UpdateBlur((!open) ? collapsedBG : background);
			}
		}
	}

	public void OpenTutorial()
	{
		if (tutorialController != null)
		{
			tutorialController.openWindow.gameObject.SetActive(true);
		}
		ShowTutorialCollapse(false);
	}

	public void AssignButtonWithDrag(UIButton button, Click method)
	{
		button.ResetDelegates();
		button.Down += SetupDragUI;
		button.Held += DragUIWait;
		button.Released += FinishDragUI;
		button.Click += method;
	}

	protected void SetCamera()
	{
		camera.Set();
		UpdateState();
	}

	protected void SetBuilding()
	{
		building.Set();
		UpdateState();
	}

	protected void SetSteering()
	{
		steering.Set();
		UpdateState();
	}

	protected void SetFlying()
	{
		flying.Set();
		UpdateState();
	}

	protected void UpdateState()
	{
		camera.button.SetToState(1);
		building.button.SetToState(1);
		steering.button.SetToState(1);
		flying.button.SetToState(1);
		selectedCategory.button.SetToState(0);
		int index = selectedCategory.index;
		for (int i = 0; i < lines.Length; i++)
		{
			lines[i].SetActive(i != index && i != index + 1);
		}
	}

	public void HideAllContent()
	{
		camera.content.SetActive(false);
		building.content.SetActive(false);
		steering.content.SetActive(false);
		flying.content.SetActive(false);
	}

	protected void IncreasePage()
	{
		selectedCategory.CurrentTut.currentPage++;
		if (selectedCategory.CurrentTut.currentPage >= selectedCategory.CurrentTut.pages.Length)
		{
			selectedCategory.CurrentTut.currentPage = 0;
		}
		selectedCategory.SetPages();
	}

	protected void DecreasePage()
	{
		selectedCategory.CurrentTut.currentPage--;
		if (selectedCategory.CurrentTut.currentPage < 0)
		{
			selectedCategory.CurrentTut.currentPage = selectedCategory.CurrentTut.pages.Length - 1;
		}
		selectedCategory.SetPages();
	}

	protected void Expand()
	{
		expanded.SetActive(true);
		collapsed.SetActive(false);
		UpdateBlur(background);
		open = true;
		display = true;
		Vector3 position = base.transform.position;
		if (position.x > 5.62f)
		{
			base.transform.position = new Vector3(5.61f, position.y, position.z);
		}
		if (OnBookOpened != null)
		{
			OnBookOpened();
		}
	}

	protected void Collapse()
	{
		expanded.SetActive(false);
		collapsed.SetActive(true);
		UpdateBlur(collapsedBG);
		open = false;
		display = true;
		if (OnBookClosed != null)
		{
			OnBookClosed();
		}
	}

	private void SetupDragUI()
	{
		expandHeld = 0f;
		Vector3 position = (startPos = Input.mousePosition);
		Vector3 vector = hudCam.ScreenToWorldPoint(position);
		position = new Vector3(vector.x, vector.y, base.transform.position.z);
		dragMouseOffset = new Vector3(position.x, position.y, 0f) - base.transform.position;
	}

	private void DragUIWait()
	{
		expandHeld += Time.unscaledDeltaTime;
		if (expandHeld > 0.25f || Vector3.Distance(Input.mousePosition, startPos) > 20f)
		{
			expandHeld = 1f;
			DragUI();
		}
	}

	private void DragUI()
	{
		Vector2 vector = Input.mousePosition;
		Vector3 vector2 = hudCam.ScreenToWorldPoint(vector);
		vector = new Vector3(vector2.x, vector2.y, base.transform.position.z);
		Vector3 pos = new Vector3(vector.x, vector.y, 0f) - dragMouseOffset;
		pos = ClampInMoveArea(pos);
		base.transform.position = pos;
	}

	private void FinishDragUI()
	{
		expandHeld = 0f;
	}

	public Vector3 ClampInMoveArea(Vector3 pos)
	{
		return new Vector3(Mathf.Clamp(pos.x, moveArea.upperLeft.position.x + 3.45f, moveArea.lowerRight.position.x + 2.45f), Mathf.Clamp(pos.y, moveArea.lowerRight.position.y + dragBG.lossyScale.y, moveArea.upperLeft.position.y), pos.z);
	}

	protected void UpdateBlur(Transform t)
	{
		blurArea.target = t;
	}

	private void SetupBlurArea()
	{
		GameObject gameObject = GameObject.Find("Blur Camera Level Editor");
		if (gameObject != null)
		{
			blurArea = gameObject.GetComponent<BlurCamTest>();
		}
	}

	protected void SetAllDynamicTextToHudCam()
	{
		DynamicText[] componentsInChildren = GetComponentsInChildren<DynamicText>(true);
		Camera component = GameObject.FindGameObjectWithTag("hudCamera").GetComponent<Camera>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].cam = component;
		}
	}
}
