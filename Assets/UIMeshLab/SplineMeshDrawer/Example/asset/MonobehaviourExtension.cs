using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonobehaviourExtension {
    
    public static Coroutine Move(this Transform owner, Vector3 end, float t)
    {
        return owner.GetComponent<MonoBehaviour>().StartCoroutine(MoveRoutine(owner, end, t));
    }

    static IEnumerator MoveRoutine(Transform owner, Vector3 end, float time)
    {
        float t = 0f;
        Vector3 start = owner.position;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            owner.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
    }
}
