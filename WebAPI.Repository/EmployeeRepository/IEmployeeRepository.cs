using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BussinessEntities.Model;

namespace WebAPI.Repository.LoginRepository
{
    //public interface ILoginRepository
    //{
    //    Task<Login> Login(Login login);
    //    Task<bool> LogOut(int Id);
    //}

    public interface IEmployeeRepository
    {

        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        Task<bool> Delete(int Id);
    }
}
