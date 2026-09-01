using UnityEngine;

public class VictoryController : MonoBehaviour
{
	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (WinCondition.hasWon && !animator.GetBool("Won"))
		{
			animator.SetBool("Won", true);
		}
	}
}
