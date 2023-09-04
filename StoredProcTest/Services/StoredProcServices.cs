using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoredProcTest.Entities;
using StoredProcTest.Helper;
using StoredProcTest.OutputModel;
using System;
using System.Data;
using System.Data.Common;
using System.Security;

namespace StoredProcTest.Services
{
    public class StoredProcServices
    {
    
        //Make method ressilient
        public async static Task<ResultClass> FindStudentsFromSql(ApplicationContext context,string sql)
        {
            try
            {
                var connection = context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                // namespace weirdness
                //command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@id", 1));

                var reader = await command.ExecuteReaderAsync();
                var firstTables = new List<FirstTable>();
                var secondTables = new List<SecondTable>();

                while (await reader.ReadAsync())
                {
                    firstTables.Add(new FirstTable
                    {
                        Name = reader.GetString("Name")
                    });
                }

                await reader.NextResultAsync();

                while (await reader.ReadAsync())
                {
                    secondTables.Add(new SecondTable
                    {
                        Name = reader.GetString("Name"),
                        ClassName = reader.GetString("ClassName")
                    });
                }

                //Add Caching If you like 
                //context.AttachRange(firstTables);
                //context.AttachRange(secondTables);
                await reader.CloseAsync();

                var result = new ResultClass { FirstTableItems = firstTables, SecondTableItems = secondTables };

                return result;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResultClass> FindStudentsFromSqlGeneric(ApplicationContext context, string sql)
        {
            return await context.ExecuteReaderAsync(DashboardDataMapper, sql);
        }

        public ResultClass DashboardDataMapper(DbDataReader reader)
        {
            var result = new ResultClass
            {
                // Result Set 1 - MenuItems
                FirstTableItems = reader.Translate<FirstTable>(),

                // Result Set 2 - Root MeuItems
                SecondTableItems = reader.Translate<SecondTable>()
            };

            return result;
        }
    }
}
