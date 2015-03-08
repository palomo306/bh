using BHermanos.Zonificacion.BusinessEntities;
using BHermanos.Zonificacion.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace BHermanos.Zonificacion.Seguridad
{
    public abstract class BaseUsuarios : IDisposable
    {

        #region Atributos
        
        protected string vector = "M@5RC@3@2L@1G@4-";
        protected DataAccessDataContext oDataAccess;

        #endregion

        #region Constructores

        public BaseUsuarios()
        {
            try
            {
                string connString = ConfigurationManager.AppSettings["ConexionZonificacion"].ToString();
                oDataAccess = new DataAccessDataContext(connString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Metodos

        protected string Encrypt(string texto, string password, string vectorInicializacion)
        {
            int passwordIterations = 4;
            int keySize = 256;
            string hashAlgorithm = "SHA1";
            string saltValue = "BHermanos.Zonificacion.Seguridad";
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(vectorInicializacion);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(texto);


            PasswordDeriveBytes passwordDB = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = passwordDB.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }

        protected string Decrypt(string texto, string password, string vectorInicializacion)
        {
            int passwordIterations = 4;
            int keySize = 256;
            string hashAlgorithm = "SHA1";
            string saltValue = "BHermanos.Zonificacion.Seguridad";
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(vectorInicializacion);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes = Convert.FromBase64String(texto);


            PasswordDeriveBytes passwordDB = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = passwordDB.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();

            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            return plainText;
        }


        protected virtual List<Menu> TransformaMenus(List<Menu> menus)
        {
            List<int> dependencias = new List<int>();
            List<Menu> lm = new List<Menu>();
            dependencias = (menus.Select(d => d.Dependencia).Distinct()).ToList<int>();
            foreach (int d in dependencias)
            {
                this.AsignaMenusXDependencias(menus, ref lm, d);
            }
            return lm;
        }

        protected virtual void AsignaMenusXDependencias(List<Menu> menus, ref List<Menu> lm, int depende)
        {
            List<Menu> selectDependencias = menus.Where(m => m.Dependencia == depende).ToList<Menu>();
            Menu selectMenu = menus.Where(m => m.Id == depende).FirstOrDefault();
            if (selectDependencias != null)
            {
                if (selectMenu == null)
                {
                    lm = selectDependencias;
                }
                else
                {
                    Menu menu = lm.Where(m => m.Id == depende).FirstOrDefault();
                    if (menu != null)
                        menu.ListMenus = selectDependencias;
                }

            }
        }

        public void Dispose()
        {
            if (oDataAccess != null)
                oDataAccess.Dispose();
        }

        #endregion

    }
}