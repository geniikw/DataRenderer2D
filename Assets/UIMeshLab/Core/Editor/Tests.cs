using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace geniikw.UIMeshLab
{
    public class Tests
    {
       /// MeshQueue data;

        [SetUp]
        public void Init()
        {
            
        }
        
        [Test]
        public void QueueTest()
        {
            var q = new Queue();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            Debug.Log(q.Dequeue() + " " + q.Dequeue() + " " + q.Dequeue());
        }

        [Test]
        public void Dot()
        {
            var input = new Vector2(-1, -1);
            input.Normalize();

            var RL = Vector2.Dot(input, Vector2.right);
            Debug.Log(RL);
        }
    }
}