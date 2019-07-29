using MovieRentalMVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MovieRentalMVCApplication.APIs
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //GET /APIs/Customers
        [HttpGet]
        public IEnumerable<Customer>GetCustomers()
        {
            return _context.Customers.ToList();
        }
        //GET /api/Customers/id
        public Customer GetCustomerById(int? id)
        {
            return _context.Customers.SingleOrDefault(c => c.Id == id);
        }
        [HttpPost]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                    //throw new HttpResponseException(HttpStatusCode.BadRequest);
                BadRequest();
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok(customer);
        }
        [HttpPut]
        public void UpdateCustomer(int id,Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDb==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            customerInDb.Name = customer.Name;
            customerInDb.DOB = customer.DOB;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            _context.SaveChanges();
        }
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
