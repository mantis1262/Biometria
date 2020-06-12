using System;
using System.Collections.Generic;
using System.Text;

namespace BiometriaApp
{
    public class Przelew
    {
        public string Odbiorca { get; set; }
        public string Rachunek { get; set; }
        public decimal Kwota { get; set; }

       public Przelew(string Odbiorca, string Rach, decimal Kwota)
        {
            this.Kwota = Kwota;
            this.Odbiorca = Odbiorca;
            this.Rachunek = Rach;
        }

    }
}
