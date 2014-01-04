using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using UnityEngine;
using UnityEditor;

public class UnibillCurrencyEditor : InventoryEditor.IPlatformEditor {

	private static List<EditableCurrency> toRemove = new List<EditableCurrency> ();
	private List<EditableCurrency> currencies = new List<EditableCurrency> ();
	public UnibillCurrencyEditor(XElement element) {
		foreach (var x in element.XPathSelectElements("currency")) {
			currencies.Add (new EditableCurrency (x));
		}
	}

	public void onGUI () {
		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Currencies:");
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical (GUI.skin.box);

		foreach (var currency in currencies) {
			currency.onGUI ();
		}

		if (GUILayout.Button ("Add currency...")) {
			currencies.Add (new EditableCurrency());
		}

		EditorGUILayout.EndVertical ();

		currencies.RemoveAll (x => toRemove.Contains (x));
		toRemove.Clear ();
	}

	public System.Xml.Linq.XElement serialise () {
		XElement root = new XElement ("currencies");
		foreach (var currency in currencies) {
			root.Add (currency.serialise ());
		}
		return root;
	}

	private class EditableCurrency : InventoryEditor.IPlatformEditor {

		private bool visible;
		private string id = "gold";
		private List<CurrencyMapping> mappings = new List<CurrencyMapping>();
		private List<CurrencyMapping> mappingsToRemove = new List<CurrencyMapping>();

		public EditableCurrency() {
			visible = true;
			mappings.Add(new CurrencyMapping(this));
		}

		public EditableCurrency(XElement element) {
			id = (string) element.XPathSelectElement("currencyId");
			foreach (var mapping in element.XPathSelectElements("mappings/mapping")) {
				mappings.Add(new CurrencyMapping(mapping, this));
			}
		}

		public void onGUI () {
			var box = EditorGUILayout.BeginVertical (GUI.skin.box);

			Rect rect = new Rect (box.xMax - 20, box.yMin - 2, 20, 20);
			if (GUI.Button (rect, "x")) {
				toRemove.Add (this);
			}

			GUIStyle s = new GUIStyle (EditorStyles.foldout);
			this.visible = EditorGUILayout.Foldout (visible, id, s);
			if (visible) {
				id = EditorGUILayout.TextField ("Currency ID:", id);
				EditorGUILayout.LabelField ("Mappings:");
				EditorGUILayout.BeginVertical (GUI.skin.box);
				foreach (var mapping in mappings) {
					mapping.onGUI ();
				}

				if (GUILayout.Button (string.Format("Add mapping for {0}", id))) {
					mappings.Add (new CurrencyMapping(this));
				}

				EditorGUILayout.EndVertical ();
			}

			EditorGUILayout.EndVertical ();


			mappings.RemoveAll (x => this.mappingsToRemove.Contains (x));
		}
		public XElement serialise () {
			XElement root = new XElement ("currency");
			root.Add(new XElement("currencyId", id));

			XElement mappingsElement = new XElement ("mappings");
			root.Add (mappingsElement);
			foreach (var mapping in mappings) {
				mappingsElement.Add (mapping.serialise ());
			}
			return root;
		}

		private class CurrencyMapping : InventoryEditor.IPlatformEditor {

			private EditableCurrency parent;
			private int amount;
			private string id;
			private int selectedItemIndex;

			public CurrencyMapping(EditableCurrency parent) {
				this.parent = parent;
			}

			public CurrencyMapping(XElement element, EditableCurrency parent) {
				this.id = (string) element.XPathSelectElement("id");
				this.amount = (int) element.XPathSelectElement("amount");
				this.parent = parent;
				var items = InventoryEditor.consumableIds();
				for (int t = 0; t < items.Count(); t++) {
					if (items[t] == this.id) {
						selectedItemIndex = t;
						break;
					}
				}
			}

			public void onGUI () {
				var box = EditorGUILayout.BeginVertical (GUI.skin.box);

				EditorGUILayout.LabelField ("When the following consumable is purchased:");
				Rect rect = new Rect (box.xMax - 20, box.yMin - 2, 20, 20);
				if (GUI.Button (rect, "x")) {
					parent.mappingsToRemove.Add(this);
				}

				var ids = InventoryEditor.consumableIds ();
				selectedItemIndex = EditorGUILayout.Popup (selectedItemIndex, ids);
				if (0 <= selectedItemIndex && selectedItemIndex < ids.Length) {
					id = ids [selectedItemIndex];
				}

				EditorGUILayout.LabelField (string.Format("Give this much {0}:", parent.id));
				amount = EditorGUILayout.IntField (amount);
				EditorGUILayout.EndVertical ();
			}

			public XElement serialise () {
				XElement root = new XElement ("mapping");
				root.Add (new XElement ("id", id));
				root.Add (new XElement ("amount", amount));
				return root;
			}

		}
	}
}
