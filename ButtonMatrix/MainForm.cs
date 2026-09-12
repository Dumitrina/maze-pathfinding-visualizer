using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace ButtonMatrix
{
    public partial class MainForm : Form
    {
       
        private int n, m;
        private Button[,] btn;
        private TableLayoutPanel tbl;
        private int[,] L;
        private int[,] x;   // matricea solutie
        private int i1, j1, i2, j2;
        private Color backgroundColor = Color.FromArgb(255, 242, 204);      //(255, 236, 179);
        private Color wallColor = Color.FromArgb(0, 128, 0);    //(128, 96, 0);
        private Color pathColor = Color.FromArgb(179, 134, 0);
        private readonly int[] di = { -1, 0, 1, 0 };
        private readonly int[] dj = { 0, 1, 0, -1 };
        private int nrsol;


        bool Ok(int i, int j)
        {
            if (i < 0 || i >= n || j < 0 || j >= m)
                return false;
            if (L[i, j] == 1) // zid
                return false;
            if (x[i, j] != 0)
                return false;
            return true;
        }

        void Back(int i, int j, int k)
        {
            if (bw.CancellationPending)
                return;

            if (!Ok(i, j))
                return;
            x[i, j] = k;
            bw.ReportProgress(10, new State(i, j, k, pathColor));
            Thread.Sleep(300);
            if (i == i2 && j == j2)
                bw.ReportProgress(10, null);
            else
                for (int d = 0; d < 4; ++d)
                {
                    int iv = i + di[d];
                    int jv = j + dj[d];
                    Back(iv, jv, k + 1);
                }
            x[i, j] = 0;
            bw.ReportProgress(10, new State(i, j, 0, backgroundColor));
            Thread.Sleep(300);
        }

        private void WriteSol()
        {
            nrsol++;
            lblSol.Text = nrsol.ToString();
            lblSol.Update();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy)
                bw.CancelAsync();
            nrsol = 0;
            lblSol.Text = "0";
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            nrsol = 0;
           
            Back(i1, j1, 1);
        }
        
        // se executa pe threadul GUI
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            State s = (State)e.UserState;
            if (s != null)
            {
                int i = s.I;
                int j = s.J;
                int k = s.K;
                Color c = s.C;
                btn[i, j].BackColor = c;
                btn[i, j].Text = k != 0 ? k.ToString() : string.Empty;
                btn[i, j].Update();
                Thread.Sleep(200);
            }
            else
            {
                nrsol++;
                lblSol.Text = nrsol.ToString();
                lblSol.Update();
                Thread.Sleep(2000);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
          //  btn[i1,j1].BackgroundImage = "soarece.pgn";   
            if (bw.IsBusy) return;
            bw.RunWorkerAsync();
        }

        public MainForm()
        {
            InitializeComponent();
            // creez o matrice de referinte la viitoare butoane
            ReadData();
            CreateGUI();

            pictureBox.Image = Image.FromFile("soarece.png");
            pictureBranza.Image = Image.FromFile("branza.png");
        }

        private void ReadData()
        {
            StreamReader sr = new StreamReader("labirint.in");
            string line = sr.ReadLine();
            char[] sep = { ' '};
            string[] s = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            n = int.Parse(s[0]);
            m = int.Parse(s[1]);
            L = new int[n, m];
            x = new int[n, m];

            line = sr.ReadLine(); // citim linia a doua
            s = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            i1 = int.Parse(s[0]); i1--;
            j1 = int.Parse(s[1]); j1--;
            i2 = int.Parse(s[2]); i2--;
            j2 = int.Parse(s[3]); j2--;

            for (int i = 0; i < n; ++i)
            {
                line = sr.ReadLine();
                s = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < m; ++j)
                    L[i, j] = int.Parse(s[j]);
            }
        }

        private void CreateGUI()
        {
            btn = new Button[n, m];
            tbl = new TableLayoutPanel()
            {
                RowCount = n,
                ColumnCount = m,
                Dock = DockStyle.Fill
            };

            int H = panel.Height;
            int W = panel.Width;
            int lat = Math.Min(H, W) / Math.Max(n, m);

            for (int j = 0; j < m; j++)
                tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, lat));

            for (int i = 0; i < n; i++)
            {
                tbl.RowStyles.Add(new RowStyle(SizeType.Percent, lat));
                for (int j = 0; j < m; ++j)
                {
                    btn[i, j] = new Button()
                    {
                        Dock = DockStyle.Fill,
                        BackColor = L[i, j] == 0 ? backgroundColor: wallColor,
                        FlatStyle = FlatStyle.Flat,
                        Margin = new Padding(0),
                        Font = new Font(new FontFamily("Arial"), 14, FontStyle.Bold)
                    };

                   
                    tbl.Controls.Add(btn[i, j]);
                }
            }
            panel.Controls.Add(tbl);
        }


    }

}
