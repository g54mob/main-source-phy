using System.Collections;
using UnityEngine;

public class StoneHengeLightningController : MonoBehaviour
{
	public Renderer vis;

	public float buildUpSpeed = 0.3f;

	public bool isShooting;

	public LineRenderer lineRendy;

	public float lineRendyVisDuration = 0.1f;

	public Transform lineStartPoint;

	public float coolDownWait = 1f;

	public float forceToAdd = 10000f;

	private float currentTime;

	private float timeToTrigger = 0.5f;

	private Rigidbody myBody;

	private Collider myCollider;

	private AudioSource audioSource;

	private void Awake()
	{
		myCollider = GetComponent<Collider>();
		myBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (StatMaster.levelSimulating)
		{
			currentTime += Time.deltaTime;
			if (currentTime >= timeToTrigger)
			{
				StartCoroutine(SetCol());
				currentTime = 0f;
			}
		}
	}

	private IEnumerator SetCol()
	{
		myCollider.enabled = false;
		yield return new WaitForFixedUpdate();
		myCollider.enabled = true;
		myBody.WakeUp();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!StatMaster.levelSimulating || isShooting || other == null)
		{
			return;
		}
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if (attachedRigidbody != null)
		{
			BlockBehaviour componentInParent = attachedRigidbody.GetComponentInParent<BlockBehaviour>();
			if (componentInParent != null && componentInParent.fireTag != null && base.gameObject.activeInHierarchy)
			{
				StartCoroutine(Shoot(attachedRigidbody));
			}
		}
	}

	protected void OnDisable()
	{
		StopAllCoroutines();
	}

	private IEnumerator Shoot(Rigidbody obj)
	{
		isShooting = true;
		yield return new WaitForSeconds(Random.Range(0f, 0.2f));
		audioSource.Play();
		yield return StartCoroutine(LerpEmiss(Color.black, Color.white));
		if (obj != null)
		{
			StartCoroutine(LineRendyVis(ShootRay(obj.transform)));
		}
		yield return StartCoroutine(LerpEmiss(Color.white, Color.black));
		yield return new WaitForSeconds(coolDownWait);
		isShooting = false;
	}

	private Vector3 ShootRay(Transform obj)
	{
		Vector3 vector = obj.position;
		BasicInfo component = obj.GetComponent<BasicInfo>();
		if (component != null)
		{
			vector = component.CenterOfBounds;
		}
		LayerMask layerMask = AddPiece.CreateLayerMask(new int[14]
		{
			0, 8, 10, 12, 14, 15, 16, 17, 18, 24,
			25, 26, 28, 29
		});
		RaycastHit[] array = Physics.SphereCastAll(lineStartPoint.position, 0.1f, (vector - lineStartPoint.position).normalized, 100f, layerMask);
		if (array.Length > 0)
		{
			int num = 0;
			float num2 = float.MaxValue;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].collider.transform.IsChildOf(base.transform.parent) && array[i].distance < num2)
				{
					num = i;
					num2 = array[i].distance;
				}
			}
			RaycastHit raycastHit = array[num];
			Vector3 result = raycastHit.point;
			FireTag component2 = raycastHit.collider.GetComponent<FireTag>();
			if (component2 == null)
			{
				Rigidbody attachedRigidbody = raycastHit.collider.attachedRigidbody;
				if (attachedRigidbody != null)
				{
					component2 = attachedRigidbody.GetComponent<FireTag>();
					result = attachedRigidbody.worldCenterOfMass;
				}
			}
			if (component2 != null)
			{
				if (component2.hasController)
				{
					component2.Ignite(1f);
					LaserHitAI component3 = component2.GetComponent<LaserHitAI>();
					if ((bool)component3)
					{
						component3.LaserHit();
					}
				}
				else
				{
					component2.VisualiseFireHit();
					component2.VisualiseFireHit();
					component2.VisualiseFireHit();
					component2.VisualiseFireHit();
				}
				return result;
			}
		}
		return obj.position;
	}

	private IEnumerator LineRendyVis(Vector3 endPos)
	{
		lineRendy.SetPosition(0, lineStartPoint.position);
		lineRendy.SetPosition(1, endPos);
		lineRendy.enabled = true;
		yield return new WaitForSeconds(lineRendyVisDuration);
		lineRendy.enabled = false;
	}

	private IEnumerator LerpEmiss(Color startCol, Color endCol)
	{
		float cTime = 0f;
		float rate = 1f / buildUpSpeed;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			vis.material.SetColor("_EmissCol", Color.Lerp(startCol, endCol, cTime));
			yield return null;
		}
	}
}
