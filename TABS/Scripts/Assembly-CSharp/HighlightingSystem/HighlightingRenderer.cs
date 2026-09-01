using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace HighlightingSystem
{
	[AddComponentMenu("Highlighting System/Highlighting Renderer", 2)]
	public class HighlightingRenderer : HighlightingBase
	{
		public static readonly List<HighlightingPreset> defaultPresets = new List<HighlightingPreset>
		{
			new HighlightingPreset
			{
				name = "Default",
				fillAlpha = 0f,
				downsampleFactor = 4,
				iterations = 2,
				blurMinSpread = 0.65f,
				blurSpread = 0.25f,
				blurIntensity = 0.3f,
				blurDirections = BlurDirections.Diagonal
			},
			new HighlightingPreset
			{
				name = "Wide",
				fillAlpha = 0f,
				downsampleFactor = 4,
				iterations = 4,
				blurMinSpread = 0.65f,
				blurSpread = 0.25f,
				blurIntensity = 0.3f,
				blurDirections = BlurDirections.Diagonal
			},
			new HighlightingPreset
			{
				name = "Strong",
				fillAlpha = 0f,
				downsampleFactor = 4,
				iterations = 2,
				blurMinSpread = 0.5f,
				blurSpread = 0.15f,
				blurIntensity = 0.325f,
				blurDirections = BlurDirections.Diagonal
			},
			new HighlightingPreset
			{
				name = "Speed",
				fillAlpha = 0f,
				downsampleFactor = 4,
				iterations = 1,
				blurMinSpread = 0.75f,
				blurSpread = 0f,
				blurIntensity = 0.35f,
				blurDirections = BlurDirections.Diagonal
			},
			new HighlightingPreset
			{
				name = "Quality",
				fillAlpha = 0f,
				downsampleFactor = 2,
				iterations = 3,
				blurMinSpread = 0.5f,
				blurSpread = 0.5f,
				blurIntensity = 0.28f,
				blurDirections = BlurDirections.Diagonal
			},
			new HighlightingPreset
			{
				name = "Solid 1px",
				fillAlpha = 0f,
				downsampleFactor = 1,
				iterations = 1,
				blurMinSpread = 1f,
				blurSpread = 0f,
				blurIntensity = 1f,
				blurDirections = BlurDirections.All
			},
			new HighlightingPreset
			{
				name = "Solid 2px",
				fillAlpha = 0f,
				downsampleFactor = 1,
				iterations = 2,
				blurMinSpread = 1f,
				blurSpread = 0f,
				blurIntensity = 1f,
				blurDirections = BlurDirections.All
			}
		};

		[SerializeField]
		private List<HighlightingPreset> _presets = new List<HighlightingPreset>(defaultPresets);

		private ReadOnlyCollection<HighlightingPreset> _presetsReadonly;

		public ReadOnlyCollection<HighlightingPreset> presets
		{
			get
			{
				if (_presetsReadonly == null)
				{
					_presetsReadonly = _presets.AsReadOnly();
				}
				return _presetsReadonly;
			}
		}

		public bool GetPreset(string name, out HighlightingPreset preset)
		{
			for (int i = 0; i < _presets.Count; i++)
			{
				HighlightingPreset highlightingPreset = _presets[i];
				if (highlightingPreset.name == name)
				{
					preset = highlightingPreset;
					return true;
				}
			}
			preset = default(HighlightingPreset);
			return false;
		}

		public bool AddPreset(HighlightingPreset preset, bool overwrite)
		{
			for (int i = 0; i < _presets.Count; i++)
			{
				if (_presets[i].name == preset.name)
				{
					if (overwrite)
					{
						_presets[i] = preset;
						return true;
					}
					return false;
				}
			}
			_presets.Add(preset);
			return true;
		}

		public bool RemovePreset(string name)
		{
			for (int i = 0; i < _presets.Count; i++)
			{
				if (_presets[i].name == name)
				{
					_presets.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		public bool LoadPreset(string name)
		{
			if (GetPreset(name, out var preset))
			{
				ApplyPreset(preset);
			}
			return false;
		}

		public void ApplyPreset(HighlightingPreset preset)
		{
			base.downsampleFactor = preset.downsampleFactor;
			base.iterations = preset.iterations;
			base.blurMinSpread = preset.blurMinSpread;
			base.blurSpread = preset.blurSpread;
			base.blurIntensity = preset.blurIntensity;
			base.blurDirections = preset.blurDirections;
		}

		public void ClearPresets()
		{
			_presets.Clear();
		}
	}
}
