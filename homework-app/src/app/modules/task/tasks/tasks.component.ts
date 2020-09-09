import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/models/Task';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  public tasks: Task[];
  public isCreateTask:boolean=false;
  constructor(private taskService: TaskService) { }

  ngOnInit(): void {
    this.taskService.getAllTasks()
    .subscribe(result => this.tasks = result);
  }
  public addTask(task:Task){
    this.tasks.push(task);
    this.isCreateTask=false;
  }

}
