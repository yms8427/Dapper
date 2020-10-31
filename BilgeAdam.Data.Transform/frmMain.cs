using BilgeAdam.Data.Transform.Managers;
using BilgeAdam.Data.Transform.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgeAdam.Data.Transform
{
    public partial class frmMain : FormBase
    {
        public frmMain()
        {
            InitializeComponent();
            Manager = Resolver.Get<IDataManager>();
        }
        private IDataManager Manager { get; set; }
        private void frmMain_Load(object sender, EventArgs e)
        {
            var query = @"SELECT c.CompanyName, o.OrderDate, 
                                 p.ProductName, od.UnitPrice AS OrderPrice, 
	                             p.UnitPrice, od.Quantity,
	                             od.UnitPrice* od.Quantity AS Summary
                          FROM Customers c
                          INNER JOIN Orders o ON o.CustomerID = c.CustomerID
                          INNER JOIN[Order Details] od ON od.OrderID = o.OrderID
                          INNER JOIN Products p ON p.ProductID = od.ProductID
                          WHERE c.CustomerID = 'BOLID'";

            var q = @"SELECT 
                      EmployeeID AS Id, FirstName + ' ' + LastName AS FullName,
                      DATEDIFF(YEAR, BirthDate, GETDATE()) - 30 AS Age 
                      FROM Employees";
            
            dgvResult.DataSource = Manager.Load<OrderHistoryDto>(query).ToList();
            dgvOther.DataSource = Manager.Load<EmployeeInfo>(q).ToList();
        }

        private IEnumerable<string> ExtractNames(IEnumerable<CustomerIdInfo> info)
        {
            foreach (var i in info)
            {
                yield return i.Id;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var autoComplete = new AutoCompleteStringCollection();
            var autoCompleteQuery = "SELECT CustomerId AS Id FROM Customers WHERE CustomerId LIKE @name";
            var customers = ExtractNames(Manager.Load<CustomerIdInfo>(autoCompleteQuery, new SqlParameter("@name", txtSearch.Text + "%"))).ToArray();
            autoComplete.AddRange(customers);
            txtSearch.AutoCompleteCustomSource = autoComplete;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var query = @"SELECT c.CompanyName, o.OrderDate, 
                                 p.ProductName, od.UnitPrice AS OrderPrice, 
	                             p.UnitPrice, od.Quantity,
	                             od.UnitPrice* od.Quantity AS Summary
                          FROM Customers c
                          INNER JOIN Orders o ON o.CustomerID = c.CustomerID
                          INNER JOIN[Order Details] od ON od.OrderID = o.OrderID
                          INNER JOIN Products p ON p.ProductID = od.ProductID
                          WHERE c.CustomerID = @name";
                dgvResult.DataSource = null;
                dgvResult.DataSource = Manager.Load<OrderHistoryDto>(query, new SqlParameter("@name", txtSearch.Text)).ToList();
            }
        }
    }
}
