using System;

namespace Backend.Models
{
    public class Player
    {
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string FullName { get; set; }
        public string Age { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
    }
}
