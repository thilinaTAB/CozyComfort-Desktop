using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Configuration;
using System.Threading.Tasks;

namespace CozyComfort_Desktop
{
    public partial class OrderView : Form
    {
        private readonly HttpClient _client;
        private const string BaseApiUrl = "https://localhost:7175/";
        private const string ManufacturerId = "1";

        public OrderView()
        {
            InitializeComponent();
            _client = GetHttpClient();
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            string apiKey = ConfigurationManager.AppSettings["CozyComfortManufacturerKey"];
            if (!string.IsNullOrEmpty(apiKey))
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            }
            client.BaseAddress = new Uri(BaseApiUrl);
            return client;
        }

        private async void Oder_Load(object sender, EventArgs e)
        {
            await LoadOrdersAsync();
        }

        private async Task LoadOrdersAsync()
        {
            try
            {
                var response = await _client.GetAsync($"api/Order/{ManufacturerId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var orders = new JavaScriptSerializer().Deserialize<List<OrderItem>>(json);
                    dgvOrder.DataSource = orders;
                }
                else
                {
                    MessageBox.Show("Failed to load orders. Status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvOrder.Rows[e.RowIndex];

                lblID.Text = row.Cells["OrderID"].Value?.ToString();
                lblDate.Text = Convert.ToDateTime(row.Cells["OrderDate"].Value).ToShortDateString();
                lblModel.Text = row.Cells["ModelName"].Value?.ToString();
                lblQty.Text = row.Cells["Quantity"].Value?.ToString();
                lblUnit.Text = row.Cells["Price"].Value?.ToString();
                label8.Text = row.Cells["Total"].Value?.ToString();
                lblStatus.Text = row.Cells["Status"].Value?.ToString();
            }
        }

        private async Task UpdateStatusAsync(string newStatus)
        {
            if (string.IsNullOrEmpty(lblID.Text))
            {
                MessageBox.Show("Please select an order first.");
                return;
            }

            if (!int.TryParse(lblID.Text, out int orderId))
            {
                MessageBox.Show("Invalid Order ID.");
                return;
            }

            try
            {
                var url = $"api/Order/{orderId}?status={newStatus}";
                var response = await _client.PutAsync(url, null);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Order {orderId} marked as {newStatus} successfully.");
                    await LoadOrdersAsync();
                }
                else
                {
                    string reason = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to update status. Status code: {response.StatusCode}. Reason: {reason}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating status: " + ex.Message);
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            await UpdateStatusAsync("Accepted");
        }

        private async void btnReject_Click(object sender, EventArgs e)
        {
            await UpdateStatusAsync("Rejected");
        }
    }
}
