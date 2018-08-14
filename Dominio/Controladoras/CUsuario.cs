using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using static Helpers.Utils;

namespace Dominio.Controladoras
{
    [Serializable]
    public class CUsuario : ISerializable
    {
        #region Singleton
        private static CUsuario _instancia = null;
        private static readonly object bloqueo = new Object();

        private CUsuario() {
            _Usuarios = new List<Usuario>();
            _Chef = new List<Chef>();
        }

        public static CUsuario Get
        {
            get
            {
                lock (bloqueo)
                {
                    if (_instancia == null)
                        _instancia = new CUsuario();

                    return _instancia;
                }
            }
        }
        #endregion

        public List<Usuario> _Usuarios { get; set; }
        public List<Chef> _Chef { get; set; }

        public ExitCode AltaAdmin(string pUsername, string pPassword, Usuario.Rol pRol)
        {
            var exit = ExitCode.EXISTING_USER_ERROR;
            Usuario u = BuscarUsuario(pUsername);

            if (u == null)
            {
                Usuario user = new Usuario()
                {
                    Username = pUsername,
                    Password = pPassword,
                    UserRole = pRol
                };
                _Usuarios.Add(user);
                exit = ExitCode.OK;
            }

            return exit;
        }

        public ExitCode AltaChef(string pUsername, string pPassword, Usuario.Rol pRol, Documento pDocumento, string pNombre, string pApellido, decimal pSueldo)
        {
            var exit = ExitCode.EXISTING_USER_ERROR;

            if (ValidarData(pUsername, pPassword, pRol, pDocumento, pNombre, pApellido, pSueldo))
            {
                Chef c = BuscarChef(pDocumento);
                if (c == null)
                {
                    Usuario u = BuscarUsuario(pUsername);
                    if (u == null)
                    {
                        c = new Chef()
                        {
                            Username = pUsername,
                            Password = pPassword,
                            Nombre = pNombre,
                            Apellido = pApellido,
                            Documento = pDocumento,
                            UserRole = pRol,
                            Sueldo = pSueldo,
                            FechaIngreso = DateTime.Today
                        };
                        _Chef.Add(c);
                        _Usuarios.Add(c);
                        exit = ExitCode.OK;
                    }
                }
            }
            else
                exit = ExitCode.INPUT_DATA_ERROR;

            return exit;
        }

        public Chef BuscarChef(Documento pDocumento)
        {
            Chef c = null;
            int contador = 0;

            while(c == null && contador < _Chef.Count)
            {
                if (_Chef[contador].Documento.Equals(pDocumento))
                {
                    c = _Chef[contador];
                }
                contador++;
            }

            return c;
        }

        public Usuario BuscarUsuario(string pUsername)
        {
            Usuario u = null;
            int contador = 0;

            while (u == null && contador < _Usuarios.Count)
            {
                if (_Usuarios[contador].Username == pUsername)
                {
                    u = _Usuarios[contador];
                }
                contador++;
            }
            return u;
        }

        public ExitCode Login(string pUsername, string pPassword)
        {
            var exit = ExitCode.WRONG_USERNAME_PASSWORD_ERROR;

            if (ValidarData(pUsername, pPassword))
            {
                Usuario u = ValidarUsuario(pUsername, pPassword);

                if (u != null)
                    exit = ExitCode.OK;
            }
            else
                exit = ExitCode.INPUT_DATA_ERROR;

            return exit;
        }

        public Usuario.Rol RolAsociado(string pRol)
        {
            Usuario.Rol ret = (Usuario.Rol)Enum.Parse(typeof(Usuario.Rol), pRol);

            return ret;
        }

        public bool ValidarData(string pUsername, string pPassword)
        {
            return (pUsername != "" && pPassword != "");
        }

        public bool ValidarData(string pUsername, string pPassword, Usuario.Rol pRol, Documento pDocumento, string pNombre, string pApellido, decimal pSueldo)
        {
            return (pUsername != "" &&
                        pPassword != "" &&
                            pRol != null &&
                                pDocumento != null &&
                                    pNombre != "" &&
                                        pApellido != "" &&
                                            pSueldo > 0);
        }

        public Usuario ValidarUsuario(string pUsername, string pPassword)
        {
            Usuario u = null;
            int contador = 0;

            while (u == null && contador < _Usuarios.Count)
            {
                if (_Usuarios[contador].Username == pUsername && _Usuarios[contador].Password == pPassword)
                    u = _Usuarios[contador];

                contador++;
            }

            return u;
        }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("listaUsuarios", this._Usuarios, typeof(List<Usuario>));
            info.AddValue("listaChef", this._Chef, typeof(List<Chef>));
        }

        public CUsuario(SerializationInfo info, StreamingContext context)
        {
            this._Usuarios = info.GetValue("listaUsuarios", typeof(List<Usuario>)) as List<Usuario>;
            CUsuario._instancia = this;
            this._Chef = info.GetValue("listaChef", typeof(List<Chef>)) as List<Chef>;
            CUsuario._instancia = this;
        }
    }
}
