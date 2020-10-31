using BilgeAdam.Data.Transform.Helpers;
using System.Windows.Forms;

namespace BilgeAdam.Data.Transform
{
    public class FormBase : Form
    {
        public FormBase()
        {
            Resolver = new DependencyResolver();
        }
        public DependencyResolver Resolver { get; set; }
    }
}