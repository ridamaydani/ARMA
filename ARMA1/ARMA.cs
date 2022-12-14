using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using k2;

//rida
namespace ARMA1
{
    public partial class ARMA : Form
    {
        int TOT = 0; double Xa;
        double PI = 3.14159274101257; double EPS = Math.Pow(10, -8);
        double dx = 1.0;
        double M = 0; double MM = 0; double delemax = 0; double dblin = 0;
        double N = 0;
        double NV = 0;
        double T1, T2, T3, T4, T5, T6, T7, T8;
        double F11, F12, F23, F34, F45, F56, F67, F78, F88;
        List<double> XP = new List<double>();
        List<double> YP = new List<double>();
        double[] X = new double[400];
        string pathNote = null;
        string path= Path.Combine(Environment.CurrentDirectory, @"Data\DIAGELEM_OMNI.txt");
        string pathAMPP = Path.Combine(Environment.CurrentDirectory, @"Data\AMPP.txt");
        string pathPHA = Path.Combine(Environment.CurrentDirectory, @"Data\PHA.txt");
        string pathAMP = Path.Combine(Environment.CurrentDirectory, @"Data\AMP.txt");
        string pathXXP = Path.Combine(Environment.CurrentDirectory, @"Data\XXP.txt");
        string pathX = Path.Combine(Environment.CurrentDirectory, @"Data\X.txt");
        string pathDSYNTH = Path.Combine(Environment.CurrentDirectory, @"Data\DSYNTH.txt");
        List<double> TETA = new List<double>();
        List<double> DELEM = new List<double>();

        double NOMB_POINT; double NOMB_ELEM; int ISYNT; int IFONC;
        int MAXFUN;
        double ang1; double ang2; int INIT;

        double tt; double t; double c; double XL;
        double[] exam = new double[181];
        double[] exph = new double[181];
        double aa = 0, bb = 0; double gmax; double gmin;
        double sign;
        double[] W = new double[300];
        double[] IW = new double[300];
        double GMAX0; double GMIN0;
        double TETTA, DETET, TETTAREF, DETETREF;
        double[] DD = new double[182];
        double G1 = 0; double TP5; double ab; double abb;
        double NA1, NN, NB, NXX, NA, NY, NB1; double NIREF, IREF;
        double SEPS; Boolean DIV4, SW;
        double NTAL, NITER, M1, XMAX;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label26.Visible = false;
                FMAXFUN.Visible = false;
                cmbxLobe.Visible = false;
                FISYNT.Visible = false;
                cmbxSynthese.Visible = false;
                FIFONC.Visible = false;
                FINIT.Visible = false;
                FNOMB_ELEM.Visible = false;
                FNOMB_POINT.Visible = false;
                cmbxAntenne.Visible = false;
                this.INITIALISATION_Click(sender, e);
                this.XINITIAL_Click(sender, e);
                this.GABARIT_Click(sender, e);
                this.FONCTION_A_SYNTHETISER_Click(sender, e);
                this.MADSEN_Click(sender, e);
                this.DIAG_Click(sender, e);
                this.NORMLOB_Click(sender, e);
                this.NORMX_Click(sender, e);
                label26.Visible = true;
                TETA = new List<double>();
                DELEM = new List<double>();
                XP = new List<double>();
                YP = new List<double>();
                TOT = 0;
                EPS = Math.Pow(10, -8);
                dx = 1.0;
                M = 0; MM = 0; delemax = 0; dblin = 0;
                N = 0;
                NV = 0;
                X = new double[400];
                exam = new double[181];
                exph = new double[181];
                aa = 0; bb = 0; W = new double[300];
                IW = new double[300];
                DD = new double[182]; G1 = 0;
                Process.Start(Path.Combine(Environment.CurrentDirectory, @"Data"));

                Class1 class1 = null;
                class1 = new Class1();
                MathWorks.MATLAB.NET.Arrays.MWCharArray mWChar = Path.Combine(Environment.CurrentDirectory, @"Data\AMP.txt");
                string str = Path.Combine(Environment.CurrentDirectory, @"Data\AMP.txt");


                class1.k2(str);
            }
            catch(Exception ex)
            {
                var test= ex.ToString();
                MessageBox.Show("error, Fill all required Fields please"+Environment.NewLine+Environment.NewLine+ test,"Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FMAXFUN.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmbxSynthese.Visible = true;
            FISYNT.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cmbxLobe.Visible = true;
            FIFONC.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FINIT.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FNOMB_POINT.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FNOMB_ELEM.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            cmbxAntenne.Visible = true;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void INITIALISATION_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.Maximum = 90;
            chart1.ChartAreas[0].AxisX.Minimum = -90;
            
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            //chart2.ChartAreas[0].AxisX.Maximum = 1000;
            chart2.ChartAreas[0].AxisX.Minimum = 0;

            //chart2.ChartAreas[0].AxisY.Maximum = 1000;
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].Cells[0].Value = "0.25";
            dataGridView1.Rows[0].Cells[1].Value = "0";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[1].Cells[0].Value = "0.75";
            dataGridView1.Rows[1].Cells[1].Value = "0";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[2].Cells[0].Value = "1.25";
            dataGridView1.Rows[2].Cells[1].Value = "0";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[3].Cells[0].Value = "1.75";
            dataGridView1.Rows[3].Cells[1].Value = "0";
            //0.0,0.5,1.0,0.0,0.5,1.0,0.0,0.5,1.0,0.0,0.5,1.0,0.0,0.5,1.0
            INITIALISATION.Visible = false;
            XINITIAL.Visible = false;
            GABARIT.Visible = false;
            FONCTION_A_SYNTHETISER.Visible = false;
            MADSEN.Visible = false;
            DIAG.Visible = false;
            NORMLOB.Visible = false;
            NORMX.Visible = false;
            this.chart();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.chart();
        }

        private void BtnTypedantenne_Click(object sender, EventArgs e)
        {
            this.groupBoxSaveNotes.Visible = true;
            pathNote = Path.Combine(Environment.CurrentDirectory, @"Note\NoteTypeAntenne.txt");
        }

        private void btnnombrediteration_Click(object sender, EventArgs e)
        {
            this.groupBoxSaveNotes.Visible = true;
            pathNote = Path.Combine(Environment.CurrentDirectory, @"Note\NoteNombreIteration.txt");
        }

        private void btntypedesynthese_Click(object sender, EventArgs e)
        {
            this.groupBoxSaveNotes.Visible = true;
            pathNote = Path.Combine(Environment.CurrentDirectory, @"Note\NoteTypeSynthese.txt");
        }

        private void btndefinitiondelobe_Click(object sender, EventArgs e)
        {
            this.groupBoxSaveNotes.Visible = true;
            pathNote = Path.Combine(Environment.CurrentDirectory, @"Note\NoteLobe.txt");
        }

        private void btnvaleurinitial_Click(object sender, EventArgs e)
        {
            this.groupBoxSaveNotes.Visible = true;
            pathNote = Path.Combine(Environment.CurrentDirectory, @"Note\NoteValeurInitial.txt");
        }

        private void btnnombredespoints_Click(object sender, EventArgs e)
        {
            this.groupBoxSaveNotes.Visible = true;
            pathNote = Path.Combine(Environment.CurrentDirectory, @"Note\NoteNbPoint.txt");
        }

        private void btnnombredeselements_Click(object sender, EventArgs e)
        {
            this.groupBoxSaveNotes.Visible = true;
            pathNote = Path.Combine(Environment.CurrentDirectory, @"Note\NoteNbElement.txt");
        }

        private void btnpositiondeselements_Click(object sender, EventArgs e)
        {
            this.groupBoxSaveNotes.Visible = true;
            pathNote = Path.Combine(Environment.CurrentDirectory, @"Note\NotePositionElement.txt");
        }

        private void btngabarit_Click(object sender, EventArgs e)
        {
            this.groupBoxSaveNotes.Visible = true;
            pathNote = Path.Combine(Environment.CurrentDirectory, @"Note\NoteGabarit.txt");
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            string lines = File.ReadAllText(pathNote);
            File.WriteAllText(pathNote, lines+ Environment.NewLine + txtnote.Text.ToString());
            txtnote.Text = "";
            this.groupBoxSaveNotes.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.groupBoxSaveNotes.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.chart2.Visible = true;
            double c = 3* Math.Pow(10, 8);
            double f =Convert.ToDouble(txtFreq.Text.ToString())*1000000;
            double lambda = c / f;
            double space =Convert.ToDouble(cmbLambda.Text);
            int nbElementX = Convert.ToInt16(numericUpDown1.Value.ToString());
            int nbElementY = Convert.ToInt16(numericUpDown2.Value.ToString());
            //double nbElement= Convert.ToDouble(FNOMB_ELEM.Text.ToString());
            this.chartMarpem(nbElementX, nbElementY, space, lambda);
        }
        private void chartMarpem(int nbElementX, int nbElementY, double space,double lambda)
        {
           this.chart2.Series.Clear();
            chart2.ChartAreas[0].AxisX.Maximum = lambda * space* nbElementX;
            chart2.ChartAreas[0].AxisY.Maximum = lambda * space*nbElementY;
            Series series = new Series("MARPEM-ANTENNA");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            // series.Name = "T1,T4  for (int i = 0; i < nbElement; i++),T5,T8";
            this.chart2.Series.Add(series);
            this.chart2.Series["MARPEM-ANTENNA"].MarkerSize = 15;
            for (int i = 0; i < nbElementY; i++)
            {
                for (int j = 0; j < nbElementX; j++)
                {
                    int index=series.Points.AddXY(lambda * space*j, lambda * space*i);

                    this.chart2.Series["MARPEM-ANTENNA"].Points[index].Label = "M" + i + j;
                    
                    // series.Points["Series"].Label = "M" + i + j;
                }
            }
            
           
            //    for (int i = 0; i < nbElement; i++)
            //{
            //    series.Points.AddXY(lambda*Math.Pow(space,i),lambda* Math.Pow(space, i));
            //}
            //this.chart2.Series.Add(series);


        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        double TOL;

        private void cmbxSynthese_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxSynthese.SelectedItem.ToString() == "Linéaire")
                FISYNT.Value = 15;
            if (cmbxSynthese.SelectedItem.ToString() == "En puissance")
                FISYNT.Value = 3;
            //En puissance
            // Linéaire
        }

        private void cmbxLobe_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbxLobe.SelectedItem.ToString() == "Sectoriel")
                FIFONC.Value = 1;
            if (cmbxLobe.SelectedItem.ToString() == "Directif")
                FIFONC.Value = 2;
            if(cmbxLobe.SelectedItem.ToString() == "2 directifs")
                FIFONC.Value = 3;
            //if (cmbxLobe.SelectedItem.ToString() == "Défini à partir d'une fonction")
            //FIFONC.Value = 4;
            if (cmbxLobe.SelectedItem.ToString() == "Avec des zéros")
                FIFONC.Value = 5;
           
            //Sectoriel
            //Directif
            //2 directifs
            //Défini à partir d'une fonction
            //Avec des zéros
        }

        private void FNOMB_ELEM_ValueChanged(object sender, EventArgs e)
        {
            string[] X = new string[this.dataGridView1.Rows.Count];
            string[] Y = new string[this.dataGridView1.Rows.Count];
            //DataGridView data = new DataGridView();
            //data = this.dataGridView1;
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                X[i] = this.dataGridView1.Rows[i].Cells[0].Value.ToString();
                Y[i] = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
            }
            
            var test = Convert.ToInt32(FNOMB_ELEM.Value) / 2;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Rows.Add(test);
            
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (i >= X.Length)
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = 0;
                    this.dataGridView1.Rows[i].Cells[1].Value = 0;
                }
                else
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = X[i];
                    this.dataGridView1.Rows[i].Cells[1].Value = Y[i];
                }
            }
           

        }

        double[,] A = new double[182, 182], A1 = new double[182, 182];
        double[] B = new double[182];
        double[] B1 = new double[182], XX = new double[300], Y = new double[300];
        double f00, F0, F1, S = 0, F, SUM, TX, T;
        double DPHI, DAMP = 0, DPHA = 0, DAMP0 = 0, DPHA0 = 0, g, sc, sd, S1, S2, SD, G;
        Complex CHAMP;
        double IADIM, GBAR = 1;
        private int testc=0;

        public ARMA()
        {
            InitializeComponent();
        }
        private void chart()
        {
            double T1, TI1, T2, TI2, T3, TI3 ,T4, T5, TI6, T6, T7,TI7, T8, TE8, F11, F12, F23, F34, F45, F56, F67, F78, F88;
            T1 = Convert.ToDouble(GT1.Text.ToString());
            TI1 = Convert.ToDouble(GT1.Text.ToString());
            T2 = Convert.ToDouble(GT2.Text.ToString());
            TI2 = Convert.ToDouble(GT2.Text.ToString());
            T3 = Convert.ToDouble(GT3.Text.ToString());
            TI3 = Convert.ToDouble(GT3.Text.ToString());
            T4 = Convert.ToDouble(GT4.Text.ToString());
            T5 = Convert.ToDouble(GT5.Text.ToString());
            T6 = Convert.ToDouble(GT6.Text.ToString());
            TI6 = Convert.ToDouble(GT6.Text.ToString());
            T7 = Convert.ToDouble(GT7.Text.ToString());
            TI7 = Convert.ToDouble(GT7.Text.ToString());
            T8 = Convert.ToDouble(GT8.Text.ToString());
            TE8 = Convert.ToDouble(GT8.Text.ToString());
            //double[] T = new double[8];
            var gabarit = new[] { TI1,T1, T4, T5, T8,TE8 };
            var T = new[] { "TI1", "T1",  "T4", "T5","T8", "TE8" };
            var gabarit2 = new[] { TI2, T2, T3 , TI3 };
            var TT2 = new[] { "TI2" ,"T2", "T3", "TI3" };
            var gabarit3 = new[] { TI6,T6, T7 , TI7 };
            var TT3 = new[] { "TI6", "T6", "T7", "TI7" };
            
            F11 = Convert.ToDouble(PF11.Text.ToString());
            if (F11 > 100) F11 = 100;
            F12 = Convert.ToDouble(PF12.Text.ToString());
            if (F12 > 100) F12 = 100;
            F23 = Convert.ToDouble(PF23.Text.ToString());
            if (F23 > 100) F23 = 100;
            F34 = Convert.ToDouble(PF34.Text.ToString());
            if (F34 > 100) F34 = 100;
            F45 = Convert.ToDouble(PF45.Text.ToString());
            if (F45 > 100) F45 = 100;
            F56 = Convert.ToDouble(PF56.Text.ToString());
            if (F56 > 100) F56 = 100;
            F67 = Convert.ToDouble(PF67.Text.ToString());
            if (F67 > 100) F67 = 100;
            F78 = Convert.ToDouble(PF78.Text.ToString());
            if (F78 > 100) F78 = 100;
            F88 = Convert.ToDouble(PF88.Text.ToString());
            if (F88 > 100) F88 = 100;
            var PP1 = new[] { 10, F11,  F34, F45, F78, 10 };
            var PP2 = new[] { 0, F12, F23, 0 };
            var PP3 = new[] { 0, F56, F67,0 };
            this.chart1.Series.Clear();
           
            Series series = new Series("T1,T4,T5,T8");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series.Name = "T1,T4,T5,T8";
            this.chart1.Series.Add(series);
            this.chart1.Series["T1,T4,T5,T8"].BorderWidth = 8;
            for (int i = 0; i < T.Length; i++)
            {
                int index=series.Points.AddXY(gabarit[i], PP1[i]);
             
            }
           

            Series series1 = new Series("T2,T3");
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series1.Name = "T2,T3";
            this.chart1.Series.Add(series1);
            this.chart1.Series["T2,T3"].BorderWidth = 8;
            for (int i = 0; i < TT2.Length; i++)
            {
                series1.Points.AddXY(gabarit2[i], PP2[i]);
                
            }
            
            Series series2 = new Series("T6,T7");
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series2.Name = "T6,T7";
            this.chart1.Series.Add(series2);
            this.chart1.Series["T6,T7"].BorderWidth = 8;
            for (int i = 0; i < TT3.Length; i++)
            {
                series2.Points.AddXY(gabarit3[i], PP3[i]);
                //series.Points.Add(pointsArray[i]);
            }
            
            // Create chart series and add data points into it.
            //for (int i = 0; i < seriesArray.Length; i++)
            //{

            //    Series series = this.chart1.Series.Add(seriesArray[i].ToString());
            //    series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            //    series.Points.AddXY(gabarit[i], pointsArray[i]);
            //    //series.Points.Add(pointsArray[i]);
            //}
            //// Create chart series and add data points into it.

            //Series series1 = new Series();
            //series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            //series1.Points.Add(0, 1);

            //series1.Points.Add(1, 3);

            //series1.Points.Add(2, 2);

            //series1.Points.Add(3, 2);

            //// Add the series to the chart series collection.

            //this.chart1.Series.Add(series1);
        }
        private void INITIALISATION_Click(object sender, EventArgs e)
        {
            if(cmbxAntenne.SelectedItem.ToString() == "Antenne Patch Plan E")
             path = Path.Combine(Environment.CurrentDirectory, @"Data\DIAGELEM_Plan_E.txt");
            if (cmbxAntenne.SelectedItem.ToString() == "Antenne Patch Plan H")
                path = Path.Combine(Environment.CurrentDirectory, @"Data\DIAGELEM_Plan_H.txt");
            if (cmbxAntenne.SelectedItem.ToString() == "Antenne MonoPolaire")
                path = Path.Combine(Environment.CurrentDirectory, @"Data\DIAGELEM_MONO.txt");
            if (cmbxAntenne.SelectedItem.ToString() == "Antenne Omnidirectionnelle")
                path = Path.Combine(Environment.CurrentDirectory, @"Data\DIAGELEM_OMNI.txt");


            string[] lines = File.ReadAllLines(path);
            for (int j = 0; j < lines.Length; j++)
            {
                TETA.Add(Math.Round(Convert.ToDouble(lines[j].Split(',')[0]),15));
                DELEM.Add(Math.Round(Convert.ToDouble(lines[j].Split(',')[1]),15));
            }
            //string[] XP0 = FXP.Text.ToString().Split(',');
            string[] XP0 = new string[1000];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                XP0[i] = dataGridView1.Rows[i].Cells[0].Value.ToString();
            string[] YP0 = new string[1000];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                YP0[i] = dataGridView1.Rows[i].Cells[1].Value.ToString();
            //string[] YP0 = FYP.Text.ToString().Split(',');
            INIT = Convert.ToInt32(FINIT.Text.ToString());
            IFONC = Convert.ToInt32(FIFONC.Text.ToString());
            MAXFUN = Convert.ToInt32(FMAXFUN.Text.ToString());
            NOMB_POINT = Convert.ToDouble(FNOMB_POINT.Text.ToString());
            NOMB_ELEM = Convert.ToDouble(FNOMB_ELEM.Text.ToString());
            ISYNT = Convert.ToInt32(FISYNT.Text.ToString());

            M = NOMB_POINT;
            if (N > 20)
                goto S102;
            if (ISYNT == 5 || ISYNT == 2 || ISYNT == 7)
            {
                N = 2 * NOMB_ELEM;
                NV = NOMB_ELEM;
                goto S101;
            }

            if (ISYNT == 15 || ISYNT == 16 || ISYNT == 12 || ISYNT == 17)
            {
                N = NOMB_ELEM;
                NV = N / 2;
                goto S101;
            }
            if (ISYNT == 14 || ISYNT == 13 || ISYNT == 11 || ISYNT == 19)
            {
                N = NOMB_ELEM / 2;
                NV = N;
                goto S101;
            }
            if (ISYNT == 3 || ISYNT == 2 || ISYNT == 1 || ISYNT == 9)
            {
                N = NOMB_ELEM;
                NV = N;
                goto S101;
            }

        S101:
            {

                for (int i = 0; i < NV; i++)
                {
                    XP.Add(Convert.ToDouble(XP0[i]));
                    YP.Add(Convert.ToDouble(YP0[i]));
                }
                T1 = Math.Round(Convert.ToDouble(GT1.Text.ToString()) * PI / 180,15);
                T2 = Math.Round(Convert.ToDouble(GT2.Text.ToString()) *  PI / 180,15);
                T3 = Math.Round(Convert.ToDouble(GT3.Text.ToString()) *  PI / 180, 15);
                T4 = Math.Round(Convert.ToDouble(GT4.Text.ToString()) *  PI / 180, 15);
                T5 = Math.Round(Convert.ToDouble(GT5.Text.ToString()) *  PI / 180, 15);
                T6 = Math.Round(Convert.ToDouble(GT6.Text.ToString()) *  PI / 180, 15);
                T7 = Math.Round(Convert.ToDouble(GT7.Text.ToString()) *  PI / 180, 15);
                T8 = Math.Round(Convert.ToDouble(GT8.Text.ToString()) *  PI / 180, 15);
                F11 = Convert.ToDouble(PF11.Text.ToString());
                F12 = Convert.ToDouble(PF12.Text.ToString());
                F23 = Convert.ToDouble(PF23.Text.ToString());
                F34 = Convert.ToDouble(PF34.Text.ToString());
                F45 = Convert.ToDouble(PF45.Text.ToString());
                F56 = Convert.ToDouble(PF56.Text.ToString());
                F67 = Convert.ToDouble(PF67.Text.ToString());
                F78 = Convert.ToDouble(PF78.Text.ToString());
                F88 = Convert.ToDouble(PF88.Text.ToString());

                if (Convert.ToInt32(FIFONC.Text.ToString()) == 4)
                {
                    if (ISYNT == 5)
                        FISYNT.Text = 7.ToString();
                    if (T7 == (-1) * T2 && T8 == (-1) * T1)
                    {
                        if (ISYNT == 7)
                            ISYNT = 1;
                        else
                            ISYNT = 11;

                    }
                }
                if (ISYNT == 5 || ISYNT == 15)
                {
                    if (T2 == (-1) * T3 && T1 == (-1) * T4)
                    {
                        if (ISYNT == 5)
                            ISYNT = 3;
                        else
                            ISYNT = 13;
                    }
                }
                if (ISYNT == 1 || ISYNT == 3)
                {
                    N = NOMB_ELEM;
                    NV = N;
                    goto S102;
                }
                if (ISYNT == 13 || ISYNT == 11 || ISYNT == 14)
                {
                    N = NOMB_ELEM / 2;
                    NV = N;
                }


            }
        S102:
            {

                MM = M;
                delemax = -99;
                for (int i = 0; i < MM; i++)
                {
                    dblin =Math.Round( DELEM[i] / 20,15);
                    DELEM[i] = Math.Round(Math.Pow(10, dblin),15);
                    if (DELEM[i] == 0)
                        DELEM[i] = Math.Round(3.16e-3,15);

                    if (DELEM[i] > delemax)
                        delemax = Math.Round(DELEM[i],15);
                    TETA[i] = Math.Round(TETA[i] *  PI / 180,14);

                }
                for (int i = 0; i < MM; i++)
                    DELEM[i] = Math.Round(DELEM[i] / delemax,15);
            }


        }
        private void XINITIAL_Click(object sender, EventArgs e)
        {
            ang1 = Math.Round(0.5 * (T3 + T2),15);
            tt = ang1;
            t = Math.Round(T3 - T2,15);
            c = 0.75;
            if (ISYNT == 17 || ISYNT == 11 || ISYNT == 7 || ISYNT == 1)
            {
                t = Math.Round(0.5 * (T5 - T4),15);
                tt = Math.Round(0.5 * (T5 + T4),15);

            }
            if (ISYNT == 14 || ISYNT == 16)
            {
                ang1 = Math.Round(0.5 * (T3 + T2),15);
                ang2 = Math.Round(0.5 * (T7 + T6),15);
                tt = ang2;
                t = Math.Round(ang2 - ang1,15);
                XL = Math.Round(XP[Convert.ToInt32(NV) - 1] - XP[0],15);
                for (int i = 0; i < Convert.ToInt32(NV); i++)
                {
                    // Xa = XP[i];
                    X[i] = Math.Round(COSPIED(XP[i], XL, 2 * t /  PI, c),15);
                    X[i + Convert.ToInt32(NV)] = 0;
                }
            }
            if (ISYNT == 13 || ISYNT == 15 || ISYNT == 12 || ISYNT == 17 || ISYNT == 11)
            {

                XL = Math.Round(XP[Convert.ToInt32(NV)-1] - XP[0],15);
                for (int i = 0; i < Convert.ToInt32(NV); i++)
                {
                     Xa = Math.Round(XP[i],15);
                    X[i] = Math.Round(COSPIED(Xa, XL, 2 * t /  PI, c),15);
                    X[i + Convert.ToInt32(NV)] = Math.Round(-1 * Xa * (Math.Sin(tt)),15);
                }
            }
            if (ISYNT == 19)
            {

                for (int i = 0; i < Convert.ToInt32(NV); i++)
                {
                    // Xa = XP[i];
                    exam[i] = 0.8 / NV;
                    X[i] = 0;
                }
            }
            if (ISYNT == 9)
            {

                for (int i = 0; i < Convert.ToInt32(NV); i++)
                {
                    // Xa = XP[i];
                    exam[i] = 0.55 / NV;
                    X[i] = 0;
                }
            }
            if (ISYNT == 1 || ISYNT == 2 || ISYNT == 5 || ISYNT == 7)
            {
                XL = XP[Convert.ToInt32((NOMB_ELEM - 1) / 2)] - XP[0];
                for (int i = 0; i < Convert.ToInt32(NOMB_ELEM); i++)
                {
                    X[i] = COSPIED(XL - XP[i], XL, 2 * t /  PI, c);
                    X[i + Convert.ToInt32(NV) - 1] = -1 * XP[i] * Math.Sin(tt);
                }
            }
            if (ISYNT == 3 || ISYNT == 1)
            {
                XL = XP[Convert.ToInt32(NOMB_ELEM / 2)] - XP[0];
                for (int i = 0; i < Convert.ToInt32(NOMB_ELEM); i++)
                {
                    X[i] = COSPIED(XL - XP[i], XL, 2 * t /  PI, c);
                    X[i + Convert.ToInt32(NV) - 1] = -1 * XP[i] * Math.Sin(tt);
                }
            }
            if (ISYNT == 13 || ISYNT == 14 || ISYNT == 3)
            {
                XL = XP[Convert.ToInt32((NV - 1) / 2)] - XP[0];
                for (int i = 0; i < Convert.ToInt32(NV); i++)
                {

                    X[i + Convert.ToInt32(NV) - 1] = 0;
                }
            }
            if (INIT == 0)
            {

                for (int i = 0; i < Convert.ToInt32(NV); i++)
                {

                    Console.WriteLine(X[i + Convert.ToInt32(NV) - 1]);
                }
            }
            Console.WriteLine(X);

        }
        private void GABARIT_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NV; i++)
                aa = Math.Round(aa,15) + Math.Round(X[i]*X[i],15);
            if (aa == 0)
                aa = Math.Round(NV / 2,15);
            gmax = Math.Round(1.0,15);
            gmin = Math.Round(0.100000001490116,15);
            GMAX0 = Math.Round(gmax,15);
            GMIN0 = Math.Round(gmin,15);
            ang1 = Math.Round(T4 - T1,15);
            ang2 = Math.Round(T8 - T5,15);
            if (ang1 > ang2)
                t = Math.Round(ang1,15);
            else
                t = Math.Round(ang2,15);
            if (ISYNT == 17 || ISYNT == 1 || ISYNT == 11 || ISYNT == 7)
                t = Math.Round(T6 - T3,15);
            if (t == 0)
                t = Math.Round(0.3,15);
            gmax = Math.Round(GMAX0 / (GMIN0 * (1 - Math.Sin(t)) + GMAX0 * Math.Sin(t)),14);
            gmin = Math.Round(GMIN0 / (GMIN0 * (1 - Math.Sin(t)) + GMAX0 * Math.Sin(t)),15);
            gmax = Math.Round(Math.Sqrt(gmax / aa),15);
            gmin = Math.Round(Math.Sqrt(gmin / aa),15);
            gmax = Math.Round(0.949999988079071 * gmax,14);
            gmin = Math.Round(0.949999988079071 * gmin,15);
        }

        private void FONCTION_A_SYNTHETISER_Click(object sender, EventArgs e)
        {
            gmax = 1.0;
            gmin = 0.009999999776482582;
            //9.99999977648258 * 1 / (Math.Pow(Math.E, 3));
            //fortran gmin = 0.01;
            for (int i = 0; i < NOMB_POINT; i++)
            {
                TETTA = TETA[i];
                DD[i + 1] = FDES(TETTA, i);
            }
            for (int j = 0; j < NOMB_POINT; j++)
            {
                if (DD[j + 1] > G1)
                    G1 = DD[j + 1];
            }
            if (G1 == 0)
                G1 = 1 * Math.Pow(10, -3);

            List<string> writeLines = new List<string>();
            for (int k = 0; k < NOMB_POINT; k++)
                writeLines.Add(TETA[k].ToString() + ',' + (DD[k + 1] / G1).ToString());

            File.WriteAllLines(pathDSYNTH, writeLines);
        }

        private void MADSEN_Click(object sender, EventArgs e)
        {
            if (N < 1 || M < 1 || dx <= 0)
                return;
            NB = 1;
            NXX = NB + M;
            NA = NXX + N;
            NY = NA + M * N;
            NB1 = NY + N;
            NA1 = NB1 + M;
            NIREF = NB1 + (N + 2) * (N + 6);
            this.OPTIMLIN();
        }
        double OPTIMLIN()
        {
            SEPS = Math.Pow(16, -13);
            DIV4 = false;
            SW = false;
            NTAL = 0;
            M1 = M + 1;
            XMAX = 0;
            for (int i = 0; i < N; i++)
                XMAX = Math.Max(XMAX, Math.Abs(X[i]));
            f00 = 0;
            F0 = 0;
            this.FDF(N, M, X, A, B, 0);
            F0 = 0;
            for (int i = 0; i < M; i++)
            {
                S =Math.Round(Math.Abs(B[i+1]),15);
                if (S > F0)
                    F0 = S;
            }
        S30: TOL = 0;
            //this.CALCMAT(M,N,A,B,dx,XX,TOL,Y,IREF);
            this.CALCMAT();
            F = 0;
            for (int i = 1; i <= M; i++)
            {
                SUM = B[i];
                for (int j = 1; j <= N; j++)
                    SUM = SUM + A[i-1, j - 1] * XX[j];
                F = Math.Max(F, Math.Abs(SUM));
            }
            if (SW) goto S135;

            dx = 0;
            for (int i = 1; i <= N; i++)
            {
                S = Math.Abs(XX[i]);
                if (S > dx)
                    dx = S;
                Y[i-1] = X[i-1] + XX[i];
            }
            this.FDF(N, M, Y, A1, B1, 1);
            F1 = 0;
            for (int i = 1; i <= M; i++)
            {
                S = Math.Abs(B1[i]);
                if (S > F1)
                    F1 = S;
            }
            if ((F0 - F1) > (F0 - F) / 4)
                goto S60;
            dx = dx / 1.2;
            DIV4 = true;
            TX = dx;
            goto S100;
        S60:
            if (DIV4) goto S90;
            S = 0;
            for (int i = 1; i <= M; i++)
            {
                T = B[i];
                for (int j = 1; j <= N; j++)
                    T = T + A[i-1, j - 1] * XX[j];
                T = Math.Abs(B1[i] - T);
                if (T > S) S = T;
            }
            if (S <= (F0 - F1) / 4) dx = dx * 2;
            S90: DIV4 = false;
        S100: f00 = F0;
            if (F0 <= F1) goto S130;
            F0 = F1;
            XMAX = 0;
            for (int i = 1; i <= N; i++)
            {
                X[i-1] = Y[i-1];
                XMAX = Math.Max(XMAX, Math.Abs(X[i-1]));
                for (int j = 1; j <= M; j++)
                    A[j-1, i - 1] = A1[j-1, i - 1];
            }
            for (int i = 1; i <= M; i++)
            {
                B[i] = B1[i];
            }
        S130: NTAL = NTAL + 1;
            if (NTAL > MAXFUN) goto S140;
            NITER = NTAL;
            if ((f00 - F <= SEPS * f00) || (dx <= Math.Pow(10, -3))) goto S140;
            if (f00 - F >= EPS) goto S30;
            SW = true;
            S = dx;
            dx = XMAX;
            goto S30;
        S135: SW = false;
            dx = S;
            if (F0 - F >= EPS * Math.Pow(10, -5)) goto S30;
            S140: XX[1] = NTAL;
            EPS = 0;
            return 0;
        }
        double FDF(double n, double m, double[] x, double[,] r, double[] b, int status)
        {

            DPHI = 0;
            if (ISYNT == 1) goto S11;
            if (ISYNT == 2) goto S22;
            if (ISYNT == 3) goto S33;
            if (ISYNT == 5) goto S55;
            if (ISYNT == 7) goto S77;
            if (ISYNT == 11) goto S111;
            if (ISYNT == 12) goto S122;
            if (ISYNT == 13) goto S133;
            if (ISYNT == 19) goto S199;
            if (ISYNT == 14) goto S144;
            if (ISYNT == 15) goto S155;
            if (ISYNT == 16) goto S166;
            if (ISYNT == 17) goto S177;
            if (ISYNT == 9) goto S399;
            S399:
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine(i); Console.WriteLine(exam[i]);
                Console.WriteLine(exph[i]);
                exph[i] = x[i];
            }
            for (int jj = 0; jj < M; jj++)
            {
                MM = 1;
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                this.DELTA();
                //this.DELTA(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,N,MM,DETET);
                CHAMP = new Complex(DAMP * Math.Cos(DPHA), DAMP * Math.Sin(DPHA));
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S309;
                }
                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S309;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;
                    goto S309;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S309;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S309;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S309;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S309;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S309;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S309:
                sc = DAMP;
                sd = DETET;
                S1 = 0;
                S2 = 0;
                for (int i = 0; i < N; i++)
                {
                    aa = 2 *  PI * x[i];
                    bb = 2 *  PI * XP[i] * Math.Sin(TETTA) + aa;
                    S1 = S1 + exam[i] * Math.Cos(bb);
                    S2 = S2 + exam[i] * Math.Sin(bb);
                }
                for (int ii = 0; ii < N; ii++)
                {
                    aa = 2 *  PI * x[ii];
                    bb = 2 *  PI * XP[ii] * Math.Sin(TETTA) + aa;
                    if (status == 0)
                    {
                        A[jj, ii] = 4 *  PI * S2 * exam[ii] * Math.Cos(bb) - 4 *  PI * S1 * exam[ii] * Math.Sin(bb);
                        A[jj, ii] = A[jj, ii] * G * SD / (2 * sc);
                    }
                    else
                    {
                        A1[jj, ii] = 4 *  PI * S2 * exam[ii] * Math.Cos(bb) - 4 *  PI * S1 * exam[ii] * Math.Sin(bb);
                        A1[jj, ii] = A1[jj, ii] * G * SD / (2 * sc);
                    }

                }

            }


            goto S999;
        S199:
            for (int i = 0; i < N; i++)
                exph[i] = x[i];
            S99:
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine(i); Console.WriteLine(exam[i]);
                Console.WriteLine(exph[i]);
                exph[i] = x[i];
            }
            MM = 1;
            for (int jj = 0; jj < M; jj++)
            {

                TETTA = TETA[jj];
                DETET = DELEM[jj];
                g = 1e-30;
                this.DELTAR();
                //this.DELTAR(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,N,MM,DETET);
                g = F23 * 1e-6;
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S79;
                }
                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S79;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S79;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S79;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S79;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S79;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S79;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S79;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S79:
                if (DAMP < 0)
                    g = g;
                sign = g * DETET;


                for (int ii = 0; ii < N; ii++)
                {
                    bb = 2 *  PI * x[ii];
                    if (status == 0)
                        A[jj, ii] = -2 *  PI * exam[ii] * Math.Sin(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;
                    else
                        A1[jj, ii] = -2 *  PI * exam[ii] * Math.Sin(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;

                }


            }

            goto S999;
        S22:
            for (int i = 0; i < N / 2; i++)
            {
                exam[i] = x[i];
                exph[i] = x[(i + Convert.ToInt32(N) / 2)];
            }
            for (int jj = 0; jj < M; jj++)
            {
                NN = N / 2;
                MM = 1;
                DPHI = 0;
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                this.DELTA();
                //this.DELTA(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,NN,MM,DETET);
                CHAMP = new Complex(DAMP * Math.Cos(DPHA), DAMP * Math.Sin(DPHA));
                g = F23 * 1e-5;
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S21;
                }
                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S21;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23 / DD[jj];
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S21;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S21;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S21;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S21;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67 / DD[jj];
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S21;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S21;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S21:
                sc = DAMP;
                sd = DETET;
                S1 = 0;
                S2 = 0;
                for (int i = 0; i < N / 2; i++)
                {
                    aa = 2 *  PI * x[i + Convert.ToInt32(N) / 2];
                    bb = 2 *  PI * XP[i] * Math.Sin(TETTA) + aa;
                    S1 = S1 + x[i] * Math.Cos(bb);
                    S2 = S2 + x[i] * Math.Sin(bb);
                }
                for (int ii = 0; ii < N / 2; ii++)
                {
                    aa = 2 *  PI * x[ii + Convert.ToInt32(N) / 2];
                    bb = 2 *  PI * XP[ii] * Math.Sin(TETTA) + aa;
                    if (status == 0)
                    {
                        A[jj, ii] = 2 * S1 * Math.Cos(bb) + 2 * S2 * Math.Sin(bb);
                        A[jj, ii] = A[jj, ii] * G * SD / (2 * sc);
                        A[jj, ii + Convert.ToInt32(N) / 2] = 4 *  PI * S2 * x[ii] * Math.Cos(bb) - 4 *  PI * S1 * x[ii] * Math.Sin(bb);
                        A[jj, ii + Convert.ToInt32(N) / 2] = A[jj, ii + Convert.ToInt32(N) / 2] * G * SD / (2 * sc);
                    }
                    else
                    {
                        A1[jj, ii] = 2 * S1 * Math.Cos(bb) + 2 * S2 * Math.Sin(bb);
                        A1[jj, ii] = A1[jj, ii] * G * SD / (2 * sc);
                        A1[jj, ii + Convert.ToInt32(N) / 2] = 4 *  PI * S2 * x[ii] * Math.Cos(bb) - 4 *  PI * S1 * x[ii] * Math.Sin(bb);
                        A1[jj, ii + Convert.ToInt32(N) / 2] = A1[jj, ii + Convert.ToInt32(N) / 2] * G * SD / (2 * sc);
                    }
                }


            }
            goto S999;
        S122:
            NN = N / 2;
            for (int i = 0; i < NN; i++)
            {
                exam[i] = x[i];
                exph[i] = x[i + Convert.ToInt32(NN)];
            }
            for (int jj = 0; jj < M; jj++)
            {
                MM = 1;
                DPHI = 0;
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                this.DELTAR();
                //this.DELTAR(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,NN,MM,DETET);
                g = F23 * 1e-5;
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S25;
                }
                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S25;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23 / DD[jj];
                    if (status == 0)
                        B[jj + 1] = B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S25;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S25;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S25;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S25;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67 / DD[jj];
                    if (status == 0)
                        B[jj + 1] = B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S25;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S25;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S25:
                sign = g * DETET;

                for (int ii = 0; ii < NN; ii++)
                {
                    if (status == 0)
                    {
                        A[jj, ii] = Math.Cos(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;

                        A[jj, ii + Convert.ToInt32(NN)] = -2 *  PI * x[ii] * Math.Sin(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;
                    }
                    else
                    {
                        A1[jj, ii] = Math.Cos(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;

                        A1[jj, ii + Convert.ToInt32(NN)] = -2 *  PI * x[ii] * Math.Sin(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;
                    }

                }


            }
            goto S999;

        S166:
            NN = N / 2;
            for (int i = 0; i < NN; i++)
            {
                exam[i] = x[i];
                exph[i] = x[i + Convert.ToInt32(NN)];
            }
            for (int jj = 0; jj < M; jj++)
            {
                MM = 1;
                DPHI = 0;
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                this.DELTARD();
                //this.DELTARD(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,NN,MM,DETET);
                g = F23 * 0.001;
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S65;
                }
                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S65;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S65;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S65;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S65;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S65;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;
                    if (status == 0)

                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else

                        B1[jj + 1] = (DAMP - DD[jj]) * g;
                    goto S65;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S65;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S65:
                sign = g * DETET;

                for (int ii = 0; ii < NN; ii++)
                {
                    bb = 2 *  PI * x[ii + Convert.ToInt32(NN)];
                    if (status == 0)
                    {
                        A[jj, ii] = Math.Sin(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;
                        A[jj, ii] = 0.01;
                        A[jj, ii + Convert.ToInt32(NN)] = 2 *  PI * x[ii] * Math.Cos(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;
                    }
                    else
                    {
                        A1[jj, ii] = Math.Sin(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;
                        A1[jj, ii] = 0.01;
                        A1[jj, ii + Convert.ToInt32(NN)] = 2 *  PI * x[ii] * Math.Cos(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;
                    }
                }


            }
            goto S999;
        S144:
            NN = N;
            for (int i = 0; i < N; i++)
            {
                exam[i] = x[i];
                if (INIT == 1)
                {
                    exph[i] = 0;
                    x[i + Convert.ToInt32(N)] = 0;
                }
                else
                    exph[i] = x[i + Convert.ToInt32(N)];

            }
            for (int jj = 0; jj < M; jj++)
            {
                MM = 1;
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                this.DELTARD();
                //this.DELTARD(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,N,MM,DETET);
                g = F23 * 0.001;
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S42;
                }

                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S42;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23;
                    if (status == 0)

                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else

                        B1[jj + 1] = (DAMP - DD[jj]) * g;
                    goto S42;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S42;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S42;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S42;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S42;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S42;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S42:
                if (DAMP < 0)
                    g = g;
                sign = g * DETET;

                for (int ii = 0; ii < N; ii++)
                {
                    bb = 2 *  PI * x[ii + Convert.ToInt32(N)];
                    if (status == 0)
                        A[jj, ii] = Math.Sin(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;
                    else
                        A1[jj, ii] = Math.Sin(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;

                }
            }
            goto S999;
        S33:

            for (int i = 0; i < N; i++)
                exam[i] = x[i];

            for (int jj = 0; jj < M; jj++)
            {
                MM = 1;
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                g = 1e-30;
                this.DELTA();
                //this.DELTA(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,N,MM,DETET);
                CHAMP = new Complex(DAMP * Math.Cos(DPHA), DAMP * Math.Sin(DPHA));
                g = F23 * 1e-5;
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;
                S1 = Convert.ToDouble(CHAMP) / DETET;
                S2 = Convert.ToDouble(CHAMP) / DETET;
                sc = DAMP;
                SD = DETET;
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S30;
                }

                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S30;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S30;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S30;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S30;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S30;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S30;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S30;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S30:
                for (int ii = 0; ii < N; ii++)
                {
                    bb = 2 *  PI * x[ii] * Math.Sin(TETTA) + exph[ii];
                    if (status == 0)
                    {
                        A[jj, ii] = 1 / sc * (S1 * Math.Cos(bb) + S2 * Math.Sin(bb));
                        A[jj, ii] = A[jj, ii] * SD * G;
                    }
                    else
                    {
                        A1[jj, ii] = 1 / sc * (S1 * Math.Cos(bb) + S2 * Math.Sin(bb));
                        A1[jj, ii] = A1[jj, ii] * SD * G;
                    }


                }
            }
            goto S999;
        S11:

            for (int i = 0; i < N; i++)
                exam[i] = x[i];

            for (int jj = 0; jj < M; jj++)
            {
                MM = 1;
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                g = 1e-30;
                this.DELTA();
                //this.DELTA(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,N,MM,DETET);
                CHAMP = new Complex(DAMP * Math.Cos(DPHA), DAMP * Math.Sin(DPHA));
                g = F45 * 0.095;
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;

                if (TETA[jj] < T3)
                {
                    g = F23 * 0.095;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;

                }
                S1 = Convert.ToDouble(CHAMP) / DETET;
                S2 = Convert.ToDouble(CHAMP) / DETET;
                sc = DAMP;
                SD = DETET;
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S14;
                }

                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S14;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S14;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S14;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S14;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S14;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S14;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S14;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S14:
                for (int ii = 0; ii < N; ii++)
                {
                    bb = 2 *  PI * XP[ii] * Math.Sin(TETTA) + exph[ii];
                    if (status == 0)
                    {
                        A[jj, ii] = 1 / sc * (S1 * Math.Cos(bb) + S2 * Math.Sin(bb));
                        A[jj, ii] = A[jj, ii] * SD * G;
                    }
                    else
                    {
                        A1[jj, ii] = 1 / sc * (S1 * Math.Cos(bb) + S2 * Math.Sin(bb));
                        A1[jj, ii] = A1[jj, ii] * SD * G;
                    }
                }
            }
            goto S999;
        S55:

            for (int i = 0; i < N / 2; i++)
            {
                exam[i] = x[i];
                exph[i] = x[i + Convert.ToInt32(N) / 2];
            }
            for (int jj = 0; jj < M; jj++)
            {
                NN = N / 2;
                MM = 1;
                DPHI = 0;
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                g = 1e-30;
                this.DELTA();
                //this.DELTA(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,NN,MM,DETET);
                CHAMP = new Complex(DAMP * Math.Cos(DPHA), DAMP * Math.Sin(DPHA));
                g = F23 * 1e-5;
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;

                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S31;
                }

                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S31;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23;
                    if (IFONC == 3)
                        g = g / DD[jj];
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S31;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S31;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S31;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S31;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;
                    if (IFONC == 3)
                        g = g / DD[jj];
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;
                    goto S31;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S31;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S31:
                sc = DAMP;
                SD = DETET;
                S1 = 0;
                S2 = 0;
                for (int i = 0; i < N / 2; i++)
                {
                    aa = 2 *  PI * x[i + Convert.ToInt32(N) / 2];
                    bb = 2 *  PI * XP[i] * Math.Sin(TETTA) + aa;
                    S1 = S1 + x[i] * Math.Cos(bb);
                    S2 = S2 + x[i] * Math.Sin(bb);
                }
                for (int ii = 0; ii < N / 2; ii++)
                {
                    aa = 2 *  PI * x[ii + Convert.ToInt32(N) / 2];
                    bb = 2 *  PI * XP[ii] * Math.Sin(TETTA) + aa;
                    if (status == 0)
                    {
                        A[jj, ii] = 2 * S1 * Math.Cos(bb) + 2 * S2 * Math.Sin(bb);
                        A[jj, ii] = A[jj, ii] * SD * G / (2 * sc);
                        A[jj, ii + Convert.ToInt32(N) / 2] = 4 *  PI * S2 * x[ii] * Math.Cos(bb) - 4 *  PI * S1 * x[ii] * Math.Sin(bb);
                        A[jj, ii + Convert.ToInt32(N) / 2] = A[jj, ii + Convert.ToInt32(N) / 2] * SD * G / (2 * sc);
                    }
                    else
                    {
                        A1[jj, ii] = 2 * S1 * Math.Cos(bb) + 2 * S2 * Math.Sin(bb);
                        A1[jj, ii] = A1[jj, ii] * SD * G / (2 * sc);
                        A1[jj, ii + Convert.ToInt32(N) / 2] = 4 *  PI * S2 * x[ii] * Math.Cos(bb) - 4 *  PI * S1 * x[ii] * Math.Sin(bb);
                        A1[jj, ii + Convert.ToInt32(N) / 2] = A1[jj, ii + Convert.ToInt32(N) / 2] * SD * G / (2 * sc);
                    }

                }
            }
            goto S999;
        S77:

            for (int i = 0; i < N / 2; i++)
            {
                exam[i] = x[i];
                exph[i] = x[i + Convert.ToInt32(N) / 2];
            }
            for (int jj = 0; jj < M; jj++)
            {
                NN = N / 2;
                MM = 1;
                DPHI = 0;
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                g = 1e-30;
                this.DELTA();
                //this.DELTA(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,NN,MM,DETET);
                //CHAMP = new Complex(DAMP * Math.Cos(DPHA), DAMP * Math.Sin(DPHA));
                g = F45 * 0.095;
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;

                if (TETA[jj] < T3)
                {
                    g = F23 * 0.095;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;

                }

                if (TETA[jj] > T6)
                {
                    g = F67 * 0.095;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;

                }
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S37;
                }

                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S37;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S37;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S37;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;
                    goto S37;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S37;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;

                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S37;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S37;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S37:
                sc = DAMP;
                SD = DETET;
                S1 = 0;
                S2 = 0;
                for (int i = 0; i < N / 2; i++)
                {
                    aa = 2 *  PI * x[i + Convert.ToInt32(N) / 2];
                    bb = 2 *  PI * XP[i] * Math.Sin(TETTA) + aa;
                    S1 = S1 + x[i] * Math.Cos(bb);
                    S2 = S2 + x[i] * Math.Sin(bb);
                }
                for (int ii = 0; ii < N / 2; ii++)
                {
                    aa = 2 *  PI * x[ii + Convert.ToInt32(N) / 2];
                    bb = 2 *  PI * XP[ii] * Math.Sin(TETTA) + aa;
                    if (status == 0)
                    {
                        A[jj, ii] = 2 * S1 * Math.Cos(bb) + 2 * S2 * Math.Sin(bb);
                        A[jj, ii] = A[jj, ii] * SD * G / (2 * sc);
                        A[jj, ii + Convert.ToInt32(N) / 2] = 4 *  PI * S2 * x[ii] * Math.Cos(bb) - 4 *  PI * S1 * x[ii] * Math.Sin(bb);
                        A[jj, ii + Convert.ToInt32(N) / 2] = A[jj, ii + Convert.ToInt32(N) / 2] * SD * G / (2 * sc);
                    }
                    else
                    {
                        A1[jj, ii] = 2 * S1 * Math.Cos(bb) + 2 * S2 * Math.Sin(bb);
                        A1[jj, ii] = A[jj, ii] * SD * G / (2 * sc);
                        A1[jj, ii + Convert.ToInt32(N) / 2] = 4 *  PI * S2 * x[ii] * Math.Cos(bb) - 4 *  PI * S1 * x[ii] * Math.Sin(bb);
                        A1[jj, ii + Convert.ToInt32(N) / 2] = A1[jj, ii + Convert.ToInt32(N) / 2] * SD * G / (2 * sc);
                    }

                }
            }
            goto S999;
        S133:

            for (int i = 0; i < N; i++)
            {
                exam[i] = x[i];
                if (INIT == 1)
                {
                    exph[i] = 0;
                    x[i + Convert.ToInt32(N)] = 0;
                }
                else
                    exph[i] = x[i + Convert.ToInt32(N)];
            }
            MM = 1;
            for (int jj = 0; jj < M; jj++)
            {
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                g = 1e-30;
                this.DELTAR();
                //this.DELTAR(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,N,MM,DETET);
                //CHAMP = new Complex(DAMP * Math.Cos(DPHA), DAMP * Math.Sin(DPHA));
                g = F23 * 1e-6;
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;

                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S32;
                }

                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S32;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;
                    goto S32;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;

                    goto S32;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S32;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S32;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S32;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S32;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S32:
                if (DAMP < 0)
                    g = g;
                sign = g * DETET;
                for (int ii = 0; ii < N; ii++)
                {
                    if (status == 0)
                        A[jj, ii] = Math.Cos(2 *  PI * XP[ii] * Math.Sin(TETTA) + exph[ii]) * sign;
                    else
                        A1[jj, ii] = Math.Cos(2 *  PI * XP[ii] * Math.Sin(TETTA) + exph[ii]) * sign;

                }
            }
            goto S999;
        S111:

            for (int i = 0; i < N; i++)
            {
                exam[i] = x[i];
                if (INIT == 1)
                {
                    exph[i] = 0;
                    x[i + Convert.ToInt32(N)] = 0;
                }
                else
                    exph[i] = x[i + Convert.ToInt32(N)];
            }
            MM = 1;
            for (int jj = 0; jj < M; jj++)
            {
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                g = 1e-30;
                this.DELTAR();
                //this.DELTAR(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,N,MM,DETET);
                //CHAMP = new Complex(DAMP * Math.Cos(DPHA), DAMP * Math.Sin(DPHA));
                g = F45 * 0.095;
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;
                if (TETA[jj] < T3)
                {
                    g = F23 * 0.095;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;

                }
                if (TETA[jj] > T6)
                {
                    g = F67 * 0.095;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;

                }
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S16;
                }

                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S16;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;
                    goto S16;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S16;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;
                    goto S16;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S16;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S16;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S16;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S16:
                if (DAMP < 0)
                    g = g;
                sign = g * DETET;
                for (int ii = 0; ii < N; ii++)
                {
                    if (status == 0)
                        A[jj, ii] = Math.Cos(2 *  PI * XP[ii] * Math.Sin(TETTA) + exph[ii]) * sign;
                    else
                        A1[jj, ii] = Math.Cos(2 *  PI * XP[ii] * Math.Sin(TETTA) + exph[ii]) * sign;
                }
            }
            goto S999;
        S177:
            NN = N / 2;
            for (int i = 0; i < NN; i++)
            {
                exam[i] = x[i];

                exph[i] = x[i + Convert.ToInt32(NN)];

            }

            for (int jj = 0; jj < M; jj++)
            {
                MM = 1;
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                g = 1e-30;
                DPHI = 0;
                this.DELTAR();
                //this.DELTAR(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,NN,MM,DETET);
                //CHAMP = new Complex(DAMP * Math.Cos(DPHA), DAMP * Math.Sin(DPHA));
                g = F45 * 0.095;

                if (TETA[jj] < T3)
                {
                    g = F23 * 0.095;
                    B[jj + 1] = (DAMP) * g;

                }
                if (TETA[jj] > T6)
                {
                    g = F67 * 0.095;
                    B[jj + 1] = (DAMP) * g;

                }
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    B[jj + 1] = (DAMP) * g;
                    goto S137;
                }

                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    B[jj + 1] = (DAMP) * g;
                    goto S137;
                }
                if (TETA[jj] > T2 && TETA[jj] < T3)
                {
                    g = F23;
                    B[jj + 1] = (DAMP - DD[jj]) * g;
                    goto S137;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    B[jj + 1] = (DAMP) * g;
                    goto S137;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    B[jj + 1] = (DAMP - DD[jj]) * g;
                    goto S137;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    B[jj + 1] = (DAMP) * g;
                    goto S137;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;
                    B[jj + 1] = (DAMP) * g;
                    goto S137;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    B[jj + 1] = (DAMP) * g;
                    goto S137;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    B[jj + 1] = (DAMP) * g;
                }
            S137:

                sign = g * DETET;
                for (int ii = 0; ii < NN; ii++)
                {
                    bb = 2 *  PI * x[ii + Convert.ToInt32(NN)];
                    A[jj, ii] = Math.Cos(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;
                    A[jj, ii + Convert.ToInt32(NN)] = -2 *  PI * x[ii] * Math.Sin(2 *  PI * XP[ii] * Math.Sin(TETTA) + bb) * sign;
                }
            }
            goto S999;

        S155:
            NN = N / 2;
            for (int i = 0; i < NN; i++)
            {
                exam[i] =Math.Round(x[i],15);

                exph[i] = Math.Round(x[i + Convert.ToInt32(NN)],15);

            }

            for (int jj = 0; jj < M; jj++)
            {
                MM = 1;
                TETTA = TETA[jj];
                DETET = DELEM[jj];
                g =1.000000003171077* Math.Pow(10, -30);
                DPHI =Math.Round( PI / 6,15);
                this.DELTAR();
                //this.DELTAR(XP,YP,exam,exph,TETTA,DPHI,DAMP,DPHA,NN,MM,DETET);
                //CHAMP = new Complex(DAMP * Math.Cos(DPHA), DAMP * Math.Sin(DPHA));
                //g = F23 * Math.Pow(10, -5);
                g = 9.999999747378752 * Math.Pow(10, -5);
                if (status == 0)
                    B[jj + 1] = (DAMP) * g;
                else
                    B1[jj + 1] = (DAMP) * g;
                if (TETA[jj] <= T1)
                {
                    g = F11;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S35;
                }

                if (TETA[jj] > T1 && TETA[jj] < T2)
                {
                    g = F12;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S35;
                }
                if (TETA[jj] > T2 && (float)System.Math.Round(TETA[jj], 7)  < (float)System.Math.Round(T3, 7) )
                {
                    g = F23;
                    
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj+1]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj+1]) * g;

                    goto S35;
                }
                if (TETA[jj] > T3 && TETA[jj] < T4)
                {
                    g = F34;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S35;
                }
                if (TETA[jj] > T4 && TETA[jj] < T5)
                {
                    g = F45;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S35;
                }
                if (TETA[jj] > T5 && TETA[jj] < T6)
                {
                    g = F56;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S35;
                }
                if (TETA[jj] > T6 && TETA[jj] < T7)
                {
                    g = F67;
                    if (status == 0)
                        B[jj + 1] = (DAMP - DD[jj]) * g;
                    else
                        B1[jj + 1] = (DAMP - DD[jj]) * g;

                    goto S35;
                }
                if (TETA[jj] > T7 && TETA[jj] < T8)
                {
                    g = F78;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                    goto S35;
                }
                if (TETA[jj] >= T8)
                {
                    g = F88;
                    if (status == 0)
                        B[jj + 1] = (DAMP) * g;
                    else
                        B1[jj + 1] = (DAMP) * g;
                }
            S35:

                sign = g * DETET;
                for (int ii = 0; ii < NN; ii++)
                {
                    bb =Math.Round(2 *  PI *x[ii + Convert.ToInt32(NN)],15);
                    if (status == 0)
                    {
                        A[jj, ii] = Math.Round(Math.Cos(2 *  PI * XP[ii] * Math.Sin(TETTA) * Math.Cos(DPHI) + 2 *  PI * YP[ii] * Math.Sin(TETTA) * Math.Sin(DPHI) + bb) * sign,15);
                        A[jj, ii + Convert.ToInt32(NN)] = Math.Round(-2 *  PI * x[ii] * Math.Sin(2 *  PI * XP[ii] * Math.Sin(TETTA) * Math.Cos(DPHI) + 2 *  PI * YP[ii] * Math.Sin(TETTA) * Math.Sin(DPHI) + bb) * sign,15);
                    }

                    else
                    {
                        A1[jj, ii] = Math.Round(Math.Cos(2 *  PI * XP[ii] * Math.Sin(TETTA) * Math.Cos(DPHI) + 2 *  PI * YP[ii] * Math.Sin(TETTA) * Math.Sin(DPHI) + bb) * sign,15);
                        A1[jj, ii + Convert.ToInt32(NN)] = Math.Round(-2 *  PI * x[ii] * Math.Sin(2 *  PI * XP[ii] * Math.Sin(TETTA) * Math.Cos(DPHI) + 2 *  PI * YP[ii] * Math.Sin(TETTA) * Math.Sin(DPHI) + bb) * sign,15);
                    }
                }
            }
        S999:
            aa = Math.Round(F1 / (gmax * F11),15);
            if (aa <= 0) goto S991;
            S991: return 0;
        }
        private void DELTA()
        {
            double RN, angle, aph, dphin, realax, dimagax; Complex C;
            Complex AX = new Complex(0, 0);

            for (int i = 0; i < N; i++)
            {
                RN = Math.Sqrt(Math.Pow(XP[i], 2) + Math.Pow(YP[i], 2));
                if (XP[i] == 0 && YP[i] == 0)
                    dphin = 0;
                else
                    dphin = Math.Atan2(YP[i], XP[i]);
                aph = 2 *  PI * exph[i];
                angle = 2 *  PI * RN * Math.Sin(TETTA) * Math.Cos(DPHI - dphin);
                C = new Complex(0, angle + aph);
                AX = AX + (exam[i]) * Math.Exp(Convert.ToDouble(C));
            }
            DAMP = Math.Abs(Convert.ToDouble(AX)) * (DETET);
            // realax = real(AX);
            //dimagax = dimag(AX);
            DPHA = Math.Atan2(AX.Imaginary, AX.Real);

        }
        private void DELTAR()
        {
            double AMP, NELEX, RN, dphin, angle, phi;
            AMP = 0;
            NELEX = NN;
            for (int i = 0; i < NELEX; i++)
            {
                RN = Math.Sqrt(Math.Pow(XP[i], 2) + Math.Pow(YP[i], 2));
                if (XP[i] == 0 && YP[i] == 0)
                    dphin = 0;
                else
                    dphin = Math.Atan2(YP[i], XP[i]);
                S1 =Math.Round(2 *  PI * exph[i],15);
                angle = Math.Round(2 *  PI * RN * Math.Sin(TETTA) * Math.Cos(DPHI - dphin),14);
                AMP = Math.Round(AMP,15) + Math.Round((exam[i]) * Math.Cos(angle + S1),15);
            }
            AMP = AMP;
            DAMP = Math.Round(((DETET) * AMP),15);
            DPHA = Math.Round(DAMP * 2 *  PI,14);


        }
        private void DELTARREF()
        {
            double AMP, NELEX, RN, dphin, angle, phi;
            AMP = 0;
            NELEX = NN;
            for (int i = 0; i < NELEX; i++)
            {
                RN = Math.Sqrt(Math.Pow(XP[i], 2) + Math.Pow(YP[i], 2));
                if (XP[i] == 0 && YP[i] == 0)
                    dphin = 0;
                else
                    dphin = Math.Atan2(YP[i], XP[i]);
                S1 = 2 *  PI * exph[i];
                angle = 2 *  PI * RN * Math.Sin(TETTAREF) * Math.Cos(DPHI - dphin);
                AMP = AMP + (exam[i]) * Math.Cos(angle + S1);
            }
            AMP = AMP;
            DAMP0 = ((DETETREF) * AMP);
            DPHA0 = DAMP0 * 2 *  PI;


        }

        private void DELTARD()
        {
            double AMP, NELEX, RN, dphin, angle, phi;
            AMP = 0;
            NELEX = NN;
            for (int i = 0; i < NELEX; i++)
            {
                RN = Math.Sqrt(Math.Pow(XP[i], 2) + Math.Pow(YP[i], 2));
                if (XP[i] == 0 && YP[i] == 0)
                    dphin = 0;
                else
                    dphin = Math.Atan2(YP[i], XP[i]);
                S1 = 2 *  PI * exph[i];
                angle = 2 *  PI * RN * Math.Sin(TETTA) * Math.Cos(DPHI - dphin);
                AMP = AMP + (exam[i]) * Math.Sin(angle + S1);
            }
            AMP = AMP;
            DAMP = ((DETET) * AMP);
            DPHA = DAMP * 2 *  PI;
        }
        private void DELTARDREF()
        {
            double AMP, NELEX, RN, dphin, angle, phi;
            AMP = 0;
            NELEX = NN;
            for (int i = 0; i < NELEX; i++)
            {
                RN = Math.Sqrt(Math.Pow(XP[i], 2) + Math.Pow(YP[i], 2));
                if (XP[i] == 0 && YP[i] == 0)
                    dphin = 0;
                else
                    dphin = Math.Atan2(YP[i], XP[i]);
                S1 = 2 *  PI * exph[i];
                angle = 2 *  PI * RN * Math.Sin(TETTAREF) * Math.Cos(DPHI - dphin);
                AMP = AMP + (exam[i]) * Math.Sin(angle + S1);
            }
            AMP = AMP;
            DAMP0 = ((DETETREF) * AMP);
            DPHA0 = DAMP0 * 2 *  PI;
        }
        double COSPIED(double X1, double X2, double B, double C)
        {
            Xa = Math.Round(2 * X1 / X2,15);
            if (B == 0)
                B = 0.01;
            if (Math.Round(Math.Abs(Xa),15) > B)
                return Math.Round(1 - c,15);
            else
                return Math.Round(1 + C * Math.Round(Math.Cos( PI * Xa / B),15),15);
        }

        double FDES(double TETTA, int I1)
        {
            double fdes = 0;
            if (IFONC == 1)
                goto S110;
            if (IFONC == 2)
                goto S220;
            if (IFONC == 3)
                goto S330;
            if (IFONC == 4)
                goto S440;
            S440:
            {
                fdes = gmin * 10;
                if (TETTA > T3 && TETTA < T6)
                    fdes = gmax;
                if (TETTA > T1 && TETTA < T2)
                    fdes = gmin / 10;
                if (TETTA > T7 && TETTA < T8)
                    fdes = gmin / 10;
                goto S33;
            }
        S110:
            {
                if (TETTA < T1)
                    fdes = gmin;
                if (TETTA > T1 && TETTA < T4)
                    fdes = gmax;
                if (TETTA > T4 && TETTA < T5)
                    fdes = gmin;
                if (TETTA > T5 && TETTA < T8)
                    fdes = gmax;
                if (TETTA > T8)
                    fdes = gmin;
                goto S33;
            }
        S220:
            {
                TP5 = T2 + 5 *  PI / 180;

                if (TETTA < T1 && TETTA > T4)
                    fdes = gmin;
                if (TETTA > T2 && TETTA <= TP5)
                    fdes = 1;
                if (TETTA > T5 && TETTA < T3)
                {
                    if ((TETTA - T2) == 0)
                        TETTA = T2 + 5E-3;
                    fdes = 0.1 / (Math.Sin(TETTA - T2));
                }
                goto S33;
            }
        S330:
            {
                ab = 0.3;
                abb = 1 - ab;
                if (TETTA < 0)
                    fdes = -1 * abb / T3 * TETTA + ab;
                if (TETTA >= 0)
                    fdes = abb / T3 * TETTA + ab;
                if (TETTA < T2 || TETTA > T3)
                    fdes = gmin;

                goto S33;
            }


        S33:
            return fdes;
        }

        private void CALCMAT()
        {
            int NP, I1, I2, I3, I4, I5, I6, I7, I8;
            NP = Convert.ToInt32(N) + 2;
            I1 = 1;
            I2 = I1 + Convert.ToInt32(N);
            I3 = I2 + NP;
            I4 = I3 + NP;
            I5 = I4 + NP;
            I6 = I5 + NP;
            I7 = I6 + NP;
            I8 = I7 + NP;
            this.Matrix(IW[I1], IW[I2], IW[I3], IW[I4],
                W[I1], W[I2], W[I3], W[I4], W[I5], W[I6], W[I7], W[I8]);
        }
        private void Matrix(double IB1, double ITH1, double KSIG1, double IA1,
     double XX1, double WSIG1, double SIG1, double SR1, double TAU1, double WA1, double WB1, double D1)
        {
            int IQ, NTH; double K = 1, KK = 1, CA = 1, CB, ID = 1, IDD = 1, TA = 1, TB = 1, TC = 1;
            int IQM = 1, IQP = 1; double SA = 1, H, SB = 1; double SC = 1; double NP = 1, DELT = 1;
            double HLB, HUB, ISW, CC, I, II, J;
            double[] IA = new double[182];
            double[] SR = new double[181];
            double[] KSIG = new double[181];
            double[] WSIG = new double[181];
            double[] SIG = new double[181];
            double[] IB = new double[181];
            double[] WA = new double[181]; double[] WB = new double[181];

            double[] D = new double[182]; double[] TAU = new double[181];
            double[] ITH = new double[181];
            double KTH = 1, WTH = 1, RHO = 1, RATIO = 1, OLDTH = 1, THETA = 1, HNUM = 1, IP = 1;
            double RHDMAX;
            HLB = 0;
            HUB = -1;
            for (int i = 1; i <= M; i++)
            {
                if (Math.Abs(B[i]) <= HUB)
                    goto S10;
                HUB = Math.Abs(B[i]);
                K = i;
            S10: IA[i] = i;

            }
            IQ = 1;
            if (K <= 1) goto S40;
            ISW = 1;

        S20:
            for (int i = 0; i < N; i++)
            {
                CC =Math.Round(A[Convert.ToInt32(K - 1), i],14);
                A[Convert.ToInt32(K - 1), i] = Math.Round(A[IQ-1, i],15);
                A[IQ-1, i] = Math.Round(CC,15);
            }


            CC = Math.Round(B[Convert.ToInt32(K)],14);
            B[Convert.ToInt32(K)] = Math.Round(B[IQ],15);
            B[IQ] = Math.Round(CC,15);
            I = Math.Round(IA[Convert.ToInt32(K)],15);
            IA[Convert.ToInt32(K)] = Math.Round(IA[IQ],15);
            IA[IQ] = I;
            if (ISW == 1)
                goto S40;
            if (ISW == 2)
                goto S230;
            if (ISW == 3)
                goto S350;
            if (ISW == 4)
                goto S660;
            // goto S40,S230,S350,S660,ISW;
            S40: SR[1] = Math.Abs(1) * Math.Sign(B[1]);
            WSIG[1] = SR[1];
            KSIG[1] = 0;
            for (int i = 1; i <= NOMB_ELEM; i++)
            {
                IB[i] = i;
                X[i-1] = 0;
                WSIG[i + 2] =Math.Round(SR[1] * A[0, i - 1],15);
                KSIG[i + 2] = 0;
                if (WSIG[i + 2] != 0)
                    goto S50;
                WSIG[i + 2] = 1;
                KSIG[i + 2] = 1;
            S50: SR[i + 2] = Math.Abs(1) * Math.Sign(WSIG[i + 2]);
            }
            IQM = 0;
            IQP = 2;
            NP = N + 2;
        S60:
            KSIG[IQP] = N;
            SA = 1;
            SB = 0;
            SC = 0;
            for (int i = 1; i <= NP; i++)
            {
                SIG[i] = 0;
                if (KSIG[i] == 0)
                    SIG[i] = WSIG[i];
                if (i - IQP < 0) goto S70;
                if (i - IQP == 0)
                    goto S90;
                if (i - IQP > 0)
                    goto S80;

                S70: SC = Math.Round(SC + SIG[i] * B[i],15);
                //SB =  Math.Abs(SIG[i]);
                goto S90;
            S80: SB =Math.Round(SB + Math.Abs(SIG[i]),15);
            S90: continue;
            }
            //KSIG[IQP] = N;
            G = GBAR;
            H =Math.Round(Math.Abs(SC) - G * SB,15);
            if (H >= 0) goto S100;
            G = Math.Round(Math.Abs(SC / SB),15);
            H = 0;
        S100:
            DELT = G;
            for (int i = 1; i <= IQ; i++)
                WA[i] = B[i];
            if (IQ > N) goto S130;
            for (int i = IQ; i <= N; i++)
            {
                II = IB[i];
                XX[Convert.ToInt32(II)] = Math.Abs(G) * Math.Sign(-SR[i + 2]);
                for (int j = 1; j <= IQ; j++)
                    WA[j] = WA[j] + A[j-1, Convert.ToInt32(II)-1] * XX[Convert.ToInt32(II)];
            }
            if (IQ <= 1) goto S190;
            S130: for (int i = 1; i <= IQ; i++)
                WB[i] = WA[i] - (Math.Abs(H) * Math.Sign(SR[i]));
            ID = 0;
            for (int i = 1; i <= IQM; i++)
            {
                II = IB[i];
                XX[Convert.ToInt32(II)] = 0;
                for (int j = 1; j <= IQ; j++)
                    XX[Convert.ToInt32(II)] = XX[Convert.ToInt32(II)] - WA[j] * D[j + Convert.ToInt32(ID)];
                ID = ID + Convert.ToInt32(NP);
                for (int j = 1; j <= IQ; j++)
                    WB[j] = WB[j] + A[j-1, Convert.ToInt32(II) - 1] * XX[Convert.ToInt32(II)];
            }

            ID = 0;
            for (int i = 1; i <= IQM; i++)
            {
                SUM = 0;
                for (int j = 1; j <= IQ; j++)
                    SUM = SUM + WB[j] * D[j + Convert.ToInt32(ID)];
                ID = ID + Convert.ToInt32(NP);
                II = IB[i];
                XX[Convert.ToInt32(II)] = XX[Convert.ToInt32(II)] - SUM;
                if (DELT >= Math.Abs(XX[Convert.ToInt32(II)])) goto S181;
                DELT = Math.Abs(XX[Convert.ToInt32(II)]);
                K = i;
            S181: continue;
            }

        S190:
            CA = H;
            CB = 0;
            for (int i = 1; i <= IQ; i++)
            {
                SUM = B[i];
                for (int j = 1; j <= N; j++)
                    SUM = SUM + A[i-1, j - 1] * XX[j];

                CA = Math.Min(CA, SUM * SR[i]);
                CB = Math.Max(CB, Math.Abs(SUM));
            }
            HLB = Math.Max(HLB, CA);
            testc = testc + 1;
            if (CB < HUB)
                goto S240;
            S220:
            ISW = 2;
            var s = X;
            IQ = 1;
        S230:
            K = IA[IQ];
           
            if (K > IQ)
                goto S20;
            IQ = IQ + 1;
            if (IQ < M) goto S230;
            TOL = HUB - HLB;
            return;
        S240:
            if (DELT <= Math.Min(GBAR, 10 * G))
                goto S260;
            if (HUB <= HLB + TOL)
                goto S220;
            KK = IB[Convert.ToInt32(K)];
            IB[Convert.ToInt32(K)] = IB[IQM];
            IB[IQM] = KK;
            DELT = DELT - G;
            SR[IQP] = Math.Abs(1) * Math.Sign(-XX[Convert.ToInt32(KK)]);
            WSIG[IQP] = SR[IQP];
            CA = 0;
            ID = NP * K - NP;
            IQM = IQM - 1;
            IDD = NP * IQM;
            for (int i = 1; i <= IQ; i++)
            {
                TAU[i] = D[i + Convert.ToInt32(ID)];
                D[i + Convert.ToInt32(ID)] = D[i + Convert.ToInt32(IDD)];
                CA = CA + TAU[i] * A[i-1, Convert.ToInt32(KK) - 1];
            }
            CA = SR[IQP] / CA;
            goto S350;
        S260:
            DELT = 0;
            if (IQ >= M) goto S290;
            for (int i = IQP; i <= M; i++)
            {
                SUM = B[i];
                for (int j = 1; j <= N; j++)
                    SUM = SUM + A[i - 1, j - 1] * XX[j];
                if (DELT >= Math.Abs(SUM))
                    goto S280;
                DELT =Math.Round(Math.Abs(SUM),15);
                K = i;
                CA = Math.Abs(1) * Math.Sign(SUM);
            S280: continue;
            }
            if (DELT >= HUB) goto S310;
            S290: HUB = Math.Max(CB, DELT);
            for (int i = 1; i <= N; i++)
                X[i-1] = XX[i];
            if (CB >= HUB) goto S220;
            S310:
            if (HUB <= HLB + TOL) goto S220;
            DELT =Math.Round(DELT - H,13);
            SR[IQP] = CA;
            WSIG[IQP] = CA;
            TAU[IQP] = 1;
            for (int i = 1; i <= IQ; i++)
                TAU[i] = -CA * SIG[i];
            if (IQ <= 1) goto S340;
            ID = -NP;
            for (int i = 1; i <= IQM; i++)
            {
                ID = ID + NP;
                D[IQP + Convert.ToInt32(ID)] = 0;
                CB =Math.Round(A[Convert.ToInt32(K - 1), Convert.ToInt32(IB[i] - 1)],14);
                for (int j = 1; j <= IQ; j++)
                    TAU[j] = TAU[j] - CB * D[j + Convert.ToInt32(ID)];
            }
        S340:
            IQ = IQP;
            ISW = 3;
            if (K > IQ) goto S20;
            S350:
            CB =Math.Round(TAU[IQ - 1],15) * Math.Round(SR[IQ - 1],15) + Math.Round(TAU[IQ],15) * Math.Round(SR[IQ],15);
            TB = 0;
            I = 0;
            if (IQM <= 0) goto S390;
            ID = 0;
        S360: I = I + 1;
            II = IB[Convert.ToInt32(I)];
            SUM = 0;
            for (int j = 1; j <= IQ; j++)
                SUM = SUM + TAU[j] * A[j-1, Convert.ToInt32(II) - 1];
            if (I > IQM) goto S410;
            for (int j = 1; j <= IQ; j++)
                TAU[j] = TAU[j] - SUM * D[j + Convert.ToInt32(ID)];
            ID = ID + NP;
            CB = CB + TAU[Convert.ToInt32(I)] * SR[Convert.ToInt32(I)];
            if (I < IQM) goto S360;
            S390: for (int j = 1; j <= IQ; j++)
                TAU[j] = CA * (TAU[j] - CB * SIG[j]);
            if (IQ - NP < 0) goto S360;
            if (IQ - NP >= 0) goto S420;

            S410: TAU[Convert.ToInt32(I) + 2] = SUM;
            TB = TB + TAU[Convert.ToInt32(I) + 2] * SR[Convert.ToInt32(I) + 2];
            if (I < N) goto S360;
            S420: TA = 0;
            TC = DELT + G * TB;
            NTH = 0;
            for (int i = 1; i <= NP; i++)
            {

                if (SR[i] * TAU[i] >= 0) goto S430;
                NTH = NTH + 1;
                WA[NTH] = -SIG[i] / TAU[i];
                if (KSIG[i] > 0) WA[NTH] = -Convert.ToDouble(KSIG[i]);
                ITH[NTH] = i;
            S430: continue;
            }

            RHDMAX = 0;
            IP = 1;
            RATIO = 1;
            RHO = DELT * SA;
            if (G >= GBAR) goto S440;
            RATIO = -1;
            RHO = DELT * SB;
            HNUM = SB * (G - GBAR);
            OLDTH = 0;
        S440: K = IP;
            goto S470;
        S450: K = K + 1;
            if (WA[Convert.ToInt32(K)] - THETA < 0) goto S470;
            if (WA[Convert.ToInt32(K)] - THETA == 0) goto S460;
            if (WA[Convert.ToInt32(K)] - THETA > 0) goto S480;

            S460: I = ITH[Convert.ToInt32(KK)];
            J = ITH[Convert.ToInt32(K)];
            if (Math.Abs(WSIG[Convert.ToInt32(J)] * TAU[Convert.ToInt32(I)]) >= Math.Abs(WSIG[Convert.ToInt32(I)] * TAU[Convert.ToInt32(J)]))
                goto S480;
            S470: THETA = WA[Convert.ToInt32(K)];
            KK = K;
        S480: if (K < NTH) goto S450;
            WA[Convert.ToInt32(KK)] = WA[Convert.ToInt32(IP)];
            K = ITH[Convert.ToInt32(KK)];
            ITH[Convert.ToInt32(KK)] = ITH[Convert.ToInt32(IP)];
            ITH[Convert.ToInt32(IP)] = K;
            if (RATIO >= 0) goto S490;
            THETA = Math.Max(THETA, 0);
            CA = SC + THETA * TC - GBAR * (SB + THETA * TB);
            if (CA <= 0) goto S490;
            THETA = (CA * OLDTH - HNUM * THETA) / (CA - HNUM);
            RATIO = (SB + THETA * TB) / (SA + THETA * TA);
        S490: if (K > IQ) goto S500;
            SA = SA - 2 * Math.Abs(SIG[Convert.ToInt32(K)]);
            TA = TA + 2 * Math.Abs(TAU[Convert.ToInt32(K)]);
            goto S510;
        S500: SB = SB - 2 * Math.Abs(SIG[Convert.ToInt32(K)]);
            TB = TB + 2 * Math.Abs(TAU[Convert.ToInt32(K)]);
        S510: CB = RHO;
            RHO = RATIO * (SA * (TC - GBAR * TB) - TA * (SC - GBAR * SB));
            if (RATIO >= 0) goto S520;
            RHO = SB * TC - TB * SC;
            OLDTH = THETA;
            HNUM = CA;
        S520: WA[Convert.ToInt32(IP)] = CB - RHO;
            RHDMAX = Math.Max(RHDMAX, WA[Convert.ToInt32(IP)]);
            IP = IP + 1;
            if (RHO < 0) goto S530;
            if (IP <= NTH) goto S440;
            S530: RHDMAX = 0.25 * RHDMAX;
        S540: IP = IP - 1;
            if (WA[Convert.ToInt32(IP)] < RHDMAX) goto S540;
            K = ITH[Convert.ToInt32(IP)];
            WTH = -WSIG[Convert.ToInt32(K)] / TAU[Convert.ToInt32(K)];
            KTH = KSIG[Convert.ToInt32(K)];
            goto S560;
        S550: SR[Convert.ToInt32(ITH[Convert.ToInt32(IP)])] = -SR[Convert.ToInt32(ITH[Convert.ToInt32(IP)])];
        S560: IP = IP - 1;
            if (IP > 0) goto S550;
            if (K <= IQ) goto S600;
            KK = IB[Convert.ToInt32(K) - 2];
            if (IQM <= 0) goto S580;
            IDD = K - NP;
            ID = -NP;
            for (int i = 1; i <= IQM; i++)
            {
                IDD = IDD + NP;
                ID = ID + NP;
                D[Convert.ToInt32(IDD)] = 0;
                for (int j = 0; j < IQ; j++)
                    D[Convert.ToInt32(IDD)] = D[Convert.ToInt32(IDD)] + D[Convert.ToInt32(j)+1 + Convert.ToInt32(ID)] * A[j, Convert.ToInt32(KK)-1];

            }

        S580: ID = NP * IQM;
            IQM = IQM + 1;
            for (int j = 1; j <= IQ; j++)
                D[Convert.ToInt32(j) + Convert.ToInt32(ID)] = TAU[Convert.ToInt32(j)] / TAU[Convert.ToInt32(K)];
            D[Convert.ToInt32(K) + Convert.ToInt32(ID)] = 0;
            IB[Convert.ToInt32(K) - 2] = IB[IQM];
            IB[IQM] = KK;
        S600: SC = 0;
            TC = 0;
            for (int i = 1; i <= IQ; i++)
            {
                SC = SC + SR[i] * SIG[i];
                TC =Math.Round(TC,15) +Math.Round(SR[i],15) *Math.Round(TAU[i],15);
            }

            if (IQM <= 0) goto S650;
            CA = SC * TAU[Convert.ToInt32(K)] - TC * SIG[Convert.ToInt32(K)];
            ID = 0;
            for (int i = 1; i <= IQM; i++)
            {
                SUM = 0;
                for (int j = 1; j <= IQ; j++)
                    SUM = SUM + D[j + Convert.ToInt32(ID)] * SR[j];

                SA = (TC * D[Convert.ToInt32(K) + Convert.ToInt32(ID)] - SUM * TAU[Convert.ToInt32(K)]) / CA;
                TA = (SUM * SIG[Convert.ToInt32(K)] - SC * D[Convert.ToInt32(K) + Convert.ToInt32(ID)]) / CA;
                for (int j = 1; j <= IQ; j++)
                    D[j + Convert.ToInt32(ID)] = D[j + Convert.ToInt32(ID)] + SA * SIG[Convert.ToInt32(j)] + TA * TAU[Convert.ToInt32(j)];


                if (K >= IQ) goto S640;
                D[Convert.ToInt32(K) + Convert.ToInt32(ID)] = D[Convert.ToInt32(IQ) + Convert.ToInt32(ID)];
            S640: ID = ID + NP;
            }

        S650: if (K > IQ) goto S670;
            ISW = 4;
            if (K < IQ) goto S20;
            S660: IQ = IQ - 1;
        S670: if (KTH == 0) SC = SC + WTH * TC;
            for (int i = 1; i <= NP; i++)
            {
                if (TAU[i] == 0) goto S700;
                if (KSIG[i] - KTH < 0) goto S700;
                if (KSIG[i] - KTH == 0) goto S680;
                if (KSIG[i] - KTH > 0) goto S690;
                S680: WSIG[i] = WSIG[i] + WTH * TAU[i];
                if (SR[i] * WSIG[i] > 0) goto S700;

                WSIG[i] = SR[i];
                KSIG[i] = KTH + 1;
                goto S700;
            S690: WSIG[i] = WTH * TAU[i];
                KSIG[i] = KTH;
            S700: WSIG[i] = WSIG[i] / SC;
            }
            IQP = IQ + 1;
            WSIG[Convert.ToInt32(K)] = WSIG[IQP];
            KSIG[Convert.ToInt32(K)] = KSIG[IQP];
            SR[Convert.ToInt32(K)] = SR[IQP];

            goto S60;

        }
        private void DELTAREF()
        {
            double RN, angle, aph, dphin, realax, dimagax; Complex C;
            Complex AX = new Complex(0, 0);

            for (int i = 0; i < N; i++)
            {
                RN = Math.Sqrt(Math.Pow(XP[i], 2) + Math.Pow(YP[i], 2));
                if (XP[i] == 0 && YP[i] == 0)
                    dphin = 0;
                else
                    dphin = Math.Atan2(YP[i], XP[i]);
                aph = 2 *  PI * exph[i];
                angle = 2 *  PI * RN * Math.Sin(TETTAREF) * Math.Cos(DPHI - dphin);
                C = new Complex(0, angle + aph);
                AX = AX + (exam[i]) * Math.Exp(Convert.ToDouble(C));
            }
            DAMP0 = Math.Abs(Convert.ToDouble(AX)) * (DETETREF);
            // realax = real(AX);
            //dimagax = dimag(AX);
            DPHA0 = Math.Atan2(AX.Imaginary, AX.Real);

        }
        private void DIAG_Click(object sender, EventArgs e)
        {
            List<string> writeLines = new List<string>();
            List<string> writeLinesXXP = new List<string>();
            List<string> writeLinesPHA = new List<string>();
            double AMPMIN, AMPMAXL, am, ATEST;
            MM = 1;
            DPHI = 0;
            AMPMIN = 100;
            AMPMAXL = Math.Pow(10, -5);
            if (ISYNT == 1) goto S33;
            if (ISYNT == 2) goto S55;
            if (ISYNT == 3) goto S33;
            if (ISYNT == 5) goto S55;
            if (ISYNT == 7) goto S55;
            if (ISYNT == 11) goto S133;
            if (ISYNT == 12) goto S155;
            if (ISYNT == 13) goto S133;
            if (ISYNT == 14) goto S1441;
            if (ISYNT == 15) goto S155;
            if (ISYNT == 16) goto S144;
            if (ISYNT == 17) goto S155;
            if (ISYNT == 19) goto S199;
            if (ISYNT == 9) goto S919;
            S33:
            for (int i = 0; i < NV; i++)
            {
                exam[i] = X[i];
                exph[i] = X[i + Convert.ToInt32(NV)];
            }
            goto S10;
        S919:
            for (int i = 0; i < NV; i++)
            {
                exam[i] = 1;
                exph[i] = X[i];
            }
            goto S10;
        S55:
            for (int i = 0; i < NV; i++)
            {
                exam[i] = X[i];
                exph[i] = X[i + Convert.ToInt32(NV)];
            }
            goto S10;
        S133: for (int i = 0; i < NV; i++)
            {
                exam[i] = X[i];
                if (INIT == 1)
                    exph[i] = 0;
                else
                    exph[i] = X[i + Convert.ToInt32(NV)];
            }
            goto S35;
        S144:
            for (int i = 0; i < NV; i++)
            {
                exam[i] = X[i];
                exph[i] = X[i + Convert.ToInt32(NV)];
            }
            goto S45;
        S1441: for (int i = 0; i < NV; i++)
            {
                exam[i] = X[i];
                if (INIT == 1)
                {
                    exph[i] = 0;
                    X[i + Convert.ToInt32(NV)] = 0;
                }
                else
                    exph[i] = X[i + Convert.ToInt32(NV)];
            }
            goto S45;
        S155:
            for (int i = 0; i < NV; i++)
            {
                exam[i] = X[i];
                exph[i] = X[i + Convert.ToInt32(NV)];
            }
            goto S35;
        S199:
            for (int i = 0; i < NV; i++)
            {
                exam[i] = 1;
                exph[i] = X[i];
            }
            goto S35;
        S10:
            DETET = 1;
            am = 0;
            for (int j = 0; j < M; j++)
            {
                TETTA = TETA[j];
                DETET = DELEM[j];
                MM = 1;
                this.DELTA();
                if (DAMP > am)
                {
                    am = DAMP;
                    TETTAREF = TETTA;
                    DETETREF = DETET;
                }
            }
            this.DELTAREF();

            for (int j = 0; j < M; j++)
            {
                TETTA = TETA[j];
                DETET = DELEM[j];
                MM = 1;
                this.DELTA();
                if (Math.Pow(DAMP / DAMP0, 2) < Math.Pow(10, -3))
                    DAMP = DAMP0 * Math.Sqrt(Math.Pow(10, -3));
                writeLines.Add((TETA[j] * 180 /  PI).ToString() + ',' + 20 * Math.Log10(DAMP / DAMP0));
                writeLinesPHA.Add((TETA[j] * 180 /  PI).ToString() + ',' + (DPHA - DPHA0).ToString());

                if (TETTA > T2 && TETTA < T3)
                {
                    if (DAMP < AMPMIN)
                        AMPMIN = DAMP;
                }
                if (TETTA < T1 || TETTA > T4)
                {
                    if (Math.Abs(DAMP) > AMPMAXL)
                        AMPMAXL = Math.Abs(DAMP);
                }
            }
            File.WriteAllLines(pathAMPP, writeLines);
            File.WriteAllLines(pathPHA, writeLinesPHA);
            ATEST = 20 * Math.Log10(AMPMAXL / am);
            goto S994;
        S35:
            DETET = 1;
            am = -10;
            for (int j = 0; j < M; j++)
            {
                TETTA = TETA[j];
                DETET = DELEM[j];
                MM = 1;
                DPHI = 0;
                this.DELTAR();
                if (Math.Abs(DAMP) > am)
                {
                    am = Math.Abs(DAMP);
                    TETTAREF = TETTA;
                    DETETREF = DETET;
                }
            }
            this.DELTARREF();
            for (int j = 0; j < M; j++)
            {
                TETTA = TETA[j];
                DETET = DELEM[j];

                this.DELTAR();
                if (Math.Pow(DAMP / DAMP0, 2) < Math.Pow(10, -5))
                    DAMP = DAMP0 * Math.Sqrt(Math.Pow(10, -5));
                writeLines.Add((TETA[j] * 180 /  PI).ToString() + ',' + 10 * Math.Log10(Math.Pow(DAMP / DAMP0, 2)));

                if (TETTA < T1 || TETTA > T4)
                {
                    if (Math.Abs(DAMP) > AMPMAXL)
                        AMPMAXL = (Math.Abs(DAMP));
                }
            }
            File.WriteAllLines(pathAMPP, writeLines);
            ATEST = 20 * Math.Log10(AMPMAXL / am);
            XMAX = -99;
            for (int i = 0; i < NV; i++)
            {
                ab = Math.Abs(exam[i]);
                if (ab > XMAX)
                    XMAX = ab;
            }
            for (int i = 0; i < NV; i++)
            {
                writeLinesXXP.Add((exam[i] / XMAX).ToString() + ',' + exph[i] * 360);

            }
            File.WriteAllLines(pathXXP, writeLinesXXP);
            goto S99;
        S45:
            am = 0;
            for (int j = 0; j < M; j++)
            {
                TETTA = TETA[j];
                DETET = DELEM[j];
                MM = 1;
                this.DELTARD();
                if (DAMP > am)
                {
                    am = DAMP;
                    TETTAREF = TETTA;
                    DETETREF = DETET;

                }
            }
            this.DELTARDREF();
            for (int j = 0; j < M; j++)
            {
                TETTA = TETA[j];
                DETET = DELEM[j];

                this.DELTARD();
                if (Math.Pow(DAMP / DAMP0, 2) < Math.Pow(10, -5))
                    DAMP = DAMP0 * Math.Sqrt(Math.Pow(10, -5));
                writeLines.Add((TETA[j] * 180 /  PI).ToString() + ',' + 10 * Math.Log10(Math.Pow(DAMP / am, 2)));

                if (TETTA <= T1 || TETTA >= T8)
                {
                    if (Math.Abs(DAMP) > AMPMAXL)
                        AMPMAXL = Math.Abs(DAMP);
                }
                if (TETTA >= T4 && TETTA <= T5)
                {
                    if (Math.Abs(DAMP) > AMPMAXL)
                        AMPMAXL = Math.Abs(DAMP);
                }
            }
            File.WriteAllLines(pathAMPP, writeLines);
            ATEST = 20 * Math.Log10(AMPMAXL / am);
        S994:
            XMAX = -99;
            for (int i = 0; i < NV; i++)
            {
                ab = Math.Abs(exam[i]);
                if (ab > XMAX)
                    XMAX = ab;
            }
            for (int i = 0; i < NV; i++)
            {
                writeLinesXXP.Add((exam[i] / XMAX).ToString() + ',' + exph[i] * 360);

            }
            File.WriteAllLines(pathXXP, writeLinesXXP);
        S99:
            return;
        }

        private void NORMLOB_Click(object sender, EventArgs e)
        {
            double AMAX = -99; Double DN;
            string[] lines = File.ReadAllLines(pathAMPP);
            for (int j = 0; j < lines.Length; j++)
            {
                if (Convert.ToDouble(lines[j].Split(',')[1]) > AMAX)
                    AMAX = Convert.ToDouble(lines[j].Split(',')[1]);
            }
            List<string> writeLines = new List<string>();
            for (int j = 0; j < lines.Length; j++)
            {
                DN = Convert.ToDouble(lines[j].Split(',')[1]) - AMAX;
                if (DN < -50)
                    DN = -50;
                writeLines.Add(lines[j].Split(',')[0] + ',' + DN);
            }
            File.WriteAllLines(pathAMP, writeLines);


        }

        private void NORMX_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(pathXXP);
            for (int j = 0; j < NV; j++)
            {
                exam[j] = Convert.ToDouble(lines[j].Split(',')[0]);
                exph[j] = Convert.ToDouble(lines[j].Split(',')[1]);
                ab = Math.Abs(X[j]);
                if (ab > XMAX)
                    XMAX = ab;
            }
            List<string> writeLines = new List<string>();
            for (int j = 0; j < NV; j++)
            {
                writeLines.Add(exam[j].ToString() + ',' + exph[j].ToString());
            }
            File.WriteAllLines(pathX, writeLines);
        }

    }
}
