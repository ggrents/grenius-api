﻿namespace grenius_api.Application.Models.Requests
{
    public class ArtistRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
    }
}
