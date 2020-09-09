import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';
import {Project} from 'src/app/models/Project'
@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  public projects: Project[];
  public isCreateProject:boolean=false;

  constructor(private projectService: ProjectService) { }

  ngOnInit(): void {
    this.projectService.getAllProjects()
    .subscribe(result => this.projects = result);
  }
 
  public addProject(project:Project){
    this.projects.push(project);
    this.isCreateProject=false;
  }
  public deleteProject(deletedProject:Project) {
    let projectId = this.projects.findIndex(
      (project) => project.id == deletedProject.id
    );
    this.projects.splice(projectId, -1);
  }
  public sendDeleteProject(deletedProject:Project) {
    this.projectService.deleteProject(deletedProject).subscribe(()=>this.projects);
    this.projects=this.projects.filter(project=>project.id!=deletedProject.id);
    }
}
