using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria.Models
{
    public class MinutiaesResult
    {
        private int _centerX;
        private int _centerY;
        private List<Minutiae> _minutiaes;

        public int CenterX { get => _centerX; set => _centerX = value; }
        public int CenterY { get => _centerY; set => _centerY = value; }
        public List<Minutiae> Minutiaes { get => _minutiaes; set => _minutiaes = value; }

        public MinutiaesResult(int centerX, int centerY, List<Minutiae> minutiaes)
        {
            _centerX = centerX;
            _centerY = centerY;
            _minutiaes = minutiaes;
        }
    }
}
