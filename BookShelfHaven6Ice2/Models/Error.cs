﻿namespace BookShelfHaven6Ice2.Models
{
    public class Error
    {
        public string? ErrorMessage { get; set; }

        public Error(string? errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
