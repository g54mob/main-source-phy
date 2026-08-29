using UnityEngine;

public class TransformSync : MonoBehaviour
{
	private enum RestState
	{
		AtRest = 0,
		JustStartedMoving = 1,
		Moving = 2
	}

	public enum CalculationType
	{
		Calculate = 0,
		AlwaysSend = 1,
		NeverSend = 2
	}

	public float sendPositionThreshold = 0.01f;

	public float sendRotationThreshold = 1f;

	public CalculationType calculationType;

	private const float SEND_RATE_PER_SECOND = 30f;

	private readonly TransformSnapshot sendingSnapshot = new TransformSnapshot();

	private readonly TransformSnapshot[] snapshotBuffer = new TransformSnapshot[8];

	private int snapshotCount;

	private float lastSnapshotSendTime;

	private bool forceSendSnapshot;

	private Vector3 lastPositionWhenSnapshotWasSent;

	private Quaternion lastRotationWhenSnapshotWasSent = Quaternion.identity;

	private Vector3 positionLastFrame;

	private Quaternion rotationLastFrame;

	private Vector3 lastSetPosition;

	private Quaternion lastSetRotation;

	private int samePositionFrameCount;

	private int sameRotationFrameCount;

	private RestState positionRestState;

	private RestState rotationRestState;

	private Rigidbody cachedRigidbody;

	private float localTime;

	private byte localTimeResetIndicator;

	private Game game;

	public void init(Game game)
	{
		this.game = game;
		positionLastFrame = base.transform.position;
		rotationLastFrame = base.transform.rotation;
		lastPositionWhenSnapshotWasSent = base.transform.position;
		lastRotationWhenSnapshotWasSent = base.transform.rotation;
	}

	public void handleSnapshot(TransformSnapshot snapshot)
	{
		if (calculationType == CalculationType.Calculate && (game.hasAuthority(base.gameObject) || game.hasDeferred(base.gameObject.GetComponent<Interactive>())))
		{
			return;
		}
		if (snapshotCount > 1)
		{
			bool num = snapshot.ownerTimestamp - snapshotBuffer[0].ownerTimestamp <= 0f;
			bool flag = snapshot.localTimeResetIndicator != snapshotBuffer[0].localTimeResetIndicator;
			if (num && !flag)
			{
				return;
			}
			if (flag)
			{
				for (int num2 = snapshotCount - 1; num2 >= 0; num2--)
				{
					snapshotBuffer[num2].ownerTimestamp -= snapshotBuffer[0].ownerTimestamp;
				}
			}
		}
		for (int num3 = snapshotBuffer.Length - 1; num3 >= 1; num3--)
		{
			snapshotBuffer[num3] = snapshotBuffer[num3 - 1];
		}
		snapshotBuffer[0] = snapshot;
		snapshotCount = Mathf.Min(snapshotCount + 1, snapshotBuffer.Length);
	}

	private void Update()
	{
		localTime += Time.deltaTime;
		if (localTime > 4096f)
		{
			localTimeResetIndicator++;
			lastSnapshotSendTime -= localTime;
			for (int i = 0; i < snapshotCount; i++)
			{
				snapshotBuffer[i].receivedTimestamp -= localTime;
			}
			localTime = 0f;
			forceSendSnapshot = true;
		}
		if (snapshotCount <= 0 || (calculationType == CalculationType.Calculate && game.hasAuthority(base.gameObject)) || calculationType == CalculationType.AlwaysSend)
		{
			return;
		}
		float num = float.MaxValue;
		int num2 = 0;
		for (int num3 = snapshotCount - 1; num3 >= 0; num3--)
		{
			float receivedTimestamp = snapshotBuffer[num3].receivedTimestamp;
			if (Time.time < receivedTimestamp)
			{
				if (num > receivedTimestamp)
				{
					num2 = num3;
					num = receivedTimestamp;
				}
			}
			else
			{
				snapshotCount = num3 + 1;
			}
		}
		if (game.shouldSyncTransform(base.gameObject))
		{
			TransformSnapshot transformSnapshot = snapshotBuffer[num2];
			TransformSnapshot transformSnapshot2 = snapshotBuffer[Mathf.Max(num2 - 1, 0)];
			float t = UnityUtils.map(transformSnapshot.receivedTimestamp, transformSnapshot2.receivedTimestamp, 0f, 1f, Time.time);
			if (num2 == snapshotCount - 1)
			{
				bool flag = UnityUtils.closeEnough(base.transform.position, transformSnapshot.position, 0.0001f);
				bool flag2 = !UnityUtils.closeEnough(base.transform.position, transformSnapshot.position, 0.5f);
				setPosition(transformSnapshot.position, transformSnapshot.teleport || flag || flag2);
				bool flag3 = UnityUtils.closeEnough(base.transform.rotation, transformSnapshot.rotation);
				bool flag4 = !UnityUtils.closeEnough(base.transform.rotation, transformSnapshot.rotation, 90f);
				setRotation(transformSnapshot.rotation, transformSnapshot.teleport || flag3 || flag4);
			}
			Vector3 b = Vector3.Lerp(transformSnapshot.position, transformSnapshot2.position, t);
			setPosition(Vector3.Lerp(base.transform.position, b, 0.85f), isTeleporting: false);
			Quaternion b2 = Quaternion.Lerp(transformSnapshot.rotation, transformSnapshot2.rotation, t);
			setRotation(Quaternion.Lerp(base.transform.rotation, b2, 0.85f), isTeleporting: false);
		}
	}

	private void setPosition(Vector3 position, bool isTeleporting)
	{
		Vector3 vector = (isTeleporting ? position : Vector3.MoveTowards(base.transform.position, position, Time.deltaTime * 5f));
		if (!Is.LowDevice || !UnityUtils.closeEnough(vector, lastSetPosition, sendPositionThreshold))
		{
			lastSetPosition = vector;
			if (cachedRigidbody == null)
			{
				cachedRigidbody = GetComponent<Rigidbody>();
			}
			if (cachedRigidbody != null)
			{
				cachedRigidbody.position = vector;
			}
			base.transform.position = vector;
		}
	}

	private void setRotation(Quaternion rotation, bool isTeleporting)
	{
		Quaternion quaternion = (isTeleporting ? rotation : Quaternion.RotateTowards(base.transform.rotation, rotation, Time.deltaTime * 180f));
		if (!Is.LowDevice || !UnityUtils.closeEnough(quaternion, lastSetRotation, sendRotationThreshold))
		{
			lastSetRotation = quaternion;
			if (cachedRigidbody == null)
			{
				cachedRigidbody = GetComponent<Rigidbody>();
			}
			if (cachedRigidbody != null)
			{
				cachedRigidbody.rotation = quaternion;
			}
			base.transform.rotation = quaternion;
		}
	}

	private void LateUpdate()
	{
		sendSnapshot();
		positionLastFrame = base.transform.position;
		rotationLastFrame = base.transform.rotation;
		forceSendSnapshot = false;
	}

	private void sendSnapshot()
	{
		if (game == null)
		{
			Debug.LogError("'TransformSync' of game-object '" + base.gameObject.name + "' does not have reference to 'game'. Removing component...", base.gameObject);
			Object.Destroy(this);
			return;
		}
		if (calculationType == CalculationType.Calculate)
		{
			if (!game.hasAuthority(base.gameObject) || !game.shouldSyncTransform(base.gameObject))
			{
				return;
			}
		}
		else if (calculationType == CalculationType.NeverSend)
		{
			return;
		}
		if (positionLastFrame == base.transform.position)
		{
			if (positionRestState != RestState.AtRest && ++samePositionFrameCount == 3)
			{
				samePositionFrameCount = 0;
				positionRestState = RestState.AtRest;
				forceSendSnapshot = true;
			}
		}
		else
		{
			if (positionRestState == RestState.AtRest)
			{
				positionRestState = RestState.JustStartedMoving;
				forceSendSnapshot = true;
			}
			else if (positionRestState == RestState.JustStartedMoving)
			{
				positionRestState = RestState.Moving;
			}
			samePositionFrameCount = 0;
		}
		if (rotationLastFrame == base.transform.rotation)
		{
			if (rotationRestState != RestState.AtRest && ++sameRotationFrameCount == 3)
			{
				sameRotationFrameCount = 0;
				rotationRestState = RestState.AtRest;
				forceSendSnapshot = true;
			}
		}
		else
		{
			if (rotationRestState == RestState.AtRest)
			{
				rotationRestState = RestState.JustStartedMoving;
				forceSendSnapshot = true;
			}
			else if (rotationRestState == RestState.JustStartedMoving)
			{
				rotationRestState = RestState.Moving;
			}
			sameRotationFrameCount = 0;
		}
		if ((localTime - lastSnapshotSendTime < 1f / 30f && !forceSendSnapshot) || !game.levelReadyProcessCompleted)
		{
			return;
		}
		bool num = forceSendSnapshot || (lastPositionWhenSnapshotWasSent - base.transform.position).sqrMagnitude > sendPositionThreshold * sendPositionThreshold;
		bool flag = forceSendSnapshot || (lastRotationWhenSnapshotWasSent != base.transform.rotation && Quaternion.Angle(lastRotationWhenSnapshotWasSent, base.transform.rotation) > sendRotationThreshold);
		if (!num && !flag)
		{
			return;
		}
		sendingSnapshot.targetObject = base.gameObject;
		sendingSnapshot.ownerTimestamp = localTime;
		sendingSnapshot.position = base.transform.position;
		sendingSnapshot.rotation = base.transform.rotation;
		sendingSnapshot.localTimeResetIndicator = localTimeResetIndicator;
		if (positionRestState == RestState.JustStartedMoving)
		{
			sendingSnapshot.position = lastPositionWhenSnapshotWasSent;
		}
		if (rotationRestState == RestState.JustStartedMoving)
		{
			sendingSnapshot.rotation = lastRotationWhenSnapshotWasSent;
		}
		if (positionRestState == RestState.JustStartedMoving || rotationRestState == RestState.JustStartedMoving)
		{
			sendingSnapshot.ownerTimestamp = localTime - Time.deltaTime;
			if (positionRestState != RestState.JustStartedMoving)
			{
				sendingSnapshot.position = positionLastFrame;
			}
			if (rotationRestState != RestState.JustStartedMoving)
			{
				sendingSnapshot.rotation = rotationLastFrame;
			}
		}
		lastSnapshotSendTime = localTime;
		lastPositionWhenSnapshotWasSent = sendingSnapshot.position;
		lastRotationWhenSnapshotWasSent = sendingSnapshot.rotation;
		sendingSnapshot.teleport = game.isInTopZoom(base.gameObject);
		game.session.send(new SyncTransformPacket
		{
			snapshot = sendingSnapshot
		});
	}
}
