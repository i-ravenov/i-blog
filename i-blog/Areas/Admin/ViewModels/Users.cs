using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using i_blog.Models;

namespace i_blog.Areas.Admin.ViewModels
{
    public class IndexUser
    {
        public IEnumerable<User> Users { get; set; }
    }

    public class NewUser
    {
        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class EditUser
    {
        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class ResetPasswordUser
    {
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}