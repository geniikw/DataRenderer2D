using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

namespace geniikw.UIMeshLab {
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class WorldDataMesh : MonoBehaviour
    {
        public bool updateInUpdate = true;
        
        IEnumerable<IMesh> _mesh;

        IEnumerable<IMesh> Mesh { get { return _mesh ?? (_mesh = MeshFactory); } }

        List<Vector3> m_pStack = new List<Vector3>();
        List<Vector2> m_uvStack = new List<Vector2>();
        List<Color> m_colorStack = new List<Color>();
        List<int> m_tStack = new List<int>();

        abstract protected IEnumerable<IMesh> MeshFactory { get; }

        MeshFilter m_mf;
        MeshFilter Mf
        {
            get
            {
                return m_mf ?? (m_mf = GetComponent<MeshFilter>());
            }
        }

        private void Update()
        {
            if (updateInUpdate)
                UpdateGeometry();
        }

        public void UpdateGeometry()
        {
            if(Mf.sharedMesh == null)
            {
                Mf.sharedMesh = new Mesh
                {
                    name = name
                };
            }
            Mf.sharedMesh.Clear();
            m_tStack.Clear();
            m_uvStack.Clear();
            m_pStack.Clear();
            m_colorStack.Clear();
                        
            foreach (var m in Mesh)
            {
                foreach (var t in m.Triangles)
                    m_tStack.Add(t + m_pStack.Count);

                foreach (var v in m.Vertices)
                {
                    m_pStack.Add(v.position);
                    m_uvStack.Add(v.uv);
                    m_colorStack.Add(v.color);
                }
            }
            
            Mf.sharedMesh.vertices = m_pStack.ToArray();
            Mf.sharedMesh.uv = m_uvStack.ToArray();
            Mf.sharedMesh.colors = m_colorStack.ToArray();
            Mf.sharedMesh.triangles = m_tStack.ToArray();
            Mf.sharedMesh.RecalculateNormals();
            Mf.sharedMesh.RecalculateBounds();
        }
    }
}