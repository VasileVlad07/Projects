using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace EvidentaAmenzi
{
    public static class SaveData
    {
        
        public static void Save(object data, string saveFile)
        {
            try
            {
                FileStream fileStream;
                BinaryFormatter formatter = new BinaryFormatter();

                if (File.Exists(saveFile))
                    File.Delete(saveFile);
                fileStream = File.Create(saveFile);
                formatter.Serialize(fileStream, data);
                fileStream.Close();
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Nu s-a putut serializa!");
                Console.WriteLine(e.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("S-a produs o exceptie IO, nu s-a putut serializa!");
                Console.WriteLine(ex.Message);
            }
            
        }

        public static AppLogic Load(string saveFile)
        {
            AppLogic obj = new AppLogic();

            FileStream fileStream;
            BinaryFormatter formatter = new BinaryFormatter();

            if (File.Exists(saveFile))
            {
                fileStream = File.OpenRead(saveFile);
                obj = formatter.Deserialize(fileStream) as AppLogic;
                fileStream.Close();
            }

            return obj;
        }
    }

}
