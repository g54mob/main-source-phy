using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations
{
	[RequireComponent(typeof(Text))]
	public class VersionNumber : MonoBehaviour
	{
		public string Version;

		protected Text _text;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}
	}
}
