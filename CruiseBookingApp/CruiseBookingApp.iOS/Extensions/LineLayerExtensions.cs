using System;
using System.Linq;
using UIKit;

namespace CruiseBookingApp.iOS.Extensions
{
    public static class LineLayerExtensions
    {
        public static LineLayer GetOrAddLineLayer(this UITextField control)
        {
            var lineLayer = control.Layer.Sublayers?.OfType<LineLayer>().FirstOrDefault();

            if (lineLayer == null)
            {
                lineLayer = new LineLayer();

                control.Layer.AddSublayer(lineLayer);
                control.Layer.MasksToBounds = true;
            }

            return lineLayer;
        }
    }
}
