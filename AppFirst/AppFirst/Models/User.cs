using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Media.Imaging;

namespace AppFirst.Models
{
    [Table("Users")]
    public class User : INotifyPropertyChanged
    {
        private int id;
        private int idLoginImage;
        private string userName;
        private string password;
        private bool isAdmin;
        private string email;
        private string firstName;
        private string lastName;
        private  WriteableBitmap imageSource;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        [Required]
        public int IdLoginImage
        {
            get => idLoginImage;
            set => SetProperty(ref idLoginImage, value);
        }

        [Required]
        [StringLength(50)]
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        [Required]
        public bool IsAdmin
        {
            get => isAdmin;
            set => SetProperty(ref isAdmin, value);
        }

        [StringLength(200)]
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        [StringLength(100)]
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        [StringLength(100)]
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        public WriteableBitmap ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
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
            return Id == (obj as User).Id;
        }
    }
}
