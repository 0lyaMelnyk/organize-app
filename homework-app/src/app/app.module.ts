import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import '@angular/compiler';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {TeamModule} from 'src/app/modules/team/team.module'
import { HttpInternalService } from './services/http-internal.service';
import { ProjectModule } from './modules/project/project.module';
import {UserModule} from './modules/user/user.module';
import {TaskModule} from './modules/task/task.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { MatCardModule } from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import { ExitUserGuard } from './modules/exit-user.guard';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    TeamModule,
    ProjectModule,
    UserModule,
    TaskModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule
  ],
  providers: [HttpInternalService, ExitUserGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
