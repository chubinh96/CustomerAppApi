using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerApp.Controllers
{


    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        List<Customer> customers = new List<Customer>{
                new Customer{id = 1, name = "Scotts", country = "Viet Nam", phone = 098765421},
                new Customer{id = 2, name = "Justin", country = "Japan", phone = 09324131},
                new Customer{id = 3, name = "Micheal", country = "Singapore", phone = 095433231},
                new Customer{id = 4, name = "Phoenix", country = "China", phone = 091242313},
                new Customer{id = 5, name = "Alens", country = "America", phone = 09887891},
                new Customer{id = 6, name = "Scable", country = "Korea", phone = 09100302},
                new Customer{id = 7, name = "Milax", country = "South America", phone = 09093293}
        };

        // GET: api/<controller>
        [HttpGet("[action]")]
        public IEnumerable<Customer> GetCustomer()
        {
            return customers;
        }

        // GET api/<controller>/SearchByName/Alex
        [HttpGet("SearchByName/{name}")]
        public string GetByName(string name)
        {
            List<Customer> customerName = new List<Customer>();

            for (int i = 0; i < customers.Count(); i++)
            {
                bool b = customers.Any(s => customers[i].country.Contains(name));
                if (customers.Any(s => customers[i].name.ToLower().Contains(name.ToLower())))
                {
                    customerName.Add(customers[i]);
                }
            }
            string json = JsonConvert.SerializeObject(customerName);
            return json;

        }

        // GET api/<controller>/SearchByCountry/Japan
        [HttpGet("SearchByCountry/{country}")]
        public string GetByCountry(string country)
        {
            List<Customer> customerCountry = new List<Customer>();

            for (int i = 0; i < customers.Count(); i++)
            {
                bool b = customers.Any(s => customers[i].country.Contains(country));
                if (customers.Any(s => customers[i].country.ToLower().Contains(country.ToLower())))
                {
                    customerCountry.Add(customers[i]);
                }
            }
            string json = JsonConvert.SerializeObject(customerCountry);
            return json;

        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            Customer cs = new Customer();
            for (int i = 0; i < customers.Count(); i++)
            {
                if (customers[i].id == id)
                {
                    cs = customers[i];
                    break;
                }
            }
            return cs;
        }

        // POST api/<controller>
        [HttpPost]
        public IEnumerable<Customer> Post([FromBody]Customer customer)
        { 
            customers.Add(customer);
            return customers;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Customer customer)
        {
            
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IEnumerable<Customer> Delete(int id)
        {
            Customer customerDeleted = new Customer();
            for (int i = 0; i < customers.Count(); i++)
            {
                if (customers[i].id == id)
                {
                    customers.RemoveAt(i);
                    //customerDeleted = customers[i];
                    break;
                }
            }

            return customers;
        }

        [HttpGet("[action]")]
        public IEnumerable<Customer> getAllCustomer(int id, string name, string country, int phone)
        {
            var list = new List<Customer>();
            var where = new StringBuilder();
            where.Append("WHERE NAME like '%%'");
            if (!string.IsNullOrWhiteSpace(name))
            {
                where.Append(" AND NAME=@NAME ");
            }
            if (!string.IsNullOrWhiteSpace(country))
            {
                where.Append(" AND COUNTRY=@COUNTRY ");
            }
            var connection = new SqlConnection("Data Source=(local);Initial Catalog=CustomerDB;Integrated Security=True");
            var command = new SqlCommand()
            {
                Connection = connection,
                CommandText = @"SELECT [ID],[NAME],[COUNTRY],[PHONE] FROM [CustomerDB].[dbo].[customer] ",
            };
            if (!string.IsNullOrWhiteSpace(name))
            {
                command.Parameters.Add(new SqlParameter("@NAME", SqlDbType.VarChar));
                command.Parameters["@NAME"].Value = name;
            }
            if (!string.IsNullOrWhiteSpace(country))
            {
                command.Parameters.Add(new SqlParameter("@COUNTRY", SqlDbType.VarChar));
                command.Parameters["@COUNTRY"].Value = country;
            }
            try
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var i = new Customer();
                    i.id = (dynamic)reader["id"];
                    i.name = reader["name"].ToString();
                    i.country = reader["country"].ToString();
                    i.phone = (dynamic)reader["phone"];

                    list.Add(i);
                }

                reader.Close();
            }
            catch { throw; }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }

                command.Dispose();
                connection.Dispose();
            }
            return list;
        }
    }
}
