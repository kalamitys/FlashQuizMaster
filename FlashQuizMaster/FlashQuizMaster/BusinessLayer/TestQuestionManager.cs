using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashQuizMaster.Models;
using FlashQuizMaster.ViewModels;

namespace FlashQuizMaster.BusinessLayer
{
    public class TestQuestionManager
    {
        private DbRepository Repository = new DbRepository();
        private int _totalCorrectAnswer ;
        private int _totalTestQuestion = 0;

        public TestQuestionManager()
        {
        }

        public int TotalCorrectAnswer
        {
            get
            {
                return _totalCorrectAnswer;
            }

            set
            {
                _totalCorrectAnswer = value;
            }
        }

        public int TotalTestQuestion
        {
            get
            {
                return _totalTestQuestion;
            }

            set
            {
                _totalTestQuestion = value;
            }
        }

        public List<QuestionAnswerViewModel> GetRandomQuestions (int _questNbr, ChapterViewModel _chapVM)
        {
            List<QuestionAnswerViewModel> TestQuestionVMList = new List<QuestionAnswerViewModel>();

            List<Question> AllQuestions = Repository.GetQuestionsByChapterId(_chapVM.ChapterId).ToList();
            Chapter selChapter = Repository.GetChapterByChapterId(_chapVM.ChapterId);
            if (AllQuestions != null && (AllQuestions.Count() >= _questNbr))
            {
                //Generate a random list of questions
                List<Question> RandomQuestionList = Tools.PickRandom(AllQuestions.ToArray(), _questNbr);
                foreach (Question question in RandomQuestionList)
                {
                    QuestionAnswerViewModel qvm = new QuestionAnswerViewModel(_chapVM);
                    qvm.QuestionId = question.ID;
                    qvm.QuestionChaperId = question.ChaperId;
                    qvm.QuestionChapterName = selChapter.Name;
                    qvm.QuestionText = question.Text;
                    qvm.CorrectAnswerText = question.CorrectAnswer;
                    qvm.FakeAnswerText1 = question.FakeAnswer1;
                    qvm.FakeAnswerText2 = question.FakeAnswer2;
                    qvm.FakeAnswerText3 = question.FakeAnswer3;                    
                    qvm.CurrentChapter = selChapter;
                    
                                        
                    TestQuestionVMList.Add(qvm);
                }
            }
            else if (AllQuestions != null && (AllQuestions.Count() > 0))
            {
                //Generate a random list of questions
                List<Question> RandomQuestionList = Tools.PickRandom(AllQuestions.ToArray(), AllQuestions.Count());
                foreach (Question question in RandomQuestionList)
                {
                    QuestionAnswerViewModel qvm = new QuestionAnswerViewModel(_chapVM);
                    qvm.QuestionId = question.ID;
                    qvm.QuestionChaperId = question.ChaperId;
                    qvm.QuestionChapterName = selChapter.Name;
                    qvm.QuestionText = question.Text;
                    qvm.CorrectAnswerText = question.CorrectAnswer;
                    qvm.FakeAnswerText1 = question.FakeAnswer1;
                    qvm.FakeAnswerText2 = question.FakeAnswer2;
                    qvm.FakeAnswerText3 = question.FakeAnswer3;
                    qvm.CurrentChapter = selChapter;

                    TestQuestionVMList.Add(qvm);
                }
            }

            return TestQuestionVMList;
        }

        /// <summary>
        /// Count the total number of correct answers
        /// </summary>
        /// <param name="questIsCorrect"></param>
        public void GetTotalCorrectAnswer(bool questIsCorrect)
        {            
            if(questIsCorrect)
            {
                TotalCorrectAnswer = TotalCorrectAnswer + 1;
            }            
        }

        
    }
}
