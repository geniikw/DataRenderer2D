using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class WorldLine : MonoBehaviour
    {
        public Line line;

        public void Update()
        {
            UpdateGeometry();
        }

        private void UpdateGeometry()
        {
            var mf = GetComponent<MeshFilter>();

            var lineBuilder = new LineBuilder()
            {
                bezierDrawer = new NormalBezierDrawer(line)
            };

            var meshData = lineBuilder.Build(line);
            mf.mesh.Clear();
            mf.mesh.vertices = meshData.vertexes.Select(v => v.position).ToArray();
            mf.mesh.uv = meshData.vertexes.Select(v => v.uv).ToArray();
            mf.mesh.triangles = meshData.triangles.ToArray();
            mf.mesh.colors = meshData.vertexes.Select(v => v.color).ToArray();
            mf.mesh.RecalculateNormals();
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
    }
}