namespace RCL.AutoRenew.Function
{
    public interface ICertificateRequestService
    {
        Task GetTestAsync();
        Task<List<Certificate>> GetCertificatesToRenew();
        Task RenewCertificate(Certificate certificate);
    }
}
