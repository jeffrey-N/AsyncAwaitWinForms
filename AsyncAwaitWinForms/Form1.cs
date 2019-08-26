using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwaitWinForms
{
    public partial class FormAsyncAwait : Form
    {
        private readonly SynchronizationContext synchronizationContext;
        private DateTime previousTime = DateTime.Now;
        public FormAsyncAwait()
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
        }
            
        private void FormAsyncAwait_Load(object sender, EventArgs e)
        {

        }

        public void UpdateUI(int value)
        {
            var timeNow = DateTime.Now;

            if ((DateTime.Now - previousTime).Milliseconds <= 50) return;

            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                labelCount.Text = @"Counter " + (int)o;
            }), value);

            previousTime = timeNow;
        }

        private async void ButtonGo_Click(object sender, EventArgs e)
        {
            buttonGo.Enabled = false;
            var count = 0;

            await Task.Run(() =>
            {
                for (var i = 0; i <= 5000000; i++)
                {
                    UpdateUI(i);
                    count = i;
                }
            });

            labelCount.Text = @"Counter " + count;
            buttonGo.Enabled = true;
        }
    }
}
