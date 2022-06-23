using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MyCustomLib;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyCustomLib.Controls;

namespace MyCustomLib.Animation
{
      public struct MovementAnimationSettings
      {
            public AnimationPath AnimationPath;
            public bool Cyclic;
            public int Delay;
      }

      public class MovementAnimator : Animator
      {
            #region === События ===
            public event EmptyEventHandler PathChanged;
            public event EmptyEventHandler CyclicChanged;
            public event EmptyEventHandler Tick;
            #endregion

            #region === Поля и свойства ===
            private AnimationState _animationState = AnimationState.Stopped;
            private AnimationPath _path;
            private bool _cyclic;
            private int _delay;

            public AnimationPath Path { get => _path; set { _path = value; PathChanged?.Invoke(); } }
            public AnimationState AnimationState => _animationState;
            public bool Cyclic { get => _cyclic; set { _cyclic = value; CyclicChanged?.Invoke(); } }
            public int Delay { get => _delay; set => _delay = value; }
            #endregion
            public MovementAnimator(ref CustomButton obj, MovementAnimationSettings settings) : base(ref obj)
            {
                  Path = settings.AnimationPath;
                  Cyclic = settings.Cyclic;
                  Delay = settings.Delay;
            }

            public void Start()
            {
                  _animationState = AnimationState.Running;

                  PerformAnimation();
            }

            public void Stop()
            {
                  _animationState = AnimationState.Stopped;
            }

            private void PerformAnimation()
            {
                  Task timer = new Task(() => Task.Delay(Delay));
                  do
                  {
                        foreach (Point point in Path)
                        {
                              if (_animationState == AnimationState.Stopped)
                                    return;

                              _mainObject.Location = point;

                              Tick?.Invoke();

                              timer.Start();
                              timer.Wait();

                              timer = new Task(async () => await Task.Delay(Delay));
                        }
                  } while (Cyclic);
            }
      }
}
