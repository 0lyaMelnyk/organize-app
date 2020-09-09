import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Team } from 'src/app/models/Team';
import { FormGroup, FormControl } from '@angular/forms';
import { TeamService } from 'src/app/services/team.service';
import {MatInput}from "@angular/material/input"
@Component({
  selector: 'app-edit-team',
  templateUrl: './edit-team.component.html',
  styleUrls: ['./edit-team.component.css']
})
export class EditTeamComponent implements OnInit {

  @Input() team: Team
  @Output() updateTeamRequest = new EventEmitter<Team>();
  constructor() { }

  ngOnInit(): void {
  }
  public updateTeam()
  {
    this.updateTeamRequest.emit(this.team);
  }
}
