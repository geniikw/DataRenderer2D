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
    public class UILine : Image, IMeshModifier, ISpline
    {
        /// if change in code. Should call GeometryUpdate() after change;
        public Spline line;

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
           // var meshData = new MeshQueue();
            //foreach (var v in meshData.Vertices)
            //    verts.AddVert(v.position, v.color, v.uv);

            //Queue<int> buffer = new Queue<int>();
            //foreach (var t in meshData.Triangles)
            //{
            //    buffer.Enqueue(t);
            //    if (buffer.Count == 3)
            //        verts.AddTriangle(buffer.Dequeue(), buffer.Dequeue(), buffer.Dequeue());
            //}
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