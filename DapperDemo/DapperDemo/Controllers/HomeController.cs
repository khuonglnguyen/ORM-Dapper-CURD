using Dapper;
using DapperDemo.Models;
using DapperDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DapperDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IDapper _dapper;
        public HomeController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Dummy> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<Dummy>($"Select * from [Dummy] where Id = {Id}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<List<Dummy>> GetAll()
        {
            var result = await Task.FromResult(_dapper.GetAll<Dummy>($"Select * from [Dummy]", null, commandType: CommandType.Text));
            return result;
        }

        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(Dummy data)
        {
            var result = await Task.FromResult(_dapper.Insert<int>($"INSERT INTO [dbo].[Dummy] ([Name] ,[Age]) VALUES ('{data.Name}' ,{data.Age})", null, commandType: CommandType.Text));
            return Ok();
        }

        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update(Dummy data)
        {
            var result = await Task.FromResult(_dapper.Update<Dummy>($"UPDATE [dbo].[Dummy] SET [Name] = '{data.Name}', [Age] = {data.Age} WHERE Id = {data.Id}", null, commandType: CommandType.Text));
            return Ok();
        }

        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await Task.FromResult(_dapper.Execute($"DELETE FROM Dummy WHERE Id = {Id}", null, commandType: CommandType.Text));
            return Ok();
        }
    }
}
