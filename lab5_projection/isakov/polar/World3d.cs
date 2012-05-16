using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polar
{
    class World3d
    {
        public static float Angle { get; set; }

        public static PointF GetProjectionXY(float x, float y, float z)
        {
            return new PointF(x, y);
        }

        public static PointF GetProjectionYZ(float x, float y, float z)
        {
            PointF p = CoorHelper.ToDecart(z, -Angle);
            return new PointF(p.X, - p.Y + y);
        }

        public static PointF GetProjectionXZ(float x, float y, float z)
        {
            PointF p = CoorHelper.ToDecart(z, -Angle);
            return new PointF(p.X + x, - p.Y);
        }

        public static PointF GetPoint(float x, float y, float z)
        {
            PointF p = CoorHelper.ToDecart(z, -Angle);
            return new PointF(p.X + x, - p.Y + y);
        }
    }
}
