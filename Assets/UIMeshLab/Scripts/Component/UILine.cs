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

        MeshBuilder _meshBuilder = new MeshBuilder();

        protected override void Start()
        {
            base.Start();

            UpdateGeometry();
        }

        public void ModifyMesh(VertexHelper verts)
        {
            verts.Clear();
            var meshData = _meshBuilder.Build(line);
            meshData.vertexes.ForEach(v=> verts.AddVert(v.position,v.color,v.uv));

            var tb = new List<int>();
            meshData.triangles.ForEach(t => 
            {
                tb.Add(t);
                if (tb.Count == 3)
                {
                    verts.AddTriangle(tb[0], tb[1], tb[2]);
                    tb.Clear();
                }
            });
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