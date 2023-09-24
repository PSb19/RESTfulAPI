namespace RESTfulAPI.DTO
{
    public class TaskDTO{
        public string? Title {get; set;}
        public string? Description {get; set;}
        public bool IsDone {get; set;}
    }
    public class NewTask{
        public string? Title {get; set;}
        public string? Description {get; set;}
    }
    public class TaskShortDTO{
        public int Id {get; set;}
        public string? Title {get; set;}
        public bool IsDone {get; set;}

        public TaskShortDTO(Models.Task task){
            this.Id = task.Id;
            this.Title = task.Title;
            this.IsDone = task.IsDone;
        }
    }
    
}