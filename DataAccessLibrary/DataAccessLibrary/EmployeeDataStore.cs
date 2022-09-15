using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccessLibrary
{
    public class EmployeeDataStore
    {
        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader reader;

        public EmployeeDataStore(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
        }

        Employee employee = new Employee();

        //write a method which returns all employees record
        //GetAllEmps

        #region GetAllEmpoyee Details

        public List<Employee> GetAllEmployees()


        {
            try
            {
                string sql = "select EmpNo, Ename,HireDate,Sal from emp";
                command = new MySqlCommand(sql, connection);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                reader = command.ExecuteReader();

                List<Employee> empList = new List<Employee>();

                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.EmpNo = (int)reader["EmpNo"];
                    employee.EName = reader["Ename"].ToString();
                    employee.HireDate = reader["HireDate"] as DateTime?;
                    employee.Sal = reader["sal"] as decimal?;
                    

                    empList.Add(employee);

                    //add this object on empListcollection
                }
                return empList;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        #endregion


        #region Search Employeebynumber
        public Employee GetEmployeeByNumber(int empno)
        {
            try
            {
                string sql = "select EmpNo, Ename,HireDate,Sal from emp where Empno=@eno";
                command = new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue("eno", empno);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                reader = command.ExecuteReader();

                Employee employee = null;

                while (reader.Read())
                {
                    employee = new Employee();
                    employee.EmpNo = (int)reader["EmpNo"];
                    employee.EName = reader["Ename"].ToString();
                    employee.HireDate = reader["HireDate"] as DateTime?;
                    employee.Sal = reader["sal"] as decimal?;


                    

                    //add this object on empListcollection
                }
                return employee;
              

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
        #endregion

        #region Insert Employee Data

        public int InsertEmployee(Employee employee)
        {
            try
            {
                string sql = "Insert into emp (Empno, Ename,HireDate,Sal) values (@eno,@ename,@hdate,@sal)";

                command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("eno", employee.EmpNo);
                command.Parameters.AddWithValue("ename", employee.EName);
                command.Parameters.AddWithValue("hdate", employee.HireDate);
                command.Parameters.AddWithValue("sal", employee.Sal);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                int count = command.ExecuteNonQuery();
                return count;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            

        }




        #endregion

        #region Delete Employee Record
        public int DeleteEmployee(int empno)
        {
            try
            {
                string sql = "Delete from emp where empno = @eno";
                command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("eno", empno);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                int count = command.ExecuteNonQuery();
                return count;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        #endregion

        #region Update Employee data
        public int UpdateEmployee(Employee employee)
        {
            try
            {
                string sql = "Update emp set ename = @ename, hiredate = @hdate, sal=@sal where empno = @eno";
                command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("eno", employee.EmpNo);
                command.Parameters.AddWithValue("ename", employee.EName);
                command.Parameters.AddWithValue("hdate", employee.HireDate);
                command.Parameters.AddWithValue("sal", employee.Sal);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                int count = command.ExecuteNonQuery();
                return count;
            }
            catch (Exception)
            {

                throw;
            }
            
           
        }
        #endregion
    }
}
