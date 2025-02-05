import { Component } from '@angular/core';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-swal-alert',
  standalone: true,
  imports: [],
  templateUrl: './swal-alert.component.html',
  styleUrl: './swal-alert.component.css'
})
export class SwalAlertComponent {
  static showAlert(title: string, text: string, icon: 'success' | 'error' | 'warning' | 'info' | 'question', buttonText?: string) {
    Swal.fire({
      title: title,
      text: text,
      icon: icon,
      confirmButtonText: buttonText || 'OK'
    });
  }

  static showConfirmationAlert(title: string, text: string, confirmButtonText: string = 'Yes', cancelButtonText: string = 'No') {
    return Swal.fire({
      title: title,
      text: text,
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: confirmButtonText,
      cancelButtonText: cancelButtonText,
      reverseButtons: true
    }).then((result) => {
      return result.isConfirmed;
    });
  }
}
