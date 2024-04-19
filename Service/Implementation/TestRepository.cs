using Dapper;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class TestRepository : ITestRepository
    {
        private readonly IConfiguration _configuration;
        public TestRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public Task<string> AddUpdate(Test entity, string userName)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Extra(Test entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Test>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            throw new NotImplementedException();
        }

        public Test GetById(int Id, string user)
        {
            throw new NotImplementedException();
        }

        //public async Task<Test> AddUpdate(Test entity)
        //{
        //    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        var parameters = new { FirstName = entity.FirstName, LastName = entity.LastName };
        //        var result = await connection.QuerySingleOrDefaultAsync<Test>("SP_CREATE", parameters, commandType: CommandType.StoredProcedure);
        //        return result;
        //    }
        //}

        //public Task<int> Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> Extra(Test entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IReadOnlyList<Test>> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Test> GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
