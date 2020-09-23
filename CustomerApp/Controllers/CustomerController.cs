using System;
using System.Collections.Generic;
using System.Linq;
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
        //private IEnumerable<Customer> customerss;

        // GET: api/<controller>
        [HttpGet("[action]")]
        public IEnumerable<Customer> GetCustomer()
        {
            //return JsonConvert.SerializeObject(customers);
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
            //Customer customerss = new Customer() { id = 8, name = "binh", country = "vn", phone = 434324 }; 
            Customer customerss = customer;
            customers.Add(customerss);
            List<Customer> cs = new List<Customer>();
            cs.Add(customerss);
            return customers;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            List<Customer> customer = new List<Customer>();

            for (int i = 0; i < customers.Count(); i++)
            {
                if (customers[i].id == id)
                {
                    customers.RemoveAt(i);
                    break;
                }
            }
        }
    }

    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public int phone { get; set; }

    }
}
