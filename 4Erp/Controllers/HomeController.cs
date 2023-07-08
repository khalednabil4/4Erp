using _4Erp.Migrations;
using _4Erp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Buffers.Text;
using System.ComponentModel;
using System;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace _4Erp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly masterContext _context;
        public HomeController(ILogger<HomeController> logger, masterContext context)
        {
            _context = context;
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

  
            return View(_context.Cities.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Add()
            
        {
       
            return View();
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CitiesAndRegions cites)

        {

            string json = Request.Form["tableDataInput2"];
            List<Region> regions = System.Text.Json.JsonSerializer.Deserialize<List<Region>>(json);

  
            if (ModelState.IsValid)
            {
                cites.Region = regions;
                _context.Cities.Add(cites);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();

            }

        }
        public IActionResult update(int id)
        {

            var city = _context.Cities.Include(e=>e.Region).First(e=>e.CityId==id);


            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true // Optional: Format the JSON with indentation for better readability
            };

            ICollection<Region> regions = city.Region;
            string json = System.Text.Json.JsonSerializer.Serialize(regions, options);
            ViewData["data"] = json;
            Console.WriteLine(json);

            if (city == null)
            {
             
                return NotFound();
            }
            

         

            return View(city);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult update(CitiesAndRegions cites)
        {
            Console.WriteLine(cites.CityId);
            string json = Request.Form["tableDataInput"];
           
            var deserializedData = JsonConvert.DeserializeObject<List<Region>>(json);
            var ci = _context.Cities.Include(e=>e.Region).FirstOrDefault(r=>r.CityId==cites.CityId);

            ViewData["data"] = json;
            if (ModelState.IsValid)
            {
                ci.CityCode = cites.CityCode;
                ci.CityDescriptionAR = cites.CityDescriptionAR;
                ci.CityDescriptionEn = cites.CityDescriptionEn;
                ci.Status = cites.Status;
                ci.Note= cites.Note;

                foreach (var region in deserializedData)
                {


                    if (region.RegionId != 0)
                    {
                        var re = ci.Region.FirstOrDefault(e => e.RegionId == region.RegionId);

                        re.RegionCode = region.RegionCode;
                        re.RegionDescriptionAR = region.RegionDescriptionAR;
                        re.RegionDescriptionEn = region.RegionDescriptionEn;

                    }
                    else
                    {
                        ci.Region.Add(region);

                    }

                }
                var deletedRegions = ci.Region?.Where(r => !deserializedData.Any(rd => rd.RegionId == r.RegionId)).ToList();
                if (deletedRegions != null)
                {
                    foreach (var deletedRegion in deletedRegions)
                    {

                        ci.Region.Remove(deletedRegion);
                    }
                }

                _context.SaveChanges();
            }
            else
            {


            }
            return RedirectToAction(nameof(update));


        }
   
        public async Task<IActionResult> Delete(int id)
        {
            var cite = await _context.Cities.FindAsync(id);
            if (cite != null)
            {
                _context.Cities.Remove(cite);
                _context.SaveChanges();

            }
            else
            {

            }
        

            return RedirectToAction(nameof(Index));
        }
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}