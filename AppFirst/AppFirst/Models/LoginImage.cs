using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace AppFirst.Models
{
    [Table("LoginImages")]
    public class LoginImage : INotifyPropertyChanged
    {
        private int id;
        private string imageName;
        private string description;
        private byte[] image;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        [Required]
        [StringLength(250)]
        public string ImageName
        {
            get => imageName;
            set => SetProperty(ref imageName, value);
        }

        [StringLength(500)]
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public byte[] Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public override bool Equals(object obj)
        {
            return Id == (obj as LoginImage).Id;
        }
    }
}
