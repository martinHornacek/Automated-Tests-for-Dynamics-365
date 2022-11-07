using OpportunityManagement.SchemaTests.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace OpportunityManagement.SchemaTests.Logic
{
    public class Solution
    {
        private IDictionary<string, EntityMetadata> _entities;
        private DirectoryInfo _solutionDirectory;

        public Solution(string solutionDirectoryPath)
        {
            if (string.IsNullOrEmpty(solutionDirectoryPath)) throw new ArgumentNullException(solutionDirectoryPath);
            if (!Directory.Exists(solutionDirectoryPath)) throw new DirectoryNotFoundException($"Solution directory '{solutionDirectoryPath}' not found.");

            _solutionDirectory = new DirectoryInfo(solutionDirectoryPath);
        }

        public IDictionary<string, EntityMetadata> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = LoadEntityXmlFiles();
                }

                return _entities;
            }
        }

        private IDictionary<string, EntityMetadata> LoadEntityXmlFiles()
        {
            return _solutionDirectory.GetFiles("Entity.xml", SearchOption.AllDirectories)
                                     .AsParallel()
                                     .Select(file =>
                                     {
                                         var xml = new XmlDocument();
                                         xml.Load(file.FullName);
                                         return ParseEntityXml(xml);
                                     })
                                    .ToDictionary(entity => entity.Name);
        }

        private AttributeMetadata ParseAttributeXml(XmlNode attributeXml, string entityLogicalName)
        {
            if (attributeXml == null) return null;

            var description = attributeXml.SelectSingleNode("./Descriptions/Description[@languagecode='1033']/@description")?.Value;
            var displayName = attributeXml.SelectSingleNode("./displaynames/displayname[@languagecode='1033']/@description")?.Value;
            var logicalName = attributeXml.SelectSingleNode("./LogicalName").InnerText;
            var type = attributeXml.SelectSingleNode("./Type").InnerText;

            return new AttributeMetadata
            {
                Description = description,
                DisplayName = displayName,
                EntityLogicalName = entityLogicalName,
                LogicalName = logicalName,
                Type = type
            };
        }

        private EntityMetadata ParseEntityXml(XmlDocument entityXml)
        {
            if (entityXml == null) return null;

            var name = entityXml.SelectSingleNode("./Entity/Name").InnerText.ToLower();
            var objectTypeCode = int.Parse(entityXml.SelectSingleNode("./Entity/ObjectTypeCode").InnerText);
            var attributes = entityXml.SelectNodes("./Entity/EntityInfo/entity/attributes/attribute");

            return new EntityMetadata
            {
                Name = name,
                ObjectTypeCode = objectTypeCode,
                Attributes = attributes.Cast<XmlNode>().Select(attr => ParseAttributeXml(attr, name)).ToList()
            };
        }
    }
}