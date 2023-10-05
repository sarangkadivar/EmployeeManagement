using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.BussinessEntities.Model
{
   
 
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool Isdeleted { get; set; }

    }
}
