using Binder.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Http;
using Titanium.Web.Proxy.Models;

namespace Binder.Core
{
    public class ProxyManager
    {
        private readonly ProxyServer _server;
        private ExplicitProxyEndPoint _explicitEndPoint;
        public List<Rule> Rules { get; set; }
        public ProxyManager()
        {
            this._server = new ProxyServer();
        }    
        public void Start()
        {
            _server.CertificateManager.CreateRootCertificate();
            _server.CertificateManager.TrustRootCertificate(true);
            _server.BeforeRequest += OnRequest;
            _server.BeforeResponse += OnResponse;
            _server.ServerCertificateValidationCallback += OnCertificateValidation;
            _server.ClientCertificateSelectionCallback += OnCertificateSelection;
            _explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, true)
            {
            };
            _explicitEndPoint.BeforeTunnelConnectRequest += OnBeforeTunnelConnectRequest;

            _server.AddEndPoint(_explicitEndPoint);
            _server.Start();
            _server.SetAsSystemProxy(_explicitEndPoint, ProxyProtocolType.AllHttp);

            var transparentEndPoint = new TransparentProxyEndPoint(IPAddress.Any, 8001, true)
            {
                GenericCertificateName = "google.com"
            };

            _server.AddEndPoint(transparentEndPoint);

            _server.SetAsSystemHttpProxy(_explicitEndPoint);
            _server.SetAsSystemHttpsProxy(_explicitEndPoint);
        }
        public void Stop()
        {
            _server.BeforeRequest -= OnRequest;
            _server.BeforeResponse -= OnResponse;
            _server.ServerCertificateValidationCallback -= OnCertificateValidation;
            _server.ClientCertificateSelectionCallback -= OnCertificateSelection;
            _server.Stop();
        }
        private async Task OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
        {
            if (Rules == null)
                Rules = new List<Rule>();
            string hostname = e.HttpClient.Request.RequestUri.Host;
            if (Rules.Where(x=>x.Site.Contains(hostname)).Count() > 0)
                e.DecryptSsl = true;

        }
        private async Task OnRequest(object sender, SessionEventArgs e)
        {
            if (Rules == null)
                Rules = new List<Rule>();
            foreach (Rule rule in Rules)
            {
                if (rule.Contains)
                {
                    if (e.HttpClient.Request.RequestUri.AbsoluteUri.Contains(rule.Site))
                    {
                        switch (rule.Type)
                        {
                            case Models.Enum.ContentType.Download:
                                try {
                                    e.Ok(new System.Net.WebClient().DownloadString(rule.Content));
                                } catch { }
                                break;
                            case Models.Enum.ContentType.Redirect:
                                e.Redirect(rule.Content);
                                break;
                            case Models.Enum.ContentType.Replace:
                                e.Ok(rule.Content);
                                break;
                        }
                    }
                }
                else
                {
                    if (e.HttpClient.Request.RequestUri.AbsoluteUri == rule.Site)
                    {
                        switch (rule.Type)
                        {
                            case Models.Enum.ContentType.Download:
                                try {
                                    e.Ok(new System.Net.WebClient().DownloadString(rule.Content));
                                } catch { }
                                break;
                            case Models.Enum.ContentType.Redirect:
                                e.Redirect(rule.Content);
                                break;
                            case Models.Enum.ContentType.Replace:
                                e.Ok(rule.Content);
                                break;
                        }
                    }
                }
            }
        }
        public async Task OnResponse(object sender, SessionEventArgs e)
        {
            if (e.HttpClient.Request.Method == "GET" || e.HttpClient.Request.Method == "POST")
            {
                if (e.HttpClient.Response.StatusCode == 200)
                {
                    if (e.HttpClient.Response.ContentType != null && e.HttpClient.Response.ContentType.Trim().ToLower().Contains("text/html"))
                    {
                        byte[] bodyBytes = await e.GetResponseBody();
                        e.SetResponseBody(bodyBytes);

                        string body = await e.GetResponseBodyAsString();
                        e.SetResponseBodyString(body);
                    }
                }
            }

            if (e.UserData != null)
            {
                var request = (Request)e.UserData;
            }
        }
        private Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                e.IsValid = true;

            return Task.CompletedTask;
        }
        private Task OnCertificateSelection(object sender, CertificateSelectionEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
