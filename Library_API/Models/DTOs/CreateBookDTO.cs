﻿namespace Library_API.Models.DTOs
{
    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvalible { get; set; }
        public string Description { get; set; }
    }
}
