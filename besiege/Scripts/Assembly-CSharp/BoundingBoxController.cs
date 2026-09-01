using System.Collections;
using System.Linq;
using UnityEngine;

public class BoundingBoxController : MonoBehaviour
{
	public Transform floorPos;

	public Transform roofPos;

	public Transform frontPos;

	public Transform backPos;

	public Transform leftPos;

	public Transform rightPos;

	public Machine machine;

	public float lerpOutSpeed = 0.15f;

	public float lerpInSpeed = 0.8f;

	public Color startColor;

	public Color endColor;

	public AddPiece addPiece;

	public Collider[] colliders;

	public AudioClip noiseWhooshSound;

	public AudioClip rewindSound;

	public Renderer[] rendysToFade;

	public float boundingBoxOffFloorHeight;

	public AudioSource aSource;

	[HideInInspector]
	public bool playFadeAudio = true;

	protected Renderer floorRenderer;

	protected Renderer roofRenderer;

	protected Renderer frontRenderer;

	protected Renderer backRenderer;

	protected Renderer leftRenderer;

	protected Renderer rightRenderer;

	protected bool boundsVisible = true;

	private Color delegateColor;

	private Renderer myRendy;

	protected Material[] defaultMaterials;

	protected Material[] waterMaterials;

	protected Transform waterQuad;

	protected Color defaultStartColor;

	protected Color waterStartColor = new Color(0.263f, 0.357f, 0.439f, 0.4f);

	private IEnumerator fadeOutIEnumerator;

	private IEnumerator fadeInIEnumerator;

	public static bool lastAboveWater = true;

	public static bool isAboveWater = true;

	private bool hasCreatedWaterMats;

	private bool isOffset;

	private bool isRegular = true;

	public static void ResetState()
	{
		lastAboveWater = isAboveWater;
		lastAboveWater = true;
	}

	protected virtual void Awake()
	{
		myRendy = GetComponent<Renderer>();
		aSource = GetComponent<AudioSource>();
		startColor = (defaultStartColor = myRendy.sharedMaterial.GetColor("_TintColor"));
		floorRenderer = floorPos.GetComponent<Renderer>();
		roofRenderer = roofPos.GetComponent<Renderer>();
		rightRenderer = rightPos.GetComponent<Renderer>();
		leftRenderer = leftPos.GetComponent<Renderer>();
		frontRenderer = frontPos.GetComponent<Renderer>();
		backRenderer = backPos.GetComponent<Renderer>();
		boundingBoxOffFloorHeight = LevelAttributes.FloorHeight;
		addPiece = SingleInstanceFindOnly<AddPiece>.Instance;
	}

	public virtual void Init()
	{
		if (machine == null)
		{
			Debug.Log("Machine is not set in BoundingBoxController!");
		}
		if (!StatMaster.isMP && Object.FindObjectOfType<WaterController>() != null)
		{
			SetToWaterVariant();
		}
		else
		{
			lastAboveWater = isAboveWater;
			isAboveWater = true;
		}
		EnableBounds(machine);
	}

	private void CreateWaterMaterials()
	{
		hasCreatedWaterMats = true;
		defaultMaterials = new Material[3];
		waterMaterials = new Material[3];
		defaultMaterials[0] = myRendy.sharedMaterial;
		defaultMaterials[1] = floorRenderer.sharedMaterial;
		defaultMaterials[2] = rendysToFade[0].sharedMaterial;
		waterStartColor = new Color(0.263f, 0.357f, 0.439f, 0.4f);
		Shader shader = ReferenceMaster.Instance.waterBoundingBox ?? Shader.Find("Particles/Alpha Blended Back Faces (On top of Water)");
		waterMaterials[0] = new Material(defaultMaterials[0]);
		waterMaterials[0].shader = shader;
		waterMaterials[0].renderQueue = 3002;
		waterMaterials[0].SetColor("_TintColor", waterStartColor);
		waterMaterials[0].SetFloat("_UnderAlpha", 0.25f);
		shader = ReferenceMaster.Instance.waterAlphaBlend ?? Shader.Find("Particles/Alpha Blended (On top of Water)");
		waterMaterials[1] = new Material(defaultMaterials[1]);
		waterMaterials[1].shader = shader;
		waterMaterials[1].renderQueue = 3002;
		waterMaterials[1].SetFloat("_UnderAlpha", 0.75f);
		waterMaterials[2] = new Material(defaultMaterials[2]);
		waterMaterials[2].shader = shader;
		waterMaterials[2].renderQueue = 3002;
		waterMaterials[2].SetColor("_TintColor", waterStartColor);
		waterMaterials[2].SetFloat("_UnderAlpha", 0.66f);
		waterQuad = CreateQuad();
		waterQuad.parent = base.transform;
	}

	protected virtual void SetToWaterVariant()
	{
		isRegular = false;
		if (!hasCreatedWaterMats)
		{
			CreateWaterMaterials();
		}
		startColor = waterStartColor;
		SetMaterials(waterMaterials);
		base.transform.localScale = new Vector3(18f, 14f, 18f);
		if (!isOffset)
		{
			isOffset = true;
			base.transform.localPosition -= new Vector3(0f, 2f, 0f);
		}
		SetWaterHeight();
	}

	protected virtual void SetToDefaultVariant()
	{
		isRegular = true;
		lastAboveWater = isAboveWater;
		isAboveWater = true;
		startColor = defaultStartColor;
		if (hasCreatedWaterMats)
		{
			SetMaterials(defaultMaterials);
			waterQuad.gameObject.SetActive(false);
			base.transform.localScale = new Vector3(18f, 10.144f, 18f);
			if (isOffset)
			{
				isOffset = false;
				base.transform.localPosition += new Vector3(0f, 2f, 0f);
			}
			for (int i = 0; i < rendysToFade.Length; i++)
			{
				Transform transform = rendysToFade[i].transform;
				transform.localPosition = new Vector3(transform.localPosition.x, -0.5f, transform.localPosition.z);
			}
		}
	}

	protected void SetWaterHeight()
	{
		if (!isRegular)
		{
			lastAboveWater = isAboveWater;
			Vector3 position = base.transform.position;
			position.y = ((!WaterController.Exist) ? 0f : WaterController.waterTransformHeight);
			position = base.transform.InverseTransformPoint(position);
			isAboveWater = position.y < 0f;
			bool flag = position.y < 0.4f && position.y > -0.4f;
			float y = ((!flag) ? 0f : position.y);
			if (!StatMaster.isMP)
			{
				y = ((!isAboveWater) ? 0f : (-3f)) / base.transform.lossyScale.y;
			}
			if (hasCreatedWaterMats)
			{
				waterQuad.gameObject.SetActive(flag);
			}
			for (int i = 0; i < rendysToFade.Length; i++)
			{
				Transform transform = rendysToFade[i].transform;
				transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
			}
		}
	}

	protected virtual void SetMaterials(Material[] m)
	{
		myRendy.material = m[0];
		floorRenderer.sharedMaterial = m[1];
		roofRenderer.sharedMaterial = m[1];
		rightRenderer.sharedMaterial = m[1];
		leftRenderer.sharedMaterial = m[1];
		frontRenderer.sharedMaterial = m[1];
		backRenderer.sharedMaterial = m[1];
		for (int i = 0; i < rendysToFade.Length - 1; i++)
		{
			rendysToFade[i].material = m[2];
		}
		SetColor("_TintColor", startColor);
	}

	public void ResetRenders()
	{
		leftRenderer.enabled = false;
		rightRenderer.enabled = false;
		floorRenderer.enabled = false;
		roofRenderer.enabled = false;
		backRenderer.enabled = false;
		frontRenderer.enabled = false;
	}

	public virtual bool BoundCheck(Bounds bounds)
	{
		if (bounds.size == Vector3.zero)
		{
			ResetRenders();
			return false;
		}
		StatMaster.Bounding.inLeftWall = bounds.min.x < StatMaster.Bounding.leftPos;
		StatMaster.Bounding.inRightWall = bounds.max.x > StatMaster.Bounding.rightPos;
		StatMaster.Bounding.inGround = bounds.min.y < StatMaster.Bounding.floorPos - 0.035f;
		StatMaster.Bounding.inRoof = bounds.max.y > StatMaster.Bounding.roofHeight;
		StatMaster.Bounding.inBackWall = bounds.min.z < StatMaster.Bounding.backPos;
		StatMaster.Bounding.inFrontWall = bounds.max.z > StatMaster.Bounding.frontPos;
		UpdateVis();
		bool flag = StatMaster.Bounding.inRoof || StatMaster.Bounding.inGround || StatMaster.Bounding.inRightWall || StatMaster.Bounding.inLeftWall || StatMaster.Bounding.inFrontWall || StatMaster.Bounding.inBackWall;
		addPiece.SetOutOfBounds(flag);
		if (flag)
		{
			TutorialSystem.StartCustomTutorial("TutorialContainerGrounding");
		}
		return flag;
	}

	public virtual void UpdateVis()
	{
		bool flag = StatMaster.Bounding.Enabled;
		leftRenderer.enabled = flag && StatMaster.Bounding.inLeftWall;
		rightRenderer.enabled = flag && StatMaster.Bounding.inRightWall;
		floorRenderer.enabled = flag && StatMaster.Bounding.inGround;
		roofRenderer.enabled = flag && StatMaster.Bounding.inRoof;
		backRenderer.enabled = flag && StatMaster.Bounding.inBackWall;
		frontRenderer.enabled = flag && StatMaster.Bounding.inFrontWall;
	}

	public virtual bool Check(Machine machine, bool renewBounds)
	{
		if (machine.BlockCount == 0)
		{
			ResetRenders();
			return false;
		}
		return BoundCheck(machine.GetBounds(renewBounds));
	}

	public void ToggleBoundsVisual(bool toggle)
	{
		if (boundsVisible == toggle)
		{
			return;
		}
		if (toggle)
		{
			if (fadeOutIEnumerator != null)
			{
				StopCoroutine(fadeOutIEnumerator);
			}
			fadeInIEnumerator = FadeIn();
			StartCoroutine(fadeInIEnumerator);
		}
		else
		{
			if (fadeInIEnumerator != null)
			{
				StopCoroutine(fadeInIEnumerator);
			}
			fadeOutIEnumerator = FadeOut();
			StartCoroutine(fadeOutIEnumerator);
		}
		UpdateVis();
		boundsVisible = toggle;
	}

	public void EnableBounds(Machine machine)
	{
		StatMaster.Bounding.Enabled = true;
		ToggleBoundsVisual(true);
		SetColliders(true);
		SetFloorPos(true);
		Check(machine, false);
	}

	public void DisableBounds(Machine machine)
	{
		StatMaster.Bounding.Enabled = false;
		roofRenderer.enabled = (StatMaster.Bounding.inRoof = false);
		floorRenderer.enabled = (StatMaster.Bounding.inGround = false);
		rightRenderer.enabled = (StatMaster.Bounding.inRightWall = false);
		leftRenderer.enabled = (StatMaster.Bounding.inLeftWall = false);
		frontRenderer.enabled = (StatMaster.Bounding.inFrontWall = false);
		backRenderer.enabled = (StatMaster.Bounding.inBackWall = false);
		ToggleBoundsVisual(false);
		SetColliders(false);
		SetFloorPos(false);
		Check(machine, false);
	}

	private void SetColliders(bool toggle)
	{
		for (int i = 0; i < colliders.Length; i++)
		{
			Collider collider = colliders[i];
			if (collider != null)
			{
				collider.enabled = toggle;
			}
		}
	}

	public virtual void SetFloorPos(bool toggle)
	{
		if (toggle)
		{
			StatMaster.Bounding.floorPos = floorPos.position.y;
			StatMaster.Bounding.roofHeight = roofPos.position.y;
			StatMaster.Bounding.frontPos = frontPos.position.z;
			StatMaster.Bounding.backPos = backPos.position.z;
			StatMaster.Bounding.leftPos = leftPos.position.x;
			StatMaster.Bounding.rightPos = rightPos.position.x;
		}
		else
		{
			StatMaster.Bounding.floorPos = ((!StatMaster.isMP) ? (SingleInstanceFindOnly<AddPiece>.Instance.floorHeight + floorPos.position.y) : boundingBoxOffFloorHeight);
			float num = (StatMaster.Bounding.frontPos = (StatMaster.Bounding.roofHeight = 10000f));
			StatMaster.Bounding.backPos = 0f - num;
			StatMaster.Bounding.leftPos = 0f - num;
			StatMaster.Bounding.rightPos = num;
		}
	}

	public virtual void FadeVis()
	{
		if (aSource.enabled && playFadeAudio)
		{
			aSource.PlayOneShot((!machine.isSimulating) ? rewindSound : noiseWhooshSound);
		}
		if (machine.boundingBoxController.boundsVisible)
		{
			if (fadeInIEnumerator != null)
			{
				StopCoroutine(fadeInIEnumerator);
			}
			if (fadeOutIEnumerator != null)
			{
				StopCoroutine(fadeOutIEnumerator);
			}
			if (machine.isSimulating)
			{
				fadeOutIEnumerator = FadeOut();
				StartCoroutine(fadeOutIEnumerator);
			}
			else
			{
				fadeInIEnumerator = FadeIn();
				StartCoroutine(fadeInIEnumerator);
			}
		}
		else
		{
			ToggleBoundsVisual(false);
		}
	}

	private IEnumerator FadeOut()
	{
		float cTime = 0f;
		float rate = 1f / lerpOutSpeed;
		float startAlpha = myRendy.sharedMaterial.GetColor("_TintColor").a;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			delegateColor = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startAlpha, endColor.a, cTime));
			SetColor("_TintColor", delegateColor);
			yield return null;
		}
		myRendy.enabled = false;
	}

	private IEnumerator FadeIn()
	{
		myRendy.enabled = true;
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		float startAlpha = myRendy.sharedMaterial.GetColor("_TintColor").a;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			delegateColor = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startAlpha, startColor.a, cTime));
			SetColor("_TintColor", delegateColor);
			yield return null;
		}
	}

	protected void SetColor(string s, Color c)
	{
		myRendy.material.SetColor(s, c);
		for (int i = 0; i < rendysToFade.Length; i++)
		{
			rendysToFade[i].material.SetColor(s, c);
		}
	}

	private Transform CreateQuad()
	{
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
		Renderer component = gameObject.GetComponent<MeshRenderer>();
		component.sharedMaterial = waterMaterials[0];
		component.material.SetFloat("_UnderAlpha", 0.66f);
		Object.Destroy(gameObject.GetComponent<MeshCollider>());
		rendysToFade = rendysToFade.Concat(new Renderer[1] { component }).ToArray();
		Transform transform = gameObject.transform;
		transform.position = floorPos.position;
		transform.Rotate(270f, 0f, 0f);
		transform.localScale = new Vector3(base.transform.localScale.x, base.transform.localScale.z, base.transform.localScale.y);
		transform.parent = base.transform.parent;
		return transform;
	}
}
