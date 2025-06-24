using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Pro_Directions))]
public class Pro_DirectionsDrawer : PropertyDrawer
{
    private static readonly float boxSize = 30f;
    private static readonly float padding = 4f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.LabelField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), label.text);
        float startY = position.y + EditorGUIUtility.singleLineHeight + 4;

        // Draw the 3x3 grid
        DrawBox(property, "up_left", 0, 0, position.x, startY);
        DrawBox(property, "up", 1, 0, position.x, startY);
        DrawBox(property, "up_Right", 2, 0, position.x, startY);

        DrawBox(property, "left", 0, 1, position.x, startY);
        // Center 'X' box
        Rect center = GetBoxRect(1, 1, position.x, startY);
        EditorGUI.LabelField(center, "X", new GUIStyle() { alignment = TextAnchor.MiddleCenter });

        DrawBox(property, "right", 2, 1, position.x, startY);

        DrawBox(property, "down_left", 0, 2, position.x, startY);
        DrawBox(property, "down", 1, 2, position.x, startY);
        DrawBox(property, "down_right", 2, 2, position.x, startY);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight + boxSize * 3 + padding * 2 + 10;
    }

    private void DrawBox(SerializedProperty property, string fieldName, int gridX, int gridY, float startX, float startY)
    {
        SerializedProperty dirProp = property.FindPropertyRelative(fieldName);
        Rect rect = GetBoxRect(gridX, gridY, startX, startY);

        Color color = GetConditionColor((Pro_TileData.Condition)dirProp.enumValueIndex);

        EditorGUI.DrawRect(rect, color);
        //EditorGUI.LabelField(rect, dirProp.enumDisplayNames[dirProp.enumValueIndex], new GUIStyle()  // to show text on the box
        //{
        //    normal = new GUIStyleState() { textColor = Color.white },
        //    alignment = TextAnchor.MiddleCenter,
        //    fontStyle = FontStyle.Bold
        //});

        // Click to cycle condition
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            int next = (dirProp.enumValueIndex + 1) % dirProp.enumDisplayNames.Length;
            dirProp.enumValueIndex = next;
            dirProp.serializedObject.ApplyModifiedProperties();
            GUI.changed = true;
        }
    }

    private Rect GetBoxRect(int gridX, int gridY, float startX, float startY)
    {
        float x = startX + gridX * (boxSize + padding);
        float y = startY + gridY * (boxSize + padding);
        return new Rect(x, y, boxSize, boxSize);
    }

    private Color GetConditionColor(Pro_TileData.Condition condition)
    {
        switch (condition)
        {
            case Pro_TileData.Condition.ThisTile:
                return new Color(0.2f, 0.8f, 0.2f); // Green
            case Pro_TileData.Condition.NotThisTile:
                return new Color(0.9f, 0.2f, 0.2f); // Red
            case Pro_TileData.Condition.Any:
                return new Color(0.2f, 0.4f, 0.9f); // Blue
            default:
                return Color.black;
        }
    }
}
