using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace polar
{
    public static class DataGridViewHelper
    {
        public static float GetXinRow(this DataGridView view, int row)
        {
            string s = view.Rows[row].Cells[0].Value.ToString();
            return float.Parse(s);
        }

        public static float GetYinRow(this DataGridView view, int row)
        {
            string s = view.Rows[row].Cells[1].Value.ToString();
            return float.Parse(s);
        }

        public static float GetZinRow(this DataGridView view, int row)
        {
            string s = view.Rows[row].Cells[2].Value.ToString();
            return float.Parse(s);
        }
    }
}
