import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {

  @Input() user:User;
  @Output() updateUserRequest = new EventEmitter<User>();
  constructor() { }

  ngOnInit(): void {
  }
  public updateUser()
  {
    this.updateUserRequest.emit(this.user);
  }
  
}
