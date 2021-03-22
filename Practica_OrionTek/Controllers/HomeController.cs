using Newtonsoft.Json;
using Practica_OrionTek.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Practica_OrionTek.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            // SE VALIDA QUE EL USUARIO ESTE LOGUEADO
            if (Session["userId"] != null)
            {
                // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
                using (DBEntities db = new DBEntities())
                {
                    // SE OBTIENEN TODOS LOS CLIENTES DE FORMA DECENDENTE
                    var clients = (from c in db.clientes
                                   orderby c.id descending
                                   select c).ToList();

                    // SE DEVUELVE A LA VISTA EL RESULTADO DEL QUERY ANTERIOR MEDIANTE VIEWBAG
                    ViewBag.Clients = clients;

                    return View();
                }
            }
            else
            {
                // SI EL USUARIO NO ESTA LOGUEADO SE LE RETORNA A LOGIN
                return Redirect("/Login");
            }

        }

        // ESTE METODO SE USA PARA OBTENER LOS DETALLES DEL CLIENTE, SE DEBE PASAR EL ID DEL CLIENTE 
        public ActionResult UserDetails(int id = 0)
        {
            // SE VERIFICA QUE EL USUARIO ESTE LOGUEADO
            if (Session["userId"] != null)
            {
                // SE VALIDA QUE EL AL METODO SE LE PASA ALGUN PARAMETRO PARA BUSCAR EN LA BASE DE DATOS 
                if (id > 0)
                {
                    // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
                    using (DBEntities db = new DBEntities())
                    {
                        // SE HACE UNA CONSULTA A LA TABLA CLIENTES PARA OBTENER EL CLIENTE PASANDO COMO PARAMETRO EL ID QUE EL METODO ESPERA
                        var details = (from c in db.clientes
                                       where c.id == id
                                       orderby c.id descending
                                       select c).First();

                        // SE HACE UNA CONSULTA A LA TABLA DIRECCIONES Y A LA TABLA PAISES CON EL ID QUE RECIBE EL METODO
                        // PARA OBTENER TODAS LAS DIRECCIONES RELACIONADAS AL ID QUE RECIBE EL METODO
                        var address = (from a in db.direcciones
                                       join p in db.paises on a.id_pais equals p.id                                       
                                       where a.id_cliente == id
                                       orderby a.id descending
                                       select new
                                       {
                                           a.id,
                                           a.line1,
                                           a.line2,
                                           a.sector,
                                           a.ciudad,
                                           pais = p.pais1,
                                           id_pais = p.id,
                                           a.zipcode
                                       }).ToList();

                        // EL RESULTADO DE LAS DIRECCIONES SE CONVIERTE A JSON PARA SER MEJOR INTERPRETADO POR EL FOREACH EN LA VISTA 
                        dynamic jsonSerializer = JsonConvert.SerializeObject(address);
                        dynamic json = JsonConvert.DeserializeObject<dynamic>(jsonSerializer);

                        // SE RETORNA LA INFORMACION DEL CLIENTE ENCONTRADO EN LA CONSULTA
                        ViewBag.detailsClient = details;
                        // SE RETORNAN LAS DIRECCIONES CONVERTIDAS A JSON
                        ViewBag.addressClient = json;

                        return View();
                    }
                }
                else
                {
                    // SI EL METODO NO RECIBE NINGUN DATO LO RETORNA A HOME
                    return Redirect("/Home");
                }
                
            }
            else
            {
                // SI EL USUARIO NO ESTA LOGUEADO LO REDIRECCIONA AL LOGIN
                return Redirect("/Login");
            }
            
        }

        // ESTE METODO SE UTILIZA PARA AGREGAR UNA NUEVA DIRECCION, SE DEBE PASAR EL ID DEL CLIENTE
        public ActionResult NewAddress(int id)
        {
            // SE DEVUELVE A LA VISTA EL ID DEL CLIENTE RECIBIDO
            ViewBag.clientId = id;

            return View();
        }

        // ESTE METODO SE UTILIZA PARA ACTUALIZAR LA INFORMACION DEL CLIENTE
        public ActionResult UpdateClient(int id)
        {
            // SE VALIDA QUE EL AL METODO SE LE PASA ALGUN PARAMETRO PARA BUSCAR EN LA BASE DE DATOS 
            if (id > 0)
            {
                // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
                using (DBEntities db = new DBEntities())
                {
                    // SE BUSCA EN LA TABLA CLIENTES TODO LO RELACIONADO CON EL ID QUE RECIBE EL PARAMETRO
                    var client = (from c in db.clientes
                                  where c.id == id
                                  select c).First();

                    // SE RETORNA A LA VISTA TODO LO ENCONTRADO POR LA CONSULTA ANTERIOR
                    ViewBag.clientInfo = client;

                    return View();
                }
            }
            else
            {
                // SI EL METODO NO RECIBE NINGUN DATO LO RETORNA A HOME
                return Redirect("/Home");
            }
                   
        }

        // ESTE METODO ES UTILIZADO PARA ACTUALIZAR DATOS DEL CLIENTEM, RECIBE UN MODELO CON LOS DATOS DEL CLIENTE
        public async Task<JsonResult> SaveChangesClient(cliente model)
        {
            // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
            using (DBEntities db = new DBEntities())
            {
                // SE VALIDA QUE LOS DATOS RECIBIDOS POR EL METODO ESTEN CORRECTOS
                if (ModelState.IsValid)
                {
                    // SE AGREGA EL MODELO RECIBIDO AL CONTEXTO Y SE ESPECIFICA QUE SU ESTADO ES DE MODIFICACION
                    db.Entry(model).State = EntityState.Modified;

                    // SE GUARDAN LOS CAMBIOS DE FORMA ASINCRONICA Y SE CAPTURA EL RESULTADO DE ESTA OPERACION
                    var res = await db.SaveChangesAsync();

                    // SI EL RESULTADO DE LA OPERACION ANTERIOR ES IGUAL A 1, TODO SALIO CORRECTO
                    if (res == 1)
                    {
                        // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 200 ESPECIFICANDO QUE LA OPERACION SE REALIZO CORRECTAMENTE
                        return Json(new { status = 200 });
                    }

                    // SI EL RESULTADO DE LA OPERACION ANTERIOR NO ES IGUAL A 1, NO SE REALIZO LA ACTUALIZACION DEL REGISTRO
                    else
                    {
                        // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 400 ESPECIFICANDO QUE LA OPERACION NO SE REALIZO CORRECTAMENTE   
                        return Json(new { status = 400, message = "Algo salio mal, vuelva a intentarlo!" });
                    }
                }

                // SI LA VALIDACION DEL MODELO RECIBIDO NO ES VALIDA
                else
                {
                    // SE OBTIENEN LOS ERRORES DE VALIDACION DEL MODELO RECIBIDO
                    var errores = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));

                    // SE RETORNA UN OBJETO CON UN ESTADO 400 ESPECIFICANDO QUE HUBO UN ERROR, TAMBIEN SE RETORNA UN ARRAY CON LOS ERRORES ENCONTRADOS
                    return Json(new { status = 400, message = errores }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        // ESTE METODO ES UTLIZADO PARA MOSTRAR LA VISTA DE ACTUALIZACION DE DIRECCIONES CON LOS DATOS CORRESPONDIENTES DE UNA DIRECCION,
        // ESTE METODO RECIBE EL ID DE LA DIRECCION Y EL ID DEL CLIENTE
        public ActionResult UpdateAddress(int idAddress = 0, int idClient = 0)
        {
            // SE VALIDA QUE EL METODO RECIBE ALGUN PARAMETRO
            if (idAddress <= 0 || idClient <= 0)
            {
                // SI EL METODO NO RECIBE NINGUN PARAMETRO SE REDIRECCIONA A HOME
                return Redirect("/Home");
            }
            else
            {
                // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
                using (DBEntities db = new DBEntities())
                {
                    // SE REALIZA UNA CONSULTA A LA TABLA DIRECCIONES PARA OBTENER LOS DATOS RELACIONADOS A EL ID DE LA DIRECCION RECIBIDO POR EL METODO
                    var _direction = (from d in db.direcciones
                                      join p in db.paises on d.id_pais equals p.id
                                      where d.id == idAddress
                                      select new
                                      {
                                          d.id,
                                          d.line1,
                                          d.line2,
                                          d.sector,
                                          d.ciudad,
                                          pais = p.pais1,
                                          id_pais = p.id,
                                          d.zipcode

                                      }).First();

                    // EL RESULTADO DE LA DIRECCION SE CONVIERTE A JSON PARA SER MEJOR INTERPRETADO POR EL FOREACH EN LA VISTA
                    dynamic jsonSerializer = JsonConvert.SerializeObject(_direction);
                    dynamic json = JsonConvert.DeserializeObject<dynamic>(jsonSerializer);

                    // SE RETORNA A LA VISTA POR VIEWBAG EL JSON DE LA DIRECCION
                    ViewBag.direction = json;
                    // SE RETORNA A LA VISTA EL ID DEL CLIENTE
                    ViewBag.clientId = idClient;
                    return View();
                }
            }                  
        }

        // ESTE METODO ES UTILIZADO PARA ACTUALIZAR LOS CAMBIOS DE UNA DIRECCION,
        // ESTE METODO RECIBE UN MODELO DE DIRECCION
        public async Task<JsonResult> SaveChangesAddress(direccione model)
        {
            // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
            using (DBEntities db = new DBEntities())
            {
                // SE VALIDA QUE LOS DATOS RECIBIDOS POR EL METODO ESTEN CORRECTOS
                if (ModelState.IsValid)
                {
                    // SE AGREGA EL MODELO RECIBIDO AL CONTEXTO Y SE ESPECIFICA QUE SU ESTADO ES DE MODIFICACION
                    db.Entry(model).State = EntityState.Modified;

                    // SE GUARDAN LOS CAMBIOS DE FORMA ASINCRONICA Y SE CAPTURA EL RESULTADO DE ESTA OPERACION
                    var res = await db.SaveChangesAsync();

                    // SI EL RESULTADO DE LA OPERACION ANTERIOR ES IGUAL A 1, TODO SALIO CORRECTO
                    if (res == 1)
                    {
                        // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 200 ESPECIFICANDO QUE LA OPERACION SE REALIZO CORRECTAMENTE
                        return Json(new { status = 200 });
                    }

                    // SI EL RESULTADO DE LA OPERACION ANTERIOR NO ES IGUAL A 1, NO SE REALIZO LA ACTUALIZACION DEL REGISTRO
                    else
                    {
                        // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 400 ESPECIFICANDO QUE LA OPERACION NO SE REALIZO CORRECTAMENTE   
                        return Json(new { status = 400, message = "Algo salio mal, vuelva a intentarlo!" });
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
        }

        // ESTE METODO SE UTILIZA RETORNAR LA VISTA DE NUEVO USUARIO
        public ActionResult NewUser()
        {
            // SE VERIFICA QUE EL USUARIO ESTE LOGUEADO
            if (Session["userId"] != null)
            {
                // SI EL USUARIO ESTA LOGUEADO LE DUELVE LA VISTA DE REGISTRO
                return View();
            }
            else
            {
                // SI EL USUARIO NO ESTA LOGUEADO LO REDIRECCIONA AL LOGIN
                return Redirect("/Login");
            }
            
        }

        // ESTE METODO ES UTILIZADO PARA OBTENER TODOS LOS PAISES
        public JsonResult Countrys()
        {
            // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
            using (DBEntities db = new DBEntities())
            {
                // SE REALIZA UNA CONSULTA A LA TABLA PAISES PARA OBTENER TODOS LOS PAISES
                var pais = (from p in db.paises
                            orderby p.pais1
                            select p).ToList();

                // SE RETORNA UN JSON CON TODOS LOS PAISES ENCONTRADOS EN LA CONSULTA ANTERIOR
                return Json(pais, JsonRequestBehavior.AllowGet);
            }
            
        }

        // ESTE METODO SE UTILIZA PARA GUARDAR UN CLIENTE
        // ESTE METODO RECIBE UN MODELO DE CLIENTE
        public async Task<JsonResult> SaveClient(cliente client)
        {
            // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
            using (DBEntities db = new DBEntities())
            {
                // SE VALIDA QUE LOS DATOS RECIBIDOS POR EL METODO ESTEN CORRECTOS
                if (ModelState.IsValid)
                {
                    // SE AGREGA EL MODELO RECIBIDO AL CONTEXTO
                    db.clientes.Add(client);

                    // SE GUARDAN LOS CAMBIOS DE FORMA ASINCRONICA
                    await db.SaveChangesAsync();

                    // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 200 ESPECIFICANDO QUE LA OPERACION SE REALIZO CORRECTAMENTE,
                    // TAMBIEN SE DEVUELVE EL ID DEL CLIENTE PARA SER UTILIZADO EN LA VISTA
                    return Json(new { status = 200, clientId = client.id }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // SE OBTIENEN LOS ERRORES DE VALIDACION DEL MODELO RECIBIDO
                    var errores = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));

                    // SE RETORNA UN OBJETO CON UN ESTADO 400 ESPECIFICANDO QUE HUBO UN ERROR, TAMBIEN SE RETORNA UN ARRAY CON LOS ERRORES ENCONTRADOS
                    return Json(new { status = 400, message = errores }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        // ESTE METODO ES UTILIZADO PARA GUARDAR DIRECCIONES EN LA VISTA DE NUEVO CLIENTE
        // RECIBE UN MODELO DE DIRECCIONES
        public async Task<JsonResult> SaveAdress(direccione address)
        {
            // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
            using (DBEntities db = new DBEntities())
            {
                // SE VALIDA QUE LOS DATOS RECIBIDOS POR EL METODO ESTEN CORRECTOS
                if (ModelState.IsValid)
                {
                    // SE AGREGA EL MODELO RECIBIDO AL CONTEXTO
                    db.direcciones.Add(address);

                    // SE GUARDAN LOS CAMBIOS DE FORMA ASINCRONICA
                    await db.SaveChangesAsync();

                    // SE BUSCA EN LA TABLA DIRECCIONES TODAS LAS DIRECCIONES RELACIONADAS AL ID DEL CLIENTE
                    var clientDireccions = (from d in db.direcciones
                                            join p in db.paises on d.id_pais equals p.id
                                            where d.id_cliente == address.id_cliente
                                            select new
                                            {
                                                d.id,
                                                d.line1,
                                                d.line2,
                                                d.sector,
                                                d.ciudad,
                                                pais = p.pais1,
                                                d.zipcode
                                            }).ToList();

                    // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 200 ESPECIFICANDO QUE LA OPERACION SE REALIZO CORRECTAMENTE,
                    // TAMBIEN SE LE RETORNA EN ESE OBJETO UNA LISTA DE DIRECCIONES ENCONTRADAS POR LA CONSULTA ANTERIOR
                    return Json(new { status = 200, direccions = clientDireccions }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // SE OBTIENEN LOS ERRORES DE VALIDACION DEL MODELO RECIBIDO
                    var errores = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));

                    // SE RETORNA UN OBJETO CON UN ESTADO 400 ESPECIFICANDO QUE HUBO UN ERROR, TAMBIEN SE RETORNA UN ARRAY CON LOS ERRORES ENCONTRADOS
                    return Json(new { status = 400, message = errores }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        // ESTE METODO ES UTILIZADO PARA FILTRAR EN LA PANTALLA PRINCIPAL
        // ESTE METODO RECIBE UN VALOR QUE CONTIENE EL ELEMENTO A FILTRAR
        public JsonResult filterClient(string filter)
        {
            // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
            using (DBEntities db = new DBEntities())
            {
                // SE CONSULTA EN LA TABLA CLIENTES TODO LO RELACIONADO CON EL VALOR DE FILTRADO QUE RECIBE ESTE METODO
                var result = (from c in db.clientes
                              where c.nombre.Contains(filter) || c.apellido.Contains(filter) || c.telefono.Contains(filter)
                              select c).ToList();

                // SE RETORNA EN FORMATO JSON TODOS LOS CLIENTES ENCONTRADOS POR LA CONSULTA ANTERIOR
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        // ESTE METODO SE UTILIZA PARA ELIMINAR UN CLIENTE
        // ESTE METODO RECIBE EL ID DEL CLIENTE
        public async Task<JsonResult> DeleteClient(int idClient)
        {
            // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
            using (DBEntities db = new DBEntities())
            {
                // SE CREA UN NUEVO MODELO CON EL ID RECIBIDO POR EL METODO
                var cliente = new cliente { id = idClient };

                // SE AGREGA EL MODELO CREADO AL CONTEXTO Y SE ESPECIFICA QUE EL ESTADO
                db.Entry(cliente).State = EntityState.Deleted;

                // SE GUARDAN LOS CAMBIOS DE FORMA ASINCRONICA
                var res = await db.SaveChangesAsync();

                // SI EL RESULTADO DE LA OPERACION ANTERIOR ES IGUAL A 1, TODO SALIO CORRECTO
                if (res == 1)
                {
                    // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 200 ESPECIFICANDO QUE LA OPERACION SE REALIZO CORRECTAMENTE
                    return Json(new { status = 200 });
                }
                else
                {
                    // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 400 ESPECIFICANDO QUE LA OPERACION NO SE REALIZO CORRECTAMENTE   
                    return Json(new { status = 400, message = "Algo salio mal, vuelva a intentarlo!" });
                }                
            }            
        }

        // ESTE METODO SE UTILIZA PARA ELIMINAR UNA DIRECCION
        public async Task<JsonResult> DeleteAddress(int idAddress)
        {
            // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
            using (DBEntities db = new DBEntities())
            {
                // SE CREA UN NUEVO MODELO CON EL ID RECIBIDO POR EL METODO
                var address = new direccione { id = idAddress };

                // SE AGREGA EL MODELO CREADO AL CONTEXTO Y SE ESPECIFICA QUE EL ESTADO
                db.Entry(address).State = EntityState.Deleted;

                // SE GUARDAN LOS CAMBIOS DE FORMA ASINCRONICA
                var res = await db.SaveChangesAsync();

                // SI EL RESULTADO DE LA OPERACION ANTERIOR ES IGUAL A 1, TODO SALIO CORRECTO
                if (res == 1)
                {
                    // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 200 ESPECIFICANDO QUE LA OPERACION SE REALIZO CORRECTAMENTE
                    return Json(new { status = 200 });
                }
                else
                {
                    // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 400 ESPECIFICANDO QUE LA OPERACION NO SE REALIZO CORRECTAMENTE   
                    return Json(new { status = 400, message = "Algo salio mal, vuelva a intentarlo!" });
                }
            }
        }

        // ESTE METODO SE UTILIZA PARA ELIMINAR UNA DIRECCION EN LA VISTA DEL REGISTRO
        // ESTE METODO RECIBE EL ID DE LA DIRECCION Y EL ID DEL CLIENTE
        public async Task<JsonResult> DeleteAddressRegistry(int idAddress, int clientID)
        {
            // SE CREA UN BLOQUE DE RECURSO PARA LUEGO DE FINALIZAR EL PROCESO SE LIBEREN LOS RECURSOS ADQUIRIDOS POR ESTE ALGORITMO
            using (DBEntities db = new DBEntities())
            {
                // SE CREA UN NUEVO MODELO CON EL ID RECIBIDO POR EL METODO
                var address = new direccione { id = idAddress };

                // SE AGREGA EL MODELO CREADO AL CONTEXTO Y SE ESPECIFICA QUE EL ESTADO
                db.Entry(address).State = EntityState.Deleted;

                // SE GUARDAN LOS CAMBIOS DE FORMA ASINCRONICA
                var res = await db.SaveChangesAsync();

                // SI EL RESULTADO DE LA OPERACION ANTERIOR ES IGUAL A 1, TODO SALIO CORRECTO
                if (res == 1)
                {
                    // SE OBTIENEN TODAS LAS DIRECCIONES RELACIONADAS AL ID DEL CLIENTE
                    var clientDireccions = (from d in db.direcciones
                                            where d.id_cliente == clientID
                                            select d).ToList();

                    // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 200 ESPECIFICANDO QUE LA OPERACION SE REALIZO CORRECTAMENTE
                    // TAMBIEN SE RETORNA UNA LISTA DE DIRECCIONES
                    return Json(new { status = 200, direccions = clientDireccions }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // SE LE RETORNA UN OBJETO CON UN NUMERO DE ESTADO 400 ESPECIFICANDO QUE LA OPERACION NO SE REALIZO CORRECTAMENTE   
                    return Json(new { status = 400, message = "Algo salio mal, vuelva a intentarlo!" });
                }
            }
        }

    }
}