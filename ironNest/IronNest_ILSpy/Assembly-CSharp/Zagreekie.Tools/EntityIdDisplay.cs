using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

namespace Zagreekie.Tools;

public class EntityIdDisplay : MonoBehaviour
{
	private EntityLocation _entityLocation;

	private TMP_Text _targetText;

	private string _format;

	private bool _clearTextWhenNoEntity;

	private bool _watchForChanges;

	private string _lastAppliedId;

	private void Awake()
	{
		if (_entityLocation == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			EntityLocation entityLocation = default(EntityLocation);
			bool flag = (object)entityLocation != null;
			EntityLocation entityLocation2 = entityLocation;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				EntityLocation entityLocation3 = default(EntityLocation);
				entityLocation2 = entityLocation3;
			}
			_entityLocation = entityLocation2;
		}
		string text;
		string text2;
		if (_entityLocation != null)
		{
			if (!(_targetText == null))
			{
				return;
			}
			GameObject gameObject = base.gameObject;
			text = gameObject.name;
			text2 = "[EntityIdDisplay] No target TMP_Text assigned on '";
		}
		else
		{
			GameObject gameObject2 = base.gameObject;
			text = gameObject2.name;
			text2 = "[EntityIdDisplay] No EntityLocation assigned or found on '";
		}
		string message = text2 + text + "'. Disabling.";
		Debug.LogWarning(message, this);
		base.enabled = false;
	}

	private void OnEnable()
	{
		_lastAppliedId = null;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 13 Invalid \"Jump target not found in method: 0x1804EB730\"");
	}

	private void Update()
	{
		RefreshText();
	}

	private void RefreshText()
	{
		EntityLocation entityLocation = _entityLocation;
		string text;
		if (entityLocation.Entity != null)
		{
			MapEntity entity = entityLocation.Entity;
			text = entity.ID;
		}
		else
		{
			text = null;
		}
		if (text != _lastAppliedId)
		{
			_lastAppliedId = text;
			ApplyToText();
		}
		if (!_watchForChanges && !string.IsNullOrEmpty(text))
		{
			base.enabled = false;
		}
	}

	private unsafe void ApplyToText()
	{
		//IL_028f: Expected O, but got I
		//IL_0297: Expected I, but got O
		//IL_02a7: Expected O, but got I
		//IL_02b7: Expected O, but got I
		//IL_02c7: Expected O, but got I
		//IL_015a: Expected O, but got I4
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01a0: Expected O, but got I
		//IL_01b0: Expected O, but got I
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected I4, but got Unknown
		object obj3;
		while (true)
		{
			if (_entityLocation != null)
			{
				EntityLocation entityLocation = _entityLocation;
				if (entityLocation.Entity != null)
				{
					MapEntity entity = entityLocation.Entity;
					if (!string.IsNullOrEmpty(entity.ID))
					{
						if (!string.IsNullOrEmpty(_format) && _format.Contains("{NUM}"))
						{
							EntityLocation entityLocation2 = _entityLocation;
							MapEntity entity2 = entityLocation2.Entity;
							if (entity2.IDIndex <= 0)
							{
								goto IL_0270;
							}
						}
						FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
						Func<KeyValuePair<string, MapEntity>, bool> predicate = delegate
						{
							//IL_00b8: Expected I4, but got O
							//IL_00a1: Expected O, but got I
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
							object obj9 = default(object);
							if (obj9 != null)
							{
								EntityLocation entityLocation5 = _entityLocation;
								if ((object)_entityLocation != null)
								{
									MapEntity entity4 = entityLocation5.Entity;
									if (entityLocation5.Entity != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
										if ((nint)0 != 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
											return ((string)0).Equals(entity4.RawID, StringComparison.OrdinalIgnoreCase);
										}
									}
								}
							}
							NullReferenceException ex = new NullReferenceException();
							return (byte)(int)ex != 0;
						};
						int num = Enumerable.Count(fireMission.Entities, predicate);
						bool flag = num == 1;
						object obj = 0;
						if (!flag)
						{
							if (string.IsNullOrEmpty(_format))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
								object obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v471 @ rax_v27+B8]");
								obj3 = 0;
							}
							else
							{
								obj3 = this + 48;
							}
							break;
						}
						goto IL_0270;
					}
				}
			}
			if (!_clearTextWhenNoEntity)
			{
				return;
			}
			goto IL_0270;
			IL_0270:
			TMP_Text targetText = _targetText;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj4 = 0;
			nint num2 = (nint)targetText;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v375 @ rax_v7+B8]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v376 @ r8_v4 (Il2CppClass<TMPro.TMP_Text>)+558]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v376 @ r8_v4 (Il2CppClass<TMPro.TMP_Text>)+560]");
			object obj7 = 0;
			object obj8 = obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v378 @ rax_v8 (should have been resolved before IL gen)");
		}
		EntityLocation entityLocation3 = _entityLocation;
		MapEntity entity3 = entityLocation3.Entity;
		string text = ((string)obj3).Replace("{ID}", entity3.RawID);
		EntityLocation entityLocation4 = _entityLocation;
		int num3 = entityLocation4.Entity + 40;
		string newValue = ((int*)num3)->ToString();
		string text2 = text.Replace("{NUM}", newValue);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181CA2AE0");
	}

	public EntityIdDisplay()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A81E]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		_format = "{ID}";
		_clearTextWhenNoEntity = true;
		base._002Ector();
	}

	private bool _003CApplyToText_003Eb__10_0(KeyValuePair<string, MapEntity> x)
	{
		//IL_00b8: Expected I4, but got O
		//IL_00a1: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
		object obj = default(object);
		if (obj != null)
		{
			EntityLocation entityLocation = _entityLocation;
			if ((object)_entityLocation != null)
			{
				MapEntity entity = entityLocation.Entity;
				if (entityLocation.Entity != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
						return ((string)0).Equals(entity.RawID, StringComparison.OrdinalIgnoreCase);
					}
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}
}
