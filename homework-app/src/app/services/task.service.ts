import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { Observable } from 'rxjs';
import {Task} from '../models/Task'
@Injectable({
  providedIn: 'root'
})
export class TaskService {

  readonly routePrefix: string = 'api/tasks';
  constructor(private httpService: HttpInternalService) { }

  public getAllTasks(): Observable<Task[]> {
      return this.httpService.getRequest<Task[]>(this.routePrefix);
  }
  public updateTask(updatedTask:Task){
    return this.httpService.putRequest<Task>(`${this.routePrefix}`,updatedTask);
  }
  public createTask(task:Task){
    return this.httpService.postRequest<Task>(`${this.routePrefix}`,task);
  }
}
