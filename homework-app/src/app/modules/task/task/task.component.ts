import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import { Task } from 'src/app/models/Task';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  @Input() public task:Task;
  @Input() taskDeleteRequest=new EventEmitter<Task>();
  public isEditTask:boolean=false;
  constructor(private taskService:TaskService) { }

  ngOnInit(): void {
  }

  public deleteTask(){
    this.taskDeleteRequest.emit(this.task);
  }
  public editTask(){
    this.isEditTask=!this.isEditTask;
    console.log("work");
  }
  taskUpdateHandler(task: Task) {
    this.taskService.updateTask(task)
      .subscribe(result => task = result);
  }

}
