using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using geniikw.CChain;
using System;
using Object = UnityEngine.Object;

/*
CoroutineChain (v0.2.1) author - geniikw@gmail.com
all free, no duty, no warranty. 
If you feel good, you can apply MIT license(this is not enforceable)
*/
#region Accessor

public enum ELogType
{
    NORMAL,
    WARRNING,
    ERROR
}

public static class MonobehaviourExtend
{    
    public static ChainBase StartChain(this MonoBehaviour mono)
    {
        return ChainBase.BasePool.Spawn(mono);
    }
}

/// <summary>
/// 호환성을 위해 이름유지.
/// </summary>
public static class CoroutineChain
{
    class Dispather : MonoBehaviour { }
    static Dispather m_instance;
    static Dispather Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameObject("CoroutineChain").AddComponent<Dispather>();
                Object.DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }

    public static void StopAll()
    {
        m_instance.StopAllCoroutines();
    }
    
    public static ChainBase Start
    {
        get
        {
            return ChainBase.BasePool.Spawn(Instance);
        }
    }

}
#endregion

#region Util

namespace geniikw.CChain
{
    public class MemoryPool<T, TParam> where T : new()
    {
        Stack<T> m_pool = new Stack<T>();

        private readonly Action<T, TParam> _OnSpawn;
        private readonly Action<T> _OnDespawn;

        public MemoryPool(Action<T, TParam> OnSpawn = null, Action<T> OnDespawn = null)
        {
            _OnDespawn = OnDespawn;
            _OnSpawn = OnSpawn;
        }

        public T Spawn(TParam init)
        {
            T item;
            if (m_pool.Count == 0)
            {
                item = new T();
            }
            else
            {
                item = m_pool.Pop();
            }
            if (_OnSpawn != null)
                _OnSpawn(item, init);

            return item;
        }

        public void Despawn(T item)
        {
            if (_OnDespawn != null)
                _OnDespawn(item);
            m_pool.Push(item);
        }
    }
    public class MemoryPool<T> where T : new()
    {
        Stack<T> m_pool = new Stack<T>();

        private readonly Action<T> _OnSpawn;
        private readonly Action<T> _OnDespawn;

        public MemoryPool(Action<T> OnSpawn = null, Action<T> OnDespawn = null)
        {
            _OnDespawn = OnDespawn;
            _OnSpawn = OnSpawn;
        }

        public T Spawn()
        {
            T item;
            if (m_pool.Count == 0)
            {
                item = new T();
            }
            else
            {
                item = m_pool.Pop();
            }
            if (_OnSpawn != null)
                _OnSpawn(item);

            return item;
        }

        public void Despawn(T item)
        {
            if (_OnDespawn != null)
                _OnDespawn(item);
            m_pool.Push(item);
        }
    }
#endregion

#region CChainInternal
    
    public class Chain
    {
        EType type;
        MonoBehaviour player;
        IEnumerator routine;
        IEnumerator[] parallelRoutine;
        Action action;

        public Coroutine Play()
        {
            switch (type)
            {
                default:
                case EType.NonCoroutine:
                    action();
                    return null;
                case EType.Parallel:
                    return player.StartCoroutine(Parallel(parallelRoutine));
                case EType.Single:
                    return player.StartCoroutine(routine);
            }
        }

        public Chain SetupRoutine(IEnumerator routine, MonoBehaviour player)
        {
            type = EType.Single;
            this.player = player;
            this.routine = routine;
            return this;
        }

        public Chain SetupParallel(IEnumerator[] routines, MonoBehaviour player)
        {
            type = EType.Parallel;
            this.player = player;
            this.parallelRoutine = routines;
            return this;
        }

        public Chain SetupNon(System.Action action, MonoBehaviour player)
        {
            type = EType.NonCoroutine;
            this.player = player;
            this.action = action;
            return this;
        }

        public void Clear()
        {
            player = null;
            routine = null;
            action = null;
            parallelRoutine = null;
        }

        IEnumerator Parallel(IEnumerator[] routines)
        {
            var all = 0;
            foreach (var r in routines)
                all++;

            var c = 0;
            foreach (var r in routines)
                player.StartChain()
                    .Play(r)
                    .Call(() => c++);

            while (c < all)
                yield return null;
        }

        public enum EType
        {
            Single,
            Parallel,
            NonCoroutine
        }
    }

    public interface IChain
    {
        Coroutine Play(MonoBehaviour mono);
    }

    public class ChainBase : CustomYieldInstruction
    {
        public static MemoryPool<ChainBase, MonoBehaviour> BasePool = new MemoryPool<ChainBase, MonoBehaviour>((c, m) => c.Setup(m), c => c.Clear());
        public static MemoryPool<Chain> ChainPool = new MemoryPool<Chain>(null, c => c.Clear());

        MonoBehaviour _player;

        Queue<Chain> m_chainQueue = new Queue<Chain>();

        bool m_isPlay = true;

        public override bool keepWaiting
        {
            get
            {
                return m_isPlay;
            }
        }

        ChainBase Setup(MonoBehaviour player)
        {
            m_isPlay = true;
            _player = player;
            _player.StartCoroutine(Routine());
            return this;
        }

        void Clear()
        {
            _player = null;
            m_chainQueue.Clear();
        }

        IEnumerator Routine()
        {
            yield return null;

            while (m_chainQueue.Count > 0)
            {
                var chain = m_chainQueue.Dequeue();
                var cr = chain.Play();
                if (cr != null)
                    yield return cr;
                ChainPool.Despawn(chain);
            }

            m_isPlay = false;
            BasePool.Despawn(this);
        }

        public ChainBase Play(IEnumerator routine)
        {
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupRoutine(routine, _player));
            return this;
        }

        public ChainBase Wait(float waitSec)
        {
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupRoutine(WaitRoutine(waitSec), _player));
            return this;
        }

        public ChainBase Parallel(params IEnumerator[] routines)
        {
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupParallel(routines, _player));
            return this;
        }

        public ChainBase Sequential(params IEnumerator[] routines)
        {
            foreach (var routine in routines)
                m_chainQueue.Enqueue(ChainPool.Spawn().SetupRoutine(routine, _player));
            return this;
        }

        public ChainBase Log(string log, ELogType type = ELogType.NORMAL)
        {
            Action action;
            switch (type)
            {
                default:
                case ELogType.NORMAL:
                    action = () => Debug.Log(log); break;
                case ELogType.WARRNING:
                    action = () => Debug.LogWarning(log); break;
                case ELogType.ERROR:
                    action = () => Debug.LogError(log); break;
            }
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupNon(action, _player));
            return this;
        }

        public ChainBase Call(Action action)
        {
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupNon(action, _player));
            return this;
        }

        IEnumerator WaitRoutine(float wait)
        {
            yield return new WaitForSeconds(wait);
        }
    }
}
#endregion
