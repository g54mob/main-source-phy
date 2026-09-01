using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Input/Game Objects", ModuleName = "Input GameObjects", Description = "")]
	[HelpURL("https://curvyeditor.com/doclink/cginputgameobject")]
	public class InputGameObject : CGModule
	{
		[HideInInspector]
		[OutputSlotInfo(typeof(CGGameObject), Array = true)]
		public CGModuleOutputSlot OutGameObject = new CGModuleOutputSlot();

		[ArrayEx]
		[SerializeField]
		private List<CGGameObjectProperties> m_GameObjects = new List<CGGameObjectProperties>();

		public List<CGGameObjectProperties> GameObjects => m_GameObjects;

		public bool SupportsIPE => false;

		public override void Reset()
		{
			base.Reset();
			GameObjects.Clear();
			base.Dirty = true;
		}

		public override void Refresh()
		{
			base.Refresh();
			if (!OutGameObject.IsLinked)
			{
				return;
			}
			CGGameObject[] array = new CGGameObject[GameObjects.Count];
			int newSize = 0;
			for (int i = 0; i < GameObjects.Count; i++)
			{
				if (GameObjects[i] != null)
				{
					array[newSize++] = new CGGameObject(GameObjects[i]);
				}
			}
			Array.Resize(ref array, newSize);
			CGModuleOutputSlot outGameObject = OutGameObject;
			CGData[] data = array;
			outGameObject.SetData(data);
		}

		public override void OnTemplateCreated()
		{
			base.OnTemplateCreated();
			GameObjects.Clear();
		}
	}
}
