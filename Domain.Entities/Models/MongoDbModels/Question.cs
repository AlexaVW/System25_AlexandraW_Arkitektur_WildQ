using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.MongoDbModels
{
    public class Question
    {
        public string Id { get; set; }
        public string QuestionText { get; set; }
        //public string AnimalId { get; set; }

        // One question belongs to one animal

        // One question has many answers
        public List<Answer> Answers { get; set; } = new List<Answer>(); //Observable collection?
    }
}
