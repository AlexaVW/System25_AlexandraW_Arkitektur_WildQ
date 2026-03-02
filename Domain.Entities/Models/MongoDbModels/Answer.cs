using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.MongoDbModels
{
    public class Answer
    {
        public string Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsTrue { get; set; }
        // public string QuestionId { get; set; } //Don't need this because question is already inside animal

        // One question has 3 answers. 3 false and 1 true

        //One answer belongs to one question
    }
}
