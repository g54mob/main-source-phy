using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public sealed class SplitFlipTextureDisplay : MonoBehaviour
{
	public enum FlipDirection
	{
		Up,
		Down
	}

	public enum DirectionMode
	{
		AutoShortest,
		ForceUp,
		ForceDown
	}

	public enum DesiredChangeDetection
	{
		EveryFrame,
		PollInterval
	}

	public enum AdaptiveSpeedMapping
	{
		Linear,
		EaseIn,
		EaseOut,
		EaseInOut
	}

	private List<MeshRenderer> oldRenderers;

	private List<MeshRenderer> newRenderers;

	private Animator animator;

	private string flipUpTrigger;

	private string flipDownTrigger;

	private List<Texture> orderedTextures;

	private string texturePropertyName;

	private bool useInstanciatedMaterials;

	private int initialIndex;

	private int desiredIndex;

	private DirectionMode directionMode;

	private bool preferDownOnTie;

	private bool autoApplyDesiredIndex;

	private bool applyDesiredOnEnable;

	private DesiredChangeDetection desiredChangeDetection;

	private float pollIntervalSeconds;

	private bool adaptiveFlipSpeed;

	private float baselineAnimatorSpeedOverride;

	private float adaptiveMinSpeedMultiplier;

	private float adaptiveMaxSpeedMultiplier;

	private int adaptiveMinDistanceSteps;

	private int adaptiveMaxDistanceSteps;

	private AdaptiveSpeedMapping adaptiveSpeedMapping;

	private bool clearNewTexturesWhenIdle;

	private int _currentCommittedIndex;

	private int _pendingDesiredIndex;

	private bool _isFlipping;

	private int _stagedNextIndex;

	private int _lastObservedDesiredIndex;

	private float _pollTimer;

	private float _baselineAnimatorSpeed;

	private int _texturePropertyID;

	public int CurrentCommittedIndex => _currentCommittedIndex;

	public int PendingDesiredIndex => _pendingDesiredIndex;

	public bool IsFlipping => _isFlipping;

	public int TextureCount
	{
		get
		{
			//IL_0023: Expected I4, but got O
			//IL_0019: Expected I4, but got O
			int num = (int)orderedTextures;
			if (orderedTextures != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal2 @ rax_v2 (System.Int32)+18]");
				return 0;
			}
			return (int)orderedTextures;
		}
	}

	public Texture CurrentCommittedTexture => GetTextureAtIndex(_currentCommittedIndex);

	private void Awake()
	{
		//IL_02ab: Expected I4, but got I8
		//IL_0172: Invalid comparison between F4 and I4
		int texturePropertyID = Shader.PropertyToID(texturePropertyName);
		int currentCommittedIndex = initialIndex;
		_texturePropertyID = texturePropertyID;
		List<Texture> list = orderedTextures;
		if (orderedTextures != null && list._size != 0)
		{
			int num = list._size - 1;
			if (initialIndex >= 0)
			{
				if (initialIndex > num)
				{
					currentCommittedIndex = num;
				}
				goto IL_0262;
			}
		}
		currentCommittedIndex = 0;
		goto IL_0262;
		IL_0262:
		_currentCommittedIndex = currentCommittedIndex;
		int pendingDesiredIndex = desiredIndex;
		List<Texture> list2 = orderedTextures;
		if (orderedTextures != null && list2._size != 0)
		{
			int num2 = list2._size - 1;
			if (desiredIndex >= 0)
			{
				if (desiredIndex > num2)
				{
					pendingDesiredIndex = num2;
				}
				goto IL_027b;
			}
		}
		pendingDesiredIndex = 0;
		goto IL_027b;
		IL_027b:
		_pendingDesiredIndex = pendingDesiredIndex;
		_lastObservedDesiredIndex = desiredIndex;
		_isFlipping = false;
		_stagedNextIndex = -1;
		_pollTimer = 0f;
		if (animator != null)
		{
			float speed = animator.speed;
			_baselineAnimatorSpeed = speed;
			if (baselineAnimatorSpeedOverride > 0f)
			{
				_baselineAnimatorSpeed = baselineAnimatorSpeedOverride;
			}
			animator.speed = _baselineAnimatorSpeed;
		}
		if (useInstanciatedMaterials)
		{
			InstantiateMaterials();
		}
		CommitOldTexture(_currentCommittedIndex);
		if (clearNewTexturesWhenIdle)
		{
			ClearNewTextures();
		}
	}

	private void OnEnable()
	{
		if (autoApplyDesiredIndex)
		{
			bool flag = !applyDesiredOnEnable;
			_lastObservedDesiredIndex = desiredIndex;
			if (!flag)
			{
				ApplyDesiredIndexNow();
			}
		}
	}

	private void Update()
	{
		if (!autoApplyDesiredIndex)
		{
			return;
		}
		if (desiredChangeDetection != DesiredChangeDetection.EveryFrame)
		{
			bool flag = !(0.02f < pollIntervalSeconds);
			float num = 0.02f;
			if (!flag)
			{
				num = pollIntervalSeconds;
			}
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			if (!(num > (_pollTimer = unscaledDeltaTime + _pollTimer)))
			{
				_pollTimer = 0f;
				if (desiredIndex != _lastObservedDesiredIndex)
				{
					_lastObservedDesiredIndex = desiredIndex;
					ApplyDesiredIndexNow();
				}
			}
		}
		else if (desiredIndex != _lastObservedDesiredIndex)
		{
			_lastObservedDesiredIndex = desiredIndex;
			ApplyDesiredIndexNow();
		}
	}

	private void InstantiateMaterials()
	{
		if (oldRenderers != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<MeshRenderer>.Enumerator enumerator = default(List<MeshRenderer>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					if ((object)obj == null)
					{
						throw new NullReferenceException();
					}
					Material sharedMaterial = ((Renderer)obj).GetSharedMaterial();
					Material material = new Material(sharedMaterial);
					((Renderer)obj).SetMaterial(material);
				}
			}
			enumerator.Dispose();
		}
		if (newRenderers == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<MeshRenderer>.Enumerator enumerator2 = default(List<MeshRenderer>.Enumerator);
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator2.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj2 != null)
				{
					if ((object)obj2 == null)
					{
						break;
					}
					Material sharedMaterial2 = ((Renderer)obj2).GetSharedMaterial();
					Material material2 = new Material(sharedMaterial2);
					((Renderer)obj2).SetMaterial(material2);
				}
				continue;
			}
			enumerator2.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public void ApplyDesiredIndexNow()
	{
		int pendingDesiredIndex = desiredIndex;
		List<Texture> list = orderedTextures;
		if (orderedTextures != null && list._size != 0)
		{
			int num = list._size - 1;
			if (desiredIndex >= 0)
			{
				if (desiredIndex > num)
				{
					pendingDesiredIndex = num;
				}
				goto IL_00c8;
			}
		}
		pendingDesiredIndex = 0;
		goto IL_00c8;
		IL_00c8:
		_pendingDesiredIndex = pendingDesiredIndex;
		TryStartNextFlipStep();
	}

	public void SetDesiredIndexAndApply(int index)
	{
		List<Texture> list = orderedTextures;
		int num2;
		if (orderedTextures != null && list._size != 0)
		{
			int num = list._size - 1;
			if (index >= 0)
			{
				bool flag = index <= num;
				num2 = index;
				if (!flag)
				{
					num2 = num;
				}
				goto IL_014e;
			}
		}
		num2 = 0;
		goto IL_014e;
		IL_014e:
		desiredIndex = num2;
		_lastObservedDesiredIndex = num2;
		List<Texture> list2 = orderedTextures;
		if (orderedTextures != null && list2._size != 0)
		{
			int num3 = list2._size - 1;
			if (num2 >= 0)
			{
				if (num2 > num3)
				{
					num2 = num3;
				}
				goto IL_0167;
			}
		}
		num2 = 0;
		goto IL_0167;
		IL_0167:
		_pendingDesiredIndex = num2;
		TryStartNextFlipStep();
	}

	public void OnFlipAnimationFinished()
	{
		//IL_0055: Expected I4, but got I8
		if (_stagedNextIndex >= 0)
		{
			_currentCommittedIndex = _stagedNextIndex;
		}
		CommitOldTexture(_currentCommittedIndex);
		if (clearNewTexturesWhenIdle)
		{
			ClearNewTextures();
		}
		_isFlipping = false;
		_stagedNextIndex = -1;
		TryStartNextFlipStep();
	}

	public void SetIndexInstant(int index)
	{
		//IL_00be: Expected I4, but got I8
		List<Texture> list = orderedTextures;
		int num2;
		if (orderedTextures != null && list._size != 0)
		{
			int num = list._size - 1;
			if (index >= 0)
			{
				bool flag = index <= num;
				num2 = index;
				if (!flag)
				{
					num2 = num;
				}
				goto IL_00f4;
			}
		}
		num2 = 0;
		goto IL_00f4;
		IL_00f4:
		_currentCommittedIndex = num2;
		CommitOldTexture(num2);
		if (clearNewTexturesWhenIdle)
		{
			ClearNewTextures();
		}
		_isFlipping = false;
		_stagedNextIndex = -1;
		ApplyAnimatorSpeed(_baselineAnimatorSpeed);
	}

	private void SnapToDesired()
	{
		//IL_00c4: Expected I4, but got I8
		int num = _pendingDesiredIndex;
		List<Texture> list = orderedTextures;
		if (orderedTextures != null && list._size != 0)
		{
			int num2 = list._size - 1;
			if (_pendingDesiredIndex >= 0)
			{
				if (_pendingDesiredIndex > num2)
				{
					num = num2;
				}
				goto IL_00fa;
			}
		}
		num = 0;
		goto IL_00fa;
		IL_00fa:
		_currentCommittedIndex = num;
		CommitOldTexture(num);
		if (clearNewTexturesWhenIdle)
		{
			ClearNewTextures();
		}
		_isFlipping = false;
		_stagedNextIndex = -1;
		ApplyAnimatorSpeed(_baselineAnimatorSpeed);
	}

	private void TryStartNextFlipStep()
	{
		//IL_00ab: Expected O, but got I4
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected I4, but got Unknown
		//IL_00e0: Expected O, but got I4
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected I4, but got Unknown
		//IL_01a9: Expected O, but got I4
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected I4, but got Unknown
		//IL_01d8: Expected O, but got I4
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Expected I4, but got Unknown
		//IL_02a7: Expected O, but got I4
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Expected O, but got Unknown
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Expected I4, but got Unknown
		//IL_027f: Expected O, but got I4
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Expected O, but got Unknown
		if (_isFlipping)
		{
			return;
		}
		List<Texture> list = orderedTextures;
		if (orderedTextures == null || list._size == 0 || _pendingDesiredIndex == _currentCommittedIndex)
		{
			ApplyAnimatorSpeed(_baselineAnimatorSpeed);
			return;
		}
		List<Texture> list2 = orderedTextures;
		int num4;
		int num5;
		if (orderedTextures != null && list2._size != 0)
		{
			object obj = list2._size - _currentCommittedIndex;
			object obj2 = obj + _pendingDesiredIndex;
			int num = obj2 % list2._size;
			object obj3 = list2._size - _pendingDesiredIndex;
			object obj4 = obj3 + _currentCommittedIndex;
			int num2 = obj4 % list2._size;
			if (directionMode != DirectionMode.ForceUp && (directionMode == DirectionMode.ForceDown || (num >= num2 && (num > num2 || preferDownOnTie))))
			{
				object obj5 = _currentCommittedIndex - 1;
				object obj6 = obj5 + list2._size;
				int num3 = obj6 % list2._size;
				num4 = 1;
				num5 = num3;
				goto IL_0443;
			}
			object obj7 = _currentCommittedIndex + 1;
			int num6 = obj7 % list2._size;
			num5 = num6;
		}
		else
		{
			num5 = 0;
		}
		num4 = 0;
		goto IL_0443;
		IL_0443:
		List<Texture> list3 = orderedTextures;
		bool flag = orderedTextures == null;
		int remainingSteps = 0;
		if (!flag)
		{
			bool flag2 = list3._size <= 0;
			remainingSteps = 0;
			if (!flag2)
			{
				object obj9;
				if (num4 != 0)
				{
					object obj8 = list3._size - _pendingDesiredIndex;
					obj9 = obj8 + _currentCommittedIndex;
				}
				else
				{
					object obj10 = list3._size - _currentCommittedIndex;
					obj9 = obj10 + _pendingDesiredIndex;
				}
				int num7 = obj9 % list3._size;
				remainingSteps = num7;
			}
		}
		UpdateAnimatorSpeedForRemainingSteps(remainingSteps);
		_stagedNextIndex = num5;
		StageNewTexture(num5);
		if (this.animator != null)
		{
			Animator animator;
			string trigger;
			if (num4 != 0)
			{
				if (string.IsNullOrEmpty(flipDownTrigger))
				{
					goto IL_0332;
				}
				animator = this.animator;
				trigger = flipDownTrigger;
			}
			else
			{
				if (string.IsNullOrEmpty(flipUpTrigger))
				{
					goto IL_0332;
				}
				animator = this.animator;
				trigger = flipUpTrigger;
			}
			animator.SetTrigger(trigger);
		}
		goto IL_0332;
		IL_0332:
		_isFlipping = true;
	}

	private void Trigger(FlipDirection direction)
	{
		if (!(this.animator != null))
		{
			return;
		}
		Animator animator;
		string trigger;
		if (direction != FlipDirection.Up)
		{
			if (string.IsNullOrEmpty(flipDownTrigger))
			{
				return;
			}
			animator = this.animator;
			trigger = flipDownTrigger;
		}
		else
		{
			if (string.IsNullOrEmpty(flipUpTrigger))
			{
				return;
			}
			animator = this.animator;
			trigger = flipUpTrigger;
		}
		animator.SetTrigger(trigger);
	}

	private void CommitOldTexture(int index)
	{
		if (oldRenderers == null)
		{
			return;
		}
		Texture textureAtIndex = GetTextureAtIndex(index);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<MeshRenderer>.Enumerator enumerator = default(List<MeshRenderer>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					if ((object)obj == null)
					{
						break;
					}
					Material material = ((Renderer)obj).GetMaterial();
					if (material != null)
					{
						Material material2 = ((Renderer)obj).GetMaterial();
						material2.SetTexture(_texturePropertyID, textureAtIndex);
					}
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	private void StageNewTexture(int index)
	{
		if (newRenderers == null)
		{
			return;
		}
		Texture textureAtIndex = GetTextureAtIndex(index);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<MeshRenderer>.Enumerator enumerator = default(List<MeshRenderer>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					if ((object)obj == null)
					{
						break;
					}
					Material material = ((Renderer)obj).GetMaterial();
					if (material != null)
					{
						Material material2 = ((Renderer)obj).GetMaterial();
						material2.SetTexture(_texturePropertyID, textureAtIndex);
					}
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	private void ClearNewTextures()
	{
		if (newRenderers == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<MeshRenderer>.Enumerator enumerator = default(List<MeshRenderer>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					if ((object)obj == null)
					{
						break;
					}
					Material material = ((Renderer)obj).GetMaterial();
					if (material != null)
					{
						Material material2 = ((Renderer)obj).GetMaterial();
						material2.SetTexture(_texturePropertyID, null);
					}
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	private Texture GetTextureAtIndex(int index)
	{
		if (orderedTextures != null)
		{
			List<Texture> list = orderedTextures;
			if (list._size != 0)
			{
				List<Texture> list2 = orderedTextures;
				if (orderedTextures != null && list2._size != 0)
				{
					int num = list2._size - 1;
					List<Texture> list3;
					if (index >= 0)
					{
						bool flag = index <= num;
						int num2 = index;
						if (!flag)
						{
							list3 = orderedTextures;
							num2 = num;
							goto IL_0106;
						}
					}
					else
					{
						int num2 = 0;
					}
					list3 = orderedTextures;
				}
				else
				{
					List<Texture> list3 = orderedTextures;
					bool flag2 = orderedTextures == null;
					int num2 = 0;
					if (flag2)
					{
						return (Texture)(object)new NullReferenceException();
					}
				}
				goto IL_0106;
			}
		}
		return null;
		IL_0106:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		Texture result = default(Texture);
		return result;
	}

	private int ClampIndex(int index)
	{
		List<Texture> list = orderedTextures;
		if (orderedTextures != null && list._size != 0)
		{
			int num = list._size - 1;
			int result;
			if (index >= 0)
			{
				bool flag = index <= num;
				result = index;
				if (!flag)
				{
					return num;
				}
			}
			else
			{
				result = 0;
			}
			return result;
		}
		return 0;
	}

	private unsafe void ChooseDirectionAndNext(int currentIndex, int desiredIndex, out FlipDirection direction, out int nextIndex)
	{
		//IL_01ae: Expected O, but got I4
		//IL_0039: Expected O, but got I4
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected I4, but got Unknown
		//IL_006a: Expected O, but got I4
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected I4, but got Unknown
		//IL_017d: Expected O, but got I4
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected I4, but got Unknown
		//IL_0197: Expected O, but got I4
		//IL_012c: Expected O, but got I4
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected I4, but got Unknown
		//IL_0161: Expected O, but got I4
		List<Texture> list = orderedTextures;
		if (orderedTextures != null && list._size != 0)
		{
			object obj = list._size - currentIndex;
			object obj2 = obj + desiredIndex;
			int num = obj2 % list._size;
			object obj3 = list._size - desiredIndex;
			object obj4 = obj3 + currentIndex;
			int num2 = obj4 % list._size;
			if (directionMode != DirectionMode.ForceUp && (directionMode == DirectionMode.ForceDown || (num >= num2 && (num > num2 || preferDownOnTie))))
			{
				object obj5 = currentIndex - 1;
				ref FlipDirection reference = ref *(FlipDirection*)1;
				object obj6 = obj5 + list._size;
				int num3 = obj6 % list._size;
				object obj7 = num3;
			}
			else
			{
				ref FlipDirection reference = ref *(FlipDirection*)null;
				object obj8 = currentIndex + 1;
				int num4 = obj8 % list._size;
				object obj7 = num4;
			}
		}
		else
		{
			ref FlipDirection reference = ref *(FlipDirection*)null;
			object obj7 = 0;
		}
	}

	private int ComputeRemainingStepsInDirection(int currentIndex, int desiredIndex, FlipDirection direction)
	{
		//IL_0097: Expected O, but got I4
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected I4, but got Unknown
		//IL_0059: Expected O, but got I4
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected I4, but got Unknown
		List<Texture> list = orderedTextures;
		if (orderedTextures != null && list._size > 0)
		{
			if (direction != FlipDirection.Up)
			{
				object obj = list._size - desiredIndex;
				object obj2 = obj + currentIndex;
				return obj2 % list._size;
			}
			object obj3 = list._size - currentIndex;
			object obj4 = obj3 + desiredIndex;
			return obj4 % list._size;
		}
		return 0;
	}

	private void UpdateAnimatorSpeedForRemainingSteps(int remainingSteps)
	{
		//IL_0325: Invalid comparison between I4 and F4
		//IL_0166: Expected F4, but got I4
		//IL_00c8: Expected O, but got I4
		//IL_00d5: Expected O, but got I4
		//IL_00ed: Invalid comparison between I4 and F4
		//IL_00b6: Expected F4, but got I4
		//IL_017b: Expected O, but got I4
		//IL_038c: Invalid comparison between I4 and F4
		//IL_02a7: Expected F4, but got I4
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		if (!adaptiveFlipSpeed || !(animator != null))
		{
			return;
		}
		bool flag = remainingSteps < 0;
		int num = 0;
		if (!flag)
		{
			num = remainingSteps;
		}
		int num2 = adaptiveMinDistanceSteps;
		if (adaptiveMinDistanceSteps < 1)
		{
			num2 = 1;
		}
		int num3 = adaptiveMaxDistanceSteps;
		if (adaptiveMaxDistanceSteps < 1)
		{
			num3 = 1;
		}
		bool flag2 = num3 < num2;
		int num4 = num2;
		if (!flag2)
		{
			num4 = num3;
		}
		if (num4 == num2)
		{
			goto IL_0121;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018041B83Bh\"");
		float num5;
		if (num2 != num4)
		{
			object obj = num4 - num2;
			object obj2 = num - num2;
			num5 = (float)obj2 / (float)obj;
			if (!(0f > num5))
			{
				if (num5 > 1f)
				{
					goto IL_0121;
				}
				goto IL_031c;
			}
		}
		num5 = 0f;
		goto IL_031c;
		IL_031c:
		if (!(0f > num5))
		{
			if (num5 > 1f)
			{
				num5 = 1f;
			}
		}
		else
		{
			num5 = 0f;
		}
		bool flag3 = adaptiveSpeedMapping == AdaptiveSpeedMapping.Linear;
		if (!flag3)
		{
			object obj3 = adaptiveSpeedMapping - 1;
			if (!flag3)
			{
				object obj4 = obj3 - 1;
				if (!flag3)
				{
					if ((nint)obj4 == 1)
					{
						float num6 = num5 + num5;
						float num7 = 3f - num6;
						float num8 = num5 * num5;
						num5 = num7 * num8;
					}
				}
				else
				{
					float num9 = 1f - num5;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
					num5 = 1f - num9;
				}
			}
			else
			{
				float num10 = num5 * num5;
				num5 = num10;
			}
		}
		bool flag4 = !(0.0001f < adaptiveMinSpeedMultiplier);
		float num11 = 0.0001f;
		if (!flag4)
		{
			num11 = adaptiveMinSpeedMultiplier;
		}
		bool flag5 = !(0.0001f < adaptiveMaxSpeedMultiplier);
		float num12 = 0.0001f;
		if (!flag5)
		{
			num12 = adaptiveMaxSpeedMultiplier;
		}
		if (!(0f > num5))
		{
			if (num5 > 1f)
			{
				num5 = 1f;
			}
		}
		else
		{
			num5 = 0f;
		}
		float num13 = num12 - num11;
		float num14 = num13 * num5;
		float num15 = num14 + num11;
		float speed = num15 * _baselineAnimatorSpeed;
		ApplyAnimatorSpeed(speed);
		return;
		IL_0121:
		num5 = 1f;
		goto IL_031c;
	}

	private static float ApplyMapping(float t01, AdaptiveSpeedMapping mapping)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_005c: Expected F4, but got I4
		//IL_006f: Expected O, but got I4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		float num;
		if (!(0f > t01))
		{
			bool flag = !(t01 > 1f);
			num = t01;
			if (!flag)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		bool flag2 = mapping == AdaptiveSpeedMapping.Linear;
		if (!flag2)
		{
			object obj = mapping - 1;
			if (!flag2)
			{
				object obj2 = obj - 1;
				if (flag2)
				{
					float num2 = 1f - num;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
					return 1f - num2;
				}
				if ((nint)obj2 == 1)
				{
					float num3 = num + num;
					float num4 = num * num;
					float num5 = 3f - num3;
					return num5 * num4;
				}
			}
			else
			{
				num *= num;
			}
		}
		return num;
	}

	private void ApplyAnimatorSpeed(float speed)
	{
		if (animator != null)
		{
			bool flag = !(0.0001f < speed);
			float speed2 = 0.0001f;
			if (!flag)
			{
				speed2 = speed;
			}
			animator.speed = speed2;
		}
	}

	public SplitFlipTextureDisplay()
	{
		List<MeshRenderer> list = new List<MeshRenderer>();
		oldRenderers = list;
		newRenderers = new List<MeshRenderer>();
		flipUpTrigger = "FlipUp";
		flipDownTrigger = "FlipDown";
		orderedTextures = new List<Texture>();
		texturePropertyName = "_MainTex";
		useInstanciatedMaterials = true;
		preferDownOnTie = true;
		applyDesiredOnEnable = true;
		pollIntervalSeconds = 0.05f;
		adaptiveFlipSpeed = true;
		adaptiveMinSpeedMultiplier = 1f;
		adaptiveMaxSpeedMultiplier = 3f;
		adaptiveMinDistanceSteps = 1;
		adaptiveMaxDistanceSteps = 12;
		adaptiveSpeedMapping = AdaptiveSpeedMapping.EaseOut;
		clearNewTexturesWhenIdle = true;
		_baselineAnimatorSpeed = 1f;
		base._002Ector();
	}
}
