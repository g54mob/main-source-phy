using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PineTweenSystem
{
	public List<PineTween> tweensNoGameObject = new List<PineTween>();

	public Dictionary<GameObject, List<PineTween>> tweensGameObject = new Dictionary<GameObject, List<PineTween>>();

	private List<PineTween> deferredTweenProcessing = new List<PineTween>(64);

	private List<PineTween> deferredTweenProcessingComplete = new List<PineTween>(64);

	private List<PineTween> collectTweensCache = new List<PineTween>(64);

	private void updateTween(PineTween tween, float unitTime)
	{
		if (tween.gameObject != null)
		{
			if (tween.activate && unitTime >= 0f)
			{
				tween.gameObject.SetActive(value: true);
			}
			if (tween.anchoredPositionTo.HasValue)
			{
				tween.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(tween.anchoredPositionFrom.Value, tween.anchoredPositionTo.Value, unitTime);
			}
			if (tween.colorTo.HasValue)
			{
				if (tween.gameObject.TryGetComponent<Text>(out var component))
				{
					component.color = Color.Lerp(tween.colorFrom.Value, tween.colorTo.Value, unitTime);
				}
				else
				{
					tween.gameObject.GetComponent<Image>().color = Color.Lerp(tween.colorFrom.Value, tween.colorTo.Value, unitTime);
				}
			}
			if (tween.positionTo.HasValue)
			{
				Vector3 position = Vector3.Lerp(tween.positionFrom.Value, tween.positionTo.Value, unitTime);
				if (tween.rigidbody == null)
				{
					tween.gameObject.transform.position = position;
				}
			}
			if (tween.localPositionTo.HasValue)
			{
				Vector3 localPosition = Vector3.Lerp(tween.localPositionFrom.Value, tween.localPositionTo.Value, unitTime);
				if (tween.rigidbody == null)
				{
					tween.gameObject.transform.localPosition = localPosition;
				}
			}
			if (tween.rotationTo.HasValue)
			{
				Quaternion rotation = Quaternion.Slerp(tween.rotationFrom.Value, tween.rotationTo.Value, unitTime);
				if (tween.rigidbody == null)
				{
					tween.gameObject.transform.rotation = rotation;
				}
			}
			if (tween.localRotationTo.HasValue)
			{
				Quaternion localRotation = Quaternion.Slerp(tween.localRotationFrom.Value, tween.localRotationTo.Value, unitTime);
				if (tween.rigidbody == null)
				{
					tween.gameObject.transform.localRotation = localRotation;
				}
			}
			if (tween.scaleTo.HasValue)
			{
				tween.gameObject.transform.localScale = Vector3.Lerp(tween.scaleFrom.Value, tween.scaleTo.Value, unitTime);
			}
			if (tween.alphaTo.HasValue)
			{
				tween.gameObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(tween.alphaFrom.Value, tween.alphaTo.Value, unitTime);
			}
			if (tween.sizeDeltaTo.HasValue)
			{
				tween.gameObject.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(tween.sizeDeltaFrom.Value, tween.sizeDeltaTo.Value, unitTime);
			}
		}
		tween.custom?.Invoke(unitTime);
	}

	private void fixedUpdateTween(PineTween tween, float unitTime)
	{
		if (tween.positionTo.HasValue)
		{
			Vector3 position = Vector3.Lerp(tween.positionFrom.Value, tween.positionTo.Value, unitTime);
			if (tween.rigidbody != null)
			{
				tween.rigidbody.MovePosition(position);
			}
		}
		if (tween.localPositionTo.HasValue)
		{
			Vector3 vector = Vector3.Lerp(tween.localPositionFrom.Value, tween.localPositionTo.Value, unitTime);
			if (tween.rigidbody != null)
			{
				Vector3 position2 = ((tween.rigidbody.transform.parent != null) ? tween.rigidbody.transform.parent.TransformPoint(vector) : vector);
				tween.rigidbody.MovePosition(position2);
			}
		}
		if (tween.rotationTo.HasValue)
		{
			Quaternion rot = Quaternion.Slerp(tween.rotationFrom.Value, tween.rotationTo.Value, unitTime);
			if (tween.rigidbody != null)
			{
				tween.rigidbody.MoveRotation(rot);
			}
		}
		if (tween.localRotationTo.HasValue)
		{
			Quaternion quaternion = Quaternion.Slerp(tween.localRotationFrom.Value, tween.localRotationTo.Value, unitTime);
			if (tween.rigidbody != null)
			{
				Quaternion rot2 = ((tween.rigidbody.transform.parent != null) ? (tween.rigidbody.transform.parent.rotation * quaternion) : quaternion);
				tween.rigidbody.MoveRotation(rot2);
			}
		}
	}

	private void calculateTimes(PineTween tween, float elapsedTime, out float unitTime, out float interpolatedTime)
	{
		unitTime = 0f;
		if (tween.duration == 0f)
		{
			unitTime = ((elapsedTime - tween.delay >= 0f) ? 1f : 0f);
		}
		else
		{
			unitTime = (elapsedTime - tween.delay) / tween.duration;
		}
		interpolatedTime = tween.interpolation.evaluate(unitTime);
	}

	private void finishEvents(PineTween tween, bool performEvents = true, bool performFinalUpdate = true)
	{
		if (tween.finished)
		{
			return;
		}
		tween.finished = true;
		if (performFinalUpdate && tween.gameObject != null)
		{
			updateTween(tween, 1f);
		}
		if (performEvents)
		{
			tween.onComplete?.Invoke();
			if (tween.gameObject != null && tween.deactivate)
			{
				tween.gameObject.SetActive(value: false);
			}
			if (tween.gameObject != null && tween.destroy)
			{
				Object.Destroy(tween.gameObject);
			}
		}
	}

	private void cleanAndCollectTweens(List<PineTween> output)
	{
		output.Clear();
		foreach (KeyValuePair<GameObject, List<PineTween>> item in tweensGameObject)
		{
			item.Deconstruct(out var _, out var value);
			foreach (PineTween item2 in value)
			{
				output.Add(item2);
			}
		}
		foreach (PineTween item3 in tweensNoGameObject)
		{
			output.Add(item3);
		}
	}

	private void internalRemoveTween(PineTween tween)
	{
		if (tween.gameObject != null && tweensGameObject.TryGetValue(tween.gameObject, out var value))
		{
			value.Remove(tween);
			if (value.Count == 0)
			{
				tweensGameObject.Remove(tween.gameObject);
			}
		}
		tweensNoGameObject.Remove(tween);
	}

	protected PineTween animateInternal(int handle, GameObject gameObject, CustomAnimation animation, float delay = 0f, int dataInt = 0)
	{
		PineTween pineTween = new PineTween
		{
			gameObject = gameObject,
			animation = animation,
			hasAnimation = true,
			delay = delay,
			handle = handle,
			dataInt = dataInt
		};
		tweensNoGameObject.Add(pineTween);
		return pineTween;
	}

	public PineTween tween(int handle, GameObject gameObject, float duration = 1f, float delay = 0f, bool deactivate = false, bool activate = false, bool destroy = false, Vector3? anchoredPositionTo = null, Vector3? anchoredPositionFrom = null, Color? colorTo = null, Color? colorFrom = null, Vector3? positionFrom = null, Vector3? positionTo = null, Vector3? localPositionFrom = null, Vector3? localPositionTo = null, Quaternion? rotationFrom = null, Quaternion? rotationTo = null, Quaternion? localRotationFrom = null, Quaternion? localRotationTo = null, Vector3? scaleFrom = null, Vector3? scaleTo = null, float? alphaTo = null, float? alphaFrom = null, float? heightTo = null, float? heightFrom = null, Interpolation interpolation = Interpolation.Linear, bool hasCustom = false, int dataInt = 0)
	{
		PineTween pineTween = tweenInternal(gameObject, duration, delay, deactivate, activate, destroy, anchoredPositionTo, anchoredPositionFrom, colorTo, colorFrom, positionFrom, positionTo, localPositionFrom, localPositionTo, rotationFrom, rotationTo, localRotationFrom, localRotationTo, scaleFrom, scaleTo, alphaTo, alphaFrom, heightTo, heightFrom, null, null, interpolation, handle, dataInt);
		pineTween.hasCustom = hasCustom;
		return pineTween;
	}

	protected PineTween tweenInternal(GameObject gameObject, float duration = 1f, float delay = 0f, bool deactivate = false, bool activate = false, bool destroy = false, Vector3? anchoredPositionTo = null, Vector3? anchoredPositionFrom = null, Color? colorTo = null, Color? colorFrom = null, Vector3? positionFrom = null, Vector3? positionTo = null, Vector3? localPositionFrom = null, Vector3? localPositionTo = null, Quaternion? rotationFrom = null, Quaternion? rotationTo = null, Quaternion? localRotationFrom = null, Quaternion? localRotationTo = null, Vector3? scaleFrom = null, Vector3? scaleTo = null, float? alphaTo = null, float? alphaFrom = null, float? heightTo = null, float? heightFrom = null, CustomProcessor custom = null, OnComplete onComplete = null, Interpolation interpolation = Interpolation.Linear, int handle = -1, int dataInt = 0)
	{
		PineTween pineTween = new PineTween();
		pineTween.gameObject = gameObject;
		pineTween.rigidbody = gameObject.GetComponent<Rigidbody>();
		if (pineTween.rigidbody != null)
		{
			Rigidbody[] componentsInParent = gameObject.GetComponentsInParent<Rigidbody>();
			foreach (Rigidbody rigidbody in componentsInParent)
			{
				if (rigidbody != pineTween.rigidbody && !rigidbody.isKinematic)
				{
					pineTween.rigidbody = null;
					break;
				}
			}
		}
		pineTween.dataInt = dataInt;
		pineTween.handle = handle;
		pineTween.deactivate = deactivate;
		pineTween.activate = activate;
		pineTween.destroy = destroy;
		pineTween.duration = duration;
		pineTween.delay = delay;
		pineTween.custom = custom;
		pineTween.hasCustom = custom != null;
		pineTween.onComplete = onComplete;
		pineTween.interpolation = interpolation;
		if (anchoredPositionTo.HasValue)
		{
			pineTween.anchoredPositionTo = anchoredPositionTo;
			pineTween.anchoredPositionFrom = anchoredPositionFrom ?? ((Vector3)gameObject.GetComponent<RectTransform>().anchoredPosition);
		}
		if (colorTo.HasValue)
		{
			pineTween.colorTo = colorTo;
			pineTween.colorFrom = colorFrom ?? (gameObject.TryGetComponent<Text>(out var component) ? component.color : gameObject.GetComponent<Image>().color);
		}
		if (positionTo.HasValue)
		{
			pineTween.positionTo = positionTo;
			pineTween.positionFrom = positionFrom ?? gameObject.transform.position;
		}
		if (localPositionTo.HasValue)
		{
			pineTween.localPositionTo = localPositionTo;
			pineTween.localPositionFrom = positionFrom ?? gameObject.transform.localPosition;
		}
		if (rotationTo.HasValue)
		{
			pineTween.rotationTo = rotationTo;
			pineTween.rotationFrom = rotationFrom ?? gameObject.transform.rotation;
		}
		if (localRotationTo.HasValue)
		{
			pineTween.localRotationTo = localRotationTo;
			pineTween.localRotationFrom = localRotationFrom ?? gameObject.transform.localRotation;
		}
		if (scaleTo.HasValue)
		{
			pineTween.scaleTo = scaleTo;
			pineTween.scaleFrom = scaleFrom ?? gameObject.transform.localScale;
		}
		if (alphaTo.HasValue)
		{
			pineTween.alphaTo = alphaTo;
			pineTween.alphaFrom = alphaFrom ?? gameObject.GetComponent<CanvasGroup>().alpha;
		}
		Vector2? sizeDeltaTo = null;
		if (heightTo.HasValue)
		{
			sizeDeltaTo = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, heightTo.Value);
		}
		Vector2? sizeDeltaFrom = null;
		if (heightFrom.HasValue)
		{
			sizeDeltaFrom = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, heightFrom.Value);
		}
		if (sizeDeltaTo.HasValue)
		{
			pineTween.sizeDeltaTo = sizeDeltaTo;
			if (sizeDeltaFrom.HasValue)
			{
				pineTween.sizeDeltaFrom = sizeDeltaFrom;
			}
			else
			{
				pineTween.sizeDeltaFrom = gameObject.GetComponent<RectTransform>().sizeDelta;
			}
		}
		if (pineTween.delay <= 0f)
		{
			updateTween(pineTween, 0f);
		}
		List<PineTween> list;
		if (!tweensGameObject.ContainsKey(gameObject))
		{
			list = new List<PineTween>();
			tweensGameObject[gameObject] = list;
		}
		else
		{
			list = tweensGameObject[gameObject];
		}
		list.Add(pineTween);
		return pineTween;
	}

	public void destroyAllTweens(bool performEvents = true, bool performFinalUpdate = true)
	{
		List<PineTween> list = new List<PineTween>(64);
		foreach (KeyValuePair<GameObject, List<PineTween>> item in tweensGameObject)
		{
			list.AddRange(item.Value);
		}
		tweensGameObject.Clear();
		list.AddRange(tweensNoGameObject);
		tweensNoGameObject.Clear();
		foreach (PineTween item2 in list)
		{
			finishEvents(item2, performEvents, performFinalUpdate);
		}
	}

	public void destroyTweens(GameObject gameObject, bool performEvents = true, bool performFinalUpdate = true)
	{
		if (tweensGameObject.TryGetValue(gameObject, out var value))
		{
			List<PineTween> list = new List<PineTween>(value);
			tweensGameObject.Remove(gameObject);
			foreach (PineTween item in list)
			{
				finishEvents(item, performEvents, performFinalUpdate);
			}
		}
		List<PineTween> list2 = new List<PineTween>(64);
		foreach (PineTween item2 in tweensNoGameObject)
		{
			if (item2.gameObject == gameObject)
			{
				list2.Add(item2);
			}
		}
		foreach (PineTween item3 in list2)
		{
			internalRemoveTween(item3);
		}
		foreach (PineTween item4 in list2)
		{
			finishEvents(item4, performEvents, performFinalUpdate);
		}
	}

	public void destroyTween(PineTween tween, bool performEvents = true, bool performFinalUpdate = true)
	{
		finishEvents(tween, performEvents, performFinalUpdate);
		internalRemoveTween(tween);
		finishEvents(tween, performEvents, performFinalUpdate);
	}

	public void processTweens(float deltaTime)
	{
		deferredTweenProcessingComplete.Clear();
		cleanAndCollectTweens(deferredTweenProcessing);
		foreach (PineTween item in deferredTweenProcessing)
		{
			if (item.finished || item.paused)
			{
				continue;
			}
			item.elapsedTime += deltaTime;
			calculateTimes(item, item.elapsedTime, out var unitTime, out var interpolatedTime);
			if (item.animation != null || item.hasAnimation)
			{
				bool flag = false;
				if (item.animation != null)
				{
					flag = item.animation(item.elapsedTime);
				}
				if (flag)
				{
					internalRemoveTween(item);
					deferredTweenProcessingComplete.Add(item);
				}
			}
			else if (unitTime >= 1f)
			{
				if (item.rigidbody != null)
				{
					Transform parent = item.rigidbody.transform.parent;
					item.rigidbody.transform.SetParent(null);
					item.rigidbody.transform.SetParent(parent);
					item.rigidbody = null;
				}
				internalRemoveTween(item);
				deferredTweenProcessingComplete.Add(item);
			}
			else
			{
				updateTween(item, interpolatedTime);
			}
		}
		foreach (PineTween item2 in deferredTweenProcessingComplete)
		{
			finishEvents(item2);
		}
		deferredTweenProcessingComplete.Clear();
	}

	public void processFixedUpdateTweens(float deltaTime)
	{
		cleanAndCollectTweens(deferredTweenProcessing);
		foreach (PineTween item in deferredTweenProcessing)
		{
			if (!item.finished && !item.paused && !(item.rigidbody == null))
			{
				item.elapsedFixedTime += deltaTime;
				calculateTimes(item, item.elapsedFixedTime, out var unitTime, out var interpolatedTime);
				if (unitTime < 1f)
				{
					fixedUpdateTween(item, interpolatedTime);
				}
			}
		}
	}
}
