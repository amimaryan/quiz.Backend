using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quiz_backend.Models;

namespace quiz_backend.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Questions")]
    public class QuestionsController : ControllerBase
    {
        readonly QuizContext context;
        public QuestionsController(QuizContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Question> Get()
        {

            return new Question[] {
                new Question() { Text = "Hello" },
                new Question() { Text = "Hi" }
            };

        }

        [HttpPost]
        public void Post([FromBody]Question question)
        {
            context.Questions.Add(new Question() { Text = "test" });
            context.SaveChanges();
        }
    }
}