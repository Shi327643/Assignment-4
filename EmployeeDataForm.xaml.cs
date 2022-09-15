using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccessLibrary;
using System.Configuration;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace WPFDemoApp
{
    /// <summary>
    /// Interaction logic for EmployeeDataForm.xaml
    /// </summary>
    public partial class EmployeeDataForm : Window
    {
        EmployeeDataStore datastore;
        ObservableCollection<Employee> empList;
        
        public EmployeeDataForm()
        {
            InitializeComponent();
            datastore = new EmployeeDataStore(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);

        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            List<Employee> employees= datastore.GetAllEmployees();
            empList = new ObservableCollection<Employee>(employees);
            EmpDataGrid.DataContext = empList;

        }

        void ClearTextBoxes()
        {
            txtEmpNo.Clear();
            txtEName.Clear();
            txtHireDate.Clear();
            txtSal.Clear();
        }


        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee employee = new Employee()
                {
                    EmpNo = Convert.ToInt32(txtEmpNo.Text),
                    EName = txtEName.Text,
                    HireDate = Convert.ToDateTime(txtHireDate.Text),
                    Sal = Convert.ToDecimal(txtSal.Text)
                };

                int count = datastore.InsertEmployee(employee);

                if (count == 1)
                {
                    MessageBox.Show("Record Inserted");

                    EmpDataGrid.DataContext= datastore.GetAllEmployees();
                   

                }
                else
                {
                    MessageBox.Show("Insert Fail");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Exception: \n" + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtEmpNo.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter the empno value");
                    return;
                }
                int empno = Convert.ToInt32(txtEmpNo.Text);
                Employee employee = datastore.GetEmployeeByNumber(empno);

                if (employee == null)
                {
                    MessageBox.Show("No Employee found for no" + txtEmpNo.ToString());

                }
                else
                {
                    txtEName.Text = employee.EName;
                    txtHireDate.Text = employee.HireDate.ToString();
                    txtSal.Text = employee.Sal.ToString();
                    
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Exception : \n" + ex.Message);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtEmpNo.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter the empno value");
                    return;
                }
                int empno = Convert.ToInt32(txtEmpNo.Text);
                Employee employee = datastore.GetEmployeeByNumber(empno);

                int count = datastore.DeleteEmployee(empno);

                if (count == 1)
                {
                    MessageBox.Show("Record Deleted");

                    EmpDataGrid.DataContext = datastore.GetAllEmployees();
                    

                }
                else
                {
                    MessageBox.Show("Delete Fail");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Exception : \n" + ex.Message);
            }
        }
    }
}
