using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Polygon {
    public static class PolygonFactory {

        public static IEnumerable<IMesh> Create(IUnitSize unit, IPolygon target)
        {
            var circle = new CircleCalculator(target, unit);
            var normal = new ZigZagPolygon(circle, target);
            var hole = new HolePolygon(circle, target);
            var manager = new PolygonDrawerManager(target, normal, hole);
            return manager.Draw();
        }
    }
}