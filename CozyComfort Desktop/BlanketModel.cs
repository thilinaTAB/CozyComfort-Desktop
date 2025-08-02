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


namespace CozyComfort_Desktop
{
    public partial class BlanketModel : Form
    {
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
            string url = "https://localhost:7175/api/BlanketModel";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var read = response.Content.ReadAsStringAsync();
                read.Wait();
                dgvModel.DataSource = null;
                dgvModel.DataSource = (new JavaScriptSerializer()).Deserialize<List<Item>>(read.Result);
            }
        }

        private void LoadMaterials()
        {
            string url = "https://localhost:7175/api/Material";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var read = response.Content.ReadAsStringAsync();
                read.Wait();

                var materials = (new JavaScriptSerializer()).Deserialize<List<Material>>(read.Result);

                cbMaterial.DataSource = materials;
                cbMaterial.DisplayMember = "MaterialName";
                cbMaterial.ValueMember = "MaterialID";
            }
            else
            {
                MessageBox.Show("Failed to load materials");
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

            string url = "https://localhost:7175/api/BlanketModel/" + modelId;
            HttpClient client = new HttpClient();
            Item itemToUpdate = new Item(); 
            itemToUpdate.ModelID = modelId;
            itemToUpdate.ModelName = txtModelName.Text;
            itemToUpdate.Description = txtModelDes.Text;

            if (int.TryParse(txtStock.Text, out int stock))
            {
                itemToUpdate.Stock = stock;
            }
            else
            {
                MessageBox.Show("Invalid Stock value.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (decimal.TryParse(txtPrice.Text, out decimal price))
            {
                itemToUpdate.Price = price;
            }
            else
            {
                MessageBox.Show("Invalid Price value.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbMaterial.SelectedValue != null)
            {
                itemToUpdate.MaterialID = (int)cbMaterial.SelectedValue;
            }
            else
            {
                MessageBox.Show("Please select a Material.", "Missing Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string data = (new JavaScriptSerializer()).Serialize(itemToUpdate);
            var request = new StringContent(data, Encoding.UTF8, "application/json");

            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update?", "Confirm Update",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:7175/api/BlanketModel";
            HttpClient client = new HttpClient();
            Item itemToAdd = new Item();
            itemToAdd.ModelName = txtModelName.Text;
            itemToAdd.Description = txtModelDes.Text;


            if (string.IsNullOrWhiteSpace(txtModelName.Text))
            {
                MessageBox.Show("Please input a Model Name", "Model Name Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (int.TryParse(txtStock.Text, out int stock))
            {
                itemToAdd.Stock = stock;
            }
            else
            {
                MessageBox.Show("Invalid Stock value.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (decimal.TryParse(txtPrice.Text, out decimal price))
            {
                itemToAdd.Price = price;
            }
            else
            {
                MessageBox.Show("Invalid Price value.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbMaterial.SelectedValue != null)
            {
                itemToAdd.MaterialID = (int)cbMaterial.SelectedValue;
            }
            else
            {
                MessageBox.Show("Please select a Material.", "Missing Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string data = (new JavaScriptSerializer()).Serialize(itemToAdd);
            var request = new StringContent(data, Encoding.UTF8, "application/json");

            try
            {         
                    var response = client.PostAsync(url, request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Blanket Model Added successfully!");
                        LoadData();
                        ClearForm();
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to Add Blanket model. Status: {response.StatusCode}\nDetails: {errorContent}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during Add: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            string url = "https://localhost:7175/api/BlanketModel/" + modelId;
            HttpClient client = new HttpClient();
            Item itemToDelete = new Item();

            string data = (new JavaScriptSerializer()).Serialize(itemToDelete);

            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    var response = client.DeleteAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Blanket Model Deleted successfully!");
                        LoadData();
                        ClearForm();
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to delete model. Status: {response.StatusCode}\nDetails: {errorContent}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during delete: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Please input a Model ID", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String modelID = txtID.Text;
            string url = "https://localhost:7175/api/BlanketModel/"+modelID;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var read = response.Content.ReadAsStringAsync();
                read.Wait();
                dgvModel.DataSource = null;
                Item item1 = (new JavaScriptSerializer()).Deserialize<Item>(read.Result);
                List<Item> items = new List<Item>();
                items.Add(item1);
                dgvModel.DataSource = items;

                ClearForm();

            }
            else
            {
                dgvModel.DataSource = null;
                MessageBox.Show("No blanket model found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
                return;
            }

        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }
    }
}
