using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace DBLite.Models
{
    public class Classroom : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private Color _color;
        [Key]
        public int Id { get { return _id; } set { _id = value; NotifyPropertyChanged(); } }
        [Required]
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }
        public Int32 Argb
        {
            get
            {
                return Color.ToArgb();
            }
            set
            {
                Color = Color.FromArgb(value);
            }
        }

        [NotMapped]
        public Color Color { get { return _color; } set { _color = value; NotifyPropertyChanged(); } }
        public ICollection<Student> Students { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
