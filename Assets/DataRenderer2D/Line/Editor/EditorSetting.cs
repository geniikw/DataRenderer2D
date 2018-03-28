using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace geniikw.DataRenderer2D.Editors
{
    public class EditorSetting : ScriptableObject
    {
        [Header("if you want to edit position in inspector, set false")]
        public bool onlyViewWidth = true;

        public PointHandler.Setting PS;

        public static EditorSetting m_instance;
        public static EditorSetting Get
        {
            get
            {
                if (m_instance == null)
                {
                    var find = AssetDatabase.FindAssets("t:EditorSetting", null);

                    if (find.Length == 0)
                        throw new System.Exception("Can't find setting file");

                    m_instance = AssetDatabase.LoadAssetAtPath<EditorSetting>(AssetDatabase.GUIDToAssetPath(find[0]));
                }

                return m_instance;
            }
        }
    }
}