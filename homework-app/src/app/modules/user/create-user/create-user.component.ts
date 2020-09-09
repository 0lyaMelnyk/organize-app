import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { User } from 'src/app/models/User';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {

  @Output() addUser: EventEmitter<User> = new EventEmitter<User>();

  userForm=new FormGroup({
    "firstName":new FormControl("Alex", Validators.required),
    "lastName":new FormControl("Asap", Validators.required),
    "email":new FormControl("bla@gmail.com", Validators.email),
    "birthday":new FormControl("01.01.1001"),
    "registeredAt":new FormControl("01.01.2001"),
    "teamId":new FormControl("1")
  });
  constructor(private userService:UserService) { }

  ngOnInit(): void {
  }
  saveUser() {
    const user:User=<User>this.userForm.value;
    let createdUser:User;
    this.userService.createUser(user).subscribe(
      (response: any) =>
       {
          const body: User = response.body;
          const userId: number = this.getIdFromLocation(response.headers.get('location'));

          createdUser = {
              id: userId,
              firstName: body.firstName,
              lastName: body.lastName,
              email: body.email,
              birthday:body.birthday,
              registeredAt: body.registeredAt,
              teamId: body.teamId
          };

          this.addUser.emit(createdUser);
      },
      (error) => console.log(error)
  )  }
  private getIdFromLocation(str: string): number {
    const startingIndex: number = str.lastIndexOf('/') + 1;
    return Number(str.slice(startingIndex, str.length));
}

}
