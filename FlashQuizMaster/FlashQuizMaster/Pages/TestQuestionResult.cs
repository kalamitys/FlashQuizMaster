using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using FlashQuizMaster.ViewModels;
using FlashQuizMaster.BusinessLayer;

namespace FlashQuizMaster.Pages
{
    public class TestQuestionResult : ContentPage
    {
        
        public TestQuestionResult(TestQuestionManager tqMgr, QuestionAnswerViewModel qaVM)
        {
            this.BindingContext = tqMgr;
           
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Votre resultat au test de " + qaVM.CurrentChapter.Name +" est de:" + tqMgr.TotalCorrectAnswer.ToString() +" / " + tqMgr.TotalTestQuestion.ToString() }
                }
            };
        }
    }
}
