using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Firewood.Models
{
    public class PublishForm
    {
        public string actname { set; get; }

        public string place { set; get; }

        public string class1 { set; get; }

        public string class2 { set; get; }

        public DateTime begintime { set; get; }

        public DateTime endtime { set; get; }

        public string money { set; get; }

        public string score { set; get; }

        public string award { set; get; }

        public string vote { set; get; }

        public string actintro { set; get; }

    }
}