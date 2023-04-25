using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class Account
{
    public string EmployeeNik { get; set; }

    public string Password { get; set; }

    [JsonIgnore]
    public virtual Employee? EmployeeNikNavigation { get; set; }  
    [JsonIgnore]
    public virtual ICollection<AccountRole>? TbTrAccountRoles { get; set; } 
}
