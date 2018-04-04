using System;
using CoreAnimation;

namespace CruiseBookingApp.iOS
{
    public class LineLayer : CALayer
    {
        public static nfloat LineHeight = 2f;

        public LineLayer()
        {
            BorderWidth = LineHeight;
        }
    }
}
