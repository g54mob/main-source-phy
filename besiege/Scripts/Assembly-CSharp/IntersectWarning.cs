using System.Collections;
using Localisation;
using UnityEngine;

public class IntersectWarning : SingleInstanceFindOnly<IntersectWarning>
{
	public TextMesh text;

	public int defLocID = 848;

	public LerpAndFade lerpCode;

	public float warningDuration = 0.6f;

	private static Transform myTransform;

	private static float startZpos;

	private Camera hudCam;

	public override string Name
	{
		get
		{
			return "IntersectWarning";
		}
	}

	private void Start()
	{
		if (text == null)
		{
			text = base.gameObject.GetComponentInChildren<TextMesh>();
			LocalisationChild component = text.GetComponent<LocalisationChild>();
			if ((bool)component)
			{
				Object.Destroy(component);
			}
		}
		myTransform = base.transform;
		startZpos = myTransform.position.z;
	}

	public static void Warning()
	{
		WarningFromPos(InputManager.CursorPosition());
	}

	public static void WarningFromWorldPos(Vector3 pos, int locID = 0)
	{
		Vector2 vector = Camera.main.WorldToScreenPoint(pos);
		WarningFromPos(vector, locID);
	}

	public static void WarningFromPos(Vector3 pos, int locID = 0)
	{
		IntersectWarning intersectWarning = SingleInstanceFindOnly<IntersectWarning>.Instance;
		if (intersectWarning == null)
		{
			Debug.LogError("Intersect Warning, but instance is null!");
			return;
		}
		if (intersectWarning.hudCam == null)
		{
			intersectWarning.hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
		}
		if (intersectWarning.hudCam == null)
		{
			Debug.LogError("Intersect Warning, but hudCam is null!");
			return;
		}
		pos = intersectWarning.hudCam.ScreenToWorldPoint(pos);
		Warning(pos, locID);
	}

	public static void Warning(Vector3 pos, int locID = 0)
	{
		IntersectWarning intersectWarning = SingleInstanceFindOnly<IntersectWarning>.Instance;
		if (locID == 0)
		{
			locID = intersectWarning.defLocID;
		}
		intersectWarning.text.text = LocalisationManager.GetTranslation(locID);
		intersectWarning.StopAllCoroutines();
		intersectWarning.GetComponent<AudioSource>().Play();
		myTransform.position = new Vector3(pos.x, pos.y, startZpos);
		intersectWarning.StartCoroutine(intersectWarning.CoWarning());
	}

	private IEnumerator CoWarning()
	{
		lerpCode.LerpIn();
		yield return new WaitForSeconds(warningDuration);
		lerpCode.LerpOut();
	}
}
