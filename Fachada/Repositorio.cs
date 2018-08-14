﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Controladoras;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Aplicacion
{
    [Serializable]
    public class Repositorio
    {
        private string rutaArchivo;
        private CMenu cMenu;
        private CIngrediente cIngrediente;
        private CDocumento cDocumento;
        private CMesa cMesa;
        private CReserva cReserva;
        private CUsuario cUsuario;

        public Repositorio()
        {
            FileStream fs = new FileStream(rutaArchivo, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, this);
            fs.Close();
        }
        public void Serializable()
        {
            FileStream fs = new FileStream(rutaArchivo, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, this);
            fs.Close();
        }


        public void Deserializable()
        {
            FileStream fs = new FileStream(rutaArchivo, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            Repositorio rep = bf.Deserialize(fs) as Repositorio;
            fs.Close();
        }

    }

}