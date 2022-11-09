namespace resolution_switcher
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddName_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameInput.Text) &&
                !NamesList.Items.Contains(NameInput.Text))
            {
                NamesList.Items.Add(NameInput.Text);
            }
        }
    }
}