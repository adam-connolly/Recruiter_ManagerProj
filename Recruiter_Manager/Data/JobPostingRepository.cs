using Recruiter_Manager.Contracts;
using Recruiter_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Data
{
    public class JobPostingRepository : RepositoryBase<JobPosting>, IJobPostingRepository
    {
        public JobPostingRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public void CreateJobPosting(JobPosting job) => Create(job);

        public void DeleteJobPosting(int jobId)
        {
            var jobToDelete = FindByCondition(j => j.Id.Equals(jobId)).SingleOrDefault();
            Delete(jobToDelete);
        }

        public void EditJobPosting(JobPosting job)
        {
            Update(job);
        }

        public JobPosting GetJobPosting(int? jobId) =>
            FindByCondition(j => j.Id.Equals(jobId)).SingleOrDefault();
    }
}
