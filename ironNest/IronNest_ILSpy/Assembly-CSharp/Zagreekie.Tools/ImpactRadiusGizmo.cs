using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Zagreekie.Tools;

public class ImpactRadiusGizmo : MonoBehaviour
{
	public enum OrientationPlane
	{
		XZ_Ground,
		XY,
		YZ,
		FaceCamera
	}

	[Serializable]
	public struct RadiusRing
	{
		public float Radius;

		public Color Color;

		public RadiusRing(float radius, Color color)
		{
			//IL_0019: Expected O, but got F4
			Radius = radius;
			Color = (Color)color.r;
		}
	}

	public List<RadiusRing> Rings;

	public ShellDefinition ShellReference;

	public Color ShellReferenceColor;

	public OrientationPlane Plane;

	public float RingThickness;

	public float LabelAngleDegrees;

	public int LabelFontSize;

	public string LabelFormat;

	public bool AlwaysVisible;

	private static readonly float[] DefaultRadii = new float[7] { 0.1f, 0.25f, 0.5f, 0.75f, 1f, 1.25f, 1.5f };

	private void Reset()
	{
		List<RadiusRing> rings = BuildDefaultRings();
		Rings = rings;
	}

	private unsafe static List<RadiusRing> BuildDefaultRings()
	{
		//IL_0024: Expected O, but got I4
		//IL_00c5: Expected O, but got Ref
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		float[] defaultRadii = DefaultRadii;
		List<RadiusRing> list = new List<RadiusRing>(defaultRadii.Length);
		object obj = 0;
		bool hdr = default(bool);
		object obj2 = default(object);
		while (true)
		{
			float[] defaultRadii2 = DefaultRadii;
			if ((nint)obj >= defaultRadii2.Length)
			{
				return list;
			}
			float[] defaultRadii3 = DefaultRadii;
			float h = (float)obj / (float)defaultRadii3.Length;
			Color color = Color.HSVToRGB(h, 0.85f, 1f, hdr);
			if (DefaultRadii == null || list == null)
			{
				break;
			}
			list.Add((RadiusRing)(&obj2));
			obj++;
		}
		return (List<RadiusRing>)(object)new NullReferenceException();
	}

	public ImpactRadiusGizmo()
	{
		//IL_002f: Expected O, but got I
		Rings = BuildDefaultRings();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		ShellReferenceColor = (Color)0;
		RingThickness = 2f;
		LabelAngleDegrees = 90f;
		LabelFontSize = 12;
		LabelFormat = "R{0:0.##}m";
		AlwaysVisible = true;
		base._002Ector();
	}
}
