using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class CatBlendShapeCustomizationConnection : Connection<float>
{
	private readonly bool eyes;

	private readonly bool body;

	private readonly bool fur;

	private readonly bool whiskers;

	private readonly int blendShapeIndex;

	private CatCustomizationController catCustomization;

	public CatBlendShapeCustomizationConnection(bool eyes, bool body, bool fur, bool whiskers, int blendShapeIndex)
	{
		bool flag = default(bool);
		this.whiskers = flag;
		this.eyes = eyes;
		this.body = body;
		int num = default(int);
		this.blendShapeIndex = num;
		this.fur = fur;
	}

	public override float Get()
	{
		//IL_0051: Expected F4, but got I4
		//IL_0057: Expected F4, but got I4
		ResolveReferenceIfNeeded();
		if (catCustomization != null)
		{
			bool flag = default(bool);
			int num = default(int);
			int blendShapeValue = catCustomization.GetBlendShapeValue(eyes, body, fur, flag, num);
			return blendShapeValue;
		}
		return 0f;
	}

	public override void Set(float value)
	{
		//IL_006b: Expected I4, but got F8
		ResolveReferenceIfNeeded();
		if (catCustomization != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
			double num = Math.Floor(0.0);
			bool flag = default(bool);
			bool flag2 = default(bool);
			int num2 = default(int);
			catCustomization.SetBlendShapeValue((int)num, eyes, body, flag, flag2, num2);
		}
	}

	private void ResolveReferenceIfNeeded()
	{
		if (catCustomization == null)
		{
			CatCustomizationController catCustomizationController = UnityEngine.Object.FindAnyObjectByType<CatCustomizationController>();
			catCustomization = catCustomizationController;
		}
	}
}
