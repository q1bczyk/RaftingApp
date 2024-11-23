import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AdminNavComponent } from './components/admin-nav/admin-nav.component';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [RouterOutlet, AdminNavComponent],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent {

}
