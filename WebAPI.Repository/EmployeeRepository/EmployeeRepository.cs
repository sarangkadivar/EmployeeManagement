using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BussinessEntities.Model;
using WebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;


namespace WebAPI.Repository.LoginRepository
{
    //public class LoginRepository : ILoginRepository
    //{
    //    private readonly TestingContext context;
    //    public LoginRepository(TestingContext testing)
    //    {
    //        context = testing ?? throw new ArgumentNullException(nameof(testing));
    //    }
    //    public async Task<Login> Login(Login login)
    //    {
    //        Login data = new Login();
    //        var result = await context.Userdetail.Where(x => x.Username == login.Username && x.Password == login.Password && x.Isdeleted != true).FirstOrDefaultAsync();
    //        if (result != null)
    //        {
    //            data.Username = result.Username;
    //            data.Password = result.Password;
    //            data.Uid = result.Uid;
    //            data.Status = "Success";
    //        }
    //        return data;
    //    }
    //    public async Task<bool> LogOut(int Id)
    //    {
    //        bool result = false;
    //        var data = await context.Userdetail.Where(x => x.Uid == Id).FirstOrDefaultAsync();
    //        if (data != null)
    //        {
    //            result = true;
    //        }
    //        return result;
    //    }
    //}


    public class EmployeeRepository : IEmployeeRepository
    {
       
        private readonly TestingContext _dbContext;

        public EmployeeRepository(TestingContext testing)
        {
            _dbContext = testing;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            return _dbContext.Employees.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
        }

        public void Update(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
        }

        public async Task<bool> Delete(int id)
        {
            bool flag = false;
            var employee = _dbContext.Employees.Find(id);

            if (employee != null)
            {
                employee.Isdeleted = true;
                _dbContext.Entry(employee).State = EntityState.Modified;

                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();

                flag = true;

            }
            return flag;

        }
    }

}
