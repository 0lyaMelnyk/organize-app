import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from 'src/app/models/User';
import {MatIconModule} from '@angular/material/icon';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @Input() user:User;
  @Output() deleteUserRequest = new EventEmitter<User>();
  isEditUser:boolean=false;
  constructor(private userService:UserService) { }

  ngOnInit(): void {
  }
  public deleteUser() {
    this.deleteUserRequest.emit(this.user);
  }
  public editUser()
  {
    this.isEditUser=!this.isEditUser;
  }
  userUpdateHandler(user: User) {
    this.userService.updateUser(user)
      .subscribe(result => user = result);
  }

}
