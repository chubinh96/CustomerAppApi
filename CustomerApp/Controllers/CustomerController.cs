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
                new Customer{id = 7, name = "Milax", country = "South America", phone = 09093293},
                new Customer{id = 3, name = "Micheal", country = "Singapore", phone = 095433231},
                new Customer{id = 2, name = "Justin", country = "Japan", phone = 09324131},
                new Customer{id = 4, name = "Phoenix", country = "China", phone = 091242313},
                new Customer{id = 6, name = "Scable", country = "Korea", phone = 09100302},
                new Customer{id = 5, name = "Alens", country = "America", phone = 09887891},
        };

        // GET: api/Customer
        [HttpGet("[action]")]
        public IEnumerable<Customer> GetCustomer()
        {
            return customers;
        }

        // GET api/Customer/SearchBy/Japan
        [HttpGet("[action]/{searchBy}/{value}")]
        public IEnumerable<Customer> GetValueSearch(string searchBy, string value)
        {
            List<Customer> resultCustomerSearch = new List<Customer>();
            if (searchBy == "name")
            {
                for (int i = 0; i < customers.Count(); i++)
                {
                    if (customers.Any(s => customers[i].name.ToLower().Contains(value.ToLower())))
                    {
                        resultCustomerSearch.Add(customers[i]);
                    }
                }
            }
            if (searchBy == "country")
            {
                for (int i = 0; i < customers.Count(); i++)
                {
                    if (customers.Any(s => customers[i].country.ToLower().Contains(value.ToLower())))
                    {
                        resultCustomerSearch.Add(customers[i]);
                    }
                }
            }
            return resultCustomerSearch;
        }

        // GET api/Customer/5
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

        // POST api/Customer
        [HttpPost]
        public IEnumerable<Customer> Post([FromBody]Customer customer)
        { 
            customers.Add(customer);
            return customers;
        }

        // PUT api/Customer/5
        [HttpPut("{id}")]
        public IEnumerable<Customer> Put(int id, [FromBody]Customer customer)
        {
            for (int i = 0; i < customers.Count(); i++)
            {
                if (customers[i].id == id)
                {
                    customers[i].id = customer.id;
                    customers[i].name = customer.name;
                    customers[i].country = customer.country;
                    customers[i].phone = customer.phone;
                }
            }
            return customers;
        }

        // DELETE api/Customer/5
        [HttpDelete("{id}")]
        public IEnumerable<Customer> Delete(int id)
        {
            Customer customerDeleted = new Customer();
            for (int i = 0; i < customers.Count(); i++)
            {
                if (customers[i].id == id)
                {
                    customers.RemoveAt(i);
                    break;
                }
            }

            return customers;
        }

        //Sort api/Customer/SortCustomer/id/1
        [HttpGet("[action]/{sortBy}/{sortByValue}")]
        public IEnumerable<Customer> SortCustomer(string sortBy, int sortByValue)
        {

            if (sortBy == "id")
            {
                if (sortByValue == 1)
                {
                    customers = customers.OrderBy(item => item.id).ToList();
                }
                else
                {
                    customers = customers.OrderByDescending(item => item.id).ToList();

                }
            }
            if (sortBy == "name")
            {
                if (sortByValue == 1)
                {
                    customers = customers.OrderBy(item => item.name).ToList();
                }
                else
                {
                    customers = customers.OrderByDescending(item => item.name).ToList();

                }
            }
            if (sortBy == "country")
            {
                if (sortByValue == 1)
                {
                    customers = customers.OrderBy(item => item.country).ToList();
                }
                else
                {
                    customers = customers.OrderByDescending(item => item.country).ToList();

                }
            }
            return customers;

        }

    }
}
