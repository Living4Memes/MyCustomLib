using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomLib.Animation
{
      public class AnimationPath : IEnumerable
      {
            private List<Point> _animationPoints;

            public List<Point> AnimationPoints { get => _animationPoints; set => _animationPoints = value; }

            public AnimationPath(GraphicsPath gp)
            {
                  AnimationPoints = ParseGraphicsPath(gp);
            }

            public IEnumerator GetEnumerator()
            {
                  return AnimationPoints.GetEnumerator();
            }

            private List<Point> ParseGraphicsPath(GraphicsPath gp)
            {
                  List<Point> result = new List<Point>();

                  foreach (PointF point in gp.PathPoints)
                        result.Add(new Point(point.X.ToInt(), point.Y.ToInt()));

                  return result.Distinct().ToList();
            }
      }
}
