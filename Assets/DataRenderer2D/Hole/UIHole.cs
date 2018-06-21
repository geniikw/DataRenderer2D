
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace geniikw.DataRenderer2D.Hole
{
    public interface IHole
    {
        HoleInfo Hole { get; }
    }

    public class UIHole : UIDataMesh, IUnitSize, IHole
    {
        public HoleInfo hole;

        IMeshDrawer m_holeDrawer;

        protected override void Start()
        {
            hole.editCallback += GeometyUpdateFlagUp;
        }


        public Vector2 Size
        {
            get
            {
                return new Vector2(rectTransform.rect.width, rectTransform.rect.height);
            }
        }

        public HoleInfo Hole
        {
            get
            {
                return hole;
            }
        }

        protected override IEnumerable<IMesh> DrawerFactory
        {
            get
            {
                var h = m_holeDrawer ?? (m_holeDrawer = new HoleDrawer(this, this));
                return h.Draw();
            }
        }
    }
}