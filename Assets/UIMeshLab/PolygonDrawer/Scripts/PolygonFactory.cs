using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab.Polygon {
    public static class PolygonFactory {

        public static IEnumerable<IMesh> Create(IUnitSizer unit, IPolygon target)
        {
            var circle = new CircleCalculator(target, unit);
            var normal = new ZigZagPolygon(circle, target);
            var hole = new HolePolygon(circle, target);
            var manager = new PolygonDrawerManager(target, normal, hole);
            return manager.Draw();
        }
    }
}