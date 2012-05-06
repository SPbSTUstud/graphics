using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polar
{
    public static class CoorHelper
    {
        public static PointF ToDecart(float radius, float angle)
        {
            float x = (float)(radius * Math.Cos((angle * Math.PI / 180)));
            float y = (float)(radius * Math.Sin((angle * Math.PI / 180)));
            return new PointF(x, y);
        }
    }
}
