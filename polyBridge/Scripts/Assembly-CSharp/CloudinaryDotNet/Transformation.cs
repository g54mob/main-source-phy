using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CloudinaryDotNet.Core;

namespace CloudinaryDotNet
{
	public class Transformation : CloudinaryDotNet.Core.ICloneable
	{
		protected Dictionary<string, object> m_transformParams = new Dictionary<string, object>();

		protected List<Transformation> m_nestedTransforms = new List<Transformation>();

		protected string m_htmlWidth;

		protected string m_htmlHeight;

		private const string VARIABLESPARAMKEY = "variables";

		private static readonly string[] SimpleParams = new string[36]
		{
			"ac", "audio_codec", "af", "audio_frequency", "bo", "border", "br", "bit_rate", "cs", "color_space",
			"d", "default_image", "dl", "delay", "dn", "density", "f", "fetch_format", "fps", "fps",
			"g", "gravity", "ki", "keyframe_interval", "l", "overlay", "p", "prefix", "pg", "page",
			"u", "underlay", "vs", "video_sampling", "sp", "streaming_profile"
		};

		private static readonly Transformation DefaultResponsiveWidthTransform = new Transformation().Width("auto").Crop("limit");

		private static Transformation m_responsiveWidthTransform;

		private static readonly Regex RangeValueRe = new Regex("^((?:\\d+\\.)?\\d+)([%pP])?$", RegexOptions.Compiled);

		private static readonly Regex RangeRe = new Regex("^(\\d+\\.)?\\d+[%pP]?\\.\\.(\\d+\\.)?\\d+[%pP]?$", RegexOptions.Compiled);

		public static object DefaultDpr { get; set; }

		public static bool DefaultIsResponsive { get; set; }

		public static Transformation ResponsiveWidthTransform
		{
			get
			{
				if (m_responsiveWidthTransform == null)
				{
					return DefaultResponsiveWidthTransform;
				}
				return m_responsiveWidthTransform;
			}
			set
			{
				m_responsiveWidthTransform = value;
			}
		}

		public Dictionary<string, object> Params => m_transformParams;

		public List<Transformation> NestedTransforms => m_nestedTransforms;

		public bool HiDpi { get; private set; }

		public bool IsResponsive { get; private set; }

		public string HtmlWidth => m_htmlWidth;

		public string HtmlHeight => m_htmlHeight;

		public Transformation Width(object value)
		{
			return Add("width", value);
		}

		public Transformation Height(object value)
		{
			return Add("height", value);
		}

		public Transformation SetHtmlWidth(object value)
		{
			m_htmlWidth = value.ToString();
			return this;
		}

		public Transformation SetHtmlHeight(object value)
		{
			m_htmlHeight = value.ToString();
			return this;
		}

		public Transformation Named(params string[] value)
		{
			return Add("transformation", value);
		}

		public Transformation AspectRatio(double value)
		{
			return AspectRatio(value.ToString(CultureInfo.InvariantCulture));
		}

		public Transformation AspectRatio(int nom, int denom)
		{
			return AspectRatio(string.Format(CultureInfo.InvariantCulture, "{0}:{1}", nom, denom));
		}

		public Transformation AspectRatio(string value)
		{
			return Add("aspect_ratio", value);
		}

		public Transformation Crop(string value)
		{
			return Add("crop", value);
		}

		public Transformation Background(string value)
		{
			return Add("background", Regex.Replace(value, "^#", "rgb:"));
		}

		public Transformation Color(string value)
		{
			return Add("color", Regex.Replace(value, "^#", "rgb:"));
		}

		public Transformation Effect(string value)
		{
			return Add("effect", value);
		}

		public Transformation Effect(string effect, object param)
		{
			return Add("effect", effect + ":" + param);
		}

		public Transformation Angle(int value)
		{
			return Add("angle", value);
		}

		public Transformation Angle(params string[] value)
		{
			return Add("angle", value);
		}

		public Transformation Border(string value)
		{
			return Add("border", value);
		}

		public Transformation Border(int width, string color)
		{
			return Add("border", string.Empty + width + "px_solid_" + Regex.Replace(color, "^#", "rgb:"));
		}

		public Transformation X(object value)
		{
			return Add("x", value);
		}

		public Transformation Y(object value)
		{
			return Add("y", value);
		}

		public Transformation Radius(object value)
		{
			return Add("radius", new Radius(value));
		}

		public Transformation Radius(Radius radius)
		{
			return Add("radius", radius);
		}

		public Transformation Quality(object value)
		{
			return Add("quality", value);
		}

		public Transformation DefaultImage(string value)
		{
			return Add("default_image", value);
		}

		public Transformation Gravity(string value)
		{
			return Add("gravity", value);
		}

		public Transformation Gravity(string value, string param)
		{
			return Gravity(value + ":" + param);
		}

		public Transformation ColorSpace(string value)
		{
			return Add("color_space", value);
		}

		public Transformation Prefix(string value)
		{
			return Add("prefix", value);
		}

		public Transformation Opacity(int value)
		{
			return Add("opacity", value);
		}

		public Transformation Overlay(string value)
		{
			return Add("overlay", value);
		}

		public Transformation Overlay(BaseLayer value)
		{
			return Add("overlay", value);
		}

		public Transformation Underlay(string value)
		{
			return Add("underlay", value);
		}

		public Transformation Underlay(BaseLayer value)
		{
			return Add("underlay", value);
		}

		public Transformation FetchFormat(string value)
		{
			return Add("fetch_format", value);
		}

		public Transformation Density(object value)
		{
			return Add("density", value);
		}

		public Transformation Page(object value)
		{
			return Add("page", value);
		}

		public Transformation Delay(object value)
		{
			return Add("delay", value);
		}

		public Transformation RawTransformation(string value)
		{
			return Add("raw_transformation", value);
		}

		public Transformation Flags(params string[] value)
		{
			return Add("flags", value);
		}

		public Transformation Zoom(int value)
		{
			return Add("zoom", value);
		}

		public Transformation Zoom(string value)
		{
			return Add("zoom", value);
		}

		public Transformation Zoom(float value)
		{
			return Add("zoom", value);
		}

		public Transformation Zoom(double value)
		{
			return Add("zoom", value);
		}

		public Transformation Dpr(object value)
		{
			return Add("dpr", value);
		}

		public Transformation ResponsiveWidth(bool value)
		{
			return Add("responsive_width", value);
		}

		public Condition IfCondition()
		{
			return new Condition().SetParent(this);
		}

		public Transformation IfCondition(string condition)
		{
			return Add("if", condition);
		}

		public Transformation IfCondition(BaseExpression expression)
		{
			return IfCondition(expression.ToString());
		}

		public Transformation IfElse()
		{
			Chain();
			return Add("if", "else");
		}

		public Transformation EndIf()
		{
			Chain();
			for (int num = m_nestedTransforms.Count - 1; num >= 0; num--)
			{
				Transformation transformation = m_nestedTransforms[num];
				if (transformation.Params.ContainsKey("if"))
				{
					object obj = transformation.Params["if"];
					string text = obj.ToString();
					if (text.Equals("end", StringComparison.Ordinal))
					{
						break;
					}
					if (transformation.Params.Count > 1)
					{
						transformation.Params.Remove("if");
						m_nestedTransforms[num] = transformation;
						m_nestedTransforms.Insert(num, new Transformation(string.Format(CultureInfo.InvariantCulture, "if={0}", obj.ToString())));
					}
					if (!string.Equals("else", text, StringComparison.Ordinal))
					{
						break;
					}
				}
			}
			Add("if", "end");
			return Chain();
		}

		public Transformation()
		{
		}

		public Transformation(List<Transformation> transforms)
		{
			if (transforms != null)
			{
				m_nestedTransforms = transforms;
			}
		}

		public Transformation(params string[] transformParams)
		{
			foreach (string text in transformParams)
			{
				string[] array = text.Split('=');
				if (array.Length != 2)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Couldn't parse '{0}'!", text));
				}
				Add(array[0], array[1]);
			}
		}

		public Transformation(Dictionary<string, object> transformParams)
		{
			foreach (string key in transformParams.Keys)
			{
				m_transformParams.Add(key, transformParams[key]);
			}
		}

		public Transformation(Dictionary<string, object>[] dictionary)
		{
			for (int i = 0; i < dictionary.Length; i++)
			{
				if (i == dictionary.Length - 1)
				{
					m_transformParams = dictionary[i];
				}
				else
				{
					m_nestedTransforms.Add(new Transformation(dictionary[i]));
				}
			}
		}

		public Transformation Chain()
		{
			Transformation transformation = Clone();
			transformation.m_nestedTransforms = null;
			m_nestedTransforms.Add(transformation);
			m_transformParams = new Dictionary<string, object>();
			return new Transformation(m_nestedTransforms);
		}

		public Transformation Variable(string name, object value)
		{
			Expression.CheckVariableName(name);
			Add(name, value);
			return this;
		}

		public Transformation Variable(string name, string[] values)
		{
			return Variable(name, "!" + ((values != null) ? string.Join(":", values) : string.Empty) + "!");
		}

		public Transformation Variables(params Expression[] variables)
		{
			Add("variables", variables);
			return this;
		}

		public Transformation CustomFunction(CustomFunction function)
		{
			Add("custom_function", function);
			return this;
		}

		public Transformation CustomPreFunction(CustomFunction function)
		{
			if (!string.IsNullOrEmpty(ToString(function)))
			{
				Add("custom_pre_function", $"pre:{function}");
			}
			return this;
		}

		public Transformation Add(string key, object value)
		{
			if (m_transformParams.ContainsKey(key))
			{
				m_transformParams[key] = value;
			}
			else
			{
				m_transformParams.Add(key, value);
			}
			return this;
		}

		public virtual string Generate()
		{
			List<string> list = new List<string>(m_nestedTransforms.Select((Transformation t) => t.GenerateThis()).ToList());
			string text = GenerateThis();
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(text);
			}
			return string.Join("/", list.ToArray());
		}

		public string GenerateThis()
		{
			string text = GetString(m_transformParams, "size");
			if (text != null)
			{
				string[] array = text.Split("x".ToArray());
				m_transformParams.Add("width", array[0]);
				m_transformParams.Add("height", array[1]);
			}
			string text2 = GetString(m_transformParams, "width");
			string text3 = GetString(m_transformParams, "height");
			if (m_htmlWidth == null)
			{
				m_htmlWidth = text2;
			}
			if (m_htmlHeight == null)
			{
				m_htmlHeight = text3;
			}
			bool num = !string.IsNullOrEmpty(GetString(m_transformParams, "overlay")) || !string.IsNullOrEmpty(GetString(m_transformParams, "underlay"));
			string text4 = GetString(m_transformParams, "crop");
			string text5 = string.Join(".", GetStringArray(m_transformParams, "angle"));
			if (!bool.TryParse(GetString(m_transformParams, "responsive_width"), out var result))
			{
				result = DefaultIsResponsive;
			}
			bool flag = num || !string.IsNullOrEmpty(text5) || text4 == "fit" || text4 == "limit";
			if (!string.IsNullOrEmpty(text2) && (Expression.ValueContainsVariable(text2) || text2.IndexOf("auto", StringComparison.OrdinalIgnoreCase) != -1 || (float.TryParse(text2, out var result2) && result2 < 1f) || flag || result))
			{
				m_htmlWidth = null;
			}
			if (!string.IsNullOrEmpty(text3) && (Expression.ValueContainsVariable(text3) || (float.TryParse(text3, out var result3) && result3 < 1f) || flag || result))
			{
				m_htmlHeight = null;
			}
			string text6 = GetString(m_transformParams, "background");
			if (text6 != null)
			{
				text6 = text6.Replace("^#", "rgb:");
			}
			string text7 = GetString(m_transformParams, "color");
			if (text7 != null)
			{
				text7 = text7.Replace("^#", "rgb:");
			}
			List<string> list = GetStringArray(m_transformParams, "transformation").ToList();
			string value = string.Join(".", list.ToArray());
			list = new List<string>();
			string value2 = string.Join(".", GetStringArray(m_transformParams, "flags"));
			object value3 = null;
			string value4 = null;
			if (m_transformParams.TryGetValue("start_offset", out value3))
			{
				value4 = NormAutoRangeValue(value3);
			}
			string value5 = null;
			if (m_transformParams.TryGetValue("end_offset", out value3))
			{
				value5 = NormRangeValue(value3);
			}
			if (m_transformParams.TryGetValue("offset", out value3))
			{
				string[] array2 = SplitRange(m_transformParams["offset"]);
				if (array2 != null && array2.Length == 2)
				{
					value4 = NormAutoRangeValue(array2[0]);
					value5 = NormRangeValue(array2[1]);
				}
			}
			string value6 = null;
			if (m_transformParams.TryGetValue("duration", out value3))
			{
				value6 = NormRangeValue(value3);
			}
			string value7 = (m_transformParams.TryGetValue("video_codec", out value3) ? ProcessVideoCodec(value3) : null);
			if (!m_transformParams.TryGetValue("dpr", out var value8))
			{
				value8 = DefaultDpr;
			}
			string text8 = ToString(value8);
			if (!string.IsNullOrEmpty(text8) && text8.ToLowerInvariant() == "auto")
			{
				HiDpi = true;
			}
			SortedList<string, string> sortedList = new SortedList<string, string>();
			sortedList.Add("a", BaseExpression<Expression>.Normalize(text5));
			sortedList.Add("ar", BaseExpression<Expression>.Normalize(GetString(m_transformParams, "aspect_ratio")));
			sortedList.Add("b", text6);
			sortedList.Add("c", text4);
			sortedList.Add("co", text7);
			sortedList.Add("dpr", text8);
			sortedList.Add("du", value6);
			sortedList.Add("e", BaseExpression<Expression>.Normalize(GetString(m_transformParams, "effect")));
			sortedList.Add("eo", value5);
			sortedList.Add("fl", value2);
			string value9 = GetString(m_transformParams, "custom_function") ?? GetString(m_transformParams, "custom_pre_function");
			sortedList.Add("fn", value9);
			sortedList.Add("h", BaseExpression<Expression>.Normalize(text3));
			sortedList.Add("o", BaseExpression<Expression>.Normalize(GetString(m_transformParams, "opacity")));
			sortedList.Add("q", BaseExpression<Expression>.Normalize(GetString(m_transformParams, "quality")));
			sortedList.Add("r", BaseExpression<Expression>.Normalize(GetString(m_transformParams, "radius")));
			sortedList.Add("so", value4);
			sortedList.Add("t", value);
			sortedList.Add("vc", value7);
			sortedList.Add("w", BaseExpression<Expression>.Normalize(text2));
			sortedList.Add("x", BaseExpression<Expression>.Normalize(GetString(m_transformParams, "x")));
			sortedList.Add("y", BaseExpression<Expression>.Normalize(GetString(m_transformParams, "y")));
			sortedList.Add("z", BaseExpression<Expression>.Normalize(GetString(m_transformParams, "zoom")));
			for (int i = 0; i < SimpleParams.Length; i += 2)
			{
				if (m_transformParams.TryGetValue(SimpleParams[i + 1], out value3))
				{
					sortedList.Add(SimpleParams[i], ToString(value3));
				}
			}
			List<string> list2 = new List<string>();
			string text9 = GetString(m_transformParams, "if");
			if (!string.IsNullOrEmpty(text9))
			{
				list2.Insert(0, string.Format(CultureInfo.InvariantCulture, "if_{0}", new Condition(text9).ToString()));
			}
			SortedSet<string> sortedSet = new SortedSet<string>();
			foreach (string key in m_transformParams.Keys)
			{
				if (Regex.IsMatch(key, "^\\$[a-zA-Z][a-zA-Z0-9]*$"))
				{
					sortedSet.Add(key + "_" + GetString(m_transformParams, key));
				}
			}
			if (sortedSet.Count > 0)
			{
				list2.Add(string.Join(",", sortedSet));
			}
			string text10 = ((m_transformParams.TryGetValue("variables", out value3) && value3 is Expression[] variables) ? ProcessVariables(variables) : null);
			if (!string.IsNullOrEmpty(text10))
			{
				list2.Add(string.Join(",", text10));
			}
			foreach (KeyValuePair<string, string> item in sortedList)
			{
				if (!string.IsNullOrEmpty(item.Value))
				{
					list2.Add(string.Format(CultureInfo.InvariantCulture, "{0}_{1}", item.Key, item.Value));
				}
			}
			string text11 = GetString(m_transformParams, "raw_transformation");
			if (text11 != null)
			{
				list2.Add(text11);
			}
			if (list2.Count > 0)
			{
				list.Add(string.Join(",", list2.ToArray()));
			}
			if (result)
			{
				list.Add(ResponsiveWidthTransform.Generate());
			}
			if (text2 == "auto" || result)
			{
				IsResponsive = true;
			}
			return string.Join("/", list.ToArray());
		}

		public override string ToString()
		{
			return Generate();
		}

		public Transformation Clone()
		{
			Transformation transformation = (Transformation)MemberwiseClone();
			transformation.m_transformParams = new Dictionary<string, object>();
			foreach (string key in m_transformParams.Keys)
			{
				object obj = m_transformParams[key];
				if (obj is Array)
				{
					transformation.Add(key, ((Array)obj).Clone());
					continue;
				}
				if (obj is string || obj is ValueType || obj is BaseExpression)
				{
					transformation.Add(key, obj);
					continue;
				}
				if (obj is CloudinaryDotNet.Core.ICloneable)
				{
					transformation.Add(key, ((CloudinaryDotNet.Core.ICloneable)obj).Clone());
					continue;
				}
				if (obj is Dictionary<string, string>)
				{
					transformation.Add(key, new Dictionary<string, string>((Dictionary<string, string>)obj));
					continue;
				}
				throw new Exception(string.Format(CultureInfo.InvariantCulture, "Couldn't clone parameter '{0}'!", key));
			}
			if (m_nestedTransforms != null)
			{
				transformation.m_nestedTransforms = new List<Transformation>();
				foreach (Transformation nestedTransform in m_nestedTransforms)
				{
					transformation.m_nestedTransforms.Add(nestedTransform.Clone());
				}
			}
			return transformation;
		}

		object CloudinaryDotNet.Core.ICloneable.Clone()
		{
			return Clone();
		}

		private static string ToString(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is string)
			{
				return obj.ToString();
			}
			if (obj is float || obj is double)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0:0.0#}", obj);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}", obj);
		}

		private static string ProcessVariables(Expression[] variables)
		{
			if (variables == null || variables.Length == 0)
			{
				return null;
			}
			return string.Join(",", variables.Select((Expression v) => v.ToString()).ToArray());
		}

		private static string[] GetStringArray(Dictionary<string, object> options, string key)
		{
			if (!options.ContainsKey(key))
			{
				return new string[0];
			}
			object obj = options[key];
			if (obj is string[])
			{
				return (string[])obj;
			}
			return new List<string> { ToString(obj) }.ToArray();
		}

		private static string GetString(Dictionary<string, object> options, string key)
		{
			if (options.ContainsKey(key))
			{
				return ToString(options[key]);
			}
			return null;
		}

		public Transformation VideoCodec(params string[] codecParams)
		{
			if (codecParams.Length == 1)
			{
				return Add("video_codec", codecParams[0]);
			}
			if (codecParams.Length > 1 && codecParams.Length % 2 == 0)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				for (int i = 0; i < codecParams.Length; i += 2)
				{
					if (!dictionary.ContainsKey(codecParams[i]))
					{
						dictionary.Add(codecParams[i], codecParams[i + 1]);
					}
				}
				return VideoCodec(dictionary);
			}
			throw new ArgumentException("codecParams: please provide either single parameter or a bunch of key-value pairs (key1, value1, key2, value2, ...).");
		}

		public Transformation VideoCodec(Dictionary<string, string> codecParams)
		{
			return Add("video_codec", codecParams);
		}

		public Transformation Fps(string value)
		{
			return Add("fps", BaseExpression<Expression>.Normalize(value));
		}

		public Transformation Fps(double value)
		{
			return Fps($"{value}");
		}

		public Transformation Fps(double? min, double? max)
		{
			if (!min.HasValue && !max.HasValue)
			{
				throw new ArgumentException("Both arguments 'min' and 'max' can not be null.");
			}
			return Fps($"{min}-{max}");
		}

		public Transformation Fps(string min, string max)
		{
			if (string.IsNullOrEmpty(min) && string.IsNullOrEmpty(max))
			{
				throw new ArgumentException("Both arguments 'min' and 'max' can not be null.");
			}
			return Fps(min + "-" + max);
		}

		public Transformation AudioCodec(string codec)
		{
			return Add("audio_codec", codec);
		}

		public Transformation BitRate(int bitRate)
		{
			return Add("bit_rate", bitRate);
		}

		public Transformation BitRate(string bitRate)
		{
			return Add("bit_rate", bitRate);
		}

		public Transformation AudioFrequency(int frequency)
		{
			return Add("audio_frequency", frequency);
		}

		public Transformation AudioFrequency(string frequency)
		{
			return Add("audio_frequency", frequency);
		}

		public Transformation AudioFrequency(AudioFrequency frequency)
		{
			return Add("audio_frequency", ApiShared.GetCloudinaryParam(frequency));
		}

		public Transformation VideoSampling(string value)
		{
			return Add("video_sampling", value);
		}

		public Transformation VideoSamplingFrames(int value)
		{
			return Add("video_sampling", value);
		}

		public Transformation VideoSamplingSeconds(int value)
		{
			return VideoSamplingSeconds((object)value);
		}

		public Transformation VideoSamplingSeconds(float value)
		{
			return VideoSamplingSeconds((object)value);
		}

		public Transformation VideoSamplingSeconds(double value)
		{
			return VideoSamplingSeconds((object)value);
		}

		public Transformation StartOffset(string value)
		{
			return Add("start_offset", value);
		}

		public Transformation StartOffset(float value)
		{
			return Add("start_offset", value);
		}

		public Transformation StartOffset(double value)
		{
			return Add("start_offset", value);
		}

		public Transformation StartOffsetPercent(float value)
		{
			return StartOffsetPercent((object)value);
		}

		public Transformation StartOffsetPercent(double value)
		{
			return StartOffsetPercent((object)value);
		}

		public Transformation StartOffsetAuto()
		{
			return StartOffset("auto");
		}

		public Transformation EndOffset(string value)
		{
			return Add("end_offset", value);
		}

		public Transformation EndOffset(float value)
		{
			return Add("end_offset", value);
		}

		public Transformation EndOffset(double value)
		{
			return Add("end_offset", value);
		}

		public Transformation EndOffsetPercent(float value)
		{
			return EndOffsetPercent((object)value);
		}

		public Transformation EndOffsetPercent(double value)
		{
			return EndOffsetPercent((object)value);
		}

		public Transformation Offset(string value)
		{
			return Add("offset", value);
		}

		public Transformation Offset(params string[] value)
		{
			if (value.Length < 2)
			{
				throw new ArgumentException("Offset range must include at least 2 items.");
			}
			return Add("offset", value);
		}

		public Transformation Offset(params float[] value)
		{
			if (value.Length < 2)
			{
				throw new ArgumentException("Offset range must include at least 2 items.");
			}
			object[] value2 = new object[2]
			{
				value[0],
				value[1]
			};
			return Offset(value2);
		}

		public Transformation Offset(params double[] value)
		{
			if (value.Length < 2)
			{
				throw new ArgumentException("Offset range must include at least 2 items.");
			}
			object[] value2 = new object[2]
			{
				value[0],
				value[1]
			};
			return Offset(value2);
		}

		public Transformation Duration(string value)
		{
			return Add("duration", value);
		}

		public Transformation Duration(float value)
		{
			return Add("duration", value);
		}

		public Transformation Duration(double value)
		{
			return Add("duration", value);
		}

		public Transformation DurationPercent(float value)
		{
			return DurationPercent((object)value);
		}

		public Transformation DurationPercent(double value)
		{
			return DurationPercent((object)value);
		}

		public Transformation KeyframeInterval(int value)
		{
			if (value <= 0)
			{
				throw new ArgumentException("The range for keyframe interval should be greater than 0.");
			}
			return Add("keyframe_interval", string.Format(CultureInfo.InvariantCulture, "{0:0.0}", value));
		}

		public Transformation KeyframeInterval(float value)
		{
			if (value <= 0f)
			{
				throw new ArgumentException("The range for keyframe interval should be greater than 0.");
			}
			return Add("keyframe_interval", value);
		}

		public Transformation KeyframeInterval(string value)
		{
			return Add("keyframe_interval", value);
		}

		public Transformation StreamingProfile(string value)
		{
			return Add("streaming_profile", value);
		}

		private static string ProcessVideoCodec(object codecParam)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (codecParam is string)
			{
				stringBuilder.Append(codecParam);
			}
			else if (codecParam is Dictionary<string, string>)
			{
				string value = null;
				Dictionary<string, string> dictionary = (Dictionary<string, string>)codecParam;
				if (!dictionary.TryGetValue("codec", out value))
				{
					return null;
				}
				stringBuilder.Append(value);
				if (dictionary.TryGetValue("profile", out value))
				{
					stringBuilder.Append(':').Append(value);
					if (dictionary.TryGetValue("level", out value))
					{
						stringBuilder.Append(':').Append(value);
					}
				}
			}
			return stringBuilder.ToString();
		}

		private static string NormAutoRangeValue(object objectValue)
		{
			if (objectValue == null || !string.Equals(objectValue.ToString(), "auto", StringComparison.Ordinal))
			{
				return NormRangeValue(objectValue);
			}
			return objectValue.ToString();
		}

		private static string NormRangeValue(object objectValue)
		{
			if (objectValue == null)
			{
				return null;
			}
			string text = ToString(objectValue);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			Match match = RangeValueRe.Match(text);
			if (!match.Success)
			{
				return null;
			}
			string text2 = string.Empty;
			if (match.Groups.Count == 3 && !string.IsNullOrEmpty(match.Groups[2].Value))
			{
				text2 = "p";
			}
			return match.Groups[1]?.ToString() + text2;
		}

		private static string[] SplitRange(object range)
		{
			if (range is string)
			{
				string text = (string)range;
				if (RangeRe.IsMatch(text))
				{
					return text.Split(new string[1] { ".." }, StringSplitOptions.RemoveEmptyEntries);
				}
			}
			else if (range is Array)
			{
				Array array = (Array)range;
				string[] array2 = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = ToString(array.GetValue(i));
				}
				return array2;
			}
			return null;
		}

		private Transformation VideoSamplingSeconds(object value)
		{
			return Add("video_sampling", ToString(value) + "s");
		}

		private Transformation StartOffsetPercent(object value)
		{
			return Add("start_offset", ToString(value) + "p");
		}

		private Transformation EndOffsetPercent(object value)
		{
			return Add("end_offset", ToString(value) + "p");
		}

		private Transformation Offset(params object[] value)
		{
			if (value.Length < 2)
			{
				throw new ArgumentException("Offset range must include at least 2 items.");
			}
			return Add("offset", value);
		}

		private Transformation DurationPercent(object value)
		{
			return Add("duration", ToString(value) + "p");
		}
	}
}
