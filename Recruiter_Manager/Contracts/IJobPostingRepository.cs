using Recruiter_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Contracts
{
    public interface IJobPostingRepository : IRepositoryBase<JobPosting>
    {
        JobPosting GetJobPosting(int? jobId);
        void CreateJobPosting(JobPosting job);
        void EditJobPosting(JobPosting job);
        void DeleteJobPosting(int jobId);
    }
}
