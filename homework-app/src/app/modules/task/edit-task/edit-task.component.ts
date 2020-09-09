import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { Task } from 'src/app/models/Task';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.css']
})
export class EditTaskComponent implements OnInit {

  @Input() task:Task;
  @Output() updateTaskRequest = new EventEmitter<Task>();
  @ViewChild('taskForm', {static: false}) taskForm: NgForm;

  constructor() { }

  ngOnInit(): void {
  }
  public updateTask()
  {
    this.updateTaskRequest.emit(this.task);
  }
}
