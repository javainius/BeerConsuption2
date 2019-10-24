using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BeerConsupion2.client;
using BeerConsupion2.Models;

namespace BeerConsupion2.Controllers
{
    public class BeerController : Controller
    {

        // GET: Beer
        public async Task<ActionResult> Index()
        {
            List<Beer> beers = await ApiClient.GetAllAsync<Beer>("Beers/");
            List<BeerType> beerTypes = await ApiClient.GetAllAsync<BeerType>("BeerTypes/");

            for (int i = 0; i < beers.Count; i++)
            {
                for (int j = 0; j < beerTypes.Count; j++)
                {
                    if (beers[i].TypeId == beerTypes[j].Id)
                    {
                        beers[i].TypeId = beerTypes[j].Name;
                    }
                }
            }
            var sortedlist = beers.OrderBy(x => x.Title);
            return View(sortedlist);
        }

        // GET: Beer/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }

        // GET: Beer/Create
        public async Task<ActionResult> Create()
        {
            var beerTypes = await ApiClient.GetAllAsync<BeerType>("BeerTypes/");
            BeerCreateView newBeer = new BeerCreateView(beerTypes);
            return View(newBeer);
        }

        // POST: Beer/Create
        [HttpPost]
        public async Task<ActionResult> Create(BeerCreate newBeer)
        {
            try
            {
                await ApiClient.PostObjectAsync(newBeer, "Beers/");
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }

        // GET: Beer/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var beerTypes = await ApiClient.GetAllAsync<BeerType>("BeerTypes/");
            var beer = await ApiClient.GetObjectAsync<Beer>(id,"Beers/");
            BeerCreateView newBeer = new BeerCreateView(beerTypes);
            newBeer.TypeId = beer.TypeId;
            newBeer.Title = beer.Title;
            newBeer.Volume = beer.Volume;
            newBeer.NonAlcohol = beer.NonAlcohol;
            return View(newBeer);
        }

        // POST: Beer/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, BeerCreate editBeer)
        {
            try
            {
                // TODO: Add update logic here
                await ApiClient.PutObjectAsync<BeerCreate>(editBeer, "Beers/" + id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Beer/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            await ApiClient.DeleteByIdAsync(id, "Beers/");

            return RedirectToAction("Index");
        }

        // POST: Beer/Delete/5
        
    }
}

