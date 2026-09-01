using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InternalModding.Loading;
using UnityEngine;

namespace Modding.Levels
{
	public class Level
	{
		public ReadOnlyCollection<Entity> Entities
		{
			get
			{
				return InternalObject.Entities.Select(Entity.From).ToList().AsReadOnly();
			}
		}

		public ReadOnlyCollection<Entity> Selection
		{
			get
			{
				return InternalObject.Selection.Select(Entity.From).ToList().AsReadOnly();
			}
		}

		public Dictionary<string, float> GlobalVariables
		{
			get
			{
				return new Dictionary<string, float>(InternalLevel.variables);
			}
		}

		public LevelSetup Setup { get; private set; }

		public XDataHolder CustomData
		{
			get
			{
				return InternalObject.CustomData;
			}
		}

		public LevelEditor InternalObject { get; private set; }

		public CustomLevel InternalLevel { get; private set; }

		private Level(LevelEditor editor)
		{
			InternalObject = editor;
			InternalLevel = editor.Level;
			Setup = LevelSetup.From(editor.Settings);
		}

		public void SetVariable(string var, float val)
		{
			InternalLevel.SetVariable(var, EventContainer.VarModifyType.Set, val);
		}

		public void AddEntity(int id, Vector3 position, Quaternion rotation, Vector3 scale, bool showEffect = true)
		{
			InternalObject.AddEntity(id, position, rotation, scale, showEffect);
		}

		public void AddEntity(Guid modId, int localId, Vector3 position, Quaternion rotation, Vector3 scale, bool showEffect = true)
		{
			int effectiveEntityId = ModIds.GetEffectiveEntityId(modId, localId);
			if (effectiveEntityId != 0)
			{
				AddEntity(effectiveEntityId, position, rotation, scale, showEffect);
			}
		}

		public void RemoveEntity(Entity entity)
		{
			if (!(entity == null))
			{
				List<long> list = new List<long>();
				list.Add(entity.Id);
				List<long> ids = list;
				InternalObject.Remove(ids, false);
			}
		}

		public ReadOnlyCollection<Entity> GetEntitiesOfType(int id)
		{
			return Entities.Where((Entity e) => e.Prefab.Id == id).ToList().AsReadOnly();
		}

		public ReadOnlyCollection<Entity> GetEntitiesOfType(Guid modId, int localId)
		{
			return GetEntitiesOfType(ModIds.GetEffectiveEntityId(modId, localId));
		}

		public override string ToString()
		{
			return "Level (" + Setup.Name + ")";
		}

		public static Level GetCurrentLevel()
		{
			return From(LevelEditor.Instance);
		}

		internal static Level From(LevelEditor editor)
		{
			if (editor == null)
			{
				return null;
			}
			return new Level(editor);
		}
	}
}
