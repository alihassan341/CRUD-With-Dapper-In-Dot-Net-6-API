using CRUDwithDapper.Model;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CRUDwithDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static async Task<IEnumerable<Users>> SelectAllUsers(SqlConnection sqlConnection)
        {
            return await sqlConnection.QueryAsync<Users>("select * from Users");
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<List<Users>>> GetAll()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //IEnumerable<Users> Admin = await SelectAllUsers(connection);
            //return Ok(Admin);
            var Admin = await connection.QueryAsync<Users>("select * from Users");
            return Ok(Admin);
        }



        [HttpGet("{Id}")]
        //[ActionName("GetAdmin")]
        public async Task<ActionResult<Users>> GetAdmin(int Id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var Admin = await connection.QueryFirstAsync<Users>("select * from Users where Id = @Id", new { Id = Id });
            return Ok(Admin);
        }


        [HttpPost]
        [ActionName("CreateAdmin")]
        public async Task<ActionResult<List<Users>>> CreateAdmin(Users user)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("insert into users (FirstName,LastName,Email,Cnic,Password,Admin) Values (@FirstName,@Lastname,@Email,@Cnic,@Password,@Admin)", user);
            return Ok(await SelectAllUsers(connection));
        }

        [HttpPut]
        [ActionName("Update")]
        public async Task<ActionResult<List<Users>>> UpdateAdmin(Users user)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var Admin = await connection.QueryFirstAsync<Users>("update Users set FirstName = @FirstName,LastName =@LastName," +
                "Email= @Email,Password = Password,Admin =@Admin where Id = @Id", user);
            return Ok(Admin);
        }


        [HttpDelete("{Id}")]
        [ActionName("DeleteAdmin")]
        public async Task<ActionResult<List<Users>>> DeleteAdmin(int Id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("delete from Users where Id   = @Id", new { Id = @Id });
            return Ok(await SelectAllUsers(connection));
            //var Admin = await connection.QueryFirstAsync<Users>("update Users set FirstName = @FirstName,LastName =@LastName," +
            //    "Email= @Email,Password = Password,Admin =@Admin where Id = @Id", user);
            //return Ok(Admin);
        }
    }
    
}
