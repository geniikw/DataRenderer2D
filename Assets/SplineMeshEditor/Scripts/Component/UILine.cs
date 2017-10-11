using geniikw.UIMeshLab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace geniikw.UIMeshLab
{
    /// <summary>
    /// draw mesh in canvas
    /// </summary>
    public class UILine : Graphic, IMeshModifier, ISpline
    {
        /// if change in code. Should call GeometryUpdate();
        public Spline line;

        IMeshBuilder m_builder;
        IMeshBuilder Builder
        {
            get
            {
                return m_builder ?? (m_builder = LineBuilder.Factory.Normal(this));
            }
        }

        public Spline Line
        {
            get
            {
                return line;
            }
        }


        protected override void Start()
        {
            base.Start();
        }

        public void ModifyMesh(VertexHelper verts)
        {
            verts.Clear();
            var meshData = Builder.Build();
            meshData.vertexes.ForEach(v => verts.AddVert(v.position, v.color, v.uv));

            foreach (var t in meshData.Triangles)
                verts.AddTriangle(t[0], t[1], t[2]);
        }

        public void ModifyMesh(Mesh mesh)
        {
            using (var vh = new VertexHelper(mesh))
            {
                ModifyMesh(vh);
                vh.FillMesh(mesh);
            }
        }
    }
}