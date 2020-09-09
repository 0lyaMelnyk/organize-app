import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Task } from 'src/app/models/Task';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent implements OnInit {

  @Output() addTask: EventEmitter<Task> = new EventEmitter<Task>();

  taskForm=new FormGroup({
    "name":new FormControl("Alex", Validators.required),
    "description":new FormControl("Asap", Validators.required),
    "deadline":new FormControl("01.01.1001"),
    "finisheddAt":new FormControl("01.01.2001"),
    "projectId":new FormControl("1"),
    "performerId":new FormControl("1")
  });
  constructor(private taskService:TaskService) { }

  ngOnInit(): void {
  }
  saveUser() {
    const task:Task=<Task>this.taskForm.value;
    let createdTask:Task;
    this.taskService.createTask(task).subscribe(
      (response: any) =>
       {
          const body: Task = response.body;
          const taskId: number = 0;

          createdTask = {
              id: taskId,
              name: body.name,
              description: body.description,
              createdAt:body.createdAt,
              finishedAt: body.finishedAt,
              projectId: body.projectId,
              performerId:body.performerId,
              state:body.state
          };

          this.addTask.emit(createdTask);
      },
      (error) => console.log(error)
  )  }

}
