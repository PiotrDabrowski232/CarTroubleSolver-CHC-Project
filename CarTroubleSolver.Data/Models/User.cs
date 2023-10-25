using CarTroubleSolver.Data.Models;

namespace TheCarMarket.Data.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Accident> Accident { get; set; }
    }
}