using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using MoveIt.Core.Data.Model.DTO;

namespace MoveIt.Web.MoveIt.Controllers
{
    [Authorize]
    public class ProposalsController : Controller
    {
        private string _apiPath = WebConfigurationManager.AppSettings["APIURL"];
        private string _proposalApi = WebConfigurationManager.AppSettings["ProposalsAPI"];
        private static HttpClient _httpClient;

        // GET: Proposals for user
        public async Task<ActionResult> Index()
        {
            IEnumerable<ProposalDTO> proposals = new List<ProposalDTO>();
            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage response =
                    await _httpClient.GetAsync($"{_apiPath}{_proposalApi}?email={User.Identity.Name}");
                if (response.IsSuccessStatusCode)
                {
                    proposals = await response.Content.ReadAsAsync<IEnumerable<ProposalDTO>>();
                }
                return View(proposals);
            }
        }


        public async Task<ActionResult> Specific(Guid id)
        {
            ProposalDTO carStatus = new ProposalDTO();
            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiPath}{_proposalApi}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    carStatus = await response.Content.ReadAsAsync<ProposalDTO>();
                }
                return View(carStatus);
            }
        }

        // GET: Proposal
        public async Task<ActionResult> Offers()
        {
            IEnumerable<ProposalDTO> proposals = new List<ProposalDTO>();
            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage response =
                    await _httpClient.GetAsync($"{_apiPath}{_proposalApi}?id={User.Identity.Name}&isAccepted=1");
                if (response.IsSuccessStatusCode)
                {
                    proposals = await response.Content.ReadAsAsync<IEnumerable<ProposalDTO>>();
                }
                return View(proposals);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProposalDTO newProposal)
        {

            if (ModelState.IsValid)
            {

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("ID", Guid.NewGuid().ToString()),
                    new KeyValuePair<string, string>("ClientID", User.Identity.Name),
                    new KeyValuePair<string, string>("Distance", newProposal.Distance.ToString()),
                    new KeyValuePair<string, string>("HasPiano", newProposal.HasPiano.ToString()),
                    new KeyValuePair<string, string>("IsAccepted", bool.FalseString),
                    new KeyValuePair<string, string>("Volume", newProposal.Volume.ToString()),
                    new KeyValuePair<string, string>("ProposalDate", DateTime.Now.ToString()),
                    new KeyValuePair<string, string>("Price", newProposal.Price.ToString())
                });

                using (_httpClient = new HttpClient())
                {
                    HttpResponseMessage response =
                        await
                            _httpClient.PostAsync(new Uri($"{_apiPath}{_proposalApi}"), content);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(newProposal);
            }
        }


        [HttpPost]
        [ActionName("Create")]
        public async Task<ActionResult> Create(decimal distance, decimal volume, bool hasPiano)
        {

            if (ModelState.IsValid)
            {

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("ID", Guid.NewGuid().ToString()),
                    new KeyValuePair<string, string>("ClientID", User.Identity.Name),
                    new KeyValuePair<string, string>("Distance", distance.ToString()),
                    new KeyValuePair<string, string>("HasPiano", hasPiano.ToString()),
                    new KeyValuePair<string, string>("IsAccepted", bool.FalseString),
                    new KeyValuePair<string, string>("Volume", volume.ToString()),
                    new KeyValuePair<string, string>("ProposalDate", DateTime.Now.ToString())
                });

                using (_httpClient = new HttpClient())
                {
                    HttpResponseMessage response =
                        await
                            _httpClient.PostAsync(new Uri($"{_apiPath}{_proposalApi}"), content);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Accept(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProposalDTO proposal = new ProposalDTO();
            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiPath}{_proposalApi}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    proposal = await response.Content.ReadAsAsync<ProposalDTO>();
                }
            }
            if (proposal == null)
            {
                return HttpNotFound();
            }
            return View(proposal);
        }

        [HttpPut]
        [ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AcceptConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProposalDTO proposal = new ProposalDTO();
            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiPath}{_proposalApi}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    proposal = await response.Content.ReadAsAsync<ProposalDTO>();
                }
            }
            proposal.IsAccepted = true;
            proposal.AcceptanceDate = DateTime.Now;
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("ID", proposal.ID.ToString()),
                    new KeyValuePair<string, string>("ClientID", proposal.ClientID.ToString()),
                    new KeyValuePair<string, string>("Distance", proposal.Distance.ToString()),
                    new KeyValuePair<string, string>("HasPiano", proposal.HasPiano.ToString()),
                    new KeyValuePair<string, string>("IsAccepted", proposal.IsAccepted.ToString()),
                    new KeyValuePair<string, string>("Volume", proposal.Volume.ToString()),
                    new KeyValuePair<string, string>("ProposalDate", proposal.ProposalDate.ToString()),
                    new KeyValuePair<string, string>("Price", proposal.Price.ToString()),
                    new KeyValuePair<string, string>("AcceptanceDate", proposal.AcceptanceDate.ToString())
                });

                using (_httpClient = new HttpClient())
                {
                    HttpResponseMessage response =
                        await
                            _httpClient.PutAsync(new Uri($"{_apiPath}{_proposalApi}"), content);
                }

                return RedirectToAction("Index");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("",
                    "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(proposal);
        }

        // GET: Proposal/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProposalDTO proposal = new ProposalDTO();
            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiPath}{_proposalApi}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    proposal = await response.Content.ReadAsAsync<ProposalDTO>();
                }
            }
            if (proposal == null)
            {
                return HttpNotFound();
            }
            return View(proposal);
        }

        // Delete: Proposal/Delete/5
        [HttpDelete]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (_httpClient = new HttpClient())
            {
                await _httpClient.DeleteAsync($"{_apiPath}{_proposalApi}/{id}");
            }

            return RedirectToAction("Index");
        }
    }
}
