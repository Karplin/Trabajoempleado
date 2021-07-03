using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trabajoempleados.Models;


namespace Trabajoempleados.Models
{
    public class combinados: BaseModelo
    {
        public List<EMPLEOS> Empleos {get; set;}
        public CATEGORIA Categoria { get; set; }
        public int idcontracheck { get; set; }
        public List<busca_categoria_Result> cat { get; set; }
        public IEnumerable<busca_categoria_Result> buscarcat{ get; set; }
    }


}