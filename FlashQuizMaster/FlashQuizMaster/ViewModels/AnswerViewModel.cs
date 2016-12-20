using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizMaster.ViewModels
{
    public class AnswerViewModel:BindableBase
    {
        private DbRepository Repository = new DbRepository();
        private string _answerText;
        private int _answerQuestionId;
        private Question question;

        public string AnswerText {
            set
            {
                _answerText = value;
            }
            get
            {
                return _answerText;
            }
        }
        public int AnswerQuestionId
        {
            set
            {
                _answerQuestionId = value;
            }
            get
            {
                return _answerQuestionId;
            }
        }
    }
}
