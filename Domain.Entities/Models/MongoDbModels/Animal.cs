using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.MongoDbModels
{
    public class Animal
    {
        public string Id { get; set; }
        public string AnimalName { get; set; }
        public string ImageSource { get; set; }
        public string Order {  get; set; }

        // One animal has a list of questions
        public List<Question> Questions { get; set; } = new List<Question>(); 
    }
}
