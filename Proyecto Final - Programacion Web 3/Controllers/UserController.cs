using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Final___Programacion_Web_3.Models;
using System.Data.SqlClient;

namespace Proyecto_Final___Programacion_Web_3.Controllers
{
    public class UserController : Controller
    {

        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader dataReader;

        void connectionString()
        {
            connection.ConnectionString = "data source=LAPTOP-2SVINSSL; database=web_final; integrated security = SSPI;";
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            user user = new user();
            return View(user);
        }

        [HttpPost]
        public ActionResult AddOrEdit(user user)
        {
            
            using (DatabaseModels databaseModel = new DatabaseModels())
            {
                try
                {
                    if (databaseModel.users.Any(x => x.email == user.email))
                    {
                        ViewBag.ErrorMessage = "Ya existe un usuario con ese email";
                        return View("AddOrEdit", new user());
                    }
                    else
                    {
                        databaseModel.users.Add(user);
                        databaseModel.SaveChanges();
                        ModelState.Clear();
                        ViewBag.SuccessMessage = "Se ha Registrado el usuario: " + user.email;
                        return View("Login");
                    }
                }
                catch(Exception exception)
                {
                    ViewBag.ErrorMessage = "Ha ocurrido un error al intentar registrar el usuario" + exception.StackTrace;
                    return View("AddOrEdit", new user());
                }
            }
            
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Verify(user user)
        {
            connectionString();
            connection.Open();
            command.Connection = connection;
            command.CommandText = "select * from users where email = '" + user.email + "' and password = '" + user.password + "'";
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                connection.Close();
                return RedirectToAction("Index", "Partners");
            }
            else
            {
                ViewBag.ErrorMessage = "Usuario no encontrado";
                return View("Login");
            }
        }

    }
}