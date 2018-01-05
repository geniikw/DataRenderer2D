using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab.Polygon
{
    public class WorldPolygon : WorldDataMesh, IPolygon, IUnitSizer
    {
        public PolygonData data;

        public Vector2 Size
        {
            get
            {
                return Vector2.one ;
            }
        }

        public PolygonData Polygon
        {
            get
            {
                return data;
            }
        }

        protected override IEnumerable<IMesh> MeshFactory
        {
            get
            {
                return PolygonFactory.Create(this, this);
            }
        }
        
    }
}