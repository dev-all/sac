using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Web.Mvc;

namespace SAC.Models.Afip
{
    public class ClaseLoginAfip
    {
        private static UInt32 _globalId = 0;

        public string serv { get; set; }
        public string url { get; set; }
        private string cert_path;
        private SecureString clave;

        private XmlDocument XmlLoginTicketRequest;
        private XmlDocument XmlLoginTicketResponse;
        private UInt32 uniqueId;
        public DateTime GenerationTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool Logeado
        {
            get
            {
                return Token == "";
            }
        }

        public X509Certificate2 certificado { get; set; }

        public XDocument XDocRequest { get; set; }
        public XDocument XDocResponse { get; set; }

        public string Token { get; set; }
        public string Sign { get; set; }

        // CONSTRUCTOR 
        public ClaseLoginAfip(string serv, string url, string cert_path, string clave)
        {
            this.serv = serv;
            this.url = url;
            this.cert_path = cert_path;
            this.clave = new SecureString();
            foreach (char character in clave)
                this.clave.AppendChar(character);
            this.clave.MakeReadOnly();
        }


        public void LoginSinWs()
        {
            string cmsFirmadoBase64;
            //string loginTicketResponse;

            XmlNode uniqueIdNode;
            XmlNode generationTimeNode;
            XmlNode ExpirationTimeNode;
            XmlNode ServiceNode;

            // Preparo el XML Request
            XmlLoginTicketRequest = new XmlDocument();
            XMLLoader.loadTemplate(XmlLoginTicketRequest, "LoginTemplate");

            uniqueIdNode = XmlLoginTicketRequest.SelectSingleNode("//uniqueId");
            generationTimeNode = XmlLoginTicketRequest.SelectSingleNode("//generationTime");
            ExpirationTimeNode = XmlLoginTicketRequest.SelectSingleNode("//expirationTime");
            ServiceNode = XmlLoginTicketRequest.SelectSingleNode("//service");
            generationTimeNode.InnerText = DateTime.Now.AddMinutes(-10).ToString("s");
            ExpirationTimeNode.InnerText = DateTime.Now.AddMinutes(+10).ToString("s");
            uniqueIdNode.InnerText = System.Convert.ToString(_globalId);
            ServiceNode.InnerText = serv;

           
            certificado = new X509Certificate2();
            if (clave.IsReadOnly())
                certificado.Import(File.ReadAllBytes(cert_path), clave, X509KeyStorageFlags.PersistKeySet);
            else
                certificado.Import(File.ReadAllBytes(cert_path));

            byte[] msgBytes = Encoding.UTF8.GetBytes(XmlLoginTicketRequest.OuterXml);

            // Firmamos

            //var contentInfo = new System.Security.Cryptography.Pkcs.ContentInfo(data);
            ContentInfo infoContenido = new ContentInfo(msgBytes);
            SignedCms cmsFirmado = new SignedCms(infoContenido);

            CmsSigner cmsFirmante = new CmsSigner(certificado);
            cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;
            cmsFirmado.ComputeSignature(cmsFirmante);
            cmsFirmadoBase64 = Convert.ToBase64String(cmsFirmado.Encode());

            Token = Token;
            Sign = Sign;


        }

        public void hacerLogin()
        {
            string cmsFirmadoBase64;
            string loginTicketResponse;

            XmlNode uniqueIdNode;
            XmlNode generationTimeNode;
            XmlNode ExpirationTimeNode;
            XmlNode ServiceNode;

            // ESTE CODIGO LO AGREGO PORQUE NO TOMA EL PKCS12 Y DA ERROR "No se puede crear un canal seguro SSL/TLS"
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // ---------------------------------------------------------------------------------------------------
            try
            {
                    ClaseLoginAfip._globalId += 1;

                    // Preparo el XML Request
                    XmlLoginTicketRequest = new XmlDocument();
                    XMLLoader.loadTemplate(XmlLoginTicketRequest, "LoginTemplate");

                    uniqueIdNode = XmlLoginTicketRequest.SelectSingleNode("//uniqueId");
                    generationTimeNode = XmlLoginTicketRequest.SelectSingleNode("//generationTime");
                    ExpirationTimeNode = XmlLoginTicketRequest.SelectSingleNode("//expirationTime");
                    ServiceNode = XmlLoginTicketRequest.SelectSingleNode("//service");
                    generationTimeNode.InnerText = DateTime.Now.AddMinutes(-10).ToString("s");
                    ExpirationTimeNode.InnerText = DateTime.Now.AddMinutes(+10).ToString("s");
                    uniqueIdNode.InnerText = System.Convert.ToString(_globalId);
                    ServiceNode.InnerText = serv;

                    // Obtenemos el Cert
                    certificado = new X509Certificate2();
                    if (clave.IsReadOnly())
                        certificado.Import(File.ReadAllBytes(cert_path), clave, X509KeyStorageFlags.PersistKeySet);
                    else
                        certificado.Import(File.ReadAllBytes(cert_path));

                    byte[] msgBytes = Encoding.UTF8.GetBytes(XmlLoginTicketRequest.OuterXml);

                // Firmamos

                    //var contentInfo = new System.Security.Cryptography.Pkcs.ContentInfo(data);
                    ContentInfo infoContenido = new ContentInfo(msgBytes);
                    SignedCms cmsFirmado = new SignedCms(infoContenido);

                    CmsSigner cmsFirmante = new CmsSigner(certificado);
                    cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;

                    cmsFirmado.ComputeSignature(cmsFirmante);

                    cmsFirmadoBase64 = Convert.ToBase64String(cmsFirmado.Encode());

                // Hago el login
                // Dim servicio As New WSAA.LoginCMSService
                afip.wsaahomo.LoginCMSService servicio = new afip.wsaahomo.LoginCMSService();
                    //wsaahomo.LoginCMSService servicio = new wsaahomo.LoginCMSService();

                    servicio.Url = url;

                    loginTicketResponse = servicio.loginCms(cmsFirmadoBase64);

                    // Analizamos la respuesta
                    XmlLoginTicketResponse = new XmlDocument();
                    XmlLoginTicketResponse.LoadXml(loginTicketResponse);

              

                    Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText;
                    Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText;

                    var exStr = XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText;
                    var genStr = XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText;
                    ExpirationTime = DateTime.Parse(exStr);
                    GenerationTime = DateTime.Parse(genStr);

                    XDocRequest = XDocument.Parse(XmlLoginTicketRequest.OuterXml);
                    XDocResponse = XDocument.Parse(XmlLoginTicketResponse.OuterXml);

                   // Interaction.MsgBox("Exito");
                }
                catch (Exception ex)
                {
                   // Interaction.MsgBox(ex.Message);
                }
            }
        }

        public class XMLLoader
        {
        public static object Server { get; private set; }

        public static void load(XmlDocument doc, string file)
            {
            //string pathToFiles = ControllerContext.HttpContext.Server.MapPath("~/" + file + ".xml");
            var ruta = HttpContext.Current.Server.MapPath("~/Templates/LoginTemplate.xml");
            doc.Load(ruta);
            }
            public static void loadTemplate(XmlDocument doc, string file)
            {
                load(doc, "");
            }
        }



    
}