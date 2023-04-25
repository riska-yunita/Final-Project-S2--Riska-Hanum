using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class Profiling
{
    public string EmployeeNik { get; set; }

    public int EducationId { get; set; }

    [JsonIgnore]
    public virtual Education? Education { get; set; } 

    [JsonIgnore]
    public virtual Employee? EmployeeNikNavigation { get; set; }
}
