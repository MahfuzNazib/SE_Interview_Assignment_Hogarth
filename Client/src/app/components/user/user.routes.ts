import { Routes } from "@angular/router";
import { UserComponent } from "./user.component";
import { UserListComponent } from "./user-list/user-list.component";
import { AddUserComponent } from "./add-user/add-user.component";
import { UserDetailsComponent } from "./user-details/user-details.component";

export const USER_ROUTES: Routes = [
    {
      path: '',
      component: UserComponent,
      children: [
        { path: '', component: UserListComponent }, // Default to UserList
        { path: 'add', component: AddUserComponent },
        { path: 'detail/:id', component: UserDetailsComponent },
      ],
    },
  ];
