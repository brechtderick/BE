using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.DTOs;
using BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BE.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Get the details of the authenticated customer
        /// </summary>
        /// <returns>the customer</returns>
        [HttpGet()]
        public ActionResult<CustomerDTO> GetCustomer()
        {
            Customer customer = _customerRepository.GetBy(User.Identity.Name);
            return new CustomerDTO(customer);
        }

    }
}