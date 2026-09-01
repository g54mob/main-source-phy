using UnityEngine;

namespace LevelCreator
{
	public class DestroyBox : TriggerBox, ITriggerable
	{
		[SerializeField]
		private ParticleSystem m_dissolveEffect;

		[SerializeField]
		private Material m_dissolveMaterial;

		[SerializeField]
		private string m_dissolveSoundRef;

		public void Trigger()
		{
			DestroyConnectedObjects();
		}

		private void DestroyConnectedObjects()
		{
			foreach (GameObject playConnection in m_playConnections)
			{
				if (playConnection != null)
				{
					EraserTool.DissolveObject(playConnection, m_dissolveEffect, m_dissolveMaterial, m_dissolveSoundRef);
				}
			}
			m_playConnections.Clear();
		}

		public override DMEditorComponent ValidateHighlightedObject(DMEditorComponent obj)
		{
			return obj;
		}
	}
}
