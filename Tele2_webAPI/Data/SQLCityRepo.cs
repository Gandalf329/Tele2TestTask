using System.Collections.Generic;
using System.Linq;
using Tele2API.Models;
using System.Net;
using System.Text.Json;

namespace Tele2API.Data
{
    public class CacheProfile : ICitizen
    {
        private readonly CitizenContext _context;
        public CacheProfile(CitizenContext context)
        {
            _context = context;
        }
        public IEnumerable<Citizen> GetCitizens()
        {
            return _context.Citizens.ToList();
        }
        public IEnumerable<Citizen> GetCitizensByParams(string sex = null, int lowAge = -1, int upAge = -1, int pageNum = -1, int pageSize = -1)
        {
            IQueryable<Citizen> query;

            if (sex == null && lowAge == -1 && upAge == -1  && pageNum == -1 && pageSize == -1)
            {
                return _context.Citizens.ToList();
            }
            else
            {
                query = _context.Citizens.AsQueryable();

                query = query.Where(c => (c.Age >= lowAge || lowAge == -1) &&
                                                 (c.Age <= upAge || upAge == -1) &&
                                                 (c.Sex == sex || sex == null));
                if (pageNum != -1 && pageSize != -1)
                {
                    query = query.Skip((pageNum - 1) * pageSize).Take(pageSize);
                }
            }
            return query.ToArray(); ;
        }
        public Citizen GetCitizenById(string id)
        {
            return _context.Citizens.FirstOrDefault(p => p.Id == id);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        public void PersonCitizens()
        {
            string json;
            
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString("http://testlodtask20172.azurewebsites.net/task");
            }
            using var doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            var users = root.EnumerateArray();
            
            while (users.MoveNext())
            {
                var citizen = new Citizen();
                var user = users.Current;
                var props = user.EnumerateObject();
                while (props.MoveNext())
                {
                    var prop = props.Current;
                    switch(prop.Name)
                    {
                        case "id":
                            citizen.Id = prop.Value.GetString();
                            break;
                        case "name":
                            citizen.Name = prop.Value.GetString();
                            break;
                        case "sex":
                            citizen.Sex = prop.Value.GetString();
                            break;
                    }
                }   
                
                using (WebClient wc = new WebClient())
                {
                    json = wc.DownloadString("http://testlodtask20172.azurewebsites.net/task/" + citizen.Id);
                    using var parse = JsonDocument.Parse(json);
                    user = parse.RootElement;
                    props = user.EnumerateObject();
                    citizen.Age = props.Last().Value.GetUInt16();
                }
                
                var theSameCitizen = _context.Citizens.AsEnumerable().Where(c => c.Id == citizen.Id).Select(c => new Citizen());
                if(!theSameCitizen.Any())
                _context.Citizens.Add(citizen);
            }
        }
    }
}
