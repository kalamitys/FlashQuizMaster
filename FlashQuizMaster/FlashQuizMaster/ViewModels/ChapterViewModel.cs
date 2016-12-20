using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizMaster.ViewModels
{
    public class ChapterViewModel:BindableBase
    {
        private DbRepository Repository = new DbRepository();        
        private TopicViewModel currentTopic;
        private Chapter currentChapter;
        private int _chapterId;
        private int _chapterTopicId;
        private double _chapterCompletionRate;
        private double _chapterSuccessRate;
        private double _chapterConfidenceRate;

        private string chapterImage = string.Empty; //SDI: the image is local to the viewmodel for simplification

        public ChapterViewModel(TopicViewModel _curTopic)
        {
            currentChapter = new Chapter();
            currentTopic = _curTopic;            
        }

        public ChapterViewModel(TopicViewModel _curTopic, Chapter _curChapter)
        {
            currentChapter = _curChapter;
            currentTopic = _curTopic;
        }

        #region Properties
        public int ChapterId
        {
            set
            {
                _chapterId = value;
            }
            get
            {
                return _chapterId;
            }
        }

        public string ChapterName
        {
            set
            {
                if (!value.Equals(currentChapter.Name, StringComparison.OrdinalIgnoreCase))
                {
                    currentChapter.Name = value;
                    OnPropertyChanged("Name");
                }
            }
            get
            {
                return currentChapter.Name;
            }
        }

        public string ChapterImage
        {
            set
            {
                this.chapterImage = value;
            }
            get
            {
                return this.chapterImage;
            }
        }

        public int ChapterTopicId
        {
            set
            {
                _chapterTopicId = value;
            }
            get
            {
                return _chapterTopicId;
            }
        }

        public double ChapterCompletionRate
        {
            set
            {
                _chapterCompletionRate = value;
            }
            get
            {
                return _chapterCompletionRate;
            }
        }

        public double ChapterSuccessRate
        {
            set
            {
                _chapterSuccessRate = value;
            }
            get
            {
                return _chapterSuccessRate;
            }
        }

        public double ChapterConfidenceRate
        {
            set
            {
                _chapterConfidenceRate = value;
            }
            get
            {
                return _chapterConfidenceRate;
            }
        }

        public TopicViewModel CurrentTopic
        {
            get
            {
                return currentTopic;
            }

            set
            {
                currentTopic = value;
            }
        }

        #endregion

        public void SaveChapterVM()
        {
            currentChapter.Name = this.ChapterName;
            currentChapter.TopicId = currentTopic.TopicId;
            Repository.SaveChapter(currentChapter);
        }

        public List<ChapterViewModel> GetChaptersForTopicVM(TopicViewModel selTopic)
        {            
            List<Chapter> AllChapters = Repository.GetChaptersByTopicId(selTopic.TopicId).ToList();
            List<ChapterViewModel> AllChaptersVM = new List<ChapterViewModel>();
            if (AllChaptersVM != null)
            {
                foreach (Chapter chapter in AllChapters)
                {
                    ChapterViewModel cvm = new ChapterViewModel(currentTopic);
                    cvm.ChapterId = chapter.ID;
                    cvm.ChapterName = chapter.Name;
                    cvm.ChapterImage = "ChapterImage.png"; //SDI:Same image for all Chapters
                    cvm.ChapterTopicId = chapter.TopicId;
                    //cvm.CurrentTopic = new TopicViewModel().ge
                    AllChaptersVM.Add(cvm);
                }
            }
            return AllChaptersVM;
        }
        

        public List<ChapterViewModel> GetChaptersVM()
        {
            List<Chapter> AllChapters = Repository.GetChapters().ToList();
            List<ChapterViewModel> AllChaptersVM = new List<ChapterViewModel>();
            if (AllChaptersVM != null)
            {
                foreach (Chapter chapter in AllChapters)
                {
                    ChapterViewModel tvm = new ChapterViewModel(currentTopic);
                    tvm.ChapterId = chapter.ID;
                    tvm.ChapterName = chapter.Name;
                    tvm.ChapterImage = "ChapterImage.png"; //SDI:Same image for all Chapters
                    AllChaptersVM.Add(tvm);
                }
            }
            return AllChaptersVM;
        }

        public void DeleteAllChapters()
        {
            Repository.DeleteAllChapters();
        }

        public int DeleteChapterById(int _chapterId)
        {
            return Repository.DeleteChapterById(_chapterId);
        }

    }
}
