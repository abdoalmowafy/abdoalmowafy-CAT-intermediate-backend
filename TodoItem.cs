using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoListApi
{
    public class TodoItem
    {
        [Key] public int Id { get; set; }
        [Required] public required string Title { get; set; }
        [Required][JsonConverter(typeof(JsonStringEnumConverter))] public required Status Status { get; set; } = Status.pending;
    }


    public enum Status
    {
        pending,
        completed
    }
}

