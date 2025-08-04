using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Configuration;
using CozyComfort_Desktop;

namespace CozyComfort_Desktop
{
    public partial class OrderView : Form
    {
        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            string apiKey = ConfigurationManager.AppSettings["CozyComfortManufacturerKey"];
            if (!string.IsNullOrEmpty(apiKey))
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            }
            client.BaseAddress = new Uri("https://localhost:7175/");
            return client;
        }
        public OrderView()
        {
            InitializeComponent();
        }

        private void Oder_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    var response = client.GetAsync("https://localhost:7175/api/Order/1").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        var orders = (new JavaScriptSerializer()).Deserialize<List<OrderItem>>(json);
                        dgvOrder.DataSource = orders;
                    }
                    else
                    {
                        MessageBox.Show("Failed to load orders.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading orders: " + ex.Message);
                }
            }
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                var row = dgvOrder.Rows[e.RowIndex];

                lblID.Text = row.Cells["OrderID"].Value.ToString();
                lblDate.Text = Convert.ToDateTime(row.Cells["OrderDate"].Value).ToShortDateString();
                lblModel.Text = row.Cells["ModelName"].Value.ToString();
                lblQty.Text = row.Cells["Quantity"].Value.ToString();
                lblUnit.Text = row.Cells["Price"].Value.ToString();
                label8.Text = row.Cells["Total"].Value.ToString();
                lblStatus.Text = row.Cells["Status"].Value.ToString();
            }
        }

        private void UpdateStatus(string newStatus)
        {
            if (string.IsNullOrEmpty(lblID.Text))
            {
                MessageBox.Show("Select an order first.");
                return;
            }

            int orderId = int.Parse(lblID.Text);

            using (HttpClient client = GetHttpClient())
            {
                var url = "https://localhost:7175/api/Order/1"+ "/status";
                var data = new JavaScriptSerializer().Serialize(new { Status = newStatus });
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                try
                {
                    var response = client.PutAsync(url, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Order marked as {newStatus}.");
                        LoadOrders();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to update status. {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating status: " + ex.Message);
                }
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            UpdateStatus("Accepted");
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            UpdateStatus("Rejected");
        }

    }
}
