using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Example : MonoBehaviour
{
    public Transform cube1;
    public Transform cube2;
    public float time = 0.4f;
    IEnumerator Start()
    {
        while (true)
        {
            yield return CoroutineChain.Start
              .Play(cube1.MoveToForSec(Vector3.up, time))
              .Wait(time)
              .Play(cube1.MoveToForSec(Vector3.zero, time))
              .Wait(time)
              .Play(cube2.MoveToForSec(Vector2.one, time))
              .Wait(time)
              .Play(cube2.MoveToForSec(Vector3.right, time))
              .Wait(time)
              .Parallel(cube1.MoveToForSec(Vector3.up, time), cube2.MoveToForSec(Vector2.one, time))
              .Log("Parallel Complete!")
              .Wait(time)
              .Sequential(cube1.MoveToForSec(Vector3.zero, time), cube2.MoveToForSec(Vector3.right, time))
              .Log("Sequential Complete", ELogType.NORMAL);
        }
    }
}

public static class CommonCoroutine
{
    static public IEnumerator MoveToForSec(this Transform trans, Vector3 worldPosition, float sec)
    {
        var start = trans.position;
        var t = 0f;
        while (t < 1f)
        {
            t = Mathf.Min(1f, t + Time.deltaTime / sec);
            trans.position = Vector3.Lerp(start, worldPosition, t);
            yield return null;
        }
    }
}
