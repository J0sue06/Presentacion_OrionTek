
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Practica_OrionTek.Helpers;
using Practica_OrionTek.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Practica_OrionTek.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // ESTE METODO ES UTILIZADO PARA INICIAR SESION
        public JsonResult SignIn(LoginViewModel model)
        {
            // SE VALIDA QUE LOS DATOS RECIBIDOS POR EL METODO ESTEN CORRECTOS
            if (ModelState.IsValid)
            {
                // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
                using (DBEntities db = new DBEntities())
                {
                    // SE BUSCA TODA LA INFORMACION REFERENTE A LOS PARAMETROS INTRODUCIDOS POR EL USUARIO
                    var userInfo = (from u in db.usuarios
                                    where u.email == model.email && u.pass == model.pass
                                    select new 
                                    {
                                        u.id,
                                        User = u.nombre + " " + u.apellido
                                    }).FirstOrDefault();

                    // SE CREA UNA SESION CON ALGUNOS DATOS DEL USUARIO
                    Session["userId"] = userInfo.id;
                    Session["userName"] = userInfo.User;                    

                    // POR ULTIMO SE RETORNA UN OBJETO CON UN ESTADO DE 200 Y UNA URL DE REDIRECCION
                    return Json(new { status = 200, message = "/Home" }, JsonRequestBehavior.AllowGet); 
                }
                
            }
            else
            {
                // SE OBTIENEN LOS ERRORES DE VALIDACION DEL MODELO RECIBIDO
                var errores = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));

                // SE RETORNA UN OBJETO CON UN ESTADO 400 ESPECIFICANDO QUE HUBO UN ERROR, TAMBIEN SE RETORNA UN ARRAY CON LOS ERRORES ENCONTRADOS
                return Json(new { status = 400, message = errores }, JsonRequestBehavior.AllowGet);
            }            
        }

        // ESTE METODO SE UTILIZA PARA CERRAR SESION
        public void LogOut()
        {
            // SE ELIMINAN LOS DATOS DE SESION GUARDADOS PREVIAMENTE
            Session["userId"] = null;
            Session["userName"] = null;
        }


    }
}