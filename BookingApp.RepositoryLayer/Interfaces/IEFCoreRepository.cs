using BookingApp.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryLayer.Interfaces;
public interface IEFCoreRepository<TClass> : IRepository<TClass> where TClass : BaseEntity
{
    IUnitOfWork UnitOfWork { get; }
}
