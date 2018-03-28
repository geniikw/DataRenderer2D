using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D
{
    public class WorldLine : WorldDataMesh , ISpline
    {
        public Spline line;

        Spline ISpline.Line
        {
            get
            {
                return line;
            }
        }

        public bool useUpdate = false;

        IMeshDrawer m_builder;
        
        protected override IEnumerable<IMesh> MeshFactory
        {
            get
            {
                return (m_builder ?? (m_builder = LineBuilder.Factory.Normal(this, transform))).Draw();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            line.EditCallBack += GeometyUpdateFlagUp;
            line.owner = this;
        }  
    }   
}