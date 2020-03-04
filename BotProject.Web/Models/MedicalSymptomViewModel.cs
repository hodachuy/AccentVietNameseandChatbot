using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class MedicalSymptomViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Cause { set; get; }
        public string Treament { set; get; }
        public string Advice { set; get; }
        public string Symptoms { set; get; }// Triệu chứng
        public string Predict { set; get; }// Tiên đoán
        public string Protect { set; get; }// Phòng ngừa
        public string DoctorCanDo { set; get; }// Bác sĩ có thể làm gì
        public string ContentHTML { set; get; }
        public string FileName { set; get; }
        public int? BotID { set; get; }
    }
}