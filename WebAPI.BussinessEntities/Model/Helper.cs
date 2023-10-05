using System;
using System.Collections.Generic;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using Npgsql;

namespace WebAPI.BussinessEntities.Model
{
    public class Helper
    {
        public static string XmlGenerate<T>(T value)
        {
            string results = "";
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;

                using (StringWriter textWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, value);
                    }
                    results = Regex.Replace(textWriter.ToString(), @" xmlns:.*?"".*?""", "");
                    return results; //This is the output as a string
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex);
                return results;
            }
        }
        public static List<T> DataReaderMapToList<T>(NpgsqlDataReader dr)
        {
            List<T> list = new List<T>();
            try
            {
                T obj = default(T);
                while (dr.Read())
                {
                    obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex);
            }
            return list;
        }
    }
}
