using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Fecha
    {
        #region Propiedades
        public DateTime FechaDateTime { get; set; }
        public string Text { get; set; }
        #endregion

        #region Propiedades Dinámicas
        public string FechaText
        {
            get
            {
                if (string.IsNullOrEmpty(Text))
                    return FechaDateTime.ToString("dd/MM/yyyy");
                else
                    return Text;
            }
        }

        public string FechaJson
        {
            get
            {
                if (string.IsNullOrEmpty(Text))
                {
                    DateTime dateUtc = FechaDateTime.ToUniversalTime();
                    return GetSerializedString(dateUtc);
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion

        #region Métodos
        private string GetSerializedString(DateTime dt)
        {

            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                Culture = new CultureInfo("es-MX")
            };
            string microsoftJson = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
            return microsoftJson;
        }
        #endregion

        #region Métodos Estáticos
        public static List<Fecha> GetFechas(List<DateTime> lstDates)
        {
            List<Fecha> result = new List<Fecha>();
            foreach (DateTime date in lstDates)
            {
                result.Add(new Fecha() { FechaDateTime = date, Text = string.Empty });
            }
            return result;
        }

        public static DateTime GetDateTime(string date)
        {
            int year = Convert.ToInt32(date.Substring(6, 4));
            int month = Convert.ToInt32(date.Substring(3, 2));
            int day = Convert.ToInt32(date.Substring(0, 2));
            return new DateTime(year, month, day);
        }
        #endregion
    }
}