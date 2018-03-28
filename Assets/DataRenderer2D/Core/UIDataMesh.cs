using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace geniikw.DataRenderer2D
{
    public abstract class UIDataMesh : Image, IMeshModifier
    {
        IEnumerable<IMesh> _mesh;
        IEnumerable<IMesh> Mesh
        {
            get { return _mesh ?? (_mesh = DrawerFactory); }
        }
        protected abstract IEnumerable<IMesh> DrawerFactory { get; }
              

        public void ModifyMesh(Mesh mesh)
        {
            using (var vh = new VertexHelper(mesh))
            {
                ModifyMesh(vh);
                vh.FillMesh(mesh);
            }
        }

        public void ModifyMesh(VertexHelper verts)
        {
            verts.Clear();

            Queue<int> buffer = new Queue<int>();
           
            foreach (var m in Mesh)
            {  
                
                foreach (var t in m.Triangles)
                {
                    buffer.Enqueue(t);
                    if (buffer.Count == 3)
                    {
                        var c = verts.currentVertCount;
                        verts.AddTriangle(c + buffer.Dequeue(), c + buffer.Dequeue(), c + buffer.Dequeue());
                    }
                }
                foreach (var v in m.Vertices)
                    verts.AddVert(v.position, v.color, v.uv);

            }

        }

    }
    
}