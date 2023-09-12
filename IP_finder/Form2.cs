using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IPMatchingApp
{
    public partial class MainForm : Form
    {
        private readonly IIPMatcher ipMatcher;

        public MainForm()
        {
            InitializeComponent();
            this.ipMatcher = new IPMatcher();

            // Subscribe to the Load event of the form
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Suspend layout updates
            this.SuspendLayout();

            // Initialize controls and perform other setup tasks here

            // Resume layout updates
            this.ResumeLayout();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Store the selected file path
                    string filePath = openFileDialog.FileName;
                    // Display the file path to the user
                    textBox1.Text = filePath;
                }
            }
        }

        private void matchButton_Click(object sender, EventArgs e)
        {
            string filePath = textBox1.Text; // Get the selected file path
            string matchingMethod = comboBox1.SelectedItem.ToString(); // Get the selected matching method

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(matchingMethod))
            {
                // Handle missing file or method selection
                MessageBox.Show("Please select a file and a matching method.");
                return;
            }

            // Use the injected IPMatcher
            List<MatchResult> results = ipMatcher.Match(filePath, comboBox1.Text);

            // Display the results in the DataGridView
            dataGridView1.Rows.Clear();
            foreach (var result in results)
            {
                dataGridView1.Rows.Add(result.IPAddress, result.Country);
            }
        }
    }
}


