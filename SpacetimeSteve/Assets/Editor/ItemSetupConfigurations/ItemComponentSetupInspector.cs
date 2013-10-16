//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using Newtonsoft.Json;

//[CustomEditor(typeof(ItemComponentSetup))]
//public class ItemComponentSetupInspector : Editor
//{
//    ItemComponentSetup.SetupByType selected;
//    SerializedProperty selectedType;
//    SerializedProperty behaviours;
//    SerializedProperty tags;
//    SerializedProperty classID;
//    SerializedProperty components;

//    void OnEnable()
//    {
//        selectedType = serializedObject.FindProperty("selectorType");
//        behaviours = serializedObject.FindProperty("behaviours");
//        tags = serializedObject.FindProperty("TagList");
//        classID = serializedObject.FindProperty("classList");
//        components = serializedObject.FindProperty("itemComponents");
//    }

//    public override void OnInspectorGUI()
//    {

//        serializedObject.Update();

//        EditorGUIUtility.LookLikeInspector();
//        DrawPropertiesExcluding(serializedObject, "selectorType", "TagList", "classList");
//        EditorGUILayout.Separator();
//        EditorGUIUtility.LookLikeControls();
//        selected = (ItemComponentSetup.SetupByType)selectedType.enumValueIndex;
//        selected = (ItemComponentSetup.SetupByType)EditorGUILayout.EnumPopup("pick Selection Type", selected);
//        selectedType.enumValueIndex = (int)selected;
//        EditorGUIUtility.LookLikeInspector();
//        switch (selected)
//        {
//            case ItemComponentSetup.SetupByType.classID:
//                DrawClassID();
//                break;
//            case ItemComponentSetup.SetupByType.tag:
//                DrawTag();
//                break;
//            case ItemComponentSetup.SetupByType.behaviour:
//                DrawBehaviourID();
//                break;
//            default:
//                break;
//        }
//        serializedObject.ApplyModifiedProperties();
//    }

//    void DrawClassID()
//    {
//        for (int i = 0; i < classID.arraySize; i++)
//        {
//            GUILayout.BeginHorizontal();
//            GUILayout.FlexibleSpace();
//            GUILayout.FlexibleSpace();
//            int validateFirst = EditorGUILayout.IntField(classID.GetArrayElementAtIndex(i).intValue, GUILayout.MaxWidth(65));
//            if (validateFirst > 1)
//            {
//                classID.GetArrayElementAtIndex(i).intValue = validateFirst;
//            }
//            if (GUILayout.Button("-"))
//            {
//                classID.DeleteArrayElementAtIndex(i);
//                GUILayout.EndHorizontal();
//                return;
//            }
//            GUILayout.FlexibleSpace();
//            GUILayout.EndHorizontal();
//        }

//        GUILayout.BeginHorizontal();
//        GUILayout.FlexibleSpace();
//        GUILayout.FlexibleSpace();
//        if (GUILayout.Button("+ Add New Class"))
//        {
//            classID.InsertArrayElementAtIndex(classID.arraySize);

//        }
//        GUILayout.FlexibleSpace();
//        GUILayout.EndHorizontal();
//    }

//    void DrawTag()
//    {
//        for (int i = 0; i < tags.arraySize; i++)
//        {
//            GUILayout.BeginHorizontal();
//            GUILayout.FlexibleSpace();
//            GUILayout.FlexibleSpace();
//            string validateFirst = EditorGUILayout.TextField(tags.GetArrayElementAtIndex(i).stringValue, GUILayout.MaxWidth(65));
//            if (!string.IsNullOrEmpty(validateFirst))
//            {
//                tags.GetArrayElementAtIndex(i).stringValue = validateFirst;
//            }
//            if (GUILayout.Button("-"))
//            {
//                tags.DeleteArrayElementAtIndex(i);
//                GUILayout.EndHorizontal();
//                return;
//            }
//            GUILayout.FlexibleSpace();
//            GUILayout.EndHorizontal();
//        }

//        GUILayout.BeginHorizontal();
//        GUILayout.FlexibleSpace();
//        GUILayout.FlexibleSpace();
//        if (GUILayout.Button("+ Add New Tag"))
//        {
//            tags.InsertArrayElementAtIndex(tags.arraySize);

//        }
//        GUILayout.FlexibleSpace();
//        GUILayout.EndHorizontal();

//    }

//    void DrawBehaviourID()
//    {
//        for (int i = 0; i < behaviours.arraySize; i++)
//        {
//            string behaviourPrefromat = behaviours.GetArrayElementAtIndex(i).stringValue;
//            ItemFilterSystem.BehaviourPair pair = JsonConvert.DeserializeObject<ItemFilterSystem.BehaviourPair>(behaviourPrefromat);
//            if (pair == null)
//            {
//                pair = new ItemFilterSystem.BehaviourPair();
//            }
//            GUILayout.BeginHorizontal();
//            GUILayout.FlexibleSpace();
//            GUILayout.FlexibleSpace();
//            GUILayout.BeginVertical();
//            int behaviourClass = EditorGUILayout.IntField("Class ID", pair.classID);
//            int behaviourID = EditorGUILayout.IntField("Behaviour ID", pair.behaviourID);
//            GUILayout.EndVertical();


//            if (GUILayout.Button("-"))
//            {
//                behaviours.DeleteArrayElementAtIndex(i);
//                GUILayout.EndHorizontal();
//                return;
//            }
//            GUILayout.FlexibleSpace();
//            GUILayout.EndHorizontal();

//            if (i < behaviours.arraySize)
//            {
//                EditorGUILayout.Separator();
//            }

//            if (behaviourClass >= 0)
//            {
//                pair.classID = behaviourClass;
//            }
//            if (behaviourID >= 0)
//            {
//                pair.behaviourID = behaviourID;
//            }

//            behaviours.GetArrayElementAtIndex(i).stringValue = JsonConvert.SerializeObject(pair);
//        }

//        GUILayout.BeginHorizontal();
//        GUILayout.FlexibleSpace();
//        GUILayout.FlexibleSpace();
//        if (GUILayout.Button("+ Add New Behaviour"))
//        {
//            behaviours.InsertArrayElementAtIndex(behaviours.arraySize);
//        }
//        GUILayout.FlexibleSpace();
//        GUILayout.EndHorizontal();


//    }
//}
