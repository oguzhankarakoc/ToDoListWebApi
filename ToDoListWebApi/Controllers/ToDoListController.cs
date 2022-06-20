using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ToDoListWebApi.Models;

namespace ToDoListWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ToDoListController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Get All List Contents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select Id as ""Id"",Id as ""ListTittle"", ListContent as ""ListContent"" from ToDoList";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ToDoListAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);
        }


        /// <summary>
        /// Create the List Contents
        /// </summary>
        /// <param name="toDoList"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(ToDoList toDo)
        {
            string query = @"insert into ToDoList(ListTittle,ListContent) values (@ListTittle,@ListContent)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ToDoListAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ListTittle", toDo.ListTittle);
                    myCommand.Parameters.AddWithValue("@ListContent", toDo.ListContent);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);
        }


        /// <summary>
        /// Update the List Contents
        /// </summary>
        /// <param name="id"></param>
        /// <param name="toDoList"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonResult Put(ToDoList toDo)
        {
            string query = @"update ToDoList set ListTittle=@ListTittle,ListContent=@ListContent where Id=@Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ToDoListAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", toDo.Id);
                    myCommand.Parameters.AddWithValue("@ListTittle", toDo.ListTittle);
                    myCommand.Parameters.AddWithValue("@ListContent", toDo.ListContent);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);
        }


        /// <summary>
        /// Delete the List Contents
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from ToDoList where Id=@Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ToDoListAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);
        }
    }
}
