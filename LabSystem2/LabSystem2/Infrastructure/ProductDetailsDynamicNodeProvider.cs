using MvcSiteMapProvider;
using LabSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabSystem2.Infrastructure
{
    public class ProductDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            // Build value 
            var returnValue = new List<DynamicNode>();

            foreach (Product a in _db.Productus)
            {
                DynamicNode n = new DynamicNode();
                n.Title = a.ProductTitle;
                n.Key = "Product_" + a.ProductId;
                n.ParentKey = "Genre_" + a.GenreId;
                n.RouteValues.Add("id", a.ProductId);                
                returnValue.Add(n);
            }

            // Return 
            return returnValue;
        }
    }
}