﻿using System.Collections.Generic;

namespace ISTQB_Foundation_Questions.Models
{
    public class Question
    {
        public long Id;
        public string EnglishText;
        public string RussianText;
        public string Resource;
        public List<Answer> Answers;
        public string Theme;
    }
}