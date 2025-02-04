import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../../core/services/user_service/user.service';
import { User } from '../../../core/models/user.model';
import { PaginationRequest } from '../../../core/models/pagination-request.model';
import { ApiResponse } from '../../../core/models/api-response.model';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit {
  constructor(private router: Router, private userService: UserService) {}

  // Variable Declaration
  users: User[] = [];
  paginationRequest: PaginationRequest = {
    pageNumber: 1,
    pageSize: 10,
    searchValue: '',
    orderBy: 'Id',
    isAscending: true,
  };

  navigateToAddUser() {
    this.router.navigate(['/user/add']);
  }

  navigateToUserDetails(userId: number) {
    this.router.navigate([`/user/detail/${userId}`]);
  }


  ngOnInit(): void {
      this.getAllUsers();
  }

  getAllUsers(): void {
    this.userService.GetUsers(this.paginationRequest).subscribe(
      (response: ApiResponse<User[]>) => {
        if (response.status && response.values) {
          this.users = response.values;
        } else {
          console.error("Error fetching users", response.message);
        }
      },
      (error) => {
        console.error("Error fetching users", error);
      }
    )
  }
}
