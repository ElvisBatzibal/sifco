using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace webapiapp.Class
{
    public class UploadedFile
    {
        string RutaFileAttch = @"C:\Project\sifco\Attached";

        public Models.ResponseDTO AddFile(Models.Appointments_attachedDTO value)
        {
            var respuesta = new Models.ResponseDTO();
            respuesta.Success = false;


            //Verifica Directorio
            if (!Directory.Exists(RutaFileAttch))
            {
                Directory.CreateDirectory(RutaFileAttch);
            }
            //Agregar en Directorio
            var FileName = Guid.NewGuid().ToString() + "" + value.FileType;
            string path = Path.Combine(RutaFileAttch, Path.GetFileName(FileName));
            if (!String.IsNullOrEmpty(FileName))
            {
                if (File.Exists(RutaFileAttch + "/" + FileName))
                {
                    File.Delete(RutaFileAttch + "/" + FileName);
                }
            }
            try
            {
                File.WriteAllBytes(path, value.Content);
                respuesta.Success = true;
                respuesta.InternalKey = FileName;
            }
            catch (Exception)
            {

                throw;
            }


            return respuesta;
        }

        public byte[] GetFile(string FileName)
        {

            MemoryStream stream = new MemoryStream();
            try
            {
                FileStream filestream = null;
                string FilePath = RutaFileAttch + "/" + FileName;
                filestream = System.IO.File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                filestream.CopyTo(stream);
                filestream.Flush();
                filestream.Dispose();
                filestream.Close();
                return stream.ToArray();

            }
            catch (Exception ex)
            {

            }
            return null;

        }
    }
}