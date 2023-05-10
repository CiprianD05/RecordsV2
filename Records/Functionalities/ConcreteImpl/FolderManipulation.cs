using Records.Functionalities.Interfaces;
using RecordsModels;

using System.Text;

namespace Records.Functionalities.ConcreteImpl
{
    public class FolderManipulation : IFolderManipulation
    {
        private IWebHostEnvironment _webHostEnvironment;

        public FolderManipulation( IWebHostEnvironment env)
        {
            _webHostEnvironment = env;
        }

        public string CreateFoldersForDocument(Document doc)
        {
            var pathBuilder=new StringBuilder(_webHostEnvironment.ContentRootPath + @"\PDFs\");
            var dirInfo = new DirectoryInfo(pathBuilder.ToString());
            
            if(!Directory.Exists(pathBuilder.ToString()+doc.Citizen.Id))
            {
                Directory.CreateDirectory(pathBuilder.ToString() + doc.Citizen.Id);
            }
            pathBuilder.Append(doc.Citizen.Id+@"\");

            dirInfo = new DirectoryInfo(pathBuilder.ToString());

            if (!Directory.Exists(pathBuilder.ToString() + doc.DocumentType.Id))
            {
                Directory.CreateDirectory(pathBuilder.ToString() + doc.DocumentType.Id);
            }
            pathBuilder.Append(doc.DocumentType.Id + @"\");

            return pathBuilder.ToString();
        }
    }
}
