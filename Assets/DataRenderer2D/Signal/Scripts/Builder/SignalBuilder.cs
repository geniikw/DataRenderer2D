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

        public void ApplySignal(ref float x0, ref float x1, SignalData.SignalOneSet data)
        {

        }

        public IEnumerable<IMesh> Draw()
        {
            var size = _unit.Size;

            var signal = _data.Signal;
            var divide = signal.divide;
            var color = signal.Color;

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

                if (signal.up.use)
                {
                    v1.position.y = size.y / 2f + signal.up.Output(i, signal.t);
                    v2.position.y = size.y / 2f + signal.up.Output(ni, signal.t);
                }
                if (signal.down.use)
                {
                    v0.position.y = -size.y / 2f + signal.down.Output(i, signal.t);
                    v3.position.y = -size.y / 2f + signal.down.Output(ni, signal.t);
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