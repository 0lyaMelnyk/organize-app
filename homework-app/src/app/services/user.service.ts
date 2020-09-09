import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpInternalService } from './http-internal.service';
import {User} from '../models/User'
@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly routePrefix: string = 'api/users';
  constructor(private httpService: HttpInternalService) { }

  public getAllUsers(): Observable<User[]> {
      return this.httpService.getRequest<User[]>(this.routePrefix);
  }
  public createUser(user:User):Observable<User>{
    return this.httpService.postRequest<User>(this.routePrefix,user);
  }
  public deleteUser(deletedUser:User){
    return this.httpService.deleteRequest<User>(`${this.routePrefix}/${deletedUser.id}`);
  }
  public updateUser(updatedUser:User):Observable<User>{
    return this.httpService.putRequest<User>(`${this.routePrefix}`,updatedUser);
  }
}
