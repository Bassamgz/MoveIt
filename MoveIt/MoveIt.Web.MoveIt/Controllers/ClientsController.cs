using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using MoveIt.Core.Data.Model.DTO;

namespace MoveIt.Web.MoveIt.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private static HttpClient _httpClient;
        private string _apiPath = WebConfigurationManager.AppSettings["APIURL"];
        private string _clientAPI = WebConfigurationManager.AppSettings["ClientsAPI"];

        // GET: ClientCar
        public async Task<ActionResult> Index()
        {
            IEnumerable<ClientDTO> clientCars = new List<ClientDTO>();
            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiPath}{_clientAPI}/");
                if (response.IsSuccessStatusCode)
                {
                    clientCars = await response.Content.ReadAsAsync<IEnumerable<ClientDTO>>();
                }
                return View(clientCars);
            }
        }

        // GET: ClientCar/ClientID
        public async Task<ActionResult> Specific(long id)
        {
            IEnumerable<ClientDTO> clientCars = new List<ClientDTO>();
            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiPath}{_clientAPI}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    clientCars = await response.Content.ReadAsAsync<IEnumerable<ClientDTO>>();
                }
                return View(clientCars);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClientDTO newClient)
        {

            if (ModelState.IsValid)
            {

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("ID", newClient.ID.ToString()),
                        new KeyValuePair<string, string>("Name", newClient.Name),
                        new KeyValuePair<string, string>("City", newClient.City),
                        new KeyValuePair<string, string>("Email", newClient.Email),
                        new KeyValuePair<string, string>("PostalCode", newClient.PostalCode),
                        new KeyValuePair<string, string>("Street", newClient.Street),
                        new KeyValuePair<string, string>("BuildingNumber", newClient.BuildingNumber.ToString()),
                        new KeyValuePair<string, string>("Password", newClient.Password)
                });

                using (_httpClient = new HttpClient())
                {
                    HttpResponseMessage response =
                        await
                            _httpClient.PostAsync(new Uri($"{_apiPath}{_clientAPI}"), content);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(newClient);
            }
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDTO client = new ClientDTO();
            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiPath}{_clientAPI}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    client = await response.Content.ReadAsAsync<ClientDTO>();
                }
            }
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDTO client = new ClientDTO();
            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiPath}{_clientAPI}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    client = await response.Content.ReadAsAsync<ClientDTO>();
                }
            }
            if (TryUpdateModel(client, "",
                new string[] { "Title", "Credits", "DepartmentID" }))
            {
                try
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("ID", client.ID.ToString()),
                        new KeyValuePair<string, string>("Name", client.Name),
                        new KeyValuePair<string, string>("City", client.City),
                        new KeyValuePair<string, string>("Email", client.Email),
                        new KeyValuePair<string, string>("PostalCode", client.PostalCode),
                        new KeyValuePair<string, string>("Street", client.Street),
                        new KeyValuePair<string, string>("BuildingNumber", client.BuildingNumber.ToString()),
                        new KeyValuePair<string, string>("Password", client.Password)
                    });

                    using (_httpClient = new HttpClient())
                    {
                        HttpResponseMessage response =
                            await
                                _httpClient.PostAsync(new Uri($"{_apiPath}{_clientAPI}"), content);
                    }

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("",
                        "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(client);
        }
    }
}
