using GuardianBagPack.Model.Auth;
using GuardianBagPack.Model.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GuardianBagPack.Model
{
    public class ConfigManager : IConfigManager
    {
        private static readonly string AUTH_FILE_NAME = "Auth.xml";
        private static readonly Lazy<ConfigManager> _instance = new Lazy<ConfigManager>(() => new ConfigManager(), true);
        public static IConfigManager Instance { get => _instance.Value; }

        public ConfigManager() { }

        public void LoadAuth()
        {
            FileInfo auth = new FileInfo(AUTH_FILE_NAME);
            if (!auth.Exists)
            {
                auth.Create().Close();
                AuthentificationProcess.Auth = new Authentification();
            }
            else
            {
                AuthentificationProcess.Auth = XmlDeSerialization<Authentification>(AUTH_FILE_NAME);
            }
        }

        public void WriteAuth()
        {

            FileInfo auth = new FileInfo(AUTH_FILE_NAME);
            if (!auth.Exists)
            {
                auth.Create().Close();
            }
            XmlSerialization(AuthentificationProcess.Auth, AUTH_FILE_NAME);
        }
        private void XmlSerialization<T>(T obj, string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextWriter streamWriter = new StreamWriter(fileName);
            xmlSerializer.Serialize(streamWriter, obj);
            streamWriter.Close();
        }
        private T XmlDeSerialization<T>(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StreamReader streamReader = new StreamReader(fileName);
            T result = (T)xmlSerializer.Deserialize(streamReader);
            streamReader.Close();
            return result;
        }
    }
}
