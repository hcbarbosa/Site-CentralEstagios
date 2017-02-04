using CentralEstagios.Models;
using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CentralEstagios.WebServices
{
    public partial class Cursos : System.Web.UI.Page
    {
        Repositorio<Curso> dbCurso = new Repositorio<Curso>();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Curso> listaCursos = dbCurso.Obter().Where(p => p.Status != 0).ToList();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(listaCursos);
            Response.Write(strJson);
        }
    }
}