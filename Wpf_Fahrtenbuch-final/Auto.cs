using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Fahrtenbuch_final
{
    [Serializable] // Objekte, die gespeichert werden sollen, müssen als "Serializable" gekennzeichnet sein.
    internal class Auto
    {
        // Attribute als private (wegen Datenkapselung {DK})
        private string marke = "";
        private string modell = "";
        private string kennzeichen = "";
        private int bauJahr = 0 ;

        // öffentlich Konstruktoren;
        public Auto() { }
        public Auto(string marke, string modell, string kennzeichen, int bauJahr)
        {
            this.marke = marke;
            this.modell = modell;
            this.kennzeichen = kennzeichen;
            this.bauJahr = bauJahr;
        }
        public void setMarke(string marke)
        {
            this.marke = marke;
        }
        public string getMarke()
        {
            return this.marke; // this.marke && marke sind okay
        }
        public void setModell(string modell)
        {
            this.modell = modell;
        }
        public string getModell()
        {
            return this.modell; // this.marke && marke sind okay
        }
        public void setKennzeichen(string kennzeichen)
        {
            this.kennzeichen = kennzeichen;
        }
        public string getKennzeichen()
        {
            return this.kennzeichen; // this.marke && marke sind okay
        }
        public void setBauJahr(int bauJahr)
        {
            this.bauJahr = bauJahr;
        }
        public int getBauJahr()
        {
            return this.bauJahr; // this.marke && marke sind okay
        }

    }
}
