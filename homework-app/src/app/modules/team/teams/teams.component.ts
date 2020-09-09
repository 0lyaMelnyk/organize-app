import { Component, OnInit } from '@angular/core';
import { Team } from 'src/app/models/Team';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit {

  public teams: Team[];
  public isCreateTeam:boolean=false;
  constructor(private teamService: TeamService) { }

  ngOnInit(): void {
    this.teamService.getAllTeams()
    .subscribe(result => this.teams = result);
  }
  public addTeam(team:Team){
    this.teams.push(team);
    this.isCreateTeam=false;
  }
  public deleteTeam(deletedTeam:Team) {
    let teamId = this.teams.findIndex(
      (team) => team.id == deletedTeam.id
    );
    this.teams.splice(teamId, -1);
  }
  public sendDeleteTeam(deletedTeam: Team) {
    this.teamService.deleteTeam(deletedTeam).subscribe(()=>this.teams);
    this.teams=this.teams.filter(team=>team.id!=deletedTeam.id);
    }

}
