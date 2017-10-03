using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace geniikw.UIMeshLab
{
    [Serializable]
    public class Node
    {
        public Vector3 position;
        public Vector3 previousControlOffset;
        public Vector3 nextControlOffset;
        public Color color;
        public float width;
        public float angle;
        public int nextDivieCount;
        public bool loop;

        public Node()
        {
            position = Vector3.zero;
            previousControlOffset = Vector3.zero;
            nextControlOffset = Vector3.zero;
            color = Color.white;
            width = 1f;
            angle = 0f;
            nextDivieCount = 5;
            loop = false;
        }
        
        public Vector3 PreviousControlPoisition
        {
            get
            {
                return previousControlOffset + position;
            }
        }
        public Vector3 NextControlPosition
        {
            get
            {
                return nextControlOffset + position;
            }
        }
    }
}