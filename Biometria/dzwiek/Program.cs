using Biometria.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dzwiek
{
    class Program
    {
        static void Main(string[] args)
        {
            AudioHelper audioHelper = new AudioHelper();
            double[] left, right;
            double[] left2, right2;
            audioHelper.openWav("C:/Users/Mantis/Desktop/sem1/test/FAML_Sr3.wav", out left, out right);
            audioHelper.openWav("C:/Users/Mantis/Desktop/sem1/test/FAML_Sr4.wav", out left2, out right2);
            Console.ReadKey();
        }
    }
}
