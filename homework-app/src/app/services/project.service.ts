import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { Observable } from 'rxjs';
import {Project} from '../models/Project'
@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  readonly routPrefix: string = 'api/projects/';
  constructor(private httpService: HttpInternalService) { }

  public getAllProjects(): Observable<Project[]> {
      return this.httpService.getRequest<Project[]>(this.routPrefix);
  }
  public createProject(project:Project){
    return this.httpService.postRequest<Project>(this.routPrefix,project);
  }
  public updateProject(updatedProject:Project):Observable<Project>{
    return this.httpService.putRequest<Project>(this.routPrefix,updatedProject);
  }
  public deleteProject(project:Project):Observable<Project>{
    return this.httpService.deleteRequest<Project>(this.routPrefix+project.id)
  }
}
