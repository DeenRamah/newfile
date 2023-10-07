namespace newfile
{
    public partial class Form1 : Form
    {
        private string filePath; // Store the path to the created file
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("File does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Rename the created file
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName = Path.GetFileName(filePath); // Set the current file name

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string newFilePath = saveFileDialog.FileName;

                    try
                    {
                        File.Move(filePath, newFilePath);
                        filePath = newFilePath; // Update the file path to the new name
                        MessageBox.Show($"File renamed to '{Path.GetFileName(newFilePath)}' successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"File renaming failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create and configure the SaveFileDialog
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;

                // Show the SaveFileDialog to get the file path from the user
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    filePath = saveFileDialog.FileName;

                    try
                    {
                        // Create a text file and write some content to it
                        using (StreamWriter sw = new StreamWriter(filePath))
                        {
                            sw.WriteLine("This is a sample text file.");
                            sw.WriteLine("You can add more content here.");
                        }

                        MessageBox.Show($"File '{filePath}' created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"File creation failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("File does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Show a MessageBox with the list of created files in the folder
            string[] files = Directory.GetFiles(Path.GetDirectoryName(filePath));
            string fileList = string.Join("\n", files);
            MessageBox.Show($"Created Files:\n\n{fileList}", "Created Files", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("File does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Open the created file using the default associated program
            try
            {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"File opening failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}