using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using gesteco.api.Configuration;
using gesteco.api.OutputModels;
using gesteco.api.Services;
using gesteco.api.src.gesteco.WebApi.OutputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using SelectPdf;

namespace gesteco.api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase {


        [HttpPost ]
        public async Task<IActionResult> SendMail(EmailDTO emailDTO)
        {
            ServiceResponse<EmailDTO> Data = new ServiceResponse<EmailDTO>();
          
            try
            {
                #region ConvertToPdf
                HtmlToPdf converter = new HtmlToPdf();
                //create a new pdf document converting an url 
                PdfDocument doc = converter.ConvertHtmlString(GeneratePDF.GetHtmlString(emailDTO));
                //save pdf document 
                byte[] pdf = doc.Save();
                //close pdf document 
                doc.Close();
                #endregion

                #region Get AzureAd Config
                var scopeFactory = this.HttpContext.RequestServices.GetService<IServiceScopeFactory>();
                using (var scope = scopeFactory.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    var options = serviceProvider.GetRequiredService<AzureAdOptions>();
                    var optionsMail = serviceProvider.GetRequiredService<SendMailOptions>();

                    #endregion

                #region FileAttachment
                MessageAttachmentsCollectionPage attachments = new MessageAttachmentsCollectionPage();
                attachments.Add(new FileAttachment
                {
                    ODataType = "#microsoft.graph.fileAttachment",
                    ContentBytes = pdf,
                    ContentType = BodyType.Text.ToString(),
                    ContentId = "Piecejointe",
                    Name = optionsMail.FileName
                });
                #endregion

                #region Message
                var message = new Message
                {
                    Subject = optionsMail.Subject ,
                    Body = new ItemBody
                    {
                        ContentType = BodyType.Html,
                        Content = "<div>Bonjour  " + emailDTO.VisiteDTO.Client.Nom + "   " + emailDTO.VisiteDTO.Client.Prenom + "  </div> <br/> <br/><div>" + Environment.NewLine + optionsMail.Body + " </div>"
                    },
                    ToRecipients = new List<Recipient>()
                    {
                        new Recipient
                        {
                            EmailAddress = new EmailAddress
                            {
                                Address = emailDTO.VisiteDTO.Client.Courriel
                            }
                        }
                    }
                           ,
                    Attachments = attachments
                };
                #endregion

                #region confidentialClient
                IConfidentialClientApplication confidentialClient = ConfidentialClientApplicationBuilder
                        .Create(options.ClientId)
                        .WithClientSecret(options.ClientSecret)
                        .WithAuthority(new Uri($"https://login.microsoftonline.com/{options.TenantId}/v2.0"))
                        .Build();
                #endregion

                #region obtient un nouveau jeton
                // Récupère un jeton d'accès pour Microsoft Graph (obtient un nouveau jeton si nécessaire).
                var authResult = await confidentialClient
                                .AcquireTokenForClient(options.Scopes)
                                .ExecuteAsync().ConfigureAwait(false);
                // Construise le client Microsoft Graph. En tant que fournisseur d'authentification, définissez un asynchrone
                // qui utilise le client MSAL pour obtenir un jeton d'accès à l'application uniquement à Microsoft Graph,
                // et insère ce jeton d'accès dans l'en-tête Authorization de chaque demande d'API.
                var token = authResult.AccessToken;

                GraphServiceClient graphServiceClient = new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
                     {
                        //Ajoutez le jeton d'accès dans l'en-tête Authorization de la requête API.
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                     })
                 );
                #endregion

                #region SendMail
                await graphServiceClient.Me
                             .SendMail(message, false)
                             .Request()
                             .PostAsync();
                }
                #endregion
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }
            return Ok(Data);
        }

    }
}
