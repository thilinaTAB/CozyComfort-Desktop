using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Configuration;
using CozyComfort_Desktop;

namespace CozyComfort_Desktop
{
    public partial class ManageMaterial : Form
    {
        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            // This key is used for Manufacturer access (CRUD)
            string apiKey = ConfigurationManager.AppSettings["CozyComfortManufacturerKey"];
            if (!string.IsNullOrEmpty(apiKey))
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            }
            client.BaseAddress = new Uri("https://localhost:7175/");
            return client;
        }

        private void ClearForm()
        {
            lblMaterialID.Text = "";
            txtMaterialName.Text = "";
            txtMaterialDes.Text = "";
            txtMaterialID.Text = "";
        }
        public ManageMaterial()
        {
            InitializeComponent();
            this.Load += new EventHandler(ManageMaterial_Load);
        }

        private void LoadData()
        {
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    var response = client.GetAsync("api/Material").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var read = response.Content.ReadAsStringAsync().Result;
                        dgvMaterial.DataSource = null;
                        dgvMaterial.DataSource = (new JavaScriptSerializer()).Deserialize<List<Material>>(read);
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to load materials. Status: {response.StatusCode}\nDetails: {errorContent}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvMaterial.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading materials: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvMaterial.DataSource = null;
                }
            }
        }

        private void ManageMaterial_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvMaterial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int column = e.ColumnIndex;
            int row = e.RowIndex;
            if (column == 0 && row >= 0)
            {
                lblMaterialID.Text = dgvMaterial.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? ""; 
                txtMaterialName.Text = dgvMaterial.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";
                txtMaterialDes.Text = dgvMaterial.Rows[e.RowIndex].Cells[3].Value?.ToString() ?? "";
            }
        }

        private void btnMaterialUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblMaterialID.Text))
            {
                MessageBox.Show("Please select a material to update.", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(lblMaterialID.Text, out int materialId))
            {
                MessageBox.Show("Invalid Material ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMaterialName.Text) || string.IsNullOrWhiteSpace(txtMaterialDes.Text))
            {
                MessageBox.Show("Material Name and Description cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (HttpClient client = GetHttpClient())
            {
                string url = "https://localhost:7175/api/Material/" + materialId;

                Material materialToUpdate = new Material
                {
                    MaterialID = materialId,
                    MaterialName = txtMaterialName.Text,
                    Description = txtMaterialDes.Text
                };

                string data = (new JavaScriptSerializer()).Serialize(materialToUpdate);
                var request = new StringContent(data, Encoding.UTF8, "application/json");

                try
                {
                    var response = client.PutAsync(url, request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Material updated successfully!");
                        LoadData();
                        ClearForm();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("Material not found on the server.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to update material. Status: {response.StatusCode}\nDetails: {errorContent}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMaterialAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaterialName.Text) || string.IsNullOrWhiteSpace(txtMaterialDes.Text))
            {
                MessageBox.Show("Material Name and Description cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (HttpClient client = GetHttpClient())
            {
                string url = "https://localhost:7175/api/Material";

                Material newMaterial = new Material
                {
                    MaterialName = txtMaterialName.Text,
                    Description = txtMaterialDes.Text
                };

                string data = (new JavaScriptSerializer()).Serialize(newMaterial);
                var request = new StringContent(data, Encoding.UTF8, "application/json");

                try
                {
                    var response = client.PostAsync(url, request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Material added successfully!");
                        LoadData();
                        ClearForm();
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to add material. Status: {response.StatusCode}\nDetails: {errorContent}", "Add Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during addition: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMaterialDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblMaterialID.Text))
            {
                MessageBox.Show("Please select a material to delete.", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(lblMaterialID.Text, out int materialId))
            {
                MessageBox.Show("Invalid Material ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to delete Material ID: {materialId}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                using (HttpClient client = GetHttpClient())
                {
                    try
                    {
                        string url = "https://localhost:7175/api/Material/" + materialId;
                        var response = client.DeleteAsync(url).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Material deleted successfully!");
                            LoadData();
                            ClearForm();
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            MessageBox.Show("Material not found on the server.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string errorContent = response.Content.ReadAsStringAsync().Result;
                            MessageBox.Show($"Failed to delete material. Status: {response.StatusCode}\nDetails: {errorContent}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred during deletion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnMaterialFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaterialID.Text))
            {
                MessageBox.Show("Please input a Material ID to find.", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtMaterialID.Text, out int materialId))
            {
                MessageBox.Show("Invalid Material ID. Please enter a number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string url = "https://localhost:7175/api/Material/" + materialId;
                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var read = response.Content.ReadAsStringAsync().Result;
                        Material foundMaterial = (new JavaScriptSerializer()).Deserialize<Material>(read);

                        dgvMaterial.DataSource = null; 
                        List<Material> materials = new List<Material> { foundMaterial }; 
                        dgvMaterial.DataSource = materials;

                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("No material found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvMaterial.DataSource = null;
                        ClearForm();
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to retrieve material. Status: {response.StatusCode}\nDetails: {errorContent}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvMaterial.DataSource = null;
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while finding the material: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvMaterial.DataSource = null;
                    ClearForm();
                }
            }
        }

        private void btnMaterialViewAll_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }
    }
}
