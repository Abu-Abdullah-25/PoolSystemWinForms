﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8Pool
{
    public partial class PoolTable : UserControl
    {


        public PoolTable()
        {
            InitializeComponent();
        }

        public class TableCompletedEventArgs : EventArgs
        {
            public string TimeText { get; }
            public int TimeInSeconds { get; }
            public float RatePerHour { get; }
            public float TotalFees { get; }

            public TableCompletedEventArgs(string TimeText, int TimeInSeconds, float RatePerHour, float TotalFees)
            {
                this.TimeText = TimeText;
                this.TimeInSeconds = TimeInSeconds;
                this.RatePerHour = RatePerHour;
                this.TotalFees = TotalFees;
            }
        }

        public event EventHandler<TableCompletedEventArgs> OnTableComplete;

        public void RaiseOnTableComplete(string TimeText, int TimeInSeconds, float RatePerHour, float TotalFees)
        {
            RaiseOnTableComplete(new TableCompletedEventArgs(TimeText, TimeInSeconds,  RatePerHour,  TotalFees));
        }

        protected virtual void RaiseOnTableComplete(TableCompletedEventArgs e)
        {
            OnTableComplete?.Invoke(this, e);
        }


        int _Seconds;
        
        // Private data member that backs the EndColor property.  
        private string _TableTitle = "Table";
       

        // The Category attribute tells the designer to display  
        // it in the Flash grouping.
        // The Description attribute provides a description of  
        // the property.
        [
        Category("Pool Config"),
        Description("The table Name.")
        ]
        // The public property EndColor accesses endColor.  
        public string TableTitle
        {
            get
            {
                return _TableTitle;
            }
            set
            {
                _TableTitle = value;
                
                grpTable.Text = value;

                // The Invalidate method calls the OnPaint method, which redraws
                // the control.  
                Invalidate();
            }
        }


        private string _TablePlayer = "Player";
        [
        Category("Pool Config"),
        Description("The Player Name.")
        ]
        
        public string TablePlayer
        {
            get
            {
                return _TablePlayer;
            }
            set
            {
                _TablePlayer = value;

                lblName.Text = value;

                // The Invalidate method calls the OnPaint method, which redraws
                // the control.  
                Invalidate();
            }
        }


        private float _HourlyRate= 10.00F;

        [
        Category("Pool Config"),
        Description("Rate Per Hour.")
        ]
        public float HourlyRate
        {
            get
            {
                return _HourlyRate;
            }
            set
            {
                _HourlyRate = value;

            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            

            if (btnStartStop.Text == "Start")
            {
                grpTable.BackColor = Color.Green;
                btnStartStop.Text = "Stop";
                timer1.Start();
            }
            else
            {
              
                grpTable.BackColor = Color.DimGray;
                btnStartStop.Text = "Start";
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _Seconds++;

            TimeSpan time = TimeSpan.FromSeconds(_Seconds);
            string str = time.ToString(@"hh\:mm\:ss");
            lblTime.Text = str;
            lblTime.Refresh();
        }

        private void PoolTable_Load(object sender, EventArgs e)
        {
            grpTable.Text = _TableTitle;
            lblName.Text=_TablePlayer;

        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            grpTable.BackColor = Color.Black;

            timer1.Stop();
            float TotalFees = ((float)_Seconds / 60 / 60) * _HourlyRate;
            RaiseOnTableComplete(lblTime.Text , _Seconds, _HourlyRate, TotalFees) ;
            grpTable.Text = "Table";
            lblName.Text = "Player";
            lblTime.Text = "00:00:00";
            btnStartStop.Text = "Start";
            _Seconds = 0;


        }

     

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            lblName.Text = toolStripTextBox1.Text;
        }
    }
}
