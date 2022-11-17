using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Fahrtenbuch_final
{
    [Serializable]
    internal class Person
    {

    //Der Betrieb hat Mitarbeiter, die die Fahrzeuge des Fuhrparks benutzen können.
    //a) Erstellen Sie eine Klasse Person mit Vorname, Name, Wohnort(PLZ, Ort, Straße,
    //Hausnummer), Geburtsdatum, Telefon, E-Mail.
    //Zeichnen Sie ein UML-Klassendiagramm und programmieren Sie die Klasse.
        private string name;
        private string vorname;
        private string wohnort;
        private string gebDatum;
        private string telefon;
        private string eMail;
        private bool fuehrerschein;
        

        public Person(string name, string vorname, string wohnort, string gebDatum, string telefon, string eMail, bool fuehrerschein)
        {
            this.name = name;
            this.vorname = vorname;
            this.wohnort = wohnort;
            this.gebDatum = gebDatum;
            this.telefon = telefon;
            this.eMail = eMail;
            this.fuehrerschein = fuehrerschein;
        }
        public void setName(string name) { this.name = name; }
        public string getName() { return name; }
        public void setVorname (string vorname) { this.vorname = vorname; }
        public string getVorame() { return vorname; }
        public void setWohnort(string wohnort) { this.wohnort = wohnort; }
        public string getWohnort() { return wohnort; }
        public void setGebDatum(string gebDatum) { this.gebDatum = gebDatum; }
        public string getGebDatum() { return gebDatum; }
        public void setTelefon(string telefon) { this.telefon = telefon; }
        public string getTelefon() { return telefon; }
        public void setEmail(string eMail) { this.eMail = eMail; }
        public string getEmail() { return eMail; }
        public void setFuehrerschein(bool fuehrerschein) { this.fuehrerschein = fuehrerschein; }
        public bool getFuehrerschein() { return fuehrerschein; }
    }
}
