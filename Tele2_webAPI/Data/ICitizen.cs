using System.Collections.Generic;
using Tele2API.Models;

namespace Tele2API.Data
{
    public interface ICitizen
    {
        bool SaveChanges();
        IEnumerable<Citizen> GetCitizens();
        Citizen GetCitizenById(string code);
        void PersonCitizens();
        IEnumerable<Citizen> GetCitizensByParams(string sex = null, int lowAge = -1, int upAge = -1, int pageNum = -1, int pageSize = -1);
    }
}
