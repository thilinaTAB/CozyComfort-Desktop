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
using System.Xml.Linq;
using System.Configuration;


namespace CozyComfort_Desktop
{
    public partial class BlanketModel : Form
    {
        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            string apiKey = ConfigurationManager.AppSettings["CozyComfortManufacturerKey"];
            if (!string.IsNullOrEmpty(apiKey))
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            }
            client.BaseAddress = new Uri("https://localhost:7175/"); // Ensure this matches your API's URL
            return client;
        }

        private void ClearForm()
        {
            lblID.Text = "";
            txtModelName.Text = "";
            txtModelDes.Text = "";
            txtStock.Text = "";
            txtPrice.Text = "";
            cbMaterial.SelectedIndex = -1;
            lblMaterialDes.Text = "";
        }
        public BlanketModel()
        {
            InitializeComponent();
            cbMaterial.SelectedIndexChanged += cbMaterial_SelectedIndexChanged;

        }

        

        private void BlanketModel_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadMaterials();
        }

        private void LoadData()
        {
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    var response = client.GetAsync("api/BlanketModel").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var read = response.Content.ReadAsStringAsync().Result;
                        dgvModel.DataSource = null;
                        dgvModel.DataSource = (new JavaScriptSerializer()).Deserialize<List<Item>>(read);
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to load models. Status: {response.StatusCode}\nDetails: {errorContent}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvModel.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading models: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvModel.DataSource = null;
                }
            }
        }

        private void LoadMaterials()
        {
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    var response = client.GetAsync("api/Material").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var read = response.Content.ReadAsStringAsync().Result;
                        var materials = (new JavaScriptSerializer()).Deserialize<List<Material>>(read);

                        cbMaterial.DataSource = materials;
                        cbMaterial.DisplayMember = "MaterialName";
                        cbMaterial.ValueMember = "MaterialID";
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to load materials. Status: {response.StatusCode}\nDetails: {errorContent}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading materials: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaterial.SelectedItem is Material selectedMaterial)
            {
                lblMaterialDes.Text = selectedMaterial.Description;
            }
        }

        private void dgvModel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int column = e.ColumnIndex;
            int row = e.RowIndex;
            if (column == 0 && row >= 0)
            {
                lblID.Text = dgvModel.Rows[row].Cells[1].Value.ToString();
                txtModelName.Text = dgvModel.Rows[row].Cells[2].Value.ToString();
                txtModelDes.Text = dgvModel.Rows[row].Cells[3].Value.ToString();
                txtStock.Text = dgvModel.Rows[row].Cells[4].Value.ToString();
                txtPrice.Text = dgvModel.Rows[row].Cells[5].Value.ToString();
                cbMaterial.Text = dgvModel.Rows[row].Cells[7].Value.ToString();

            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblID.Text))
            {
                MessageBox.Show("Please select a model to update.", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(lblID.Text, out int modelId))
            {
                MessageBox.Show("Invalid Model ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (HttpClient client = GetHttpClient())
            {
                string url = "https://localhost:7175/api/BlanketModel/" + modelId;

                Item itemToUpdate = new Item();
                itemToUpdate.ModelID = modelId;
                itemToUpdate.ModelName = txtModelName.Text;
                itemToUpdate.Description = txtModelDes.Text;

                if (!int.TryParse(txtStock.Text, out int stock)) { MessageBox.Show("Invalid Stock value.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                itemToUpdate.Stock = stock;

                if (!decimal.TryParse(txtPrice.Text, out decimal price)) { MessageBox.Show("Invalid Price value.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                itemToUpdate.Price = price;

                if (cbMaterial.SelectedValue == null) { MessageBox.Show("Please select a Material.", "Missing Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                itemToUpdate.MaterialID = (int)cbMaterial.SelectedValue;

                string data = (new JavaScriptSerializer()).Serialize(itemToUpdate);
                var request = new StringContent(data, Encoding.UTF8, "application/json");

                try
                {
                    var response = client.PutAsync(url, request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Model updated successfully!");
                        LoadData();
                        ClearForm();
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to update model. Status: {response.StatusCode}\nDetails: {errorContent}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (HttpClient client = GetHttpClient())
            {
                string url = "https://localhost:7175/api/BlanketModel";

                Item newItem = new Item();
                newItem.ModelName = txtModelName.Text;
                newItem.Description = txtModelDes.Text;

                if (!int.TryParse(txtStock.Text, out int stock)) { MessageBox.Show("Invalid Stock value.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                newItem.Stock = stock;

                if (!decimal.TryParse(txtPrice.Text, out decimal price)) { MessageBox.Show("Invalid Price value.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                newItem.Price = price;

                if (cbMaterial.SelectedValue == null) { MessageBox.Show("Please select a Material.", "Missing Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                newItem.MaterialID = (int)cbMaterial.SelectedValue;

                string data = (new JavaScriptSerializer()).Serialize(newItem);
                var request = new StringContent(data, Encoding.UTF8, "application/json");

                try
                {
                    var response = client.PostAsync(url, request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Model added successfully!");
                        LoadData();
                        ClearForm();
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to add model. Status: {response.StatusCode}\nDetails: {errorContent}", "Add Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during addition: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblID.Text))
            {
                MessageBox.Show("Please select a model to delete.", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(lblID.Text, out int modelId))
            {
                MessageBox.Show("Invalid Model ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to delete Model ID: {modelId}?",
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
                        string url = "api/BlanketModel/" + modelId;
                        var response = client.DeleteAsync(url).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Model deleted successfully!");
                            LoadData();
                            ClearForm();
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            MessageBox.Show("Model not found on the server.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string errorContent = response.Content.ReadAsStringAsync().Result;
                            MessageBox.Show($"Failed to delete model. Status: {response.StatusCode}\nDetails: {errorContent}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred during deletion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Please input a Model ID", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtID.Text, out int modelID))
            {
                MessageBox.Show("Invalid Model ID. Please enter a number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string url = "https://localhost:7175/api/BlanketModel/" + modelID;
                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var read = response.Content.ReadAsStringAsync().Result;
                        Item foundItem = (new JavaScriptSerializer()).Deserialize<Item>(read);

                        dgvModel.DataSource = null;
                        List<Item> items = new List<Item> { foundItem };
                        dgvModel.DataSource = items;

                        if (dgvModel.Rows.Count > 0)
                        {
                            dgvModel.Rows[0].Selected = true;
                        }
                        ClearForm();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("No blanket model found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvModel.DataSource = null;
                        ClearForm();
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to retrieve model. Status: {response.StatusCode}\nDetails: {errorContent}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvModel.DataSource = null;
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while finding the model: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvModel.DataSource = null;
                    ClearForm();
                }
            }

        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }
    }
}
