using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TweenController : Singleton<TweenController>
{
	private static Dictionary<GameObject, List<Tween>> tweenDict = new Dictionary<GameObject, List<Tween>>();

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		ResetDict();
	}

	private void ResetDict()
	{
		tweenDict = new Dictionary<GameObject, List<Tween>>();
	}

	private void Start()
	{
		DOTween.SetTweensCapacity(1500, 50);
		ResetDict();
	}

	public static void KillTweens(GameObject obj)
	{
		if (obj == null || tweenDict == null)
		{
			return;
		}
		if (tweenDict.ContainsKey(obj))
		{
			Tween[] array = tweenDict[obj].ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]?.Kill();
			}
		}
		DOTween.Kill(obj);
	}

	public static void AddTweenoDict(GameObject obj, Tween tween)
	{
		if (tweenDict.ContainsKey(obj))
		{
			tweenDict[obj].Add(tween);
			return;
		}
		tweenDict.Add(obj, new List<Tween> { tween });
	}

	public static void RemoveTweenoDict(GameObject obj, Tween tween)
	{
		if (!(obj == null) && tweenDict != null && tweenDict.ContainsKey(obj) && tween != null)
		{
			tweenDict[obj].Remove(tween);
			if (tweenDict[obj].Count == 0)
			{
				tweenDict.Remove(obj);
			}
		}
	}

	public static void CompleteTween(GameObject obj, Tween tween, TweenCallback OnComplete = null)
	{
		if (!(obj == null))
		{
			RemoveTweenoDict(obj, tween);
			OnComplete?.Invoke();
		}
	}

	public static void DelayedCall(GameObject obj, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(obj == null))
		{
			Tween tween = null;
			tween = DOVirtual.DelayedCall(TweenDataStore.GetTweenDuration(tweenDuration), delegate
			{
				CompleteTween(obj, tween, tweenCallback);
			}, ignoreTimeScale);
			AddTweenoDict(obj, tween);
		}
	}

	public static void DelayedCall(GameObject obj, float delay, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(obj == null))
		{
			Tween tween = null;
			tween = DOVirtual.DelayedCall(delay, delegate
			{
				CompleteTween(obj, tween, tweenCallback);
			}, ignoreTimeScale);
			AddTweenoDict(obj, tween);
		}
	}

	public static void Shake(Transform objTransform, Vector3 originalScale, float duration, float percent, bool ignoreTimeScale = false)
	{
		if (objTransform == null)
		{
			return;
		}
		Vector3 startPos = objTransform.transform.localPosition;
		Tween tween = null;
		tween = objTransform.DOShakeScale(duration, 0.1f * percent).SetUpdate(ignoreTimeScale).OnKill(delegate
		{
			if (!(objTransform == null))
			{
				objTransform.transform.localScale = originalScale;
				objTransform.transform.localPosition = startPos;
			}
		})
			.OnComplete(delegate
			{
				if (!(objTransform == null))
				{
					objTransform.transform.localScale = originalScale;
					objTransform.transform.localPosition = startPos;
					CompleteTween(objTransform.gameObject, tween);
				}
			});
		AddTweenoDict(objTransform.gameObject, tween);
	}

	public static void DOLocalRectMoveX(RectTransform objTransform, float endValue, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(objTransform == null))
		{
			GameObject obj = objTransform.gameObject;
			Tween tween = null;
			tween = objTransform.DOLocalMoveX(endValue, TweenDataStore.GetTweenDuration(tweenDuration)).SetUpdate(ignoreTimeScale).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					CompleteTween(obj, tween, tweenCallback);
				});
			AddTweenoDict(obj, tween);
		}
	}

	public static void DOLocalRectMoveY(RectTransform objTransform, float endValue, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(objTransform == null))
		{
			GameObject obj = objTransform.gameObject;
			Tween tween = null;
			tween = objTransform.DOLocalMoveY(endValue, TweenDataStore.GetTweenDuration(tweenDuration)).SetUpdate(ignoreTimeScale).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					CompleteTween(obj, tween, tweenCallback);
				});
			AddTweenoDict(obj, tween);
		}
	}

	public static void DOLocalMoveX(Transform objTransform, float endValue, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(objTransform == null))
		{
			GameObject obj = objTransform.gameObject;
			Tween tween = null;
			tween = objTransform.DOLocalMoveX(endValue, TweenDataStore.GetTweenDuration(tweenDuration)).SetUpdate(ignoreTimeScale).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					CompleteTween(obj, tween, tweenCallback);
				});
			AddTweenoDict(obj, tween);
		}
	}

	public static void DOLocalMove(Transform objTransform, Vector3 globalEndValue, float tweenDuration, AnimationCurve movementCurve, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(objTransform == null))
		{
			GameObject obj = objTransform.gameObject;
			Tween tween = null;
			tween = objTransform.DOLocalMove(globalEndValue, tweenDuration).SetUpdate(ignoreTimeScale).SetEase(movementCurve)
				.OnComplete(delegate
				{
					CompleteTween(obj, tween, tweenCallback);
				});
			AddTweenoDict(obj, tween);
		}
	}

	public static void DOMove(Transform objTransform, Vector3 globalEndValue, float tweenDuration, AnimationCurve movementCurve, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(objTransform == null))
		{
			GameObject obj = objTransform.gameObject;
			Tween tween = null;
			tween = objTransform.DOMove(globalEndValue, tweenDuration).SetUpdate(ignoreTimeScale).SetEase(movementCurve)
				.OnComplete(delegate
				{
					CompleteTween(obj, tween, tweenCallback);
				});
			AddTweenoDict(obj, tween);
		}
	}

	public static void DOLocalRotation(Transform objTransform, Vector3 globalRot, float tweenDuration, AnimationCurve movementCurve, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(objTransform == null))
		{
			GameObject obj = objTransform.gameObject;
			Tween tween = null;
			tween = objTransform.DOLocalRotate(globalRot, tweenDuration).SetUpdate(ignoreTimeScale).SetEase(movementCurve)
				.OnComplete(delegate
				{
					CompleteTween(obj, tween, tweenCallback);
				});
			AddTweenoDict(obj, tween);
		}
	}

	public static void DORotation(Transform objTransform, Vector3 globalRot, float tweenDuration, AnimationCurve movementCurve, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(objTransform == null))
		{
			GameObject obj = objTransform.gameObject;
			Tween tween = null;
			tween = objTransform.DORotate(globalRot, tweenDuration).SetUpdate(ignoreTimeScale).SetEase(movementCurve)
				.OnComplete(delegate
				{
					CompleteTween(obj, tween, tweenCallback);
				});
			AddTweenoDict(obj, tween);
		}
	}

	public static void DOMoveTransform(Transform objTransform, Transform targetTransform, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		DOMoveTransform(objTransform, targetTransform.position, targetTransform.eulerAngles, targetTransform.lossyScale, tweenDuration, tweenCallback, ignoreTimeScale);
	}

	public static void DOMoveTransform(Transform objTransform, Vector3 targetPosition, Vector3 targetAngles, Vector3 targetScale, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(objTransform == null))
		{
			GameObject obj = objTransform.gameObject;
			Vector3 startingPos = objTransform.localPosition;
			Vector3 startingAngles = objTransform.localEulerAngles;
			Vector3 startingScale = objTransform.lossyScale;
			Tween tween = null;
			tween = DOVirtual.Float(0f, 1f, TweenDataStore.GetTweenDuration(tweenDuration), delegate(float floatValue)
			{
				objTransform.localPosition = Vector3.Lerp(startingPos, targetPosition, floatValue);
				objTransform.localRotation = Quaternion.Lerp(Quaternion.Euler(startingAngles), Quaternion.Euler(targetAngles), floatValue);
				objTransform.localScale = Vector3.Lerp(startingScale, targetScale, floatValue);
			}).SetUpdate(ignoreTimeScale).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					CompleteTween(obj, tween, tweenCallback);
				});
			AddTweenoDict(obj, tween);
		}
	}

	public static void Scale(GameObject obj, float startScale, float endScale, float tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(obj == null))
		{
			Scale(obj.transform, startScale, endScale, tweenDuration, tweenCallback, ignoreTimeScale);
		}
	}

	public static void Scale(GameObject obj, float startScale, float endScale, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(obj == null))
		{
			Scale(obj.transform, startScale, endScale, TweenDataStore.GetTweenDuration(tweenDuration), tweenCallback, ignoreTimeScale);
		}
	}

	public static void Scale(Transform objTransform, float startScale, float endScale, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		Scale(objTransform, startScale, endScale, TweenDataStore.GetTweenDuration(tweenDuration), tweenCallback, ignoreTimeScale);
	}

	public static void Scale(Transform objTransform, float startScale, float endScale, float tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (objTransform == null)
		{
			return;
		}
		Ease ease = ((!(startScale > endScale)) ? Ease.OutBack : Ease.InBack);
		objTransform.localScale = Vector2.one * startScale;
		Tween tween = null;
		tween = objTransform.DOScale(Vector2.one * endScale, tweenDuration).SetEase(ease).SetUpdate(ignoreTimeScale)
			.OnComplete(delegate
			{
				if (!(objTransform == null))
				{
					objTransform.localScale = Vector2.one * endScale;
					CompleteTween(objTransform.gameObject, tween, tweenCallback);
				}
			});
		AddTweenoDict(objTransform.gameObject, tween);
	}

	public static void ScaleNoEase(Transform objTransform, float startScale, float endScale, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		ScaleNoEase(objTransform, startScale, endScale, TweenDataStore.GetTweenDuration(tweenDuration), tweenCallback, ignoreTimeScale);
	}

	public static void ScaleNoEase(Transform objTransform, float startScale, float endScale, float tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (objTransform == null)
		{
			return;
		}
		objTransform.localScale = Vector2.one * startScale;
		Tween tween = null;
		tween = objTransform.DOScale(Vector2.one * endScale, tweenDuration).SetUpdate(ignoreTimeScale).OnComplete(delegate
		{
			if (!(objTransform == null))
			{
				objTransform.localScale = Vector2.one * endScale;
				CompleteTween(objTransform.gameObject, tween, tweenCallback);
			}
		});
		AddTweenoDict(objTransform.gameObject, tween);
	}

	public static void ScaleV3(Transform objTransform, float startScale, float endScale, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (objTransform == null)
		{
			return;
		}
		Ease ease = ((!(startScale > endScale)) ? Ease.OutBack : Ease.InBack);
		objTransform.localScale = Vector3.one * startScale;
		Tween tween = null;
		tween = objTransform.DOScale(Vector3.one * endScale, TweenDataStore.GetTweenDuration(tweenDuration)).SetEase(ease).SetUpdate(ignoreTimeScale)
			.OnKill(delegate
			{
				if (!(objTransform == null))
				{
					objTransform.localScale = Vector3.one * endScale;
				}
			})
			.OnComplete(delegate
			{
				if (!(objTransform == null))
				{
					objTransform.localScale = Vector3.one * endScale;
					CompleteTween(objTransform.gameObject, tween, tweenCallback);
				}
			});
		AddTweenoDict(objTransform.gameObject, tween);
	}

	public static void PunchScale(GameObject obj, float Power, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (!(obj == null))
		{
			PunchScale(obj.transform, Power, tweenDuration, tweenCallback, ignoreTimeScale);
		}
	}

	public static void PunchScale(Transform objTransform, float Power, TweenDuration tweenDuration, TweenCallback tweenCallback = null, bool ignoreTimeScale = false)
	{
		if (objTransform == null)
		{
			return;
		}
		Vector3 startScale = objTransform.localScale;
		Tween tween = null;
		tween = objTransform.DOPunchScale(startScale * Power, TweenDataStore.GetTweenDuration(tweenDuration)).SetUpdate(ignoreTimeScale).OnKill(delegate
		{
			if (!(objTransform == null))
			{
				objTransform.localScale = startScale;
			}
		})
			.OnComplete(delegate
			{
				if (!(objTransform == null))
				{
					objTransform.localScale = startScale;
					CompleteTween(objTransform.gameObject, tween, tweenCallback);
				}
			});
		AddTweenoDict(objTransform.gameObject, tween);
	}

	public static void DOFloat(GameObject obj, float from, float to, TweenDuration tweenDuration, TweenCallback<float> tweenCallUpdate, TweenCallback tweenCallBack = null, bool ignoreTimeScale = false)
	{
		DOFloat(obj, from, to, TweenDataStore.GetTweenDuration(tweenDuration), tweenCallUpdate, tweenCallBack, ignoreTimeScale);
	}

	public static void DOFloat(GameObject obj, float from, float to, float time, TweenCallback<float> tweenCallUpdate, TweenCallback tweenCallBack = null, bool ignoreTimeScale = false)
	{
		if (!(obj == null))
		{
			Tween tween = null;
			tween = DOVirtual.Float(from, to, time, delegate(float floatValue)
			{
				tweenCallUpdate(floatValue);
			}).SetUpdate(ignoreTimeScale).OnComplete(delegate
			{
				CompleteTween(obj, tween, tweenCallBack);
			});
			AddTweenoDict(obj, tween);
		}
	}

	public static void DOFloat(GameObject obj, float from, float to, float time, AnimationCurve animationCurve, TweenCallback<float> tweenCallUpdate, TweenCallback tweenCallBack = null, bool ignoreTimeScale = false)
	{
		if (!(obj == null))
		{
			Tween tween = null;
			tween = DOVirtual.Float(from, to, time, delegate(float floatValue)
			{
				tweenCallUpdate(floatValue);
			}).SetUpdate(ignoreTimeScale).SetEase(animationCurve)
				.OnComplete(delegate
				{
					CompleteTween(obj, tween, tweenCallBack);
				});
			AddTweenoDict(obj, tween);
		}
	}

	public static void ScaleLineRenderer(LineRenderer line, float startWidth, float endWidth, TweenDuration tweenDuration, TweenCallback tweenCallBack = null, bool ignoreTimeScale = false)
	{
		if (line == null)
		{
			return;
		}
		SetLineRendererWidth(line, startWidth);
		Tween tween = null;
		tween = DOVirtual.Float(startWidth, endWidth, TweenDataStore.GetTweenDuration(tweenDuration), delegate(float floatValue)
		{
			SetLineRendererWidth(line, floatValue);
		}).SetUpdate(ignoreTimeScale).OnKill(delegate
		{
			if (!(line == null))
			{
				SetLineRendererWidth(line, endWidth);
			}
		})
			.OnComplete(delegate
			{
				if (!(line == null))
				{
					SetLineRendererWidth(line, endWidth);
					CompleteTween(line.gameObject, tween, tweenCallBack);
				}
			});
		AddTweenoDict(line.gameObject, tween);
	}

	public static void PunchLineRenderer(LineRenderer line, float startWidth, float power, TweenDuration tweenDuration, TweenCallback tweenCallBack = null, bool ignoreTimeScale = false)
	{
		Tween tween = null;
		tween = DOVirtual.Float(startWidth, startWidth - power, TweenDataStore.GetTweenDuration(tweenDuration) / 4f, delegate(float floatValue)
		{
			SetLineRendererWidth(line, floatValue);
		}).SetUpdate(ignoreTimeScale).OnKill(delegate
		{
			SetLineRendererWidth(line, startWidth);
		})
			.OnComplete(delegate
			{
				if (!(line == null))
				{
					DOVirtual.Float(startWidth - power, startWidth, TweenDataStore.GetTweenDuration(tweenDuration) / 4f, delegate(float floatValue)
					{
						SetLineRendererWidth(line, floatValue);
					}).SetUpdate(ignoreTimeScale).OnComplete(delegate
					{
						if (!(line == null))
						{
							DOVirtual.Float(startWidth, line.startWidth + power / 2f, TweenDataStore.GetTweenDuration(tweenDuration) / 4f, delegate(float floatValue)
							{
								SetLineRendererWidth(line, floatValue);
							}).SetUpdate(ignoreTimeScale).OnComplete(delegate
							{
								if (!(line == null))
								{
									DOVirtual.Float(line.startWidth + power / 2f, startWidth, TweenDataStore.GetTweenDuration(tweenDuration) / 4f, delegate(float floatValue)
									{
										SetLineRendererWidth(line, floatValue);
									}).SetUpdate(ignoreTimeScale).OnComplete(delegate
									{
										if (!(line == null))
										{
											CompleteTween(line.gameObject, tween, tweenCallBack);
											SetLineRendererWidth(line, startWidth);
										}
									});
								}
							});
						}
					});
				}
			});
		AddTweenoDict(line.gameObject, tween);
	}

	public static void SetLineRendererWidth(LineRenderer line, float width)
	{
		if (!(line == null))
		{
			line.startWidth = width;
			line.endWidth = width;
		}
	}

	public static void FadeInAudioSource(GameObject obj, float fadeInVolume, AudioSource audioSource, TweenDuration tweenDuration, TweenCallback callBack = null, bool ignoreTimeScale = false)
	{
		DOFloat(obj, 0f, fadeInVolume, tweenDuration, delegate(float volume)
		{
			audioSource.volume = volume;
		}, callBack, ignoreTimeScale);
	}

	public static void FadeOutAudioSource(GameObject obj, AudioSource audioSource, TweenDuration tweenDuration, TweenCallback callBack = null, bool ignoreTimeScale = false)
	{
		float volume = audioSource.volume;
		DOFloat(obj, volume, 0f, tweenDuration, delegate(float volume2)
		{
			audioSource.volume = volume2;
		}, callBack, ignoreTimeScale);
	}

	public static void FadeInAudioSourcePitch(GameObject obj, float pitch, AudioSource audioSource, TweenDuration tweenDuration, TweenCallback callBack = null, bool ignoreTimeScale = false)
	{
		DOFloat(obj, 0f, pitch, tweenDuration, delegate(float pitch2)
		{
			audioSource.pitch = pitch2;
		}, callBack, ignoreTimeScale);
	}

	public static void FadeOutAudioSourcePitch(GameObject obj, AudioSource audioSource, TweenDuration tweenDuration, TweenCallback callBack = null, bool ignoreTimeScale = false)
	{
		float volume = audioSource.volume;
		DOFloat(obj, volume, 0f, tweenDuration, delegate(float pitch)
		{
			audioSource.pitch = pitch;
		}, callBack, ignoreTimeScale);
	}

	public static void CanvasGroupAlpha(CanvasGroup CG, float from, float to, TweenDuration tweenDuration, TweenCallback tweenCallBack = null, bool ignoreTimeScale = false)
	{
		if (CG == null)
		{
			return;
		}
		DOFloat(CG.gameObject, from, to, TweenDuration.Super_Short, delegate(float floatValue)
		{
			if (!(CG == null))
			{
				CG.alpha = floatValue;
			}
		}, tweenCallBack, ignoreTimeScale: true);
	}

	public static void ChangeColor(SpriteRenderer objSR, Color startColor, Color endColor, TweenDuration tweenDuration, TweenCallback callBack = null, bool ignoreTimeScale = false)
	{
		if (!(objSR != null))
		{
			return;
		}
		DOFloat(objSR.gameObject, 0f, 1f, tweenDuration, delegate(float floatValue)
		{
			if (objSR != null)
			{
				objSR.color = Color.Lerp(startColor, endColor, floatValue);
			}
		}, callBack, ignoreTimeScale);
	}

	public static void ChangeColor(SpriteRenderer objSR, Color startColor, Color endColor, float tweenDuration, AnimationCurve animationCurve, TweenCallback callBack = null, bool ignoreTimeScale = false)
	{
		if (!(objSR != null))
		{
			return;
		}
		DOFloat(objSR.gameObject, 0f, 1f, tweenDuration, animationCurve, delegate(float floatValue)
		{
			if (objSR != null)
			{
				objSR.color = Color.Lerp(startColor, endColor, floatValue);
			}
		}, callBack, ignoreTimeScale);
	}

	public static void ChangeColor(Image objSR, Color startColor, Color endColor, TweenDuration tweenDuration, TweenCallback callBack = null, bool ignoreTimeScale = false)
	{
		if (!(objSR != null))
		{
			return;
		}
		DOFloat(objSR.gameObject, 0f, 1f, tweenDuration, delegate(float floatValue)
		{
			if (objSR != null)
			{
				objSR.color = Color.Lerp(startColor, endColor, floatValue);
			}
		}, callBack, ignoreTimeScale);
	}

	public static void ChangeColor(TextMeshProUGUI objSR, Color startColor, Color endColor, TweenDuration tweenDuration, TweenCallback callBack = null, bool ignoreTimeScale = false)
	{
		if (!(objSR != null))
		{
			return;
		}
		DOFloat(objSR.gameObject, 0f, 1f, tweenDuration, delegate(float floatValue)
		{
			if (objSR != null)
			{
				objSR.color = Color.Lerp(startColor, endColor, floatValue);
			}
		}, callBack, ignoreTimeScale);
	}
}
