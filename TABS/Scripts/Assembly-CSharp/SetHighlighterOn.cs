using HighlightingSystem;
using UnityEngine;

public class SetHighlighterOn : MonoBehaviour
{
	private Highlighter _highlighter;

	private void Awake()
	{
		_highlighter = GetComponent<Highlighter>();
	}

	private void Update()
	{
		_highlighter.Hover(Color.white);
	}
}
