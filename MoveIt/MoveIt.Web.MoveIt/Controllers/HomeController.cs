using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using MoveIt.Core.Data.Model.DTO;

namespace MoveIt.Web.MoveIt.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private static HttpClient httpClient;
        private string apiPath = WebConfigurationManager.AppSettings["APIURL"];
        private string priceLogicAPI = WebConfigurationManager.AppSettings["PriceLogicAPI"];
        private string _proposalApi = WebConfigurationManager.AppSettings["ProposalsAPI"];


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About MoveIt";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

        public async Task<ActionResult> PriceLogic(decimal distance, decimal volume, bool hasPiano)
        {
            using (httpClient = new HttpClient())
            {
                if (Request.IsAjaxRequest())
                {
                    PriceLogicDTO priceLogic = new PriceLogicDTO();
                    HttpResponseMessage response =
                        await
                            httpClient.GetAsync(
                                $"{apiPath}{priceLogicAPI}?distance={distance}&volume={volume}&hasPiano={hasPiano}");
                    if (response.IsSuccessStatusCode)
                    {
                        priceLogic = await response.Content.ReadAsAsync<PriceLogicDTO>();
                    }

                    //Add new proposal
                    await AddNewProposal(distance, volume, hasPiano, priceLogic);

                    return PartialView("CostView", priceLogic);
                }
                return View();
            }
        }

        private async Task AddNewProposal(decimal distance, decimal volume, bool hasPiano, PriceLogicDTO priceLogic)
        {
            HttpResponseMessage response;
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("ID", Guid.NewGuid().ToString()),
                new KeyValuePair<string, string>("Email", User.Identity.Name),
                new KeyValuePair<string, string>("Distance", distance.ToString()),
                new KeyValuePair<string, string>("HasPiano", hasPiano.ToString()),
                new KeyValuePair<string, string>("IsAccepted", bool.FalseString),
                new KeyValuePair<string, string>("Volume", volume.ToString()),
                new KeyValuePair<string, string>("ProposalDate", DateTime.Now.ToString()),
                new KeyValuePair<string, string>("Price", priceLogic.TotalCost.ToString())
            });

            using (httpClient = new HttpClient())
            {
                response =
                    await
                        httpClient.PostAsync(new Uri($"{apiPath}{_proposalApi}"), content);
            }
        }
    }
}