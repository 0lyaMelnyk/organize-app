import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {TeamService} from 'src/app/services/team.service';
import { TeamsComponent } from './teams/teams.component';
import { TeamComponent } from './team/team.component';
import { CreateTeamComponent } from './create-team/create-team.component'
import { MatCardModule } from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { EditTeamComponent } from './edit-team/edit-team.component';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [TeamsComponent, TeamComponent, CreateTeamComponent, EditTeamComponent],
  imports: [
    CommonModule,
    MatCardModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatButtonModule
 
  ],
  providers:[
    TeamService,
    MatDatepickerModule,  
  ]
})
export class TeamModule { }
