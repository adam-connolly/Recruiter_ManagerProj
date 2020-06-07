using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Contracts
{
    public interface IRepositoryWrapper
    {
        ICustomerRepository Customer { get; }
        IJobPostingRepository JobPosting { get; }
        IRecruiterRepository Recruiter { get; }
        void Save();
    }
}
