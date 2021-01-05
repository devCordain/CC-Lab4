using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizService.Models;

namespace QuizService.Data
{
    public class QuizContext : DbContext
    {
        public QuizContext (DbContextOptions<QuizContext> options)
            : base(options)
        {
        }

        public DbSet<Quiz> Quiz { get; set; }
    }
}
