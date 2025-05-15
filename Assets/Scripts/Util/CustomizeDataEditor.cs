using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomizeData))]
public class CustomizeDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CustomizeData data = (CustomizeData)target;

        if (data.IconImage != null)
        {
            GUILayout.Space(10);

            Texture2D texture = data.IconImage.texture;
            Rect rect = data.IconImage.rect;

            float previewHeight = 100f;
            float previewWidth = 100f;

            Rect texCoords = new Rect(
                rect.x / texture.width,
                rect.y / texture.height,
                rect.width / texture.width,
                rect.height / texture.height
            );

            GUILayout.Box(GUIContent.none, GUILayout.Width(previewWidth), GUILayout.Height(previewHeight));
            Rect lastRect = GUILayoutUtility.GetLastRect();
            GUI.DrawTextureWithTexCoords(lastRect, texture, texCoords);
        }
    }
}