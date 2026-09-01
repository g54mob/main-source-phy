using System;
using System.Collections.Generic;
using TFBGames;
using UnityEngine;

namespace HighlightingSystem
{
	[AddComponentMenu("Highlighting System/Highlighter", 0)]
	public class Highlighter : HighlighterCore, IHighlight
	{
		protected const float HALFPI = (float)Math.PI / 2f;

		[SerializeField]
		protected bool _overlay;

		[SerializeField]
		protected bool _occluder;

		protected Color _hoverColor = Color.red;

		protected int _hoverFrame = -1;

		[SerializeField]
		protected bool _tween;

		[SerializeField]
		protected Gradient _tweenGradient = new Gradient
		{
			colorKeys = new GradientColorKey[2]
			{
				new GradientColorKey(new Color(0f, 1f, 1f, 1f), 0f),
				new GradientColorKey(new Color(0f, 1f, 1f, 1f), 1f)
			},
			alphaKeys = new GradientAlphaKey[2]
			{
				new GradientAlphaKey(0f, 0f),
				new GradientAlphaKey(1f, 1f)
			}
		};

		[SerializeField]
		protected float _tweenDuration = 1f;

		[SerializeField]
		protected bool _tweenReverse;

		[SerializeField]
		protected LoopMode _tweenLoop = LoopMode.PingPong;

		[SerializeField]
		protected Easing _tweenEasing;

		[SerializeField]
		protected float _tweenDelay;

		[SerializeField]
		protected int _tweenRepeatCount = -1;

		[SerializeField]
		protected bool _tweenUseUnscaledTime;

		[SerializeField]
		protected bool _constant;

		[SerializeField]
		protected Color _constantColor = Color.yellow;

		[SerializeField]
		protected float _constantFadeInTime = 0.1f;

		[SerializeField]
		protected float _constantFadeOutTime = 0.25f;

		[SerializeField]
		protected Easing _constantEasing;

		[SerializeField]
		protected bool _constantUseUnscaledTime;

		[SerializeField]
		protected RendererFilterMode _filterMode;

		[SerializeField]
		protected List<Transform> _filterList = new List<Transform>();

		protected bool _tweenEnabled;

		protected float _tweenStart;

		protected bool _constantEnabled;

		protected float _constantStart;

		protected float _constantDuration;

		private GradientColorKey[] flashingColorKeys = new GradientColorKey[2]
		{
			new GradientColorKey(new Color(0f, 1f, 1f, 0f), 0f),
			new GradientColorKey(new Color(0f, 1f, 1f, 1f), 1f)
		};

		private GradientAlphaKey[] flashingAlphaKeys = new GradientAlphaKey[2]
		{
			new GradientAlphaKey(1f, 0f),
			new GradientAlphaKey(1f, 1f)
		};

		public bool overlay
		{
			get
			{
				return _overlay;
			}
			set
			{
				_overlay = value;
			}
		}

		public bool occluder
		{
			get
			{
				return _occluder;
			}
			set
			{
				_occluder = value;
			}
		}

		public bool tween
		{
			get
			{
				return _tween;
			}
			set
			{
				TweenSet(value);
			}
		}

		public Gradient tweenGradient
		{
			get
			{
				return _tweenGradient;
			}
			set
			{
				_tweenGradient = value;
			}
		}

		public float tweenDuration
		{
			get
			{
				return _tweenDuration;
			}
			set
			{
				_tweenDuration = value;
				ValidateRanges();
			}
		}

		public float tweenDelay
		{
			get
			{
				return _tweenDelay;
			}
			set
			{
				_tweenDelay = value;
			}
		}

		public bool tweenUseUnscaledTime
		{
			get
			{
				return _tweenUseUnscaledTime;
			}
			set
			{
				if (_tweenUseUnscaledTime != value)
				{
					float num = GetTweenTime() - _tweenStart;
					_tweenUseUnscaledTime = value;
					_tweenStart = GetTweenTime() - num;
				}
			}
		}

		public LoopMode tweenLoop
		{
			get
			{
				return _tweenLoop;
			}
			set
			{
				_tweenLoop = value;
			}
		}

		public Easing tweenEasing
		{
			get
			{
				return _tweenEasing;
			}
			set
			{
				_tweenEasing = value;
			}
		}

		public bool tweenReverse
		{
			get
			{
				return _tweenReverse;
			}
			set
			{
				_tweenReverse = value;
			}
		}

		public int tweenRepeatCount
		{
			get
			{
				return _tweenRepeatCount;
			}
			set
			{
				_tweenRepeatCount = value;
			}
		}

		public bool constant
		{
			get
			{
				return _constant;
			}
			set
			{
				ConstantSet(value);
			}
		}

		public Color constantColor
		{
			get
			{
				return _constantColor;
			}
			set
			{
				_constantColor = value;
			}
		}

		public float constantFadeTime
		{
			set
			{
				_constantFadeInTime = value;
				_constantFadeOutTime = value;
				ValidateRanges();
				ConstantSet();
			}
		}

		public float constantFadeInTime
		{
			get
			{
				return _constantFadeInTime;
			}
			set
			{
				_constantFadeInTime = value;
				ValidateRanges();
				if (_constant)
				{
					ConstantSet();
				}
			}
		}

		public float constantFadeOutTime
		{
			get
			{
				return _constantFadeOutTime;
			}
			set
			{
				_constantFadeOutTime = value;
				ValidateRanges();
				if (!_constant)
				{
					ConstantSet();
				}
			}
		}

		public bool constantUseUnscaledTime
		{
			get
			{
				return _constantUseUnscaledTime;
			}
			set
			{
				if (_constantUseUnscaledTime != value)
				{
					float num = GetConstantTime() - _constantStart;
					_constantUseUnscaledTime = value;
					_constantStart = GetConstantTime() - num;
				}
			}
		}

		public Easing constantEasing
		{
			get
			{
				return _constantEasing;
			}
			set
			{
				_constantEasing = value;
			}
		}

		public RendererFilterMode filterMode
		{
			get
			{
				return _filterMode;
			}
			set
			{
				if (_filterMode != value)
				{
					_filterMode = value;
					SetDirty();
				}
			}
		}

		public List<Transform> filterList => _filterList;

		protected override RendererFilter rendererFilterToUse
		{
			get
			{
				if (base.rendererFilter != null)
				{
					return base.rendererFilter;
				}
				if (_filterMode == RendererFilterMode.None)
				{
					if (HighlighterCore.globalRendererFilter == null)
					{
						return HighlighterCore.DefaultRendererFilter;
					}
					return HighlighterCore.globalRendererFilter;
				}
				if (_filterMode == RendererFilterMode.Include)
				{
					return TransformFilterInclude;
				}
				if (_filterMode == RendererFilterMode.Exclude)
				{
					return TransformFilterExclude;
				}
				return HighlighterCore.DefaultRendererFilter;
			}
		}

		protected bool hover
		{
			get
			{
				return _hoverFrame == Time.frameCount;
			}
			set
			{
				_hoverFrame = (value ? Time.frameCount : (-1));
			}
		}

		protected float constantValue
		{
			get
			{
				if (!(_constantDuration > 0f))
				{
					return 1f;
				}
				return Mathf.Clamp01((GetConstantTime() - _constantStart) / _constantDuration);
			}
		}

		public bool IsHighlighted => base.enabled;

		protected virtual void OnDidApplyAnimationProperties()
		{
			ValidateAll();
		}

		protected override void OnEnableSafe()
		{
			ValidateAll();
		}

		protected override void OnDisableSafe()
		{
			_tweenEnabled = false;
			_constantEnabled = false;
			_constantStart = GetConstantTime() - _constantDuration;
		}

		protected void ValidateAll()
		{
			ValidateRanges();
			TweenSet();
			ConstantSet();
			SetDirty();
		}

		protected void ValidateRanges()
		{
			if (_tweenDuration < 0f)
			{
				_tweenDuration = 0f;
			}
			if (_constantFadeInTime < 0f)
			{
				_constantFadeInTime = 0f;
			}
			if (_constantFadeOutTime < 0f)
			{
				_constantFadeInTime = 0f;
			}
		}

		public void Hover(Color color)
		{
			_hoverColor = color;
			hover = true;
		}

		public void ConstantOn(float fadeTime = 0.25f)
		{
			ConstantSet(fadeTime, value: true);
		}

		public void ConstantOn(Color color, float fadeTime = 0.25f)
		{
			_constantColor = color;
			ConstantSet(fadeTime, value: true);
		}

		public void ConstantOff(float fadeTime = 0.25f)
		{
			ConstantSet(fadeTime, value: false);
		}

		public void ConstantSwitch(float fadeTime = 0.25f)
		{
			ConstantSet(fadeTime, !_constant);
		}

		public void ConstantOnImmediate()
		{
			ConstantSet(0f, value: true);
		}

		public void ConstantOnImmediate(Color color)
		{
			_constantColor = color;
			ConstantSet(0f, value: true);
		}

		public void ConstantOffImmediate()
		{
			ConstantSet(0f, value: false);
		}

		public void ConstantSwitchImmediate()
		{
			ConstantSet(0f, !_constant);
		}

		public void Off()
		{
			hover = false;
			TweenSet(value: false);
			ConstantSet(0f, value: false);
		}

		public void TweenStart()
		{
			TweenSet(value: true);
		}

		public void TweenStop()
		{
			TweenSet(value: false);
		}

		public void TweenSet(bool value)
		{
			_tween = value;
			if (_tweenEnabled != _tween)
			{
				_tweenEnabled = _tween;
				_tweenStart = GetTweenTime();
			}
		}

		public void ConstantSet(float fadeTime, bool value)
		{
			if (fadeTime < 0f)
			{
				fadeTime = 0f;
			}
			if (_constantDuration != fadeTime)
			{
				float constantTime = GetConstantTime();
				_constantStart = ((_constantDuration > 0f) ? (constantTime - fadeTime / _constantDuration * (constantTime - _constantStart)) : (constantTime - fadeTime));
				_constantDuration = fadeTime;
			}
			_constant = value;
			if (_constantEnabled != _constant)
			{
				_constantEnabled = _constant;
				_constantStart = GetConstantTime() - _constantDuration * (1f - constantValue);
			}
		}

		protected void TweenSet()
		{
			TweenSet(_tween);
		}

		protected void ConstantSet()
		{
			ConstantSet(_constant);
		}

		protected void ConstantSet(bool value)
		{
			ConstantSet(value ? constantFadeInTime : _constantFadeOutTime, value);
		}

		protected override void UpdateHighlighting()
		{
			if (hover)
			{
				color = _hoverColor;
				mode = (_overlay ? HighlighterMode.Overlay : HighlighterMode.Default);
				return;
			}
			if (_tweenEnabled)
			{
				float num = GetTweenTime() - (_tweenStart + _tweenDelay);
				if (num >= 0f)
				{
					float x = ((_tweenDuration > 0f) ? (num / _tweenDuration) : 0f);
					x = Loop(x, _tweenLoop, _tweenReverse, _tweenRepeatCount);
					if (x >= 0f)
					{
						x = Ease(x, _tweenEasing);
						color = _tweenGradient.Evaluate(x);
						mode = (_overlay ? HighlighterMode.Overlay : HighlighterMode.Default);
						return;
					}
				}
			}
			float num2 = (_constantEnabled ? constantValue : (1f - constantValue));
			if (num2 > 0f)
			{
				num2 = Ease(num2, _constantEasing);
				color = _constantColor;
				color.a *= num2;
				mode = (_overlay ? HighlighterMode.Overlay : HighlighterMode.Default);
			}
			else if (_occluder)
			{
				mode = HighlighterMode.Occluder;
			}
			else
			{
				mode = HighlighterMode.Disabled;
			}
		}

		protected bool TransformFilterInclude(Renderer renderer, List<int> submeshIndices)
		{
			Transform transform = renderer.transform;
			for (int i = 0; i < _filterList.Count; i++)
			{
				Transform transform2 = _filterList[i];
				if (!(transform2 == null) && transform.IsChildOf(transform2))
				{
					submeshIndices.Add(-1);
					return true;
				}
			}
			return false;
		}

		protected bool TransformFilterExclude(Renderer renderer, List<int> submeshIndices)
		{
			Transform transform = renderer.transform;
			for (int i = 0; i < _filterList.Count; i++)
			{
				Transform transform2 = _filterList[i];
				if (!(transform2 == null) && transform.IsChildOf(transform2))
				{
					return false;
				}
			}
			submeshIndices.Add(-1);
			return true;
		}

		protected float Loop(float x, LoopMode loop, bool reverse = false, int repeatCount = -1)
		{
			float num = -1f;
			switch (loop)
			{
			default:
				if (x >= 0f && x <= 1f)
				{
					num = x;
				}
				break;
			case LoopMode.Loop:
			{
				int num3 = Mathf.FloorToInt(x);
				if (repeatCount < 0 || num3 < repeatCount)
				{
					num = x - (float)num3;
				}
				break;
			}
			case LoopMode.PingPong:
			{
				int num2 = Mathf.FloorToInt(x * 0.5f);
				if (repeatCount < 0 || num2 < repeatCount)
				{
					num = 1f - Mathf.Abs(x - (float)num2 * 2f - 1f);
				}
				break;
			}
			case LoopMode.ClampForever:
				num = Mathf.Clamp01(x);
				break;
			}
			if (num >= 0f && reverse)
			{
				num = 1f - num;
			}
			return num;
		}

		protected float Ease(float x, Easing easing)
		{
			x = Mathf.Clamp01(x);
			switch (easing)
			{
			default:
				return x;
			case Easing.QuadIn:
				return x * x;
			case Easing.QuadOut:
				return (0f - x) * (x - 2f);
			case Easing.QuadInOut:
				return (x < 0.5f) ? (2f * x * x) : (2f * x * (2f - x) - 1f);
			case Easing.CubicIn:
				return x * x * x;
			case Easing.CubicOut:
				x -= 1f;
				return x * x * x + 1f;
			case Easing.CubicInOut:
				if (x < 0.5f)
				{
					return 4f * x * x * x;
				}
				x = 2f * x - 2f;
				return 0.5f * (x * x * x + 2f);
			case Easing.SineIn:
				return 1f - Mathf.Cos(x * ((float)Math.PI / 2f));
			case Easing.SineOut:
				return Mathf.Sin(x * ((float)Math.PI / 2f));
			case Easing.SineInOut:
				return -0.5f * (Mathf.Cos(x * (float)Math.PI) - 1f);
			}
		}

		protected float GetTweenTime()
		{
			if (!_tweenUseUnscaledTime)
			{
				return Time.time;
			}
			return Time.unscaledTime;
		}

		protected float GetConstantTime()
		{
			if (!_constantUseUnscaledTime)
			{
				return Time.time;
			}
			return Time.unscaledTime;
		}

		public static Color HSVToRGB(float hue, float saturation, float value)
		{
			float num = 6f * Mathf.Clamp01(hue);
			saturation = Mathf.Clamp01(saturation);
			value = Mathf.Clamp01(value);
			return new Color(value * (1f + (Mathf.Clamp01(Mathf.Max(2f - num, num - 4f)) - 1f) * saturation), value * (1f + (Mathf.Clamp01(Mathf.Min(num, 4f - num)) - 1f) * saturation), value * (1f + (Mathf.Clamp01(Mathf.Min(num - 2f, 6f - num)) - 1f) * saturation));
		}

		[Obsolete]
		public void On()
		{
			hover = true;
		}

		[Obsolete]
		public void On(Color color)
		{
			Hover(color);
		}

		[Obsolete]
		public void OnParams(Color color)
		{
			_hoverColor = color;
		}

		[Obsolete]
		public void ConstantParams(Color color)
		{
			_constantColor = color;
		}

		[Obsolete]
		public void FlashingParams(Color color1, Color color2, float freq)
		{
			flashingColorKeys[0].color = color1;
			flashingColorKeys[1].color = color2;
			_tweenGradient.SetKeys(flashingColorKeys, flashingAlphaKeys);
			_tweenDuration = 1f / freq;
		}

		[Obsolete]
		public void FlashingOn()
		{
			TweenSet(value: true);
		}

		[Obsolete]
		public void FlashingOn(Color color1, Color color2)
		{
			flashingColorKeys[0].color = color1;
			flashingColorKeys[1].color = color2;
			_tweenGradient.SetKeys(flashingColorKeys, flashingAlphaKeys);
			TweenSet(value: true);
		}

		[Obsolete]
		public void FlashingOn(Color color1, Color color2, float freq)
		{
			flashingColorKeys[0].color = color1;
			flashingColorKeys[1].color = color2;
			_tweenGradient.SetKeys(flashingColorKeys, flashingAlphaKeys);
			_tweenDuration = 1f / freq;
			TweenSet(value: true);
		}

		[Obsolete]
		public void FlashingOn(float freq)
		{
			_tweenDuration = 1f / freq;
			TweenSet(value: true);
		}

		[Obsolete]
		public void FlashingOff()
		{
			TweenSet(value: false);
		}

		[Obsolete]
		public void FlashingSwitch()
		{
			tween = !tween;
		}

		public void BeginHighlight()
		{
			_constant = true;
			base.enabled = true;
		}

		public void EndHighlight()
		{
			_constant = false;
			base.enabled = false;
		}

		public void SetHighlightColor(Color newHighlightColor)
		{
			constantColor = newHighlightColor;
			_hoverColor = newHighlightColor;
		}
	}
}
