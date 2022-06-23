using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MyCustomLib;
using System.Linq;

namespace MyCustomLib.Animation
{
      public class MovementAnimator : Animator
      {
            private AnimationState _animationState = AnimationState.Stopped;
            private Point _startPoint;
            private Point _endPoint;
            private GraphicsPath _path;
            private List<Point> _animationPoints;

            public Point StartPoint { get => _startPoint; set => _startPoint = value; }
            public Point EndPoint { get => _endPoint; set => _endPoint = value; }
            public GraphicsPath Path { get => _path; set { _path = value; _animationPoints = ParseGraphicsPath(value); } }
            public AnimationState AnimationState => _animationState;

            public MovementAnimator(IAnimatable obj, GraphicsPath animationPath) : base(obj)
            {
                  _startPoint = obj.Location;
                  _path = animationPath;
            }

            public void Start()
            {
                  _animationState = AnimationState.Running;

            }

            public void Pause()
            {
                  _animationState = AnimationState.Paused;

            }

            public void Stop()
            {
                  _animationState = AnimationState.Stopped;

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
