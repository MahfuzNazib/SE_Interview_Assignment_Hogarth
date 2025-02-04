import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent {
  constructor(private router: Router) {}

  navigateToAddUser() {
    this.router.navigate(['/user/add']);
  }

  navigateToUserDetails(userId: number) {
    this.router.navigate([`/user/detail/${userId}`]);
  }
}
