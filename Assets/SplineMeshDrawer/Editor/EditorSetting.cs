using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace geniikw.UIMeshLab.Editors
{
    public class EditorSetting : ScriptableObject
    {
        [Header("if you want to edit position in inspector, set false")]
        public bool onlyViewWidth = true;
        static string Path = "Assets/SplineMeshDrawer/Editor/EditorSetting.asset";

        public static EditorSetting Get
        {
            get
            {
                return AssetDatabase.LoadAssetAtPath<EditorSetting>(Path);
            }
        }
    }
}