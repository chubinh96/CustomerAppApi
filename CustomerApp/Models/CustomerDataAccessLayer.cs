using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CustomerApp.Models
{
    public class CustomerDataAccessLayer
    {
        string connectionString = "Data Source=(local);Initial Catalog=CustomerDB;Integrated Security=True";

        //To View all Customer details
        public IEnumerable<Customer> GetAllCustomer()
        {
            try
            {
                List<Customer> lstcustomer = new List<Customer>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Customer customer = new Customer();

                        customer.id = Convert.ToInt32(rdr["Id"]);
                        customer.name = rdr["Name"].ToString();
                        customer.country = rdr["Country"].ToString();
                        customer.phone = Convert.ToInt32(rdr["Phone"]);

                        lstcustomer.Add(customer);
                    }
                    con.Close();
                }
                return lstcustomer;
            }
            catch
            {
                throw;
            }
        }

        //To Add new Customer record 
        public int AddCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spAddCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", customer.name);
                    cmd.Parameters.AddWithValue("@Country", customer.country);
                    cmd.Parameters.AddWithValue("@Phone", customer.phone);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar customer
        public int UpdateCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", customer.id);
                    cmd.Parameters.AddWithValue("@Name", customer.name);
                    cmd.Parameters.AddWithValue("@Country", customer.country);
                    cmd.Parameters.AddWithValue("@Phone", customer.phone);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular customer
        public Customer GetCustomerData(int id)
        {
            try
            {
                Customer customer = new Customer();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT * FROM customer WHERE id= " + id;
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        customer.id = Convert.ToInt32(rdr["id"]);
                        customer.name = rdr["Name"].ToString();
                        customer.country = rdr["Country"].ToString();
                        customer.phone = Convert.ToInt32(rdr["Phone"]);
                    }
                }
                return customer;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record on a particular customer
        public int DeleteCustomer(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spDeleteCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
}
