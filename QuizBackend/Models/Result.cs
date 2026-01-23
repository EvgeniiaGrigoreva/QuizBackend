using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuizBackend.Models;

public partial class Result
{
    public int Id { get; set; }

    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("CorAnswer")]
    public int CorAnswer { get; set; }
}
