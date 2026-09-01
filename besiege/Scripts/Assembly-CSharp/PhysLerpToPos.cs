using UnityEngine;

public class PhysLerpToPos : MonoBehaviour
{
	public Vector3 animDirection;

	public Vector3 rotateAxis = Vector3.zero;

	public Rigidbody objToLerp;

	public float speed = 1f;

	public float flipRate = 1f;

	public float pauseBetween;

	private float currentTime;

	private bool canMove = true;

	private Rigidbody myBody;

	private Vector3 multiplier;

	private float direction = 1f;

	protected void Awake()
	{
		myBody = GetComponent<Rigidbody>();
		multiplier = animDirection * speed * Time.fixedDeltaTime;
	}

	protected void FixedUpdate()
	{
		if (StatMaster.isClient)
		{
			return;
		}
		currentTime += Time.fixedDeltaTime;
		if (canMove)
		{
			if (currentTime > flipRate)
			{
				direction *= -1f;
				canMove = false;
				currentTime = 0f;
			}
			if (multiplier.sqrMagnitude > 0f)
			{
				objToLerp.MovePosition(myBody.position + multiplier * direction);
			}
			if (rotateAxis.sqrMagnitude > 0f)
			{
				objToLerp.MoveRotation(myBody.rotation * Quaternion.Euler(rotateAxis * Time.fixedDeltaTime * speed * direction));
			}
		}
		else if (currentTime >= pauseBetween)
		{
			canMove = true;
			currentTime = 0f;
		}
	}
}
