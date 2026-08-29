using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LockInputVisualizer : MonoBehaviour
{
	public Lock referencedLock;

	public Text textComponent;

	private StringBuilder sb;

	private void Awake()
	{
		if (textComponent == null)
		{
			textComponent = GetComponentInChildren<Text>();
		}
		sb = new StringBuilder(referencedLock.password.Length);
	}

	public void updateForNewInput()
	{
		sb.Clear();
		int num = referencedLock.password.Length;
		int num2 = (referencedLock.nextValueIndex - 1) % num;
		for (int i = 0; i < num; i++)
		{
			if (i <= num2)
			{
				sb.Append(referencedLock.currentValues[i]);
			}
			else
			{
				sb.Append("-");
			}
		}
		textComponent.text = sb.ToString();
	}
}
