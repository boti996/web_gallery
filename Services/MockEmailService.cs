using System.Diagnostics;
using System.Threading.Tasks;

namespace web_gallery.Services
{
    public class MockEmailService : IEmailService
    {
        public async Task<IEmailServiceResponse> sendEmailMessage(string address, string content)
        {
            Debug.WriteLine($"Email was sent to address: {address}");
            Debug.WriteLine($"Content: {content}");
            return await Task.FromResult(new MockEmailServiceResponse { });
        }
    }

    public class MockEmailServiceResponse : IEmailServiceResponse {}
}