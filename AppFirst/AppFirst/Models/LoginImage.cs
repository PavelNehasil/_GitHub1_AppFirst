using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Media.Imaging;

namespace AppFirst.Models
{
    [Table("LoginImages")]
    public class LoginImage : INotifyPropertyChanged
    {
        private int id;
        private string imageName;
        private string description;
        private byte[] image;
        private BitmapImage imageSource;
        private int countImages;

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

        public BitmapImage ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        public int CountImages
        {
            get => countImages;
            set => SetProperty(ref countImages, value);
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

        private FrameworkElement testButton3;

        public FrameworkElement TestButton3 { get => testButton3; set => SetProperty(ref testButton3, value); }

        public double IsUsed
        {
            get => (CountImages > 0) ? 1.0 : 0.0;
        }
    }
}
