using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace MyCustomLib.Controls
{
      public class CustomProgressBar : Control
      {
            public int Minimum { get; set; } = 0;
            public int Maximum { get; set; } = 100;
            public int Value { get { return Val; } set => SetValue(value); }
            public int Step { get { return Stp; } set => SetStep(value); }

            private int Val;
            private int PrevVal;
            private int Stp;
            private bool animationStarted = false;

            public Color ProgressColor { get; set; } = Colors.ProgressBarProgressColor;
            public Color ProgressBorderColor { get; set; } = Colors.ProgressBarBorderColor;

            public CustomProgressBar() : base()
            {
                  BackColor = Colors.ProgressBarBackColor;
                  Size = new Size(100, 30);
                  Value = 0;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                  base.OnPaint(e);

                  Graphics graph = e.Graphics;

                  graph.DrawRectangle(new Pen(ProgressBorderColor, 1f), new Rectangle(0, 0, Width - 1, Height - 1));

                  if (Val != PrevVal)
                        AnimateProgress();
                  else
                  {
                        graph.DrawRectangle(new Pen(ProgressColor, 1f), new Rectangle(0, 0, Width * Val / Maximum, Height));
                        graph.FillRectangle(new SolidBrush(ProgressColor), new Rectangle(0, 0, Width * Val / Maximum, Height));
                  }
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                  base.OnSizeChanged(e);

                  Invalidate();
            }

            public void PerformStep()
            {
                  int temp = Val + Stp;
                  if (temp > Maximum)
                        SetValue(Maximum);
                  else if (temp < Minimum)
                        SetValue(Minimum);
                  else
                        SetValue(temp);
            }

            private void SetValue(int value)
            {
                  PrevVal = Val;
                  if (value <= Maximum && value >= Minimum)
                        Val = value;
                  else if (value > Maximum)
                        Val = Maximum;
                  else if (value < Minimum)
                        Val = Minimum;
                  Invalidate();
            }

            private void SetStep(int value)
            {
                  if (value <= Maximum)
                        Stp = value;
                  else
                        Stp = Maximum;
            }

            private async void AnimateProgress()
            {
                  if (!animationStarted)
                  {
                        animationStarted = true;
                        int counter = PrevVal == 0 ? 1 : PrevVal;
                        while (counter <= Val)
                        {
                              CreateGraphics().FillRectangle(new SolidBrush(ProgressColor), new Rectangle(0, 0, Width * counter / Maximum, Height));
                              counter++;
                              await Task.Delay(new TimeSpan(10000));
                        }
                        PrevVal = Val;
                        animationStarted = false;
                  }
            }
      }
}
