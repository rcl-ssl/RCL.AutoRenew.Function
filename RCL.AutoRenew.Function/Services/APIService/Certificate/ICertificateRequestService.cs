namespace RCL.AutoRenew.Function
{
    public interface ICertificateRequestService
    {
        Task TestAsync();
        Task<List<Certificate>> GetCertificatesToRenew();
        Task RenewCertificate(Certificate certificate);
    }
}
