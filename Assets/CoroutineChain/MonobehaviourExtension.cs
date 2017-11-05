using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonobehaviourExtension {
    
    public static CoroutineChain.Chain Move(this Transform owner, Vector3 end, float t)
    {
        return CoroutineChain.Start
            .Play(CommonCoroutine.Move(owner, end, t));
    }

}
