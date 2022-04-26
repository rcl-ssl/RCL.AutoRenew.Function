#nullable disable

namespace RCL.AutoRenew.Function
{
    public class Certificate 
    {
        public string certificateName { get; set; }
        public string rootDomain { get; set; }
        public string email { get; set; }
        public string challengeType { get; set; }
        public string orderUri { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime expiryDate { get; set; }
        public string target { get; set; }
        public string renewal { get; set; }
        public int id { get; set; }
        public int subscriptionId { get; set; }
        public string password { get; set; }
        public string pfxString { get; set; }
        public CertificateDownloadUrl certificateDownloadUrl { get; set; }
        public string azureSubscriptionId { get; set; }
        public string dnsZoneResourceGroup { get; set; }
        public string keyVaultName { get; set; }
        public string keyVaultCertificateName { get; set; }
        public int siteId { get; set; }
    }

    public class CertificateDownloadUrl
    {
        public string pemUrl { get; set; }
        public string pfxUrl { get; set; }
        public string cerUrl { get; set; }
        public string crtUrl { get; set; }
        public string txtUrl { get; set; }

        public string keyUrl { get; set; }
        public string certCrtUrl { get; set; }
        public string cabundleCrtUrl { get; set; }
        public string fullchainCrtUrl { get; set; }
    }
}
