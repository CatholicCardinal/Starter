using System.Collections.Generic;

namespace Starter.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Person> People { get; set; }
    }
}
