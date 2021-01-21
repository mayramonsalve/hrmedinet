using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{

    public class FeedbackViewModel {
        public Feedback Feedback { get; private set; }
        public SelectList OptionsList { get; private set; }
        public Dictionary<int, string> FeaturesList { get; private set; }
        public int ScoreCount { get; private set; }
        public Dictionary<int, List<int>> ScoreAverageByFeature { get; private set; }
        public Dictionary<int, int> ScoreByFeature { get; private set; }
        public List<string> AddCommentStrings { get; private set; }
        public List<string> CommentStrings { get; private set; }
        public SelectList TypesList { get; private set; }
        public string TypeName { get; private set; }
        public bool showDialog { get; private set; }

        public FeedbackViewModel(SelectList typesList)
        {
            this.TypesList = typesList;
        }

        public FeedbackViewModel(Feedback Feedback, Dictionary<int, string> Features, SelectList Options,
                                    int ScoreCount, Dictionary<int, int> ScoreByFeature)
        {
            this.Feedback = Feedback;
            this.FeaturesList = Features;
            this.OptionsList = Options;
            this.ScoreCount = ScoreCount;
            this.ScoreByFeature = ScoreByFeature;
        }

        public FeedbackViewModel(Dictionary<int, string> Features, SelectList Options,
                                    Dictionary<int, List<int>> ScoreAverageByFeature,
                                    List<string> AddCommentStrings, List<string> CommentStrings,
                                    SelectList TypesList, string TypeName)
        {
            this.FeaturesList = Features;
            this.OptionsList = Options;
            this.ScoreAverageByFeature = ScoreAverageByFeature;
            this.AddCommentStrings = AddCommentStrings;
            this.CommentStrings = CommentStrings;
            this.TypesList = TypesList;
            this.TypeName = TypeName;
        }

        public FeedbackViewModel(Feedback Feedback, Dictionary<int, string> Features, SelectList Options, bool show)
        {
            this.Feedback = Feedback;
            this.FeaturesList = Features;
            this.OptionsList = Options;
            this.showDialog = show;
        }

        public SelectList GetSelectListByFeature(SelectList options, int score)
        {
            SelectList select = new SelectList(options.Items, "Key", "Value", score);
            return select;
        }

        public SelectList GetAverageSelectListByFeature(SelectList options, List<int> scores)
        {
            double avg = scores.Average();
            int score = Convert.ToInt32(avg*4);
            SelectList select = new SelectList(options.Items, "Key", "Value", score);
            return select;
        }

        private int GetHeigthForAccordion()
        {
            int stars = FeaturesList.Count * 30;
            int add = AddCommentStrings.Count * 10;
            int comments = CommentStrings.Count * 10;
            int heigth = stars;
            
            return heigth;
        }

        public string GetStringHeigthForAccordion()
        {
            int heigth = GetHeigthForAccordion();
            return "style=height:" + heigth + "px;";
        }
    }
}
    
