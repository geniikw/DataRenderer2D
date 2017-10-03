using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class WorldLine : MonoBehaviour
    {
        public Line line;

        public void Start()
        {
        }

        public void Reset()
        {
            var mf = GetComponent<MeshFilter>();
           
            if(mf.sharedMesh == null)
            {
                var mesh = new Mesh(); ;
                mesh.name = name;
                mf.sharedMesh = mesh;
            }
        }
    }
}