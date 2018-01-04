using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab.Polygon
{
    public class PolygonDrawerManager 
    {
        IPolygon _target;
        IMeshDrawer _normal;
        IMeshDrawer _angle;
        IMeshDrawer _hole;

        public PolygonDrawerManager(IPolygon target ,
            IMeshDrawer normal, IMeshDrawer angle ,IMeshDrawer hole)
        {
            _target = target;
            _normal = normal;
            _angle = angle;
            _hole = hole;
        }

        public IEnumerable<IMesh> Draw()
        {
            var polyGon = _target.Polygon;

            if (polyGon.count < 3)
                yield break;

            var cc = polyGon.startAngle == 0 && polyGon.endAngle == 1;

            if (polyGon.type == PolygonType.Simple)
                foreach (var m in _normal.Draw())
                    yield return m;

            else if (polyGon.type == PolygonType.CenterVertex || polyGon.innerRatio <= 0f)
                foreach (var m in _angle.Draw())
                    yield return m;

            else if (polyGon.type >= PolygonType.Hole)
                foreach (var m in _hole.Draw())
                    yield return m;

            yield break;
        }
        
    }
}