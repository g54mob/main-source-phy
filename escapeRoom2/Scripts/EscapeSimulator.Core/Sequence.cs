using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class Sequence : MonoBehaviour
{
	[Serializable]
	public class SequenceRecord
	{
		public string name;

		public List<ObjectState> state = new List<ObjectState>();

		[NonSerialized]
		public List<ObjectState> startStates = new List<ObjectState>();

		[NonSerialized]
		public List<ObjectState> bezierControl1 = new List<ObjectState>();

		[NonSerialized]
		public List<ObjectState> bezierControl2 = new List<ObjectState>();

		public float time;

		public float lerpTime = -1f;
	}

	[HideInInspector]
	public float sequenceTime;

	[HideInInspector]
	public float sequenceDuration;

	[HideInInspector]
	public float delayBefore;

	[HideInInspector]
	public float delayAfter;

	[HideInInspector]
	public float targetSequenceTime;

	[HideInInspector]
	public List<SequenceRecord> sequenceKeyFrames = new List<SequenceRecord>();

	public Interpolation interpolation;

	public bool playOnAwake;

	public bool loop;

	[ReadOnly]
	public float speed;

	private bool forceUpdate;

	[HideInInspector]
	public EventReference soundStart;

	[HideInInspector]
	public EventReference soundLoop;

	[HideInInspector]
	public EventReference soundEnd;

	[HideInInspector]
	public StudioEventEmitter soundLoopEmitter;

	private string getTransformPath(Transform current, Transform root)
	{
		string text = "";
		while (current != root && current != null)
		{
			text = ((text == "") ? current.name : (current.name + "/" + text));
			current = current.parent;
		}
		if (!(current == null))
		{
			return text;
		}
		return null;
	}

	public void play(float atTime = -1f, float goal = -1f, float speed = 1f)
	{
		if (atTime != -1f)
		{
			sequenceTime = atTime;
		}
		targetSequenceTime = ((goal == -1f) ? sequenceDuration : goal);
		this.speed = speed;
		forceUpdate = true;
		PineFmod.playOneShotSoundAttached(soundStart, base.gameObject);
	}

	public void pause(float atTime = -1f)
	{
		if (atTime == -1f)
		{
			targetSequenceTime = sequenceTime;
		}
		else
		{
			targetSequenceTime = atTime;
			sequenceTime = atTime;
		}
		forceUpdate = true;
	}

	public void update(float dt, out bool complete)
	{
		bool flag = false;
		float num = sequenceTime;
		sequenceTime = Mathf.MoveTowards(sequenceTime, targetSequenceTime, dt * speed);
		if (loop)
		{
			if (num < sequenceTime && sequenceTime >= sequenceDuration)
			{
				sequenceTime -= sequenceDuration;
			}
			else if (num > sequenceTime && sequenceTime <= 0f)
			{
				sequenceTime += sequenceDuration;
			}
		}
		if (num != sequenceTime)
		{
			flag = true;
		}
		complete = false;
		if (sequenceTime == targetSequenceTime && (flag || forceUpdate))
		{
			complete = true;
		}
		if (!flag && !forceUpdate)
		{
			return;
		}
		syncVisuals();
		forceUpdate = false;
		if (complete)
		{
			if (PineFmod.isPlaying(soundLoopEmitter))
			{
				PineFmod.stop(soundLoopEmitter);
			}
			PineFmod.playOneShotSoundAttached(soundEnd, base.gameObject);
		}
		else if (!PineFmod.isPlaying(soundLoopEmitter))
		{
			PineFmod.play(soundLoopEmitter);
		}
	}

	public void setTime(float time)
	{
		sequenceTime = time;
		targetSequenceTime = time;
		syncVisuals();
	}

	public float evaluateBezier(Vector2 key1, Vector2 handle1, Vector2 handle2, Vector2 key2, float time)
	{
		return Mathf.Pow(1f - time, 3f) * key1.y + 3f * Mathf.Pow(1f - time, 2f) * time * handle1.y + 3f * (1f - time) * Mathf.Pow(time, 2f) * handle2.y + Mathf.Pow(time, 3f) * key2.y;
	}

	private void OnDestroy()
	{
		if (soundLoopEmitter != null)
		{
			if (soundLoopEmitter.IsPlaying())
			{
				soundLoopEmitter.Stop();
			}
			soundLoopEmitter.EventInstance.release();
		}
	}

	public void syncVisuals()
	{
		float num = sequenceTime;
		if (sequenceTime > delayBefore && sequenceTime < sequenceDuration - delayAfter)
		{
			float t = interpolation.evaluate((sequenceTime - delayBefore) / (sequenceDuration - delayBefore - delayAfter));
			num = Mathf.Lerp(delayBefore, sequenceDuration - delayAfter, t);
		}
		int num2 = 0;
		if (num > sequenceKeyFrames[sequenceKeyFrames.Count - 1].time)
		{
			num2 = sequenceKeyFrames.Count - 1;
		}
		for (int i = 1; i < sequenceKeyFrames.Count; i++)
		{
			if (num > sequenceKeyFrames[i - 1].time && num <= sequenceKeyFrames[i].time)
			{
				num2 = i;
			}
		}
		for (int j = 0; j < num2; j++)
		{
			if (sequenceKeyFrames[j].lerpTime != 1f)
			{
				syncRecord(j, 1f);
			}
		}
		for (int num3 = sequenceKeyFrames.Count - 1; num3 > num2; num3--)
		{
			if (sequenceKeyFrames[num3].lerpTime != 0f)
			{
				syncRecord(num3, 0f);
			}
		}
		float num4 = normalizeTime((num2 > 0) ? sequenceKeyFrames[num2 - 1].time : delayBefore, sequenceKeyFrames[num2].time, num);
		if (sequenceKeyFrames[num2].time == 0f && num == 0f)
		{
			num4 = 0f;
		}
		if (sequenceKeyFrames[num2].lerpTime != num4)
		{
			syncRecord(num2, Mathf.Clamp01(num4));
		}
		static float normalizeTime(float a, float b, float c)
		{
			float num5 = b - a;
			return (c - a) / num5;
		}
		void syncRecord(int keyIndex, float weight)
		{
			SequenceRecord sequenceRecord = sequenceKeyFrames[keyIndex];
			SequenceRecord prevRecord = null;
			float time = sequenceRecord.time;
			float prevTime = delayBefore;
			if (keyIndex > 0)
			{
				prevRecord = sequenceKeyFrames[keyIndex - 1];
				prevTime = prevRecord.time;
			}
			float keyframeTimeDiff = (time - prevTime) / 3f;
			float control1Time = prevTime + keyframeTimeDiff;
			float control2Time = time - keyframeTimeDiff;
			float lerpTime = sequenceRecord.lerpTime;
			sequenceRecord.lerpTime = weight;
			for (int k = 0; k < sequenceRecord.state.Count; k++)
			{
				ObjectState objectState = sequenceRecord.state[k];
				ObjectState objectState2 = sequenceRecord.startStates[k];
				ObjectState objectState3 = sequenceRecord.bezierControl1[k];
				ObjectState objectState4 = sequenceRecord.bezierControl2[k];
				GameObject gameObject = objectState.gameObject;
				if (gameObject == null)
				{
					Debug.LogError(string.Format("Object is null in {0} '{1}', state at index {2}.", "Sequence", base.name, k), this);
				}
				else
				{
					if (gameObject.TryGetComponent<Light>(out var component))
					{
						if ((objectState.flags & 0x4000) != 0)
						{
							component.color = Color.Lerp(objectState2.Light_Filter, objectState.Light_Filter, weight);
						}
						if ((objectState.flags & 0x8000) != 0)
						{
							component.colorTemperature = Mathf.Lerp(objectState2.Light_Temperature, objectState.Light_Temperature, weight);
						}
						if ((objectState.flags & 0x10000) != 0)
						{
							component.intensity = Mathf.Lerp(objectState2.Light_Intensity, objectState.Light_Intensity, weight);
						}
					}
					if (gameObject.TryGetComponent<Image>(out var component2) && (objectState.flags & 2) != 0)
					{
						component2.color = Color.Lerp(objectState2.Image_color, objectState.Image_color, weight);
					}
					if (gameObject.TryGetComponent<Text>(out var component3))
					{
						if ((objectState.flags & 4) != 0)
						{
							component3.color = Color.Lerp(objectState2.Text_color, objectState.Text_color, weight);
						}
						if ((objectState.flags & 8) != 0)
						{
							component3.fontSize = (int)Mathf.Lerp(objectState2.Text_fontSize, objectState.Text_fontSize, weight);
						}
					}
					if (gameObject.TryGetComponent<Item>(out var component4))
					{
						if ((objectState.flags & 0x100) != 0)
						{
							component4.examinePivotOffset = Vector3.Lerp(objectState2.Item_examinePivotOffset, objectState.Item_examinePivotOffset, weight);
						}
						if ((objectState.flags & 0x200) != 0)
						{
							component4.examineScaleModifier = Mathf.Lerp(objectState2.Item_examineScaleModifier, objectState.Item_examineScaleModifier, weight);
						}
						if ((objectState.flags & 0x2000) != 0)
						{
							component4.examineBaseRotation = Vector3.Lerp(objectState2.Item_examineBaseRotation, objectState.Item_examineBaseRotation, weight);
						}
						if ((objectState.flags & 0x20000) != 0)
						{
							component4.groundRotation = Vector3.Lerp(objectState2.Item_groundRotation, objectState.Item_groundRotation, weight);
						}
					}
					if (gameObject.TryGetComponent<Interactive>(out var component5) && (objectState.flags & 0x400) != 0)
					{
						component5.targetPriority = (int)Mathf.Lerp(objectState2.Interactive_targetPriority, objectState.Interactive_targetPriority, weight);
					}
					if (gameObject.TryGetComponent<MaterialState>(out var component6) && (objectState.flags & 0x800) != 0)
					{
						for (int l = 0; l < component6.materialStateData.materialStates.Count; l++)
						{
							MaterialState.MaterialStateRecord materialStateRecord = component6.materialStateData.materialStates[l];
							if (l < objectState.MaterialState.Length)
							{
								materialStateRecord.weight = Mathf.Lerp(objectState2.MaterialState[l], objectState.MaterialState[l], weight);
								materialStateRecord.targetWeight = materialStateRecord.weight;
								component6.syncVisuals();
							}
						}
						if (!Application.isPlaying)
						{
							component6.syncVisuals();
						}
					}
					if (gameObject.TryGetComponent<TweenState>(out var component7) && (objectState.flags & 0x1000) != 0)
					{
						if (Application.isPlaying)
						{
							if (sequenceTime == targetSequenceTime && !component7.soundLoop.IsNull && component7.soundLoopEmitter.IsPlaying())
							{
								component7.soundLoopEmitter.Stop();
							}
							if ((lerpTime == 0f && weight > 0f) || (lerpTime == 1f && weight < 1f))
							{
								PineFmod.playOneShotSoundAttached(component7.soundStart, component7.gameObject);
							}
							else if ((lerpTime > 0f && weight == 0f) || (lerpTime < 1f && weight == 1f))
							{
								PineFmod.playOneShotSoundAttached(component7.soundEnd, component7.gameObject);
								if (!component7.soundLoop.IsNull && component7.soundLoopEmitter.IsPlaying())
								{
									component7.soundLoopEmitter.Stop();
								}
							}
							else if (!component7.soundLoop.IsNull && !component7.soundLoopEmitter.IsPlaying())
							{
								component7.soundLoopEmitter.Play();
							}
						}
						for (int m = 0; m < component7.tweenStateDataNeo.Count; m++)
						{
							TweenState.TweenStateRecord tweenStateRecord = component7.tweenStateDataNeo[m];
							if (m < objectState.tweenState.Length)
							{
								tweenStateRecord.weight = Mathf.Lerp(objectState2.tweenState[m], objectState.tweenState[m], weight);
								tweenStateRecord.targetWeight = tweenStateRecord.weight;
							}
						}
						if (!Application.isPlaying)
						{
							component7.syncVisuals();
						}
					}
					if ((objectState.flags & 0x40) != 0)
					{
						Vector3 zero = Vector3.zero;
						zero.x = getTargetValueOnCurve(objectState2.Transform_localPosition.x, objectState.Transform_localPosition.x, objectState3.Transform_localPosition.x, objectState4.Transform_localPosition.x);
						zero.y = getTargetValueOnCurve(objectState2.Transform_localPosition.y, objectState.Transform_localPosition.y, objectState3.Transform_localPosition.y, objectState4.Transform_localPosition.y);
						zero.z = getTargetValueOnCurve(objectState2.Transform_localPosition.z, objectState.Transform_localPosition.z, objectState3.Transform_localPosition.z, objectState4.Transform_localPosition.z);
						gameObject.transform.localPosition = zero;
					}
					if ((objectState.flags & 0x10) != 0)
					{
						gameObject.transform.localRotation = Quaternion.Lerp(objectState2.Transform_localRotation, objectState.Transform_localRotation, weight);
					}
					if ((objectState.flags & 0x20) != 0)
					{
						Vector3 zero2 = Vector3.zero;
						zero2.x = getTargetValueOnCurve(objectState2.Transform_localScale.x, objectState.Transform_localScale.x, objectState3.Transform_localScale.x, objectState4.Transform_localScale.x);
						zero2.y = getTargetValueOnCurve(objectState2.Transform_localScale.y, objectState.Transform_localScale.y, objectState3.Transform_localScale.y, objectState4.Transform_localScale.y);
						zero2.z = getTargetValueOnCurve(objectState2.Transform_localScale.z, objectState.Transform_localScale.z, objectState3.Transform_localScale.z, objectState4.Transform_localScale.z);
						gameObject.transform.localScale = zero2;
					}
					if (gameObject.TryGetComponent<AnimationSampler>(out var component8) && (objectState.flags & 0x80) != 0)
					{
						component8.setUnitTime(Mathf.Lerp(objectState2.AnimationSampler_unitTime, objectState.AnimationSampler_unitTime, weight));
					}
				}
			}
			float getTargetValueOnCurve(float prevValue, float curValue, float control1, float control2)
			{
				float min = Mathf.Min(prevValue, curValue);
				float max = Mathf.Max(prevValue, curValue);
				Vector2 zero3 = Vector2.zero;
				if (prevRecord != null)
				{
					float num5 = 0f;
					num5 = ((keyIndex <= 1) ? delayBefore : sequenceKeyFrames[keyIndex - 2].time);
					float x = (prevRecord.time - num5) / 3f;
					Vector2 original = new Vector2(x, control1);
					original = uniformScaleVector(original, keyframeTimeDiff, xAxis: true);
					float num6 = prevValue + original.y;
					float num7 = Mathf.Clamp(num6, min, max);
					float num8 = num6 - num7;
					original = uniformScaleVector(original, original.y - num8, xAxis: false);
					zero3 = new Vector2(prevTime + original.x, prevValue + original.y);
				}
				else
				{
					zero3 = new Vector2(control1Time, prevValue);
				}
				Vector2 key = new Vector2(prevTime, prevValue);
				Vector2 key2 = new Vector2(time, curValue);
				Vector2 handle = new Vector2(control2Time, curValue + control2);
				return evaluateBezier(key, zero3, handle, key2, weight);
			}
		}
	}

	public Vector2 uniformScaleVector(Vector2 original, float targetValue, bool xAxis)
	{
		float num = 1f;
		Vector2 vector = original;
		if (targetValue != 0f)
		{
			num = ((!xAxis) ? (targetValue / original.y) : (targetValue / original.x));
		}
		else if (xAxis)
		{
			vector.x = 0f;
		}
		else
		{
			vector.y = 0f;
		}
		return vector * num;
	}

	public void collectStartStates()
	{
		if (!soundLoop.IsNull && Application.isPlaying)
		{
			soundLoopEmitter = base.gameObject.AddComponent<StudioEventEmitter>();
			soundLoopEmitter.EventReference = soundLoop;
			PineFmod.set3DAttributes(soundLoopEmitter.EventInstance, PineFmod.to3DAttributes(base.transform));
		}
		List<ObjectState> list = new List<ObjectState>();
		List<GameObject> list2 = new List<GameObject>();
		foreach (SequenceRecord sequenceKeyFrame in sequenceKeyFrames)
		{
			foreach (ObjectState item in sequenceKeyFrame.state)
			{
				if (!list2.Contains(item.gameObject))
				{
					list2.Add(item.gameObject);
				}
			}
		}
		captureWholeScene(list, new List<ObjectState>(), null, captureAll: true, list2);
		for (int i = 0; i < sequenceKeyFrames.Count; i++)
		{
			SequenceRecord sequenceRecord = sequenceKeyFrames[i];
			SequenceRecord prevRecord = null;
			if (i > 0)
			{
				prevRecord = sequenceKeyFrames[i - 1];
			}
			sequenceRecord.startStates = new List<ObjectState>();
			sequenceRecord.bezierControl1 = new List<ObjectState>();
			sequenceRecord.bezierControl2 = new List<ObjectState>();
			for (int j = 0; j < sequenceRecord.state.Count; j++)
			{
				sequenceRecord.startStates.Add(new ObjectState());
				sequenceRecord.bezierControl1.Add(new ObjectState());
				sequenceRecord.bezierControl2.Add(new ObjectState());
				ObjectState objectState = sequenceRecord.state[j];
				int flags = objectState.flags;
				int num = 0;
				for (int num2 = i - 1; num2 >= 0; num2--)
				{
					foreach (ObjectState item2 in sequenceKeyFrames[num2].state)
					{
						if (item2.gameObject == objectState.gameObject)
						{
							if (num != 0)
							{
								fillStartState(item2, sequenceRecord.bezierControl1[j], num);
								num = 0;
							}
							fillStartState(item2, sequenceRecord.startStates[j], flags);
							if (num2 == i - 1)
							{
								num = sequenceRecord.startStates[j].flags;
							}
						}
					}
					if (sequenceRecord.startStates[j].flags == flags)
					{
						fillStartState(sequenceRecord.startStates[j], sequenceRecord.bezierControl1[j], sequenceRecord.startStates[j].flags);
						break;
					}
				}
				if (sequenceRecord.startStates[j].flags != flags || sequenceRecord.bezierControl1[j].flags != flags)
				{
					foreach (ObjectState item3 in list)
					{
						if (item3.gameObject == objectState.gameObject)
						{
							fillStartState(item3, sequenceRecord.startStates[j], flags);
							fillStartState(item3, sequenceRecord.bezierControl1[j], flags);
						}
					}
				}
				if (i < sequenceKeyFrames.Count - 1)
				{
					foreach (ObjectState item4 in sequenceKeyFrames[i + 1].state)
					{
						if (item4.gameObject == objectState.gameObject)
						{
							fillStartState(item4, sequenceRecord.bezierControl2[j], flags);
						}
					}
				}
				if (sequenceRecord.bezierControl2[j].flags != flags)
				{
					fillStartState(objectState, sequenceRecord.bezierControl2[j], flags);
				}
				constructBezierControlVectors(sequenceRecord, prevRecord, j);
			}
		}
	}

	private void constructBezierControlVectors(SequenceRecord record, SequenceRecord prevRecord, int j)
	{
		GameObject gameObject = record.state[j].gameObject;
		ObjectState objectState = record.state[j];
		ObjectState objectState2 = record.startStates[j];
		_ = record.bezierControl1[j];
		ObjectState objectState3 = record.bezierControl2[j];
		ObjectState objectState4 = new ObjectState();
		ObjectState objectState5 = new ObjectState();
		if (prevRecord != null)
		{
			for (int i = 0; i < prevRecord.state.Count; i++)
			{
				if (prevRecord.state[i].gameObject == gameObject)
				{
					objectState4 = prevRecord.state[i];
					objectState5 = prevRecord.bezierControl2[i];
				}
			}
		}
		constructForProperty(gameObject, objectState2.Transform_localPosition.x, objectState.Transform_localPosition.x, objectState3.Transform_localPosition.x, objectState4.Transform_localPosition.x, objectState5.Transform_localPosition.x, ref record.bezierControl1[j].Transform_localPosition.x, ref record.bezierControl2[j].Transform_localPosition.x);
		constructForProperty(gameObject, objectState2.Transform_localPosition.y, objectState.Transform_localPosition.y, objectState3.Transform_localPosition.y, objectState4.Transform_localPosition.y, objectState5.Transform_localPosition.y, ref record.bezierControl1[j].Transform_localPosition.y, ref record.bezierControl2[j].Transform_localPosition.y);
		constructForProperty(gameObject, objectState2.Transform_localPosition.z, objectState.Transform_localPosition.z, objectState3.Transform_localPosition.z, objectState4.Transform_localPosition.z, objectState5.Transform_localPosition.z, ref record.bezierControl1[j].Transform_localPosition.z, ref record.bezierControl2[j].Transform_localPosition.z);
		constructForProperty(gameObject, objectState2.Transform_localScale.x, objectState.Transform_localScale.x, objectState3.Transform_localScale.x, objectState4.Transform_localScale.x, objectState5.Transform_localScale.x, ref record.bezierControl1[j].Transform_localScale.x, ref record.bezierControl2[j].Transform_localScale.x);
		constructForProperty(gameObject, objectState2.Transform_localScale.y, objectState.Transform_localScale.y, objectState3.Transform_localScale.y, objectState4.Transform_localScale.y, objectState5.Transform_localScale.y, ref record.bezierControl1[j].Transform_localScale.y, ref record.bezierControl2[j].Transform_localScale.y);
		constructForProperty(gameObject, objectState2.Transform_localScale.z, objectState.Transform_localScale.z, objectState3.Transform_localScale.z, objectState4.Transform_localScale.z, objectState5.Transform_localScale.z, ref record.bezierControl1[j].Transform_localScale.z, ref record.bezierControl2[j].Transform_localScale.z);
		void constructForProperty(GameObject go, float prevStateValue, float currentStateValue, float controlPoint2Value, float prevKeyStateValue, float prevKeyControl2Value, ref float finalControl1, ref float finalControl2)
		{
			float min = Mathf.Min(prevStateValue, currentStateValue);
			float max = Mathf.Max(prevStateValue, currentStateValue);
			float num = 0f;
			if (prevRecord != null)
			{
				num = 0f - prevKeyControl2Value;
			}
			float num2 = (controlPoint2Value - currentStateValue) / 2f;
			float num3 = Mathf.Clamp(currentStateValue - num2, min, max) - currentStateValue;
			finalControl1 = num;
			finalControl2 = num3;
		}
	}

	private void fillStartState(ObjectState prevState, ObjectState fillState, int flags)
	{
		if (checkFlag(16384))
		{
			fillState.Light_Filter = prevState.Light_Filter;
			fillState.flags |= 16384;
		}
		if (checkFlag(32768))
		{
			fillState.Light_Temperature = prevState.Light_Temperature;
			fillState.flags |= 32768;
		}
		if (checkFlag(65536))
		{
			fillState.Light_Intensity = prevState.Light_Intensity;
			fillState.flags |= 65536;
		}
		if (checkFlag(2))
		{
			fillState.Image_color = prevState.Image_color;
			fillState.flags |= 2;
		}
		if (checkFlag(4))
		{
			fillState.Text_color = prevState.Text_color;
			fillState.flags |= 4;
		}
		if (checkFlag(8))
		{
			fillState.Text_fontSize = prevState.Text_fontSize;
			fillState.flags |= 8;
		}
		if (checkFlag(256))
		{
			fillState.Item_examinePivotOffset = prevState.Item_examinePivotOffset;
			fillState.flags |= 256;
		}
		if (checkFlag(512))
		{
			fillState.Item_examineScaleModifier = prevState.Item_examineScaleModifier;
			fillState.flags |= 512;
		}
		if (checkFlag(8192))
		{
			fillState.Item_examineBaseRotation = prevState.Item_examineBaseRotation;
			fillState.flags |= 8192;
		}
		if (checkFlag(131072))
		{
			fillState.Item_groundRotation = prevState.Item_groundRotation;
			fillState.flags |= 131072;
		}
		if (checkFlag(1024))
		{
			fillState.Interactive_targetPriority = prevState.Interactive_targetPriority;
			fillState.flags |= 1024;
		}
		if (checkFlag(2048))
		{
			fillState.MaterialState = prevState.MaterialState;
			fillState.flags |= 2048;
		}
		if (checkFlag(4096))
		{
			fillState.tweenState = prevState.tweenState;
			fillState.flags |= 4096;
		}
		if (checkFlag(64))
		{
			fillState.Transform_localPosition = prevState.Transform_localPosition;
			fillState.flags |= 64;
		}
		if (checkFlag(16))
		{
			fillState.Transform_localRotation = prevState.Transform_localRotation;
			fillState.flags |= 16;
		}
		if (checkFlag(32))
		{
			fillState.Transform_localScale = prevState.Transform_localScale;
			fillState.flags |= 32;
		}
		if (checkFlag(128))
		{
			fillState.AnimationSampler_unitTime = prevState.AnimationSampler_unitTime;
			fillState.flags |= 128;
		}
		bool checkFlag(int flag)
		{
			if ((fillState.flags & flag) == 0 && (flags & flag) != 0)
			{
				return (prevState.flags & flag) != 0;
			}
			return false;
		}
	}

	private void captureWholeScene(List<ObjectState> states, List<ObjectState> startStates, SequenceRecord diffRecord, bool captureAll = false, List<GameObject> overrideAllObjects = null)
	{
		if (!captureAll)
		{
			return;
		}
		if (overrideAllObjects != null)
		{
			foreach (GameObject overrideAllObject in overrideAllObjects)
			{
				if (overrideAllObject != null)
				{
					captureStateRecursive(overrideAllObject.transform, overrideAllObject.transform, states, startStates, diffRecord, captureAll);
				}
			}
			return;
		}
		GameObject[] rootGameObjects = base.gameObject.scene.GetRootGameObjects();
		foreach (GameObject gameObject in rootGameObjects)
		{
			captureStateRecursive(gameObject.transform, gameObject.transform, states, startStates, diffRecord, captureAll);
		}
	}

	private void captureStateRecursive(Transform current, Transform root, List<ObjectState> states, List<ObjectState> startStates, SequenceRecord diffRecord, bool captureAll)
	{
		if (TweenState.captureObjectState(current, diffRecord?.state, root, out var state, out var diffObjectState, Matrix4x4.identity) || captureAll)
		{
			states.Add(state);
			startStates.Add(diffObjectState);
		}
		if (!captureAll)
		{
			return;
		}
		foreach (Transform item in current)
		{
			captureStateRecursive(item, current, states, startStates, diffRecord, captureAll);
		}
	}
}
