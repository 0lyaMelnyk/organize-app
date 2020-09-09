import { Component, OnInit, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { Team } from 'src/app/models/Team';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { TeamService } from 'src/app/services/team.service';
import { HttpResponse } from '@angular/common/http';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-create-team',
  templateUrl: './create-team.component.html',
  styleUrls: ['./create-team.component.css']
})
export class CreateTeamComponent implements OnInit {

  @Output() addTeam: EventEmitter<Team> = new EventEmitter<Team>();

  teamForm=new FormGroup({
    "name":new FormControl("Alex", Validators.required),
    "createdAt":new FormControl("10.01.1001")
  });
  constructor(private teamService:TeamService) { }

  ngOnInit(): void {
  }
  saveTeam() {
    const team:Team=<Team>this.teamForm.value;
    let createdTeam:Team;
    this.teamService.createTeam(team).subscribe(
      (response: any) =>
       {
          const body: Team = response.body;
          const teamId: number = this.getIdFromLocation(response.headers.get('location'));

          createdTeam = {
              id: teamId,
              name: body.name,
              createdAt: body.createdAt
          };

          this.addTeam.emit(createdTeam);
      },
      (error) => console.log(error)
  )  }
  private getIdFromLocation(str: string): number {
    const startingIndex: number = str.lastIndexOf('/') + 1;
    return Number(str.slice(startingIndex, str.length));
}
  
}
