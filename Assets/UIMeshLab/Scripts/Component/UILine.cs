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
        public Line line;

        LineBuilder _lineBuilder = new LineBuilder();

        protected override void Start()
        {
            base.Start();

            UpdateGeometry();
        }

        public void ModifyMesh(VertexHelper verts)
        {
            verts.Clear();
            var meshData = _lineBuilder.Build(line);
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