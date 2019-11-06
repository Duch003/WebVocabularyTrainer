using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public List<Sentence> Get()
        {
            return _context.Sentences
                .ToList();
        }
        [HttpGet("{id}")]
        public Sentence Get(int id)
        {
            return _context.Sentences
                .Where(item => item.ID == id)
                .FirstOrDefault();
        }
    }
}