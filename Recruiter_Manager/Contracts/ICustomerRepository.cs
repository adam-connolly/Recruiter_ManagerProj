﻿using Recruiter_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Contracts
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Customer GetCustomer(int? customerId);
        Customer GetCustomer(string userId);
        void CreateCustomer(Customer customer);
        void EditCustomer(Customer customer);
        void DeleteCustomer(int customerId);
    }
}
