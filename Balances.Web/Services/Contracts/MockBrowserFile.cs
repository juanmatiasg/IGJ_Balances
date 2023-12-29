using Microsoft.AspNetCore.Components.Forms;

namespace Balances.Web.Services.Contracts
{
    public class MockBrowserFile:IBrowserFile
    {

        private readonly byte[] content;

        public MockBrowserFile(string name, byte[] content)
        {
            this.content = content ?? throw new ArgumentNullException(nameof(content));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string ContentType { get; set; } = "application/octet-stream";

        public string Name { get; }

        public long Size => content.Length;

        public DateTimeOffset LastModified => DateTimeOffset.UtcNow; 

        public Task CopyToAsync(Stream stream)
        {
            return stream.WriteAsync(content, 0, content.Length);
        }

        public Task CopyToAsync(Stream stream, CancellationToken cancellationToken)
        {
            return stream.WriteAsync(content, 0, content.Length, cancellationToken);
        }


        public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
        {
            return new MemoryStream(content);
        }
    }
}
