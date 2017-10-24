using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace geniikw.UIMeshLab.Editors
{
    [CustomPropertyDrawer(typeof(Spline))]
    public class CurvePropertyDrawer : PropertyDrawer
    {
        //**hardcoding : if you want to edit position data in inspector, set false//
        bool _open = false;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {

            return 100f;
        }

        public override void OnGUI(Rect position, SerializedProperty sp, GUIContent label)
        {



        }
    }
}