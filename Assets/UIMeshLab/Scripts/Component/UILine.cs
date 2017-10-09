using geniikw.UIMeshLab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace geniikw.UIMeshLab
{
    public class UILine : Graphic, IMeshModifier
    {
        /// <summary>
        /// if change in code. Should call GeometryUpdate();
        /// </summary>
        public Line line;

        LineBuilder m_lineBuilder;

        LineBuilder LineBuilder
        {
            get
            {
                return m_lineBuilder ?? (m_lineBuilder = LineBuilder.Factory.Normal(line));
            }
        }

        protected override void Start()
        {
            base.Start();
        }

        public void ModifyMesh(VertexHelper verts)
        {
            verts.Clear();
            var meshData = LineBuilder.Build();
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