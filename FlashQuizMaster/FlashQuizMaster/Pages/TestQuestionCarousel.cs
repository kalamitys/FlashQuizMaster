using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using FlashQuizMaster.BusinessLayer;

namespace FlashQuizMaster.Pages
{
    public class TestQuestionCarousel : CarouselPage
    {
        private int _totalCorrectAnswer;
        public TestQuestionCarousel()
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
    }
}
