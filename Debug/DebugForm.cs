using MyCustomLib.Controls;
using MyCustomLib.Animation;
using MyCustomLib.GraphicFunctions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Debug
{
      public partial class DebugForm : CustomForm
      {
            private MovementAnimator animator;
            public DebugForm()
            {
                  InitializeComponent();

                  animator = new MovementAnimator(ref customButton1, new MovementAnimationSettings()
                  {
                        AnimationPath = new AnimationPath(CustomGraphics.GetRectanglePath(new Rectangle(50, 50, 100, 100))),
                        Delay = 1000,
                        Cyclic = false
                  });
            }

            private void customButton1_Click(object sender, System.EventArgs e)
            {
                  if (animator.AnimationState == AnimationState.Stopped)
                        animator.Start();
                  else
                        animator.Stop();

            }
      }
}
