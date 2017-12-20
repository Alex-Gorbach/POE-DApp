using POE.DAL.Entities;
using System;
using System.Collections.Generic;

namespace POE.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
        List<string> GetAddressByEmail(string id);
    }
}
