﻿using System.Collections;
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

        LineBuilder m_lineBuilder;
        LineBuilder LineBuilder => m_lineBuilder ??(m_lineBuilder = LineBuilder.Factory.Normal(line));

        public void Update()
        {
            UpdateGeometry();
        }

        private void UpdateGeometry()
        {
            var mf = GetComponent<MeshFilter>();

            var meshData = LineBuilder.Build();
            mf.sharedMesh.Clear();
            mf.sharedMesh.vertices = meshData.vertexes.Select(v => v.position).ToArray();
            mf.sharedMesh.uv = meshData.vertexes.Select(v => v.uv).ToArray();
            mf.sharedMesh.triangles = meshData.triangles.ToArray();
            mf.sharedMesh.colors = meshData.vertexes.Select(v => v.color).ToArray();
            mf.sharedMesh.RecalculateNormals();
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