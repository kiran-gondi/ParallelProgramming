using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public async Task<int> CalculateValueAsync()
        {
            await Task.Delay(5000);
            return 123;
        }

        protected async void Unnamed1_Click(object sender, EventArgs e)
        {
            int value = await CalculateValueAsync();
            lblResult.Text = value.ToString();

            await Task.Delay(5000);

            using (var wc = new WebClient())
            {
                string data = await wc.DownloadStringTaskAsync(new Uri("http://google.com/robots.txt"));
                lblResult.Text = data.Split('\n')[0].Trim();
            }


        }
    }
}