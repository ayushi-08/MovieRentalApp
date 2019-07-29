using MovieRentalMVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRentalMVCApplication.ViewModels;
using System.Net.Http;

namespace MovieRentalMVCApplication.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customer
        //public ActionResult Index()
        //{
        //    var customers = _context.Customers.Include(c => c.MembershipType).ToList();
        //    return View(customers);
        //}
       // [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            IEnumerable<Customer> customers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53221/api/");
                var responseTask = client.GetAsync("customers");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Customer>>();
                    readTask.Wait();
                    customers = readTask.Result;
                }
                else
                {
                    customers = Enumerable.Empty<Customer>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }

            }
            return View(customers);
        }
      
        public ActionResult Details(int? id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
        
        public ActionResult Edit(int? id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            var viewModel = new NewCustomerViewModel
             {
                 Customer = customer,
                 MembershipType = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
                    

        }
        [Authorize(Roles = Roles.Admin)]
        public ActionResult CustomerForm()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel
            {
                MembershipType = membershipTypes
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MembershipType = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if(customer.Id==0)
                 _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.DOB = customer.DOB;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
        //public ActionResult Create(Customer customer)
        //{
        //    _context.Customers.Add(customer);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index", "Customer");
        //}


        //public ActionResult DeleteCustomer(int? id)
        //{
        //    var customer = _context.Customers.Find(id);
        //    _context.Customers.Remove(customer);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index", "Customer");
        //}

    }
}