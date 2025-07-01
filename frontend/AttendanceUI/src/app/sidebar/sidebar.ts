import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClientService } from '../services/http-client-service';

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css',
})
export class Sidebar {
  api = inject(HttpClientService);
  router = inject(Router);

  logout() {
    this.api.logout().subscribe({
      next: (data) => this.router.navigateByUrl('/'),
      error: (error) => console.log(error),
    });
  }
}
