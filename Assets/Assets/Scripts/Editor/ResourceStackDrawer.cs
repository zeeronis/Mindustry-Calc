using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ResourceStack))]
public class ResourceStackDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);

        float fullWidth = contentPosition.width;
        const float resCountWidth = 35;
        const float resCountRightOffset = 5;
        const float resTexRightOffset = 5;
        const float resourceObjRightOffset = 20;
        const float minResObjWidth = 20;
        const float maxResObjWidth = 180;


        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;



        EditorGUI.BeginProperty(contentPosition, label, property);

        //  EditorGUI.LabelField();

        contentPosition.width = resCountWidth;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("count"), GUIContent.none);

        contentPosition.width = contentPosition.height;
        contentPosition.x += resCountWidth + resCountRightOffset;
        var resData = (property.FindPropertyRelative("resourceData").objectReferenceValue as ResourceDataObj);
        if (resData != null)
        {
            EditorGUI.DrawTextureTransparent(contentPosition, resData.Sprite.texture);
        }

        contentPosition.width = Mathf.Clamp(fullWidth - (resCountWidth + resCountRightOffset + resourceObjRightOffset + resTexRightOffset),
            minResObjWidth, maxResObjWidth);
        contentPosition.x += contentPosition.height + resTexRightOffset;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("resourceData"), GUIContent.none);
        EditorGUI.EndProperty();

        EditorGUI.indentLevel = indent;
    }
}
#endif
