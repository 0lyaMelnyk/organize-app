import { NgModule, LOCALE_ID } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user/user.component';
import { UsersComponent } from './users/users.component';
import { UserService } from 'src/app/services/user.service';
import { MatCardModule } from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import { UpdateUserComponent } from './update-user/update-user.component';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { registerLocaleData } from '@angular/common';
import localeUa from '@angular/common/locales/ru-UA';
import {ExitUserGuard} from '../exit-user.guard';
import { CreateUserComponent } from './create-user/create-user.component'
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
registerLocaleData(localeUa, 'ru-UA');

@NgModule({
  declarations: [UsersComponent, UserComponent, UpdateUserComponent, CreateUserComponent],
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    FormsModule,
    MatInputModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatButtonModule

  ],
  providers:[
    UserService,
    { provide: LOCALE_ID, useValue: 'ru-UA' },
    ExitUserGuard
  ]
})
export class UserModule { }
