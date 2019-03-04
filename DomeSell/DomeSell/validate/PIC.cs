using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomeSell.Models
{
    public class PIC
    {
        [DisplayName("รูปภาพ")]
        public HttpPostedFileBase UPostedFile { get; set; }
    }

    public class PICPAY
    {
        [DisplayName("รูปภาพ")]
        public HttpPostedFileBase OPostedFile { get; set; }
    }
}