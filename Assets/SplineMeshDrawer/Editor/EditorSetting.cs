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

        public PointHandler.Setting PS;
        
        static string Path = "Assets/SplineMeshDrawer/Editor/EditorSetting.asset";
        public static EditorSetting m_instance;
        public static EditorSetting Get
        {
            get
            {
                return m_instance ?? (m_instance =AssetDatabase.LoadAssetAtPath<EditorSetting>(Path));
            }
        }
    }
}