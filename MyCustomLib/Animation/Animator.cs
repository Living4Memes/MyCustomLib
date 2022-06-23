using MyCustomLib.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCustomLib.Animation
{
      public abstract class Animator
      {
            protected CustomButton _mainObject;

            public Animator(ref CustomButton obj)
            {
                  _mainObject = obj;
            }
      }
}
