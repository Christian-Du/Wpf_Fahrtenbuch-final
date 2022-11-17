using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Fahrtenbuch_final
{
    [Serializable]
    internal class Fahrt
    {
        private string start;
        private string ziel;
        private string fahrer;
        private string mitfahrer1 = "";
        private string mitfahrer2 = "";
        private string mitfahrer3 = "";
        private string mitfahrer4 = "";
        private string datum;
        private string kfz;

        public Fahrt(string start, string ziel, string fahrer, string mitfahrer1, string mitfahrer2, string mitfahrer3, string mitfahrer4, string datum, string kfz)
        {
            this.start = start;
            this.ziel = ziel;
            this.fahrer = fahrer;
            this.mitfahrer1 = mitfahrer1;
            this.mitfahrer2 = mitfahrer2;
            this.mitfahrer3 = mitfahrer3;
            this.mitfahrer4 = mitfahrer4;
            this.datum = datum;
            this.kfz = kfz;
        }
        //public void Fahrt() { }
        public void setStart (string start) { this.start=start; }
        public string getStart() { return this.start; }
        public void setZiel (string ziel) { this.ziel = ziel; }
        public string getZiel() { return this.ziel; }
        public void setFahrer (string fahrer) { this.fahrer = fahrer; }
        public string getFahrer() { return this.fahrer; }   
        public void setMitfahrer1 (string mitfahrer1) { this.mitfahrer1 = mitfahrer1; }
        public string getMitfahrer1() { return this.mitfahrer1; }
        public void setMitfahrer2(string mitfahrer1) { this.mitfahrer2 = mitfahrer1; }
        public string getMitfahrer2() { return this.mitfahrer2; }
        public void setMitfahrer3(string mitfahrer1) { this.mitfahrer3 = mitfahrer1; }
        public string getMitfahrer3() { return this.mitfahrer3; }
        public void setMitfahrer4(string mitfahrer1) { this.mitfahrer4 = mitfahrer1; }
        public string getMitfahrer4() { return this.mitfahrer4; }
        public void setDatum(string datum) { this.datum = datum; }
        public string getDatum() { return this.datum; }
        public void setKfz(string kfz) { this.kfz = kfz; }
        public string getKfz() { return this.kfz; }
    }
}
