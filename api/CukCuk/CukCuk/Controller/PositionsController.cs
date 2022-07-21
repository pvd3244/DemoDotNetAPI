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
    public class PositionsController : ControllerBase
    {
        [HttpGet]
        public Object GetPosition()
        {
            List<Position> positionList = new List<Position>();
            string connectionString = "Server=localhost;Port=3306;Database=wdt.2022.pvdinh;Uid=root;Pwd=123456;";
            var mySqlConnection = new MySqlConnection(connectionString);
            string procName = "SELECT*FROM positions ORDER BY @abc ASC";
            var parameters = new DynamicParameters();
            parameters.Add("@abc", "PositionCode");
            var multipleResults = mySqlConnection.QueryMultiple(procName, parameters, commandType: System.Data.CommandType.Text);
            if (multipleResults != null)
            {
                var positions = multipleResults.Read<Position>();
                positionList = positions.ToList();
            }
            return positionList;
        }
    }
}
