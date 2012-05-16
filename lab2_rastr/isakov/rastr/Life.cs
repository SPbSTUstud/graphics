using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Life
{
    class Life
    {
        private static Random rnd = new Random();

        private University _prev;
        private University _uni;
        private int _size;
        
        public Life(int size, int numOfLifes)
        {
            _uni = new University(size);
            _size = size;
            
            List<int> generated = new List<int>();
            for (int i = 0; i < numOfLifes; i++)
            {
                int r = rnd.Next(size * size);
                if (generated.Contains(r))
                    continue;
                int row = r % size;
                int column = r / size;
                _uni[row, column] = true;
            }
        }



//пустая (мёртвая) клетка, рядом с которой ровно три живые клетки, оживает;
//если у живой клетки есть две или три живые соседки, то эта клетка продолжает жить; в противном случае (если соседей меньше двух или больше трёх) клетка умирает (от «одиночества» или от «перенаселённости»).
//Игра прекращается, если на поле не останется ни одной «живой» клетки, или если при очередном шаге ни одна из клеток не меняет своего состояния (складывается стабильная конфигурация).
        public void Evaluate(int subTaskNumberSqrt)
        {
            //create shalow copy of universe
            _prev = new University(_uni);

            //evaluate universe size for each task
            int taskSize = _size / subTaskNumberSqrt;

            Task[] tasks = new Task[subTaskNumberSqrt * subTaskNumberSqrt];
            for (int k = 0; k < subTaskNumberSqrt * subTaskNumberSqrt; k++)
            {
                tasks[k] = new Task((state) =>
                {
                    int ind = (int)state;
                    int starti = (ind % subTaskNumberSqrt) * taskSize;
                    int startj = (ind / subTaskNumberSqrt) * taskSize;
                    for (int i = starti; i < starti + taskSize; i++)
                        for (int j = startj; j < startj + taskSize; j++)
                        {
                            int countNeig = CountNeighborn(_prev, i, j);
                            if (_prev[i, j])
                                if (countNeig < 2 || countNeig > 3)
                                    _uni[i, j] = false;
                            if (!_prev[i, j])
                                if (countNeig == 3)
                                        _uni[i, j] = true;
                        }
                },k);
                tasks[k].Start();
            }

            //wait for all tasks end
            for (int i = 0; i < subTaskNumberSqrt * subTaskNumberSqrt; i++)
                tasks[i].Wait();
                
            //clear reference for gc
            _prev = null;
        }

        public University GetUniversity()
        {
            return _uni;
        }

        private int CountNeighborn(University uni, int row, int col)
        {
            int count = 0;
            for (int i = row - 1; i <= row + 1; i++)
                for (int j = col - 1; j <= col + 1; j++)
                    if (uni[i, j])
                        count++;
            if (uni[row, col])
                count--;
            return count;
        }
    }
}
