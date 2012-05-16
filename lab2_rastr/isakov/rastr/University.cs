using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life
{
    class University
    {
        private bool[,] uni;
        private int size;

        public University(int size)
        {
            uni = new bool[size,size];
            this.size = size;
        }

        public University(University u)
        {
            this.size = u.size;            
            uni = (bool[,])u.uni.Clone();
        }

        public bool this[int x, int y]
        {
            get{
                int i = x;
                int j = y;
                if (x < 0)
                    i = size + x;
                if (y < 0)
                    j = size + y;
                if (x >= size)
                    i = x - size;
                if (y >= size)
                    j = y - size;
                    
                return uni[i,j];
            }
            set{
                int i = x;
                int j = y;
                if (x < 0)
                    i = size + x;
                if (y < 0)
                    j = size + y;
                if (x >= size)
                    i = x - size;
                if (y >= size)
                    j = y - size;

                uni[i, j] = value;
            }
        }

        public bool[,] ToBoolArray()
        {
            return uni;
        }
    }
}
