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
    }


}