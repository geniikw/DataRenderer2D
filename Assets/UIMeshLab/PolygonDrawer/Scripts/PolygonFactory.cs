using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab.Polygon {
    public static class PolygonFactory {

        public static IEnumerable<IMesh> Create(IUnitSizer unit, IPolygon target)
        {
            var circle = new CircleCalculator(target, unit);
            var normal = new NormalDrawer(circle, target);
            var angleCut = new CenterPolygon(circle, target);
            var hole = new HolePolygon(circle, target);
            var manager = new PolygonDrawerManager(target, normal, angleCut, hole);
            return manager.Draw();
        }
    }
}