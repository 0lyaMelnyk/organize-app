import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';
import { Subject } from 'rxjs';
import { takeUntil} from 'rxjs/operators';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  public users: User[];
  private unsubscribe$ = new Subject<void>();
  public isCreateUser:boolean=false;
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getAllUsers()
    .subscribe(result => this.users =result);
  }
  public deleteUser(deletedUser: User) {
    let userId = this.users.findIndex(
      (user) => user.id == deletedUser.id
    );
    this.users.splice(userId, 1);
  }
  public sendDeleteUser(deletedUser: User) {
    this.userService.deleteUser(deletedUser).subscribe(()=>this.users);
    this.users=this.users.filter(user=>user.id!=deletedUser.id);
    }
}
