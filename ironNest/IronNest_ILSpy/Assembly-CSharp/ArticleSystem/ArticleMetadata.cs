using UnityEngine;

namespace ArticleSystem;

public class ArticleMetadata : MonoBehaviour
{
	public int priority = 50;

	public bool reusable;

	public int maxColumnsPerPass = 2;
}
