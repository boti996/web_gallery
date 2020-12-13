using System.Threading.Tasks;

namespace web_gallery.Services
{
    public interface IEmailService
    {
        Task<IEmailServiceResponse> sendEmailMessage(string address, string content);
    }

    public interface IEmailServiceResponse {}
}