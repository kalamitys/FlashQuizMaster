using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizMaster.ViewModels
{
    public class QuestionAnswerViewModel:BindableBase
    {
        private DbRepository Repository = new DbRepository();
        private int _questionId;
        private string _questionText;
        private int _questionChapterId;
        private string _questionChapterName;
        private string _correctAnswerText;
        private string _fakeAnswerText1;
        private string _fakeAnswerText2;
        private string _fakeAnswerText3;
        private Chapter _currentChapter;
        private ChapterViewModel currentChapterVM;
        private Question _currentQuestion;
        private List<String> _currentAnswers = new List<String>(); //SDI: This is just to be able to randomly affect each answer for the test screen
        private List<Answer> _questAnswerList = new List<Answer>(); //SDI: To check if I can save the answers
        private string _correctAnswerTextRandomDisplay;
        private string _fakeAnswerText1RandomDisplay;
        private string _fakeAnswerText2RandomDisplay;
        private string _fakeAnswerText3RandomDisplay;
        private int _testScore = 0;

        #region  Properties
        public int QuestionId {
            set
            {
                _questionId = value;
            }
            get
            {
                return _questionId;
            }
        }        
        public string QuestionText
        {
            set
            {
                _questionText = value;
            }
            get
            {
                return _questionText;
            }
        }
        public int QuestionChaperId
        {
            set
            {
                _questionChapterId = value;
            }
            get
            {
                return _questionChapterId;
            }
        }
        public string QuestionChapterName
        {
            set
            {
                _questionChapterName = value;
            }
            get
            {
                return _questionChapterName;
            }
        }

        public string CorrectAnswerText
        {
            set
            {
                _correctAnswerText = value;
            }
            get
            {
                return _correctAnswerText;
            }
        }

        public string FakeAnswerText1
        {
            set
            {
                _fakeAnswerText1 = value;
            }
            get
            {
                return _fakeAnswerText1;
            }
        }
        public string FakeAnswerText2
        {
            set
            {
                _fakeAnswerText2 = value;
            }
            get
            {
                return _fakeAnswerText2;
            }
        }

        public string FakeAnswerText3
        {
            set
            {
                _fakeAnswerText3 = value;
            }
            get
            {
                return _fakeAnswerText3;
            }
        }

        public Chapter CurrentChapter
        {
            get
            {
                return _currentChapter;
            }

            set
            {
                _currentChapter = value;
            }
        }

        public Question CurrentQuestion
        {
            get
            {
                return _currentQuestion;
            }

            set
            {
                _currentQuestion = value;
            }
        }

        public List<string> CurrentAnswers
        {
            get
            {
                return _currentAnswers;
            }

            set
            {
                _currentAnswers = value;
            }
        }

        public string CorrectAnswerTextRandomDisplay
        {
            get
            {
                return _correctAnswerTextRandomDisplay;
            }

            set
            {
                _correctAnswerTextRandomDisplay = value;
            }
        }

        public string FakeAnswerText1RandomDisplay
        {
            get
            {
                return _fakeAnswerText1RandomDisplay;
            }

            set
            {
                _fakeAnswerText1RandomDisplay = value;
            }
        }

        public string FakeAnswerText2RandomDisplay
        {
            get
            {
                return _fakeAnswerText2RandomDisplay;
            }

            set
            {
                _fakeAnswerText2RandomDisplay = value;
            }
        }

        public string FakeAnswerText3RandomDisplay
        {
            get
            {
                return _fakeAnswerText3RandomDisplay;
            }

            set
            {
                _fakeAnswerText3RandomDisplay = value;
            }
        }

        public int TestScore
        {
            get
            {
                return _testScore;
            }

            set
            {
                _testScore = value;
            }
        }

        public List<Answer> QuestAnswerList
        {
            get
            {
                return _questAnswerList;
            }

            set
            {
                _questAnswerList = value;
            }
        }
        #endregion

        public QuestionAnswerViewModel(ChapterViewModel _curChapter)
        {
            this.currentChapterVM = _curChapter;
            this.CurrentChapter = new Chapter();
            this.CurrentQuestion = new Question();
        }

        public void FillAnswerListFromViewModel(QuestionAnswerViewModel qvm)
        {
            if (!string.IsNullOrEmpty(qvm.CorrectAnswerText))
            {
                qvm.CurrentAnswers.Add(qvm.CorrectAnswerText);
            }
            if (!string.IsNullOrEmpty(qvm.FakeAnswerText1))
            {
                qvm.CurrentAnswers.Add(qvm.FakeAnswerText1);
            }
            if (!string.IsNullOrEmpty(qvm.FakeAnswerText2))
            {
                qvm.CurrentAnswers.Add(qvm.FakeAnswerText2);
            }
            if (!string.IsNullOrEmpty(qvm.FakeAnswerText3))
            {
                qvm.CurrentAnswers.Add(qvm.FakeAnswerText3);
            }
            
        }

        public List<QuestionAnswerViewModel> GetQuestionsForChapterVM(ChapterViewModel selChapterVM)
        {
            List<Question> AllQuestions = Repository.GetQuestionsByChapterId(selChapterVM.ChapterId).ToList();
            Chapter selChapter = Repository.GetChapterByChapterId(selChapterVM.ChapterId);
            List<QuestionAnswerViewModel> AllQuestionsVM = new List<QuestionAnswerViewModel>();
            if (AllQuestions != null)
            {
                foreach (Question question in AllQuestions)
                {
                    QuestionAnswerViewModel qvm = new QuestionAnswerViewModel(selChapterVM);
                    qvm.QuestionId = question.ID;
                    qvm.QuestionChaperId = question.ChaperId;
                    qvm.QuestionChapterName = selChapter.Name;
                    qvm.QuestionText = question.Text;
                    qvm.CorrectAnswerText = question.CorrectAnswer;
                    qvm.FakeAnswerText1 = question.FakeAnswer1;
                    qvm.FakeAnswerText2 = question.FakeAnswer2;
                    qvm.FakeAnswerText3 = question.FakeAnswer3;
                    qvm.currentChapterVM = selChapterVM;
                    qvm.CurrentChapter = selChapter;                    

                    AllQuestionsVM.Add(qvm);
                }
            }
            return AllQuestionsVM;
        }

        public void SaveQuestionVM()
        {
            CurrentQuestion.ChaperId = currentChapterVM.ChapterId;
            CurrentQuestion.Text = QuestionText;
            if (this.QuestionId > 0)
            {
                CurrentQuestion.ID = this.QuestionId;
            }
            CurrentQuestion.CorrectAnswer = CorrectAnswerText;
            CurrentQuestion.FakeAnswer1 = FakeAnswerText1;
            CurrentQuestion.FakeAnswer2 = FakeAnswerText2;
            CurrentQuestion.FakeAnswer3 = FakeAnswerText3;

            if (!string.IsNullOrEmpty(CorrectAnswerText.Trim()))
            {
                CurrentAnswers.Add(CorrectAnswerText);
                //Answer correctAnswer = new Answer();
                //correctAnswer.IsCorrect = true;
                //correctAnswer.QuestionId = CurrentQuestion.
            }
            if (!string.IsNullOrEmpty(FakeAnswerText1.Trim()))
            {
                CurrentAnswers.Add(FakeAnswerText1);
            }
            if (!string.IsNullOrEmpty(FakeAnswerText2.Trim()))
            {
                CurrentAnswers.Add(FakeAnswerText2);
            }
            if (!string.IsNullOrEmpty(FakeAnswerText3.Trim()))
            {
                CurrentAnswers.Add(FakeAnswerText3);
            }
            Repository.SaveQuestion(CurrentQuestion);
        }

        public void DeleteAllQuestions()
        {
            Repository.DeleteAllQuestions();
        }

        public int DeleteQuestionById(int _questionId)
        {
            return Repository.DeleteQuestionById(_questionId);
        }
    }
}
