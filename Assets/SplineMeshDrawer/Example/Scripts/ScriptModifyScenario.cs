using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptModifyScenario : MonoBehaviour {

    public Transform cube;
    public geniikw.UIMeshLab.WorldLine line;

	// Use this for initialization
	IEnumerator Start () {
        line.line.option.startRatio = 0f;
        line.line.option.endRatio = 0f;
        bool flag = false;

        while (true)
        {
            var p0 = line.line.points[0];
            var p1 = line.line.points[1];
            p0.nextControlOffset = new Vector3(Random.Range(0, 15), 0, Random.Range(-15, 0));
            p1.previousControlOffset = new Vector3(Random.Range(-15, 0), 0, Random.Range(0, 15));
            line.line.points[0] = p0;
            line.line.points[1] = p1;
            
            yield return DrawLine(1f, (flag = !flag));
            yield return StartCoroutine(MoveOnLine(1, flag));
        }
    }

    Vector3 RandomPosition
    {
        get
        {
            return new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        }
    }
    delegate float F();
    IEnumerator MoveOnLine(float time,bool inverse)
    {
        float t = 0;
        F f = () => t;
        if (inverse)
            f = () => 1 - t;
        
        while(t< 1)
        {
            t = Mathf.Clamp01(t + Time.deltaTime/time);
            cube.position = line.transform.TransformPoint( line.line.GetPosition(f()));
            cube.LookAt(cube.position+line.line.GetDirection(f()));
            yield return null;
        }
    }

    delegate void LerpFunc(float t);
    IEnumerator DrawLine(float time, bool inverse)
    {
        float t = 0f;
        LerpFunc lf;
        if (inverse)
        {
            line.line.option.endRatio = 1f;
            line.line.option.startRatio = 1f;
            lf = tt => line.line.option.startRatio = 1f-tt;
        }
        else
        {
            line.line.option.endRatio = 0f;
            line.line.option.startRatio = 0f;
            lf = tt => line.line.option.endRatio = tt;
        }

        while (t< 1f)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / time);
            lf(t);
            line.UpdateGeometry();
            yield return null;
        }
    }
}
