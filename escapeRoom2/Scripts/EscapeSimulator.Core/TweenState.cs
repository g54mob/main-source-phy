using System;
using System.Collections.Generic;
using System.Text;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TweenState : MonoBehaviour
{
	[Serializable]
	public class TweenStateRecord : IReadWrite
	{
		public string name;

		[FormerlySerializedAs("state")]
		public List<ObjectState> states = new List<ObjectState>();

		public float weight;

		public float targetWeight;

		public float speed = 1f;

		public float delay;

		public virtual void Write(FastBinaryWriter writer)
		{
			writer.Write(name);
			writer.WriteList(states, delegate(FastBinaryWriter w, ObjectState e)
			{
				w.WriteIReadWrite(e);
			});
			writer.Write(in weight, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in targetWeight, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in speed, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in delay, default(FastBinaryWriter.ForPrimitives));
		}

		public virtual void Read(FastBinaryReader reader)
		{
			name = reader.ReadString();
			states = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<ObjectState>());
			weight = reader.ReadSingle();
			targetWeight = reader.ReadSingle();
			speed = reader.ReadSingle();
			delay = reader.ReadSingle();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("name: " + ToStringHelper.Stringify(name));
			stringBuilder.AppendLine("states: " + ToStringHelper.Stringify(states, (ObjectState e) => ToStringHelper.Stringify(e)));
			stringBuilder.AppendLine("weight: " + $"{weight}");
			stringBuilder.AppendLine("targetWeight: " + $"{targetWeight}");
			stringBuilder.AppendLine("speed: " + $"{speed}");
			stringBuilder.Append("delay: " + $"{delay}");
			return stringBuilder.ToString();
		}
	}

	public const int Flag_Image_color = 2;

	public const int Flag_Text_color = 4;

	public const int Flag_Text_fontSize = 8;

	public const int Flag_Transform_localRotation = 16;

	public const int Flag_Transform_localScale = 32;

	public const int Flag_Transform_localPosition = 64;

	public const int Flag_AnimationSampler_unitTime = 128;

	public const int Flag_Item_examinePivotOffset = 256;

	public const int Flag_Item_examineScaleModifier = 512;

	public const int Flag_Interactive_targetPriority = 1024;

	public const int Flag_MaterialState = 2048;

	public const int Flag_TweenState = 4096;

	public const int Flag_ExamineBaseRotation = 8192;

	public const int Flag_Light_Filter = 16384;

	public const int Flag_Light_Temperature = 32768;

	public const int Flag_Light_Intensity = 65536;

	public const int Flag_Item_groundRotation = 131072;

	public const int ID_Flag_Image_color = 1;

	public const int ID_Flag_Text_color = 2;

	public const int ID_Flag_Text_fontSize = 3;

	public const int ID_Flag_Transform_localRotation = 4;

	public const int ID_Flag_Transform_localScale = 5;

	public const int ID_Flag_Transform_localPosition = 6;

	public const int ID_Flag_AnimationSampler_unitTime = 7;

	public const int ID_Flag_Item_examinePivotOffset = 8;

	public const int ID_Flag_Item_examineScaleModifier = 9;

	public const int ID_Flag_Interactive_targetPriority = 10;

	public const int ID_Flag_MaterialState = 11;

	public const int ID_Flag_TweenState = 12;

	public const int ID_Flag_ExamineBaseRotation = 13;

	public const int ID_Flag_Light_Filter = 14;

	public const int ID_Flag_Light_Temperature = 15;

	public const int ID_Flag_Light_Intensity = 16;

	public const int ID_Flag_Item_groundRotation = 17;

	private const int WeightCount = 18;

	public const string DefaultRecord = "Default";

	[HideInInspector]
	public TweenStateData tweenStateData;

	public TweenStateData overrideTweenStateData;

	[ReadOnly]
	public List<TweenStateRecord> tweenStateDataNeo = new List<TweenStateRecord>();

	[NonSerialized]
	public Matrix4x4 localTweenOffset;

	[NonSerialized]
	public Matrix4x4 lastRootTransform;

	private TweenStateRecord defaultRecord;

	public Interpolation interpolation = Interpolation.SmootherStep;

	[HideInInspector]
	public bool ignoreAudio;

	[HideInInspector]
	public EventReference soundStart;

	[HideInInspector]
	public EventReference soundLoop;

	[HideInInspector]
	public EventReference soundEnd;

	[HideInInspector]
	public StudioEventEmitter soundLoopEmitter;

	public void init()
	{
		if (overrideTweenStateData != null)
		{
			initOverrideTween();
		}
		localTweenOffset = Matrix4x4.TRS(getUniversalLocalPosition(base.gameObject), base.transform.localRotation, base.transform.localScale);
		lastRootTransform = localTweenOffset;
		List<ObjectState> states = new List<ObjectState>();
		captureStateRecursive(base.transform, base.transform, states, null);
		defaultRecord = new TweenStateRecord
		{
			name = "Default",
			states = states,
			weight = 1f
		};
		foreach (ObjectState state in defaultRecord.states)
		{
			state.flags = 0;
			state.allWeights = new float[18];
			state.validState = true;
		}
		foreach (TweenStateRecord item in tweenStateDataNeo)
		{
			foreach (ObjectState state2 in item.states)
			{
				foreach (ObjectState state3 in defaultRecord.states)
				{
					if (state3.gameObject == state2.gameObject)
					{
						state3.flags |= state2.flags;
						state2.defaultState = state3;
						state2.validState = true;
					}
				}
			}
		}
		if (!soundLoop.IsNull && Application.isPlaying)
		{
			soundLoopEmitter = base.gameObject.AddComponent<StudioEventEmitter>();
			soundLoopEmitter.EventReference = soundLoop;
			PineFmod.set3DAttributes(soundLoopEmitter.EventInstance, PineFmod.to3DAttributes(base.transform));
		}
	}

	public void relinkStates()
	{
		foreach (TweenStateRecord item in tweenStateDataNeo)
		{
			foreach (ObjectState state in item.states)
			{
				if (state.gameObject == null || !state.gameObject.transform.IsChildOf(base.transform))
				{
					Debug.Log(state.path + " GO link is missing, trying to recreate link " + base.gameObject);
					Transform transform = base.transform.Find(state.path);
					state.gameObject = ((transform == null) ? null : transform.gameObject);
				}
			}
		}
	}

	public void initOverrideTween()
	{
		if (overrideTweenStateData == null)
		{
			return;
		}
		TweenStateData tweenStateData = UnityEngine.Object.Instantiate(overrideTweenStateData);
		tweenStateDataNeo = tweenStateData.tweenStates;
		UnityEngine.Object.DestroyImmediate(tweenStateData);
		foreach (TweenStateRecord item in tweenStateDataNeo)
		{
			item.targetWeight = 0f;
			item.weight = 0f;
			foreach (ObjectState state in item.states)
			{
				Transform transform = base.transform.Find(state.path);
				state.gameObject = ((transform == null) ? null : transform.gameObject);
			}
		}
	}

	public static TweenStateContext createContext(GameObject root)
	{
		TweenStateContext tweenStateContext = new TweenStateContext();
		tweenStateContext.tweenStates = root.GetComponentsInChildren<TweenState>(includeInactive: true);
		TweenState[] tweenStates = tweenStateContext.tweenStates;
		for (int i = 0; i < tweenStates.Length; i++)
		{
			tweenStates[i].init();
		}
		return tweenStateContext;
	}

	private void OnEnable()
	{
	}

	public TweenStateRecord findStateByName(string name)
	{
		if (!(name == "Default"))
		{
			return tweenStateDataNeo.Find((TweenStateRecord x) => x.name == name);
		}
		return defaultRecord;
	}

	public static string getTransformPath(Transform current, Transform root)
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

	public bool hasRecord(string record)
	{
		return findStateByName(record) != null;
	}

	public float getWeight(string record)
	{
		TweenStateRecord tweenStateRecord = findStateByName(record);
		if (tweenStateRecord == null)
		{
			Debug.LogError("Cannot find state '" + record + "' on '" + base.name + "'.", this);
			return 0f;
		}
		return tweenStateRecord.weight;
	}

	public void setWeight(string record, float weight)
	{
		setWeight(findStateByName(record), weight);
	}

	public void setWeight(TweenStateRecord record, float weight)
	{
		if (PineFmod.isPlaying(soundLoopEmitter))
		{
			PineFmod.stop(soundLoopEmitter);
		}
		record.weight = weight;
		record.targetWeight = weight;
		syncVisuals();
	}

	public void setState(string record, float weight = 1f)
	{
		setState(findStateByName(record), weight);
	}

	public void setState(TweenStateRecord record, float weight = 1f)
	{
		foreach (TweenStateRecord item in tweenStateDataNeo)
		{
			item.weight = ((record == item) ? weight : 0f);
			item.targetWeight = item.weight;
		}
		syncVisuals();
	}

	public void transitionToDuration(string record, float duration = 1f, float weight = 1f, bool playSound = true, float delay = 0f)
	{
		transitionToDuration(findStateByName(record), duration, weight, playSound, delay);
	}

	public void transitionToDuration(TweenStateRecord record, float duration = 1f, float weight = 1f, bool playSound = true, float delay = 0f)
	{
		transitionTo(record, 1f / duration, weight, playSound, delay);
	}

	public void transitionTo(string record, float speed = 1f, float weight = 1f, bool playSound = true, float delay = 0f)
	{
		transitionTo(findStateByName(record), speed, weight, playSound, delay);
	}

	public void transitionTo(TweenStateRecord record, float speed = 1f, float weight = 1f, bool playSound = true, float delay = 0f)
	{
		foreach (TweenStateRecord item in tweenStateDataNeo)
		{
			updateRecord(item);
		}
		if (playSound && record != null)
		{
			PineFmod.playOneShotSoundAttached(soundStart, base.gameObject);
		}
		syncVisuals();
		void updateRecord(TweenStateRecord current)
		{
			current.targetWeight = ((record == current) ? weight : 0f);
			current.speed = speed;
			current.delay = delay;
		}
	}

	public void transitionToStateAdditive(string record, float speed = 1f, float weight = 1f)
	{
		transitionToStateAdditive(findStateByName(record), speed, weight);
	}

	public void transitionToStateAdditive(TweenStateRecord record, float speed = 1f, float weight = 1f)
	{
		record.targetWeight = weight;
		record.speed = speed;
		syncVisuals();
	}

	public void update(float dt, List<string> transitionCompleteEvents)
	{
		bool changed = false;
		bool completedEvent = false;
		foreach (TweenStateRecord item in tweenStateDataNeo)
		{
			updateWeights(item);
		}
		if (!changed)
		{
			return;
		}
		syncVisuals();
		if (completedEvent)
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
		void updateWeights(TweenStateRecord record)
		{
			float num = record.weight;
			if (record.delay > 0f)
			{
				record.delay = Mathf.MoveTowards(record.delay, 0f, dt);
			}
			else
			{
				num = Mathf.MoveTowards(record.weight, record.targetWeight, record.speed * dt);
			}
			if (num != record.weight)
			{
				if (num == record.targetWeight)
				{
					transitionCompleteEvents.Add(record.name);
					completedEvent = true;
				}
				record.weight = num;
				changed = true;
			}
		}
	}

	private static Vector3 getUniversalLocalPosition(GameObject go)
	{
		if (go.TryGetComponent<RectTransform>(out var component))
		{
			return new Vector3(component.anchoredPosition.x, component.anchoredPosition.y, 0f);
		}
		return go.transform.localPosition;
	}

	private static void setUniversalLocalPosition(GameObject go, Vector3 position)
	{
		RectTransform component2;
		if (go.TryGetComponent<Switch3D>(out var component))
		{
			if (!component.hasRigidbody)
			{
				go.transform.localPosition = position;
				return;
			}
			Vector3 position2 = go.transform.TransformPoint(position);
			component.rigidbody.position = position2;
			UnityUtils.drawSphereAt(position2, default(Color), 0.02f, "Debug Sphere", 5f);
		}
		else if (go.TryGetComponent<RectTransform>(out component2))
		{
			component2.anchoredPosition = new Vector2(position.x, position.y);
		}
		else
		{
			go.transform.localPosition = position;
		}
	}

	public void syncVisuals()
	{
		Matrix4x4 matrix4x = Matrix4x4.TRS(getUniversalLocalPosition(base.gameObject), base.transform.localRotation, base.transform.localScale);
		if (matrix4x != lastRootTransform)
		{
			localTweenOffset = matrix4x * (localTweenOffset.inverse * lastRootTransform).inverse;
		}
		foreach (ObjectState state2 in defaultRecord.states)
		{
			Array.Clear(state2.allWeights, 0, state2.allWeights.Length);
		}
		foreach (TweenStateRecord item in tweenStateDataNeo)
		{
			foreach (ObjectState state3 in item.states)
			{
				if (!state3.validState)
				{
					continue;
				}
				for (int i = 0; i < state3.defaultState.allWeights.Length; i++)
				{
					if ((state3.flags & (1 << i)) != 0)
					{
						state3.defaultState.allWeights[i] += item.weight;
					}
				}
			}
		}
		syncRecord(defaultRecord, 1f, defaultRecord: true);
		foreach (TweenStateRecord item2 in tweenStateDataNeo)
		{
			syncRecord(item2, item2.weight, defaultRecord: false);
		}
		Matrix4x4 matrix4x2 = Matrix4x4.TRS(getUniversalLocalPosition(base.gameObject), base.transform.localRotation, base.transform.localScale);
		Matrix4x4 matrix = localTweenOffset * matrix4x2;
		decomposeMatrix(in matrix, out var position, out var rotation, out var scale);
		setUniversalLocalPosition(base.gameObject, position);
		base.transform.localRotation = rotation;
		base.transform.localScale = scale;
		lastRootTransform = matrix;
		void syncRecord(TweenStateRecord record, float weight, bool defaultRecord)
		{
			weight = interpolation.evaluate(weight);
			foreach (ObjectState state4 in record.states)
			{
				ObjectState state = state4;
				if (state.validState)
				{
					GameObject gameObject = state.gameObject;
					if (!(gameObject == null) && gameObject.transform.IsChildOf(base.transform))
					{
						if (gameObject.TryGetComponent<Light>(out var component))
						{
							if ((state.flags & 0x4000) != 0)
							{
								if (state.defaultState == null)
								{
									component.color = state.Light_Filter;
								}
								else
								{
									component.color += (state.Light_Filter - state.defaultState.Light_Filter) * calculateBlendWeight(14);
								}
							}
							if ((state.flags & 0x8000) != 0)
							{
								if (state.defaultState == null)
								{
									component.colorTemperature = state.Light_Temperature;
								}
								else
								{
									component.colorTemperature += (state.Light_Temperature - state.defaultState.Light_Temperature) * calculateBlendWeight(15);
								}
							}
							if ((state.flags & 0x10000) != 0)
							{
								if (state.defaultState == null)
								{
									component.intensity = state.Light_Intensity;
								}
								else
								{
									component.intensity += (state.Light_Intensity - state.defaultState.Light_Intensity) * calculateBlendWeight(16);
								}
							}
						}
						if (gameObject.TryGetComponent<Image>(out var component2) && (state.flags & 2) != 0)
						{
							if (state.defaultState == null)
							{
								component2.color = state.Image_color;
							}
							else
							{
								component2.color += (state.Image_color - state.defaultState.Image_color) * calculateBlendWeight(1);
							}
						}
						if (gameObject.TryGetComponent<Text>(out var component3))
						{
							if ((state.flags & 4) != 0)
							{
								if (state.defaultState == null)
								{
									component3.color = state.Text_color;
								}
								else
								{
									component3.color += (state.Text_color - state.defaultState.Text_color) * calculateBlendWeight(2);
								}
							}
							if ((state.flags & 8) != 0)
							{
								if (state.defaultState == null)
								{
									component3.fontSize = state.Text_fontSize;
								}
								else
								{
									component3.fontSize += (int)((float)(state.Text_fontSize - state.defaultState.Text_fontSize) * calculateBlendWeight(3));
								}
							}
						}
						if (gameObject.TryGetComponent<Item>(out var component4))
						{
							if ((state.flags & 0x100) != 0)
							{
								if (state.defaultState == null)
								{
									component4.examinePivotOffset = state.Item_examinePivotOffset;
								}
								else
								{
									Vector3 vector = state.Item_examinePivotOffset - state.defaultState.Item_examinePivotOffset;
									component4.examinePivotOffset += vector * weight;
								}
							}
							if ((state.flags & 0x200) != 0)
							{
								if (state.defaultState == null)
								{
									component4.examineScaleModifier = state.Item_examineScaleModifier;
								}
								else
								{
									float num = state.Item_examineScaleModifier - state.defaultState.Item_examineScaleModifier;
									component4.examineScaleModifier += num * weight;
								}
							}
							if ((state.flags & 0x2000) != 0)
							{
								if (state.defaultState == null)
								{
									component4.examineBaseRotation = state.Item_examineBaseRotation;
								}
								else
								{
									Vector3 vector2 = state.Item_examineBaseRotation - state.defaultState.Item_examineBaseRotation;
									component4.examineBaseRotation += vector2 * weight;
								}
							}
							if ((state.flags & 0x20000) != 0)
							{
								if (state.defaultState == null)
								{
									component4.groundRotation = state.Item_groundRotation;
								}
								else
								{
									Vector3 vector3 = state.Item_groundRotation - state.defaultState.Item_groundRotation;
									component4.groundRotation += vector3 * weight;
								}
							}
						}
						if (gameObject.TryGetComponent<Interactive>(out var component5) && (state.flags & 0x400) != 0)
						{
							component5.targetPriority = (int)Mathf.Lerp(component5.targetPriority, state.Interactive_targetPriority, weight);
						}
						if (gameObject.TryGetComponent<MaterialState>(out var component6) && (state.flags & 0x800) != 0)
						{
							for (int j = 0; j < component6.materialStateData.materialStates.Count; j++)
							{
								MaterialState.MaterialStateRecord materialStateRecord = component6.materialStateData.materialStates[j];
								if (state.MaterialState == null)
								{
									state.MaterialState = new float[0];
								}
								if (j < state.MaterialState.Length)
								{
									materialStateRecord.weight = Mathf.Lerp(materialStateRecord.weight, state.MaterialState[j], weight);
									materialStateRecord.targetWeight = materialStateRecord.weight;
								}
							}
							component6.syncVisuals();
						}
						if ((state.flags & 0x40) != 0 || (gameObject == base.gameObject && defaultRecord))
						{
							if (state.defaultState == null)
							{
								setUniversalLocalPosition(gameObject, state.Transform_localPosition);
							}
							else
							{
								Vector3 vector4 = state.Transform_localPosition - state.defaultState.Transform_localPosition;
								setUniversalLocalPosition(gameObject, getUniversalLocalPosition(gameObject) + vector4 * weight);
							}
						}
						if ((state.flags & 0x10) != 0 || (gameObject == base.gameObject && defaultRecord))
						{
							if (state.defaultState == null)
							{
								gameObject.transform.localRotation = state.Transform_localRotation;
							}
							else
							{
								Quaternion b = Quaternion.Inverse(state.defaultState.Transform_localRotation) * state.Transform_localRotation;
								gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Lerp(Quaternion.identity, b, weight);
							}
						}
						if ((state.flags & 0x20) != 0 || (gameObject == base.gameObject && defaultRecord))
						{
							if (state.defaultState == null)
							{
								gameObject.transform.localScale = state.Transform_localScale;
							}
							else
							{
								Vector3 vector5 = state.Transform_localScale - state.defaultState.Transform_localScale;
								gameObject.transform.localScale += vector5 * weight;
							}
						}
						if (gameObject.TryGetComponent<AnimationSampler>(out var component7) && (state.flags & 0x80) != 0)
						{
							component7.setUnitTime(Mathf.Lerp(component7.unitTime, state.AnimationSampler_unitTime, weight));
						}
					}
				}
				float calculateBlendWeight(int flagId)
				{
					float num2 = state.defaultState.allWeights[flagId];
					if (num2 == 0f)
					{
						return 0f;
					}
					return weight / num2;
				}
			}
		}
	}

	private Matrix4x4 getTRSMatrix(Transform transform)
	{
		return Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
	}

	private void captureStateRecursive(Transform current, Transform root, List<ObjectState> states, List<ObjectState> diffStates)
	{
		Matrix4x4 matrix4x = ((current == root) ? localTweenOffset : Matrix4x4.identity);
		captureObjectState(current, diffStates, root, out var state, out var _, matrix4x);
		if (state.flags != 0)
		{
			states.Add(state);
		}
		foreach (Transform item in current)
		{
			captureStateRecursive(item, root, states, diffStates);
		}
	}

	public static bool captureObjectState(Transform current, List<ObjectState> diffStates, Transform root, out ObjectState state, out ObjectState diffObjectState, Matrix4x4 localTweenOffset)
	{
		state = new ObjectState();
		state.gameObject = current.gameObject;
		state.path = getTransformPath(current, root);
		diffObjectState = null;
		if (diffStates != null)
		{
			foreach (ObjectState diffState in diffStates)
			{
				if (diffState.gameObject == current.gameObject)
				{
					diffObjectState = diffState;
				}
			}
		}
		if (current.TryGetComponent<Light>(out var component))
		{
			state.Light_Filter = component.color;
			if (diffObjectState?.Light_Filter != state.Light_Filter)
			{
				state.flags |= 0x4000;
			}
			state.Light_Temperature = component.colorTemperature;
			if (diffObjectState?.Light_Temperature != state.Light_Temperature)
			{
				state.flags |= 0x8000;
			}
			state.Light_Intensity = component.intensity;
			if (diffObjectState?.Light_Intensity != state.Light_Intensity)
			{
				state.flags |= 0x10000;
			}
		}
		if (current.TryGetComponent<Image>(out var component2))
		{
			state.Image_color = component2.color;
			if (diffObjectState?.Image_color != state.Image_color)
			{
				state.flags |= 2;
			}
		}
		if (current.TryGetComponent<Text>(out var component3))
		{
			state.Text_color = component3.color;
			if (diffObjectState?.Text_color != state.Text_color)
			{
				state.flags |= 4;
			}
			state.Text_fontSize = component3.fontSize;
			if (diffObjectState?.Text_fontSize != state.Text_fontSize)
			{
				state.flags |= 8;
			}
		}
		if (current.TryGetComponent<Item>(out var component4))
		{
			state.Item_examinePivotOffset = component4.examinePivotOffset;
			if (diffObjectState?.Item_examinePivotOffset != state.Item_examinePivotOffset)
			{
				state.flags |= 0x100;
			}
			state.Item_examineScaleModifier = component4.examineScaleModifier;
			if (diffObjectState?.Item_examineScaleModifier != state.Item_examineScaleModifier)
			{
				state.flags |= 0x200;
			}
			state.Item_examineBaseRotation = component4.examineBaseRotation;
			if (diffObjectState?.Item_examineBaseRotation != state.Item_examineBaseRotation)
			{
				state.flags |= 0x2000;
			}
			state.Item_groundRotation = component4.groundRotation;
			if (diffObjectState?.Item_groundRotation != state.Item_groundRotation)
			{
				state.flags |= 0x20000;
			}
		}
		if (current.TryGetComponent<Interactive>(out var component5))
		{
			state.Interactive_targetPriority = component5.targetPriority;
			if (diffObjectState?.Interactive_targetPriority != state.Interactive_targetPriority)
			{
				state.flags |= 0x400;
			}
		}
		if (current.TryGetComponent<MaterialState>(out var component6) && component6.materialStateData != null)
		{
			int count = component6.materialStateData.materialStates.Count;
			state.MaterialState = new float[count];
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				MaterialState.MaterialStateRecord materialStateRecord = component6.materialStateData.materialStates[i];
				state.MaterialState[i] = materialStateRecord.weight;
				ObjectState obj = diffObjectState;
				if (((obj != null) ? new float?(obj.MaterialState[i]) : ((float?)null)) != materialStateRecord.weight)
				{
					flag = true;
				}
			}
			if (flag)
			{
				state.flags |= 0x800;
			}
		}
		if (current.TryGetComponent<TweenState>(out var component7))
		{
			int count2 = component7.tweenStateDataNeo.Count;
			state.tweenState = new float[count2];
			bool flag2 = false;
			for (int j = 0; j < count2; j++)
			{
				TweenStateRecord tweenStateRecord = component7.tweenStateDataNeo[j];
				state.tweenState[j] = tweenStateRecord.weight;
				ObjectState obj2 = diffObjectState;
				if (((obj2 != null) ? new float?(obj2.tweenState[j]) : ((float?)null)) != tweenStateRecord.weight)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				state.flags |= 0x1000;
			}
		}
		if (current.TryGetComponent<AnimationSampler>(out var component8))
		{
			state.AnimationSampler_unitTime = component8.editor_unitTime;
			if (diffObjectState?.AnimationSampler_unitTime != state.AnimationSampler_unitTime)
			{
				state.flags |= 0x80;
			}
		}
		Matrix4x4 inverse = localTweenOffset.inverse;
		Matrix4x4 matrix4x = Matrix4x4.TRS(getUniversalLocalPosition(current.gameObject), current.transform.localRotation, current.transform.localScale);
		decomposeMatrix(inverse * matrix4x, out var position, out var rotation, out var scale);
		state.Transform_localPosition = position;
		if (diffObjectState?.Transform_localPosition != state.Transform_localPosition)
		{
			state.flags |= 0x40;
		}
		state.Transform_localRotation = rotation;
		if (diffObjectState?.Transform_localRotation != state.Transform_localRotation)
		{
			state.flags |= 0x10;
		}
		state.Transform_localScale = scale;
		if (diffObjectState?.Transform_localScale != state.Transform_localScale)
		{
			state.flags |= 0x20;
		}
		return state.flags != 0;
	}

	public static void decomposeMatrix(in Matrix4x4 matrix, out Vector3 position, out Quaternion rotation, out Vector3 scale)
	{
		position = new Vector3(matrix.m03, matrix.m13, matrix.m23);
		Vector3 vector = matrix.GetColumn(0);
		Vector3 upwards = matrix.GetColumn(1);
		Vector3 forward = matrix.GetColumn(2);
		scale = new Vector3(vector.magnitude, upwards.magnitude, forward.magnitude);
		vector /= scale.x;
		upwards /= scale.y;
		forward /= scale.z;
		if (Matrix4x4.Determinant(matrix) < 0f)
		{
			scale.x = 0f - scale.x;
		}
		rotation = Quaternion.LookRotation(forward, upwards);
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
}
