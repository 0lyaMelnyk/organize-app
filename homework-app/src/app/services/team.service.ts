import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpInternalService } from './http-internal.service';
import {Team } from '../models/Team'
@Injectable({
  providedIn: 'root'
})
export class TeamService {

  readonly routePrefix: string = 'api/teams';
  constructor(private httpService: HttpInternalService) { }

  public getAllTeams(): Observable<Team[]> {
      return this.httpService.getRequest<Team[]>(this.routePrefix);
  }
  public getTeam(id:number):Observable<Team>{
    return this.httpService.getRequest<Team>(`${this.routePrefix}/${id}`);
  }
  public deleteTeam(team:Team){
    return this.httpService.deleteRequest(`${this.routePrefix}/${team.id}`)
  }
  public createTeam(team:Team){
    return this.httpService.postRequest<Team>(this.routePrefix,team);
  }
  public updateTeam(updatedTeam:Team){
    return this.httpService.putRequest<Team>(`${this.routePrefix}`, updatedTeam);
  }

}
