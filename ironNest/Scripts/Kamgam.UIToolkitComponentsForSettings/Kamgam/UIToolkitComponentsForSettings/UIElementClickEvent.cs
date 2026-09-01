using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Kamgam.UIToolkitComponentsForSettings
{
	public class UIElementClickEvent : MonoBehaviour
	{
		[Header("Query Criteria")]
		[Tooltip("Only elements of this type are used.")]
		public UIElementType Type;

		[Tooltip("If set then the element will be search by the class name.\nIf an element name is set then both have to match.")]
		public string BindingClass;

		[Tooltip("If set then the element will be search by the element name.\nIf a class name is set then both have to match.")]
		public string BindingName;

		[Tooltip("If enabled then all elements matching the criteria are used.")]
		public bool MultipleResults;

		[Header("Events")]
		public UnityEvent<ClickEvent> OnClick;

		protected UIDocument _document;

		[NonSerialized]
		public List<VisualElement> Elements;

		public Predicate<VisualElement> BindingPredicate;

		public UIDocument Document => null;

		public virtual void OnEnable()
		{
		}

		public void RefreshElements()
		{
		}

		public virtual void OnDisable()
		{
		}

		public virtual void OnDestroy()
		{
		}

		public virtual void RegisterEvents()
		{
		}

		public virtual void UnregisterEvents()
		{
		}

		protected virtual void onClick(ClickEvent evt)
		{
		}
	}
}
