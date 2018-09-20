using geniikw.DataRenderer2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Example
{

    public class DragExample : MonoBehaviour
    {

        public UILine line;
        public Gradient grad;
        public Gradient red;
        private bool isGrad = false;

        public void Clear()
        {
            line.line.Clear();

        }

        public void ColorToggle()
        {
            if (isGrad)
                line.line.option.color = grad;
            else
                line.line.option.color = red;
            isGrad = !isGrad;

            line.GeometyUpdateFlagUp();
        }

    }
}