import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { TeamsComponent } from './modules/team/teams/teams.component';
import { ProjectsComponent } from './modules/project/projects/projects.component';
import { UsersComponent } from './modules/user/users/users.component';
import { TasksComponent } from './modules/task/tasks/tasks.component';
import { ExitUserGuard } from './modules/exit-user.guard';

const routes: Routes = [
  {path:'', component:TeamsComponent, pathMatch:'full'},
  {path:'teams', component:TeamsComponent, pathMatch:'full'},
  {path:'users', component:UsersComponent, pathMatch:'full', canDeactivate:[ExitUserGuard]},
  {path:'tasks', component:TasksComponent, pathMatch:'full'},
  {path:'projects', component:ProjectsComponent, pathMatch:'full'},
  {path:'**', redirectTo:''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
