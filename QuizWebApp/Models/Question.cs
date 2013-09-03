using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApp.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        public string OwnerUserId { get; set; }

        [Display(Name = "問題本文"), Required, AllowHtml]
        public string Body { get; set; }

        [Display(Name = "問題本文の書式"), Required]
        public TextFormatType BodyFormat { get; set; }

        [Display(Name = "回答選択肢1"), Required, AllowHtml]
        public string Option1 { get; set; }
        [Display(Name = "回答選択肢画像1")]
        public string OptionImage1 { get; set; }

        [Display(Name = "回答選択肢2"), Required, AllowHtml]
        public string Option2 { get; set; }
        [Display(Name = "回答選択肢画像2")]
        public string OptionImage2 { get; set; }

        [Display(Name = "回答選択肢3"), AllowHtml]
        public string Option3 { get; set; }
        [Display(Name = "回答選択肢画像3")]
        public string OptionImage3 { get; set; }
        
        [Display(Name = "回答選択肢4"), AllowHtml]
        public string Option4 { get; set; }
        [Display(Name = "回答選択肢画像4")]
        public string OptionImage4 { get; set; }

        [Display(Name = "回答選択肢5"), AllowHtml]
        public string Option5 { get; set; }
        [Display(Name = "回答選択肢画像5")]
        public string OptionImage5 { get; set; }

        [Display(Name = "回答選択肢6"), AllowHtml]
        public string Option6 { get; set; }
        [Display(Name = "回答選択肢画像6")]
        public string OptionImage6 { get; set; }

        public OptionViewModel[] GetOptions(bool trim = true)
        {
            Func<OptionViewModel, bool> filter = trim ?
                (Func<OptionViewModel, bool>)(opt => string.IsNullOrEmpty(opt.Option) == false) :
                (Func<OptionViewModel, bool>)(_ => true);

            return new[] { 
                new OptionViewModel(1, Option1, OptionImage1),
                new OptionViewModel(2, Option2, OptionImage2),
                new OptionViewModel(3, Option3, OptionImage3),
                new OptionViewModel(4, Option4, OptionImage4),
                new OptionViewModel(5, Option5, OptionImage5),
                new OptionViewModel(6, Option6, OptionImage6)
            }.Where(filter).ToArray();
        }

        [Display(Name = "正解の選択肢の番号")]
        public int IndexOfCorrectOption { get; set; }

        [Display(Name = "解説"), AllowHtml]
        public string Comment { get; set; }

        [Display(Name = "問題本文の書式"), Required]
        public TextFormatType CommentFormat { get; set; }

        //public string Category { get; set; }

        [Display(Name = "投稿日時")]
        public DateTime CreateAt { get; set; }

        public Question()
        {
            this.CreateAt = DateTime.UtcNow;
        }
    }
}