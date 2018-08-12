﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Utils
    {
        public Random Random { get; set; }

        #region Singleton
        private static Utils _instancia = null;
        private static readonly object bloqueo = new Object();

        private Utils() { this.Random = new Random(); }

        public static Utils Get
        {
            get
            {
                lock (bloqueo)
                {
                    if (_instancia == null)
                        _instancia = new Utils();

                    return _instancia;
                }
            }
        }
        #endregion

        public string GenerarCodigo(int largo)
        {
            var charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[largo];
            var random = this.Random;

            for (int i = 0; i < stringChars.Length; i++)
                stringChars[i] = charSet[random.Next(charSet.Length)];

            return new string(stringChars);
        }
    }
}