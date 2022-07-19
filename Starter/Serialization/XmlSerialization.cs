using Starter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Starter.Serialization
{
    internal class XmlSerialization<T> : IDisposable
    {
        private XmlSerializer _serializer;
        private XmlWriterSettings _xmlWriterSettings;
        public XmlSerialization()
        {
            _xmlWriterSettings = new XmlWriterSettings() { Indent = true };
            _serializer = new XmlSerializer(typeof(T));
        }

        public void Export(string filePath, T data)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(filePath, _xmlWriterSettings))
                    {
                        _serializer.Serialize(xmlWriter, data);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Dispose()
        {
            try
            {
        
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}