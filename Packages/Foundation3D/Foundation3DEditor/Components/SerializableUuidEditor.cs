using System;
using RamenSea.Foundation.Extensions;
using RamenSea.Foundation3D.Components;
using UnityEditor;
using UnityEngine;

namespace RamenSea.Foundation3DEditor.Components {
    /// <summary>
    /// Todo make the layout nicer
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializableUuid))]
    public class SerializableUuidEditor: PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            bool shouldSave = false; //whether the value has changed and se should save to disk
            
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
 
            // Get the stored properties
            // Uuid is stored as two ulongs
            SerializedProperty serializedLowBytes = property.FindPropertyRelative("lowBytes");
            SerializedProperty serializedHighBytes = property.FindPropertyRelative("highBytes");

            // parsed values
            Guid uuid = UUIDExtensions.CreateUuidFrom(serializedLowBytes.ulongValue, serializedHighBytes.ulongValue);
            string stringUuid = uuid.ToString();


            if (GUI.Button(position, "Random")) {
                stringUuid = Guid.NewGuid().ToString();
                shouldSave = true;
            }
            
            string updated = EditorGUILayout.TextField(stringUuid);
            if (shouldSave == false && stringUuid != updated) {
                shouldSave = true;
                stringUuid = updated;
            }

            if (shouldSave) {
                try {
                    uuid = Guid.Parse(stringUuid);
                    uuid.GetULong(out ulong lowBytes, out ulong highBytes);
                    serializedLowBytes.ulongValue = lowBytes;
                    serializedHighBytes.ulongValue = highBytes;
                } catch (Exception e) {
                    Debug.LogError(e);
                }
            }
            
            EditorGUI.EndProperty();
        }
    }
}