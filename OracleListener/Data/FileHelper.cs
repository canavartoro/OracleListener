using OracleListener.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace OracleListener.Data
{
    public class FileHelper
    {
        public static string ToXml(object serilizeObject)
        {
            if (object.ReferenceEquals(serilizeObject, null)) return string.Empty;
            var xmlstring = "";

            try
            {
                XmlSerializer serializer = new XmlSerializer(serilizeObject.GetType());
                MemoryStream memoryStream = new MemoryStream();
                StreamWriter streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);
                serializer.Serialize(streamWriter, serilizeObject);
                memoryStream.Seek(0, SeekOrigin.Begin);
                StreamReader streamReader = new StreamReader(memoryStream, System.Text.Encoding.UTF8);
                xmlstring = streamReader.ReadToEnd();

                //XmlSerializer xmlSerializer = new XmlSerializer(serilizeObject.GetType());
                //using (var sww = new StringWriter())
                //{
                //    using (XmlWriter writer = XmlWriter.Create(sww))
                //    {
                //        xmlSerializer.Serialize(writer, serilizeObject);
                //        xmlstring = sww.ToString();
                //    }
                //}
            }
            catch (IOException ioexc)
            {
                Logger.E(ioexc);
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }

            return xmlstring;
        }

        public static object FromXml(string xmlstr, Type type)
        {
            if (string.IsNullOrEmpty(xmlstr)) return null;

            try
            {
                XmlSerializer deserializer = new XmlSerializer(type);
                StringReader streamreader = new StringReader(xmlstr);
                XmlReader xmlreader = new XmlTextReader(streamreader);
                if (deserializer.CanDeserialize(xmlreader))
                {
                    return deserializer.Deserialize(xmlreader);
                }
            }
            catch (IOException ioexc)
            {
                Logger.E(ioexc);
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }

            return null;
        }

        public static void SaveFile(string filename, string data)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string fullName = string.Format("{0}\\{1}", System.Windows.Forms.Application.StartupPath, filename);

                using (FileStream file = new FileStream(fullName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (StreamWriter write = new StreamWriter(file, Encoding.Default))
                {
                    write.WriteLine(data);
                    write.Flush();
                    write.Close();
                    file.Close();
                }
            }
            catch (IOException ioexc)
            {
                Logger.E(ioexc);
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static void SaveFilefromResource(string filename, string targetfile)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Assembly assembly = Assembly.GetCallingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(filename))
                {
                    using (BinaryReader binaryReader = new BinaryReader(stream))
                    {
                        byte[] buffer = binaryReader.ReadBytes((int)stream.Length);
                        using (FileStream fileStream = new FileStream(string.Format("{0}\\{1}", System.Windows.Forms.Application.StartupPath, targetfile), FileMode.OpenOrCreate))
                        {
                            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                            {
                                binaryWriter.Write(buffer);
                                binaryWriter.Flush();
                            }
                        }
                    }
                }
            }
            catch (IOException ioexc)
            {
                Logger.E(ioexc);
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static string ReadFile(string filename)
        {
            string data = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string fullName = string.Format("{0}\\{1}", System.Windows.Forms.Application.StartupPath, filename);

                if (File.Exists(fullName))
                {
                    using (FileStream file = new FileStream(fullName, FileMode.Open, FileAccess.Read))
                    using (StreamReader reader = new StreamReader(file, Encoding.Default))
                    {
                        data = reader.ReadToEnd();
                        reader.Close();
                        file.Close();
                    }
                }
            }
            catch (IOException ioexc)
            {
                Logger.E(ioexc);
            }
            catch (Exception exc)
            {
               Logger.E(exc);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return data;
        }

        public static void DeleteFile(string filename)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string fullName = string.Format("{0}\\{1}", System.Windows.Forms.Application.StartupPath, filename);
                if (File.Exists(fullName))
                {
                    File.Delete(fullName);
                }
            }
            catch (IOException ioexc)
            {
                Logger.E(ioexc);
            }
            catch (Exception exc)
            {
               Logger.E(exc);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
