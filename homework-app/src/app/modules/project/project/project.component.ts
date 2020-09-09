import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import { Project } from 'src/app/models/Project';
import { ProjectService } from 'src/app/services/project.service';
@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {
  @Input() public project:Project;
  @Input() projectDeleteRequest=new EventEmitter<Project>();
  public isEditProject:boolean=false;
  constructor(private projectService:ProjectService) { }

  ngOnInit(): void {
  }

  public deleteProject(){
    this.projectDeleteRequest.emit(this.project);
  }
  public editProject(){
    this.isEditProject=!this.isEditProject;
  }
  projectUpdateHandler(project:Project) {
    this.projectService.updateProject(project)
      .subscribe(result => project = result);
  }

}
