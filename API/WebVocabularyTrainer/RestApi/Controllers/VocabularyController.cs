using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestApi.Context;
using RestApi.Models;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VocabularyController : Controller
    {
        private readonly VocabularyContext _context;

        public VocabularyController(VocabularyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Sentence> Get()
        {
            return _context.Sentences
                .ToArray();
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var output = _context.Sentences
                .SingleOrDefault(item => item.ID == id);

            if (output is null)
            {
                return NotFound();
            }
            return Ok(output);
        }

        //https://stackoverflow.com/questions/14407458/webapi-multiple-put-post-parameters
        [HttpPost]
        public IActionResult Add([FromBody]JObject data)
        {
            var newEntry = data.ToObject<Sentence>();
            var existingEntry = _context.Sentences
                .SingleOrDefault(item => item.Foreign.Equals(newEntry.Foreign) && item.Primary.Equals(newEntry.Primary));
            if (existingEntry != null)
            {
                return StatusCode(409); //Conflict
            }

            _context.Sentences.Add(newEntry);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]JObject data)
        {
            if (data is null)
            {
                return StatusCode(204); //No content
            }

            Sentence updatedEntry = null;
            Sentence existingEntry = null;
            try
            {
                updatedEntry = data.ToObject<Sentence>();
                existingEntry = _context.Sentences
                .SingleOrDefault(item => item.ID == updatedEntry.ID);
            }
            catch (Exception e)
            {
                return StatusCode(422, e.Message); //Unprocessable entry
            }

            if (existingEntry == null)
            {
                return NotFound();
            }

            _context.Sentences.Update(updatedEntry);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingEntry = _context.Sentences
                .SingleOrDefault(item => item.ID == id);
            if (existingEntry is null)
            {
                return NotFound();
            }

            _context.Sentences.Remove(existingEntry);
            _context.SaveChanges();
            return Ok();
        }
    }
}