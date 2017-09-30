using geniikw.UIMeshLab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace geniikw.UIMeshLab
{

    public class UILine : Graphic, IMeshModifier
    {
        public Line line;

        public void ModifyMesh(Mesh mesh)
        {
            //Obstacle.
        }

        public void ModifyMesh(VertexHelper verts)
        {

        }
    }
}