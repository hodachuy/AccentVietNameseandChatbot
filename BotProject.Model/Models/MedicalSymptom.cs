using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("MedicalSymptoms")]
    public class MedicalSymptom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }// Mô tả
        public string Cause { set; get; }// Nguyên nhân
        public string Treament { set; get; }// Điều trị
        public string Advice { set; get; }// Lời khuyên bác sĩ
        public string Symptoms { set; get; }// Triệu chứng
        public string Predict { set; get; }// Tiên đoán
        public string Protect { set; get; }// Phòng ngừa
        public string DoctorCanDo { set; get; }// Bác sĩ có thể làm gì
        public string ContentHTML { set; get; }
        public string FileName { set; get; }
        public int? BotID { set; get; }
    }
}
