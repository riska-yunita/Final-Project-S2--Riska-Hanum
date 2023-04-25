using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class AccountRole
{
    public int Id { get; set; }

    public string AccountNik { get; set; }

    public int RoleId { get; set; }

    [JsonIgnore]
    public virtual Account? AccountNikNavigation { get; set; }

    [JsonIgnore]
    public virtual Role? Role { get; set; }
}
