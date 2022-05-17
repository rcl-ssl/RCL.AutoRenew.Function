#nullable disable

using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using RCL.SDK;

namespace RCL.AutoRenew.Function
{
    public class CertificateService
    {
        private readonly ILogger _logger;
        private readonly ICertificateRequestService _certificateRequestService;

        public CertificateService(ILoggerFactory loggerFactory,
            ICertificateRequestService certificateRequestService)
        {
            _logger = loggerFactory.CreateLogger<CertificateRequestService>();
            _certificateRequestService = certificateRequestService;
        }

        [Function("Certificate-Test")]
        public async Task<HttpResponseData> RunTest([HttpTrigger(AuthorizationLevel.Function, "get", Route = "certificate/test")] HttpRequestData req)
        {
            _logger.LogInformation("Testing AutoRenew Function ...");

            try
            {
                await _certificateRequestService.GetTestAsync();

                _logger.LogInformation("TEST PASSED");

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "text/plain ; charset=utf-8");
                response.WriteString("TEST PASSED");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"TEST FAILED: {ex.Message}");

                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.Headers.Add("Content-Type", "text/plain ; charset=utf-8");
                response.WriteString($"TEST FAILED: {ex.Message}");
                return response;
            }
        }

        [Function("Certificate-Get-RenewList")]
        public async Task<HttpResponseData> RunGetRenewList([HttpTrigger(AuthorizationLevel.Function, "get", Route = "certificate/renew/getlist")] HttpRequestData req)
        {
            _logger.LogInformation("Getting certificates to renew ...");

            try
            {
                List<Certificate> certificates = await _certificateRequestService.GetCertificatesToRenewAsync();

                if (certificates?.Count > 0)
                {
                    _logger.LogInformation(JsonSerializer.Serialize(certificates));
                }
                else
                {
                    _logger.LogInformation("Did not find any certificates to renew.");
                }

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json ; charset=utf-8");
                response.WriteString(JsonSerializer.Serialize(certificates));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get certificates to renew, {ex.Message}");

                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.Headers.Add("Content-Type", "text/plain ; charset=utf-8");
                response.WriteString(ex.Message);
                return response;
            }
        }

        [Function("Certificate-Renew-Manual")]
        public async Task<HttpResponseData> RunRenewManual([HttpTrigger(AuthorizationLevel.Function, "get", Route = "certificate/renew/manual")] HttpRequestData req)
        {
            _logger.LogInformation("Getting certificates to renew ...");

            try
            {
                List<Certificate> certificates = await _certificateRequestService.GetCertificatesToRenewAsync();

                if (certificates?.Count > 0)
                {
                    _logger.LogInformation($"Found {certificates.Count} certificate(s) to renew");

                    foreach (Certificate cert in certificates)
                    {
                        await _certificateRequestService.RenewCertificateAsync(cert);

                        _logger.LogInformation($"Scheduled {cert.certificateName} for renewal");
                    }
                }
                else
                {
                    _logger.LogInformation("Did not find any certificates to renew.");
                }

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "text/plain ; charset=utf-8");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get certificates to renew, {ex.Message}");

                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.Headers.Add("Content-Type", "text/plain ; charset=utf-8");
                response.WriteString(ex.Message);
                return response;
            }
        }

        [Function("Certificate-Renew-Automatic")]
        public async Task RunRenewAutomatic([TimerTrigger("%CRON_EXPRESSION%")] MyInfo myTimer)
        {
            try
            {
                List<Certificate> certificates = await _certificateRequestService.GetCertificatesToRenewAsync();

                if(certificates?.Count > 0)
                {
                    foreach(Certificate cert in certificates)
                    {
                        await _certificateRequestService.RenewCertificateAsync(cert);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
