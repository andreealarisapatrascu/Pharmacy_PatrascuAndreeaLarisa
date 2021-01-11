using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyModel.Data;
using PharmacyModel.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Pharmacy_PatrascuAndreeaLarisa.Controllers
{
    public class FurnizoriController : Controller
    {
        private readonly PharmacyContext _context;
        private string _baseUrl = "http://localhost:49650/api/Furnizori";

        public FurnizoriController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Furnizori
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var furnizori = JsonConvert.DeserializeObject<List<Furnizor>>(await response.Content.ReadAsStringAsync());
                return View(furnizori);
            }
            return NotFound();

        }



        // GET: Furnizori/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var furnizor = await _context.Furnizori
        //        .FirstOrDefaultAsync(m => m.FurnizorID == id);
        //    if (furnizor == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(furnizor);
        //}
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var furnizor = JsonConvert.DeserializeObject<Furnizor>(
                await response.Content.ReadAsStringAsync());
                return View(furnizor);
            }
            return NotFound();
        }

        // GET: Furnizori/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Furnizori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("FurnizorID,NumeFurnizor,Adresa,Telefon,Email")]Furnizor furnizor)
        {
            if (!ModelState.IsValid) return View(furnizor);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(furnizor);
                var response = await client.PostAsync(_baseUrl,new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record:{ ex.Message}");
            }
            return View(furnizor);
        }

        // GET: Furnizori/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var furnizor = JsonConvert.DeserializeObject<Furnizor>(
                await response.Content.ReadAsStringAsync());
                return View(furnizor);
            }
            return new NotFoundResult();
        }

        // POST: Furnizori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("FurnizorID,NumeFurnizor,Adresa,Telefon,Email")]Furnizor furnizor)
        {
            if (!ModelState.IsValid) return View(furnizor);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(furnizor);
            var response = await client.PutAsync($"{_baseUrl}/{furnizor.FurnizorID}",
            new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(furnizor);
        }

        // GET: Furnizori/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var furnizor = JsonConvert.DeserializeObject<Furnizor>(await response.Content.ReadAsStringAsync());
                return View(furnizor);
            }
            return new NotFoundResult();
        }

        // POST: Furnizori/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("FurnizorID")] Furnizor furnizor)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Delete,$"{_baseUrl}/{furnizor.FurnizorID}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(furnizor),Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record:{ ex.Message}");
            }
            return View(furnizor);
        }
    }
}

