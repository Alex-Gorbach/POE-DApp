using POE.DAL.EF;
using POE.DAL.Entities;
using POE.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace POE.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public List<string> GetAddressByEmail(string id)
        {
            var addresses = new List<string>();
            var address = from data in Database.ClientProfiles where data.Id == id select data.Address;
            foreach (var item in address)
            {
                addresses.Add(item);
            }
            return addresses;
         }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
