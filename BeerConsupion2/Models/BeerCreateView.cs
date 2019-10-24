using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerConsupion2.Models
{
    public class BeerCreateView
    {
        public List<BeerType> BeerTypes { get; set; }
        public string TypeId { get; set; }
        public string Title { get; set; }
        public bool NonAlcohol { get; set; }
        public decimal Volume { get; set; }
        public BeerCreateView(List<BeerType> bts)
        {
            BeerTypes = bts;
        }
    }
}