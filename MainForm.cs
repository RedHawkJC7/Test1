using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkedServerTestConnectivity
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set up the DataGridView
            dataGridView1.Columns.Add("LinkedServer", "Linked Server");
            dataGridView1.Columns.Add("Status", "Status");
        }

        private void btnCheckConnectivity_Click(object sender, EventArgs e)
        {
            // Clear previous results
            dataGridView1.Rows.Clear();

            // Replace "YourServer" with your actual SQL Server instance name
            ServerConnection serverConnection = new ServerConnection(".");

            try
            {
                // Connect to the server
                Server server = new Server(serverConnection);

                // Iterate through linked servers
                foreach (LinkedServer linkedServer in server.LinkedServers)
                {
                    // Check connectivity
                    string status = "Connected";
                    try
                    {
                        linkedServer.TestConnection();
                    }
                    catch (Exception ex)
                    {
                        status = "Error: " + ex.Message;
                    }

                    // Add data to DataGridView
                    dataGridView1.Rows.Add(linkedServer.Name, status);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the connection
                serverConnection.Disconnect();
            }
        }

        
    }
}
