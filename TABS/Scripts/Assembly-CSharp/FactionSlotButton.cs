using UnityEngine;
using UnityEngine.UI;

public class FactionSlotButton : MonoBehaviour
{
	public bool isDragged;

	public FactionSlotButton attachedTo;

	public FactionSlotButton snapTarget;

	public RectTransform rect;

	public Image thumbnail;

	[HideInInspector]
	public Image rootImage;

	[HideInInspector]
	public Vector3 buttonOffsetVelocity;

	private Vector3 buttonOffset;

	private Vector3 currentWigglePos;

	private Vector3 wigglePosTarget;

	private Vector3 wigglePosVelocity;

	private LineRenderer line;

	public bool isSlot;

	public bool isSlotted;

	private float widthVelocity;

	private Vector2 buttonVelocty;

	private Vector3 buttonVeloctyTargetOffset;

	private Vector3 rotationVelocity;

	private float gravity;

	public void Start()
	{
		rootImage = GetComponentInChildren<Image>();
		rect = GetComponent<RectTransform>();
		line = GetComponentInChildren<LineRenderer>();
		if ((bool)line)
		{
			currentWigglePos = base.transform.position;
			for (int i = 0; i < line.positionCount; i++)
			{
				line.SetPosition(i, base.transform.position);
			}
		}
	}

	public void LateUpdate()
	{
		if (!rootImage || isSlot)
		{
			return;
		}
		DoOffset();
		if (!isDragged)
		{
			DoVelcity();
		}
		if ((bool)snapTarget)
		{
			line.SetPosition(0, base.transform.GetChild(0).position + Vector3.forward * 0.5f);
			line.SetPosition(line.positionCount - 1, snapTarget.transform.GetChild(0).position + Vector3.forward * 0.3f);
			wigglePosTarget = (line.GetPosition(0) + line.GetPosition(line.positionCount - 1)) / 2f;
			wigglePosVelocity += (wigglePosTarget - currentWigglePos) * 500f * Time.deltaTime;
			wigglePosVelocity -= wigglePosVelocity * Time.deltaTime * 15f;
			wigglePosVelocity.z = 0f;
			wigglePosVelocity += Random.onUnitSphere * Mathf.Clamp(Vector3.Distance(base.transform.position, snapTarget.transform.position) - 25f, 0f, 5f) * 0.5f;
			currentWigglePos += wigglePosVelocity * Time.deltaTime;
			currentWigglePos.z = line.GetPosition(0).z;
			for (int i = 1; i < line.positionCount - 1; i++)
			{
				line.SetPosition(i, BezierCurve.QuadraticBezier(line.GetPosition(0), currentWigglePos, line.GetPosition(line.positionCount - 1), (float)i / (float)line.positionCount));
			}
			float num = Mathf.Clamp(1.5f - Vector3.Distance(base.transform.position, snapTarget.transform.position) * 0.05f, 0.25f, 1f) * 1f;
			widthVelocity += (num - line.widthMultiplier) * Time.deltaTime * 500f;
			widthVelocity -= widthVelocity * Time.deltaTime * 15f;
			line.widthMultiplier += widthVelocity * Time.deltaTime;
		}
		else
		{
			for (int j = 0; j < line.positionCount; j++)
			{
				line.SetPosition(j, base.transform.position + Vector3.forward * 0.5f);
				currentWigglePos = base.transform.position + Vector3.forward * 0.5f;
				wigglePosVelocity = Vector3.zero;
			}
		}
	}

	private void DoOffset()
	{
		if ((bool)snapTarget && (bool)snapTarget.rootImage && Vector3.Distance(base.transform.position, snapTarget.transform.position) > 0f)
		{
			buttonOffsetVelocity += (snapTarget.rootImage.transform.position - rootImage.transform.position) * Time.deltaTime * 500f;
			snapTarget.buttonOffsetVelocity -= (snapTarget.rootImage.transform.position - rootImage.transform.position) * Time.deltaTime * 500f;
		}
		buttonOffsetVelocity += -buttonOffset * Time.deltaTime * 1000f;
		buttonOffsetVelocity += -buttonOffsetVelocity * Time.deltaTime * 30f;
		buttonOffset += buttonOffsetVelocity * Time.deltaTime;
		rootImage.transform.localPosition = buttonOffset;
	}

	public void Throw(Vector3 deltaPos)
	{
		if ((bool)snapTarget)
		{
			buttonVeloctyTargetOffset = base.transform.position + deltaPos * 0.1f - snapTarget.transform.position;
		}
		buttonVelocty = new Vector2(deltaPos.x, deltaPos.y);
		gravity = 1f;
		isDragged = false;
	}

	public void PickUp()
	{
		isDragged = true;
	}

	private void DoVelcity()
	{
		if ((bool)snapTarget)
		{
			buttonVelocty -= buttonVelocty * Time.deltaTime * 40f;
			buttonVelocty += (Vector2)(buttonVeloctyTargetOffset + snapTarget.transform.position - base.transform.position) * Time.deltaTime * 100f;
		}
		else
		{
			gravity += Time.deltaTime * 5f * gravity;
			buttonVelocty -= Vector2.up * Time.deltaTime * 60f * gravity;
		}
		buttonVelocty = Vector2.ClampMagnitude(buttonVelocty, 100f);
		if (base.transform.position.y < 0f)
		{
			Object.Destroy(base.gameObject);
		}
		buttonVelocty -= buttonVelocty * buttonVelocty.magnitude * Time.deltaTime * 0.5f;
		rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y) + buttonVelocty * Time.deltaTime * 80f;
	}
}
