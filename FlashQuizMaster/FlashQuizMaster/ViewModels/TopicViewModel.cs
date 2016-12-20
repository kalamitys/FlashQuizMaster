using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashQuizMaster;

namespace FlashQuizMaster.ViewModels
{
    public class TopicViewModel: BindableBase
    {
        private DbRepository Repository = new DbRepository();
        private ConnectCreateUserViewModel currentUserVM;
        private Topic topic;
        private int _topicId;
        private int _topicUserId;
        private double _topicCompletionRate;
        private double _topicSuccessRate;
        private double _topicConfidenceRate;

        private string topicImage = string.Empty; //SDI: the image is local to the viewmodel for simplification

        public TopicViewModel(ConnectCreateUserViewModel _curUserVM)
        {
            topic = new Topic();
            currentUserVM = _curUserVM;
        }

       

        public int TopicId
        {
            set
            {
                _topicId = value;
            }
            get
            {
                return _topicId;
            }
        }

        public string TopicName
        {
            set
            {
                if (!value.Equals(topic.Name, StringComparison.OrdinalIgnoreCase))
                {
                    topic.Name = value;
                    OnPropertyChanged("Name");
                }
            }
            get
            {
                return topic.Name;
            }
        }

        public string TopicImage
        {
            set
            {
                this.topicImage = value;
            }
            get
            {
                return this.topicImage;
            }
        }

        public int TopicUserId
        {
            set
            {
                _topicUserId = value;
            }
            get
            {
                return _topicUserId;
            }
        }

        public double TopicCompletionRate
        {
            set
            {
                _topicCompletionRate = value;
            }
            get
            {
                return _topicCompletionRate;
            }
        }

        public double TopicSuccessRate
        {
            set
            {
                _topicSuccessRate = value;
            }
            get
            {
                return _topicSuccessRate;
            }
        }

        public double TopicConfidenceRate
        {
            set
            {
                _topicConfidenceRate = value;
            }
            get
            {
                return _topicConfidenceRate;
            }
        }

        public ConnectCreateUserViewModel CurrentUserVM
        {
            get
            {
                return currentUserVM;
            }

            set
            {
                currentUserVM = value;
            }
        }

        public void SaveTopicVM()
        {
            topic.Name = this.TopicName;
            topic.UserId = currentUserVM.UserId;
            Repository.SaveTopic(topic);
        }

        public List<TopicViewModel> GetTopicsVM()
        {
            List<Topic> AllTopics = Repository.GetTopics().ToList();
            List<TopicViewModel> AllTopicsVM = new List<TopicViewModel>();
            if (AllTopicsVM != null)
            {
                foreach (Topic topic in AllTopics)
                {
                    TopicViewModel tvm = new TopicViewModel(currentUserVM);
                    tvm.TopicId = topic.ID;
                    tvm.TopicName = topic.Name;
                    tvm.TopicImage =  "TopicImage.png"; //SDI:Same image for all topics
                    AllTopicsVM.Add(tvm);
                }
            }
            return AllTopicsVM;
        }

        public List<TopicViewModel> GetTopicsForUserVM(ConnectCreateUserViewModel conUser)
        {
            
            List<Topic> AllTopics = Repository.GetTopicsByUserId(conUser.UserId).ToList();
            List<TopicViewModel> AllTopicsVM = new List<TopicViewModel>();
            if (AllTopicsVM != null)
            {
                foreach (Topic topic in AllTopics)
                {
                    TopicViewModel tvm = new TopicViewModel(currentUserVM);
                    tvm.TopicId = topic.ID;
                    tvm.TopicName = topic.Name;
                    tvm.TopicImage = "TopicImage.png"; //SDI:Same image for all topics
                    AllTopicsVM.Add(tvm);
                }
            }
            return AllTopicsVM;
        }

        public void DeleteAllTopics()
        {
            Repository.DeleteAllTopics();
        }

    }
}
