using DbModel = EmployeeDetailStore.Model.DatabaseModels ;
using DtoModel = EmployeeDetailStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDetailStore.Utilities.Mapper
{
    public static class EmployeeMapper
    {
        public static DtoModel.Employee MapDbToDto(DbModel.Employee dbModelEmployee)
        {
            var employee = new DtoModel.Employee();
            employee.EmployeeId = dbModelEmployee.EmployeeId;
            employee.Age = dbModelEmployee.Age;
            employee.Address = dbModelEmployee.Address;
            employee.Email = dbModelEmployee.Email;
            employee.FirstName = dbModelEmployee.FirstName;
            employee.LastName = dbModelEmployee.LastName;
            employee.Gender = dbModelEmployee.Gender;

            return employee;
        }

        public static DbModel.Employee MapDtoToDb(DtoModel.Employee dtoEmployee)
        {
            var employee = new DbModel.Employee();
            employee.EmployeeId = dtoEmployee.EmployeeId;
            employee.Age = dtoEmployee.Age;
            employee.Address = dtoEmployee.Address;
            employee.Email = dtoEmployee.Email;
            employee.FirstName = dtoEmployee.FirstName;
            employee.LastName = dtoEmployee.LastName;
            employee.Gender = dtoEmployee.Gender;

            return employee;
        }
    }
}
