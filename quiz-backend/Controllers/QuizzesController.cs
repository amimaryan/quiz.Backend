﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_backend;
using quiz_backend.Models;

namespace quiz_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        readonly QuizContext _context;

        public QuizzesController(QuizContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<Quiz> Get()
        {
            var userId = HttpContext.User.Claims.First().Value;
            return _context.Quiz.Where(q => q.OwnerId == userId);
        }

        // GET: api/Quizzes
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Quiz>>> GetQuiz()
        //{
        //    return await _context.Quiz.ToListAsync();
        //}

        // GET: api/Quizzes/5
        [Authorize]
        [HttpGet("{id}")]
        public IEnumerable<Quiz> GetQuiz(int id)
        {
            //    var quiz = await _context.Quiz.FindAsync(id);

            //    if (quiz == null)
            //    {
            //        return NotFound();
            //    }

            //    return quiz;
            var userId = HttpContext.User.Claims.First().Value;
            return _context.Quiz.Where(q => q.OwnerId == userId);
        }

        [HttpGet("all")]
        public IEnumerable<Quiz> GetAllQuizzes()
        {
            return _context.Quiz;
        }

        // PUT: api/Quizzes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (id != quiz.ID)
                return BadRequest();

            _context.Entry(quiz).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(quiz);
        }

        // POST: api/Quizzes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {

            if (quiz == null)
            {
                return NotFound();
            }

            var userId = HttpContext.User.Claims.First().Value;

            quiz.OwnerId = userId;

            _context.Quiz.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.ID }, quiz);
        }

        // DELETE: api/Quizzes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Quiz>> DeleteQuiz(int id)
        {
            var quiz = await _context.Quiz.FindAsync(id);

            _context.Quiz.Remove(quiz);
            await _context.SaveChangesAsync();

            return quiz;
        }

        private bool QuizExists(int id)
        {
            return _context.Quiz.Any(e => e.ID == id);
        }
    }
}
