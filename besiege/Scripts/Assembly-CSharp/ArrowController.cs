using UnityEngine;

public class ArrowController : MonoBehaviour
{
	public float killTimer = 10f;

	public ArcherAI myArcherAiParent;

	public float blockDamageAmount = 1f;

	public RandomSoundController randomSoundController;

	public float impactForceMultiplier = 10f;

	public LookAtVelocityArrow lookAtVelocityCode;

	[SerializeField]
	protected Collider myCollider;

	[SerializeField]
	protected Rigidbody myBody;

	private float speed;

	private float timey;

	private bool hasAttached;

	private Rigidbody attachedRigidbody;

	private float myMass;

	private Vector3 myCOM;

	private RigidbodyInterpolation myInterpolation;

	private float myAngularDrag;

	private int myIterationCount;

	private float myMaxAngularVelocity;

	protected void Start()
	{
		if (lookAtVelocityCode == null)
		{
			lookAtVelocityCode = base.gameObject.GetComponent<LookAtVelocityArrow>();
		}
		if (randomSoundController == null)
		{
			randomSoundController = base.gameObject.GetComponent<RandomSoundController>();
		}
		if (myBody != null)
		{
			myMass = myBody.mass;
			myCOM = myBody.centerOfMass;
			myInterpolation = myBody.interpolation;
			myAngularDrag = myBody.angularDrag;
			myIterationCount = myBody.solverIterations;
			myMaxAngularVelocity = myBody.maxAngularVelocity;
		}
		else
		{
			Debug.LogWarning("Warning: ArrowController doesn't have a rigidbody!", base.gameObject);
		}
	}

	protected void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger || hasAttached || !myBody)
		{
			return;
		}
		hasAttached = true;
		attachedRigidbody = other.attachedRigidbody;
		randomSoundController.Play();
		base.gameObject.tag = "ArrowRigStatic";
		if (attachedRigidbody != null)
		{
			attachedRigidbody.AddForce(myBody.velocity * impactForceMultiplier);
			BlockBehaviour component = attachedRigidbody.GetComponent<BlockBehaviour>();
			if (component != null)
			{
				if (component.Prefab.hasHealthBar)
				{
					component.BlockHealth.DamageBlock(blockDamageAmount);
				}
				if (component.Prefab.Type == BlockType.Balloon)
				{
					base.transform.position = new Vector3(1000f, -1000f, 1000f);
				}
			}
			if (!attachedRigidbody.GetComponent<ExplodeOnCollide>() && !attachedRigidbody.name.Contains("Floor"))
			{
				base.transform.SetParent(attachedRigidbody.transform, true);
				base.transform.localScale = new Vector3(base.transform.localScale.x / base.transform.lossyScale.x, base.transform.localScale.y / base.transform.lossyScale.y, base.transform.localScale.z / base.transform.lossyScale.z);
				myBody.velocity = Vector3.zero;
			}
		}
		DestroyComponents();
	}

	public void ResetRigidbody()
	{
		hasAttached = false;
		base.gameObject.tag = "ArrowRigMove";
		myCollider.enabled = true;
		lookAtVelocityCode.enabled = true;
		base.transform.localScale = Vector3.one;
		if (myBody == null)
		{
			myBody = base.gameObject.AddComponent<Rigidbody>();
			myBody.mass = myMass;
			myBody.centerOfMass = myCOM;
			myBody.solverIterations = myIterationCount;
			myBody.angularDrag = myAngularDrag;
			myBody.interpolation = myInterpolation;
			myBody.maxAngularVelocity = myMaxAngularVelocity;
		}
		myBody.isKinematic = false;
		lookAtVelocityCode.ResetBody();
		myBody.velocity = Vector3.zero;
	}

	private void DestroyComponents()
	{
		lookAtVelocityCode.enabled = false;
		myCollider.enabled = false;
		Object.Destroy(myBody);
		myBody = null;
	}
}
