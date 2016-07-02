using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZedGraph;
using System.Globalization;


namespace graph_plot
{
    

    public partial class Form1 : Form
    {
        public static string FileNames;
        public static decimal[] x=new decimal[100000];
        public static decimal[] y = new decimal[100000];

        

        public static int selc = 0,ind=0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the graph
            CreateGraph(zedGraphControl1);
            // Size the control to fill the form with a margin
           // SetSize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            FileNames = "";
            selc = 0;

            Array.Clear(x, 0, x.Length);
            Array.Clear(y, 0, y.Length);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            // Set filter options and filter index.
            openFileDialog.Filter = "CSV File (.csv)|*.csv|Text Files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 2;
           

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileNames = openFileDialog.FileName;
               // MessageBox.Show(openFileDialog.Filter.f);
                if (openFileDialog.FilterIndex == 1)
                    selc = 1;
                else
                    selc = 2;
            }
            if (FileNames.Length > 0)
            {
                read();
                label1.Text = "File selected!";
            }
            else
            {
                label1.Text = "File is not selected!";
            }
            
        }

        public static void read()
        {
            ind = 0;
            int fs = 0;
            using (StreamReader sr = new StreamReader(FileNames))
            {
                string line = "";
                string dv = "\t";
                if(selc==2)
                {
                    while ((line = sr.ReadLine()) != null)
                {
                    if (fs > 0)
                    {
                        string[] values = line.Split(dv.ToArray());
                        decimal fn = decimal.Parse(values[0], NumberStyles.Float);
                        decimal sn = decimal.Parse(values[0], NumberStyles.Float);

                        x[ind] = fn;
                        y[ind] = sn;
                        ind++;
                        line = "";
                    }
                    fs++;
                }
            }
                else if (selc == 1)
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (fs > 0)
                        {
                            string[] values = line.Split(',');
                            decimal fn = decimal.Parse(values[0], NumberStyles.Float);
                            decimal sn = decimal.Parse(values[0], NumberStyles.Float);

                            x[ind] = fn;
                            y[ind] = sn;
                            ind++;
                            line = "";
                        }
                        fs++;
                    }
                
                
                
                
                }

            
            
            }
            
            
            MessageBox.Show(x[0].ToString());
        
        }
        // Respond to the form 'Resize' event
        private void Form1_Resize(object sender, EventArgs e)
        {
           // SetSize();
        }

        // SetSize() is separate from Resize() so we can 
        // call it independently from the Form1_Load() method
        // This leaves a 10 px margin around the outside of the control
        // Customize this to fit your needs
        private void SetSize()
        {
            zedGraphControl1.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
            zedGraphControl1.Size = new Size(ClientRectangle.Width - 40,
                                    ClientRectangle.Height - 40);
        }
        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        // Build the Chart
        private void CreateGraph(ZedGraphControl zgc)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;

            zgc.GraphPane.CurveList.Clear();
            myPane.AxisChange();
            zgc.Invalidate();
            zgc.Refresh();

            // Set the Titles
            myPane.Title.Text = "Graph\n";
            myPane.XAxis.Title.Text = " X Axis";
            myPane.YAxis.Title.Text = " Y Axis";

            // Make up some data arrays based on the Sine function
            double x1, y1, y2;
            PointPairList list1 = new PointPairList();

            for (int i = 0; i < ind; i++)
            {
                x1 =Convert.ToDouble(x[i]);
                y1 = Convert.ToDouble(y[i]);
                
                list1.Add( x1, y1);
               
            }

            // Generate a red curve with diamond
            // symbols, and "Porsche" in the legend
            LineItem myCurve = myPane.AddCurve("Salery",
                  list1, Color.Blue, SymbolType.XCross);

           

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zgc.AxisChange();
            zgc.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            CreateGraph(zedGraphControl1);
        }

    }
}
