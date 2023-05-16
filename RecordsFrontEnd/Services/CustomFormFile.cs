using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;


namespace RecordsFrontEnd.Services
{
    public class CustomFormFile 
    {
        private readonly IBrowserFile file;

        public CustomFormFile(IBrowserFile file)
        {
            this.file = file;
            ContentType = file.ContentType;
            ContentDisposition = $"form-data; name=\"{file.Name}\"; filename=\"{file.Name}\"";
            Headers = new HeaderDictionary();
            Length = file.Size;
            Name = file.Name;
            FileName = file.Name;
        }

        public Stream OpenReadStream()
        {
            return file.OpenReadStream();
        }

        public void CopyTo(Stream target)
        {
            using (var stream = file.OpenReadStream())
            {
                stream.CopyTo(target);
            }
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            using (var stream = file.OpenReadStream())
            {
                return stream.CopyToAsync(target, cancellationToken);
            }
        }

        public Task<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken = default)
        {
            using (var stream = file.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return Task.FromResult(memoryStream.ToArray());
            }
        }

        // Implement other members of the IFormFile interface as needed

        public long Length { get; }
        public string ContentType { get; }
        public string ContentDisposition { get; }
        public IHeaderDictionary Headers { get; }
        public string Name { get; }
        public string FileName { get; }
    
}
}
