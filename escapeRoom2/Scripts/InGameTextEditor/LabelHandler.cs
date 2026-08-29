using InGameTextEditor;
using UnityEngine;

public class LabelHandler : MonoBehaviour
{
	public InGameTextEditor.TextEditor textEditor;

	public Sprite underlineSprite;

	public Sprite labelIconSprite;

	public void AddLabels()
	{
		textEditor.RemoveLabels();
		foreach (Line line in textEditor.Lines)
		{
			int num = 0;
			while (true)
			{
				int num2 = line.Text.IndexOf("label", num);
				if (num2 < 0)
				{
					break;
				}
				num = num2 + "label".Length;
				line.AddLabel(num2, num, underlineSprite, new Color(0f, 0.8f, 0f, 0.8f), labelIconSprite, new Color(0f, 0.8f, 0f, 1f), "This is a label", Line.Label.DeleteCondition.THIS_LINE_CHANGES);
			}
		}
	}

	public void ClearLabels()
	{
		textEditor.RemoveLabels();
	}
}
