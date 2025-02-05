import { Component, OnInit } from '@angular/core';
import { User } from '../../../core/models/user.model';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../../core/services/user_service/user.service';
import { ApiResponse } from '../../../core/models/api-response.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-details',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.css'
})
export class UserDetailsComponent implements OnInit {
  user: User = {
    id: 0,
    firstName: '',
    lastName: '',
    active: true,
    company: '',
    sex: '',
    roleId: 0,
    contact: {
      id: 0,
      phone: '',
      address: '',
      city: '',
      country: '',
    },
    role: {
      id: 0,
      name: '',
    },
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService
  ) { }

  navigateToUserList() {
    this.router.navigate(['/user']);
  }

  ngOnInit(): void {
    const userId = this.route.snapshot.paramMap.get('id');
    if (userId) {
      this.loadUserDetails(+userId);
    }
  }

  loadUserDetails(userId: number): void {
    this.userService.GetUserById(userId).subscribe(
      (response: ApiResponse<User>) => {
        if (response.status && response.values) {
          this.user = response.values;
        } else {
          console.error('Error fetching user details', response.message);
        }
      },
      (error) => {
        console.error('Error fetching user details', error);
      }
    );
  }

  onSubmitUpdateUser() {
    this.userService.UpdateUser(this.user).subscribe(
      (response) => {
        if (response.status) {
          console.log('User updated successfully');
          this.navigateToUserList();
        } else {
          console.error('Error updating user:', response.message);
        }
      },
      (error) => {
        console.error('Error updating user:', error);
      }
    );
  }

  deleteUser(): void {
  if (confirm('Are you sure you want to delete this user?')) {
    this.userService.DeleteUser(this.user.id).subscribe(
      (response) => {
        if (response.status) {
          console.log('User deleted successfully');
          this.navigateToUserList();
        } else {
          console.error('Error deleting user:', response.message);
        }
      },
      (error) => {
        console.error('Error deleting user:', error);
      }
    );
  }
}

}
