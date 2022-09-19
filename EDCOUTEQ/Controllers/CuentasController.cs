using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EDCOUTEQ.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services.Description;

namespace EDCOUTEQ.Controllers
{
    
    public class CuentasController : Controller
    {
        static string cadenaConexion = "Data Source=DESKTOP-RDBRQG8;Initial Catalog=edcouteq;Integrated Security=true;";
        //static string cadenaConexion = "Data Source=DESKTOP-ADDCRJO;Initial Catalog=edcouteq;Integrated Security=true; user id=sa; pwd=123";
        
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return RedirectToAction("Login");
        }

        public ActionResult RegistroUsuario()
        {
            return RedirectToAction("Admin", "Cursos");
        }

        //Metodo de inicio de sesion
        [HttpPost]
        public ActionResult Login(LoginViewModel credenciales)
        {
            credenciales.Password = EncriptarSha256(credenciales.Password);
            
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SP_validaUsuario", cn);
                cmd.Parameters.AddWithValue("Email", credenciales.Email);
                cmd.Parameters.AddWithValue("Pass", credenciales.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                credenciales.idUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());

               /* cmd = new SqlCommand("SP_obtenUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                credenciales = Convert.ToString(cmd.);*/
                
            }

            if(credenciales.idUsuario != 0)
            {
                Session["usuario"] = credenciales;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }
        }

        //Metodo para registrar usuarios
        [HttpPost]
        public ActionResult Registro(Usuarios usuarioInfo)
        {
            bool registrado;
            string mensaje;
            if(usuarioInfo.Password == usuarioInfo.confirmPassword)
            {
                usuarioInfo.Password = EncriptarSha256(usuarioInfo.Password);

            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SP_registraUsuario", cn);
                cmd.Parameters.AddWithValue("Rol", usuarioInfo.rol);
                cmd.Parameters.AddWithValue("Nombre", usuarioInfo.nombre);
                cmd.Parameters.AddWithValue("Apellido", usuarioInfo.apellido);
                cmd.Parameters.AddWithValue("Email", usuarioInfo.Email);
                cmd.Parameters.AddWithValue("Pass", usuarioInfo.Password);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }

            ViewData["Mensaje"] = mensaje;

            if(registrado)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
            
        }

        //Metodo para registrar usuarios desde admin
        [HttpPost]
        public ActionResult RegistroUsuarios(Usuarios usuarioInfo)
        {
            bool registrado;
            string mensaje;
            if (usuarioInfo.Password == usuarioInfo.confirmPassword)
            {
                usuarioInfo.Password = EncriptarSha256(usuarioInfo.Password);

            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SP_registraUsuario", cn);
                cmd.Parameters.AddWithValue("Rol", usuarioInfo.rol);
                cmd.Parameters.AddWithValue("Nombre", usuarioInfo.nombre);
                cmd.Parameters.AddWithValue("Apellido", usuarioInfo.apellido);
                cmd.Parameters.AddWithValue("Email", usuarioInfo.Email);
                cmd.Parameters.AddWithValue("Pass", usuarioInfo.Password);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }

        }

        //Metodo para cerrar sesion
        public ActionResult Logout()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Cuentas");
        }
        
        public static string EncriptarSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach(byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

    }
}