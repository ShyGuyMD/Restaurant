using Dominio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Controladoras
{
    public class CDocumento
    {
        #region Singleton
        private static CDocumento _instancia = null;
        private static readonly object bloqueo = new Object();

        private CDocumento() { }

        public static CDocumento Get
        {
            get
            {
                lock (bloqueo)
                {
                    if (_instancia == null)
                        _instancia = new CDocumento();

                    return _instancia;
                }
            }
        }
        #endregion

        public Documento ArmarDocumento(string pNumDoc, string pTipoDoc)
        {
            Documento d = new Documento()
            {
                Numero = pNumDoc,
                TipoDoc = (Documento.TipoDocumento)Enum.Parse(typeof(Documento.TipoDocumento), pTipoDoc)
            };

            return d;
        }
    }
}
