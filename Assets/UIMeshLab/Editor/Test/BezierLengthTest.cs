using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace geniikw.UIMeshLab
{
    public class BezierLengthTest
    {
        Line testLine;

        [SetUp]
        public void Init()
        {
            testLine = new Line();
            testLine.points.Add(new Node());
            testLine.points.Add(new Node());
            testLine.points.Add(new Node());

            testLine.points[0].position = Vector3.zero;
            testLine.points[0].previousControlOffset = Vector3.zero;
            testLine.points[0].nextControlOffset = Vector3.zero;

            testLine.points[1].position = Vector3.one;
            testLine.points[1].previousControlOffset = Vector3.left;
            testLine.points[1].nextControlOffset = Vector3.right * 2f;

            testLine.points[2].position = Vector3.one*2f;
            testLine.points[2].previousControlOffset = Vector3.down;
        }
        
        [Test]
        public void QuadraticBezierLengthTest()
        {
            var slow = CurveLength.SumDirections(testLine.points[0], testLine.points[1]);
            var fast = CurveLength.Auto(testLine.points[0], testLine.points[1]);

            Debug.Log(slow);
            Debug.Log(fast);
            var accurate = slow / fast;

            Assert.True(accurate < 1.01f && accurate > 0.99f);
        }

        [Test]
        public void CubicBezierLengthTest()
        {
            var testCase = 100;
            var testAccuracy = 0.05f;
            var results = new List<float>();
            for (int i = 0; i < testCase; i++)
            {
                var A = new Vector3(0, 0, 0);
                var B = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
                var C = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
                var D = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));

                var slow = CurveLength.SumDirections(A, B, C, D);
                var fast = CurveLength.Auto(A, B, C, D);

                var accurate = slow / fast;

                results.Add(accurate);
            }
            Assert.True(results.Average() > 1f - testAccuracy && results.Average() < 1f + testAccuracy);          
            Debug.Log("Avg accuracy : " + results.Average());

        }


    }
}