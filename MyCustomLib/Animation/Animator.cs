using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCustomLib.Animation
{
      public interface IAnimatable
      {
            Point Location { get; set; }
            Size Size { get; set; }
      }

      public class Animator
      {
            private IAnimatable _mainObject;

            public Animator(IAnimatable obj)
            {

            }
      }
}
