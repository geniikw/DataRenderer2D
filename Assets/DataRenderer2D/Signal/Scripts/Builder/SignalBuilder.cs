using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Signal
{
    public interface ISignalData
    {
        SignalData Signal { get; }
    }

    public class SignalBuilder 
    {
        ISignalData _data;
        IUnitSize _unit;

        public SignalBuilder(ISignalData data, IUnitSize unit)
        {
            _data = data;
            _unit = unit;
        }

        public IEnumerable<IMesh> Draw()
        {
            var size = _unit.Size;
            var signal = _data.Signal;
            var divide = signal.divide;
            var amp = signal.amplify;
            var tFactor = signal.timeFactor;
            var frq = signal.frequncy;
            var color = signal.Color;
            var t = Time.realtimeSinceStartup;

            var width = size.x / divide;

            var v0 = new Vertex(new Vector2(-size.x / 2f, -size.y / 2f), Vector2.zero, Color.white);
            var v1 = new Vertex(new Vector2(-size.x / 2f, size.y / 2f), Vector2.zero, Color.white);
            var v2 = new Vertex(new Vector2(-size.x / 2f + width, size.y / 2f), Vector2.zero, Color.white);
            var v3 = new Vertex(new Vector2(-size.x / 2f + width, -size.y / 2f), Vector2.zero, Color.white);

            for (var i = 0f; i < 1f; i += 1f / divide)
            {
                var ni = Mathf.Min(1f, i + 1f / divide);

                v0.position.x = -size.x / 2f + i * size.x;
                v1.position.x = -size.x / 2f + i * size.x;
                v2.position.x = -size.x / 2f + ni * size.x;
                v3.position.x = -size.x / 2f + ni * size.x;
                if (signal.up)
                {
                    v1.position.y = size.y / 2f + amp * Mathf.Sin((i + t / tFactor) * frq);
                    v2.position.y = size.y / 2f + amp * Mathf.Sin((ni + t / tFactor) * frq);
                }

                if (signal.down)
                {
                    v0.position.y = -size.y / 2f + amp * Mathf.Sin((i + t / tFactor) * frq);
                    v3.position.y = -size.y / 2f + amp * Mathf.Sin((ni + t / tFactor) * frq);
                }

                v0.color = color.Evaluate(i);
                v1.color = color.Evaluate(i);
                v2.color = color.Evaluate(ni);
                v3.color = color.Evaluate(ni);

                yield return new Quad(v0, v3, v1, v2);
            }
            yield return new Quad();
        }
        
    }
}