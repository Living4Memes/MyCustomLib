using MyCustomLib.GraphicFunctions;
using System.Drawing;
using System.Windows.Forms;

namespace MyCustomLib.Controls
{
      public class TestControl : CustomPictureBox
      {
            
            public TestControl() : base()
            {
                  Image = Properties.Resources.DFX;

                  _mainPictureBox.MouseDown += (s, e) =>
                  {
                        switch (e.Button)
                        {
                              case MouseButtons.Left: { _mainPictureBox.Image = _enhancedImage.PlainImage; break; }
                              case MouseButtons.Right: { _mainPictureBox.Image = _enhancedImage.ShadowedImage; break; }
                              case MouseButtons.Middle: { _mainPictureBox.Visible = !_mainPictureBox.Visible; break; }
                        }
                  };
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                  base.OnPaint(e);
            }


      }
}
