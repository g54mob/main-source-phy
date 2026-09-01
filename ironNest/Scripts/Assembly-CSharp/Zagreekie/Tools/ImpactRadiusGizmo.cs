using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zagreekie.Tools
{
	[DisallowMultipleComponent]
	[AddComponentMenu("Zagreekie.Tools/Debug/Impact Radius Gizmo")]
	public class ImpactRadiusGizmo : MonoBehaviour
	{
		public enum OrientationPlane
		{
			XZ_Ground = 0,
			XY = 1,
			YZ = 2,
			FaceCamera = 3
		}

		[Serializable]
		public struct RadiusRing
		{
			[Tooltip("World-space radius of this ring, in meters (Unity units). Drawn as a circle centered on this GameObject's position.")]
			public float Radius;

			[Tooltip("Color used for both the ring outline and its radius label, including its alpha channel — lower alpha draws a more transparent/faded ring and label so it reads as a secondary reference rather than the shell's actual radius. Pick distinct colors per ring so they stay easy to tell apart in Scene view.")]
			public Color Color;

			public RadiusRing(float radius, Color color)
			{
				Radius = 0f;
				Color = default(Color);
			}
		}

		[Header("Radius Rings")]
		[Tooltip("The radii to visualize as concentric rings around this GameObject's origin, each with its own color and label.\n\nThis list IS the 'input field' for adding a custom radius: use the list's '+' button (or change the Size field) to add a new entry, type a Radius value and pick a Color, and it appears immediately in the Scene view — no Play Mode or extra setup needed. Use '-' or reduce Size to remove one.\n\nDefaults to a common measurement ladder: 0.1, 0.25, 0.5, 0.75, 1, 1.25, 1.5, each at 50% opacity (alpha 0.5) so they read as reference guides rather than the shell's actual radius — see the ShellReference ring below for that. Right-click the component header and choose 'Reset' to restore these defaults at any time.")]
		public List<RadiusRing> Rings;

		[Header("Shell Definition Reference (Optional)")]
		[Tooltip("Optional. Drag a ShellDefinition asset here to automatically draw its ImpactRadius as an extra ring, colored using 'Shell Reference Color' below and labeled with the shell's DisplayName (falls back to ShellId if DisplayName is empty). Use this to visually confirm a crater's on-screen size matches the shell's actual gameplay ImpactRadius. Leave empty to skip. Read-only: this never modifies the ShellDefinition asset, and has no runtime effect.")]
		public ShellDefinition ShellReference;

		[Tooltip("Color used for the automatically-added ShellReference ring and its label. Drawn at 100% opacity (unlike the 50%-opacity default Rings above) and at double 'Ring Thickness' below, so it visually stands out as the authoritative, gameplay-accurate radius rather than a reference guide. Ignored if ShellReference is empty.")]
		public Color ShellReferenceColor;

		[Header("Appearance")]
		[Tooltip("Which local plane the rings are drawn on, centered on this GameObject's position:\n• XZ Ground — flat on the ground plane (local X/Z). Default; matches a top-down crater footprint for artillery impacts.\n• XY — flat on the local X/Y plane, facing local +Z. Useful for wall-mounted or vertical impacts.\n• YZ — flat on the local Y/Z plane, facing local +X.\n• Face Camera — always rotates to directly face the active Scene view camera, useful for inspecting from any angle.")]
		public OrientationPlane Plane;

		[Tooltip("Extra outline thickness, in pixels, applied to each ring in the Rings list above — purely a visibility aid. 0 draws a thin 1px line; higher values (e.g. 2–5) make rings easier to spot in a busy scene. The ShellReference ring always draws at double this value, so it reads as visually 'heavier' than the reference rings.")]
		[Range(0f, 8f)]
		public float RingThickness;

		[Tooltip("Angle in degrees, measured counter-clockwise from local +X (or from screen-right when using Face Camera), at which every ring's radius label is placed. All rings share this angle so labels line up as a readable column.\n  90 = top of the circle (default)\n  270 (or -90) = bottom\n  0 = right\n  180 = left")]
		public float LabelAngleDegrees;

		[Tooltip("Font size, in points, used for the radius labels drawn in the Scene view.")]
		[Range(8f, 24f)]
		public int LabelFontSize;

		[Tooltip("Format string used to build each ring's label via string.Format, where {0} is substituted with the ring's Radius value. Only the {0} token is supported (standard .NET numeric format codes may be used inside it). Examples:\n  \"R{0:0.##}m\"  ->  \"R0.25m\"   (default — 'R' prefix, up to 2 decimals, trailing zeros trimmed, meters suffix)\n  \"{0:0.00}\"     ->  \"0.25\"     (fixed 2 decimals, no prefix/suffix)\nIf the format string is invalid, the raw radius value is shown instead as a safe fallback.")]
		public string LabelFormat;

		[Tooltip("If enabled, rings are drawn at all times this GameObject is visible in the Scene/Prefab view, even when not selected. If disabled, rings only draw while this GameObject (or a parent) is selected in the Hierarchy. Editor convenience only — has no effect at runtime either way.")]
		public bool AlwaysVisible;

		private static readonly float[] DefaultRadii;

		private void Reset()
		{
		}

		private static List<RadiusRing> BuildDefaultRings()
		{
			return null;
		}
	}
}
