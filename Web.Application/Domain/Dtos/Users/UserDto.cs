﻿using Web.Application.Domain.Enums;

namespace Web.Application.Domain.Dtos.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public EProfile Profile { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
