using System;
using System.IO;

namespace Comunes
{
    public class ManejoArchivos
    {
        //Metodo para convertir un archivo en un arreglo de bytes
        public byte[] fileToBinary(String rutaArchivo)
        {
            FileStream stream;
            stream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            byte[] archivo = reader.ReadBytes((int)stream.Length);
            reader.Close();
            stream.Close();

            return archivo;
        }

        //Metodo para convertir un arreglo de bytes en un archivo
        public void binaryToFile(Byte[] flujoBytes, String ruta)
        {
            FileStream fichero = File.Create(ruta);
            fichero.Write(flujoBytes, 0, flujoBytes.Length);
            fichero.Close();
        }

    }
}
