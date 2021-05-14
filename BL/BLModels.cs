using System;
using System.Collections.Generic;

namespace BL
{
    public class Puzzle
    {
        public Guid Id { get; set; } // Puzzle Id
        public Guid UId { get; set; } // User Id
        public Guid Seed { get; set; } // Seed Id in case of puzzle sharing implementation.
        public Guid SolvedBy { get; set; } // Who solved this puzzle?
        public string Dividend { get; set; } // Right part.
        public string Divisor { get; set; } // Left part.
        public string Quotient { get; set; } // Top part.
        public string Letters { get; set; } // Letters in order of 0-9
        public DateTime Created { get; set; } // When it was created.
        public DateTime Solved { get; set; } // When it was solved.

        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public Guid CommentId { get; set; } // Comment Id
        public Guid UIdC { get; set; } // User Id of the commenter. 
        public string Remark { get; set; } // The comment
        public DateTime Created { get; set; } // Date the comment was created

        public Guid Id { get; set; } // Puzzle Id
        public Puzzle Puzzle { get; set; }
    }
    public class Answer
    {
        public Guid id { get; set; }
        public string one { get; set; }
        public string two { get; set; }
        public string three { get; set; }
        public string four { get; set; }
        public string five { get; set; }
        public string six { get; set; }
        public string seven { get; set; }
        public string eight { get; set; }
        public string nine { get; set; }
        public string ten { get; set; }
    }
}
