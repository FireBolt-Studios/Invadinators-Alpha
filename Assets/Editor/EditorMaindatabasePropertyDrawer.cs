using UnityEngine;
using UnityEditor;
using System.Collections;

[cust(typeof(PartData))]
public class EditorMaindatabasePropertyDrawer : PropertyDrawer {

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty (position, label, property);

		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 3;

		// Calculate rects
		var nameRect = new Rect (position.x, position.y, position.width-1, position.height);

		// Draw fields - passs GUIContent.none to each so they are drawn without labels
		EditorGUI.PropertyField (nameRect, property.FindPropertyRelative ("Name"));

		// Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty ();
	}
}
