import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../../core/services/user_service/user.service';
import { User } from '../../../core/models/user.model';
import { Contact } from '../../../core/models/contact.model';

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

  // Handle form submission
  onSubmitAddUser() {
    this.userService.AddUser(this.user).subscribe(
      (response) => {
        if (response.status) {
          console.log('User added successfully');
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
