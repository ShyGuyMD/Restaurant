using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    // Este método de encriptación NO ES SEGURO, y NO DEBE SER USADO fuera de una situación académica.
    public class Encryption
    {
        // En un ambiente de producción estos deberían ser generados, no hard-coded.
        private byte[] Key = { 183, 127, 131, 248, 189, 148, 21, 178, 161, 157, 119, 30, 15, 160, 136, 98, 210, 88, 90, 32, 121, 25, 30, 97, 124, 218, 161, 25, 164, 226, 184, 40 };
        private byte[] Vector = { 158, 234, 69, 173, 216, 117, 185, 103, 145, 141, 78, 77, 103, 255, 46, 55 };


        private ICryptoTransform encriptador, desencriptador;
        private UTF8Encoding UTFEncoder;

        public Encryption()
        {
            // Método de encriptación
            RijndaelManaged rm = new RijndaelManaged();

            // Crea un encriptador y un desencriptador usando la Clave y el Vector definidos
            encriptador = rm.CreateEncryptor(this.Key, this.Vector);
            desencriptador = rm.CreateDecryptor(this.Key, this.Vector);

            // Necesario para conocer como traducir los byte[] a String y viceversa.
            UTFEncoder = new UTF8Encoding();
        }

        // Encripta y devuelve el resultado como string (útil para URLs y guardar en base)
        public string EncryptToString(string TextValue)
        {
            return ByteArrToString(Encrypt(TextValue));
        }

        // Encripta texto como byte[]
        public byte[] Encrypt(string TextValue)
        {
            Byte[] bytes = UTFEncoder.GetBytes(TextValue);

            MemoryStream memoryStream = new MemoryStream();

            // Escribir desencriptado a MemoryStream
            CryptoStream cs = new CryptoStream(memoryStream, encriptador, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            

            // Leer salida encriptada de MemoryStream
            memoryStream.Position = 0;
            byte[] encrypted = new byte[memoryStream.Length];
            memoryStream.Read(encrypted, 0, encrypted.Length);
            
            // Cierra streams
            cs.Close();
            memoryStream.Close();

            return encrypted;
        }

        // Lo mismo, al revés
        public string DecryptString(string EncryptedString)
        {
            return Decrypt(StrToByteArray(EncryptedString));
        }

        public string Decrypt(byte[] EncryptedValue)
        {
            // Escribir encriptado
            MemoryStream encryptedStream = new MemoryStream();
            CryptoStream decryptStream = new CryptoStream(encryptedStream, desencriptador, CryptoStreamMode.Write);
            decryptStream.Write(EncryptedValue, 0, EncryptedValue.Length);
            decryptStream.FlushFinalBlock();

            // Leer desencriptado
            encryptedStream.Position = 0;
            Byte[] decryptedBytes = new Byte[encryptedStream.Length];
            encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
            encryptedStream.Close();

            return UTFEncoder.GetString(decryptedBytes);
        }

        // Convierte string a byte[]
        public byte[] StrToByteArray(string str)
        {
            if (str.Length == 0)
                throw new Exception("Invalid string value in StrToByteArray");

            byte val;
            byte[] byteArr = new byte[str.Length / 3];
            int i = 0, j = 0;
            do
            {
                val = byte.Parse(str.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            }
            while (i < str.Length);
            return byteArr;
        }

        // Convierte byte[] a string
        public string ByteArrToString(byte[] byteArr)
        {
            byte val;
            string tempStr = "";
            for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
            {
                val = byteArr[i];
                if (val < (byte)10)
                    tempStr += "00" + val.ToString();
                else if (val < (byte)100)
                    tempStr += "0" + val.ToString();
                else
                    tempStr += val.ToString();
            }
            return tempStr;
        }
    }
}