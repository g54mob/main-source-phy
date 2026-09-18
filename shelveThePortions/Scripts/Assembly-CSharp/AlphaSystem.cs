using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlphaSystem : MonoBehaviour
{
	public static Color Alphalizer(Color c, float alpha)
	{
		Color result = c;
		result.a = alpha;
		return result;
	}

	public static Color WhiteAlphalizer(float alpha)
	{
		Color white = Color.white;
		white.a = alpha;
		return white;
	}

	public static void Alphalizer(Image image, float alpha)
	{
		Color color = image.color;
		color.a = alpha;
		image.color = color;
	}

	public static void Alphalizer(Image image, float goal, TweenDuration tweenDuration, bool IgnoreTimeScale = false, TweenCallback tweenCallback = null)
	{
		TweenController.KillTweens(image.gameObject);
		TweenController.DOFloat(image.gameObject, image.color.a, goal, tweenDuration, delegate(float alphaValue)
		{
			if (image != null)
			{
				Alphalizer(image, alphaValue);
			}
		}, tweenCallback, IgnoreTimeScale);
	}

	public static void Alphalizer(SpriteRenderer spriteRenderer, float alpha)
	{
		Color color = spriteRenderer.color;
		color.a = alpha;
		spriteRenderer.color = color;
	}

	public static void Alphalizer(SpriteRenderer spriteRenderer, float goal, TweenDuration tweenDuration, bool IgnoreTimeScale = false, TweenCallback tweenCallback = null)
	{
		TweenController.KillTweens(spriteRenderer.gameObject);
		TweenController.DOFloat(spriteRenderer.gameObject, spriteRenderer.color.a, goal, tweenDuration, delegate(float alphaValue)
		{
			if (spriteRenderer != null)
			{
				Alphalizer(spriteRenderer, alphaValue);
			}
		}, tweenCallback, IgnoreTimeScale);
	}

	public static void Alphalizer(SpriteRenderer spriteRenderer, float goal, float time, bool IgnoreTimeScale = false, TweenCallback tweenCallback = null)
	{
		TweenController.KillTweens(spriteRenderer.gameObject);
		TweenController.DOFloat(spriteRenderer.gameObject, spriteRenderer.color.a, goal, time, delegate(float alphaValue)
		{
			if (spriteRenderer != null)
			{
				Alphalizer(spriteRenderer, alphaValue);
			}
		}, tweenCallback, IgnoreTimeScale);
	}

	public static void Alphalizer(TextMeshProUGUI text, float alpha)
	{
		Color color = text.color;
		color.a = alpha;
		text.color = color;
	}

	public static void Alphalizer(TextMeshPro text, float alpha)
	{
		Color color = text.color;
		color.a = alpha;
		text.color = color;
	}

	public static void Alphalizer(TextMeshProUGUI text, float goal, TweenDuration tweenDuration, bool IgnoreTimeScale = false, TweenCallback tweenCallback = null)
	{
		TweenController.KillTweens(text.gameObject);
		TweenController.DOFloat(text.gameObject, text.color.a, goal, tweenDuration, delegate(float alphaValue)
		{
			if (text != null)
			{
				Alphalizer(text, alphaValue);
			}
		}, tweenCallback, IgnoreTimeScale);
	}

	public static void Alphalizer(TextMeshPro text, float goal, TweenDuration tweenDuration, bool IgnoreTimeScale = false, TweenCallback tweenCallback = null)
	{
		TweenController.KillTweens(text.gameObject);
		TweenController.DOFloat(text.gameObject, text.color.a, goal, tweenDuration, delegate(float alphaValue)
		{
			if (text != null)
			{
				Alphalizer(text, alphaValue);
			}
		}, tweenCallback, IgnoreTimeScale);
	}

	public static void Alphalizer(CanvasGroup cg, float goal, TweenDuration tweenDuration, bool IgnoreTimeScale = false, TweenCallback tweenCallback = null)
	{
		TweenController.KillTweens(cg.gameObject);
		TweenController.DOFloat(cg.gameObject, cg.alpha, goal, tweenDuration, delegate(float alphaValue)
		{
			if (cg != null)
			{
				cg.alpha = alphaValue;
			}
		}, tweenCallback, IgnoreTimeScale);
	}

	public static void Alphalizer(CanvasGroup cg, float goal, float time, bool IgnoreTimeScale = false, TweenCallback tweenCallback = null)
	{
		TweenController.KillTweens(cg.gameObject);
		TweenController.DOFloat(cg.gameObject, cg.alpha, goal, time, delegate(float alphaValue)
		{
			if (cg != null)
			{
				cg.alpha = alphaValue;
			}
		}, tweenCallback, IgnoreTimeScale);
	}
}
