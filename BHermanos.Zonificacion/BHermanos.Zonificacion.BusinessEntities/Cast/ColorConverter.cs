using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities.Cast
{
    public static class ColorConverter
    {
        public static Color GetColor(string color)
        {
            if (!string.IsNullOrEmpty(color) && color.Length == 6)
            {
                string r = color.Substring(0, 2);
                string g = color.Substring(2, 2);
                string b = color.Substring(4, 2);
                return System.Drawing.Color.FromArgb(Convert.ToInt32(r, 16), Convert.ToInt32(g, 16), Convert.ToInt32(b, 16));
            }
            return System.Drawing.Color.Yellow;
        }
    }
}