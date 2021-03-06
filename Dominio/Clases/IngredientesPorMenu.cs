﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Serializable]
    public class IngredientesPorMenu
    {
        public Ingrediente Ingrediente { get; set; }
        public int Cantidad { get; set; }
        public string IngredienteDescripcion { get { return Ingrediente.Descripcion; } }
        public string IngredienteCodigo { get { return Ingrediente.Codigo; } }

        public IngredientesPorMenu() { }

        public decimal CalcularCostoIngrediente()
        {
            return Ingrediente.Costo * Cantidad;
        }
    }
}
