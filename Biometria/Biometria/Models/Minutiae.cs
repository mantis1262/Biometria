using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria.Models
{
    public class Minutiae
    {
        private int _x;
        private int _y;
        private int _crossingNumber;

        public Minutiae()
        {
            _x = 0;
            _y = 0;
            _crossingNumber = 0;
        }

        public Minutiae(int x, int y, int crossingNumber)
        {
            _x = x;
            _y = y;
            _crossingNumber = crossingNumber;
        }

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int CrossingNumber { get => _crossingNumber; set => _crossingNumber = value; }
    }
}
