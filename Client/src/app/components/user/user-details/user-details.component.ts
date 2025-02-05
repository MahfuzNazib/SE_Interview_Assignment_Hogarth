import { Component, OnInit } from '@angular/core';
import { User } from '../../../core/models/user.model';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../../core/services/user_service/user.service';
import { ApiResponse } from '../../../core/models/api-response.model';
import { FormsModule } from '@angular/forms';
import { SwalAlertComponent } from '../../swal-alert/swal-alert.component';

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

  updateUserValidation() {
    if (this.user.firstName === '') {
      SwalAlertComponent.showAlert("Warning", "First name is required", "warning");
      return false;
    }
    if (this.user.lastName === '') {
      SwalAlertComponent.showAlert("Warning", "Last name is required", "warning");
      return false;
    }
    if (this.user.contact.phone === '') {
      SwalAlertComponent.showAlert("Warning", "Phone number is required", "warning");
      return false;
    }

    if (this.user.contact.phone.length > 11) {
      SwalAlertComponent.showAlert("Warning", "Phone number cannot exceed 11 digit", "warning");
      return false;
    }

    if (this.user.contact.address === '') {
      SwalAlertComponent.showAlert("Warning", "Address is required", "warning");
      return false;
    }
    if (this.user.contact.city === '') {
      SwalAlertComponent.showAlert("Warning", "City is required", "warning");
      return false;
    }
    if (this.user.contact.country === '') {
      SwalAlertComponent.showAlert("Warning", "Country is required", "warning");
      return false;
    }
    if (this.user.roleId === 0) {
      SwalAlertComponent.showAlert("Warning", "Role is required", "warning");
      return false;
    }
    if (this.user.sex === '') {
      SwalAlertComponent.showAlert("Warning", "Sex is required", "warning");
      return false;
    }
    this.onSubmitUpdateUser();
    return true;
  }

  onSubmitUpdateUser() {
    this.userService.UpdateUser(this.user).subscribe(
      (response) => {
        if (response.status) {
          SwalAlertComponent.showAlert("Success", "User updated successfully", "success");
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
    SwalAlertComponent.showConfirmationAlert(
      'Are you sure?',
      'Are you sure you want to delete this user?',
      'Yes, delete it!',
      'No, cancel!'
    ).then((confirmed) => {
      if (confirmed) {
        this.userService.DeleteUser(this.user.id).subscribe(
          (response) => {
            if (response.status) {
              SwalAlertComponent.showAlert("Success", "User deleted successfully", "success");
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
    });
  }

}
