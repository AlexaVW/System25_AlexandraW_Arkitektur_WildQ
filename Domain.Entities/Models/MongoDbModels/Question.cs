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
        
        // One question belongs to one animal

        // One question has a list of answers
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
