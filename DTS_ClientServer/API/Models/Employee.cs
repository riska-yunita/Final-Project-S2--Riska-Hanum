using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class Employee
{
    public string Nik { get; set; }

    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime Birthdate { get; set; }

    public Gender Gender { get; set; }

    public DateTime HiringDate { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    [JsonIgnore]
    public virtual Account? TbMAccount { get; set; }

    [JsonIgnore]
    public virtual Profiling? TbTrProfiling { get; set; }
}
public enum Gender { Male, Female }

