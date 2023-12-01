using EmailManager.Grpc.Protos;

namespace WebScraper.Application.Services.GrpcServices;

public class EmailManagerGrpcService
{
    private readonly EmailProtoService.EmailProtoServiceClient _emailProtoServiceClient;

    public EmailManagerGrpcService(EmailProtoService.EmailProtoServiceClient emailProtoServiceClient)
    {
        _emailProtoServiceClient = emailProtoServiceClient;
    }

    public async Task<GetAllEmailsResponse> GetAllEmails(Empty request)
    {
        return await _emailProtoServiceClient.GetAllEmailsAsync(request);
    }
}