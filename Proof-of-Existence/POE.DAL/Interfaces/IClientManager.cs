using POE.DAL.Entities;
using System;

namespace POE.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
