import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Team } from 'src/app/models/Team';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.css']
})
export class TeamComponent implements OnInit {
  @Input() public team:Team;
  @Output() teamDeleteRequest=new EventEmitter<Team>();
  public isUpdate:boolean=false;
  constructor(private teamService:TeamService) { }

  ngOnInit(): void {
    
  }
  public addTeam(){

  }
  public deleteTeam(){
    this.teamDeleteRequest.emit(this.team);
    console.log("work");
    }
    public editTeam()
    {
      this.isUpdate=!this.isUpdate;
    }
    teamUpdateHandler(team:Team) {
      this.teamService.updateTeam(team)
        .subscribe(result => team = result);
    }

}
