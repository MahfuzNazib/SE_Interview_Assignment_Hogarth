import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../../core/services/user_service/user.service';
import { User } from '../../../core/models/user.model';
import { SwalAlertComponent } from '../../swal-alert/swal-alert.component';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css',
})
export class AddUserComponent implements OnInit {
  user: User = {
    id: 0,
    firstName: '',
    lastName: '',
    active: true,
    company: '',
    sex: 'M',
    roleId: 1,
    contact: {
      phone: '',
      address: '',
      city: '',
      country: '',
    },
    role: { id: 0, name: '' },
  };

  constructor(private router: Router, private userService: UserService) {}

  ngOnInit(): void {}

  // Navigate back to the user list
  navigateToUserList() {
    this.router.navigate(['/user']);
  }

  addUserValidation() {
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
    this.onSubmitAddUser();
    return true;
  }

  // Handle form submission
  onSubmitAddUser() {
    this.userService.AddUser(this.user).subscribe(
      (response) => {
        if (response.status) {
          SwalAlertComponent.showAlert("Success", "User added successfully", "success");
          this.navigateToUserList(); // Redirect to user list after successful addition
        } else {
          console.error('Error adding user:', response.message);
        }
      },
      (error) => {
        console.error('Error adding user:', error);
      }
    );
  }

  // Reset the form
  resetForm() {
    this.user = {
      id: 0,
      firstName: '',
      lastName: '',
      active: true,
      company: '',
      sex: 'M',
      roleId: 1,
      contact: {
        phone: '',
        address: '',
        city: '',
        country: '',
      },
      role: { id: 0, name: '' },
    };
  }
}
