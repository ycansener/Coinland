using Coinland.App.Console.Domain.Entities;
using Coinland.App.Console.Domain.Services;
using Coinland.App.Console.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Coinland.App.Console
{
    public partial class Form1 : Form
    {
        List<CurrencyInfoModel> result;
        DateTime latestUpdate;
        int sortIndex;
        int selectedIndex;
        bool ascending;
        bool isOnline;
        bool isRefreshing;
        public Form1()
        {
            InitializeComponent();
            this.Icon = Resources.coin_stack;
            result = null;
            latestUpdate = DateTime.MinValue;
            sortIndex = 1;
            selectedIndex = 0;
            ascending = true;
            isOnline = true;
            isRefreshing = false;
        }

        public void RefreshData()
        {
            DateTime currentTime = DateTime.Now;
            double difference = (currentTime - latestUpdate).TotalSeconds;
            if ((result == null || difference > 20) && !isRefreshing)
            {
                BindData();
                latestUpdate = currentTime;
            }
        }

        public void ForceRefreshData()
        {
            BindData();
        }

        public string GetTitle()
        {
            string title = "CoinLAND - " + (isOnline ? "Online" : "Offline") + " " + (isRefreshing ? "(Refreshing...)" : string.Empty);
            return title;
        }

        public void UpdateTitle()
        {
            this.Text = GetTitle();
        }

        public void BindData()
        {
            List<CurrencyInfoModel> items = null;
            isRefreshing = true;
            UpdateTitle();
            // TODO: Filtre seçenekleri zenginleştirilecek
            items = new CurrencyInfoService().GetCurrencyInfo(null, 250, "TRY");

            result = items;
            dataGridViewCurrencies.DataSource = result;
            SortData();
            dataGridViewCurrencies.Rows[selectedIndex].Selected = true;
            isRefreshing = false;
            UpdateTitle();
        }

        public void AddUpDownImages()
        {
            if (dataGridViewCurrencies.Columns["Status"] == null)
            {
                DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
                iconColumn.Name = "Status";
                iconColumn.HeaderText = "#";
                dataGridViewCurrencies.Columns.Insert(0, iconColumn);
            }

            foreach (DataGridViewRow row in dataGridViewCurrencies.Rows)
            {
                decimal? percentChangeNullable = (decimal?)row.Cells["PercentChange_1h"].Value;
                decimal percentChange = percentChangeNullable.HasValue ? percentChangeNullable.Value : 0;

                if (percentChange > 0)
                {
                    row.Cells["Status"].Value = Resources.arrow_up;
                }
                else if (percentChange < 0)
                {
                    row.Cells["Status"].Value = Resources.arrow_down;
                }
                else
                {
                    row.Cells["Status"].Value = Resources.arrow_right;
                }
            }
        }

        public void SortData()
        {
            string selectedColumn = dataGridViewCurrencies.Columns[sortIndex].Name;

            if (ascending)
            {
                result = result.OrderBy(p => p.GetType().GetProperty(selectedColumn).GetValue(p, null)).ToList();
            }
            else
            {
                result = result.OrderByDescending(p => p.GetType().GetProperty(selectedColumn).GetValue(p, null)).ToList();
            }

            dataGridViewCurrencies.DataSource = result;
            AddUpDownImages();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshData();
            timerRefreshData.Start();
        }

        private void dataGridViewCurrencies_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseOverRow = dataGridViewCurrencies.SelectedRows[0].Index;
            selectedIndex = currentMouseOverRow;

            if (e.Button == MouseButtons.Right)
            {
                ContextMenu contextMenu = new ContextMenu();
                MenuItem refreshDataMenuItem = new MenuItem("Refresh");
                refreshDataMenuItem.Click += RefreshDataMenuItem_Click;
                contextMenu.MenuItems.Add(refreshDataMenuItem);

                if (isOnline)
                {
                    MenuItem stopTimerMenuItem = new MenuItem("Go Offline!");
                    stopTimerMenuItem.Click += StopTimerMenuItem_Click;
                    contextMenu.MenuItems.Add(stopTimerMenuItem);
                }
                else
                {
                    MenuItem startTimerMenuItem = new MenuItem("Go Online!");
                    startTimerMenuItem.Click += StartTimerMenuItem_Click;
                    contextMenu.MenuItems.Add(startTimerMenuItem);
                }

                //int currentMouseOverRow = dataGridViewCurrencies.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    string currencyName = dataGridViewCurrencies.Rows[currentMouseOverRow].Cells[1].Value.ToString();

                    contextMenu.MenuItems.Add(new MenuItem(string.Format("Follow: {0}", currencyName.ToString())));
                }

                MenuItem propertiesMenuItem = new MenuItem("Properties");
                propertiesMenuItem.Click += PropertiesMenuItem_Click;
                contextMenu.MenuItems.Add(propertiesMenuItem);

                contextMenu.Show(dataGridViewCurrencies, new Point(e.X, e.Y));
            }
        }

        private void PropertiesMenuItem_Click(object sender, EventArgs e)
        {
            new FormProperties().ShowDialog();
        }

        private void StartTimerMenuItem_Click(object sender, EventArgs e)
        {
            timerRefreshData.Start();
            isOnline = true;
            UpdateTitle();
        }

        private void StopTimerMenuItem_Click(object sender, EventArgs e)
        {
            timerRefreshData.Stop();
            isOnline = false;
            UpdateTitle();
        }

        private void RefreshDataMenuItem_Click(object sender, EventArgs e)
        {
            ForceRefreshData();
        }

        private void timerRefreshData_Tick(object sender, EventArgs e)
        {
            //Thread t = new Thread(new ThreadStart(RefreshData));
            //t.Start();
            RefreshData();
        }

        private void dataGridViewCurrencies_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int currentIndex = e.ColumnIndex;

            if (currentIndex == 0)
            {
                currentIndex = dataGridViewCurrencies.Columns["PercentChange_1h"].Index;
            }

            if (sortIndex == currentIndex)
            {
                ascending = !ascending;
            }
            else
            {
                sortIndex = currentIndex;
                ascending = true;
            }
            SortData();
        }
    }
}
