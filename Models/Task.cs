using RESTfulAPI.DTO;

namespace RESTfulAPI.Models
{
    public class Task{
        public int Id {get; set;}
        public string? Title {get; set;}
        public string? Description {get; set;}
        public bool? IsDone {get; set;}

        public Task() {}
        public Task(NewTask newContent) {
            Title = newContent.Title;
            Description = newContent.Description;
        }
        public void Update(TaskDTO newContent){
            Title = newContent.Title;
            Description = newContent.Description;
            IsDone = newContent.IsDone;
        }
        public void ChangeStatus(){
            if(IsDone.HasValue){
                IsDone = !IsDone;
            }else{
                IsDone = false;
            }
        }
    }
    
}