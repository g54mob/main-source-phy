using UnityEngine;

public class ChangeAIOnNAK : MonoBehaviour
{
	public GameObject normalVis;

	public GameObject tencentVis;

	public EnemyAISimple simpleAI;

	public EntityAI complexAI;

	public RandomSoundController soundController;

	public AudioClip[] newAudio;

	public bool cartBobing;

	private void Awake()
	{
		if (tencentVis != null)
		{
			Object.DestroyImmediate(tencentVis);
		}
		if (normalVis != null)
		{
			normalVis.SetActive(true);
		}
		Object.Destroy(this);
	}
}
