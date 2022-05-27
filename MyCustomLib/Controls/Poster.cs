using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCustomLib.Controls
{
      public class Poster : CustomPictureBox
      {
            private Label _mainLabel = new Label();

            public new string Text { get => _mainLabel.Text; set => _mainLabel.Text = value; }
            public new Font Font { get => _mainLabel.Font; set => _mainLabel.Font = value; }
            public new Color ForeColor { get => _mainLabel.ForeColor; set => _mainLabel.ForeColor = value; }

            [Category("Custom settings"), Description("Sets indent of text from the bottom edge.")]
            public int TextIndent { get => _mainLabel.Height; set => _mainLabel.Height = value; }

            public Poster() : base()
            {
                  Initialize();
            }

            private void Initialize()
            {
                  _mainLabel.Dock = DockStyle.Bottom;
                  _mainLabel.BackColor = Color.Transparent;
                  _mainLabel.ForeColor = Colors.PosterFontColor;
                  _mainLabel.Height = 50;
                  _mainLabel.TextAlign = ContentAlignment.TopCenter;

                  _mainLabel.Parent = this;
                  Controls.Add(_mainLabel);
            }
      }
}
