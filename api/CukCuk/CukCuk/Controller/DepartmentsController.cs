using CukCuk.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CukCuk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        [HttpGet]
        public Object GetDepartment()
        {
            List<Department> departmentList = new List<Department>();
            string connectionString = "Server=localhost;Port=3306;Database=wdt.2022.pvdinh;Uid=root;Pwd=123456;";
            var mySqlConnection = new MySqlConnection(connectionString);
            string procName = "SELECT*FROM department ORDER BY @abc ASC";
            var parameters = new DynamicParameters();
            parameters.Add("@abc", "DepartmentCode");
            var multipleResults = mySqlConnection.QueryMultiple(procName, parameters, commandType: System.Data.CommandType.Text);
            if (multipleResults != null)
            {
                var departments = multipleResults.Read<Department>();
                departmentList = departments.ToList();
            }
            return departmentList;
        }
    }
}
