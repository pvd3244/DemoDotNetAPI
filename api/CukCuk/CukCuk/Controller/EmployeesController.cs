using CukCuk.Entities;
using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CukCuk.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public Object FilterEmployee([FromQuery]string? seach, [FromQuery]int positionCode = -1, [FromQuery] int departmentCode = -1, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            List<Employee> employeeList = new List<Employee>();
            string connectionString = "Server=localhost;Port=3306;Database=wdt.2022.pvdinh;Uid=root;Pwd=123456;";
            var mySqlConnection = new MySqlConnection(connectionString);
            string procName = "Proc_Employee_GetData";
            var parameters = new DynamicParameters();
            string Seach = "";
            string Code = "";
            string Page = "";
            if(positionCode == -1 && departmentCode == -1)
            {
                Code = "";
            }
            else if(positionCode != -1 && departmentCode == -1)
            {
                Code = "WHERE e.PositionCode = "+positionCode;
            }
            else if (positionCode == -1 && departmentCode != -1)
            {
                Code = "WHERE e.DepartmentCode = " + departmentCode;
            }
            else
            {
                Code = "WHERE e.PositionCode = " + positionCode + " AND e.DepartmentCode = " + departmentCode;
            }
            int skip, take = pageSize;
            skip = (pageNumber - 1) * pageSize;
            Page = "LIMIT "+skip+", "+take;
            parameters.Add("@$Code", Code);
            parameters.Add("@$Page", Page);
            var multipleResults = mySqlConnection.QueryMultiple(procName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            if(multipleResults != null)
            {
                var employees = multipleResults.Read<Employee>();
                employeeList = employees.ToList();
            }

            return employeeList;
        }
        [HttpDelete("{employeeCode}")]
        public int DeleteEmployee([FromRoute]string employeeCode)
        {
            string connectionString = "Server=localhost;Port=3306;Database=wdt.2022.pvdinh;Uid=root;Pwd=123456;"; 
            var mySqlConnection = new MySqlConnection(connectionString);
            string sqlDeleteEmployee = "DELETE FROM `wdt.2022.pvdinh`.employee WHERE EmployeeCode = @employeeCode";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeCode", employeeCode);
            int affectedRows = mySqlConnection.Execute(sqlDeleteEmployee, parameters);
            if (affectedRows > 0)
                return 1;
            else
                return 0;
        }
        [HttpPost]
        public int InsertEmployee([FromBody]Employee employee)
        {
            string connectionString = "Server=localhost;Port=3306;Database=wdt.2022.pvdinh;Uid=root;Pwd=123456;"; 
            var mySqlConnection = new MySqlConnection(connectionString);

            string sqlInsertEmployee = "INSERT INTO employee (EmployeeID, EmployeeCode, EmployeeName, DateOfBirth, Gender, PeopleID, AddressRange, DateRange, Email, Phone, PositionCode, DepartmentCode, SingerCode, Salary, DateOfJoin, WorkStatus, CreatedDate, CreatedBy) " +
                    "VALUES (@EmployeeID, @EmployeeCode, @EmployeeName, @DateOfBirth, @Gender, @PeopleID, @AddressRange, @DateRange, @Email, @Phone, @PositionCode, @DepartmentCode, @SingerCode, @Salary, @DateOfJoin, @WorkStatus, @CreatedDate, @CreatedBy);"; ;

            var employeeID = Guid.NewGuid();
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeID", employeeID);
            parameters.Add("@EmployeeCode", employee.EmployeeCode);
            parameters.Add("@EmployeeName", employee.EmployeeName);
            parameters.Add("@DateOfBirth", employee.DateOfBirth);
            parameters.Add("@Gender", employee.Gender);
            parameters.Add("@PeopleID", employee.PeopleID);
            parameters.Add("@AddressRange", employee.AddressRange);
            parameters.Add("@DateRange", employee.DateRange);
            parameters.Add("@Email", employee.Email);
            parameters.Add("@Phone", employee.Phone);
            parameters.Add("@PositionCode", employee.PositionCode);
            parameters.Add("@DepartmentCode", employee.DepartmentCode);
            parameters.Add("@SingerCode", employee.SingerCode);
            parameters.Add("@Salary", employee.Salary);
            parameters.Add("@DateOfJoin", employee.DateOfJoin);
            parameters.Add("@WorkStatus", employee.WorkStatus);
            parameters.Add("@CreatedBy", employee.CreatedBy);
            parameters.Add("@CreatedDate", employee.CreatedDate);
            if (CheckCode(employee.EmployeeCode))
            {
                return 0;
            }
            int numberOfAffectedRows = mySqlConnection.Execute(sqlInsertEmployee, parameters);
            if (numberOfAffectedRows > 0)
                return 1;
            else
                return 0;
        }
        [HttpGet("{employeeCode}")]
        public Object GetDataByCode([FromRoute]string employeeCode)
        {
            string connectionString = "Server=localhost;Port=3306;Database=wdt.2022.pvdinh;Uid=root;Pwd=123456;"; 
            var mySqlConnection = new MySqlConnection(connectionString);

            string procName = "Proc_Employee_GetDataByEmployeeCode";

            var parameters = new DynamicParameters();
            parameters.Add("@$employeeCode", employeeCode);

            var employee = mySqlConnection.QueryFirstOrDefault<Employee>(procName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return employee;
        }
        [HttpPut("{employeeCode}")]
        public int UpdateEmployee([FromRoute]string employeeCode, [FromBody]Employee employee)
        {
            string connectionString = "Server=localhost;Port=3306;Database=wdt.2022.pvdinh;Uid=root;Pwd=123456;";
            var mySqlConnection = new MySqlConnection(connectionString);
            string procName = "Proc_Employee_Update";
            var parameters = new DynamicParameters();
            parameters.Add("@$employeeCode",employeeCode);
            parameters.Add("@$employeeName", employee.EmployeeName);
            parameters.Add("@$dateOfBirth", employee.DateOfBirth);
            parameters.Add("@$gender", employee.Gender);
            parameters.Add("@$peopleID", employee.PeopleID);
            parameters.Add("@$addressRange", employee.AddressRange);
            parameters.Add("@$dateRange", employee.DateRange);
            parameters.Add("@$email", employee.Email);
            parameters.Add("@$phone", employee.Phone);
            parameters.Add("@$positionCode", employee.PositionCode);
            parameters.Add("@$departmentCode", employee.DepartmentCode);
            parameters.Add("@$singerCode", employee.SingerCode);
            parameters.Add("@$salary", employee.Salary);
            parameters.Add("@$dateOfJoin", employee.DateOfJoin);
            parameters.Add("@$workStatus", employee.WorkStatus);

            int result = mySqlConnection.Execute(procName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            if (result > 0)
                return 1;
            else
                return 0;
        }

        public static bool CheckCode(string employeeCode)
        {
            string connectionString = "Server=localhost;Port=3306;Database=wdt.2022.pvdinh;Uid=root;Pwd=123456;"; 
            var mySqlConnection = new MySqlConnection(connectionString);

            string procName = "Proc_Employee_GetDataByEmployeeCode";

            var parameters = new DynamicParameters();
            parameters.Add("@$employeeCode", employeeCode);

            var employee = mySqlConnection.QueryFirstOrDefault<Employee>(procName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            if (employee != null)
                return true;
            else
                return false;
        }
    }
}
