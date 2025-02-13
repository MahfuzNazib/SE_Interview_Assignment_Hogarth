import { Component } from '@angular/core';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { UserComponent } from '../user/user.component';

@Component({
  selector: 'app-app-layout',
  standalone: true,
  imports: [NavBarComponent, UserComponent],
  templateUrl: './app-layout.component.html',
  styleUrl: './app-layout.component.css'
})
export class AppLayoutComponent {

}
