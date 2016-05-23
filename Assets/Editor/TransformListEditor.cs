using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

[CustomEditor(typeof(TransformList))]
public class TransformListEditor : Editor {
        private ReorderableList list;

        private void OnEnable() {
                list = new ReorderableList(serializedObject,
                        serializedObject.FindProperty("transformList"),
                        true, true, true, true);

                list.drawElementCallback =  
                        (Rect rect, int index, bool isActive, bool isFocused) => {
                        var element = list.serializedProperty.GetArrayElementAtIndex(index);
                        rect.y += 2;
                        EditorGUI.LabelField(new Rect(rect.x, rect.y, 80, EditorGUIUtility.singleLineHeight), 
                                "Element " + index);
                        EditorGUI.PropertyField(
                                new Rect(rect.x + 80, rect.y, 200, EditorGUIUtility.singleLineHeight),
                                element, GUIContent.none);
                };
        }

        public override void OnInspectorGUI() {
               //base.OnInspectorGUI();
               serializedObject.Update();
               list.DoLayoutList();
               serializedObject.ApplyModifiedProperties();
        }

        void OnSceneGUI() {
                Handles.color = Color.red;
                List<Transform> transformList = (target as TransformList).transformList;
                for (int i = 0; i < transformList.Count; i++) {
                        Transform start = transformList [i];
                        Transform end = transformList [(i + 1) % transformList.Count];

                        if (start && end) {
                                Handles.DrawLine(start.position, end.position);

                                Vector3 relativePos = end.position - start.position;
                                Quaternion rotation = Quaternion.LookRotation(relativePos);
                                Handles.ArrowCap(0, start.position, rotation, 0.8f);
                        }
                }
        }
}