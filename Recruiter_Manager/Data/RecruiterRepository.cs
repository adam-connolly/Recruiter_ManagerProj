using Recruiter_Manager.Contracts;
using Recruiter_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Data
{
    public class RecruiterRepository : RepositoryBase<Recruiter>, IRecruiterRepository
    {
        public RecruiterRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public void CreateRecruiter(Recruiter recruiter) => Create(recruiter);

        public void DeleteRecruiter(int recruiterId)
        {
            var recruiterToDelete = FindByCondition(r => r.Id.Equals(recruiterId)).SingleOrDefault();
            Delete(recruiterToDelete);
        }

        public void EditRecruiter(Recruiter recruiter)
        {
            Update(recruiter);
        }

        public Recruiter GetRecruiter(int? recruiterId) =>
            FindByCondition(r => r.Id.Equals(recruiterId)).SingleOrDefault();
    }
}
