using geniikw.DataRenderer2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace geniikw.DataRenderer2D
{
    /// <summary>
    /// draw mesh in canvas
    /// </summary>
    public partial class UILine : UIDataMesh , ISpline
    {
        public Spline line;

        /// <summary>
        /// hard copy.
        /// </summary>
        Spline ISpline.Line
        {
            get
            {
                return line;
            }
        }

        IEnumerable<IMesh> m_Drawer = null;
        protected override IEnumerable<IMesh> DrawerFactory
        {
            get
            {
                return m_Drawer ?? (m_Drawer = LineBuilder.Factory.Normal(this, transform).Draw());
            }
        }

        protected override void Start()
        {
            base.Start();
            line.owner = this;
            line.EditCallBack += GeometyUpdateFlagUp;
        }
        
        public static UILine CreateLine(Transform parent = null)
        {
            var go = new GameObject("UILine");
            if (parent != null)
                go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;

            var line = go.AddComponent<UILine>();
            line.line = Spline.Default;
            line.Start();
            return line;
        }
    }
    
}