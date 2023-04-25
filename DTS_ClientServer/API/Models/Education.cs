using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class Education
{
    public int Id { get; set; }

    public string Major { get; set; }

    public string Degree { get; set; }

    public double Gpa { get; set; }

    public int UniversityId { get; set; }

    [JsonIgnore]
    public virtual Profiling? TbTrProfiling { get; set; }

    [JsonIgnore]
    public virtual University? University { get; set; }
}
