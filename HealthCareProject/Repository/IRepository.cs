using HealthCareProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Repository
{
    public interface IRepository<T> where T :class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task <T>create(T obj);
        Task <T>Update(int id, T obj);
        Task <T>Delete(int id);

    }

    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> SearchBySpecialization(string SpecializationName);
    }
}
