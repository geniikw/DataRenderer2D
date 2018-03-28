using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D
{
    public interface ISpline
    {
        Spline Line { get; }
    }

    public interface ISplineOption
    {
        LineOption Option { get; }
    }
}