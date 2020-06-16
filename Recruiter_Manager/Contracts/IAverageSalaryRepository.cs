using SalaryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Contracts
{
    public interface IAverageSalaryRepository
    {
        List<AverageSalary> GetAverageSalaries();
        List<AverageSalary> GetAverageSalaries(string jobTitle);
    }
}
