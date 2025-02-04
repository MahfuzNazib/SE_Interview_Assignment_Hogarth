import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../../core/services/user_service/user.service';
import { User } from '../../../core/models/user.model';
import { PaginationRequest } from '../../../core/models/pagination-request.model';
import { ApiResponse } from '../../../core/models/api-response.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit {
  constructor(private router: Router, private userService: UserService) {}

  // Variable Declaration
  users: User[] = [];
  totalPages: number = 0;
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
      if (response.status && response.values && response.paginationSummary) {
        this.users = response.values;
        this.totalPages = Math.ceil(response.paginationSummary.totalCount / this.paginationRequest.pageSize);
      } else {
        console.error("Error fetching users", response.message);
      }
    },
    (error) => {
      console.error("Error fetching users", error);
    }
  );
}

  onPageChange(pageNumber: number): void {
    if (pageNumber >= 1 && pageNumber <= this.totalPages) {
      this.paginationRequest.pageNumber = pageNumber;
      this.getAllUsers();
    }
  }

  onPageSizeChange(): void {
    this.paginationRequest.pageNumber = 1;
    this.getAllUsers();
  }

  onSearchChange(): void {
    this.paginationRequest.pageNumber = 1;
    this.getAllUsers();
  }

  getPages(): number[] {
    const pages: number[] = [];
    for (let i = 1; i <= this.totalPages; i++) {
      pages.push(i);
    }
    return pages;
  }

  sortBy(column: string): void {
    if (this.paginationRequest.orderBy === column) {
      this.paginationRequest.isAscending = !this.paginationRequest.isAscending;
    } else {
      this.paginationRequest.orderBy = column;
      this.paginationRequest.isAscending = true;
    }
    this.getAllUsers();
  }
}
