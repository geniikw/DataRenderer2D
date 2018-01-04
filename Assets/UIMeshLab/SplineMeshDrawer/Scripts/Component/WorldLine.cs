using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class WorldLine : MonoBehaviour, ISpline
    {
        public Spline line;

        public bool useUpdate = true;
        
        //IMeshBuilder m_builder;
        //IMeshBuilder Builder {
        //    get
        //    {
        //        return m_builder ?? (m_builder = LineBuilder.Factory.Normal(this));
        //    }
        //}
        public Spline Line {
            get
            {
                return line;
            }
        }

        public void UpdateGeometry()
        {
            var mf = GetComponent<MeshFilter>();

            //var meshData = new MeshQueue();
            //mf.sharedMesh.Clear();
            //mf.sharedMesh.vertices = meshData.Vertices.Select(v => v.position).ToArray();
            //mf.sharedMesh.uv = meshData.Vertices.Select(v => v.uv).ToArray();
            //mf.sharedMesh.colors = meshData.Vertices.Select(v => v.color).ToArray();
            //mf.sharedMesh.triangles = meshData.Triangles.ToArray();
            //mf.sharedMesh.RecalculateNormals();
        }

        public void Reset()
        {
            var mf = GetComponent<MeshFilter>();

            if (mf.sharedMesh == null)
            {
                var mesh = new Mesh();
                mesh.name = name;
                mf.sharedMesh = mesh;
            }
        }

        public void Update()
        {
            //I recommand to use UniRx.
            if(useUpdate)
                UpdateGeometry();
        }
    }   
}