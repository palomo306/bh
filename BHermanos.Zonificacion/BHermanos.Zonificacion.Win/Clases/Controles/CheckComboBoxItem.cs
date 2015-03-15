using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.Win.Clases.Controles
{
    public class CheckComboBoxItem
    {
        public CheckComboBoxItem(string text, object tag, bool initialCheckState)
        {
            _checkState = initialCheckState;
            _text = text;
            _tag = tag;
        }

        private bool _checkState = false;
        public bool CheckState
        {
            get { return _checkState; }
            set { _checkState = value; }
        }

        private string _text = "";
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }


        private object _tag = null;
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public override string ToString()
        {
            return "--Seleccione un estado--";
        }
    }
}
