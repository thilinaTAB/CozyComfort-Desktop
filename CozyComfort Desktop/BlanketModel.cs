using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Web.Script.Serialization;


namespace CozyComfort_Desktop
{
    public partial class BlanketModel : Form
    {
        public BlanketModel()
        {
            InitializeComponent();
        }

        

        private void BlanketModel_Load(object sender, EventArgs e)
        {
            LoadData();
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
                cbMaterial.Text = dgvModel.Rows[row].Cells[6].Value.ToString();
                lblMaterialDes.Text = dgvModel.Rows[row].Cells[7].Value.ToString();

            }
        }
    }
}
