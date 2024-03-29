﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Starter.Serialization.Implementation
{
    internal class XmlSerialization<T> : ISerialization<T>
    {
        private XmlSerializer _serializer;
        private XmlWriterSettings _xmlWriterSettings;
        public XmlSerialization()
        {
            _xmlWriterSettings = new XmlWriterSettings() { Indent = true };
            _serializer = new XmlSerializer(typeof(List<T>));
        }

        public async Task Serialization(string filePath, object data)
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