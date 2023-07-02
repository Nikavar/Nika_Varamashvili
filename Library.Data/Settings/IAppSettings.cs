using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Settings
{
    public interface IAppSettings
    {
        string Secret { get; set; }
        string PasswordHashSecret {get; set;}
    }
}
