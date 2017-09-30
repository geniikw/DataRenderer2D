using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    [Serializable]
    public class Line
    {
        public List<Node> points;
        public float startRatio;
        public float endRatio;
        public bool isLoop;
    }
}