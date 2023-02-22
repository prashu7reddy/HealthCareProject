using HealthCareProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Repository
{
    public interface IGetRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
    }
    public interface IRepository<T> where T :class
    {
       
        Task <T>create(T obj);
        Task <T>Update(int id, T obj);
        Task <T>Delete(int id);

    }

    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> SearchBySpecialization(string SpecializationName);
        Task<IEnumerable<DocSpecialization>> GetSpecializations();
    }
    public interface IGetUserDetailsRepository<T> where T : class
    {
        Task<T> GetUserId(string userName);
    }
   
}
