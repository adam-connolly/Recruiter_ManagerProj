using Recruiter_Manager.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;
   
        private ICustomerRepository _customer;
        private IJobPostingRepository _jobPosting;
        private IRecruiterRepository _recruiter;
        public ICustomerRepository Customer
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new CustomerRepository(_context);
                }
                return _customer;
            }
        }
        public IJobPostingRepository JobPosting
        {
            get
            {
                if(_jobPosting == null)
                {
                    _jobPosting = new JobPostingRepository(_context);
                }
                return _jobPosting;
            }
        }
        public IRecruiterRepository Recruiter
        {
            get
            {
                if(_recruiter == null)
                {
                    _recruiter = new RecruiterRepository(_context);
                }
                return _recruiter;
            }
            
        }
        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
