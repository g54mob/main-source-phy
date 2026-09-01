using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndDestroy : MonoBehaviour
{
	public AnimationCurve opacityCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	public float destroyAfterSeconds = 1f;

	private Image img;

	private float timer;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Image image = default(Image);
		img = image;
	}

	private unsafe void Update()
	{
		//IL_0106: Invalid comparison between I4 and F4
		//IL_003c: Expected F4, but got I4
		//IL_00a5: Expected O, but got Ref
		float deltaTime = Time.deltaTime;
		float num = (timer = deltaTime + timer) / destroyAfterSeconds;
		if (!(0f > num))
		{
			if (num > 1f)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		if (img != null)
		{
			Color color = img.color;
			float num2 = opacityCurve.Evaluate(num);
			object obj = default(object);
			img.color = (Color)(&obj);
		}
		if (!(timer < destroyAfterSeconds))
		{
			GameObject obj2 = base.gameObject;
			Object.Destroy(obj2);
		}
	}
}
