using Recruiter_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Contracts
{
    public interface IRecruiterRepository : IRepositoryBase<Recruiter>
    {
        Recruiter GetRecruiter(int? recruiterId);
        void CreateRecruiter(Recruiter recruiter);
        void EditRecruiter(Recruiter recruiter);
        void DeleteRecruiter(int recruiterId);
    }
}
