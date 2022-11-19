using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using US_5A_Net.models;

namespace US_5A_Net
{
    public partial class AirportForm : Form
    {
        


        public AirportForm()
        {
            InitializeComponent();
            FlightsDGV.AutoGenerateColumns = false;
            FlightsDGV.DataSource = ReadDb();
            InfoStatCal();
        }
        private void CreateDb(FlightsForm infoForm)
        {
            using (ApplicationContext db = new ApplicationContext(DataBaseHellper.Options()))
            {
                infoForm.Flights.id = Guid.NewGuid();
                db.FlightersBD.Add(infoForm.Flights);
                db.SaveChanges();
            }
        }
        private static List<Flights> ReadDb()
        {
            using (ApplicationContext db = new ApplicationContext(DataBaseHellper.Options()))
            {
                return db.FlightersBD.ToList();
            }
        }
        private static void UpDateDb(Flights pln)
        {
            using (ApplicationContext db = new ApplicationContext(DataBaseHellper.Options()))
            {
                var Pln = db.FlightersBD.FirstOrDefault(u => u.id == pln.id);
                if (Pln != null)
                {
                    Pln.numflight = pln.numflight;
                    Pln.type = pln.type;
                    Pln.eta = pln.eta;
                    Pln.countPas = pln.countPas;
                    Pln.pricePas = pln.pricePas;
                    Pln.countCrew = pln.countCrew;
                    Pln.priceCrew = pln.priceCrew;
                    Pln.procDop = pln.procDop;
                    db.SaveChanges();
                }
            }
        }
        private static void RemoveDb(Flights pln)
        {
            using (ApplicationContext db = new ApplicationContext(DataBaseHellper.Options()))
            {
                var Pln = db.FlightersBD.FirstOrDefault(u => u.id == pln.id);
                if (pln != null)
                {
                    db.FlightersBD.Remove(Pln);
                    db.SaveChanges();
                }
            }
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Задание выполнил: Черницкий Дмитрий", "Аэропорт",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void AddTool_Click(object sender, EventArgs e)
        {
            var infoForm = new FlightsForm();
            
            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                CreateDb(infoForm);
                FlightsDGV.DataSource = ReadDb();
                InfoStatCal();
            }
        }


        private void DeleteTool_Click(object sender, EventArgs e)
        {
            var id = (Flights)FlightsDGV.Rows[FlightsDGV.SelectedRows[0].Index].DataBoundItem;
            if(MessageBox.Show($"Вы действительно хотите удалить рейс {id.numflight}, прилетающий {id.eta:G}?",
                "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RemoveDb(id);
                FlightsDGV.DataSource = ReadDb();
                InfoStatCal();
            }
        }

       
        private void ChangeTool_Click(object sender, EventArgs e)
        {
            var id = (Flights)FlightsDGV.Rows[FlightsDGV.SelectedRows[0].Index].DataBoundItem;
            var infoForm = new FlightsForm(id);
            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                id.numflight = infoForm.Flights.numflight;
                id.type = infoForm.Flights.type;
                id.eta = infoForm.Flights.eta;
                id.countPas = infoForm.Flights.countPas;
                id.pricePas = infoForm.Flights.pricePas;
                id.countCrew = infoForm.Flights.countCrew;
                id.priceCrew = infoForm.Flights.priceCrew;
                id.procDop = infoForm.Flights.procDop;
                UpDateDb(id);
                FlightsDGV.DataSource = ReadDb();
                InfoStatCal();
            }
        }


        private void FlightsDGV_SelectionChanged(object sender, EventArgs e)
        {
            DeliteMenu.Enabled =
            ChangeMenu.Enabled =
            DeliteTool.Enabled = 
            ChangeTool.Enabled = 
            FlightsDGV.SelectedRows.Count > 0;
        }


        private void InfoStatCal()
        {
            using (ApplicationContext db = new ApplicationContext(DataBaseHellper.Options()))
            {
                CountFlightsTSSL.Text = $"Кол-во рейсов {db.FlightersBD.ToList().Count()}";
                CountPasTSSL.Text = $"Всего пассажиров {db.FlightersBD.Sum(x => x.countPas)}";
                CountCrewTSSL.Text = $"Всего экипажа {db.FlightersBD.Sum(x => x.countCrew)}";
                SumTSSL.Text = $"Общая сумма {db.FlightersBD.Sum(x => x.sum)}";
            }
        }


        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
