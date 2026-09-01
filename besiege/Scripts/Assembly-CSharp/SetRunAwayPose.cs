using UnityEngine;

public class SetRunAwayPose : MonoBehaviour
{
	private enum poseState
	{
		Default = 0,
		Running = 1
	}

	public EnemyAISimple enemyAiCode;

	public MeshFilter myMeshFilter;

	public Mesh[] runAwayPoses;

	private poseState pose;

	private Mesh startPose;

	private Mesh runAwayPose;

	private Rigidbody myBody;

	private void Start()
	{
		runAwayPose = runAwayPoses[Random.Range(0, runAwayPoses.Length)];
		startPose = myMeshFilter.sharedMesh;
		myBody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating || !myBody)
		{
			return;
		}
		if (enemyAiCode.isRunningAway)
		{
			if (pose == poseState.Default && myBody.velocity.sqrMagnitude > 1f)
			{
				myMeshFilter.sharedMesh = runAwayPose;
				pose = poseState.Running;
			}
		}
		else if (pose == poseState.Running && myBody.velocity.sqrMagnitude < 1f)
		{
			myMeshFilter.sharedMesh = startPose;
			pose = poseState.Default;
		}
	}
}
