using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using keep.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace keep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> _products = new List<Product>(new[] {
            new Product() { Title = "First Post", PlainText = "blah bluh bleeeeeh", CheckList = new List<string>(new[] {"jdkf","ckdnc" }) , PinnedStatus = true, Label = "dog"},
            new Product() { Title = "Second Post", PlainText = "Something not new", CheckList = new List<string>(new[] {"rokfork","klfdk" }) , PinnedStatus = false, Label = "cat" },
            new Product() { Title = "Third Post", PlainText = "Something not new", CheckList = new List<string>(new[] {"rokfork","klfdk" }) , PinnedStatus = false, Label = "owl"}
        });

        [HttpGet]
        public List<Product> Get()
        {
            return _products; //Pretend to go to DB
        }

        [HttpGet("label/{label}")]  //capture route parameter
        public IActionResult GetByLabel(string label)
        {
            var product = _products.Where(p => p.Label == label);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("pin/{val}")]  //capture route parameter
        public IActionResult GetByPinnedStatus(bool val)
        {
            var product = _products.Where(p => p.PinnedStatus == val);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpPost]
        public IActionResult Post(
            [FromBody]
            Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _products.Add(product);
            //var routeValues = new { title = product.Title };
            //return CreatedAtAction(nameof(Get),
            //    routeValues, product);
            return Ok();
        }


        [HttpDelete("delete/{title}")]
        public IActionResult Delete(string title)
        {
            var product = _products.SingleOrDefault(p => p.Title == title);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _products.Remove(product);
            var routeValues = new { title = product.Title };
            return CreatedAtAction(nameof(Get),
                routeValues, product);
        }



        [HttpPut("put/{title}")]
        public IActionResult Put([FromRoute] string title, [FromBody] string value)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            _products.SingleOrDefault(pr => pr.Title == title).Title = value;
            //p["Title"] = value;
            //if (p == null)
            //{
            //    return NotFound();
            //}

            //p.Title = value;

            //p.SaveChanges();
            return Ok();

            




        }


    }
}