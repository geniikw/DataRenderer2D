using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Polygon
{
    public class UIPolygon : UIDataMesh, IPolygon, IUnitSize
    {
        public PolygonData data;

        public PolygonData Polygon
        {
            get
            {
                return data;
            }
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2(rectTransform.rect.width , rectTransform.rect.height);
            }
        }

        protected override IEnumerable<IMesh> DrawerFactory
        {
            get
            {
                return PolygonFactory.Create(this,this);
            }
        }
    }
}