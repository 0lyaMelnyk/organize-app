import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Project } from 'src/app/models/Project';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ProjectService } from 'src/app/services/project.service';
import { ProjectComponent } from '../project/project.component';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent implements OnInit {

  @Output() addProject: EventEmitter<Project> = new EventEmitter<Project>();

  projectForm=new FormGroup({
    "name":new FormControl("Letto", Validators.required),
    "description":new FormControl("Asap", Validators.required),
    "deadline":new FormControl("01.01.1001"),
    "finisheddAt":new FormControl("01.01.2001"),
    "teamId":new FormControl("1"),
    "authorId":new FormControl("1")
  });
  constructor(private projectService:ProjectService) { }

  ngOnInit(): void {
  }
  saveProject() {
    const project:Project=<Project>this.projectForm.value;
    let createdProject:Project;
    this.projectService.createProject(project).subscribe(
      (response: any) =>
       {
          const body: Project = response.body;
          createdProject = {
              name: body.name,
              description: body.description,
              createdAt: body.createdAt,
              teamId: body.teamId,
              authorId:body.authorId,
              deadline:body.deadline,
              id:null
           };

          this.addProject.emit(createdProject);
      },
      (error) => console.log(error)
  )  
}
}
