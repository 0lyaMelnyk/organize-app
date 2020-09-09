import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Project } from 'src/app/models/Project';
import { ProjectComponent } from '../project/project.component';
import { ProjectService } from 'src/app/services/project.service';
import { FormGroup, FormControl, FormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.css']
})
export class EditProjectComponent implements OnInit {

  @Input() project:Project;
  @Output() updateProjectRequest = new EventEmitter<Project>();
  constructor() { }

  ngOnInit(): void {
  }
  public updateProject()
  {
    this.updateProjectRequest.emit(this.project);
  }

}
