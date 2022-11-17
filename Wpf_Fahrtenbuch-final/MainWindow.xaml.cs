using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Fahrtenbuch_final
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Auto> genListeAutos = new List<Auto>();
        List<Person> genListeMitarbeiter = new List<Person>();
        List<Fahrt> genListeFahrten = new List<Fahrt>();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEintrag_Click(object sender, RoutedEventArgs e)
        {
            // prüfen ob die gewünschten Felder gefüllt sind, zwischenspeichern in String, in Liste eintragen und pkw Object erstellen und in ListeAutos eintragen
            if (txtMarke.Text != "" && txtModell.Text != "" && txtKennzeichen.Text != "" && txtBaujahr.Text != "")
            {
                string marke = txtMarke.Text;
                string modell = txtModell.Text;
                string kennzeichen = txtKennzeichen.Text;
                int baujahr = Convert.ToInt32(txtBaujahr.Text);
                lstAuto.Items.Add(marke + " " + modell + " " + kennzeichen + " " + " " + baujahr);

                // Auto Obj anlegen

                Auto pkw = new Auto(marke, modell, kennzeichen, baujahr);
                genListeAutos.Add(pkw);
            }
            else
            {
                MessageBox.Show("Es müssen alle Felder belegt sein");
            }
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            // FileDialog öffnen, Speicherort wählen, Liste als Datei speichern
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FileStream fs1 = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                BinaryFormatter bf1 = new BinaryFormatter();
                bf1.Serialize(fs1, genListeAutos);
                fs1.Close();
            }
        }

        private void btnLaden_Click(object sender, RoutedEventArgs e)
        {
            // Datei wählen, AutoListe aus Datei Laden und Objekte zu String umwandel und in wpf Liste lstAuto einfügen
            OpenFileDialog openFileDialog = new OpenFileDialog();
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    FileStream fs_lesen = new FileStream(openFileDialog.FileName, FileMode.Open);
                    BinaryFormatter bf_lesen = new BinaryFormatter();
                    genListeAutos = (List<Auto>)bf_lesen.Deserialize(fs_lesen);
                    fs_lesen.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: \n" + ex.Message);
            }
            // ListBox leeren
            lstAuto.Items.Clear();

            foreach (Auto obj in genListeAutos)
            {
                string marke = obj.getMarke();
                string modell = obj.getModell();
                string kennzeichen = obj.getKennzeichen();
                int baujahr = obj.getBauJahr();
                lstAuto.Items.Add(marke + " " + modell + " " + kennzeichen + " " + " " + baujahr);
            }
        }

        private void btnMaLaden_Click(object sender, RoutedEventArgs e)
        {
            // Datei wählen, MitarbeiterListe aus Datei Laden und Objekte zu String umwandel und in wpf Liste lst Mitarbeiter einfügen
            OpenFileDialog openedFileDialog = new OpenFileDialog();
            try
            {
                if (openedFileDialog.ShowDialog() == true)
                {
                    FileStream fs_lesen = new FileStream(openedFileDialog.FileName, FileMode.Open);
                    BinaryFormatter bf_lesen = new BinaryFormatter();
                    genListeMitarbeiter = (List<Person>)bf_lesen.Deserialize(fs_lesen);
                    fs_lesen.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Fehler: \n" + ex.Message); }
            // ListBox leeren
            lstMitarbeiter.Items.Clear();

            foreach (Person obj in genListeMitarbeiter)
            {
                string name = obj.getName();
                string vorname = obj.getVorame();
                string wohnort = obj.getWohnort();
                string telefon = obj.getTelefon();
                string email = obj.getEmail();
                string gebDat = obj.getGebDatum();
                bool fuehrerschein = obj.getFuehrerschein();
                lstMitarbeiter.Items.Add(name + ", " + vorname + " FS:" + fuehrerschein + " " + gebDat + " " + wohnort + " " + telefon + " " + email);
            }
        }

        private void btnMaEintrag_Click(object sender, RoutedEventArgs e)
        {
            // prüfen ob alle Felder gefüllt, Felder zu String und eintragen des Strings in Liste und Obj erzeugen
            if (txtName.Text != "" && txtVorname.Text != "" && txtPLZ.Text != "" && txtOrt.Text != "" && txtStrasse.Text != "" && txtTelefon.Text != "" && txtEmail.Text != ""
                && txtGebDat.Text != "" && ((bool)rbtnFuehrerscheinJa.IsChecked ^ (bool)rbtnFuehrerscheinNein.IsChecked))
            {
                string name = txtName.Text;
                string vorname = txtVorname.Text;
                string strasse = txtStrasse.Text;
                string strNr = txtStrasseNummer.Text;
                string plz = txtPLZ.Text;
                string ort = txtOrt.Text;
                string telefon = txtTelefon.Text;
                string email = txtEmail.Text;
                string gebDat = txtGebDat.Text;
                bool fuehrerschein = (bool)rbtnFuehrerscheinJa.IsChecked;

                lstMitarbeiter.Items.Add(name + ", " + vorname + " " + "FS:" + fuehrerschein + " " + gebDat + " " + plz
                    + " " + ort + " " + strasse + " " + strNr + " " + telefon + " " + email);

                // Person Obj anlegen
                Person mitarbeiter = new Person(name, vorname, strasse + " " + strNr + " " + plz + " " + ort, gebDat, telefon, email, fuehrerschein);
                genListeMitarbeiter.Add(mitarbeiter);
            }
            else
            {
                MessageBox.Show("Es müssen alle Felder belegt sein");
            }
        }

        private void btnMaSpeichern_Click(object sender, RoutedEventArgs e)
        {
            // File Dialog öffnen, mit Filestream und BinaryFormatter Liste in Datei sicher
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FileStream fs1 = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                BinaryFormatter bf1 = new BinaryFormatter();
                bf1.Serialize(fs1, genListeMitarbeiter);
                fs1.Close();
            }
        }
        private void cbx_Kfz_DropDownOpened(object sender, EventArgs e)
        {
            // prüfen ob genListeAutos bereits geladen, ggf File Dialog öffnen, mit Filestream und BinaryFormatter aus der gewählten Datei die genListeAutos füllen
            cbx_Kfz.Items.Clear();
            if (genListeAutos.Count == 0)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                try
                {
                    if (openFileDialog.ShowDialog() == true)
                    {
                        FileStream fs_lesen = new FileStream(openFileDialog.FileName, FileMode.Open);
                        BinaryFormatter bf_lesen = new BinaryFormatter();
                        genListeAutos = (List<Auto>)bf_lesen.Deserialize(fs_lesen);
                        fs_lesen.Close();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Fehler: \n" + ex.Message); }
            }

            foreach (Auto auto in genListeAutos)
            {
                cbx_Kfz.Items.Add(auto.getKennzeichen());
            }

        }
        private void cbx_Fahrer_DropDownOpened(object sender, EventArgs e)
        {
            cbx_Fahrer.Items.Clear();
            // prüfen ob genListeMitarbeiter bereits geladen, ggf File Dialog öffnen, mit Filestream und BinaryFormatter aus der gewählten Datei die genListe füllen
            if (genListeMitarbeiter.Count == 0)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                try
                {
                    if (openFileDialog.ShowDialog() == true)
                    {
                        FileStream fs_lesen = new FileStream(openFileDialog.FileName, FileMode.Open);
                        BinaryFormatter bf_lesen = new BinaryFormatter();
                        genListeMitarbeiter = (List<Person>)bf_lesen.Deserialize(fs_lesen);
                        fs_lesen.Close();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Fehler: \n" + ex.Message); }
            }

            // laden der combobox Felder und convertieren zu String
            string mitfahrer1, mitfahrer2, mitfahrer3, mitfahrer4;
            if (cbx_Mitfahrer_1.SelectedItem == null) { mitfahrer1 = " "; } else { mitfahrer1 = cbx_Mitfahrer_1.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_2.SelectedItem == null) { mitfahrer2 = " "; } else { mitfahrer2 = cbx_Mitfahrer_2.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_3.SelectedItem == null) { mitfahrer3 = " "; } else { mitfahrer3 = cbx_Mitfahrer_3.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_4.SelectedItem == null) { mitfahrer4 = " "; } else { mitfahrer4 = cbx_Mitfahrer_4.SelectedItem.ToString(); }

            // nur die Personen die noch nicht woanders eingetragen sind UND einen Führerschein haben erscheinen als Auswahlmöglichkeit
            foreach (Person person in genListeMitarbeiter)
            {
                if (person.getFuehrerschein() // Person als Fahrer eintragen wenn Person Führerschein hat
                    && !(mitfahrer1 == (person.getName() + ", " + person.getVorame())) // check if person auf anderem Platz eingetragen
                    && !(mitfahrer2 == (person.getName() + ", " + person.getVorame()))
                    && !(mitfahrer3 == (person.getName() + ", " + person.getVorame()))
                    && !(mitfahrer4 == (person.getName() + ", " + person.getVorame())))
                {
                    cbx_Fahrer.Items.Add(person.getName() + ", " + person.getVorame());
                }
            }
        }

        private void cbx_Mitfahrer_1_DropDownOpened(object sender, EventArgs e)
        {
            cbx_Mitfahrer_1.Items.Clear();
            // prüfen ob genListeMitarbeiter bereits geladen, ggf File Dialog öffnen, mit Filestream und BinaryFormatter aus der gewählten Datei die genListeFahrten füllen
            if (genListeMitarbeiter.Count == 0)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                try
                {
                    if (openFileDialog.ShowDialog() == true)
                    {
                        FileStream fs_lesen = new FileStream(openFileDialog.FileName, FileMode.Open);
                        BinaryFormatter bf_lesen = new BinaryFormatter();
                        genListeMitarbeiter = (List<Person>)bf_lesen.Deserialize(fs_lesen);
                        fs_lesen.Close();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Fehler: \n" + ex.Message); }
            }

            // laden der combobox Felder und convertieren zu String
            string fahrer, mitfahrer2, mitfahrer3, mitfahrer4;
            if (cbx_Fahrer.SelectedItem == null) { fahrer = " "; } else { fahrer = cbx_Fahrer.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_2.SelectedItem == null) { mitfahrer2 = " "; } else { mitfahrer2 = cbx_Mitfahrer_2.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_3.SelectedItem == null) { mitfahrer3 = " "; } else { mitfahrer3 = cbx_Mitfahrer_3.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_4.SelectedItem == null) { mitfahrer4 = " "; } else { mitfahrer4 = cbx_Mitfahrer_4.SelectedItem.ToString(); }

            // nur die Personen die noch nicht woanders eingetragen sind erscheinen als Auswahlmöglichkeit
            foreach (Person person in genListeMitarbeiter)
            {
                if (!(fahrer == (person.getName() + ", " + person.getVorame())) // check if person auf anderem Platz eingetragen
                    && !(mitfahrer2 == (person.getName() + ", " + person.getVorame()))
                    && !(mitfahrer3 == (person.getName() + ", " + person.getVorame()))
                    && !(mitfahrer4 == (person.getName() + ", " + person.getVorame())))
                {
                    cbx_Mitfahrer_1.Items.Add(person.getName() + ", " + person.getVorame());
                }
            }
        }

        private void cbx_Mitfahrer_2_DropDownOpened(object sender, EventArgs e)
        {
            cbx_Mitfahrer_2.Items.Clear();
            // prüfen ob genListeMitarbeiter bereits geladen, ggf File Dialog öffnen, mit Filestream und BinaryFormatter aus der gewählten Datei die genListeFahrten füllen
            if (genListeMitarbeiter.Count == 0)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                try
                {
                    if (openFileDialog.ShowDialog() == true)
                    {
                        FileStream fs_lesen = new FileStream(openFileDialog.FileName, FileMode.Open);
                        BinaryFormatter bf_lesen = new BinaryFormatter();
                        genListeMitarbeiter = (List<Person>)bf_lesen.Deserialize(fs_lesen);
                        fs_lesen.Close();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Fehler: \n" + ex.Message); }
            }

            // laden der combobox Felder und convertieren zu String
            string fahrer, mitfahrer1, mitfahrer3, mitfahrer4;
            if (cbx_Fahrer.SelectedItem == null) { fahrer = " "; } else { fahrer = cbx_Fahrer.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_1.SelectedItem == null) { mitfahrer1 = " "; } else { mitfahrer1 = cbx_Mitfahrer_1.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_3.SelectedItem == null) { mitfahrer3 = " "; } else { mitfahrer3 = cbx_Mitfahrer_3.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_4.SelectedItem == null) { mitfahrer4 = " "; } else { mitfahrer4 = cbx_Mitfahrer_4.SelectedItem.ToString(); }

            // nur die Personen die noch nicht woanders eingetragen sind erscheinen als Auswahlmöglichkeit
            foreach (Person person in genListeMitarbeiter)
            {
                if (!(fahrer == (person.getName() + ", " + person.getVorame())) // check if person auf anderem Platz eingetragen
                    && !(mitfahrer1 == (person.getName() + ", " + person.getVorame()))
                    && !(mitfahrer3 == (person.getName() + ", " + person.getVorame()))
                    && !(mitfahrer4 == (person.getName() + ", " + person.getVorame())))
                {
                    cbx_Mitfahrer_2.Items.Add(person.getName() + ", " + person.getVorame());
                }
            }
        }

        private void cbx_Mitfahrer_3_DropDownOpened(object sender, EventArgs e)
        {
            cbx_Mitfahrer_3.Items.Clear();
            // prüfen ob genListeMitarbeiter bereits geladen, ggf File Dialog öffnen, mit Filestream und BinaryFormatter aus der gewählten Datei die genListeFahrten füllen
            if (genListeMitarbeiter.Count == 0)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                try
                {
                    if (openFileDialog.ShowDialog() == true)
                    {
                        FileStream fs_lesen = new FileStream(openFileDialog.FileName, FileMode.Open);
                        BinaryFormatter bf_lesen = new BinaryFormatter();
                        genListeMitarbeiter = (List<Person>)bf_lesen.Deserialize(fs_lesen);
                        fs_lesen.Close();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Fehler: \n" + ex.Message); }
            }

            // laden der combobox Felder und convertieren zu String
            string fahrer, mitfahrer1, mitfahrer2, mitfahrer4;
            if (cbx_Fahrer.SelectedItem == null) { fahrer = " "; } else { fahrer = cbx_Fahrer.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_1.SelectedItem == null) { mitfahrer1 = " "; } else { mitfahrer1 = cbx_Mitfahrer_1.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_2.SelectedItem == null) { mitfahrer2 = " "; } else { mitfahrer2 = cbx_Mitfahrer_2.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_4.SelectedItem == null) { mitfahrer4 = " "; } else { mitfahrer4 = cbx_Mitfahrer_4.SelectedItem.ToString(); }

            // nur die Personen die noch nicht woanders eingetragen sind erscheinen als Auswahlmöglichkeit
            foreach (Person person in genListeMitarbeiter)
            {
                if (!(fahrer == (person.getName() + ", " + person.getVorame())) // check if person auf anderem Platz eingetragen
                    && !(mitfahrer1 == (person.getName() + ", " + person.getVorame()))
                    && !(mitfahrer2 == (person.getName() + ", " + person.getVorame()))
                    && !(mitfahrer4 == (person.getName() + ", " + person.getVorame())))
                {
                    cbx_Mitfahrer_3.Items.Add(person.getName() + ", " + person.getVorame());
                }
            }
        }

        private void cbx_Mitfahrer_4_DropDownOpened(object sender, EventArgs e)
        {
            cbx_Mitfahrer_4.Items.Clear();
            // prüfen ob genListeMitarbeiter bereits geladen, ggf File Dialog öffnen, mit Filestream und BinaryFormatter aus der gewählten Datei die genListeFahrten füllen
            if (genListeMitarbeiter.Count == 0)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                try
                {
                    if (openFileDialog.ShowDialog() == true)
                    {
                        FileStream fs_lesen = new FileStream(openFileDialog.FileName, FileMode.Open);
                        BinaryFormatter bf_lesen = new BinaryFormatter();
                        genListeMitarbeiter = (List<Person>)bf_lesen.Deserialize(fs_lesen);
                        fs_lesen.Close();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Fehler: \n" + ex.Message); }
            }

            // laden der combobox Felder und convertieren zu String
            string fahrer, mitfahrer1, mitfahrer2, mitfahrer3;
            if (cbx_Fahrer.SelectedItem == null) { fahrer = " "; } else { fahrer = cbx_Fahrer.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_1.SelectedItem == null) { mitfahrer1 = " "; } else { mitfahrer1 = cbx_Mitfahrer_1.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_2.SelectedItem == null) { mitfahrer2 = " "; } else { mitfahrer2 = cbx_Mitfahrer_2.SelectedItem.ToString(); }
            if (cbx_Mitfahrer_3.SelectedItem == null) { mitfahrer3 = " "; } else { mitfahrer3 = cbx_Mitfahrer_3.SelectedItem.ToString(); }

            // nur die Personen die noch nicht woanders eingetragen sind erscheinen als Auswahlmöglichkeit
            foreach (Person person in genListeMitarbeiter)
            {
                if (!(fahrer == (person.getName() + ", " + person.getVorame())) // check if person auf anderem Platz eingetragen
                    && !(mitfahrer1 == (person.getName() + ", " + person.getVorame()))
                    && !(mitfahrer2 == (person.getName() + ", " + person.getVorame()))
                    && !(mitfahrer3 == (person.getName() + ", " + person.getVorame())))
                {
                    cbx_Mitfahrer_4.Items.Add(person.getName() + ", " + person.getVorame());
                }
            }
        }

        private void btnFaLaden_Click(object sender, RoutedEventArgs e)
        {
            // File Dialog öffnen, mit Filestream und BinaryFormatter aus der gewählten Datei die genListeFahrten füllen
            OpenFileDialog openedFileDialog = new OpenFileDialog();
            try
            {
                if (openedFileDialog.ShowDialog() == true)
                {
                    FileStream fs_lesen = new FileStream(openedFileDialog.FileName, FileMode.Open);
                    BinaryFormatter bf_lesen = new BinaryFormatter();
                    genListeFahrten = (List<Fahrt>)bf_lesen.Deserialize(fs_lesen);
                    fs_lesen.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Fehler: \n" + ex.Message); }

            // ListBox leeren
            lstFahrten.Items.Clear();

            // mit foreach Propertys aus jeweiligen Objekten holen
            foreach (Fahrt frt in genListeFahrten)
            {
                string start = frt.getStart();
                string ziel = frt.getZiel();
                string fahrer = frt.getFahrer();
                string mitfahrer1 = frt.getMitfahrer1();
                string mitfahrer2 = frt.getMitfahrer2();
                string mitfahrer3 = frt.getMitfahrer3();
                string mitfahrer4 = frt.getMitfahrer4();
                string datum = frt.getDatum();
                // in wpf lstFahrten Liste eintragen
                lstFahrten.Items.Add(datum + " von: " + start + " nach: " + ziel + " Fahrer: " + fahrer
                    + " Mitfahrer (" + mitfahrer1 + ", " + mitfahrer2 + ", " + mitfahrer3 + ", " + mitfahrer4 + ")");
            }
        }

        private void btnFaEintragen_Click(object sender, RoutedEventArgs e)
        {
            // check ob alle Pflichtfelder gefüllt sind
            if (txtStart.Text != "" && txtZiel.Text != "" && cbx_Fahrer.Text != "" && datDatum.Text != "")
            {
                string start = txtStart.Text;
                string ziel = txtZiel.Text;
                string fahrer = cbx_Fahrer.Text;
                string mitfahrer1 = cbx_Mitfahrer_1.Text;
                string mitfahrer2 = cbx_Mitfahrer_2.Text;
                string mitfahrer3 = cbx_Mitfahrer_3.Text;
                string mitfahrer4 = cbx_Mitfahrer_4.Text;
                string datum = datDatum.Text;
                string kfz = cbx_Kfz.Text;
                // add to wpf list
                lstFahrten.Items.Add(datum + " " + kfz + " von: " + start + " nach: " + ziel + " Fahrer: " + fahrer
                    + " Mitfahrer: (" + mitfahrer1 + ", " + mitfahrer2 + ", " + mitfahrer3 + ", " + mitfahrer4 + ")");

                // Obj Fahrt erstellen und in Liste eintragen
                Fahrt fahrt = new Fahrt(start, ziel, fahrer, mitfahrer1, mitfahrer2, mitfahrer3, mitfahrer4, datum, kfz);
                genListeFahrten.Add(fahrt);
            }
            else
            {
                MessageBox.Show("Es müssen alle Felder, die mit * gekennzeichnet sind, belegt sein.");
            }
        }

        private void btnFaSpeichern_Click(object sender, RoutedEventArgs e)
        {
            // File Dialog öffnen, mit Filestream und BinaryFormatter die Liste genListeFahrten in gewählte Datei schreiben
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FileStream fs1 = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                BinaryFormatter bf1 = new BinaryFormatter();
                bf1.Serialize(fs1, genListeFahrten);
                fs1.Close();
            }
        }
    }
}
