﻿namespace ProiectMDS.Models.DTOs
{
    public class ReviewDTO
    {
        public int PostareId { get; set; }

        public int UserId { get; set; }

        public string titlu { get; set; }

        public string comentariu { get; set; }

        public int rating { get; set; }

        public DateTime dataReview { get; set; }
    }
}
