using System;
using System.Configuration;
using System.IO;
using Wipro.Support.JProperties;

namespace Wipro.Support.PageMapping.PageElementMaping
{
    class PageElementMapping
    {
        private static readonly String filenameMappingCP = ConfigurationManager.AppSettings["MappingFileLoc"];
        private static readonly String filenameMappingRP = ConfigurationManager.AppSettings["MappingFileLoc"];

        public static JavaPropertiesbits mappingObjectCP = new JavaPropertiesbits();
        public static JavaPropertiesbits mappingObjectRP = new JavaPropertiesbits();
        private static Stream stream = null;


        //##############################################################################################
        public static JavaPropertiesbits GetElementMappingObjectCP()
        {
            try
            {
                stream = new FileStream(filenameMappingCP, FileMode.Open);
                mappingObjectCP.Load(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return mappingObjectCP;
        }

        public static JavaPropertiesbits GetElementMappingObjectRP()
        {
            try
            {
                stream = new FileStream(filenameMappingRP, FileMode.Open);
                mappingObjectRP.Load(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return mappingObjectRP;
        }

        public static JavaPropertiesbits GetElementMappingObject(string filenameMapping)
        {
            try
            {
                stream = new FileStream(filenameMapping, FileMode.Open);
                mappingObjectRP.Load(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return mappingObjectRP;
        }

    }
}
