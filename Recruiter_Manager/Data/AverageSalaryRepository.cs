using Recruiter_Manager.Contracts;
using Recruiter_Manager.Data;
using SalaryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Services
{
    public class AverageSalaryRepository : RepositoryBase<AverageSalary>, IAverageSalaryRepository
    {
        public AverageSalaryRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {

        }
        public List<AverageSalary> GetAverageSalaries() =>
            FindAll().ToList();
        public List<AverageSalary> GetAverageSalaries(string jobTitle) =>
            FindByCondition(s => s.JobTitle.Contains(jobTitle)).ToList();
    }
}
