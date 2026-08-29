using UnityEngine;

public class UnluckFlagGUI : MonoBehaviour
{
	public GameObject[] prefabs;

	public Material[] bgrs;

	public Light[] lights;

	public GameObject nextButton;

	public GameObject prevButton;

	public GameObject bgrButton;

	public GameObject lightButton;

	public GameObject texturePreview;

	private GameObject activeObj;

	private int counter;

	private int bCounter;

	private int lCounter;

	public TextMesh txt;

	public TextMesh debug;

	public void Start()
	{
		Swap();
		if (txt == null)
		{
			txt = base.transform.Find("txt").GetComponent<TextMesh>();
		}
		if (nextButton == null)
		{
			nextButton = base.transform.Find("nextButton").GetComponent<GameObject>();
		}
		if (prevButton == null)
		{
			prevButton = base.transform.Find("prevButton").GetComponent<GameObject>();
		}
		if (bgrButton == null)
		{
			bgrButton = base.transform.Find("bgrButton").GetComponent<GameObject>();
		}
		if (lightButton == null)
		{
			lightButton = base.transform.Find("lightButton").gameObject;
		}
		if (texturePreview == null)
		{
			texturePreview = base.transform.Find("texturePreview").GetComponent<GameObject>();
		}
		if (debug == null)
		{
			debug = base.transform.Find("debug").GetComponent<TextMesh>();
		}
	}

	public void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			ButtonUp();
		}
		if (Input.GetKeyUp("right"))
		{
			Next();
		}
		if (Input.GetKeyUp("left"))
		{
			Prev();
		}
		if (Input.GetKeyUp("space"))
		{
			nextButton.SetActive(!nextButton.activeInHierarchy);
			prevButton.SetActive(nextButton.activeInHierarchy);
			bgrButton.SetActive(nextButton.activeInHierarchy);
			texturePreview.SetActive(nextButton.activeInHierarchy);
			txt.gameObject.SetActive(nextButton.activeInHierarchy);
			lightButton.gameObject.SetActive(nextButton.activeInHierarchy);
		}
	}

	public void ButtonUp()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(ray, out hitInfo))
		{
			if (hitInfo.transform.gameObject == nextButton)
			{
				Next();
			}
			else if (hitInfo.transform.gameObject == prevButton)
			{
				Prev();
			}
			else if (hitInfo.transform.gameObject == bgrButton)
			{
				NextBgr();
			}
			else if (hitInfo.transform.gameObject == lightButton)
			{
				LightChange();
			}
		}
	}

	public void LightChange()
	{
		if (lights.Length != 0)
		{
			lights[lCounter].enabled = false;
			lCounter++;
			if (lCounter >= lights.Length)
			{
				lCounter = 0;
			}
			lights[lCounter].enabled = true;
		}
	}

	public void NextBgr()
	{
		if (bgrs.Length != 0)
		{
			bCounter++;
			if (bCounter >= bgrs.Length)
			{
				bCounter = 0;
			}
			RenderSettings.skybox = bgrs[bCounter];
		}
	}

	public void Next()
	{
		counter++;
		if (counter > prefabs.Length - 1)
		{
			counter = 0;
		}
		Swap();
	}

	public void Prev()
	{
		counter--;
		if (counter < 0)
		{
			counter = prefabs.Length - 1;
		}
		Swap();
	}

	public void Swap()
	{
		if (prefabs.Length != 0)
		{
			Object.Destroy(activeObj);
			GameObject gameObject = Object.Instantiate(prefabs[counter]);
			activeObj = gameObject;
			if (txt != null)
			{
				txt.text = activeObj.name;
				txt.text = txt.text.Replace("(Clone)", "");
				txt.text = txt.text + " " + activeObj.GetComponent<UnluckAnimatedMesh>().meshContainerFBX.name;
				txt.text = txt.text.Replace("_", " ");
				txt.text = txt.text.Replace("Flag ", "");
			}
			if (texturePreview != null)
			{
				texturePreview.GetComponent<Renderer>().sharedMaterial = activeObj.GetComponent<Renderer>().sharedMaterial;
			}
		}
	}
}
